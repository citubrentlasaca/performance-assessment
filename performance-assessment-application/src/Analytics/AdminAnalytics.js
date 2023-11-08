import React, { useState, useEffect } from 'react';
import NavBar from '../Shared/NavBar';
import TopBarThree from '../Shared/TopBarThree';
import './AdminAnalytics.css';
import analyticsheader from './analytics-trophy.png';

const AdminAnalytics = () => {
    const [employeeData, setEmployeeData] = useState([]);
    const employee = JSON.parse(localStorage.getItem("employeeData"));

    useEffect(() => {
        fetch(`https://localhost:7236/api/assessments/employee/${employee.id}`)
            .then((response) => response.json())
            .then((data) => {
                // Find the first assessment with the title "Daily Performance Report"
                const dailyPerformanceReportAssessment = data.find(
                    (assessment) => assessment.title === 'Daily Performance Report'
                );

                if (dailyPerformanceReportAssessment) {
                    // Use the found assessment's ID to fetch employee data
                    fetch(
                        `https://localhost:7236/api/analytics/performance/get-analytics-by-assessmentid?assessmentId=${dailyPerformanceReportAssessment.id}`
                    )
                        .then((response) => response.json())
                        .then((analyticsData) => {
                            setEmployeeData(analyticsData);
                        })
                        .catch((error) => {
                            console.error('Error fetching employee data:', error);
                        });
                } else {
                    console.error('Daily Performance Report assessment not found.');
                }
            })
            .catch((error) => {
                console.error('Error fetching assessments:', error);
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
                                {employee.averageResult.toFixed(2) * 100} Points
                            </div>
                        </div>
                    </div>
                ))}
            </div>
        </NavBar>
    );
};

export default AdminAnalytics;