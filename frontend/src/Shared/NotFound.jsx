import React from "react";
import { Box, Typography, Button } from "@mui/material";

const NotFound = () => {
  return (
    <Box display="flex" flexDirection="column" justifyContent="center" alignItems="center" textAlign="center" p={5}>
      <Typography variant="h1" component="h1" gutterBottom>
        404
      </Typography>
      <Typography variant="h4" component="h2" gutterBottom>
        Page Not Found
      </Typography>
      <Typography variant="body1" gutterBottom>
        Sorry, the page you are looking for does not exist.
      </Typography>
      <Button variant="contained" color="primary" sx={{ mt: 2 }} href="/">
        Go to Home
      </Button>
    </Box>
  );
};

export default NotFound;
