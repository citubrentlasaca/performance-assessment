import React, { useState } from 'react';

import { Stack, Button } from '@mui/material';

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

  return (
    <Stack direction="column" justifyContent="center" alignItems="center" spacing={2}>
      <AssessmentTitle title={title} description={description} setTitle={setTitle} setDescription={setDescription}/>
      {components.map((component) => (
        <NewQuestion key={component.index} index={component.index} handleDeleteComponent={handleDeleteComponent} title={title} description={description}/>
      ))}
      <Button variant="contained" onClick={handleAddComponent}
        sx={{
          backgroundColor: "white",
          color: "black",
          fontFamily: "Montserrat Regular",
          "&:hover": {
            backgroundColor: "white",
            color: "black",
            fontFamily: "Montserrat Regular",
          },
        }}
      >
        Add new question
      </Button>
    </Stack>
  );
}

export default AssessmentQuestion;
