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

    useEffect(() => {
        const fetchData = async () => {
            try {
                // Fetch assessment data
                const assessmentResponse = await fetch(`https://localhost:7236/api/assessments/${id}`);
                const assessmentData = await assessmentResponse.json();
                setAssessmentData(assessmentData);

                // Fetch item data
                const itemResponse = await fetch(`https://localhost:7236/api/items`);
                const itemData = await itemResponse.json();
                // Assuming you want to filter for assessmentId = 3
                const filteredItems = itemData.filter(item => item.assessmentId === Number(id));
                // Now, 'filteredItems' contains only the items with assessmentId = 3
                setItemData(filteredItems);


                // Implement loop here to fetch choice data for each item in filteredItems
                const choiceResponse = await fetch(`https://localhost:7236/api/choices`);
                const choiceData = await choiceResponse.json();
                // Create an object to store choice data by itemId
                const choicesByItemId = {};

                // Loop through filteredItems and filter choice data for each item
                filteredItems.forEach(item => {
                    const itemId = item.id;
                    const choicesForItem = choiceData.filter(choice => choice.itemId === itemId);
                    choicesByItemId[itemId] = choicesForItem;
                });

                // Now, 'choicesByItemId' is an object that maps item IDs to their respective choices
                setChoiceData(choicesByItemId);

                // All requests completed, set loading to false
                setLoading(false);
                console.log(filteredItems)
                console.log(choicesByItemId);
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
                <p>Loading assessment data...</p>
            ) : (
                <Stack
                    direction="column"
                    justifyContent="flex-start"
                    alignItems="center"
                    spacing={2}
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