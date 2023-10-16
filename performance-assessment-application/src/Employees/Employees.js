import React, { useState, useEffect } from 'react';
import NavBar from "../Shared/NavBar";
import TopBarTwo from "../Shared/TopBarTwo";
import "./Employees.css";

function Employees() {
    const [employees, setEmployees] = useState([]);
    const [searchQuery, setSearchQuery] = useState('');

    useEffect(() => {
        fetch('https://localhost:7236/api/employees')
            .then(response => response.json())
            .then(data => {
                // Fetch user details based on userId
                Promise.all(data.map(employee =>
                    fetch(`https://localhost:7236/api/users/${employee.userId}`)
                        .then(response => response.json())
                        .then(userData => ({ ...employee, ...userData }))
                ))
                    .then(employeesWithUserDetails => setEmployees(employeesWithUserDetails));
            });
    }, []);


    return (
        <NavBar>
            <TopBarTwo />
            <div className="employee-container">
                <div className="employee-header">
                    <div className="employee-title">
                        <h2>Employees({employees.length})</h2>
                    </div>
                    <div>
                        <select>
                            <option value="department" disabled selected>Sort by Department</option>
                            <option value="sales">Sales</option>
                            <option value="manufacturing">Manufacturing</option>
                            <option value="construction">Construction</option>
                            <option value="delivery">Delivery and Logistics</option>
                        </select>
                    </div>
                    <div>
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
                </div>

                <div>
                    <hr className="employee-divider" />
                </div>

                <div className="employee-department">
                    <h4>Manufacturing</h4>
                    {employees.map((employee, index) => {
                        const fullName = `${employee.firstName} ${employee.lastName}`;
                        const match = fullName.toLowerCase().includes(searchQuery.toLowerCase());

                        if (searchQuery && !match) {
                            return null; // Skip this employee if searchQuery is provided but there's no match
                        }

                        return (
                            <div className="employee-details" key={index}>
                                <h5>{employee.firstName} {employee.lastName}</h5>
                                <div>
                                    {employee.dateTimeCreated}
                                </div>
                            </div>
                        );
                    })}
                </div>

                <div>
                    <hr className="employee-divider" />
                </div>

                <div className="employee-department">
                    <h4>Sales Department</h4>
                    {employees.map((employee, index) => {
                        const fullName = `${employee.firstName} ${employee.lastName}`;
                        const match = fullName.toLowerCase().includes(searchQuery.toLowerCase());

                        if (searchQuery && !match) {
                            return null; // Skip this employee if searchQuery is provided but there's no match
                        }

                        return (
                            <div className="employee-details" key={index}>
                                <h5>{employee.firstName} {employee.lastName}</h5>
                                <div>
                                    {employee.dateTimeCreated}
                                </div>
                            </div>
                        );
                    })}
                </div>


            </div>

        </NavBar>
    )
}

export default Employees;
