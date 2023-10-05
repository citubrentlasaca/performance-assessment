import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import AssessmentQuestion from './Create Assessment/Components/AssessmentQuestion';
import Templates from './Create Assessment/Components/Templates';
import UserAssessment from './Answer Assessment/UserAssessment';
import AnswerAssessment from './Answer Assessment/AnswerAssessment';
import UserRegistration from './User Registration/UserRegistration';
import Login from './Login/Login.js';
import EmployeeAnalytics from './Analytics/EmployeeAnalytics';
import LandingPage from './Landing Page/LandingPage';

import SuccessPage from './Team Creation/SuccessPage.js';
import TeamCreation from './Team Creation/TeamCreation';

function App() {
  return (
    <div
      style={{
        height: "100vh",
        width: "100vw",
        minHeight: "100vh",
        backgroundColor: "#d6f4f8",
      }}
    >
      <Router>
        <Routes>
          <Route path="/" element={<LandingPage />} />
          <Route path="/createassessment" element={<AssessmentQuestion />} />
          <Route path="/adminassessments" element={<Templates />} />
          <Route path="/userassessments" element={<UserAssessment />} />
          <Route path="/answerassessment/:id" element={<AnswerAssessment />} />
          <Route path="/login" element={<Login />} />
          <Route path="/success" element={<SuccessPage />} />
          <Route path="/teamcreation" element={<TeamCreation />} />
          <Route path="/register" element={<UserRegistration />} />
          <Route path="/employeeanalytics" element={<EmployeeAnalytics />} />
        </Routes>
      </Router>
    </div>
  );
}

export default App;