import { axiosPublic } from "./axios-client";

// API call to fetch cafes
export const getCafes = async () => {
  const response = await axiosPublic.get(`/cafes`);

  if (response.status !== 200) throw new Error("Failed to fetch cafes");
  return response.data;
};

// fetch cafe by location
export const getCafesByLocation = async (location) => {
  const response = await axiosPublic.get(`/cafes?location=${location}`);

  if (response.status !== 200) throw new Error("Failed to fetch cafes");
  return response.data;
};

export const getCafeByCafeId = async (id) => {
  const response = await axiosPublic.get(`/cafe/${id}`);

  if (response.status !== 200) throw new Error("Failed to fetch cafe");
  return response.data;
};

// insert a cafe
export const createCafe = async (newCafe) => {
  const response = await axiosPublic.post(`/cafe`, newCafe);
  if (response.status !== 201) throw new Error("Failed to create Cafe");
  return response.data;
};

// update a cafe
export const updateCafe = async ({ id, data }) => {
  const response = await axiosPublic.put(`/cafe/${id}`, data);

  if (response.status !== 204) throw new Error("Failed to update Cafe");
  return response.data;
};

export const deleteCafe = async (id) => {
  debugger;
  const response = await axiosPublic.delete(`/cafe/${id}`);

  if (response.status !== 204) throw new Error(`Failed to delete cafe`);
  return response.data;
};
