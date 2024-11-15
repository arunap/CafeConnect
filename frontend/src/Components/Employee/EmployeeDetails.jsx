import React from "react";
import { useParams } from "react-router-dom";

const EmployeeDetails = () => {
  const { employeeId } = useParams();

  return (
    <div>
      <h2>Employee Detail - {employeeId}</h2>
      {/* Details for the employee */}
    </div>
  );
};

export default EmployeeDetails;
