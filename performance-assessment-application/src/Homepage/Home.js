import { Box, Stack } from '@mui/material'
import NavBar from "../Shared/NavBar"
import teamPhoto from './Images/teampic.png';
import joinPhoto from './Images/join.png';
import { Link } from 'react-router-dom';

function Home() {
    return (
        <NavBar>
            <Stack
                direction="column"
                justifyContent="flex-start"
                alignItems="flex-start"
                spacing={2}
                sx={{
                    width: "100%",
                    height: "100%",
                    padding: "40px",
                    overflow: "auto"
                }}
            >
                <b style={{ color: "#055c9d" }}>Let's get started</b>
                <div className="text-center w-100">
                    <div className="row row-cols-md-4 row-cols-sm-2 row-cols-1 row-gap-1">
                        <div className="col"
                            style={{
                                height: "250px",
                                padding: "10px"
                            }}
                        >
                            <Link to="/create-team"
                                style={{
                                    textDecoration: "none",
                                    color: "black"
                                }}
                            >
                                <div
                                    style={{
                                        height: "100%",
                                        backgroundColor: "white",
                                        borderRadius: "10px",
                                        display: "flex",
                                        flexDirection: "column",
                                        justifyContent: "center",
                                        alignItems: "center",
                                        gap: "10px",
                                        cursor: "pointer",
                                        boxShadow: "0px 0px 10px rgba(0, 0, 0, 0.2)",
                                        transition: "box-shadow 0.3s ease-in-out",
                                        '&:hover': {
                                            boxShadow: "0px 0px 20px rgba(0, 0, 0, 0.4)"
                                        }
                                    }}
                                >
                                    <img src={teamPhoto} alt="Create a Team" style={{ maxWidth: "50%", maxHeight: "100%" }} draggable="false" />
                                    <b>Create a Team</b>
                                </div>
                            </Link>
                        </div>
                        <div className="col"
                            style={{
                                height: "250px",
                                padding: "10px"
                            }}
                        >
                            <Link to="/join-team"
                                style={{
                                    textDecoration: "none",
                                    color: "black"
                                }}
                            >
                                <div
                                    style={{
                                        height: "100%",
                                        backgroundColor: "white",
                                        borderRadius: "10px",
                                        display: "flex",
                                        flexDirection: "column",
                                        justifyContent: "center",
                                        alignItems: "center",
                                        gap: "10px",
                                        cursor: "pointer",
                                        boxShadow: "0px 0px 10px rgba(0, 0, 0, 0.2)",
                                        transition: "box-shadow 0.3s ease-in-out",
                                        '&:hover': {
                                            boxShadow: "0px 0px 20px rgba(0, 0, 0, 0.4)"
                                        }
                                    }}
                                >
                                    <img src={joinPhoto} alt="Join an Existing Team" style={{ maxWidth: "50%", maxHeight: "100%" }} draggable="false" />
                                    <b>Join an Existing Team</b>
                                </div>
                            </Link>
                        </div>
                    </div>
                </div>
            </Stack >
        </NavBar >
    )
}

export default Home