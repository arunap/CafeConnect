import { Avatar, Box, Button, FormControl, Paper, Typography } from "@mui/material";
import React, { useEffect, useState } from "react";
import { Input } from "@mui/material";
import ReusableTextbox from "./../../Shared/ReusableTextbox";
import { useForm, Controller } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { BASE_IMAGE_URL } from "../../Constants";
import { useDispatch } from "react-redux";

const CafeForm = ({ cafeId, cafeItem, onSuccess }) => {
  const navigate = useNavigate();
  const isEdit = cafeId !== undefined;
  const dispatch = useDispatch();

  const [file, setFile] = useState(null);
  const [fileName, setFileName] = useState(null);

  const handleFileChange = (e) => {
    if (e.target.files[0]) {
      setFile(e.target.files[0]);
      setFileName(e.target.files[0].name);
      setValue("logo", e.target.files[0]);
    }
  };

  const {
    control,
    handleSubmit,
    reset,
    setValue,
    formState: { errors },
  } = useForm({
    defaultValues: cafeItem || { name: "", description: "", logo: "", location: "" },
  });

  useEffect(() => {
    if (cafeItem) reset(cafeItem);
  }, [cafeItem, reset]);

  const onSubmit = async (cafeData) => {
    const formData = new FormData();

    Object.keys(cafeData).forEach(function (key) {
      formData.append(key, cafeData[key]);
    });

    if (isEdit) formData.append("id", cafeId);
    // if (file && fileName) formData.append("logo", file);

    onSuccess(formData);
  };

  return (
    <Box sx={{ maxWidth: 400, mx: "auto", mt: 4, display: "flex", justifyContent: "center", alignItems: "center", flexDirection: "column" }}>
      <Paper elevation={3} sx={{ p: 5 }} square={false}>
        <Typography variant="h5" gutterBottom>
          {isEdit ? "Update Cafe" : "Add New Cafe"}
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

          {/* Description Field */}
          <ReusableTextbox
            name="description"
            control={control}
            label="Description"
            multiline
            rows={4}
            rules={{
              required: "Description is required",
              maxLength: { value: 256, message: "Description cannot exceed 256 characters" },
            }}
          />
          {isEdit ? (
            <Box sx={{ display: "flex", justifyContent: "center", alignItems: "center" }}>
              <Avatar src={`${BASE_IMAGE_URL}/${cafeItem?.logoPath}`} sx={{ width: 80, height: 80, mt: 2 }} />{" "}
            </Box>
          ) : (
            ""
          )}

          {/* Image Upload Field */}
          <FormControl fullWidth style={{ marginTop: "20px" }}>
            {/* <FormLabel>Upload Profile Picture</FormLabel> */}
            <Controller
              name="logo"
              control={control}
              rules={{
                validate: {
                  lessThan2MB: (file) => file?.size <= 2 * 1024 * 1024 || "File size should be less than 2MB",
                },
              }}
              render={({ field }) => <Input type="file" onChange={handleFileChange} accept="image/*" />}
            />
          </FormControl>
          {errors.logo && <p style={{ color: "#d32f2f", paddingLeft: "10px", fontSize: "0.75rem" }}>{errors.logo.message}</p>}

          {/* Location Field */}
          <ReusableTextbox
            name="location"
            control={control}
            label="Location"
            rules={{
              required: "Location is required",
            }}
          />
          <Box sx={{ my: 2 }}>
            <Button type="submit" variant="contained" color="primary">
              {isEdit ? "Update Cafe" : "Add Cafe"}
            </Button>
            <Button variant="outlined" color="secondary" style={{ marginLeft: "20px" }} onClick={() => navigate("/cafes")}>
              Cancel
            </Button>
          </Box>
        </form>
      </Paper>
    </Box>
  );
};

export default CafeForm;
