import React from "react";
import NavBar from "./NavBar";
import { Box } from "@mui/material";
import { Outlet } from "react-router-dom";
import NotificationBanner from "./NotificationBanner";
import Breadcrumb from "./Breadcrumb";

const Layout = () => {
  return (
    <div style={{ height: "100vh" }}>
      <NavBar />
      <Breadcrumb />
      <NotificationBanner />
      <Box sx={{ p: 2, my: 0, mx: 0 }}>
        <Outlet />
      </Box>
    </div>
  );
};

export default Layout;
