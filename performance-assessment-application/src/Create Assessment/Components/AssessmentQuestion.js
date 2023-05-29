import React, { useState } from 'react';

import { Stack, IconButton, Box } from '@mui/material';
import AddBoxOutlinedIcon from '@mui/icons-material/AddBoxOutlined';

import AssessmentTitle from './AssessmentTitle';
import NewQuestion from './NewQuestion';

function AssessmentQuestion() {
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [componentCount, setComponentCount] = useState(1);
  const [components, setComponents] = useState([{ index: 0 }]);

  const handleAddComponent = () => {
    const newIndex = componentCount;
    setComponentCount(componentCount + 1);
    setComponents([...components, { index: newIndex }]);
  };

  const handleDeleteComponent = (indexToDelete) => {
    const updatedComponents = components.filter((component) => component.index !== indexToDelete);
    setComponents(updatedComponents);
  };

  const hasNoQuestions = components.length === 0;

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
    </Stack>
  );
}

export default AssessmentQuestion;