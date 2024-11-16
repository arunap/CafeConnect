import React from "react";
import EmployeeForm from "./EmployeeForm";

import { useUpdateEmployeeFn } from "../../Services/mutations.employee";
import { useGetEmployeesByIdQuery } from "../../Services/queries.employee";
import { useGetCafesQuery } from "../../Services/queries.cafe";
import { useLocation, useNavigate, useParams } from "react-router-dom";

const EmployeeEdit = () => {
  const navigate = useNavigate();
  const location = useLocation();

  // Helper function to parse query parameters
  const queryParams = new URLSearchParams(location.search);
  const employeeId = queryParams.get("employeeId");

  const updateMutateFn = useUpdateEmployeeFn();
  const { data: employeeData, isLoading: isEmployeeLoading, error: employeeError } = useGetEmployeesByIdQuery(employeeId, true);
  const { data: cafeData, isLoading: isCafeLoading, error: cafeError } = useGetCafesQuery();

  const onUpdateHandler = async (formData) => {
    await updateMutateFn.mutateAsync({ id: employeeId, data: formData });
    navigate("/employees");
  };

  // Handle loading states
  if (isCafeLoading || isEmployeeLoading) return <div>Loading...</div>;

  // Handle errors
  if (cafeError || employeeError) return <div>Error fetching data.</div>;

console.log("runing......!")
  // const savedCafe = cafeData.filter((x) => x.name === employeeData.cafeName)[0];
  // const employeeItem = { cafeId: savedCafe == null ? "-1" : savedCafe["id"], ...employeeData };

  return <EmployeeForm employeeId={employeeId} employeeItem={employeeData} cafeData={cafeData} isLoading={isCafeLoading || isEmployeeLoading} onSuccess={onUpdateHandler} />;
};

export default EmployeeEdit;
