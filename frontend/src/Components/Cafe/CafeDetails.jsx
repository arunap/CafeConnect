import React from "react";
import { AgGridReact } from "ag-grid-react";
import { Grid, Typography, Card, CardMedia, CardContent, Box, Avatar } from "@mui/material";

import "ag-grid-community/styles/ag-grid.css";
import "ag-grid-community/styles/ag-theme-alpine.css";

import { useGetCafeByCafeIdQuery } from "../../Services/queries.cafe";
import { useGetEmployeesQuery } from "../../Services/queries.employee";
import { useLocation, useParams } from "react-router-dom";
import { BASE_IMAGE_URL } from "../../Constants";
import CafeLogoPlaceHolder from "../../Images/cafe_logo_placeholder.png";

const CafeDetails = () => {
  const location = useLocation();

  // Helper function to parse query parameters
  const queryParams = new URLSearchParams(location.search);
  const cafeId = queryParams.get("cafeId");
  const cafe = queryParams.get("cafe");

  const { data: cafeItem } = useGetCafeByCafeIdQuery(cafeId, true);
  const { data: employeeData, error, isLoading } = useGetEmployeesQuery(cafe);

  const columnDefs = [
    {
      headerName: "Photo",
      field: "photo",
      autoHeight: true,
      cellRenderer: (params) => (
        <Avatar
          alt={params.value}
          sx={{
            bgcolor: "#f0f0f0",
            width: 40,
            height: 40,
            borderRadius: "50%",
            margin: "5px",
          }}
        />
      ),
    },
    { headerName: "Employee ID", field: "employeeId", sortable: true, filter: true, flex: 1 },
    { headerName: "Name", field: "name", sortable: true, filter: true, flex: 1 },
    { headerName: "Email", field: "emailAddress", sortable: true, filter: true, flex: 1 },
    { headerName: "Phone Number", field: "phoneNumber", sortable: true, filter: true, flex: 1 },
    {
      headerName: "Start Date",
      field: "startedAt",
      sortable: true,
      filter: true,
      flex: 1,
      valueFormatter: (params) => {
        const date = new Date(params.value);
        return date.toLocaleDateString("en-US", { year: "numeric", month: "short", day: "numeric" });
      },
    },
    { headerName: "Days Worked", field: "daysWorked", sortable: true, filter: true, flex: 1 },
    { headerName: "Café Name", field: "cafeName", sortable: true, filter: true, flex: 1 },
  ];

  const defaultImageUrl = "cafe_logo_placeholder.png";
  return (
    <Box sx={{ padding: 2 }}>
      <Grid container spacing={2}>
        {/* Left Side: Cafe details */}
        <Grid item xs={12} md={2}>
          <Card>
            <CardMedia
              component="img"
              sx={{ objectFit: "contain" }}
              image={`${BASE_IMAGE_URL}/${cafeItem?.logoPath}`}
              alt={cafeItem?.name}
              onError={(e) => {
                e.target.onerror = null; // Prevents infinite loop in case default also fails
                e.target.src = CafeLogoPlaceHolder;
              }}
            />
            <CardContent>
              <Typography variant="h5">{cafeItem?.name}</Typography>
              <Typography variant="subtitle2">{cafeItem?.description}</Typography>
              <Typography variant="subtitle1">{cafeItem?.location}</Typography>
            </CardContent>
          </Card>
        </Grid>

        {/* Right Side: Employee details */}
        <Grid item xs={12} md={10}>
          {isLoading ? (
            <Typography>Loading...</Typography>
          ) : error ? (
            <Typography color="error">Failed to load employees.</Typography>
          ) : (
            <div className="ag-grid-wrapper ag-theme-alpine" style={{ height: "100%", width: "100%" }}>
              <AgGridReact rowData={employeeData} columnDefs={columnDefs} pagination={true} paginationPageSize={20} />
            </div>
          )}
        </Grid>
      </Grid>
    </Box>
  );
};

export default CafeDetails;
