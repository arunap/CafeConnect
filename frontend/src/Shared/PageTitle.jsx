import React from "react";
import Typography from "@mui/material/Typography";
import Box from "@mui/material/Box";
import { Divider } from "@mui/material";

const PageTitle = ({ title }) => {
  return (
    <Box sx={{ mb: 3, mt: 0 }}>
      <Typography variant="h7" component="h7" gutterBottom>
        {title}
      </Typography>
      <Divider />
    </Box>
  );
};

export default PageTitle;
