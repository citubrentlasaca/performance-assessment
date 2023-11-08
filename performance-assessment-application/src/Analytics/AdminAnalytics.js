import React from 'react';
import NavBar from '../Shared/NavBar';
import TopBarThree from '../Shared/TopBarThree';
import './AdminAnalytics.css';
import analyticsheader from './analytics-trophy.png';

const AdminAnalytics = () => {
    const employeeData = Array.from({ length: 10 }, (_, index) => ({
        rank: index + 1,
        name: 'Name',
        points: 100,
    }));

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
                            <div className="employee-rank">{employee.rank}</div>
                            <div className="employee-analytics-name">{employee.name}</div>
                            <div className="employee-analytics-points">{employee.points} Points</div>
                        </div>
                    </div>
                ))}
            </div>
        </NavBar>
    );
};

export default AdminAnalytics;