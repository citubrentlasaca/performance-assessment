<<<<<<< HEAD
import React, { useState, useEffect } from "react";
=======
import React, { useEffect, useState } from "react";
>>>>>>> 8be0b4f113687c5406d2374c01f083bc721b2328
import NavBar from "../Shared/NavBar";
import TopBarThree from "../Shared/TopBarThree";
import TopBarTwo from "../Shared/TopBarTwo";
import announcementPhoto from './Images/announcement.png';
<<<<<<< HEAD
import { Stack } from '@mui/material';
=======
import axios from 'axios';
import { Box, Stack, Modal } from "@mui/material";
>>>>>>> 8be0b4f113687c5406d2374c01f083bc721b2328

function OrganizationDetails() {
    const [teamName, setTeamName] = useState('');
    const [announcement, setAnnouncement] = useState('');
    const [announcementList, setAnnouncementList] = useState([]);
    const employee = JSON.parse(localStorage.getItem("employeeData"));
    const [addAnnouncementHover, setAddAnnouncementHover] = useState(false);
    const [postAnnouncementHover, setPostAnnouncementHover] = useState(false);
    const [contentError, setContentError] = useState('');
    const [open, setOpen] = useState(false);

    const handleAnnouncementChange = (event) => {
        setAnnouncement(event.target.value);
        setContentError('');
    }

    const handleOpen = () => {
        setOpen(true)
        setAnnouncement('');
    };
    const handleClose = () => {
        setOpen(false)
        setAnnouncement('');
    };

    const normalStyle = {
        background: 'linear-gradient(to right, #0076fe, #00c5ff)',
        color: 'white',
        border: 'none',
        borderRadius: '10px',
        width: 'fit-content',
        padding: '10px',
    };

    const hoverStyle = {
        background: 'linear-gradient(to right, #0076fe, #00c5ff)',
        color: 'white',
        border: 'none',
        borderRadius: '10px',
        width: 'fit-content',
        padding: '10px',
        boxShadow: '0px 0px 15px rgba(0, 0, 0, 0.3)'

    };

    const handleAddHover = () => {
        setAddAnnouncementHover(true);
    };

    const handleAddLeave = () => {
        setAddAnnouncementHover(false);
    };

    const handlePostHover = () => {
        setPostAnnouncementHover(true);
    };

    const handlePostLeave = () => {
        setPostAnnouncementHover(false);
    };

    useEffect(() => {
        axios.get(`https://localhost:7236/api/teams/${employee.teamId}`)
            .then((response) => {
                const organizationName = response.data.organization;
                setTeamName(organizationName);
            })
            .catch((error) => {
                console.error('Error fetching team data:', error);
            });

        axios.get(`https://localhost:7236/api/announcements`)
            .then((response) => {
                const announcements = response.data.reverse();
                let tempAnnouncementList = [];
                announcements.map((announcement) => {
                    if (announcement.teamId === employee.teamId) {
                        const formattedDate = new Date(announcement.dateTimeCreated).toLocaleString('en-US', {
                            month: 'long',
                            day: 'numeric',
                            year: 'numeric',
                            hour: 'numeric',
                            minute: 'numeric',
                            hour12: true,
                        });
                        announcement.dateTimeCreated = formattedDate;
                        tempAnnouncementList.push(announcement);
                    }
                })
                setAnnouncementList(tempAnnouncementList);
            })
            .catch((error) => {
                console.error('Error fetching announcements:', error);
            });
    }, [announcement]);


    const handlePostClick = () => {
        setContentError('');
        if (announcement === '') {
            setContentError('Please enter an announcement');
        }

        if (announcement !== '') {
            const payload = {
                teamId: employee.teamId,
                content: announcement,
            };

            axios
                .post('https://localhost:7236/api/announcements', payload)
                .then((response) => {
                    console.log('Announcement posted successfully:', response.data);
                    handleClose();
                })
                .catch((error) => {
                    console.error('Error posting announcement:', error);
                });
        }
    };

    return (
        <NavBar>
            {employee.role === "Admin" ? <TopBarTwo /> : <TopBarThree />}
            <Modal
                open={open}
                onClose={handleClose}
            >
                <Box
                    sx={{
                        position: 'absolute',
                        top: '50%',
                        left: '50%',
                        transform: 'translate(-50%, -50%)',
                        width: '1000px',
                        height: 'fit-content',
                        backgroundColor: 'white',
                        boxShadow: 24,
                    }}
                >
                    <Stack
                        direction="column"
                        justifyContent="center"
                        alignItems="center"
                        spacing={2}
                        sx={{
                            width: "100%",
                            padding: '40px'
                        }}
                    >
                        <textarea type="text" className="form-control" value={announcement} onChange={handleAnnouncementChange}></textarea>
                        {contentError && <div style={{ color: 'red', fontSize: '12px' }}>{contentError}</div>}
                        <button style={postAnnouncementHover ? { ...normalStyle, ...hoverStyle } : normalStyle} onClick={handlePostClick}
                            onMouseEnter={handlePostHover}
                            onMouseLeave={handlePostLeave}
                        >
                            Post Announcement
                        </button>
                    </Stack>
                </Box>
            </Modal>
            <Stack
                direction="column"
                justifyContent="space-between"
                alignItems="center"
                spacing={2}
                sx={{
                    width: "100%",
                    height: "100%",
                    padding: '40px'
                }}
            >
                <Stack
                    direction="column"
                    justifyContent="center"
                    alignItems="center"
                    spacing={2}
                    sx={{
                        width: "100%",
                    }}
                >

                    {announcementList.length > 0 ? (
                        <>
                            {announcementList.map((announcement, index) => (
                                <Stack key={index}
                                    direction="column"
                                    justifyContent="flex-start"
                                    alignItems="flex-start"
                                    spacing={2}
                                    sx={{
                                        width: '100%',
                                        height: '100%',
                                        padding: '10px',
                                    }}
                                >
                                    <p className="mb-0">{announcement.dateTimeCreated}</p>
                                    <Box className='gap-2'
                                        sx={{
                                            width: '100%',
                                            height: 'fit-content',
                                            backgroundColor: 'white',
                                            borderRadius: '10px',
                                            display: 'flex',
                                            flexDirection: 'column',
                                            justifyContent: 'center',
                                            alignItems: 'flex-start',
                                            padding: '30px',
                                        }}
                                    >
                                        <p className="mb-0">{announcement.content}</p>
                                    </Box>
                                </Stack>
                            ))}
                        </>
                    ) : (
                        <>
                            <h1
                                style={{
                                    color: '#055c9d',
                                    fontWeight: 'bold'
                                }}
                            >
                                Welcome to {teamName}
                            </h1>
                            <img src={announcementPhoto} alt="Announcement" style={{ width: '10%' }} />
                            <p className="mb-0">No announcement yet</p>
                        </>

                    )}

                </Stack>
                {employee.role === "Admin" ? (
                    <button style={addAnnouncementHover ? { ...normalStyle, ...hoverStyle } : normalStyle} onClick={handleOpen}
                        onMouseEnter={handleAddHover}
                        onMouseLeave={handleAddLeave}
                    >
                        Add Announcement
                    </button>
                ) : (null)}
            </Stack>
        </NavBar>
    );
}

export default OrganizationDetails;
