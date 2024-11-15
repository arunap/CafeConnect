import React from "react";
import AppBar from "@mui/material/AppBar";
import Toolbar from "@mui/material/Toolbar";
import MenuItem from "@mui/material/MenuItem";
import Box from "@mui/material/Box";
import { blue } from '@mui/material/colors';

import { useNavigate } from "react-router-dom";

const NavBar = () => {
  const navigate = useNavigate();

  return (
    <AppBar position="static">
      <Toolbar style={{ backgroundColor: blue[900] }}>
        <Box component="img" src="/logo.png" alt="Logo" sx={{ marginRight: 2, width: 40 }} />

        <Box sx={{ display: "flex", flexGrow: 1 }}>
          <MenuItem onClick={() => navigate("cafes")}>Cafes</MenuItem>
          <MenuItem onClick={() => navigate("employees")}>Employees</MenuItem>
        </Box>
      </Toolbar>
    </AppBar>
  );
};

export default NavBar;
