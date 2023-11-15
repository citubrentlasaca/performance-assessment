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
    const [loading, setLoading] = useState(true);
    const [currentPasswordError, setCurrentPasswordError] = useState('');
    const [newPasswordError, setNewPasswordError] = useState('');
    const [confirmNewPasswordError, setConfirmNewPasswordError] = useState('');
    const [error, setError] = useState('');
    const [isHovered, setIsHovered] = useState(false);
    const [backgroundImage, setBackgroundImage] = useState(null);
    const [backgroundImageFile, setBackgroundImageFile] = useState(null);
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

    const handleFileChange = (event) => {
        const file = event.target.files[0];

        if (file) {
            if (file.type.startsWith('image/') && /\.(jpe?g|png)$/i.test(file.name)) {
                const reader = new FileReader();
                reader.onload = () => {
                    setBackgroundImage(reader.result);
                    setBackgroundImageFile(file);
                };
                reader.readAsDataURL(file);
            } else {
                alert('Please select a valid image file (png, jpg, jpeg)');
            }
        }
    };

    useEffect(() => {
        const fetchData = async () => {
            try {
                const userResponse = await axios.get(`https://localhost:7236/api/users/${userId}`);
                setUserDetails(userResponse.data);
                if (userResponse.data.profilePicture !== null) {
                    setBackgroundImage(`data:image/png;base64,${userResponse.data.profilePicture}`);
                }
                setLoading(false);
            } catch (error) {
                console.error(`Error fetching data:`, error);
            }
        };

        fetchData();
    }, []);

    const handleSaveChangesClick = async () => {
        try {
            if (currentPassword === '' && newPassword === '' && confirmNewPassword === '') {
                const formData = new FormData();
                formData.append('emailAddress', userDetails.emailAddress);

                if (backgroundImageFile instanceof File) {
                    formData.append('profilePicture', backgroundImageFile);
                }

                const updateAccount = await axios.put(`https://localhost:7236/api/users/${userId}`, formData);
                if (updateAccount.data) {
                    navigate('/home');
                }
                console.log("Account updated successfully")
            }
            else {
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
                        const formData = new FormData();
                        formData.append('password', newPassword);


                        if (backgroundImageFile instanceof File) {
                            formData.append('profilePicture', backgroundImageFile);
                        }

                        const updateAccount = await axios.put(`https://localhost:7236/api/users/${userId}`, formData);
                        if (updateAccount.data) {
                            localStorage.removeItem('token');
                            localStorage.removeItem('userId');
                            localStorage.removeItem('firstName');
                            localStorage.removeItem('lastName');
                            localStorage.removeItem('employeeData');
                            navigate('/login');
                        }
                        console.log("Account updated successfully")
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
                            direction="row"
                            justifyContent="center"
                            alignItems="center"
                            spacing={2}
                            sx={{
                                width: "100%"
                            }}
                        >
                            <div
                                style={{
                                    width: "100px",
                                    height: "100px",
                                    borderRadius: "50%",
                                    border: "2px solid #85dde7",
                                    display: "flex",
                                    justifyContent: "center",
                                    alignItems: "center",
                                    backgroundImage: `url(${backgroundImage})`,
                                    backgroundSize: 'cover',
                                    backgroundPosition: 'center',
                                }}
                                onMouseEnter={() => setIsHovered(true)}
                                onMouseLeave={() => setIsHovered(false)}
                                onClick={() => {
                                    document.getElementById('fileInput').click();
                                }}
                            >
                                {isHovered && (
                                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-pencil" viewBox="0 0 16 16">
                                        <path d="M12.146.146a.5.5 0 0 1 .708 0l3 3a.5.5 0 0 1 0 .708l-10 10a.5.5 0 0 1-.168.11l-5 2a.5.5 0 0 1-.65-.65l2-5a.5.5 0 0 1 .11-.168l10-10zM11.207 2.5 13.5 4.793 14.793 3.5 12.5 1.207 11.207 2.5zm1.586 3L10.5 3.207 4 9.707V10h.5a.5.5 0 0 1 .5.5v.5h.5a.5.5 0 0 1 .5.5v.5h.293l6.5-6.5zm-9.761 5.175-.106.106-1.528 3.821 3.821-1.528.106-.106A.5.5 0 0 1 5 12.5V12h-.5a.5.5 0 0 1-.5-.5V11h-.5a.5.5 0 0 1-.468-.325z" />
                                    </svg>
                                )}
                                <input
                                    className='form-control'
                                    type="file"
                                    id="fileInput"
                                    style={{ display: 'none' }}
                                    onChange={handleFileChange}
                                    accept=".png, .jpg, .jpeg"
                                />
                            </div>
                        </Stack>
                        <Stack
                            direction="row"
                            justifyContent="center"
                            alignItems="center"
                            spacing={2}
                            sx={{
                                width: "100%"
                            }}
                        >
                            <Stack
                                direction="column"
                                justifyContent="center"
                                alignItems="flex-start"
                                spacing={1}
                                sx={{
                                    width: "50%"
                                }}
                            >
                                <b>First Name</b>
                                <input type='text' className='form-control' value={userDetails.firstName} disabled
                                    style={{
                                        border: "2px solid #85dde7"
                                    }}
                                />
                            </Stack>
                            <Stack
                                direction="column"
                                justifyContent="center"
                                alignItems="flex-start"
                                spacing={1}
                                sx={{
                                    width: "50%"
                                }}
                            >
                                <b>Last Name</b>
                                <input type='text' className='form-control' value={userDetails.lastName} disabled
                                    style={{
                                        border: "2px solid #85dde7"
                                    }}
                                />
                            </Stack>
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
                            <b>Email Address</b>
                            <input type='text' className='form-control' value={userDetails.emailAddress} disabled
                                style={{
                                    border: "2px solid #85dde7"
                                }}
                            />
                        </Stack>
                        <hr
                            style={{
                                width: "100%",
                                height: "1px",
                            }}
                        />
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
                            <button type="button" className="btn btn-danger" onClick={handleSaveChangesClick}>Save Changes</button>
                        </Stack>
                    </Stack>
                </Stack>
            )}
        </NavBar>
    )
}

export default AccountManagement