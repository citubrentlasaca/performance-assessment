import React, { useState } from 'react';

import { Stack, IconButton, Box, Button } from '@mui/material';
import AddBoxOutlinedIcon from '@mui/icons-material/AddBoxOutlined';

import AssessmentTitle from './AssessmentTitle';
import NewQuestion from './NewQuestion';
import AssessmentDialog from './AssessmentDialog';

import axios from 'axios';
import NavBar from '../../Shared/NavBar';

function AssessmentQuestion() {
  const [title, setTitle] = useState('');
  const [tempTitle, setTempTitle] = useState('');
  const [description, setDescription] = useState('');
  const [componentCount, setComponentCount] = useState(1);
  const [components, setComponents] = useState([]);
  const [open, setOpen] = useState(false);
  const [dialogText, setDialogText] = useState('');

  const handleAddComponent = () => {
    const newIndex = componentCount;
    setComponentCount(componentCount + 1);
    if (components.length === 0) {
      setComponents([{ index: newIndex }]);
    } else {
      setComponents([...components, { index: newIndex }]);
    }
  };

  const handleDeleteComponent = (indexToDelete) => {
    const updatedComponents = components.filter((component) => component.index !== indexToDelete);
    setComponents(updatedComponents);
  };

  const hasNoQuestions = components.length === 0;

  const updateAssessment = () => {
    axios
      .get("https://localhost:7236/api/assessments")
      .then((response) => {
        const assessments = response.data;
        const assessment = assessments.find((item) => item.title === tempTitle);

        if (assessment) {
          const assessmentId = assessment.id;

          const updatedAssessment = {
            title: title,
            description: description,
          };

          axios
            .put(`https://localhost:7236/api/assessments/${assessmentId}`, updatedAssessment)
            .then(() => {
              console.log("Assessment updated successfully!");
            })
            .catch((error) => {
              console.error("Error updating assessment:", error);
            });
        } else {
          console.log("Assessment not found.");
        }
      })
      .catch((error) => {
        console.error("Error retrieving assessments:", error);
      });
  };

  const handlePublishOpen = () => {
    setOpen(true);
    setTempTitle(title);
    updateAssessment();
    setDialogText("ASSESSMENT PUBLISHED")
  };

  const handleClose = () => {
    setOpen(false);
  };

  const deleteAssessment = () => {
    axios
      .get("https://localhost:7236/api/assessments")
      .then((response) => {
        const assessments = response.data;
        const assessment = assessments.find((item) => item.title === tempTitle);

        if (assessment) {
          const assessmentId = assessment.id;

          axios
            .delete(`https://localhost:7236/api/assessments/${assessmentId}`)
            .then(() => {
              console.log("Assessment deleted successfully!");
            })
            .catch((error) => {
              console.error("Error deleting assessment:", error);
            });
        } else {
          console.log("Assessment not found.");
        }
      })
      .catch((error) => {
        console.error("Error retrieving assessments:", error);
      });
  };

  const handleDiscardOpen = () => {
    setOpen(true);
    setTempTitle(title);
    deleteAssessment();
    setDialogText("ASSESSMENT DISCARDED");
  };

  const handlePostAssessment = async () => {
    try {
      const assessmentData = {
        title: title,
        description: description,
      };
      const response = await axios.post('https://localhost:7236/api/assessments', assessmentData);
      setTempTitle(title);
      console.log('Assessment published successfully!', response.data);
    } catch (error) {
      console.error('Error publishing assessment:', error);
    }
  };

  return (
    <NavBar>
      <Stack direction="column" justifyContent="center" alignItems="center" spacing={2}>
        <AssessmentTitle title={title} description={description} setTitle={setTitle} setDescription={setDescription} />
        {components.map((component) => (
          <NewQuestion
            key={component.index}
            index={component.index}
            handleDeleteComponent={handleDeleteComponent}
            handleAddComponent={handleAddComponent}
            title={title}
            description={description}
          />
        ))}
        {hasNoQuestions && (
          <IconButton onClick={() => { handleAddComponent(); handlePostAssessment(); }}>
            <Box
              sx={{
                width: "50px",
                height: "50px",
                backgroundColor: "white",
                display: "flex",
                justifyContent: "center",
                alignItems: "center",
                borderRadius: "10px"
              }}
            >
              <AddBoxOutlinedIcon />
            </Box>
          </IconButton>
        )}
        <Stack direction="row" justifyContent="center" alignItems="center" spacing={2}
          style={{
            marginBottom: "16px"
          }}
        >
          <Button variant="contained" onClick={handlePublishOpen}
            sx={{
              backgroundColor: "white",
              color: "black",
              fontFamily: "Montserrat Regular",
              '&:hover': {
                backgroundColor: 'green',
                color: 'white',
                fontFamily: "Montserrat Regular",
              },
            }}
          >
            Publish Assessment
          </Button>
          <Button variant="contained" onClick={handleDiscardOpen}
            sx={{
              backgroundColor: "white",
              color: "black",
              fontFamily: "Montserrat Regular",
              '&:hover': {
                backgroundColor: 'red',
                color: 'white',
                fontFamily: "Montserrat Regular",
              },
            }}
          >
            Discard Assessment
          </Button>
          <AssessmentDialog open={open} handleClose={handleClose} dialogText={dialogText} />
        </Stack>
      </Stack>
    </NavBar>
  );
}

export default AssessmentQuestion;