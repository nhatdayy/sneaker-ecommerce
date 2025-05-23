import React from "react";
import "./App.css";
import Navbar from "./components/layout/navbar/Navbar";
import PublicRouter from "./routers/publicRouter";
import { useLocation } from "react-router-dom";
function App() {
  const location = useLocation(); // ✅ Get current location

  const isLoginPage = location.pathname === "/auth/login";
  const isRegisterPage = location.pathname === "/auth/register";

  return (
    <>
      {!isLoginPage && !isRegisterPage && <Navbar />} <PublicRouter />
    </>
  );
}
export default App;
