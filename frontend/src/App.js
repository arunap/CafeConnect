import React from "react";

import "./App.css";
import { Route, Routes } from "react-router-dom";
import NotFound from "./Shared/NotFound";

import Layout from "./Shared/Layout";
import Cafe from "./Pages/Cafe";
import CafeEdit from "./../src/Components/Cafe/CafeEdit";
import CafeAdd from "./../src/Components/Cafe/CafeAdd";
import CafeDetails from "./../src/Components/Cafe/CafeDetails";

import Employee from "./Pages/Employee";
import EmployeeDetails from "./Components/Employee/EmployeeDetails";
import EmployeeAdd from "./Components/Employee/EmployeeAdd";
import EmployeeEdit from "./Components/Employee/EmployeeEdit";
import ServerErrors from "./Shared/ServerErrors";

function App() {
  return (
    <Routes>
      <Route path="/" element={<Layout />}>
        <Route path="*" element={<NotFound />} />
        <Route path="/servererror" element={<ServerErrors />} />

        {/* cafe routes */}
        <Route>
          <Route path="/" element={<Cafe title="All Cafes" />}></Route>
          <Route path="/cafes" element={<Cafe title="All Cafes" />}></Route>
          <Route path="/cafes/add" element={<CafeAdd title="Add Cafe" />} />
          <Route path="/cafes/edit" element={<CafeEdit title="Edit List" />} />
          <Route path="/cafes/details" element={<CafeDetails title="View Cafe Details" />} />
        </Route>

        {/* employee routes */}
        <Route>
          <Route path="/" element={<Employee title="All Employees" />}></Route>
          <Route path="/employees" element={<Employee title="All Employees" />}></Route>
          <Route path="/employees/add" element={<EmployeeAdd title="Add Employee" />} />
          <Route path="/employees/edit" element={<EmployeeEdit title="Edit Employee" />} />
          <Route path="/employees/details" element={<EmployeeDetails title="View Employee Details" />} />
        </Route>
      </Route>
    </Routes>
  );
}

export default App;
