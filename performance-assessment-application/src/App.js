import { Stack } from '@mui/material';
import './App.css';
import AssessmentTitle from './Create Assessment/Components/AssessmentTitle';

function App() {
  return (
    <div
      style={{
        width: "100vw",
        height: "100vh",
        backgroundColor: "#d6f4f8"
      }}
    >
      <Stack direction="column" justifyContent="center" alignItems="center">
        <AssessmentTitle/>
      </Stack>
    </div>
  );
}

export default App;
