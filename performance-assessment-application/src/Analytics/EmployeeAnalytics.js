import React from 'react';
import './EmployeeAnalytics.css';
import sample from './sample.png';
import NavBar from '../Shared/NavBar';
import TopBarThree from '../Shared/TopBarThree';

const analyticsData = [
  { id: 1, imageUrl: sample, title: 'Sales 1' },
  { id: 2, imageUrl: sample, title: 'Sales 2' },
  { id: 3, imageUrl: sample, title: 'Sales 3' },
  { id: 4, imageUrl: sample, title: 'Sales 4' },
  { id: 5, imageUrl: sample, title: 'Sales 5' },
  { id: 6, imageUrl: sample, title: 'Sales 6' },

];
// Sample data for sales

const EmployeeAnalytics = () => {
  return (
    <NavBar>
      <TopBarThree />
      <div className="emp-analytics-container">
        {analyticsData.map((item) => (
          <div key={item.id} className="emp-analytics-item">
            <img src={item.imageUrl} alt={item.title} />
            <h4>{item.title}</h4>
            <h6>Click to view full details</h6>
          </div>
        ))}
      </div>
    </NavBar>
  );
};

export default EmployeeAnalytics;