import React from "react";
import { Fab, Box } from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import { lime } from "@mui/material/colors";

const CustomAddButton = ({ onAddClickEvent }) => {
  return (
    <Box sx={{ position: "fixed", bottom: 16, right: 16, zIndex: 1000 }}>
      <Fab sx={{ backgroundColor: lime[900], "&:hover": { backgroundColor: lime[500] } }} aria-label="add">
        <AddIcon onClick={() => onAddClickEvent()} />
      </Fab>
    </Box>
  );
};

export default CustomAddButton;
