import { useQuery } from "@tanstack/react-query";
import { getEmployeeById, getEmployees, getEmployeesByCafeName } from "./endpoints.employee";

export const useGetEmployeesQuery = (cafe) => {
  return useQuery({
    queryKey: ["employees"],
    queryFn: () => (cafe === undefined ? getEmployees() : getEmployeesByCafeName(cafe)),
  });
};

export const useGetEmployeesByCafeNameQuery = (cafeName) => {
  return useQuery({
    queryKey: ["employeesByCafe", { cafeName }],
    queryFn: () => getEmployeesByCafeName(cafeName),
  });
};

export const useGetEmployeesByIdQuery = (employeeId, isEnabled) => {
  return useQuery({
    queryKey: ["employeesById", { employeeId }],
    queryFn: () => getEmployeeById(employeeId),
    enabled: !!employeeId && isEnabled,
  });
};
