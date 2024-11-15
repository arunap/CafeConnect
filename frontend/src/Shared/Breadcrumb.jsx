import React from "react";
import { Link as RouterLink, useLocation } from "react-router-dom";
import Breadcrumbs from "@mui/material/Breadcrumbs";
import Typography from "@mui/material/Typography";
import Link from "@mui/material/Link";
import { grey } from "@mui/material/colors";

const Breadcrumb = () => {
  const location = useLocation();

  // Split the pathnames
  const pathnames = location.pathname.split("/").filter((x) => x);

  return (
    <Breadcrumbs aria-label="breadcrumb" sx={{  p: 2, backgroundColor: grey[200] }}>
      <Link component={RouterLink} to="/" color="inherit">
        Home
      </Link>

      {pathnames.map((value, index) => {
        const to = `/${pathnames.slice(0, index + 1).join("/")}`;

        // Capitalize the breadcrumb text for readability
        const breadcrumbLabel = value.charAt(0).toUpperCase() + value.slice(1);

        return index === pathnames.length - 1 ? (
          <Typography color="text.primary" key={to}>
            {breadcrumbLabel}
          </Typography>
        ) : (
          <Link component={RouterLink} to={to} color="inherit" key={to}>
            {breadcrumbLabel}
          </Link>
        );
      })}
    </Breadcrumbs>
  );
};

export default Breadcrumb;
