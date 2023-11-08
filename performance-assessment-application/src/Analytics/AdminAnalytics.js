import React, { useState, useEffect } from 'react';
import NavBar from '../Shared/NavBar';
import TopBarThree from '../Shared/TopBarThree';
import './AdminAnalytics.css';
import analyticsheader from './analytics-trophy.png';

const AdminAnalytics = () => {
    /*const employeeData = Array.from({ length: 10 }, (_, index) => ({
        rank: index + 1,
        name: 'Name',
        points: 100,
    }));*/
    const [employeeData, setEmployeeData] = useState([]);

    useEffect(() => {
        // Make an API request to fetch employee data
        fetch('https://localhost:7236/api/analytics/performance/get-analytics-by-assessmentid?assessmentId=1')
            .then((response) => response.json())
            .then((data) => {
                setEmployeeData(data);
            })
            .catch((error) => {
                console.error('Error fetching employee data:', error);
            });
    }, []);

    return (
        <NavBar>
            <TopBarThree />
            <div className="leaderboard-header">
                <img src={analyticsheader} alt="Analytics Header" />
            </div>
            <div className="scrollable-content">
                {employeeData.map((employee, index) => (
                    <div className="employee-rank-container" key={index}>
                    <div className="name-container">
                        <div className="employee-rank">{index + 1}</div>
                            <div className="employee-analytics-name">
                                {employee.firstName} {employee.lastName}
                            </div>
                            <div className="employee-analytics-points">
                                {employee.averageResult.toFixed(2)} Points
                            </div>
                        </div>
                    </div>
                ))}
            </div>
        </NavBar>
    );
};

export default AdminAnalytics;