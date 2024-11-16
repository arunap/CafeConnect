import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useDispatch } from "react-redux";
import { showNotification } from "./../Store/notificationSlice";
import { createEmployee, deleteEmployee, updateEmployee } from "./endpoints.employee";

export const useCreateEmployeeFn = () => {
  const dispatch = useDispatch();
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (data) => createEmployee(data),
    onSuccess: () => {
      dispatch(showNotification({ message: "Employee registered successfully!", severity: "success", duration: 3000 }));
    },
    onError: (error) => {
      console.log(error);
      dispatch(showNotification({ message: error || "Error registering employee.", severity: "error", duration: 3000 }));
    },
  });
};

export const useUpdateEmployeeFn = () => {
  const dispatch = useDispatch();
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ id, data }) => updateEmployee({ id, data }),
    onSuccess: () => {
      dispatch(showNotification({ message: "Employee updated successfully!", severity: "success", duration: 3000 }));
    },
    onError: (error) => {
      console.log(error);
      dispatch(showNotification({ message: error || "Error updating Employee.", severity: "error", duration: 3000 }));
    },
  });
};

export const useDeleteEmployeeFn = () => {
  const queryClient = useQueryClient();
  const dispatch = useDispatch();
  return useMutation({
    mutationFn: (id) => deleteEmployee(id),
    onSuccess: () => dispatch(showNotification({ message: `Employee deleted successfully!`, severity: "success", duration: 3000 })),
    onError: (error) => dispatch(showNotification({ message: error || `Error deleting employee.`, severity: "error", duration: 3000 })),
    onSettled: () => queryClient.invalidateQueries({ queryKey: ["employees"] }),
  });
};
