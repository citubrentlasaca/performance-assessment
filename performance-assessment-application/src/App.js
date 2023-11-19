import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import Templates from './Create Assessment/Templates';
import UserAssessment from './Answer Assessment/UserAssessment';
import AnswerAssessment from './Answer Assessment/AnswerAssessment';
import UserRegistration from './User Registration/UserRegistration';
import Login from './Login/Login.js';
import EmployeeAnalytics from './Analytics/EmployeeAnalytics';
import AdminAnalytics from './Analytics/AdminAnalytics';
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
import CreateReport from './Create Assessment/CreateReport';
import Notifications from './Notifications/Notifications';
import ViewAnswers from './View Answers/ViewAnswers';
import AccountManagement from './Account Management/AccountManagement.js';

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
          <Route path="/createassessment" element={<CreateAssessment />} />
          <Route path="/createreport" element={<CreateReport />} />
          <Route path="/adminassessments/:id" element={<UpdateAssessment />} />
          <Route path="/organizations/:teamId/admin-assessments" element={<Templates />} />
          <Route path="/organizations/:teamId/admin-assessments/:assessmentId" element={<ViewAnswers />} />
          <Route path="/answerassessment/:id" element={<AnswerAssessment />} />
          <Route path="/login" element={<Login />} />
          <Route path="/success" element={<SuccessPage />} />
          <Route path="/teamcreation" element={<TeamCreation />} />
          <Route path="/register" element={<UserRegistration />} />
          <Route path="/organizations/:teamId/employee-analytics" element={<EmployeeAnalytics />} />
          <Route path="/organizations/:teamId/admin-analytics" element={<AdminAnalytics />} />
          <Route path="/join-team" element={<InvitationPage />} />
          <Route path="/success/:data" element={<SuccessPage />} />
          <Route path="/create-team" element={<TeamCreation />} />
          <Route path="/organizations" element={<PrivateRoute redirectTo="/login" component={Organizations} />} />
          <Route path="/organizations/:teamId/performance" element={<Performance />} />
          <Route path="/organizations/:teamId/employee-assessments" element={<UserAssessment />} />
          <Route path="/organizations/:teamId/announcements" element={<OrganizationDetails />} />
          <Route path="/organizations/:teamId/employees" element={<Employees />} />
          <Route path="/home" element={<PrivateRoute redirectTo="/login" component={Home} />} />
          <Route path='/notifications/:id' element={<Notifications />} />
          <Route path='/account/:userId' element={<AccountManagement />} />
        </Routes>
      </Router>
    </div>
  );
}

export default App;

const PrivateRoute = ({ component: Component, redirectTo }) => {
  const token = localStorage.getItem('token');
  return token ? <Component /> : <Navigate to={redirectTo} />;
};