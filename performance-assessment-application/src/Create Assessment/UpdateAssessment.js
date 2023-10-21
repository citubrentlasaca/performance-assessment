import React, { useState, useEffect } from 'react'
import NavBar from '../Shared/NavBar'
import { useParams, useNavigate } from 'react-router-dom'
import axios from 'axios';
import { Stack } from '@mui/material';
import QuestionBox from './QuestionBox';

function UpdateAssessment() {
    const { id } = useParams();
    const [loading, setLoading] = useState(true);
    const [title, setTitle] = useState('');
    const [description, setDescription] = useState('');
    const [showAlert, setShowAlert] = useState(false);
    const [showUpdateSuccessAlert, setShowUpdateSuccessAlert] = useState(false);
    const [deletedChoiceIds, setDeletedChoiceIds] = useState([]);
    const employeeStorage = JSON.parse(localStorage.getItem("employeeData"));
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
    const [originalQuestions, setOriginalQuestions] = useState([
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

    useEffect(() => {
        const fetchData = async () => {
            try {
                const assessmentResponse = await axios.get(`https://localhost:7236/api/assessments/${id}/items`);
                const assessmentData = assessmentResponse.data;
                const itemsData = assessmentData.items;

                const transformedQuestions = itemsData.map((item) => ({
                    id: item.id,
                    questionText: item.question,
                    questionType: item.questionType,
                    multipleChoices: item.choices
                        .filter((choice) => item.questionType === 'Multiple choice')
                        .map((choice, index) => ({
                            id: choice.id,
                            text: `Option`,
                            valueText: choice.choiceValue,
                            choiceWeight: choice.weight,
                        })),
                    checkboxesChoices: item.choices
                        .filter((choice) => item.questionType === 'Checkboxes')
                        .map((choice, index) => ({
                            id: choice.id,
                            text: `Option`,
                            valueText: choice.choiceValue,
                            choiceWeight: choice.weight,
                        })),
                    questionWeight: item.weight,
                    questionTarget: item.target,
                    questionRequired: item.required,
                }));

                setTitle(assessmentData.title);
                setDescription(assessmentData.description);
                setQuestions(transformedQuestions);
                setOriginalQuestions(transformedQuestions);
                setLoading(false);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        fetchData();
    }, [id]);

    const handleTitleChange = (e) => {
        setTitle(e.target.value);
    }

    const handleDescriptionChange = (e) => {
        setDescription(e.target.value);
    }

    async function doesChoiceExistInDatabase(choiceId) {
        try {
            await axios.get(`https://localhost:7236/api/choices/${choiceId}`);
            return true;
        } catch (error) {
            return false;
        }
    }

    const updateAssessment = async () => {
        try {
            if (totalWeight !== 100) {
                setShowAlert(true);
                setTimeout(() => {
                    setShowAlert(false);
                }, 5000);
                return;
            }

            await axios.put(`https://localhost:7236/api/assessments/${id}`, {
                title,
                description,
            });

            for (const question of questions) {
                if (originalQuestions.some((originalQuestion) => originalQuestion.id === question.id)) {
                    await axios.put(`https://localhost:7236/api/items/${question.id}`, {
                        question: question.questionText,
                        questionType: question.questionType,
                        weight: question.questionWeight,
                        target: question.questionTarget,
                        required: question.questionRequired,
                    });

                    if (question.questionType === 'Multiple choice' || question.questionType === 'Checkboxes') {
                        const choices = question.questionType === 'Multiple choice' ? question.multipleChoices : question.checkboxesChoices;

                        for (const choice of choices) {
                            const choiceExists = await doesChoiceExistInDatabase(choice.id);

                            if (choiceExists) {
                                await axios.put(`https://localhost:7236/api/choices/${choice.id}`, {
                                    choiceValue: choice.valueText,
                                    weight: choice.choiceWeight,
                                });
                            } else {
                                const choiceResponse = await axios.post('https://localhost:7236/api/choices', {
                                    choiceValue: choice.valueText,
                                    weight: choice.choiceWeight,
                                    itemId: question.id,
                                });
                                choice.id = choiceResponse.data.id;
                            }
                        }
                    }
                }
            }

            const choicesResponse = await axios.get('https://localhost:7236/api/choices');
            const choicesData = choicesResponse.data;

            for (const choiceId of deletedChoiceIds) {
                const choiceToDelete = choicesData.find((choice) => choice.id === choiceId);

                if (choiceToDelete) {
                    await axios.delete(`https://localhost:7236/api/choices/${choiceId}`);
                }
            }

            const itemsResponse = await axios.get('https://localhost:7236/api/items');
            const itemsData = itemsResponse.data;

            for (const question of questions) {
                const existingItem = itemsData.find((item) => item.id === question.id);

                if (!existingItem) {
                    const itemResponse = await axios.post('https://localhost:7236/api/items', {
                        question: question.questionText,
                        questionType: question.questionType,
                        weight: question.questionWeight,
                        target: question.questionTarget,
                        required: question.questionRequired,
                        assessmentId: id,
                    });
                    question.id = itemResponse.data.id;

                    if (question.questionType === 'Multiple choice' || question.questionType === 'Checkboxes') {
                        const choices = question.questionType === 'Multiple choice' ? question.multipleChoices : question.checkboxesChoices;
                        for (const choice of choices) {
                            const choiceResponse = await axios.post('https://localhost:7236/api/choices', {
                                choiceValue: choice.valueText,
                                weight: choice.choiceWeight,
                                itemId: question.id,
                            });
                            choice.id = choiceResponse.data.id;
                        }
                    }
                }
            }

            for (const originalQuestion of originalQuestions) {
                const found = questions.find((question) => question.id === originalQuestion.id);
                if (!found) {
                    await axios.delete(`https://localhost:7236/api/items/${originalQuestion.id}`);
                }
            }

            setShowUpdateSuccessAlert(true);
            setTimeout(() => {
                navigate(`/organizations/${employeeStorage.teamId}/admin-assessments`);
            }, 3000);
            console.log('Assessment has been updated successfully');
        } catch (error) {
            console.error('Error while updating assessment:', error);
        }
    };

    const discardChanges = () => {
        navigate(`/organizations/${employeeStorage.teamId}/admin-assessments`);
    }

    return (
        <NavBar>
            {loading ? (
                <Stack
                    justifyContent="center"
                    alignItems="center"
                    spacing={2}
                    sx={{
                        height: "100%",
                        width: "100%",
                        padding: "40px",
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
                        height: '100%',
                        padding: '40px',
                        overflowY: 'auto',
                    }}
                >
                    <Stack
                        direction="column"
                        justifyContent="flex-start"
                        alignItems="center"
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
                            {title === 'Daily Performance Report' ? (
                                <input type='text' className='form-control form-control-lg' placeholder='Untitled assessment' value={title} disabled
                                    style={{
                                        border: 'none',
                                        padding: '0px'
                                    }}
                                />
                            ) : (
                                <input type='text' className='form-control form-control-lg' placeholder='Untitled assessment' value={title} onChange={handleTitleChange}
                                    style={{
                                        border: 'none',
                                        padding: '0px'
                                    }}
                                />
                            )}
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
                        {showUpdateSuccessAlert && (
                            <div className="alert alert-success alert-dismissible fade show w-100" role="alert">
                                Assessment updated successfully.
                                <button type="button" className="btn-close" data-bs-dismiss="alert" aria-label="Close" onClick={() => setShowUpdateSuccessAlert(false)}></button>
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
                            <button type="button" className="btn btn-success" onClick={updateAssessment}
                                style={{
                                    width: '200px',
                                    marginBottom: '40px'
                                }}
                            >Update Assessment</button>
                            <button type="button" className="btn btn-danger" onClick={discardChanges}
                                style={{
                                    width: '200px',
                                    marginBottom: '40px'
                                }}
                            >Discard Changes</button>
                        </Stack>
                    </Stack>
                </Stack>
            )}
        </NavBar>
    )
}

export default UpdateAssessment