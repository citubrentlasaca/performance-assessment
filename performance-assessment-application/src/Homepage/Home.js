import { Box, Stack } from '@mui/material'
import NavBar from "../Shared/NavBar"
import teamPhoto from './Images/teampic.png';
import joinPhoto from './Images/join.png';

function Home() {
    return (
        <NavBar>
            <Stack direction="row" justifyContent="flex-start" alignItems="center">
                <b
                    style={{
                        color: '#065d9d',
                        padding: '30px'
                    }}
                >
                    Let's get started
                </b>
            </Stack>
            <Stack direction="row" justifyContent="flex-start" alignItems="center" spacing={5}
                style={{
                    paddingLeft: "50px"
                }}
            >
                <a href="/create-team" style={{ textDecoration: 'none' }}>
                    <Box
                        sx={{
                            width: "230px",
                            height: "230px",
                            backgroundColor: "white",
                            display: "flex",
                            justifyContent: "center",
                            alignItems: "center",
                            borderRadius: "20px",
                            boxShadow: "0px 0px 10px rgba(0, 0, 0, 0.2)",
                            transition: "box-shadow 0.3s ease-in-out",
                            '&:hover': {
                                boxShadow: "0px 0px 20px rgba(0, 0, 0, 0.4)"
                            }
                        }}
                    >
                        <Stack direction="column" justifyContent="center" alignItems="center" spacing={1}>
                            <img src={teamPhoto} alt="Create a Team"
                                style={{
                                    width: '150px',
                                    height: '150px'
                                }}
                            />
                            <b
                                style={{
                                    color: 'black'
                                }}
                            >
                                Create a Team
                            </b>
                        </Stack>
                    </Box>
                </a>
                <a href="/join-team" style={{ textDecoration: 'none' }}>
                    <Box
                        sx={{
                            width: "230px",
                            height: "230px",
                            backgroundColor: "white",
                            display: "flex",
                            justifyContent: "center",
                            alignItems: "center",
                            borderRadius: "20px",
                            boxShadow: "0px 0px 10px rgba(0, 0, 0, 0.2)",
                            transition: "box-shadow 0.3s ease-in-out",
                            '&:hover': {
                                boxShadow: "0px 0px 20px rgba(0, 0, 0, 0.4)"
                            }
                        }}
                    >
                        <Stack direction="column" justifyContent="center" alignItems="center" spacing={1}>
                            <img src={joinPhoto} alt="Join an Existing Team"
                                style={{
                                    width: '150px',
                                    height: '150px'
                                }}
                            />
                            <b
                                style={{
                                    color: 'black'
                                }}
                            >
                                Join an Existing Team
                            </b>
                        </Stack>
                    </Box>
                </a>
            </Stack>
        </NavBar>
    )
}

export default Home