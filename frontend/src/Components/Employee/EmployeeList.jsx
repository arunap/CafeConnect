import React, { useState } from "react";
import { Box, Button, IconButton, Typography } from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import { AgGridReact } from "ag-grid-react";

import "ag-grid-community/styles/ag-grid.css";
import "ag-grid-community/styles/ag-theme-alpine.css";

import ConfirmationPopup from "../../Shared/ConfirmationPopup";
import { useGetEmployeesQuery } from "../../Services/queries.employee";
import { useNavigate, useParams } from "react-router-dom";
import { useDeleteEmployeeFn } from "../../Services/mutations.employee";

const EmployeeList = () => {
  const { cafe } = useParams();
  const navigate = useNavigate();

  const deleteEmployeeMutationFn = useDeleteEmployeeFn();

  const cafeNameParam = cafe;

  // Conditionally execute the appropriate query hook
  const { data: employees, error, isLoading } = useGetEmployeesQuery(cafeNameParam);

  // set delete confirmation pop up settings
  const [isDialogOpen, setIsDialogOpen] = useState(false);
  const [entityId, setEntityId] = useState(null);
  const [entityType, setEntityType] = useState("");

  const handleOpenDialog = (id, type) => {
    setEntityId(id);
    setEntityType(type);
    setIsDialogOpen(true);
  };

  const handleCloseDialog = () => {
    setIsDialogOpen(false);
    setEntityId(null);
    setEntityType("");
  };

  const handleDeleteConfirmed = async () => {
    await deleteEmployeeMutationFn.mutateAsync(entityId);
  };

  const columnDefs = [
    { headerName: "Employee ID", field: "employeeId", sortable: true, filter: true, flex: 1 },
    { headerName: "Name", field: "name", sortable: true, filter: true, flex: 1 },
    { headerName: "Email", field: "emailAddress", sortable: true, filter: true, flex: 1 },
    { headerName: "Phone Number", field: "phoneNumber", sortable: true, filter: true, flex: 1 },
    { headerName: "Days Worked", field: "daysWorked", sortable: true, filter: true, flex: 1 },
    { headerName: "Café Name", field: "cafeName", sortable: true, filter: true, flex: 1 },
    {
      headerName: "Actions",
      field: "actions",
      flex: 1,
      cellRenderer: (params) => (
        <>
          <IconButton color="primary" onClick={() => navigate(`/employees/edit?employeeId=${params.data.employeeId}`)}>
            <EditIcon />
          </IconButton>
          <IconButton color="secondary" onClick={() => handleOpenDialog(params.data.employeeId, "employee")}>
            <DeleteIcon />
          </IconButton>
        </>
      ),
    },
  ];

  return (
    <div style={{ width: "100%", height: "80vh", display: "flex", flexDirection: "column", margin: 0 }}>
      <ConfirmationPopup open={isDialogOpen} handleClose={handleCloseDialog} handleConfirmed={handleDeleteConfirmed} entityId={entityId} entityType={entityType} />

      <Box display="flex" justifyContent="space-between" alignItems="center" style={{ margin: "10px 0px" }}>
        <Button variant="contained" color="primary" onClick={() => navigate("/employees/add")}>
          Add New Employee
        </Button>
      </Box>

      {isLoading ? (
        <Typography>Loading...</Typography>
      ) : error ? (
        <Typography color="error">Failed to load employees.</Typography>
      ) : (
        <div className="ag-grid-wrapper ag-theme-alpine" style={{ height: "100%", width: "100%" }}>
          <AgGridReact rowData={employees} columnDefs={columnDefs} pagination={true} paginationPageSize={20} />
        </div>
      )}
    </div>
  );
};

export default EmployeeList;
