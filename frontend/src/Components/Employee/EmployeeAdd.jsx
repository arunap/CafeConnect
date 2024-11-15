import React from "react";
import { useNavigate } from "react-router-dom";

import EmployeeForm from "./EmployeeForm";
import { useCreateEmployeeFn } from "../../Services/mutations.employee";
import { useGetCafesQuery } from "../../Services/queries.cafe";

const EmployeeAdd = () => {
  const navigate = useNavigate();
  const createMutateFn = useCreateEmployeeFn();

  const onSubmitHandler = async (formData) => {
    await createMutateFn.mutateAsync(formData);
    navigate("/employees");
  };

  const { data: cafeData, isLoading: isCafeLoading, error: cafeError } = useGetCafesQuery();

  // Handle loading states
  if (isCafeLoading) return <div>Loading...</div>;

  // Handle errors
  if (cafeError) return <div>Error fetching data.</div>;

  return <EmployeeForm onSuccess={onSubmitHandler} cafeData={cafeData} isLoading={isCafeLoading} />;
};

export default EmployeeAdd;
