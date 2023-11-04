import React, { useEffect, useRef, useState } from 'react';
import Chart from 'chart.js/auto';
import axios from 'axios';

const SalesChart = () => {
  const chartRef = useRef(null);
  const chartInstance = useRef(null);
  const [chartData, setChartData] = useState(null);

  useEffect(() => {
    const employeeData = JSON.parse(localStorage.getItem('employeeData'));

    if (employeeData && employeeData.id) {
      const employeeId = employeeData.id;
      const apiUrl = `https://localhost:7236/api/answers/get-by-employee-and-assessment?employeeId=${employeeId}&assessmentId=1`;

      axios.get(apiUrl)
        .then(response => {
          const counterValues = response.data.items.map(item => item.answers[0].counterValue);

          setChartData({
            labels: response.data.items.map(item => item.question),
            counterValues,
          });
        })
        .catch(error => {
          console.error('Error fetching data:', error);
        });
    }
  }, []);

  useEffect(() => {
    if (chartRef.current && chartData) {
      const data = {
        labels: chartData.labels,
        datasets: [
          {
            label: 'Number of Sales',
            data: chartData.counterValues,
            backgroundColor: 'rgba(75, 192, 192, 0.2)',
            borderColor: 'rgba(75, 192, 192, 1)',
            borderWidth: 1,
          },
        ],
      };

      const options = {
        scales: {
          x: {
            type: 'category',
            position: 'bottom',
          },
          y: {
            beginAtZero: true,
            suggestedMax: Math.max(...data.datasets[0].data) + 10,
          },
        },
      };

      if (chartInstance.current) {
        chartInstance.current.data = data;
        chartInstance.current.options = options;
        chartInstance.current.update();
      } else {
        chartInstance.current = new Chart(chartRef.current, {
          type: 'bar',
          data: data,
          options: options,
        });
      }
    }
  }, [chartData]);

  return (
    <div
      style={{
        width: '100%',
        height: '85%',
        justifyContent: 'center',
        alignItems: 'center',
        display: 'flex',
        padding: 0,
        margin: 0,
      }}
    >
      <canvas ref={chartRef} />
    </div>
  );
};

export default SalesChart;