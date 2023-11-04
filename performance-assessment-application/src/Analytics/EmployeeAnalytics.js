import React, { useState, useEffect } from 'react';
import './EmployeeAnalytics.css';
import NavBar from '../Shared/NavBar';
import SalesChart from './SalesChart';
import TopBarThree from '../Shared/TopBarThree';
import axios from 'axios';

const EmployeeAnalytics = () => {
  const [title, setTitle] = useState('');
  const [employeeId, setEmployeeId] = useState(null); // State for storing the employee ID

  useEffect(() => {
    // Retrieve the employee ID from localStorage
    const employeeData = JSON.parse(localStorage.getItem('employeeData'));

    if (employeeData && employeeData.id) {
      setEmployeeId(employeeData.id);
    }
  }, []);

  useEffect(() => {
    // Use the dynamically set employeeId in the API request
    if (employeeId !== null) {
      axios
        .get(`https://localhost:7236/api/answers/get-by-employee-and-assessment?employeeId=${employeeId}&assessmentId=1`)
        .then((response) => {
          const responseData = response.data;
          const apiTitle = responseData.title;
          setTitle(apiTitle);
        })
        .catch((error) => {
          console.error(error);
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