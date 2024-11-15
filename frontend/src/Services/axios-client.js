import axios from "axios";
import { BASE_API_URL } from "../Constants";

export const axiosPublic = axios.create({
  baseURL: BASE_API_URL,
  // headers: { "Content-Type": "application/json" },
  withCredentials: false,
});

// Add a request interceptor to include the token
axiosPublic.interceptors.request.use(
  (config) => config,
  (error) => {
    debugger;
    return Promise.reject(error);
  }
);

axiosPublic.interceptors.response.use(
  (response) => response,
  (error) => {
    const errorMessage = error.response?.data?.Detailed || error.message || "An error occurred";
    window.location.href = `/servererror?detailed=${encodeURIComponent(errorMessage)}`;
    console.log(error);

    return Promise.reject(error);
  }
);
