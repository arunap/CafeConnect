import { axiosPublic } from "./axios-client";

// Define the mutation function
export const createEmployee = async (newEmployee) => {
  const response = await axiosPublic.post(`/employee`, { ...newEmployee });

  if (response.status !== 201) throw new Error("Failed to create Employee");
  return response.data;
};

export const updateEmployee = async ({ data }) => {
  debugger;
  const { id, data: formData } = data;
  //console.log(x);
  const response = await axiosPublic.put(`/employee/${id}`, formData);

  if (response.status !== 204) throw new Error("Failed to update Employee");
  return response.data;
};

export const getEmployees = async () => {
  const response = await axiosPublic.get(`/Employees`);

  if (response.status !== 200) throw new Error("Failed to fetch Employees");
  return response.data;
};

export const getEmployeesByCafeName = async (cafeName) => {
  const response = await axiosPublic.get(`/Employees?cafe=${cafeName}`);

  if (response.status !== 200) throw new Error("Failed to fetch Employees");
  return response.data;
};

export const getEmployeeById = async (id) => {
  const response = await axiosPublic.get(`/Employee/${id}`);

  if (response.status !== 200) throw new Error("Failed to fetch Employee");
  return response.data;
};

export const deleteEmployee = async (id) => {
  const response = await axiosPublic.delete(`/Employee/${id}`);

  if (response.status !== 204) throw new Error(`Failed to delete employee`);
  return response.data;
};
