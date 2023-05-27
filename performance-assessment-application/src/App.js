import { Stack } from '@mui/material';
import './App.css';
import AssessmentTitle from './Create Assessment/Components/AssessmentTitle';

function App() {
  return (
    <div
      style={{
        minHeight: "100vh",
        width: "100vw",
        backgroundColor: "#d6f4f8",
      }}
    >
      <Stack direction="column" spacing={2}>
        <AssessmentTitle />
      </Stack>
    </div>
  );
}

export default App;
