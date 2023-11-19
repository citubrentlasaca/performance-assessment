import { Box, Skeleton, Stack } from '@mui/material'
import axios from 'axios';
import React, { useEffect, useState } from 'react'
import { Link, useNavigate } from 'react-router-dom';

function TopBar() {
    const firstName = localStorage.getItem('firstName');
    const lastName = localStorage.getItem('lastName');
    const [showAccount, setShowAccount] = useState(false);
    const userId = JSON.parse(localStorage.getItem("userId"));
    const [userDetails, setUserDetails] = useState(null);
    const [backgroundImage, setBackgroundImage] = useState(null);
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();

    const handleAccountClick = () => {
        setShowAccount(!showAccount);
    }

    const handleLogout = () => {
        localStorage.removeItem('token');
        localStorage.removeItem('userId');
        localStorage.removeItem('firstName');
        localStorage.removeItem('lastName');
        localStorage.removeItem('employeeData');

        navigate('/login');
    };

    useEffect(() => {
        const fetchData = async () => {
            try {
                const userResponse = await axios.get(`https://workpa.azurewebsites.net/api/users/${userId}`);
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

    return (
        <Box
            sx={{
                display: 'flex',
                flexDirection: 'row',
                justifyContent: 'space-between',
                alignItems: 'center',
                width: '100%',
                height: '100px',
                backgroundColor: '#3dccdd',
                padding: "30px"
            }}
        >
            {loading ? (
                <>
                    <Stack
                        direction="row"
                        justifyContent="center"
                        alignItems="center"
                        spacing={2}
                    >
                        <Skeleton variant='circular'>
                            <div
                                style={{
                                    cursor: 'pointer'
                                }}
                            >
                                <svg xmlns="http://www.w3.org/2000/svg" width="40" height="40" fill="black" className="bi bi-person-circle" viewBox="0 0 16 16">
                                    <path d="M11 6a3 3 0 1 1-6 0 3 3 0 0 1 6 0z" />
                                    <path fillRule="evenodd" d="M0 8a8 8 0 1 1 16 0A8 8 0 0 1 0 8zm8-7a7 7 0 0 0-5.468 11.37C3.242 11.226 4.805 10 8 10s4.757 1.225 5.468 2.37A7 7 0 0 0 8 1z" />
                                </svg>
                            </div>
                        </Skeleton>
                    </Stack>
                    <Stack
                        direction="row"
                        justifyContent="center"
                        alignItems="center"
                        spacing={2}
                    >
                        <button type='button' className='btn gap-3'
                            style={{
                                display: 'flex',
                                flexDirection: 'row',
                                justifyContent: 'center',
                                alignItems: 'center',
                            }}
                        >
                            <Skeleton variant='text'>
                                <p className='mb-0'>Logout</p>
                            </Skeleton>
                            <Skeleton>
                                <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" className="bi bi-box-arrow-right" viewBox="0 0 16 16">
                                    <path fillRule="evenodd" d="M10 12.5a.5.5 0 0 1-.5.5h-8a.5.5 0 0 1-.5-.5v-9a.5.5 0 0 1 .5-.5h8a.5.5 0 0 1 .5.5v2a.5.5 0 0 0 1 0v-2A1.5 1.5 0 0 0 9.5 2h-8A1.5 1.5 0 0 0 0 3.5v9A1.5 1.5 0 0 0 1.5 14h8a1.5 1.5 0 0 0 1.5-1.5v-2a.5.5 0 0 0-1 0v2z" />
                                    <path fillRule="evenodd" d="M15.854 8.354a.5.5 0 0 0 0-.708l-3-3a.5.5 0 0 0-.708.708L14.293 7.5H5.5a.5.5 0 0 0 0 1h8.793l-2.147 2.146a.5.5 0 0 0 .708.708l3-3z" />
                                </svg>
                            </Skeleton>
                        </button>
                    </Stack>
                </>
            ) : (
                <>
                    <Stack
                        direction="row"
                        justifyContent="center"
                        alignItems="center"
                        spacing={2}
                    >
                        {backgroundImage ? (
                            <div onClick={handleAccountClick}
                                style={{
                                    width: "40px",
                                    height: "40px",
                                    borderRadius: "50%",
                                    backgroundImage: `url(${backgroundImage})`,
                                    backgroundSize: 'cover',
                                    backgroundPosition: 'center',
                                    cursor: 'pointer',
                                    border: '1px solid black'
                                }}
                            />
                        ) : (
                            <div onClick={handleAccountClick}
                                style={{
                                    cursor: 'pointer'
                                }}
                            >
                                <svg xmlns="http://www.w3.org/2000/svg" width="40" height="40" fill="black" className="bi bi-person-circle" viewBox="0 0 16 16">
                                    <path d="M11 6a3 3 0 1 1-6 0 3 3 0 0 1 6 0z" />
                                    <path fillRule="evenodd" d="M0 8a8 8 0 1 1 16 0A8 8 0 0 1 0 8zm8-7a7 7 0 0 0-5.468 11.37C3.242 11.226 4.805 10 8 10s4.757 1.225 5.468 2.37A7 7 0 0 0 8 1z" />
                                </svg>
                            </div>
                        )}

                        <Stack
                            direction="column"
                            justifyContent="center"
                            alignItems="flex-start"
                        >
                            {showAccount ? (
                                <>
                                    <p className='mb-0'>{firstName} {lastName}</p>
                                    <Link to={`/account/${userId}`}
                                        style={{
                                            textDecoration: "none"
                                        }}
                                    >
                                        <p className='mb-0' style={{ color: '#034cac' }}>Manage your account</p>
                                    </Link>
                                </>
                            ) : null}

                        </Stack>
                    </Stack>
                    <Stack
                        direction="row"
                        justifyContent="center"
                        alignItems="center"
                        spacing={2}
                    >
                        <button type='button' className='btn gap-3' onClick={handleLogout}
                            style={{
                                display: 'flex',
                                flexDirection: 'row',
                                justifyContent: 'center',
                                alignItems: 'center',
                            }}
                        >
                            <p className='mb-0'>Logout</p>
                            <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" className="bi bi-box-arrow-right" viewBox="0 0 16 16">
                                <path fillRule="evenodd" d="M10 12.5a.5.5 0 0 1-.5.5h-8a.5.5 0 0 1-.5-.5v-9a.5.5 0 0 1 .5-.5h8a.5.5 0 0 1 .5.5v2a.5.5 0 0 0 1 0v-2A1.5 1.5 0 0 0 9.5 2h-8A1.5 1.5 0 0 0 0 3.5v9A1.5 1.5 0 0 0 1.5 14h8a1.5 1.5 0 0 0 1.5-1.5v-2a.5.5 0 0 0-1 0v2z" />
                                <path fillRule="evenodd" d="M15.854 8.354a.5.5 0 0 0 0-.708l-3-3a.5.5 0 0 0-.708.708L14.293 7.5H5.5a.5.5 0 0 0 0 1h8.793l-2.147 2.146a.5.5 0 0 0 .708.708l3-3z" />
                            </svg>
                        </button>
                    </Stack>
                </>
            )}
        </Box >
    )
}

export default TopBar