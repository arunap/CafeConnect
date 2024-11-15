import React from "react";
import CafeForm from "./CafeForm";

import { useUpdateCafeFn } from "../../Services/mutations.cafe";
import { useGetCafeByCafeIdQuery } from "../../Services/queries.cafe";
import { useLocation, useNavigate } from "react-router-dom";

const CafeEdit = () => {
  const navigate = useNavigate();
  const location = useLocation();

  // Helper function to parse query parameters
  const queryParams = new URLSearchParams(location.search);
  const cafeId = queryParams.get('cafeId');


  const updateMutateFn = useUpdateCafeFn();
  const { data: cafeItem } = useGetCafeByCafeIdQuery(cafeId, true);

  const onUpdateHandler = async (formData) => {
    await updateMutateFn.mutateAsync({ id: cafeId, data: formData });
    navigate("/cafes");
  };

  return <CafeForm cafeId={cafeId} cafeItem={cafeItem} onSuccess={onUpdateHandler} />;
};

export default CafeEdit;
