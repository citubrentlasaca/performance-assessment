import { Stack } from '@mui/material';
import './App.css';
import AssessmentTitle from './Create Assessment/Components/AssessmentTitle';
import AssessmentQuestion from './Create Assessment/Components/AssessmentQuestion';

function App() {
  return (
    <div
      style={{
        minHeight: "100vh",
        width: "100vw",
        backgroundColor: "#d6f4f8",
      }}
    >
      <Stack direction="column" justifyContent="center" alignItems="center" spacing={2}>
        <AssessmentQuestion/>
      </Stack>
    </div>
  );
}

export default App;
