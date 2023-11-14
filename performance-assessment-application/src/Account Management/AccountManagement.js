import React, { useEffect, useState } from 'react'
import NavBar from '../Shared/NavBar'
import { useNavigate, useParams } from 'react-router-dom'
import { Stack } from '@mui/material';
import axios from 'axios';

function AccountManagement() {
    const { userId } = useParams();
    const [currentPassword, setCurrentPassword] = useState('');
    const [newPassword, setNewPassword] = useState('');
    const [confirmNewPassword, setConfirmNewPassword] = useState('');
    const [userDetails, setUserDetails] = useState(null);
    const [currentPasswordError, setCurrentPasswordError] = useState('');
    const [newPasswordError, setNewPasswordError] = useState('');
    const [confirmNewPasswordError, setConfirmNewPasswordError] = useState('');
    const [error, setError] = useState('');
    const navigate = useNavigate();

    const handleCurrentPasswordChange = (event) => {
        setCurrentPassword(event.target.value);
        setCurrentPasswordError('');
    };

    const handleNewPasswordChange = (event) => {
        setNewPassword(event.target.value);
        setNewPasswordError('');
        setError('');
    };

    const handleConfirmNewPasswordChange = (event) => {
        setConfirmNewPassword(event.target.value);
        setConfirmNewPasswordError('');
        setError('');
    };

    useEffect(() => {
        const fetchData = async () => {
            try {
                const userResponse = await axios.get(`https://localhost:7236/api/users/${userId}`);
                setUserDetails(userResponse.data);
            } catch (error) {
                console.error(`Error fetching data:`, error);
            }
        };

        fetchData();
    }, []);

    const handleChangePasswordClick = async () => {
        try {
            const authenticate = await axios.get(`https://localhost:7236/api/users/authenticate?email=${userDetails.emailAddress}&password=${currentPassword}`);
            if (authenticate.data) {
                if (newPassword !== confirmNewPassword) {
                    setError('New passwords do not match');
                }
                if (newPassword === '') {
                    setNewPasswordError('New password is required');
                }
                if (confirmNewPassword === '') {
                    setConfirmNewPasswordError('Confirm new password is required');
                }
                else if (newPassword === confirmNewPassword) {
                    const updateAccount = await axios.put(`https://localhost:7236/api/users/${userId}`,
                        {
                            firstName: userDetails.firstName,
                            lastName: userDetails.lastName,
                            emailAddress: userDetails.emailAddress,
                            password: newPassword,
                        }
                    );
                    if (updateAccount.data) {
                        localStorage.removeItem('token');
                        localStorage.removeItem('userId');
                        localStorage.removeItem('firstName');
                        localStorage.removeItem('lastName');
                        localStorage.removeItem('employeeData');
                        navigate('/login');
                    }
                }
            }

        } catch (error) {
            setCurrentPasswordError('Password does not match');
            console.error('Error changing password:', error);
        }
    };

    return (
        <NavBar>
            <Stack
                direction="column"
                justifyContent="center"
                alignItems="center"
                spacing={2}
                sx={{
                    width: "100%",
                    height: "100%",
                    padding: "40px"
                }}
            >
                <Stack
                    direction="column"
                    justifyContent="center"
                    alignItems="flex-start"
                    spacing={2}
                    sx={{
                        width: "50%",
                    }}
                >
                    <Stack
                        direction="column"
                        justifyContent="center"
                        alignItems="flex-start"
                        spacing={1}
                        sx={{
                            width: "100%"
                        }}
                    >
                        <b>Current Password</b>
                        <input type='password' className='form-control' value={currentPassword} onChange={handleCurrentPasswordChange}
                            style={{
                                border: "2px solid #85dde7"
                            }}
                        />
                        {currentPasswordError && <div style={{ color: 'red', fontSize: '12px' }}>{currentPasswordError}</div>}
                    </Stack>
                    <Stack
                        direction="column"
                        justifyContent="center"
                        alignItems="flex-start"
                        spacing={1}
                        sx={{
                            width: "100%"
                        }}
                    >
                        <b>New Password</b>
                        <input type='password' className='form-control' value={newPassword} onChange={handleNewPasswordChange}
                            style={{
                                border: "2px solid #85dde7"
                            }}
                        />
                        {newPasswordError && <div style={{ color: 'red', fontSize: '12px' }}>{newPasswordError}</div>}
                    </Stack>
                    <Stack
                        direction="column"
                        justifyContent="center"
                        alignItems="flex-start"
                        spacing={1}
                        sx={{
                            width: "100%"
                        }}
                    >
                        <b>Confirm New Password</b>
                        <input type='password' className='form-control' value={confirmNewPassword} onChange={handleConfirmNewPasswordChange}
                            style={{
                                border: "2px solid #85dde7"
                            }}
                        />
                        {confirmNewPasswordError && <div style={{ color: 'red', fontSize: '12px' }}>{confirmNewPasswordError}</div>}
                    </Stack>
                    <Stack
                        direction="column"
                        justifyContent="center"
                        alignItems="center"
                        spacing={2}
                        sx={{
                            width: "100%"
                        }}
                    >
                        {error && <div style={{ color: 'red' }}>{error}</div>}
                        <button type="button" className="btn btn-danger" onClick={handleChangePasswordClick}>Change Password</button>
                    </Stack>
                </Stack>
            </Stack>
        </NavBar>
    )
}

export default AccountManagement