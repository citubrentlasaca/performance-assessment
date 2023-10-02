import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import AssessmentQuestion from './Create Assessment/Components/AssessmentQuestion';
import Templates from './Create Assessment/Components/Templates';
import UserAssessment from './Answer Assessment/UserAssessment';
import AnswerAssessment from './Answer Assessment/AnswerAssessment';
import SelfAssessmentTemplate from './Create Assessment/Components/Self Assessment/SelfAssessmentTemplate';
import Login from './Login/Login.js';

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
          <Route path="/createassessment" element={<AssessmentQuestion />} />
          <Route path="/adminassessments" element={<Templates />} />
          <Route path="/userassessments" element={<UserAssessment />} />
          <Route path="/answerassessment/:id" element={<AnswerAssessment />} />
          <Route path="/selfassessment" element={<SelfAssessmentTemplate />} />
          <Route path="/login" element={<Login />} />
        </Routes>
      </Router>
    </div>
  );
}

export default App;