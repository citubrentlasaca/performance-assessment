import React, { useState, useEffect } from 'react';
import NavBar from '../../../Shared/NavBar';
import { FormControl, IconButton, MenuItem, Select, Stack, TextField, Switch } from '@mui/material';
import DeleteOutlineOutlinedIcon from '@mui/icons-material/DeleteOutlineOutlined';

function SelfAssessmentTemplate() {
    const [assessmentData, setAssessmentData] = useState({ title: '', description: '' });
    const [itemData, setItemData] = useState(null);
    const [choiceData, setChoiceData] = useState(null);
    const [loading, setLoading] = useState(true);

    const handleTitleChange = (event) => {
        setAssessmentData({
            ...assessmentData,
            title: event.target.value,
        });
    };

    const handleDescriptionChange = (event) => {
        setAssessmentData({
            ...assessmentData,
            description: event.target.value,
        });
    };

    const updateItemData = (updatedItem) => {
        const updatedItemData = itemData.map((item) => {
            if (item.id === updatedItem.id) {
                return updatedItem;
            }
            return item;
        });
        setItemData(updatedItemData);
    };

    const handleQuestionChange = (event, itemId) => {
        const updatedItem = {
            ...itemData.find((item) => item.id === itemId),
            question: event.target.value,
        };
        updateItemData(updatedItem);
    };

    const handleTypeChange = (event, itemId) => {
        const updatedItem = {
            ...itemData.find((item) => item.id === itemId),
            questionType: event.target.value,
        };
        updateItemData(updatedItem);
    };

    const handleWeightChange = (event, itemId) => {
        const updatedItem = {
            ...itemData.find((item) => item.id === itemId),
            weight: event.target.value,
        };
        updateItemData(updatedItem);
    };

    const handleTargetChange = (event, itemId) => {
        const updatedItem = {
            ...itemData.find((item) => item.id === itemId),
            target: event.target.value,
        };
        updateItemData(updatedItem);
    };

    const handleRequiredChange = (event, itemId) => {
        const updatedItem = {
            ...itemData.find((item) => item.id === itemId),
            required: event.target.checked,
        };
        updateItemData(updatedItem);
    };

    const handleChoiceValueChange = (event, itemId, choiceId) => {
        const updatedChoices = { ...choiceData };
        const updatedChoiceIndex = updatedChoices[itemId].findIndex((c) => c.id === choiceId);
        updatedChoices[itemId][updatedChoiceIndex].choiceValue = event.target.value;
        setChoiceData(updatedChoices);
    };

    useEffect(() => {
        const fetchData = async () => {
            try {
                const assessmentResponse = await fetch('https://localhost:7236/api/selfassessments/1');
                const assessment = await assessmentResponse.json();
                setAssessmentData(assessment);

                const itemResponse = await fetch(`https://localhost:7236/api/selfassessmentitems`);
                const item = await itemResponse.json();
                const filteredItems = item.filter(item => item.selfAssessmentId === Number(assessment.id));
                setItemData(filteredItems);

                const choiceResponse = await fetch(`https://localhost:7236/api/selfassessmentchoices`);
                const choices = await choiceResponse.json();
                const choicesByItemId = {};
                filteredItems.forEach(item => {
                    const itemId = item.id;
                    const choicesForItem = choices.filter(choice => choice.selfAssessmentItemId === itemId);
                    choicesByItemId[itemId] = choicesForItem;
                });
                setChoiceData(choicesByItemId);

                setLoading(false);
            } catch (error) {
                console.error('Error fetching data:', error);
                setLoading(false);
            }
        };

        fetchData();
    }, []);

    return (
        <NavBar>
            {loading ? (
                <Stack
                    justifyContent="center"
                    alignItems="center"
                    spacing={2}
                    sx={{
                        height: '100%',
                        width: '100%',
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
                        height: '100%',
                        width: '100%',
                    }}
                >
                    <div
                        style={{
                            width: '750px',
                            height: '150px',
                            backgroundColor: 'white',
                            borderRadius: '10px',
                            borderTop: '10px solid #27c6d9',
                            padding: '0 20px',
                        }}
                    >
                        <TextField
                            label="Assessment Title"
                            value={assessmentData.title}
                            onChange={handleTitleChange}
                            variant="standard"
                            fullWidth
                            sx={{ marginTop: '10px' }}
                        />
                        <TextField
                            label="Assessment Description"
                            value={assessmentData.description}
                            onChange={handleDescriptionChange}
                            variant="standard"
                            fullWidth
                        />
                    </div>
                    {itemData.map((item) => (
                        <div
                            key={item.id}
                            style={{
                                width: '750px',
                                height: 'fit-content',
                                backgroundColor: 'white',
                                borderRadius: '10px',
                                padding: '20px',
                            }}
                        >
                            <Stack
                                direction="column"
                                justifyContent="center"
                                alignItems="flex-start"
                                spacing={2}
                                sx={{
                                    width: '100%',
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
                                    <TextField
                                        multiline
                                        label="Question"
                                        variant="filled"
                                        value={item.question}
                                        onChange={(event) => handleQuestionChange(event, item.id)}
                                        sx={{
                                            width: '70%'
                                        }}
                                    />
                                    <button type='btn' class='btn'>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-image" viewBox="0 0 16 16">
                                            <path d="M6.002 5.5a1.5 1.5 0 1 1-3 0 1.5 1.5 0 0 1 3 0z" />
                                            <path d="M2.002 1a2 2 0 0 0-2 2v10a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V3a2 2 0 0 0-2-2h-12zm12 1a1 1 0 0 1 1 1v6.5l-3.777-1.947a.5.5 0 0 0-.577.093l-3.71 3.71-2.66-1.772a.5.5 0 0 0-.63.062L1.002 12V3a1 1 0 0 1 1-1h12z" />
                                        </svg>
                                    </button>
                                    <FormControl
                                        sx={{
                                            width: '30%'
                                        }}
                                    >
                                        <Select value={item.questionType} onChange={(event) => handleTypeChange(event, item.id)}
                                            sx={{
                                                fontFamily: "Montserrat Regular"
                                            }}
                                        >
                                            <MenuItem value={'Short answer'}>Short answer</MenuItem>
                                            <MenuItem value={'Paragraph'}>Paragraph</MenuItem>
                                            <MenuItem value={'Multiple choice'}>Multiple choice</MenuItem>
                                            <MenuItem value={'Checkboxes'}>Checkboxes</MenuItem>
                                        </Select>
                                    </FormControl>
                                </Stack>
                                <Stack
                                    direction="column"
                                    justifyContent="center"
                                    alignItems="flex-start"
                                    spacing={2}
                                    sx={{
                                        width: '100%',
                                    }}
                                >
                                    {item.questionType === 'Short answer' && (
                                        <TextField
                                            disabled
                                            variant="standard"
                                            label="Short answer"
                                            sx={{
                                                width: '100%',
                                            }}
                                        />
                                    )}
                                    {item.questionType === 'Paragraph' && (
                                        <TextField
                                            variant="standard"
                                            label="Paragraph"
                                            disabled
                                            sx={{
                                                width: "100%"
                                            }}
                                        />
                                    )}
                                    {item.questionType === 'Multiple choice' && (
                                        <div
                                            style={{
                                                width: '100%',
                                            }}
                                        >
                                            {choiceData[item.id].map((choice) => (
                                                <div key={choice.id} className="form-check gap-2"
                                                    style={{
                                                        width: '100%',
                                                        display: 'flex',
                                                        alignItems: 'center',
                                                        justifyContent: 'center',
                                                    }}
                                                >
                                                    <input
                                                        className="form-check-input"
                                                        type="radio"
                                                        name={`choice-${item.id}`}
                                                        id={`choice-${choice.id}`}
                                                        disabled
                                                    />
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        value={choice.choiceValue}
                                                        onChange={(event) => handleChoiceValueChange(event, item.id, choice.id)}
                                                        style={{
                                                            width: "100%",
                                                            border: "none",
                                                            borderBottom: "1px solid #c9c9c9",
                                                            borderRadius: "0px",
                                                        }}
                                                    />
                                                </div>
                                            ))}
                                        </div>
                                    )}
                                    {item.questionType === 'Checkboxes' && (
                                        <div
                                            style={{
                                                width: '100%',
                                            }}
                                        >
                                            {choiceData[item.id].map((choice) => (
                                                <div key={choice.id} className="form-check gap-2"
                                                    style={{
                                                        width: '100%',
                                                        display: 'flex',
                                                        alignItems: 'center',
                                                        justifyContent: 'center',
                                                    }}
                                                >
                                                    <input
                                                        className="form-check-input"
                                                        type="checkbox"
                                                        name={`choice-${item.id}`}
                                                        id={`choice-${choice.id}`}
                                                        disabled
                                                    />
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        value={choice.choiceValue}
                                                        onChange={(event) => handleChoiceValueChange(event, item.id, choice.id)}
                                                        style={{
                                                            width: "100%",
                                                            border: "none",
                                                            borderBottom: "1px solid #c9c9c9",
                                                            borderRadius: "0px",
                                                        }}
                                                    />
                                                </div>
                                            ))}
                                        </div>
                                    )}
                                </Stack>
                                <hr
                                    style={{
                                        width: '100%',
                                        height: '1px',
                                        backgroundColor: '#c9c9c9',
                                        marginTop: '20px',
                                    }}
                                />
                                <Stack
                                    direction="row"
                                    justifyContent="space-between"
                                    alignItems="center"
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
                                        <p class="mb-0">Weight value (0-100%):</p>
                                        <input type='number' value={item.weight} onChange={(event) => handleWeightChange(event, item.id)}
                                            style={{
                                                width: "60px"
                                            }}
                                        ></input>
                                    </Stack>
                                    <Stack
                                        direction="row"
                                        justifyContent="center"
                                        alignItems="center"
                                        spacing={2}
                                    >
                                        <p class="mb-0">Target value:</p>
                                        <input type='number' value={item.target} onChange={(event) => handleTargetChange(event, item.id)}
                                            style={{
                                                width: "60px"
                                            }}
                                        ></input>
                                    </Stack>
                                    <IconButton>
                                        <DeleteOutlineOutlinedIcon />
                                    </IconButton>
                                    <Stack
                                        direction="row"
                                        justifyContent="center"
                                        alignItems="center"
                                        spacing={2}
                                    >
                                        <p class="mb-0">Required</p>
                                        <Switch checked={item.required} onChange={(event) => handleRequiredChange(event, item.id)} />
                                    </Stack>
                                </Stack>
                            </Stack>
                        </div>
                    ))}
                </Stack>
            )}
        </NavBar>
    );
}

export default SelfAssessmentTemplate;