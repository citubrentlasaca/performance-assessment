import React, { useState, useEffect } from 'react';
import './EmployeeAnalytics.css';
import NavBar from '../Shared/NavBar';
import SalesChart from './SalesChart';
import TopBarThree from '../Shared/TopBarThree';
import axios from 'axios';

const EmployeeAnalytics = () => {
  const [title, setTitle] = useState('');
  const [employeeId, setEmployeeId] = useState(null);

  useEffect(() => {
    const employeeData = JSON.parse(localStorage.getItem('employeeData'));

    if (employeeData && employeeData.id) {
      setEmployeeId(employeeData.id);
    }
  }, []);

  useEffect(() => {
    if (employeeId !== null) {
      axios
        .get(`https://localhost:7236/api/results/employees/${employeeId}`)
        .then((resultResponse) => {
          if (resultResponse.data.length > 0) {
            const sortedAssessments = resultResponse.data.sort((a, b) => {
              return new Date(b.dateTimeCreated) - new Date(a.dateTimeCreated);
            });

            const latestAssessment = sortedAssessments[0];

            axios
              .get(`https://localhost:7236/api/answers/get-by-employee-and-assessment?employeeId=${employeeId}&assessmentId=${latestAssessment.assessmentId}`)
              .then((response) => {
                const responseData = response.data;
                const apiTitle = responseData.title;
                setTitle(apiTitle);
              })
              .catch((error) => {
                console.error(error);
              });
          } else {
            console.error('No assessment found for the employee.');
          }
        })
        .catch((error) => {
          console.error('Error fetching assessments:', error);
        });
    }
  }, [employeeId]);

  return (
    <NavBar>
      <TopBarThree />
      <div className="employee-analytics-container">
        <h1>{title}</h1>
        <SalesChart />
      </div>
    </NavBar>
  );
};

export default EmployeeAnalytics;