import React, { useState, useEffect } from 'react';
import NavBar from "../Shared/NavBar";
import TopBarTwo from "../Shared/TopBarTwo";
import "./Employees.css";
import AccountCircleIcon from '@mui/icons-material/AccountCircle';

function Employees() {
    const [employees, setEmployees] = useState([]);
    const [searchQuery, setSearchQuery] = useState('');
    const [listView, setListView] = useState(true);
    const [teamNamePlaceholder, setTeamNamePlaceholder] = useState('');
    const employeeStorage = JSON.parse(localStorage.getItem("employeeData"));

    useEffect(() => {
        // Fetch employees
        fetch('https://localhost:7236/api/employees')
            .then(response => response.json())
            .then(data => {
                const filteredEmployees = data.filter(employee => employee.role !== "Admin");
                Promise.all(filteredEmployees.map(employee =>
                    fetch(`https://localhost:7236/api/users/${employee.userId}`)
                        .then(response => response.json())
                        .then(userData => ({ ...employee, ...userData }))
                ))
                    .then(employeesWithUserDetails => setEmployees(employeesWithUserDetails));
            });

        // Fetch team data
        fetch(`https://localhost:7236/api/teams/${employeeStorage.teamId}`)
            .then(response => response.json())
            .then(data => {
                const businessType = data.businessType;
                setTeamNamePlaceholder(businessType);
            });

    }, [employeeStorage.teamId]);

    const sortEmployees = (order) => {
        const sortedEmployees = [...employees];

        sortedEmployees.sort((a, b) => {
            const nameA = `${a.firstName} ${a.lastName}`.toUpperCase();
            const nameB = `${b.firstName} ${b.lastName}`.toUpperCase();

            if (order === 'asc') {
                return nameA.localeCompare(nameB);
            } else if (order === 'desc') {
                return nameB.localeCompare(nameA);
            }

            return 0;
        });

        setEmployees(sortedEmployees);
    };

    const handleChangeView = () => {
        setListView(!listView); // Toggle the view
    }

    const filterEmployees = (data, query) => {
        return data.filter(employee => {
            const fullName = `${employee.firstName} ${employee.lastName}`;
            return fullName.toLowerCase().includes(query.toLowerCase());
        });
    };

    return (
        <NavBar>
            <TopBarTwo />
            <div className="employee-container">
                <div className="employee-header">
                    <div className="employee-title">
                        <h2>Employees({employees.length})</h2>
                    </div>

                    <div className="employee-controls">
                        <div className="sort-buttons">
                            <button onClick={() => sortEmployees('asc')}>Sort A-Z</button>
                            <button onClick={() => sortEmployees('desc')}>Sort Z-A</button>
                        </div>
                        <div className="search-input">
                            <input
                                type="text"
                                placeholder="Find an Employee"
                                value={searchQuery}
                                onChange={(e) => {
                                    setSearchQuery(e.target.value);
                                    console.log(e.target.value); // Add this line for debugging
                                }}
                            />
                        </div>
                        <div className="view-button">
                            <button onClick={handleChangeView}>Change View</button>
                        </div>
                    </div>
                </div>

                <div>
                    <hr className="employee-divider" />
                </div>
                {listView ? (
                    <div className="employee-department">
                        <h4>{teamNamePlaceholder}</h4>
                        {employees.map((employee, index) => {
                            const fullName = `${employee.firstName} ${employee.lastName}`;
                            const match = fullName.toLowerCase().includes(searchQuery.toLowerCase());

                            if (searchQuery && !match) {
                                return null; // Skip this employee if searchQuery is provided but there's no match
                            }

                            return (
                                <div className="employee-details" key={index}>
                                    <div className="employee-info">
                                        <div className="icon-container">
                                            <AccountCircleIcon className="custom-icon" fontSize="large" />
                                        </div>
                                        <div>
                                            <h5>{employee.firstName} {employee.lastName}</h5>
                                            <div>
                                                {employee.dateTimeCreated}
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            );
                        })}
                    </div>
                ) : (
                    <div className="employee-table">
                        {/* Render employees in table format */}
                        <table className="styled-table">
                            <thead>
                                <tr>
                                    <th>Name</th>
                                    <th>Email</th>
                                    <th>Status</th>
                                    <th>Date Created</th>
                                </tr>
                            </thead>
                            <tbody>
                                {filterEmployees(employees, searchQuery).map((employee, index) => (
                                    <tr key={index}>
                                        <td>{employee.firstName} {employee.lastName}</td>
                                        <td>{employee.emailAddress}</td>
                                        <td>{employee.status}</td>
                                        <td>{employee.dateTimeCreated}</td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                )}


            </div>

        </NavBar>
    )
}

export default Employees;
