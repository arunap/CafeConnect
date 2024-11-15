import React from "react";
import CafeForm from "./CafeForm";
import { useCreateCafeFn } from "../../Services/mutations.cafe";
import { useNavigate } from "react-router-dom";

const CafeAdd = () => {
  const navigate = useNavigate();
  const createMutateFn = useCreateCafeFn();

  const onSubmitHandler = async (formData) => {
    await createMutateFn.mutateAsync(formData);
    navigate("/cafes");
  };

  return <CafeForm onSuccess={onSubmitHandler} />;
};

export default CafeAdd;
