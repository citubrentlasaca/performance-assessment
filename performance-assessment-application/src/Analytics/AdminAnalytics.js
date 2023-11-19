import React, { useState, useEffect } from 'react';
import NavBar from '../Shared/NavBar';
import TopBarTwo from '../Shared/TopBarTwo';
import analyticsheader from './analytics-trophy.png';
import { Stack } from "@mui/material";

const AdminAnalytics = () => {
    const [assessments, setAssessments] = useState([]);
    const [selectedMonth, setSelectedMonth] = useState('');
    const [selectedYear, setSelectedYear] = useState('');
    const [employeeData, setEmployeeData] = useState([]);
    const [currentAssessment, setCurrentAssessment] = useState(null);

    useEffect(() => {
        const employee = JSON.parse(localStorage.getItem("employeeData"));

        fetch(`https://workpa.azurewebsites.net/api/assessments/employee/${employee.id}`)
            .then((response) => response.json())
            .then((data) => {
                setAssessments(data);
                const dailyPerformanceReportAssessment = data.find(
                    (assessment) => assessment.title === 'Daily Performance Report'
                );

                if (dailyPerformanceReportAssessment) {
                    setCurrentAssessment(dailyPerformanceReportAssessment);
                } else {
                    console.error('Daily Performance Report assessment not found.');
                }
            })
            .catch((error) => {
                console.error('Error fetching assessments:', error);
            });
    }, []);

    useEffect(() => {
        if (selectedMonth && currentAssessment) {
            fetch(`https://workpa.azurewebsites.net/api/analytics/performance/get-analytics-by-assessmentid-and-monthnumber?assessmentId=${currentAssessment.id}&monthNumber=${selectedMonth}`)
                .then((response) => response.json())
                .then((analyticsData) => {
                    setEmployeeData(analyticsData);
                })
                .catch((error) => {
                    console.error('Error fetching employee data:', error);
                });
        }
    }, [selectedMonth, currentAssessment]);

    useEffect(() => {
        if (selectedYear && currentAssessment) {
            fetch(`https://workpa.azurewebsites.net/api/analytics/performance/get-analytics-by-assessmentid-and-year?assessmentId=${currentAssessment.id}&year=${selectedYear}`)
                .then((response) => response.json())
                .then((analyticsData) => {
                    setEmployeeData(analyticsData);
                })
                .catch((error) => {
                    console.error('Error fetching employee data:', error);
                });
        }
    }, [selectedYear, currentAssessment]);

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

    const handleAssessmentChange = async (event) => {
        const selectedAssessmentIndex = event.target.value;
        const selectedAssessment = assessments[selectedAssessmentIndex];

        if (selectedAssessment) {
            try {
                setSelectedMonth('');
                setSelectedYear('');

                const response = await fetch(`https://workpa.azurewebsites.net/api/analytics/performance/get-analytics-by-assessmentid?assessmentId=${selectedAssessment.id}`);
                const analyticsData = await response.json();
                setEmployeeData(analyticsData);
                setCurrentAssessment(selectedAssessment);
            } catch (error) {
                console.error('Error fetching employee data:', error);
            }
        }
    };

    return (
        <NavBar>
            <TopBarTwo />
            <Stack
                direction="column"
                justifyContent="flex-start"
                alignItems="center"
                spacing={2}
                sx={{
                    width: "100%",
                    height: "calc(100% - 100px)",
                }}
            >
                <img src={analyticsheader}
                    style={{
                        width: "100%",
                        height: "120px"
                    }}
                />
                <Stack
                    direction="column"
                    justifyContent="flex-start"
                    alignItems="center"
                    spacing={4}
                    sx={{
                        width: "100%",
                        height: "calc(100% - 120px)",
                        padding: "20px",
                        overflow: 'auto',
                    }}
                >
                    <Stack
                        direction="row"
                        justifyContent="space-between"
                        alignItems="center"
                        spacing={2}
                        sx={{
                            width: "100%"
                        }}
                    >
                        <select
                            className="form-select select-month-option"
                            aria-label="Select Assessment"
                            onChange={handleAssessmentChange}
                            style={{
                                width: "25%",
                            }}
                        >
                            <option value="" disabled selected>Select Assessment</option>
                            {assessments.map((assessment, index) => (
                                <option key={index} value={index}>
                                    {`${assessment.title} ${index + 1}`}
                                </option>
                            ))}
                        </select>
                        <Stack
                            direction="row"
                            justifyContent="flex-end"
                            alignItems="center"
                            spacing={2}
                            sx={{
                                width: "100%"
                            }}
                        >
                            <select
                                className="form-select select-month-option"
                                aria-label="Select Month"
                                value={selectedMonth}
                                onChange={handleMonthChange}
                                style={{
                                    width: "25%"
                                }}
                            >
                                <option value="" disabled selected>Select Month</option>
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
                            <select
                                className="form-select select-month-option"
                                aria-label="Default select example"
                                value={selectedYear}
                                onChange={handleYearChange}
                                style={{
                                    width: "25%"
                                }}
                            >
                                <option value="" disabled selected>Select Year</option>
                                {generateYearOptions()}
                            </select>
                        </Stack>
                    </Stack>
                    {employeeData.length === 0 ? (
                        <Stack
                            direction="column"
                            justifyContent="center"
                            alignItems="center"
                            spacing={2}
                            sx={{
                                width: "100%",
                                height: "100%"
                            }}
                        >
                            <p className='mb-0'>No results found</p>
                        </Stack>
                    ) : (
                        <Stack
                            direction="column"
                            justifyContent="flex-start"
                            alignItems="center"
                            spacing={2}
                            sx={{
                                width: "75%",
                            }}
                        >
                            {employeeData.map((employee, index) => (
                                <div
                                    key={index}
                                    style={{
                                        width: "100%",
                                        backgroundColor: "#27C6D9",
                                        padding: "20px",
                                        borderRadius: "10px",
                                        marginBottom: "10px"
                                    }}
                                >
                                    <Stack
                                        direction="row"
                                        justifyContent="space-between"
                                        alignItems="center"
                                        spacing={2}
                                        sx={{
                                            width: "100%",
                                            height: "100%"
                                        }}
                                    >
                                        <b>{index + 1}</b>
                                        <p className='mb-0'>{employee.firstName} {employee.lastName}</p>
                                        <b>{(employee.averageResult.toFixed(2)) * 100} Points</b>
                                    </Stack>
                                </div>
                            ))}
                        </Stack>
                    )}
                </Stack>
            </Stack>
        </NavBar>
    );
};

export default AdminAnalytics;