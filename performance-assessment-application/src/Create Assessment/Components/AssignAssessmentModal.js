import React from 'react'
import { Box, Modal, Stack } from '@mui/material'

function AssignAssessmentModal({ open, handleClose }) {
    return (
        <Modal
            open={open}
        >
            <Box
                sx={{
                    position: 'absolute',
                    top: '50%',
                    left: '50%',
                    transform: 'translate(-50%, -50%)',
                    width: '1000px',
                    height: '500px',
                    backgroundColor: 'white',
                    boxShadow: 24,
                }}
            >
                <Stack
                    direction="column"
                    justifyContent="flex-start"
                    alignItems="center"
                    sx={{
                        width: '100%',
                        height: '100%',
                    }}
                >
                    <Box
                        sx={{
                            width: '100%',
                            height: '40px',
                            backgroundColor: '#27c6d9',
                            display: 'flex',
                            alignItems: 'center',
                            justifyContent: 'flex-end',
                        }}
                    >
                        <button type='button' class='btn' onClick={handleClose}>
                            <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-x-lg" viewBox="0 0 16 16">
                                <path d="M2.146 2.854a.5.5 0 1 1 .708-.708L8 7.293l5.146-5.147a.5.5 0 0 1 .708.708L8.707 8l5.147 5.146a.5.5 0 0 1-.708.708L8 8.707l-5.146 5.147a.5.5 0 0 1-.708-.708L7.293 8 2.146 2.854Z" />
                            </svg>
                        </button>
                    </Box>
                    <Stack
                        direction="row"
                        justifyContent="space-evenly"
                        alignItems="center"
                        spacing={2}
                        sx={{
                            width: '100%',
                            height: '100%',
                        }}
                    >
                        <Stack
                            direction="column"
                            justifyContent="flex-start"
                            alignItems="flex-start"
                            spacing={2}
                            sx={{
                                width: '50%',
                                height: '100%',
                                padding: '20px',
                            }}
                        >
                            <h5>Assign To</h5>
                            <Stack
                                direction="column"
                                justifyContent="flex-start"
                                alignItems="center"
                                sx={{
                                    width: '100%',
                                    height: '100%',
                                }}
                            >
                                <div
                                    style={{
                                        width: '100%',
                                        height: '40px',
                                        backgroundColor: '#ecf5f9',
                                        borderRadius: '10px',
                                        border: '1px solid #e3e9eb',
                                        display: 'flex',
                                        flexDirection: 'row',
                                        alignItems: 'center',
                                        justifyContent: 'space-between',
                                        padding: '10px',
                                    }}
                                >
                                    <p class='mb-0'>All employees</p>
                                    <button type="button" class="btn">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-person-add" viewBox="0 0 16 16">
                                            <path d="M12.5 16a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7Zm.5-5v1h1a.5.5 0 0 1 0 1h-1v1a.5.5 0 0 1-1 0v-1h-1a.5.5 0 0 1 0-1h1v-1a.5.5 0 0 1 1 0Zm-2-6a3 3 0 1 1-6 0 3 3 0 0 1 6 0ZM8 7a2 2 0 1 0 0-4 2 2 0 0 0 0 4Z" />
                                            <path d="M8.256 14a4.474 4.474 0 0 1-.229-1.004H3c.001-.246.154-.986.832-1.664C4.484 10.68 5.711 10 8 10c.26 0 .507.009.74.025.226-.341.496-.65.804-.918C9.077 9.038 8.564 9 8 9c-5 0-6 3-6 4s1 1 1 1h5.256Z" />
                                        </svg>
                                    </button>
                                </div>
                                <div className='gap-2'
                                    style={{
                                        width: '100%',
                                        height: '100%',
                                        backgroundColor: '#fafdff',
                                        borderRadius: '10px',
                                        border: '1px solid #e3e9eb',
                                        display: 'flex',
                                        flexDirection: 'column',
                                        alignItems: 'center',
                                        justifyContent: 'flex-start',
                                        padding: '20px',
                                    }}
                                >
                                    <div
                                        style={{
                                            width: '100%',
                                            height: '40px',
                                            backgroundColor: '#ecf5f9',
                                            borderRadius: '10px',
                                            border: '1px solid #e3e9eb',
                                            display: 'flex',
                                            flexDirection: 'row',
                                            alignItems: 'center',
                                            justifyContent: 'space-between',
                                            padding: '10px',
                                        }}
                                    >
                                        <input type="text" class="form-control" placeholder="Type a name or department"
                                            style={{
                                                border: 'none',
                                                backgroundColor: 'transparent'
                                            }}
                                        />
                                        <button type="button" class="btn">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-search" viewBox="0 0 16 16">
                                                <path d="M11.742 10.344a6.5 6.5 0 1 0-1.397 1.398h-.001c.03.04.062.078.098.115l3.85 3.85a1 1 0 0 0 1.415-1.414l-3.85-3.85a1.007 1.007 0 0 0-.115-.1zM12 6.5a5.5 5.5 0 1 1-11 0 5.5 5.5 0 0 1 11 0z" />
                                            </svg>
                                        </button>
                                    </div>
                                    <Stack
                                        direction="row"
                                        justifyContent="flex-start"
                                        alignItems="center"
                                        sx={{
                                            width: '100%'
                                        }}
                                    >
                                        <p class='mb-0'>Choose employees</p>
                                    </Stack>
                                    <Stack
                                        direction="row"
                                        justifyContent="space-between"
                                        alignItems="center"
                                        spacing={2}
                                        sx={{
                                            width: '100%'
                                        }}
                                    >
                                        <div class="form-check">
                                            <input class="form-check-input" type="checkbox" value="" id="flexCheckDefault" />
                                            <label class="form-check-label" htmlFor="flexCheckDefault">
                                                Select all
                                            </label>
                                        </div>
                                        <button type="button" class="btn"
                                            style={{
                                                color: 'red'
                                            }}
                                        >
                                            Clear
                                        </button>
                                    </Stack>
                                    <Stack
                                        direction="row"
                                        justifyContent="space-between"
                                        alignItems="center"
                                        spacing={2}
                                        sx={{
                                            width: '100%'
                                        }}
                                    >
                                        <Stack
                                            direction="row"
                                            justifyContent="center"
                                            alignItems="center"
                                            spacing={2}
                                        >
                                            <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-person-circle" viewBox="0 0 16 16">
                                                <path d="M11 6a3 3 0 1 1-6 0 3 3 0 0 1 6 0z" />
                                                <path fill-rule="evenodd" d="M0 8a8 8 0 1 1 16 0A8 8 0 0 1 0 8zm8-7a7 7 0 0 0-5.468 11.37C3.242 11.226 4.805 10 8 10s4.757 1.225 5.468 2.37A7 7 0 0 0 8 1z" />
                                            </svg>
                                            <p className='mb-0'>John Doe</p>
                                        </Stack>
                                        <div class="form-check"
                                            style={{
                                                marginRight: '17px'
                                            }}
                                        >
                                            <input class="form-check-input" type="checkbox" value="" id="flexCheckDefault1" />
                                        </div>
                                    </Stack>
                                </div>
                            </Stack>
                        </Stack>
                        <Stack
                            direction="column"
                            justifyContent="flex-start"
                            alignItems="flex-start"
                            spacing={1}
                            sx={{
                                width: '50%',
                                height: '100%',
                                padding: '20px',
                            }}
                        >
                            <h5>Scheduler</h5>
                            <Stack
                                direction="column"
                                justifyContent="flex-start"
                                alignItems="flex-start"
                                spacing={1}
                                sx={{
                                    width: '100%',
                                    height: '100%',
                                }}
                            >

                                <p className='mb-0'>Reminder</p>
                                <select class="form-select">
                                    <option value="everyday">Everyday</option>
                                </select>
                                <p className='mb-0'>Occurence</p>
                                <select class="form-select">
                                    <option value="once">Once</option>
                                </select>
                                <p className='mb-0'>Due Date</p>
                                <input type="date" class="form-control" ></input>
                                <p className='mb-0'>Time</p>
                                <input type="time" class="form-control" ></input>
                            </Stack>
                            <Stack
                                direction="row"
                                justifyContent="flex-end"
                                alignItems="center"
                                spacing={2}
                                sx={{
                                    width: '100%'
                                }}
                            >
                                <button type="button" class="btn"
                                    style={{
                                        backgroundColor: '#27c6d9',
                                        color: 'white'
                                    }}
                                >
                                    PUBLISH
                                </button>
                            </Stack>
                        </Stack>
                    </Stack>
                </Stack>
            </Box>
        </Modal>
    )
}

export default AssignAssessmentModal