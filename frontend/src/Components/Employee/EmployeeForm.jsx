import React, { useCallback, useEffect, useState } from "react";
import MenuItem from "@mui/material/MenuItem";
import FormControl from "@mui/material/FormControl";
import { useForm, Controller } from "react-hook-form";
import { TextField, Button, Box, Typography, FormControlLabel, RadioGroup, Radio, FormLabel } from "@mui/material";

import ReusableTextbox from "../../Shared/ReusableTextbox";
import { useNavigate } from "react-router-dom";

const EmployeeForm = ({ employeeId, employeeItem, cafeData, onSuccess, isLoading }) => {
  const navigate = useNavigate();

  const isEdit = !!employeeId;
  const {
    control,
    handleSubmit,
    reset,
    formState: { isDirty },
  } = useForm({
    defaultValues: employeeItem || { name: "", emailAddress: "", phoneNumber: "", cafeId: "0", gender: "Male" },
  });

  const onSubmit = async (data) => {
    if (data.cafeId == "0") data = { ...data, cafeId: null }; // reset the cafeId

    if (isEdit) onSuccess({ id: employeeId, data });
    else onSuccess(data);
  };

  return (
    <Box sx={{ maxWidth: 400, mx: "auto", mt: 4 }}>
      <Typography variant="h5" gutterBottom>
        {isEdit ? "Update Employee" : "Add New Employee"}
      </Typography>
      <form onSubmit={handleSubmit(onSubmit)}>
        {/* Name Field */}
        <ReusableTextbox
          name="name"
          control={control}
          label="Name"
          rules={{
            required: "Name is required",
            minLength: { value: 6, message: "Name must be at least 6 characters" },
            maxLength: { value: 10, message: "Name cannot exceed 10 characters" },
          }}
        />

        {/* Email Field */}
        <ReusableTextbox
          name="emailAddress"
          control={control}
          label="Email"
          rules={{
            required: "Email is required",
            pattern: {
              value: /^[\w-.]+@([\w-]+\.)+[\w-]{2,4}$/,
              message: "Invalid email address",
            },
          }}
        />

        {/* Phone Number Field */}
        <ReusableTextbox
          name="phoneNumber"
          control={control}
          label="Phone Number"
          rules={{
            required: "Phone number is required",
            pattern: { value: /^(8|9)\d{7}$/, message: "Invalid SG phone number. Must start with 8 or 9 and be 8 digits long." },
          }}
        />

        {/* Assigned Café Dropdown */}
        <FormControl fullWidth sx={{ my: 2 }}>
          <Controller
            name="cafeId"
            control={control}
            rules={{ required: "Assigned Café is required" }}
            render={({ field, fieldState: { error } }) => (
              <TextField select label="Assign Cafe" {...field} variant="outlined" fullWidth error={!!error} helperText={error?.message}>
                {/* Default placeholder option */}
                <MenuItem value="0">
                  <em>--Select a Cafe--</em>
                </MenuItem>
                {/* Map the cafeData options */}
                {cafeData &&
                  cafeData.map((option) => (
                    <MenuItem key={option.cafeId} value={option.cafeId}>
                      {option.name}
                    </MenuItem>
                  ))}
              </TextField>
            )}
          />
        </FormControl>

        {/* Gender Radio Button Group */}
        <FormControl component="fieldset" sx={{ my: 2 }}>
          <FormLabel component="legend">Gender</FormLabel>
          <Controller
            name="gender"
            control={control}
            rules={{ required: "Gender is required" }}
            defaultValue={"Male"} // <-- Ensure an initial value is set
            render={({ field }) => (
              <RadioGroup {...field} row value={field.value}>
                <FormControlLabel value="Male" control={<Radio />} label="Male" />
                <FormControlLabel value="Female" control={<Radio />} label="Female" />
              </RadioGroup>
            )}
          />
        </FormControl>

        {/* Submit Button */}
        <Box sx={{ my: 2 }}>
          <Button type="submit" variant="contained" color="primary" disabled={isLoading}>
            {isEdit ? "Update Employee" : "Add Employee"}
          </Button>
          <Button variant="outlined" color="secondary" style={{ marginLeft: "20px" }} onClick={() => navigate("/employees")}>
            Cancel
          </Button>
        </Box>
      </form>
    </Box>
  );
};

export default EmployeeForm;
