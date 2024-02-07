import React, { useState, useEffect } from 'react';
import './EmployeeAnalytics.css';
import NavBar from '../Shared/NavBar';
import SalesChart from './SalesChart';
import TopBarThree from '../Shared/TopBarThree';
import axios from 'axios';
import { Stack } from "@mui/material";

const EmployeeAnalytics = () => {
  const [title, setTitle] = useState('');
  const [employeeId, setEmployeeId] = useState(null);
  const [assessments, setAssessments] = useState([]);
  const [currentAssessmentId, setCurrentAssessmentId] = useState(null);

  useEffect(() => {
    const employeeData = JSON.parse(localStorage.getItem('employeeData'));

    if (employeeData && employeeData.id) {
      setEmployeeId(employeeData.id);
    }
  }, []);

  useEffect(() => {
    if (employeeId !== null) {
      axios
        .get(`https://workpa.azurewebsites.net/api/results/employees/${employeeId}`)
        .then((resultResponse) => {
          if (resultResponse.data.length > 0) {
            const uniqueAssessmentIds = [...new Set(resultResponse.data.map(item => item.assessmentId))];
            const promises = uniqueAssessmentIds.map(assessmentId => {
              return axios.get(`https://workpa.azurewebsites.net/api/assessments/${assessmentId}`)
                .then((response) => {
                  return { id: assessmentId, title: response.data.title };
                })
                .catch((error) => {
                  console.error(`Error fetching title for assessment ID ${assessmentId}:`, error);
                  return null;
                });
            });

            Promise.all(promises)
              .then((titles) => {
                const validTitles = titles.filter(title => title !== null);
                setAssessments(validTitles);
              })
              .catch((error) => {
                console.error('Error fetching assessment titles:', error);
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

  const handleAssessmentChange = (event) => {
    const selectedIndex = event.target.value;
    const selectedAssessmentId = assessments[selectedIndex]?.id;
    setCurrentAssessmentId(selectedAssessmentId);

    if (employeeId && selectedAssessmentId) {
      axios.get(`https://workpa.azurewebsites.net/api/assessments/${selectedAssessmentId}`)
        .then((response) => {
          setTitle(response.data.title);
        })
        .catch((error) => {
          console.error('Error fetching assessment title:', error);
        });
    }
  };

  return (
    <NavBar>
      <TopBarThree />
      <Stack
        direction="row"
        justifyContent="space-between"
        alignItems="center"
        spacing={2}
        sx={{
          width: "100%",
          paddingLeft: "20px",
          paddingTop: "20px"
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
              {`${assessment.title}`}
            </option>
          ))}
        </select>
      </Stack>
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
        <h4
          style={{
            color: "#055C9D",
            fontWeight: "bold"
          }}
        >
          {title}
        </h4>
        {currentAssessmentId && <SalesChart assessmentId={currentAssessmentId} />}
      </Stack>
    </NavBar>
  );
};

export default EmployeeAnalytics;