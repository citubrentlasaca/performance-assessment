import React, { useState, useEffect } from 'react'
import { Box, Modal, Stack } from '@mui/material'
import axios from 'axios';

function AssignAssessmentModal({ open, handleClose, assessmentId, assessmentTitle }) {
    const [users, setUsers] = useState([]);
    const [loading, setLoading] = useState(true);
    const [selectAllChecked, setSelectAllChecked] = useState(false);
    const [searchQuery, setSearchQuery] = useState('');
    const [selectedUserIds, setSelectedUserIds] = useState([]);
    const [occurrence, setOccurence] = useState('Daily');
    const [date, setDate] = useState('');
    const [time, setTime] = useState('');
    const [showPublishSuccessAlert, setShowPublishSuccessAlert] = useState(false);
    const employeeStorage = JSON.parse(localStorage.getItem("employeeData"));

    const handleOccurrenceChange = (event) => {
        setOccurence(event.target.value);
    };

    const handleTimeChange = (event) => {
        //setTime(event.target.value);
        const selectedTime = event.target.value;
    
        // Parse the input time as a date object
        const timeDate = new Date(`2000-01-01T${selectedTime}`);

        // Format the time as "HH:mm:ss"
        const formattedTime = timeDate.toTimeString().slice(0, 8);

        setTime(formattedTime);
    };

    const handleDateChange = (event) => {
        setDate(event.target.value);
    };

    const handleSearchChange = (event) => {
        setSearchQuery(event.target.value);
    };

    const filteredUsers = users.filter((user) => {
        const fullName = `${user.firstName} ${user.lastName}`;
        return fullName.toLowerCase().includes(searchQuery.toLowerCase());
    });

    useEffect(() => {
        const fetchData = async () => {
            try {
                const employeesResponse = await fetch(`https://localhost:7236/api/employees/teams/${employeeStorage.teamId}`);
                const employeesData = await employeesResponse.json();

                const userTemp = [];
                for (const employee of employeesData) {
                    if (employee.status === 'Active' && employee.role !== 'Admin') {
                        const userId = employee.userId;
                        const userResponse = await fetch(`https://localhost:7236/api/users/${userId}`);
                        const userData = await userResponse.json();
                        userTemp.push(userData);
                    }
                }

                setUsers(userTemp);
                setLoading(false);
            } catch (error) {
                console.error(`Error fetching data:`, error);
                setLoading(false);
            }
        };

        fetchData();
    }, [employeeStorage.teamId]);

    const handleSelectAll = () => {
        const checkboxes = document.querySelectorAll('input[type="checkbox"]');
        checkboxes.forEach((checkbox) => {
            checkbox.checked = !selectAllChecked;
        });
        setSelectAllChecked(!selectAllChecked);

        const allUserIds = filteredUsers.map((user) => user.id);
        setSelectedUserIds(selectAllChecked ? [] : allUserIds);
    };


    const handleCheckboxClick = (userId) => {
        const selectedIndex = selectedUserIds.indexOf(userId);
        let newSelectedUserIds = [...selectedUserIds];

        if (selectedIndex === -1) {
            newSelectedUserIds.push(userId);
        } else {
            newSelectedUserIds.splice(selectedIndex, 1);
        }

        setSelectedUserIds(newSelectedUserIds);
    };

    const handleClear = () => {
        const checkboxes = document.querySelectorAll('input[type="checkbox"]');
        checkboxes.forEach((checkbox) => {
            checkbox.checked = false;
        });

        setSelectedUserIds([]);
    };

    const handlePublishClick = async () => {
        for (const userId of selectedUserIds) {
            try {
                const employeeResponse = await axios.get(`https://localhost:7236/api/employees/users/${userId}`);
                const employeeData = employeeResponse.data;

                if (Array.isArray(employeeData)) {
                    for (const employee of employeeData) {
                        if (employee.teamId === employeeStorage.teamId) {
                            const employeeId = employee.id;
                            const schedulerData = {
                                employeeIds: [employeeId],
                                scheduler: {
                                    assessmentId: assessmentId,
                                    occurrence: occurrence,
                                    dueDate: date,
                                    time: time,
                                    isAnswered: false,
                                },
                            };

                            await axios.post('https://localhost:7236/api/schedulers', schedulerData);
                            console.log(`Scheduler created for employee with ID: ${employeeId}`);
                        }
                    }
                }
                else {
                    console.error(`Employee not found for user with ID: ${userId}`);
                }
                setShowPublishSuccessAlert(true);
                setTimeout(() => {
                    setShowPublishSuccessAlert(false);
                }, 3000);
                handleClear();
                setOccurence('Daily');
                setDate('');
                setTime('');
            } catch (error) {
                console.error('Error creating scheduler:', error);
            }
        }
    };

    useEffect(() => {
        if (open) {
            handleClear();
            setOccurence('Daily');
            setDate('');
            setTime('');
        } else {
            handleClear();
            setOccurence('Daily');
            setDate('');
            setTime('');
        }
    }, [open]);

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
                                flexDirection: 'row',
                                alignItems: 'center',
                                justifyContent: 'space-between',
                            }}
                        >
                            <h4 className='mb-0'
                                style={{
                                    padding: '6px 12px'
                                }}
                            >{assessmentTitle}</h4>
                            <button type='button' className='btn' onClick={handleClose}>
                                <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" className="bi bi-x-lg" viewBox="0 0 16 16">
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
                                        <p className='mb-0'>All employees</p>
                                        <button type="button" className="btn" onClick={handleSelectAll}>
                                            <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" className="bi bi-person-add" viewBox="0 0 16 16">
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

                                            <input type="text" className="form-control" placeholder="Type a name" value={searchQuery} onChange={handleSearchChange}
                                                style={{
                                                    border: 'none',
                                                    backgroundColor: 'transparent'
                                                }}
                                            />
                                            <button type="button" className="btn">
                                                <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" className="bi bi-search" viewBox="0 0 16 16">
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
                                            <p className='mb-0'>Choose employees</p>
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
                                            <div className="form-check">
                                                <input className="form-check-input" type="checkbox" value="" id="flexCheckDefault" onClick={handleSelectAll} />
                                                <label className="form-check-label" htmlFor="flexCheckDefault">
                                                    Select all
                                                </label>
                                            </div>
                                            <button type="button" className="btn" onClick={handleClear}
                                                style={{
                                                    color: 'red'
                                                }}
                                            >
                                                Clear
                                            </button>
                                        </Stack>
                                        <div
                                            style={{
                                                height: '160px',
                                                width: '100%',
                                                overflowY: 'auto',
                                            }}
                                        >
                                            <Stack
                                                direction="column"
                                                justifyContent="center"
                                                alignItems="center"
                                                spacing={2}
                                                sx={{
                                                    width: '100%',
                                                }}
                                            >
                                                {filteredUsers.map((user) => (
                                                    <Stack
                                                        key={user.id}
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
                                                            <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" className="bi bi-person-circle" viewBox="0 0 16 16">
                                                                <path d="M11 6a3 3 0 1 1-6 0 3 3 0 0 1 6 0z" />
                                                                <path fillRule="evenodd" d="M0 8a8 8 0 1 1 16 0A8 8 0 0 1 0 8zm8-7a7 7 0 0 0-5.468 11.37C3.242 11.226 4.805 10 8 10s4.757 1.225 5.468 2.37A7 7 0 0 0 8 1z" />
                                                            </svg>
                                                            <p className='mb-0'>{user.firstName} {user.lastName}</p>
                                                        </Stack>
                                                        <div className="form-check" style={{ marginRight: '17px' }}>
                                                            <input className="form-check-input" type="checkbox" value="" id={`flexCheckDefault${user.id}`} onClick={() => handleCheckboxClick(user.id)} />
                                                        </div>
                                                    </Stack>
                                                ))}
                                            </Stack>
                                        </div>
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
                                    {assessmentTitle === 'Daily Performance Report' ? (
                                        <>
                                            <p className='mb-0'>Occurence</p>
                                            <select className="form-select" value={occurrence} onChange={handleOccurrenceChange}>
                                                <option value="Daily">Daily</option>
                                            </select>
                                        </>
                                    ) : (
                                        <>
                                            <p className='mb-0'>Occurence</p>
                                            <select className="form-select" value={occurrence} onChange={handleOccurrenceChange}>
                                                <option value="Once">Once</option>
                                                <option value="Daily">Daily</option>
                                                <option value="Weekly">Weekly</option>
                                                <option value="Monthly">Monthly</option>
                                            </select>
                                        </>
                                    )}
                                    <p className='mb-0'>Due Date</p>
                                    <input type="date" className="form-control" value={date} onChange={handleDateChange} ></input>
                                    <p className='mb-0'>Time</p>
                                    <input type="time" className="form-control" value={time} onChange={handleTimeChange} ></input>
                                </Stack>
                                {showPublishSuccessAlert && (
                                    <div className="alert alert-success alert-dismissible fade show w-100" role="alert">
                                        Assessment assigned successfully.
                                        <button type="button" className="btn-close" data-bs-dismiss="alert" aria-label="Close" onClick={() => setShowPublishSuccessAlert(false)}></button>
                                    </div>
                                )}
                                <Stack
                                    direction="row"
                                    justifyContent="flex-end"
                                    alignItems="center"
                                    spacing={2}
                                    sx={{
                                        width: '100%'
                                    }}
                                >
                                    <button type="button" className="btn" onClick={handlePublishClick}
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
                )}
            </Box>
        </Modal>
    )
}

export default AssignAssessmentModal