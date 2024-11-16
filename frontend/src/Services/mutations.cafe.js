import { useMutation, useQueryClient } from "@tanstack/react-query";
import { createCafe, deleteCafe, updateCafe } from "./endpoints.cafe";
import { useDispatch } from "react-redux";
import { showNotification } from "./../Store/notificationSlice";

export const useCreateCafeFn = () => {
  const queryClient = useQueryClient();
  const dispatch = useDispatch();

  return useMutation({
    mutationFn: (data) => createCafe(data),
    onSuccess: () => dispatch(showNotification({ message: "Cafe registered successfully!", severity: "success", duration: 3000 })),
    onError: (error) => dispatch(showNotification({ message: error || "Error registering cafe.", severity: "error", duration: 3000 })),
  });
};

export const useUpdateCafeFn = () => {
  const dispatch = useDispatch();

  return useMutation({
    mutationFn: ({ id, data }) => updateCafe({ id, data }),
    onSuccess: () => dispatch(showNotification({ message: "Cafe updated successfully!", severity: "success", duration: 3000 })),
    onError: (error) => {
      console.log(error);
      dispatch(showNotification({ message: error || "Error updating cafe.", severity: "error", duration: 3000 }));
    },
  });
};

export const useDeleteCafeFn = () => {
  const dispatch = useDispatch();
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (id) => deleteCafe(id),
    onSuccess: () => dispatch(showNotification({ message: `Cafe deleted successfully!`, severity: "success", duration: 3000 })),
    onError: (error) => dispatch(showNotification({ message: error || `Error deleting cafe.`, severity: "error", duration: 3000 })),
    onSettled: () => queryClient.invalidateQueries({ queryKey: ["cafesByLocation"] }),
  });
};
