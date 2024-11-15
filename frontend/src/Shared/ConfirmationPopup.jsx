import React from "react";
import { Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, Button } from "@mui/material";

const ConfirmationPopup = ({ open, handleClose, handleConfirmed, entity, entityId, entityType }) => {
  const handleDelete = () => {
    handleConfirmed();
    handleClose();
  };

  return (
    <Dialog open={open} onClose={handleClose} aria-labelledby="delete-confirmation-title" aria-describedby="delete-confirmation-description">
      <DialogTitle id="delete-confirmation-title">Delete {entityType.charAt(0).toUpperCase() + entityType.slice(1)}</DialogTitle>
      <DialogContent>
        <DialogContentText id="delete-confirmation-description">Are you sure you want to delete this {entityType}? This action cannot be undone.</DialogContentText>
      </DialogContent>
      <DialogActions>
        <Button onClick={handleClose} color="primary">
          Cancel
        </Button>
        <Button onClick={handleDelete} color="secondary" variant="contained">
          Delete
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default ConfirmationPopup;
