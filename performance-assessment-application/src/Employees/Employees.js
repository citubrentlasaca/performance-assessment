import React, { useEffect, useState } from 'react'
import NavBar from '../Shared/NavBar'
import TopBarTwo from '../Shared/TopBarTwo'
import { Stack } from '@mui/material'
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

function Employees() {
    const employeeStorage = JSON.parse(localStorage.getItem('employeeData'));
    const [employees, setEmployees] = useState(null);
    const [filteredEmployees, setFilteredEmployees] = useState(null);
    const [employeeCount, setEmployeeCount] = useState(0);
    const [refresh, setRefresh] = useState(false);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchData = async () => {
            try {
                const employeeResponse = await axios.get(`https://localhost:7236/api/employees/teams/${employeeStorage.teamId}`)
                const employeeTemp = [];
                for (const employee of employeeResponse.data) {
                    if (employee.role !== 'Admin') {
                        const userResponse = await axios.get(`https://localhost:7236/api/users/${employee.userId}`);
                        employeeTemp.push({
                            id: employee.id,
                            userId: employee.userId,
                            firstName: userResponse.data.firstName,
                            lastName: userResponse.data.lastName,
                            email: userResponse.data.emailAddress,
                            status: employee.status,
                            role: employee.role,
                            teamId: employee.teamId
                        });
                    }
                }

                setEmployees(employeeTemp);
                setFilteredEmployees(employeeTemp);
                setEmployeeCount(employeeTemp.length);
                setRefresh(false);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        fetchData();
    }, [employeeStorage.teamId, refresh]);

    const handleDelete = async (id) => {
        try {
            await axios.delete(`https://localhost:7236/api/employees/${id}`);
            setRefresh(true);
            console.log('Employee is now inactive');
        } catch (error) {
            console.error('Error deleting employee:', error);
        }
    };

    const handleActivate = async (id, userId, teamId, role) => {
        try {
            await axios.put(`https://localhost:7236/api/employees/${id}`, {
                userId: userId,
                teamId: teamId,
                role: role,
                status: 'Active'
            });
            setRefresh(true);
            console.log('Employee is now active');
        } catch (error) {
            console.error('Error updating employee status:', error);
        }
    };

    const handleSearch = (searchText) => {
        const filtered = employees.filter((employee) => {
            const fullName = `${employee.firstName} ${employee.lastName}`;
            return fullName.toLowerCase().includes(searchText.toLowerCase());
        });

        setFilteredEmployees(filtered);
    };

    const handleCopy = async () => {
        try {
            const getTeam = await axios.get(`https://localhost:7236/api/teams/${employeeStorage.teamId}`);
            const team = getTeam.data;
            const textarea = document.createElement('textarea');
            textarea.value = team.teamCode;
            document.body.appendChild(textarea);
            textarea.select();
            document.execCommand('copy');
            document.body.removeChild(textarea);
            console.log('Team code copied to clipboard');
        } catch (error) {
            console.error('Error fetching or copying team code:', error);
        }
    };

    const handleDisband = async () => {
        try {
            await axios.delete(`https://localhost:7236/api/teams/${employeeStorage.teamId}`);
            console.log('Team disbanded');
            navigate('/organizations');
        } catch (error) {
            console.error('Error deleting team:', error);
        }
    }

    return (
        <NavBar>
            <TopBarTwo />
            <Stack
                direction="column"
                justifyContent="flex-start"
                alignItems="center"
                spacing={2}
                sx={{
                    width: '100%',
                    height: `calc(100% - 100px)`,
                    overflowY: 'auto',
                    padding: '40px'
                }}
            >
                <Stack
                    direction="row"
                    justifyContent="space-between"
                    alignItems="center"
                    spacing={2}
                    sx={{
                        width: '100%',
                    }}
                >
                    <b>Employees ({employeeCount})</b>
                    <input type='text' className='form-control w-25' placeholder='Search for employees' onChange={(e) => handleSearch(e.target.value)}
                        style={{
                            backgroundColor: '#a0cce0',
                            border: 'none',
                            color: 'white',
                        }}
                    />
                </Stack>
                <hr
                    style={{
                        width: '100%',
                        height: '1px',
                        backgroundColor: 'black'
                    }}
                />
                <table className="table table-striped align-middle text-center w-100">
                    <thead>
                        <tr>
                            <th scope="col"
                                style={{
                                    backgroundColor: '#6ea8cb',
                                }}
                            >
                                First Name
                            </th>
                            <th scope="col"
                                style={{
                                    backgroundColor: '#6ea8cb',
                                }}
                            >
                                Last Name
                            </th>
                            <th scope="col"
                                style={{
                                    backgroundColor: '#6ea8cb',
                                }}
                            >
                                Email
                            </th>
                            <th scope="col"
                                style={{
                                    backgroundColor: '#6ea8cb',
                                }}
                            >
                                Status
                            </th>
                            <th scope="col"
                                style={{
                                    backgroundColor: '#6ea8cb',
                                }}
                            >
                                Action
                            </th>
                        </tr>
                    </thead>
                    {refresh ? (
                        <tbody>
                            <tr>
                                <td colSpan="5" style={{ textAlign: 'center' }}>
                                    <div className="spinner-border" role="status">
                                        <span className="visually-hidden">Loading...</span>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    ) : (
                        <tbody>
                            {filteredEmployees && filteredEmployees.map((employee, index) => (
                                <tr key={index}>
                                    <td>{employee.firstName}</td>
                                    <td>{employee.lastName}</td>
                                    <td>{employee.email}</td>
                                    <td>{employee.status}</td>
                                    <td>
                                        {employee.status === 'Active' ? (
                                            <button type="button" className="btn" onClick={() => handleDelete(employee.id)}>
                                                <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" className="bi bi-trash" viewBox="0 0 16 16">
                                                    <path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5Zm2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5Zm3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0V6Z" />
                                                    <path d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1v1ZM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4H4.118ZM2.5 3h11V2h-11v1Z" />
                                                </svg>
                                            </button>
                                        ) : (
                                            <button type="button" className="btn" onClick={() => handleActivate(employee.id, employee.userId, employee.teamId, employee.role)}>
                                                <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" className="bi bi-person-plus" viewBox="0 0 16 16">
                                                    <path d="M6 8a3 3 0 1 0 0-6 3 3 0 0 0 0 6zm2-3a2 2 0 1 1-4 0 2 2 0 0 1 4 0zm4 8c0 1-1 1-1 1H1s-1 0-1-1 1-4 6-4 6 3 6 4zm-1-.004c-.001-.246-.154-.986-.832-1.664C9.516 10.68 8.289 10 6 10c-2.29 0-3.516.68-4.168 1.332-.678.678-.83 1.418-.832 1.664h10z" />
                                                    <path fillRule="evenodd" d="M13.5 5a.5.5 0 0 1 .5.5V7h1.5a.5.5 0 0 1 0 1H14v1.5a.5.5 0 0 1-1 0V8h-1.5a.5.5 0 0 1 0-1H13V5.5a.5.5 0 0 1 .5-.5z" />
                                                </svg>
                                            </button>
                                        )}
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    )}
                </table>
                <Stack
                    direction="row"
                    justifyContent="center"
                    alignItems="center"
                    spacing={2}
                    sx={{
                        width: '100%',
                    }}
                >
                    <button type="button" className="btn btn-success" style={{ width: "25%" }} onClick={handleCopy}>Copy Invitation Code</button>
                    <button type="button" className="btn btn-danger" style={{ width: "25%" }} onClick={handleDisband}>Disband Team</button>
                </Stack>
            </Stack>
        </NavBar >
    )
}

export default Employees