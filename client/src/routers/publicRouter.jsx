import React from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import Login from "../pages/login/Login";

const PublicRouter = () => {
  return (
    <Routes>
      <Route path="/auth/login" element={<Login />} />
    </Routes>
  );
};

export default PublicRouter;
