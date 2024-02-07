import React, { useEffect, useRef, useState } from 'react';
import Chart from 'chart.js/auto';
import axios from 'axios';
import ChartDataLabels from 'chartjs-plugin-datalabels';
import 'chartjs-adapter-moment';

const SalesChart = ({ assessmentId }) => {
  const chartRefs = useRef([]);
  const chartInstances = useRef([]);
  const [chartData, setChartData] = useState([]);
  const [employeeId, setEmployeeId] = useState(null);

  useEffect(() => {
    const employeeData = JSON.parse(localStorage.getItem('employeeData'));

    if (employeeData && employeeData.id) {
      setEmployeeId(employeeData.id);
    }
  }, []);

  useEffect(() => {
    const fetchChartData = async () => {
      try {
        if (!employeeId) return;
  
        const response = await axios.get(`https://workpa.azurewebsites.net/api/answers/get-by-employee-and-assessment?employeeId=${employeeId}&assessmentId=${assessmentId}`);
        const items = response.data.items;
        const data = items.map(item => ({
          question: item.question,
          labels: item.answers.map(answer => {
            const dateTimeAnswered = new Date(answer.dateTimeAnswered + 'Z');
            return dateTimeAnswered.toISOString().split('T')[0];
          }),
          data: item.answers.map(answer => answer.counterValue),
        }));
        console.log(data);
        setChartData(data);
      } catch (error) {
        console.error('Error fetching chart data:', error);
      }
    };
  
    if (assessmentId && employeeId) {
      fetchChartData();
    }
  }, [assessmentId, employeeId]);
  

  useEffect(() => {
    if (chartRefs.current.length > 0 && chartData.length > 0) {
      chartData.forEach((data, index) => {
        const chartRef = chartRefs.current[index];
        const chartInstance = chartInstances.current[index];

        const chartData = {
          labels: data.labels,
          datasets: [{
            label: data.question,
            data: data.data,
            backgroundColor: 'rgba(75, 192, 192, 0.2)',
            borderColor: 'rgba(75, 192, 192, 1)',
            borderWidth: 1,
            barPercentage: 0.8 / data.data.length,
          }],
        };

        const options = {
          scales: {
            x: {
              type: 'time',
              time: {
                unit: 'day',
              },
              position: 'bottom',
            },
            y: {
              beginAtZero: true,
              suggestedMax: Math.max(...data.data) + 10,
            },
          },
          plugins: {
            datalabels: {
              display: true,
              color: 'black',
            },
          },
        };

        if (chartInstance) {
          chartInstance.data = chartData;
          chartInstance.options = options;
          chartInstance.update();
        } else {
          chartInstances.current[index] = new Chart(chartRef, {
            type: 'bar',
            data: chartData,
            options: options,
            plugins: [ChartDataLabels],
          });
        }
      });
    }
  }, [chartData]);

  return (
    <div style={{paddingBottom: "20px", marginTop: 0, marginBottom: "20px"}}>
      {chartData.map((data, index) => (
        <div key={index} style={{paddingBottom: "20px"}}>
          <canvas ref={el => chartRefs.current[index] = el} />
        </div>
      ))}
    </div>
  );
};

export default SalesChart;
