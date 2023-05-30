import React from 'react'
import { Dialog, DialogContent, DialogContentText, Typography } from '@mui/material';

function AssessmentDialog({ open, handleClose, dialogText }) {
  return (
    <Dialog
        open={open}
        onClose={handleClose}
    >
        <DialogContent>
        <DialogContentText>
            <Typography variant="body1" fontFamily="Montserrat Regular">
                {dialogText}
            </Typography>
        </DialogContentText>
        </DialogContent>
    </Dialog>
  )
}

export default AssessmentDialog