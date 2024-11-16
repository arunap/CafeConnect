import React, { useEffect, useState } from "react";
import { useQueryClient } from "@tanstack/react-query";
import { Box, Button, Typography, TextField, IconButton, Avatar } from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import PersonSearchIcon from "@mui/icons-material/PersonSearch";
import { AgGridReact } from "ag-grid-react";

import "ag-grid-community/styles/ag-grid.css";
import "ag-grid-community/styles/ag-theme-alpine.css";

import ConfirmationPopup from "../../Shared/ConfirmationPopup";
import { useGetCafesByLocationQuery } from "../../Services/queries.cafe";
import { useDeleteCafeFn } from "../../Services/mutations.cafe";
import { useNavigate } from "react-router-dom";

import { BASE_IMAGE_URL } from "../../Constants";

const CafeList = () => {
  const navigate = useNavigate();

  // set location filter
  const [locationFilter, setLocationFilter] = React.useState("");
  const [cafeLocation, setCafeLocation] = React.useState("");

  // set delete confirmation pop up settings
  const [isDialogOpen, setIsDialogOpen] = useState(false);
  const [entityId, setEntityId] = useState(null);
  const [entityType, setEntityType] = useState("");

  const queryClient = useQueryClient();
  const deleteCafeMutationFn = useDeleteCafeFn();

  const handleOpenDialog = (id, type) => {
    setEntityId(id);
    setEntityType(type);
    setIsDialogOpen(true);
  };

  const handleCloseDialog = () => {
    setIsDialogOpen(false);
    setEntityId(null);
    setEntityType("");

    //queryClient.invalidateQueries({ queryKey: ["cafesByLocation"] });
  };

  const handleDeleteConfirmed = async () => {
    await deleteCafeMutationFn.mutateAsync(entityId);
  };

  // Debounced API call function
  useEffect(() => {
    const delayDebounceFn = setTimeout(() => {
      setCafeLocation(locationFilter);
    }, 1000); // 500ms debounce

    return () => clearTimeout(delayDebounceFn);
  }, [locationFilter]);

  const { data: cafes, isLoading: isLoading, error: error } = useGetCafesByLocationQuery(cafeLocation);

  const columns = [
    // { headerName: "Id", field: "cafeId", flex: 2 },
    {
      headerName: "Logo",
      field: "logoPath",
      autoHeight: true,
      cellRenderer: (params) => (
        <Avatar src={`${BASE_IMAGE_URL}/${params.data.logoPath}`} alt={params.value} sx={{ bgcolor: "#f0f0f0", width: 40, height: 40, borderRadius: "50%", margin: "5px" }} />
      ),
      flex: 1,
    },
    { headerName: "Name", field: "name", flex: 2 },
    { headerName: "Description", field: "description", flex: 3 },
    { headerName: "Location", field: "location", flex: 2 },
    {
      headerName: "No Of Employees",
      field: "employeeCount",
      cellRenderer: (params) => (
        <Button variant="text" onClick={() => navigate(`/cafes/details?cafeId=${params.data.cafeId}&cafe=${encodeURIComponent(params.data.name)}`)}>
          {params.data.employeeCount}
        </Button>
      ),
      flex: 1,
    },
    {
      headerName: "Actions",
      field: "actions",
      flex: 2,
      cellRenderer: (params) => (
        <>
          <IconButton color="primary" onClick={() => navigate(`/cafes/details?cafeId=${params.data.cafeId}&cafe=${encodeURIComponent(params.data.name)}`)}>
            <PersonSearchIcon />
          </IconButton>
          <IconButton color="primary" onClick={() => navigate(`/cafes/edit?cafeId=${params.data.cafeId}`)}>
            <EditIcon />
          </IconButton>
          <IconButton color="secondary" onClick={() => handleOpenDialog(params.data.cafeId, "cafe")}>
            <DeleteIcon />
          </IconButton>
        </>
      ),
    },
  ];

  return (
    <div style={{ width: "100%", height: "80vh", display: "flex", flexDirection: "column", margin: 0 }}>
      <ConfirmationPopup open={isDialogOpen} handleClose={handleCloseDialog} handleConfirmed={handleDeleteConfirmed} entityId={entityId} entityType={entityType} />

      {/* <Typography variant="h4">Cafe List</Typography> */}
      <Box display="flex" justifyContent="space-between" alignItems="center" style={{ margin: "10px 0px" }}>
        <Button variant="contained" color="primary" onClick={() => navigate("/cafes/add")}>
          Add New Cafe
        </Button>
        <TextField label="Filter by Location" variant="outlined" value={locationFilter} onChange={(e) => setLocationFilter(e.target.value)} size="small" />
      </Box>

      {isLoading ? (
        <Typography>Loading...</Typography>
      ) : error ? (
        <Typography color="error">Failed to load cafes.</Typography>
      ) : (
        <div className="ag-grid-wrapper ag-theme-alpine" style={{ height: "100%", width: "100%" }}>
          <AgGridReact rowData={cafes} columnDefs={columns} pagination={true} paginationPageSize={20} />
        </div>
      )}
    </div>
  );
};

export default CafeList;
