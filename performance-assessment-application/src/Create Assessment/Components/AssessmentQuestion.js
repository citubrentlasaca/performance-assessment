import React, { useState } from 'react';

import { Stack, IconButton, Box, Button } from '@mui/material';
import AddBoxOutlinedIcon from '@mui/icons-material/AddBoxOutlined';

import AssessmentTitle from './AssessmentTitle';
import NewQuestion from './NewQuestion';
import AssessmentDialog from './AssessmentDialog';

import { getFirestore, collection, getDocs, deleteDoc } from 'firebase/firestore';
import { app } from '../../firebase';

import axios from 'axios';

function AssessmentQuestion() {
  const [title, setTitle] = useState('');
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
    handlePostAssessment();
  };  

  const handleDeleteComponent = (indexToDelete) => {
    const updatedComponents = components.filter((component) => component.index !== indexToDelete);
    setComponents(updatedComponents);
  };

  const hasNoQuestions = components.length === 0;

  const handlePublishOpen = () => {
    setOpen(true);
    setDialogText("ASSESSMENT PUBLISHED")
  };

  const handleClose = () => {
    setOpen(false);
  };

  const handleDiscardOpen = async () => {
    setOpen(true);
    setDialogText("ASSESSMENT DISCARDED");
  
    try {
      const db = getFirestore(app);
      const assessmentCollectionRef = collection(db, title);
      const snapshot = await getDocs(assessmentCollectionRef);
  
      snapshot.forEach((doc) => {
        deleteDoc(doc.ref);
      });
  
      console.log('Collection deleted successfully!');
    } catch (error) {
      console.error('Error deleting collection:', error);
    }
  };

  const handlePostAssessment = async () => {
    try {
      const assessmentData = {
        title: title,
        description: description,
      };
      const response = await axios.post('https://localhost:7236/api/assessments', assessmentData);
      console.log('Assessment published successfully!', response.data);
    } catch (error) {
      console.error('Error publishing assessment:', error);
    }
  };
  

  return (
    <Stack direction="column" justifyContent="center" alignItems="center" spacing={2}>
      <AssessmentTitle title={title} description={description} setTitle={setTitle} setDescription={setDescription}/>
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
        <IconButton onClick={handleAddComponent}>
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
              <AddBoxOutlinedIcon/>
          </Box>
        </IconButton>
      )}
      <Stack direction="row" justifyContent="center" alignItems="center" spacing={2}>
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
            <AssessmentDialog open={open} handleClose={handleClose} dialogText={dialogText}/>
      </Stack>
    </Stack>
  );
}

export default AssessmentQuestion;