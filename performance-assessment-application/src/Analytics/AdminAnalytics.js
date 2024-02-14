import React, { useState, useEffect } from 'react';
import NavBar from '../Shared/NavBar';
import TopBarTwo from '../Shared/TopBarTwo';
import analyticsheader from './analytics-trophy.png';
import { Stack } from "@mui/material";
import axios from 'axios';
import { Bar } from 'react-chartjs-2';

const AdminAnalytics = () => {
    const [assessments, setAssessments] = useState([]);
    const [selectedMonth, setSelectedMonth] = useState('');
    const [selectedYear, setSelectedYear] = useState('');
    const [employeeData, setEmployeeData] = useState([]);
    const [currentAssessment, setCurrentAssessment] = useState(null);
    const [overall, setOverall] = useState('');
    const employeeStorage = JSON.parse(localStorage.getItem('employeeData'));
    const [employees, setEmployees] = useState([]);
    const [chartData, setChartData] = useState(null);
    const [selectedEmployee, setSelectedEmployee] = useState(null);

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

    useEffect(() => {
        const fetchData = async () => {
            try {
                const employeeResponse = await axios.get(`https://workpa.azurewebsites.net/api/employees/teams/${employeeStorage.teamId}`);
                const usersResponse = await axios.get(`https://workpa.azurewebsites.net/api/users`);
                const users = usersResponse.data;
                const updatedEmployees = [];

                for (const employee of employeeResponse.data) {
                    const resultsResponse = await axios.get(`https://workpa.azurewebsites.net/api/results/employees/${employee.id}`);
                    const assessmentResults = [];

                    for (const result of resultsResponse.data) {
                        const assessmentResponse = await axios.get(`https://workpa.azurewebsites.net/api/assessments/${result.assessmentId}`);
                        const assessment = assessmentResponse.data;
                        assessmentResults.push({ assessmentTitle: assessment.title, score: result.score });
                    }

                    const calculateAverageScores = (results) => {
                        const scoreMap = {};
                        results.forEach((result) => {
                            if (!scoreMap[result.assessmentTitle]) {
                                scoreMap[result.assessmentTitle] = [result.score];
                            } else {
                                scoreMap[result.assessmentTitle].push(result.score);
                            }
                        });
                        const averages = Object.keys(scoreMap).map((title) => {
                            const scores = scoreMap[title];
                            const averageScore = scores.reduce((acc, curr) => acc + curr, 0) / scores.length;
                            return { assessmentTitle: title, average: averageScore.toFixed(2) };
                        });
                        return averages;
                    };

                    const averageScores = calculateAverageScores(assessmentResults);

                    for (const user of users) {
                        if (employee.userId === user.id && employee.role === 'User') {
                            updatedEmployees.push({
                                id: user.id,
                                employeeId: employee.id,
                                firstName: user.firstName,
                                lastName: user.lastName,
                                assessmentResults: averageScores
                            });
                        }
                    }
                }

                setEmployees(updatedEmployees);
                setChartData({
                    labels: selectedEmployee.assessmentResults.map(assessment => assessment.assessmentTitle),
                    datasets: [
                        {
                            label: 'Average Score Percentage',
                            data: selectedEmployee.assessmentResults.map(assessment => (assessment.average * 100).toFixed(0)),
                        }
                    ]
                });

            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        fetchData();
    }, [selectedEmployee]);

    const handleEmployeeChange = (employee) => {
        setSelectedEmployee(employee);
        console.log(employee);
    };

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
        if (selectedAssessmentIndex === 'Overall') {
            setOverall(selectedAssessmentIndex)
        }
        else {
            setOverall('');
        }
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
                            <option value="Overall">Overall Performance</option>
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
                    {overall === 'Overall' && (
                        <div className="accordion w-100" id="accordionExample">
                            {employees && employees.map((employee, index) => (
                                <div className="accordion-item" key={index}>
                                    <h2 className="accordion-header">
                                        <button onClick={() => handleEmployeeChange(employee)} className="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target={`#collapse${index}`} aria-expanded="false" aria-controls={`collapse${index}`}>
                                            {employee.firstName} {employee.lastName}
                                        </button>
                                    </h2>
                                    <div id={`collapse${index}`} className="accordion-collapse collapse" data-bs-parent="#accordionExample">
                                        <div className="accordion-body">
                                            {chartData && (
                                                <Bar
                                                    data={chartData}
                                                    options={{
                                                        plugins: {
                                                            title: {
                                                                display: true,
                                                                text: 'Overall Performance',
                                                                font: {
                                                                    size: 18
                                                                }
                                                            }
                                                        }
                                                    }}
                                                />
                                            )}
                                        </div>
                                    </div>
                                </div>
                            ))}
                        </div>
                    )}
                    {employeeData.length === 0 && overall !== "Overall" ? (
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