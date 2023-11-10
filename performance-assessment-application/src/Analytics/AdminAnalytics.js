import React, { useState, useEffect } from 'react';
import NavBar from '../Shared/NavBar';
import TopBarTwo from '../Shared/TopBarTwo';
import './AdminAnalytics.css';
import analyticsheader from './analytics-trophy.png';
import { Stack } from "@mui/material";

const AdminAnalytics = () => {
    const [employeeData, setEmployeeData] = useState([]);
    const [selectedMonth, setSelectedMonth] = useState('');
    const [selectedYear, setSelectedYear] = useState('');
    const employee = JSON.parse(localStorage.getItem("employeeData"));
    const [assessmentId, setAssessmentId] = useState(null);

    useEffect(() => {
        fetch(`https://localhost:7236/api/assessments/employee/${employee.id}`)
            .then((response) => response.json())
            .then((data) => {
                // Find the first assessment with the title "Daily Performance Report"
                const dailyPerformanceReportAssessment = data.find(
                    (assessment) => assessment.title === 'Daily Performance Report'
                );

                if (dailyPerformanceReportAssessment) {
                    setAssessmentId(dailyPerformanceReportAssessment.id);
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

    useEffect(() => {
        if (selectedMonth && assessmentId) {
            fetch(`https://localhost:7236/api/analytics/performance/get-analytics-by-assessmentid-and-monthnumber?assessmentId=${assessmentId}&monthNumber=${selectedMonth}`)
                .then((response) => response.json())
                .then((analyticsData) => {
                    setEmployeeData(analyticsData);
                })
                .catch((error) => {
                    console.error('Error fetching employee data:', error);
                });
        }
    }, [selectedMonth, assessmentId]);

    useEffect(() => {
        // Make the API request with the selected year parameter
        if (selectedYear && assessmentId) {
            fetch(`https://localhost:7236/api/analytics/performance/get-analytics-by-assessmentid-and-year?assessmentId=${assessmentId}&year=${selectedYear}`)
                .then((response) => response.json())
                .then((analyticsData) => {
                    setEmployeeData(analyticsData);
                })
                .catch((error) => {
                    console.error('Error fetching employee data:', error);
                });
        }
    }, [selectedYear, assessmentId]);

    const handleMonthChange = (event) => {
        setSelectedMonth(event.target.value);
    };

    const generateYearOptions = () => {
        const currentYear = new Date().getFullYear();
        const years = Array.from({ length: 25 }, (_, index) => currentYear - index); 
        return years.map((year) => (
            <option key={year} value={year}>
                {year}
            </option>
        ));
    };

    const handleYearChange = (event) => {
        const selectedYear = event.target.value;
        setSelectedYear(selectedYear);
    };
    
    return (
        <NavBar>
            <TopBarTwo />
            <div className="leaderboard-header">
                <img src={analyticsheader} alt="Analytics Header" />
            </div>
            <Stack direction="row" justifyContent="flex-end" alignItems="left" sx={{ marginTop: "15px" }}>
                <select
                    className="form-select select-month-option"
                    aria-label="Select Month"
                    onChange={handleMonthChange}
                >
                    <option value="" selected>Select Month</option>
                    <option value="1">January</option>
                    <option value="2">February</option>
                    <option value="3">March</option>
                    <option value="4">April</option>
                    <option value="5">May</option>
                    <option value="6">June</option>
                    <option value="7">July</option>
                    <option value="8">August</option>
                    <option value="9">September</option>
                    <option value="10">October</option>
                    <option value="11">November</option>
                    <option value="12">December</option>
                </select>
                <select className="form-select select-month-option" aria-label="Default select example" onChange={handleYearChange}>
                    <option value="" disabled selected>Select Year</option>
                    {generateYearOptions()}
                </select>
            </Stack>

            <div className="scrollable-content">
                {employeeData.length === 0 ? (
                    <div className="centered-text">No results found</div>
                ) : (
                    employeeData.map((employee, index) => (
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
                    ))
                )}
            </div>
        </NavBar>
    );
};

export default AdminAnalytics;