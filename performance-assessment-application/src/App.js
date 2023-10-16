import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Templates from './Create Assessment/Templates';
import UserAssessment from './Answer Assessment/UserAssessment';
import AnswerAssessment from './Answer Assessment/AnswerAssessment';
import UserRegistration from './User Registration/UserRegistration';
import Login from './Login/Login.js';
import EmployeeAnalytics from './Analytics/EmployeeAnalytics';
import LandingPage from './Landing Page/LandingPage';
import InvitationPage from './Team Creation/InvitationPage';
import SuccessPage from './Team Creation/SuccessPage.js';
import TeamCreation from './Team Creation/TeamCreation';
import Home from './Homepage/Home';
import Organizations from './OrganizationsPage/Organizations';
import Performance from './PerformancePage/Performance';
import CreateAssessment from './Create Assessment/CreateAssessment';
import UpdateAssessment from './Create Assessment/UpdateAssessment';
import OrganizationDetails from './OrganizationsPage/OrganizationDetails';
import Employees from './Employees/Employees';

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
          <Route index element={<LandingPage />} />
          <Route path="createassessment" element={<CreateAssessment />} />
          <Route path="/adminassessments/:id" element={<UpdateAssessment />} />
          <Route path="/organizations/adminassessments" element={<Templates />} />
          <Route path="/answerassessment/:id" element={<AnswerAssessment />} />
          <Route path="/login" element={<Login />} />
          <Route path="/success" element={<SuccessPage />} />
          <Route path="/teamcreation" element={<TeamCreation />} />
          <Route path="/register" element={<UserRegistration />} />
          <Route path="/employeeanalytics" element={<EmployeeAnalytics />} />
          <Route path="/join-team" element={<InvitationPage />} />
          <Route path="/success/:data" element={<SuccessPage />} />
          <Route path="/create-team" element={<TeamCreation />} />
          <Route path="/home" element={<Home />} />
          <Route path="/organizations" element={<Organizations />} />
          <Route path="/organizations/performance" element={<Performance />} />
          <Route path="/organizations/userassessments" element={<UserAssessment />} />
          <Route path="/organizations/:teamId" element={<OrganizationDetails />} />
          <Route path="/organizations/employees" element={<Employees />} />
        </Routes>
      </Router>
    </div>
  );
}

export default App;