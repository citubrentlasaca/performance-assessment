import React, { useState } from 'react';

import {
  Box,
  IconButton,
  Stack,
  TextField,
  FormControl,
  Select,
  MenuItem,
  Typography,
  Switch
} from '@mui/material';
import EditIcon from '@mui/icons-material/Edit';
import LockOpenIcon from '@mui/icons-material/LockOpen';
import AddBoxOutlinedIcon from '@mui/icons-material/AddBoxOutlined';
import DeleteOutlineOutlinedIcon from '@mui/icons-material/DeleteOutlineOutlined';
import ImageOutlinedIcon from '@mui/icons-material/ImageOutlined';

import { getFirestore, collection, setDoc, getDoc, doc, updateDoc, deleteDoc, deleteField } from 'firebase/firestore';
import { getStorage, ref, uploadBytes, getDownloadURL } from 'firebase/storage';
import { app } from '../../firebase';

import Paragraph from './Paragraph';
import ShortAnswer from './ShortAnswer';
import Checkboxes from './Checkboxes';
import MultipleChoice from './MultipleChoice';

function NewQuestion({ index, title, description, handleDeleteComponent, handleAddComponent }) {
    const [question, setQuestion] = useState('');
    const [type, setType] = useState('Multiple choice');
    const [choices, setChoices] = useState([]);
    const [checkboxChoices, setCheckboxChoices] = useState([]);
    const [weight, setWeight] = useState(0);
    const [isRequired, setIsRequired] = useState(false);
    const [paragraphAnswer, setParagraphAnswer] = useState('');
    const [temporaryQuestion, setTemporaryQuestion] = useState('');
    const [isDisabled, setIsDisabled] = useState(false);
    const [uploadedImageData, setUploadedImageData] = useState({}); 

    const handleImageUpload = (event) => {
      const file = event.target.files[0];
      const reader = new FileReader();
  
      reader.onloadend = () => {
        const imageData = reader.result;
  
        setUploadedImageData((prevData) => ({
          ...prevData,
          [index]: imageData,
        }));
      };
  
      if (file) {
        reader.readAsDataURL(file);
      }
    };
    
    const handleQuestionChange = (event) => {
      setQuestion(event.target.value);
    };
  
    const handleTypeChange = (event) => {
      setType(event.target.value);
    };
  
    const handleWeightChange = (event) => {
      setWeight(event.target.value);
    };
  
    const handleIsRequiredChange = (event) => {
      setIsRequired(event.target.checked);
    };
  
    const handleParagraphAnswerChange = (event) => {
      setParagraphAnswer(event.target.value);
    };
  
    const handleAddClick = () => {
      setIsDisabled(true);
      setTemporaryQuestion(question);
      handleAddComponent();
    };
  
    const handleUnlockClick = () => {
      setIsDisabled(false);
      setTemporaryQuestion(question);
    };
  
    const addAssessment = () => {
      const db = getFirestore(app);
      const assessmentCollectionRef = collection(db, title);
    
      const uploadImage = async () => {
        const response = await fetch(uploadedImageData[index]);
        const blob = await response.blob();
    
        const storage = getStorage(app);
        const storageRef = ref(storage, `images/${question}-${Date.now()}`);
    
        const snapshot = await uploadBytes(storageRef, blob);
    
        const downloadURL = await getDownloadURL(storageRef);
    
        let assessmentData = {
          assessmentDescription: description,
          question: question,
          type: type,
          weight: weight,
          isRequired: isRequired
        };
    
        if (downloadURL) {
          assessmentData.imageUrl = downloadURL;
        }
    
        if (type === "Multiple choice") {
          assessmentData.choice = choices;
        } else if (type === "Checkboxes") {
          assessmentData.checkboxChoices = checkboxChoices;
        }
    
        setDoc(doc(assessmentCollectionRef, question), assessmentData)
          .then(() => {
            handleAddClick();
            console.log('Document written with ID: ', question);
          })
          .catch((error) => {
            console.error('Error adding document: ', error);
          });
      };
    
      if (uploadedImageData[index]) {
        uploadImage();
      } else {

        let assessmentData = {
          assessmentDescription: description,
          question: question,
          type: type,
          weight: weight,
          isRequired: isRequired
        };
    
        if (type === "Multiple choice") {
          assessmentData.choice = choices;
        } else if (type === "Checkboxes") {
          assessmentData.checkboxChoices = checkboxChoices;
        }
    
        setDoc(doc(assessmentCollectionRef, question), assessmentData)
          .then(() => {
            handleAddClick();
            console.log('Document written with ID: ', question);
          })
          .catch((error) => {
            console.error('Error adding document: ', error);
          });
      }
    };    
  
    const updateAssessment = () => {
      const db = getFirestore(app);
      const assessmentCollectionRef = collection(db, title);
      const documentId = temporaryQuestion;
      let updatedFields = {};
    
      const uploadImage = async () => {
        const response = await fetch(uploadedImageData);
        const blob = await response.blob();
    
        const storage = getStorage(app);
        const storageRef = ref(storage, `images/${question}-${Date.now()}`);
    
        const snapshot = await uploadBytes(storageRef, blob);
    
        const downloadURL = await getDownloadURL(storageRef);
    
        updatedFields.imageUrl = downloadURL;
    
        updateDocument();
      };
    
      const updateDocument = () => {
        const documentRef = doc(assessmentCollectionRef, documentId);
    
        getDoc(documentRef)
          .then((docSnap) => {
            if (docSnap.exists()) {
              const existingData = docSnap.data();
    
              if (type === "Multiple choice") {
                updatedFields = {
                  ...updatedFields,
                  assessmentDescription: description,
                  question: question,
                  type: type,
                  choice: choices,
                  weight: weight,
                  isRequired: isRequired
                };
              } else if (type === "Short answer" || type === "Paragraph") {
                updatedFields = {
                  ...updatedFields,
                  assessmentDescription: description,
                  question: question,
                  type: type,
                  weight: weight,
                  isRequired: isRequired
                };
              } else if (type === "Checkboxes") {
                updatedFields = {
                  ...updatedFields,
                  assessmentDescription: description,
                  question: question,
                  type: type,
                  checkboxChoices: checkboxChoices,
                  weight: weight,
                  isRequired: isRequired
                };
              }
    
              for (const field in existingData) {
                if (!(field in updatedFields)) {
                  updatedFields[field] = deleteField();
                }
              }
    
              updateDoc(documentRef, updatedFields)
                .then(() => {
                  setIsDisabled(true);
                  console.log('Document successfully updated!');
                })
                .catch((error) => {
                  console.error('Error updating document:', error);
                });
            } else {
              console.error('Document does not exist.');
            }
          })
          .catch((error) => {
            console.error('Error getting document:', error);
          });
      };
    
      if (uploadedImageData) {
        uploadImage();
      } else {
        updateDocument();
      }
    };    
      
  
    const deleteDocument = () => {
      const db = getFirestore(app);
      
      if (!question) {
        handleDeleteComponent(index);
        return;
      }

      const assessmentCollectionRef = collection(db, title);
      const documentRef = doc(assessmentCollectionRef, question);
    
      deleteDoc(documentRef)
        .then(() => {
          handleDeleteComponent(index);
          console.log('Document successfully deleted!');
        })
        .catch((error) => {
          console.error('Error deleting document:', error);
        });
    };    

  return (
    <Stack direction="row" justifyContent="center" alignItems="center" spacing={2}>
        <Stack direction="column" justifyContent="center" alignItems="flex-start" spacing={2}>
          <Box
            sx={{
              width: '750px',
              height: 'auto',
              backgroundColor: 'white',
              borderRadius: '10px',
              padding: '20px',
              opacity: isDisabled ? 0.75 : 1,
              pointerEvents: isDisabled ? 'none' : 'auto',
              marginLeft: "65px"
            }}
          >
            <Stack direction="row" justifyContent="flex-start" alignItems="flex-start" spacing={2}>
              <TextField
                multiline
                label="Question"
                variant="filled"
                value={question}
                onChange={handleQuestionChange}
                sx={{
                  width: '100%'
                }}
              />
              <input type="file" onChange={handleImageUpload} accept="image/*" style={{ display: 'none' }} id={`upload-input-${index}`} />
              <label htmlFor={`upload-input-${index}`}>
                <IconButton component="span">
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
                    <ImageOutlinedIcon />
                  </Box>
                </IconButton>
              </label>
              <FormControl
                sx={{
                  width: '270px'
                }}
              >
                <Select value={type} onChange={handleTypeChange}
                  sx={{
                    fontFamily: "Montserrat Regular"
                  }}
                >
                  <MenuItem value={'Short answer'}>Short answer</MenuItem>
                  <MenuItem value={'Paragraph'}>Paragraph</MenuItem>
                  <MenuItem value={'Multiple choice'}>Multiple choice</MenuItem>
                  <MenuItem value={'Checkboxes'}>Checkboxes</MenuItem>
                </Select>
              </FormControl>
            </Stack>
            <Stack justifyContent="center" alignItems="center">
              {uploadedImageData[index] && (
                <Box
                  sx={{
                    width: "500px",
                    height: "500px",
                    backgroundColor: "white",
                    display: "flex",
                    justifyContent: "center",
                    alignItems: "center",
                    borderRadius: "10px",
                    backgroundImage: `url(${uploadedImageData[index]})`,
                    backgroundSize: "cover",
                    backgroundPosition: "center",
                    marginTop: "10px"
                  }}
                />
              )}
            </Stack>
            {type === 'Multiple choice' && <MultipleChoice choices={choices} setChoices={setChoices} />}
            {type === 'Checkboxes' && (
              <Checkboxes checkboxChoices={checkboxChoices} setCheckboxChoices={setCheckboxChoices} />
            )}
            {type === 'Paragraph' && (
              <Paragraph
                value={paragraphAnswer}
                onChange={handleParagraphAnswerChange}
                label="Enter your long answer"
              />
            )}
            {type === 'Short answer' && ( <ShortAnswer label="Short answer"/> )}
            <hr
              style={{
                width: '100%',
                height: '1px',
                backgroundColor: 'black',
                margin: '20px 0 20px 0'
              }}
            />
            <Stack direction="row" justifyContent="flex-start" alignItems="center">
              <Typography variant="body1" fontFamily="Montserrat Regular" marginRight="10px">
                Weight value (0-100%):
              </Typography>
              <TextField
                variant="outlined"
                size="small"
                onChange={handleWeightChange}
                sx={{
                  width: '150px'
                }}
              />
              <IconButton onClick={deleteDocument}
                sx={{
                  marginLeft: '200px',
                  marginRight: '20px'
                }}
              >
                <DeleteOutlineOutlinedIcon />
              </IconButton>
              <Typography variant="body1" fontFamily="Montserrat Regular">
                Required
              </Typography>
              <Switch checked={isRequired} onChange={handleIsRequiredChange} />
            </Stack>
          </Box>
        </Stack>
      <Box
        sx={{
          width: '50px',
          height: 'auto',
          backgroundColor: 'white',
          borderRadius: '10px',
          display: 'flex',
          justifyContent: 'center',
          alignItems: 'center'
        }}
      >
        <Stack direction="column" justifyContent="center" alignItems="center">
          <IconButton onClick={addAssessment}>
            <AddBoxOutlinedIcon />
          </IconButton>
          <IconButton onClick={handleUnlockClick}>
            <LockOpenIcon/>
          </IconButton>
          <IconButton onClick={updateAssessment}>
            <EditIcon />
          </IconButton>
        </Stack>
      </Box>
    </Stack>
  )
}

export default NewQuestion