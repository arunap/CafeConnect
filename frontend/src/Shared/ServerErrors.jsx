import React from "react";
import { Box, Typography, Button, Container } from "@mui/material";
import ErrorOutlineIcon from "@mui/icons-material/ErrorOutline";
import { useLocation, useNavigate } from "react-router-dom";

const ServerErrors = () => {
  const navigate = useNavigate();
  const location = useLocation();

  const queryParams = new URLSearchParams(location.search);
  const detailed = queryParams.get("detailed");

  return (
    <Container maxWidth="sm" sx={{ textAlign: "center", mt: 5 }}>
      <Box display="flex" flexDirection="column" alignItems="center">
        <ErrorOutlineIcon color="error" sx={{ fontSize: 80 }} />
        <Typography variant="h4" color="error" gutterBottom>
          Oops! Something went wrong.
        </Typography>
        <Typography variant="body1" color="textSecondary" sx={{ mb: 3 }}>
          {detailed || "An unexpected error has occurred. Please try again later."}
        </Typography>
        <Button variant="contained" color="primary" onClick={() => navigate("/cafes")}>
          GO TO HOME
        </Button>
      </Box>
    </Container>
  );
};

export default ServerErrors;
