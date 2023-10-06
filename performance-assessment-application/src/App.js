import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import AssessmentQuestion from './Create Assessment/Components/AssessmentQuestion';
import Templates from './Create Assessment/Components/Templates';
import UserAssessment from './Answer Assessment/UserAssessment';
import AnswerAssessment from './Answer Assessment/AnswerAssessment';
import Login from './Login/Login.js';
import LandingPage from './Landing Page/LandingPage';
import InvitationPage from './Team Creation/InvitationPage';
import SuccessPage from './Team Creation/SuccessPage.js';
import TeamCreation from './Team Creation/TeamCreation';
import Home from './Homepage/Home';

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
          <Route path="/invitation/:data" element={<InvitationPage />} />
          <Route path="/success/:data" element={<SuccessPage />} />
          <Route path="/create-team" element={<TeamCreation />} />
          <Route path="/home" element={<Home />} />
        </Routes>
      </Router>
    </div>
  );
}

export default App;