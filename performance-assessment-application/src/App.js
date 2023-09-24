import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import AssessmentQuestion from './Create Assessment/Components/AssessmentQuestion';
import Templates from './Create Assessment/Components/Templates';

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
          <Route path="/assessments" element={<AssessmentQuestion />} />
          <Route path="/templates" element={<Templates />} />
        </Routes>
      </Router>
    </div>
  );
}

export default App;
