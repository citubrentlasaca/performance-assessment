import React, { useEffect, useState } from 'react'
import NavBar from '../Shared/NavBar'
import TopBarTwo from '../Shared/TopBarTwo'
import { useParams } from 'react-router-dom';
import axios from 'axios';
import { Stack } from '@mui/material';

function ViewAnswers() {
    const { teamId, assessmentId } = useParams();
    const [employees, setEmployees] = useState([]);
    const [selectedName, setSelectedName] = useState('');
    const [assessment, setAssessment] = useState(null);
    const [answers, setAnswers] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const firstResponse = await axios.get(`https://localhost:7236/api/assessments/${assessmentId}/items`);
                setAssessment(firstResponse.data);
                const items = firstResponse.data.items;

                const employeesData = [];
                for (const item of items) {
                    const secondResponse = await axios.get(`https://localhost:7236/api/answers/items/${item.id}`);
                    const answers = secondResponse.data;

                    for (const answer of answers) {
                        const thirdResponse = await axios.get(`https://localhost:7236/api/employees/${answer.employeeId}`);
                        const employeeId = thirdResponse.data.userId;

                        const fourthResponse = await axios.get(`https://localhost:7236/api/users/${employeeId}`);
                        employeesData.push(fourthResponse.data);
                    }
                }
                setEmployees(employeesData);
                setLoading(false);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        fetchData();
    }, [assessmentId]);

    const handleSelectChange = async (event) => {
        const selectedName = event.target.value;
        setSelectedName(selectedName);

        // Extract employee.firstName from the selected name
        const firstName = selectedName.split(' ')[0];

        // Find the employee with the matching firstName
        const employee = employees.find(emp => emp.firstName === firstName);

        if (employee) {
            // Make a GET request to fetch the employee data
            const employeeResponse = await axios.get(`https://localhost:7236/api/employees/users/${employee.id}`);

            // Filter the response based on teamId
            let employeeId = 0;
            employeeResponse.data.forEach(employee => {
                if (employee.teamId === Number(teamId)) {
                    employeeId = employee.id;
                }
            });

            if (employeeId !== 0) {
                const employeeIdTemp = employeeId;
                const assessmentItems = assessment.items;
                let answersArray = [];

                for (const item of assessmentItems) {
                    const itemResponse = await axios.get(`https://localhost:7236/api/answers/items/${item.id}`);
                    itemResponse.data.forEach(answer => {
                        if (answer.employeeId === employeeIdTemp) {
                            if (item.questionType === 'Short answer' || item.questionType === 'Paragraph') {
                                answersArray.push(answer.answerText);
                            } else if (item.questionType === 'Multiple choice' || item.questionType === 'Checkboxes') {
                                answersArray.push(answer.selectedChoices);
                            } else if (item.questionType === 'Counter') {
                                answersArray.push(answer.counterValue);
                            }
                        }
                    });
                }
                setAnswers(answersArray);
            }
        }
    };

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
                    justifyContent="center"
                    alignItems="center"
                    spacing={2}
                    sx={{
                        width: '100%',
                        height: '100%',
                        padding: '40px'
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
                        <select className="form-select" onChange={handleSelectChange}>
                            <option selected disabled>Select an employee</option>
                            {[...new Set(employees.map(employee => employee.firstName + ' ' + employee.lastName))].map((name, index) => (
                                <option key={index} value={name}>
                                    {name}
                                </option>
                            ))}
                        </select>
                    </Stack>
                    {assessment.items.map((item, index) => (
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
                                {item.questionType === 'Short answer' && (
                                    <input type='text' className='form-control' value={answers[index]} readOnly />
                                )}
                                {item.questionType === 'Paragraph' && (
                                    <input type='text' className='form-control' value={answers[index]} readOnly />
                                )}
                                {item.questionType === 'Multiple choice' && (
                                    <input type='text' className='form-control' value={answers[index]} readOnly />
                                )}
                                {item.questionType === 'Checkboxes' && (
                                    <input type='text' className='form-control' value={answers[index]} readOnly />
                                )}
                                {item.questionType === 'Counter' && (
                                    <input type='text' className='form-control' value={answers[index]} readOnly />
                                )}
                            </Stack>
                        </div>
                    ))}
                </Stack>
            )}
        </NavBar>
    )
}

export default ViewAnswers