import React, { useEffect, useState } from 'react'
import NavBar from '../Shared/NavBar'
import TopBarTwo from '../Shared/TopBarTwo'
import { useParams } from 'react-router-dom';
import axios from 'axios';
import { Stack } from '@mui/material';

function ViewAnswers() {
    const { assessmentId } = useParams();
    const [assessment, setAssessment] = useState(null);
    const [items, setItems] = useState(null);
    const [loading, setLoading] = useState(true);
    const [answers, setAnswers] = useState([]);
    const [selectedEmployee, setSelectedEmployee] = useState("Choose employee");
    const [selectedAnswers, setSelectedAnswers] = useState([]);
    const [score, setScore] = useState(0);

    const handleEmployeeSelect = (event) => {
        setSelectedEmployee(event.target.value);
        for (const answer of answers) {
            if (answer.id === Number(event.target.value)) {
                setSelectedAnswers(answer.answers);
                setScore(answer.score * 100);
            }
        }
    };

    useEffect(() => {
        const fetchData = async () => {
            try {
                const assessmentResponse = await axios.get(`https://workpa.azurewebsites.net/api/assessments/${assessmentId}`);
                const itemsTemp = [];
                const itemsResponse = await axios.get(`https://workpa.azurewebsites.net/api/items`);
                for (const item of itemsResponse.data) {
                    if (item.assessmentId === Number(assessmentId)) {
                        itemsTemp.push(item);
                    }
                }
                const resultResponse = await axios.get(`https://workpa.azurewebsites.net/api/results/assessments/${assessmentResponse.data.id}`);
                const answersTemp = [];

                for (const result of resultResponse.data) {
                    const employeeResponse = await axios.get(`https://workpa.azurewebsites.net/api/employees/${result.employeeId}`);
                    const userResponse = await axios.get(`https://workpa.azurewebsites.net/api/users/${employeeResponse.data.userId}`);
                    answersTemp.push({
                        id: result.id,
                        name: userResponse.data.firstName + ' ' + userResponse.data.lastName,
                        date: result.dateTimeCreated,
                        score: result.score,
                        answers: [],
                    });
                }

                for (const item of itemsTemp) {
                    const answerResponse = await axios.get(`https://workpa.azurewebsites.net/api/answers/items/${item.id}`);
                    for (const answer of answerResponse.data) {
                        for (const answerTemp of answersTemp) {
                            if (answer.resultId === answerTemp.id) {
                                if (item.questionType === 'Short answer' || item.questionType === 'Paragraph') {
                                    answerTemp.answers.push(answer.answerText);
                                }
                                else if (item.questionType === 'Multiple choice' || item.questionType === 'Checkboxes') {
                                    answerTemp.answers.push(answer.selectedChoices);
                                }
                                else if (item.questionType === 'Counter') {
                                    answerTemp.answers.push(answer.counterValue);
                                }
                            }
                        }
                    }
                }

                setAssessment(assessmentResponse.data);
                setItems(itemsTemp);
                setAnswers(answersTemp);
                setLoading(false);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        fetchData();
    }, [assessmentId]);

    function formatDate(dateString) {
        const date = new Date(dateString);
        const options = {
            year: 'numeric',
            month: 'long',
            day: 'numeric',
        };
        const dateStr = date.toLocaleDateString('en-US', options);
        const timeStr = date.toLocaleTimeString('en-US', {
            hour: 'numeric',
            minute: '2-digit',
        });
        return `${dateStr} at ${timeStr}`;
    }

    function getScoreColor(score) {
        if (score >= 0 && score < 60) {
            return 'red';
        } else if (score >= 60 && score < 75) {
            return 'orange';
        } else if (score >= 75 && score <= 100) {
            return 'green';
        }

        return 'black';
    }


    return (
        <NavBar>
            <TopBarTwo />
            {loading ? (
                <Stack
                    justifyContent="center"
                    alignItems="center"
                    spacing={2}
                    sx={{
                        height: "100%",
                        width: "100%"
                    }}
                >
                    <div className="spinner-border" role="status">
                        <span className="visually-hidden">Loading...</span>
                    </div>
                </Stack>
            ) : (
                <Stack
                    direction="column"
                    justifyContent="flex-start"
                    alignItems="center"
                    spacing={2}
                    sx={{
                        width: '100%',
                        height: 'calc(100% - 100px)',
                        padding: '40px',
                        overflowY: 'auto',
                    }}
                >
                    <div
                        style={{
                            width: '75%',
                            height: 'fit-content',
                            backgroundColor: 'white',
                            padding: '20px',
                            borderRadius: '10px',
                            borderTop: "10px solid #27c6d9",
                        }}
                    >
                        <h1 className='mb-0'>{assessment.title}</h1>
                        <hr
                            style={{
                                width: "100%",
                                height: "2px",
                                backgroundColor: "black",
                            }}
                        />
                        <h6>{assessment.description}</h6>
                    </div>
                    <Stack
                        direction="row"
                        justifyContent="space-between"
                        alignItems="center"
                        spacing={2}
                        sx={{
                            width: '75%',
                        }}
                    >
                        <select className="form-select" value={selectedEmployee} onChange={handleEmployeeSelect}>
                            <option value="Choose employee" disabled>Choose employee</option>
                            {answers.map((answer, index) => (
                                <option key={index} value={answer.id}>
                                    {answer.name} | {formatDate(answer.date)}
                                </option>
                            )
                            )}
                        </select>
                        <Stack className='w-25'
                            direction="row"
                            justifyContent="center"
                            alignItems="center"
                            spacing={2}
                        >
                            <p className='mb-0'>Score:</p>
                            <input type='text' className='form-control' value={score.toFixed(0) + '%'} disabled
                                style={{
                                    textAlign: 'center',
                                    color: getScoreColor(score)
                                }}
                            />
                        </Stack>
                    </Stack>
                    {items.map((item, index) => (
                        <div key={index}
                            style={{
                                width: '75%',
                                height: 'fit-content',
                                backgroundColor: 'white',
                                padding: '20px',
                                borderRadius: '10px',
                            }}
                        >
                            <p>{item.question}</p>
                            <Stack
                                direction="row"
                                justifyContent="flex-start"
                                alignItems="center"
                                spacing={2}
                                sx={{
                                    width: '100%',
                                }}
                            >
                                <p className='mb-0'>Answer:</p>
                                <input type='text' className='form-control' value={selectedAnswers[index]} disabled />
                            </Stack>
                        </div>
                    ))}
                </Stack>
            )}
        </NavBar>
    )
}

export default ViewAnswers