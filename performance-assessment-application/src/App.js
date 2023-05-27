import { Stack } from '@mui/material';
import './App.css';
import AssessmentTitle from './Create Assessment/Components/AssessmentTitle';
import AssessmentQuestion from './Create Assessment/Components/AssessmentQuestion';

function App() {
  return (
    <div
      style={{
        minHeight: "100vh", // Set the minimum height of the container to 100% of viewport height
        width: "100vw",
        backgroundColor: "#d6f4f8",
      }}
    >
      <Stack direction="column" spacing={2}>
        <AssessmentTitle />
        <AssessmentQuestion />
      </Stack>
    </div>
  );
}

export default App;
