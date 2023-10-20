import React, { useState } from 'react'
import NavBar from '../Shared/NavBar'
import { Stack } from '@mui/material'
import QuestionBox from './QuestionBox'
import axios from 'axios';
import { useNavigate } from "react-router-dom";

function CreateAssessment() {
    const [title, setTitle] = useState('');
    const [description, setDescription] = useState('');
    const [showAlert, setShowAlert] = useState(false);
    const [showPublishSuccessAlert, setShowPublishSuccessAlert] = useState(false);
    const [deletedChoiceIds, setDeletedChoiceIds] = useState([]);
    const employee = JSON.parse(localStorage.getItem("employeeData"));
    const [questions, setQuestions] = useState([
        {
            id: 1,
            questionText: '',
            questionType: 'Multiple choice',
            multipleChoices: [
                {
                    id: 1,
                    text: 'Option',
                    valueText: '',
                    choiceWeight: 0,
                },
            ],
            checkboxesChoices: [
                {
                    id: 1,
                    text: 'Option',
                    valueText: '',
                    choiceWeight: 0,
                },
            ],
            questionWeight: 0,
            questionTarget: 0,
            questionRequired: false,
        },
    ]);
    const totalWeight = questions.reduce((accumulator, question) => accumulator + parseInt(question.questionWeight), 0);
    const navigate = useNavigate();

    const handleTitleChange = (e) => {
        setTitle(e.target.value);
    }

    const handleDescriptionChange = (e) => {
        setDescription(e.target.value);
    }

    const updateDeletedChoiceIds = (newDeletedChoiceIds) => {
        setDeletedChoiceIds(newDeletedChoiceIds);
    };

    async function postAssessment() {
        try {
            if (totalWeight !== 100) {
                setShowAlert(true);
                setTimeout(() => {
                    setShowAlert(false);
                }, 5000);
                return;
            }

            const assessmentResponse = await axios.post('https://localhost:7236/api/assessments', {
                employeeId: employee.id,
                title,
                description,
            });
            const assessmentId = assessmentResponse.data.id;

            for (const question of questions) {
                const itemResponse = await axios.post('https://localhost:7236/api/items', {
                    question: question.questionText,
                    questionType: question.questionType,
                    weight: question.questionWeight,
                    target: question.questionTarget,
                    required: question.questionRequired,
                    assessmentId,
                });
                const itemId = itemResponse.data.id;

                if (question.questionType === 'Multiple choice' || question.questionType === 'Checkboxes') {
                    const choices = question.questionType === 'Multiple choice'
                        ? question.multipleChoices
                        : question.checkboxesChoices;

                    for (const choice of choices) {
                        await axios.post('https://localhost:7236/api/choices', {
                            choiceValue: choice.valueText,
                            weight: choice.choiceWeight,
                            itemId,
                        });
                    }
                }
            }

            console.log('Assessment has been posted successfully');
            setShowPublishSuccessAlert(true);
            setTimeout(() => {
                navigate(`/organizations/${employee.teamId}/admin-assessments`);
            }, 3000);
        } catch (error) {
            console.error('Error while posting assessment:', error);
        }
    }

    const discardAssessment = () => {
        navigate(`/organizations/${employee.teamId}/admin-assessments`);
    }

    return (
        <NavBar>
            <Stack
                direction="column"
                justifyContent="flex-start"
                alignItems="center"
                spacing={2}
                sx={{
                    width: '100%',
                    height: '100%',
                    padding: '40px'
                }}
            >
                <Stack
                    direction="column"
                    justifyContent="center"
                    alignItems="flex-start"
                    spacing={2}
                    sx={{
                        width: '75%',
                        height: '100%',
                    }}
                >
                    <div className='gap-2'
                        style={{
                            width: '100%',
                            height: '140px',
                            backgroundColor: 'white',
                            borderRadius: '10px',
                            borderTop: '10px solid #27c6d9',
                            display: 'flex',
                            flexDirection: 'column',
                            alignItems: 'flex-start',
                            justifyContent: 'center',
                            padding: '20px'
                        }}
                    >
                        <input type='text' className='form-control form-control-lg' placeholder='Untitled assessment' value={title} onChange={handleTitleChange}
                            style={{
                                border: 'none',
                                padding: '0px'
                            }}
                        />
                        <input type='text' className='form-control' placeholder='Assessment description' value={description} onChange={handleDescriptionChange}
                            style={{
                                border: 'none',
                                padding: '0px'
                            }}
                        />
                    </div>
                    <QuestionBox questions={questions} setQuestions={setQuestions} deletedChoiceIds={deletedChoiceIds} setDeletedChoiceIds={setDeletedChoiceIds} />
                    {showAlert && (
                        <div className="alert alert-danger alert-dismissible fade show w-100" role="alert">
                            Total weight must be equal to 100.
                            <button type="button" className="btn-close" data-bs-dismiss="alert" aria-label="Close" onClick={() => setShowAlert(false)}></button>
                        </div>
                    )}
                    {showPublishSuccessAlert && (
                        <div className="alert alert-success alert-dismissible fade show w-100" role="alert">
                            Assessment published successfully.
                            <button type="button" className="btn-close" data-bs-dismiss="alert" aria-label="Close" onClick={() => setShowPublishSuccessAlert(false)}></button>
                        </div>
                    )}
                    <Stack
                        direction="row"
                        justifyContent="center"
                        alignItems="center"
                        spacing={2}
                        sx={{
                            width: '100%',
                        }}
                    >
                        <button type="button" className="btn btn-success" onClick={postAssessment}
                            style={{
                                width: '200px'
                            }}
                        >Publish Assessment</button>
                        <button type="button" className="btn btn-danger" onClick={discardAssessment}
                            style={{
                                width: '200px'
                            }}
                        >Discard Assessment</button>
                    </Stack>
                </Stack>
            </Stack>
        </NavBar>
    )
}

export default CreateAssessment