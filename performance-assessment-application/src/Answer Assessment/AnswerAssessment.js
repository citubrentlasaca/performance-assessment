import React, { useState, useEffect } from 'react';
import NavBar from '../Shared/NavBar'
import { Stack } from '@mui/material'
import { useParams } from 'react-router-dom'

function AnswerAssessment() {
    const { id } = useParams();
    const [assessmentData, setAssessmentData] = useState(null);
    const [itemData, setItemData] = useState(null);
    const [choiceData, setChoiceData] = useState(null);
    const [loading, setLoading] = useState(true);
    const [counterValues, setCounterValues] = useState({});

    const handleIncrement = (itemId) => {
        setCounterValues((prevValues) => ({
            ...prevValues,
            [itemId]: (prevValues[itemId] || 0) + 1,
        }));
    };

    const handleDecrement = (itemId) => {
        if (counterValues[itemId] > 0) {
            setCounterValues((prevValues) => ({
                ...prevValues,
                [itemId]: (prevValues[itemId] || 0) - 1,
            }));
        }
    };

    useEffect(() => {
        const fetchData = async () => {
            try {
                const assessmentResponse = await fetch(`https://localhost:7236/api/assessments/${id}`);
                const assessmentData = await assessmentResponse.json();
                setAssessmentData(assessmentData);

                const itemResponse = await fetch(`https://localhost:7236/api/items`);
                const itemData = await itemResponse.json();
                const filteredItems = itemData.filter(item => item.assessmentId === Number(id));
                setItemData(filteredItems);

                const choiceResponse = await fetch(`https://localhost:7236/api/choices`);
                const choiceData = await choiceResponse.json();
                const choicesByItemId = {};
                filteredItems.forEach(item => {
                    const itemId = item.id;
                    const choicesForItem = choiceData.filter(choice => choice.itemId === itemId);
                    choicesByItemId[itemId] = choicesForItem;
                });
                setChoiceData(choicesByItemId);

                setLoading(false);
            } catch (error) {
                console.error(`Error fetching data:`, error);
                setLoading(false);
            }
        };

        fetchData();
    }, [id]);


    return (
        <NavBar>
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
                    <div class="spinner-border" role="status">
                        <span class="visually-hidden">Loading...</span>
                    </div>
                </Stack>
            ) : (
                <Stack
                    direction="column"
                    justifyContent="flex-start"
                    alignItems="center"
                    spacing={2}
                    sx={{
                        padding: '40px'
                    }}
                >
                    <div
                        style={{
                            height: "200px",
                            width: "700px",
                            backgroundColor: "white",
                            borderRadius: "10px",
                            borderTop: "10px solid #27c6d9",
                            display: 'flex',
                            justifyContent: 'start',
                            alignItems: 'center',
                            padding: '20px'
                        }}
                    >
                        <Stack
                            direction="column"
                            justifyContent="center"
                            alignItems="flex-start"
                            spacing={2}
                            sx={{
                                width: "100%",
                                height: "100%",
                            }}
                        >
                            <h1 class='mb-0'>{assessmentData.title}</h1>
                            <hr
                                style={{
                                    width: "100%",
                                    height: "2px",
                                    backgroundColor: "black",
                                }}
                            />
                            <h6>{assessmentData.description}</h6>
                        </Stack>
                    </div>
                    {itemData.map((item, index) => (
                        <div key={index}>
                            {item.questionType === "Short answer" && (
                                <div
                                    style={{
                                        height: "fit-content",
                                        width: "700px",
                                        backgroundColor: "white",
                                        borderRadius: "10px",
                                        display: 'flex',
                                        justifyContent: 'start',
                                        alignItems: 'center',
                                        padding: '20px'
                                    }}
                                >
                                    <Stack
                                        direction="column"
                                        justifyContent="center"
                                        alignItems="flex-start"
                                        spacing={2}
                                        sx={{
                                            width: "100%",
                                            height: "100%",
                                        }}
                                    >
                                        <p className='mb-0'>{item.question}</p>
                                        <input
                                            type='text'
                                            placeholder='Your answer'
                                            style={{
                                                border: "none",
                                                borderBottom: "1px solid black",
                                            }}
                                        />
                                    </Stack>
                                </div>
                            )}
                            {item.questionType === "Paragraph" && (
                                <div
                                    style={{
                                        height: "fit-content",
                                        width: "700px",
                                        backgroundColor: "white",
                                        borderRadius: "10px",
                                        display: 'flex',
                                        justifyContent: 'start',
                                        alignItems: 'center',
                                        padding: '20px'
                                    }}
                                >
                                    <Stack
                                        direction="column"
                                        justifyContent="center"
                                        alignItems="flex-start"
                                        spacing={2}
                                        sx={{
                                            width: "100%",
                                            height: "100%",
                                        }}
                                    >
                                        <p className='mb-0'>{item.question}</p>
                                        <textarea
                                            style={{
                                                width: "100%",
                                                height: "100px"
                                            }}
                                        />
                                    </Stack>
                                </div>
                            )}
                            {item.questionType === "Counter" && (
                                <div
                                    style={{
                                        height: "fit-content",
                                        width: "700px",
                                        backgroundColor: "white",
                                        borderRadius: "10px",
                                        display: 'flex',
                                        justifyContent: 'start',
                                        alignItems: 'center',
                                        padding: '20px'
                                    }}
                                >
                                    <Stack
                                        direction="column"
                                        justifyContent="center"
                                        alignItems="flex-start"
                                        spacing={2}
                                        sx={{
                                            width: "100%",
                                            height: "100%",
                                        }}
                                    >
                                        <p className='mb-0'>{item.question}</p>
                                        <Stack
                                            direction="row"
                                            justifyContent="center"
                                            alignItems="center"
                                            spacing={2}
                                            sx={{
                                                width: "100%",
                                            }}
                                        >
                                            <button type='btn' class='btn' onClick={() => handleDecrement(item.id)}>
                                                <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="#27c6d9" class="bi bi-dash-circle-fill" viewBox="0 0 16 16">
                                                    <path d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0zM4.5 7.5a.5.5 0 0 0 0 1h7a.5.5 0 0 0 0-1h-7z" />
                                                </svg>
                                            </button>
                                            <input type='number' value={counterValues[item.id] || 0}
                                                style={{
                                                    textAlign: 'center',
                                                }}
                                            />
                                            <button type='btn' class='btn' onClick={() => handleIncrement(item.id)}>
                                                <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="#27c6d9" class="bi bi-plus-circle-fill" viewBox="0 0 16 16">
                                                    <path d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0zM8.5 4.5a.5.5 0 0 0-1 0v3h-3a.5.5 0 0 0 0 1h3v3a.5.5 0 0 0 1 0v-3h3a.5.5 0 0 0 0-1h-3v-3z" />
                                                </svg>
                                            </button>
                                        </Stack>
                                    </Stack>
                                </div>
                            )}
                            {item.questionType === "Multiple choice" && (
                                <div
                                    style={{
                                        height: "fit-content",
                                        width: "700px",
                                        backgroundColor: "white",
                                        borderRadius: "10px",
                                        display: 'flex',
                                        justifyContent: 'start',
                                        alignItems: 'center',
                                        padding: '20px'
                                    }}
                                >
                                    <Stack
                                        direction="column"
                                        justifyContent="center"
                                        alignItems="flex-start"
                                        spacing={2}
                                        sx={{
                                            width: "100%",
                                            height: "100%",
                                        }}
                                    >
                                        <p className='mb-0'>{item.question}</p>
                                        {choiceData[item.id] &&
                                            choiceData[item.id].map((choice, choiceIndex) => (
                                                <div key={choiceIndex} className="form-check">
                                                    <input className="form-check-input" type="radio" name={`flexRadioDefault${item.id}`} id={`flexRadioDefault${item.id}${choiceIndex}`} />
                                                    <label className="form-check-label" htmlFor={`flexRadioDefault${item.id}${choiceIndex}`}>
                                                        {choice.choiceValue}
                                                    </label>
                                                </div>
                                            ))}
                                    </Stack>
                                </div>
                            )}
                            {item.questionType === "Checkboxes" && (
                                <div
                                    style={{
                                        height: "fit-content",
                                        width: "700px",
                                        backgroundColor: "white",
                                        borderRadius: "10px",
                                        display: 'flex',
                                        justifyContent: 'start',
                                        alignItems: 'center',
                                        padding: '20px'
                                    }}
                                >
                                    <Stack
                                        direction="column"
                                        justifyContent="center"
                                        alignItems="flex-start"
                                        spacing={2}
                                        sx={{
                                            width: "100%",
                                            height: "100%",
                                        }}
                                    >
                                        <p className='mb-0'>{item.question}</p>
                                        {choiceData[item.id] &&
                                            choiceData[item.id].map((choice, choiceIndex) => (
                                                <div key={choiceIndex} className="form-check">
                                                    <input className="form-check-input" type="checkbox" name={`flexCheckDefault${item.id}`} id={`flexCheckDefault${item.id}${choiceIndex}`} />
                                                    <label className="form-check-label" htmlFor={`flexCheckDefault${item.id}${choiceIndex}`}>
                                                        {choice.choiceValue}
                                                    </label>
                                                </div>
                                            ))}
                                    </Stack>
                                </div>
                            )}
                        </div>
                    ))}
                    <button type="button" class="btn btn-primary"
                        style={{
                            backgroundColor: "#27c6d9",
                            border: "#27c6d9",
                        }}
                    >
                        Submit
                    </button>
                </Stack>
            )
            }
        </NavBar >
    )
}

export default AnswerAssessment
