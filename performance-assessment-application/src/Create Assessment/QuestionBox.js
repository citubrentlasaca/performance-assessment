import { Stack } from '@mui/material'
import React from 'react'

function QuestionBox({ questions, setQuestions }) {
    const duplicateQuestionBox = () => {
        const newQuestionId = questions.length + 1;
        const newQuestion = {
            id: newQuestionId,
            questionText: '',
            questionType: 'Multiple choice',
            multipleChoices: [
                {
                    id: 1,
                    text: 'Option 1',
                    valueText: '',
                    choiceWeight: 0,
                },
            ],
            checkboxesChoices: [
                {
                    id: 1,
                    text: 'Option 1',
                    valueText: '',
                    choiceWeight: 0,
                },
            ],
            questionWeight: 0,
            questionTarget: 0,
            questionRequired: false,
        };
        setQuestions([...questions, newQuestion]);
    };

    const deleteQuestionBox = (questionId) => {
        const updatedQuestions = questions.filter((question) => question.id !== questionId);
        setQuestions(updatedQuestions);
    };

    const handleQuestionTextChange = (e, questionId) => {
        const updatedQuestions = questions.map((question) => {
            if (question.id === questionId) {
                return {
                    ...question,
                    questionText: e.target.value,
                };
            }
            return question;
        });
        setQuestions(updatedQuestions);
    };

    const handleQuestionTypeChange = (e, questionId) => {
        const updatedQuestions = questions.map((question) => {
            if (question.id === questionId) {
                return {
                    ...question,
                    questionType: e.target.value,
                };
            }
            return question;
        });
        setQuestions(updatedQuestions);
    };

    const handleQuestionWeightChange = (e, questionId) => {
        const updatedQuestions = questions.map((question) => {
            if (question.id === questionId) {
                return {
                    ...question,
                    questionWeight: e.target.value,
                };
            }
            return question;
        });
        setQuestions(updatedQuestions);
    };

    const handleQuestionTargetChange = (e, questionId) => {
        const updatedQuestions = questions.map((question) => {
            if (question.id === questionId) {
                return {
                    ...question,
                    questionTarget: e.target.value,
                };
            }
            return question;
        });
        setQuestions(updatedQuestions);
    };

    const handleQuestionRequiredChange = (e, questionId) => {
        const updatedQuestions = questions.map((question) => {
            if (question.id === questionId) {
                return {
                    ...question,
                    questionRequired: e.target.checked,
                };
            }
            return question;
        });
        setQuestions(updatedQuestions);
    };

    const handleChoiceTextChange = (e, questionId, choiceId) => {
        const updatedQuestions = questions.map((question) => {
            if (question.id === questionId) {
                if (question.questionType === 'Multiple choice') {
                    const updatedChoices = question.multipleChoices.map((choice) => {
                        if (choice.id === choiceId) {
                            return {
                                ...choice,
                                valueText: e.target.value,
                            };
                        }
                        return choice;
                    });
                    return {
                        ...question,
                        multipleChoices: updatedChoices,
                    };
                }
                else if (question.questionType === 'Checkboxes') {
                    const updatedChoices = question.checkboxesChoices.map((choice) => {
                        if (choice.id === choiceId) {
                            return {
                                ...choice,
                                valueText: e.target.value,
                            };
                        }
                        return choice;
                    });
                    return {
                        ...question,
                        checkboxesChoices: updatedChoices,
                    };
                }
            }
            return question;
        });
        setQuestions(updatedQuestions);
    };

    const handleChoiceWeightChange = (e, questionId, choiceId) => {
        const updatedQuestions = questions.map((question) => {
            if (question.id === questionId) {
                if (question.questionType === 'Multiple choice') {
                    const updatedChoices = question.multipleChoices.map((choice) => {
                        if (choice.id === choiceId) {
                            return {
                                ...choice,
                                choiceWeight: e.target.value,
                            };
                        }
                        return choice;
                    });
                    return {
                        ...question,
                        multipleChoices: updatedChoices,
                    };
                }
                else if (question.questionType === 'Checkboxes') {
                    const updatedChoices = question.checkboxesChoices.map((choice) => {
                        if (choice.id === choiceId) {
                            return {
                                ...choice,
                                choiceWeight: e.target.value,
                            };
                        }
                        return choice;
                    });
                    return {
                        ...question,
                        checkboxesChoices: updatedChoices,
                    };
                }
            }
            return question;
        });
        setQuestions(updatedQuestions);
    };

    const addChoice = (questionId) => {
        const updatedQuestions = questions.map((question) => {
            if (question.id === questionId) {
                if (question.questionType === 'Multiple choice') {
                    const newChoiceId = question.multipleChoices.length + 1;
                    const newChoice = {
                        id: newChoiceId,
                        text: `Option ${newChoiceId}`,
                        valueText: '',
                        choiceWeight: 0,
                    };
                    return {
                        ...question,
                        multipleChoices: [...question.multipleChoices, newChoice],
                    };
                }
                else if (question.questionType === 'Checkboxes') {
                    const newChoiceId = question.checkboxesChoices.length + 1;
                    const newChoice = {
                        id: newChoiceId,
                        text: `Option ${newChoiceId}`,
                        valueText: '',
                        choiceWeight: 0,
                    };
                    return {
                        ...question,
                        checkboxesChoices: [...question.checkboxesChoices, newChoice],
                    };
                }
            }
            return question;
        });
        setQuestions(updatedQuestions);
    };

    const deleteChoice = (questionId, choiceId) => {
        const updatedQuestions = questions.map((question) => {
            if (question.id === questionId) {
                if (question.questionType === 'Multiple choice') {
                    const updatedChoices = question.multipleChoices.filter(
                        (choice) => choice.id !== choiceId
                    );
                    return {
                        ...question,
                        multipleChoices: updatedChoices,
                    };
                }
                else if (question.questionType === 'Checkboxes') {
                    const updatedChoices = question.checkboxesChoices.filter(
                        (choice) => choice.id !== choiceId
                    );
                    return {
                        ...question,
                        checkboxesChoices: updatedChoices,
                    };
                }
            }
            return question;
        });
        setQuestions(updatedQuestions);
    };


    return (
        <>
            {questions.map((question) => (
                <Stack key={question.id}
                    direction="row"
                    justifyContent="center"
                    alignItems="flex-start"
                    spacing={2}
                    sx={{
                        width: '100%',
                    }}
                >

                    <div className='gap-2'
                        style={{
                            width: '100%',
                            height: 'fit-content',
                            backgroundColor: 'white',
                            borderRadius: '10px',
                            display: 'flex',
                            flexDirection: 'column',
                            alignItems: 'flex-start',
                            justifyContent: 'center',
                            padding: '20px'
                        }}
                    >
                        <Stack
                            direction="row"
                            justifyContent="flex-start"
                            alignItems="center"
                            spacing={2}
                            sx={{
                                width: '100%',
                            }}
                        >
                            <input type='text' className='form-control w-75' placeholder='Untitled Question' value={question.questionText} onChange={(e) => handleQuestionTextChange(e, question.id)} />
                            <select class="form-select w-25" value={question.questionType} onChange={(e) => handleQuestionTypeChange(e, question.id)}>
                                <option value="Short answer">Short answer</option>
                                <option value="Paragraph">Paragraph</option>
                                <option value="Multiple choice">Multiple choice</option>
                                <option value="Checkboxes">Checkboxes</option>
                                <option value="Counter">Counter</option>
                            </select>
                        </Stack>
                        <Stack
                            direction="row"
                            justifyContent="flex-start"
                            alignItems="center"
                            spacing={2}
                            sx={{
                                width: '100%',
                            }}
                        >
                            {question.questionType === 'Short answer' &&
                                <input type='text' className='form-control' placeholder='Short answer text' disabled
                                    style={{
                                        border: 'none',
                                    }}
                                />
                            }
                            {question.questionType === 'Paragraph' &&
                                <input type='text' className='form-control' placeholder='Long answer text' disabled
                                    style={{
                                        border: 'none',
                                    }}
                                />
                            }
                            {question.questionType === 'Multiple choice' &&
                                <Stack
                                    direction="column"
                                    justifyContent="center"
                                    alignItems="flex-start"
                                    sx={{
                                        width: '100%',
                                    }}
                                >
                                    {question.multipleChoices.map((choice) => (
                                        <div
                                            key={choice.id}
                                            className="form-check w-100 gap-2"
                                            style={{
                                                display: 'flex',
                                                flexDirection: 'row',
                                                alignItems: 'center',
                                                justifyContent: 'center',
                                            }}
                                        >
                                            <input disabled
                                                className="form-check-input"
                                                type="radio"
                                                name="flexRadioDefault"
                                                style={{
                                                    marginBottom: '5px',
                                                }}
                                            />
                                            <input
                                                type="text"
                                                className="form-control"
                                                placeholder={choice.text}
                                                id={`multipleChoice${choice.id}`}
                                                value={choice.valueText}
                                                onChange={(e) => handleChoiceTextChange(e, question.id, choice.id)}
                                                style={{
                                                    border: 'none',
                                                    width: '90%'
                                                }}
                                            />
                                            <input type="number" className="form-control" id={`choiceWeight${choice.id}`} value={choice.choiceWeight} onChange={(e) => handleChoiceWeightChange(e, question.id, choice.id)}
                                                style={{
                                                    width: '10%'
                                                }}
                                            />
                                            <button type='button' className='btn mt-0 p-0' onClick={() => deleteChoice(question.id, choice.id)}>
                                                <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-x" viewBox="0 0 16 16">
                                                    <path d="M4.646 4.646a.5.5 0 0 1 .708 0L8 7.293l2.646-2.647a.5.5 0 0 1 .708.708L8.707 8l2.647 2.646a.5.5 0 0 1-.708.708L8 8.707l-2.646 2.647a.5.5 0 0 1-.708-.708L7.293 8 4.646 5.354a.5.5 0 0 1 0-.708z" />
                                                </svg>
                                            </button>
                                        </div>
                                    ))}
                                    <button type='button' className='btn mt-0 p-0' onClick={() => addChoice(question.id)}>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-plus" viewBox="0 0 16 16">
                                            <path d="M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4z" />
                                        </svg>
                                    </button>
                                </Stack>
                            }
                            {question.questionType === 'Checkboxes' &&
                                <Stack
                                    direction="column"
                                    justifyContent="center"
                                    alignItems="flex-start"
                                    sx={{
                                        width: '100%',
                                    }}
                                >
                                    {question.checkboxesChoices.map((choice) => (
                                        <div
                                            key={choice.id}
                                            className="form-check w-100 gap-2"
                                            style={{
                                                display: 'flex',
                                                flexDirection: 'row',
                                                alignItems: 'center',
                                                justifyContent: 'center',
                                            }}
                                        >
                                            <input disabled
                                                className="form-check-input"
                                                type="checkbox"
                                                name="flexRadioDefault"
                                                style={{
                                                    marginBottom: '5px',
                                                }}
                                            />
                                            <input
                                                type="text"
                                                className="form-control"
                                                placeholder={choice.text}
                                                id={`checkboxChoice${choice.id}`}
                                                value={choice.valueText}
                                                onChange={(e) => handleChoiceTextChange(e, question.id, choice.id)}
                                                style={{
                                                    border: 'none',
                                                    width: '90%'
                                                }}
                                            />
                                            <input type="number" className="form-control" id={`choiceWeight${choice.id}`} value={choice.choiceWeight} onChange={(e) => handleChoiceWeightChange(e, question.id, choice.id)}
                                                style={{
                                                    width: '10%'
                                                }}
                                            />
                                            <button type='button' className='btn mt-0 p-0' onClick={() => deleteChoice(question.id, choice.id)}>
                                                <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-x" viewBox="0 0 16 16">
                                                    <path d="M4.646 4.646a.5.5 0 0 1 .708 0L8 7.293l2.646-2.647a.5.5 0 0 1 .708.708L8.707 8l2.647 2.646a.5.5 0 0 1-.708.708L8 8.707l-2.646 2.647a.5.5 0 0 1-.708-.708L7.293 8 4.646 5.354a.5.5 0 0 1 0-.708z" />
                                                </svg>
                                            </button>
                                        </div>
                                    ))}
                                    <button type='button' className='btn mt-0 p-0' onClick={() => addChoice(question.id)}>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-plus" viewBox="0 0 16 16">
                                            <path d="M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4z" />
                                        </svg>
                                    </button>
                                </Stack>
                            }
                            {question.questionType === 'Counter' &&
                                <Stack
                                    direction="row"
                                    justifyContent="center"
                                    alignItems="center"
                                    spacing={2}
                                    sx={{
                                        width: '100%',
                                    }}
                                >
                                    <button type='button' className='btn' disabled
                                        style={{
                                            border: 'none'
                                        }}
                                    >
                                        <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-dash-square" viewBox="0 0 16 16">
                                            <path d="M14 1a1 1 0 0 1 1 1v12a1 1 0 0 1-1 1H2a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1h12zM2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2z" />
                                            <path d="M4 8a.5.5 0 0 1 .5-.5h7a.5.5 0 0 1 0 1h-7A.5.5 0 0 1 4 8z" />
                                        </svg>
                                    </button>
                                    <input type="number" className="form-control" disabled />
                                    <button type='button' className='btn' disabled
                                        style={{
                                            border: 'none'
                                        }}
                                    >
                                        <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-plus-square" viewBox="0 0 16 16">
                                            <path d="M14 1a1 1 0 0 1 1 1v12a1 1 0 0 1-1 1H2a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1h12zM2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2z" />
                                            <path d="M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4z" />
                                        </svg>
                                    </button>
                                </Stack>
                            }
                        </Stack>
                        <hr
                            style={{
                                width: '100%',
                                height: '1px',
                                backgroundColor: 'black',
                            }}
                        />
                        <Stack
                            direction="row"
                            justifyContent="space-between"
                            alignItems="center"
                            spacing={2}
                            sx={{
                                width: '100%',
                            }}
                        >
                            <Stack
                                direction="row"
                                justifyContent="center"
                                alignItems="center"
                                spacing={2}
                            >
                                <Stack
                                    direction="row"
                                    justifyContent="center"
                                    alignItems="center"
                                    spacing={2}
                                    sx={{
                                        width: 'fit-content',
                                    }}
                                >
                                    <p className='mb-0'>Weight value (0-100%):</p>
                                    <input type="number" className="form-control" value={question.questionWeight} onChange={(e) => handleQuestionWeightChange(e, question.id)}
                                        style={{
                                            width: '75px'
                                        }}
                                    />
                                </Stack>
                                <Stack
                                    direction="row"
                                    justifyContent="center"
                                    alignItems="center"
                                    spacing={2}
                                    sx={{
                                        width: 'fit-content',
                                    }}
                                >
                                    <p className='mb-0'>Target value (0-100):</p>
                                    <input type="number" className="form-control" value={question.questionTarget} onChange={(e) => handleQuestionTargetChange(e, question.id)}
                                        style={{
                                            width: '75px'
                                        }}
                                    />
                                </Stack>
                            </Stack>
                            <Stack
                                direction="row"
                                justifyContent="center"
                                alignItems="center"
                                spacing={2}
                            >
                                <button type='button' className='btn' onClick={() => deleteQuestionBox(question.id)}>
                                    <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-trash3" viewBox="0 0 16 16">
                                        <path d="M6.5 1h3a.5.5 0 0 1 .5.5v1H6v-1a.5.5 0 0 1 .5-.5ZM11 2.5v-1A1.5 1.5 0 0 0 9.5 0h-3A1.5 1.5 0 0 0 5 1.5v1H2.506a.58.58 0 0 0-.01 0H1.5a.5.5 0 0 0 0 1h.538l.853 10.66A2 2 0 0 0 4.885 16h6.23a2 2 0 0 0 1.994-1.84l.853-10.66h.538a.5.5 0 0 0 0-1h-.995a.59.59 0 0 0-.01 0H11Zm1.958 1-.846 10.58a1 1 0 0 1-.997.92h-6.23a1 1 0 0 1-.997-.92L3.042 3.5h9.916Zm-7.487 1a.5.5 0 0 1 .528.47l.5 8.5a.5.5 0 0 1-.998.06L5 5.03a.5.5 0 0 1 .47-.53Zm5.058 0a.5.5 0 0 1 .47.53l-.5 8.5a.5.5 0 1 1-.998-.06l.5-8.5a.5.5 0 0 1 .528-.47ZM8 4.5a.5.5 0 0 1 .5.5v8.5a.5.5 0 0 1-1 0V5a.5.5 0 0 1 .5-.5Z" />
                                    </svg>
                                </button>
                                <hr
                                    style={{
                                        width: '1px',
                                        height: '25px',
                                        backgroundColor: 'black',
                                    }}
                                />
                                <Stack
                                    direction="row"
                                    justifyContent="center"
                                    alignItems="center"
                                    spacing={2}
                                    sx={{
                                        width: 'fit-content',
                                    }}
                                >
                                    <p className='mb-0'>Required</p>
                                    <div class="form-check form-switch">
                                        <input class="form-check-input" type="checkbox" role="switch" checked={question.questionRequired} onChange={(e) => handleQuestionRequiredChange(e, question.id)}
                                            style={{
                                                border: '1px solid #27c6d9',
                                                backgroundColor: '#27c6d9',
                                            }}
                                        />
                                    </div>
                                </Stack>
                            </Stack>
                        </Stack>
                    </div >
                    <div
                        style={{
                            width: 'fit-content',
                            height: 'fit-content',
                            backgroundColor: 'white',
                            borderRadius: '10px',
                            display: 'flex',
                            flexDirection: 'column',
                            alignItems: 'center',
                            justifyContent: 'center',
                            padding: '20px'
                        }}
                    >
                        <button type='button' className='btn mt-0 p-0' onClick={duplicateQuestionBox}>
                            <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-plus-circle" viewBox="0 0 16 16">
                                <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14zm0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16z" />
                                <path d="M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4z" />
                            </svg>
                        </button>
                    </div>
                </Stack>
            ))
            }
        </>
    )
}

export default QuestionBox