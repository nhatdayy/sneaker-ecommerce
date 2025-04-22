import "../login/Login.css";
import React, { useState } from "react";
import authService from "../../services/Auth/authService";
import { message } from "antd";
import { useNavigate } from "react-router-dom";

const Register = () => {
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [showPassword, setShowPassword] = useState(false);
  const togglePassword = () => setShowPassword(!showPassword);

  const [messageApi, contextHolder] = message.useMessage();
  const navigate = useNavigate();

  const success = () => {
    messageApi.open({
      type: "success",
      content: "Register successful. Please login.",
    });
  };

  const error = (msg = "Register failed") => {
    messageApi.open({
      type: "error",
      content: msg,
    });
  };

  const register = async (e) => {
    e.preventDefault();
    if (password !== confirmPassword) {
      return error("Passwords do not match");
    }
    console.log(name, email, password, confirmPassword);

    authService
      .register(name, email, password)
      .then((value) => {
        if (value !== null) {
          console.log(value);

          success();
          return navigate("/auth/login");
        }
      })
      .catch(() => {
        error();
      });
  };

  return (
    <div className="login-wrapper">
      <div className="wrapper">
        <form onSubmit={register}>
          <h1>Register</h1>
          {contextHolder}
          <div className="input-box">
            <input
              type="text"
              placeholder="Name"
              value={name}
              onChange={(e) => setName(e.target.value)}
              required
            />
            <i className="fa-solid fa-user icon"></i>
          </div>
          <div className="input-box">
            <input
              type="text"
              placeholder="Email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
            />
            <i className="fa-solid fa-envelope icon"></i>
          </div>
          <div className="input-box">
            <input
              type={showPassword ? "text" : "password"}
              placeholder="Password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
            />
            <i
              className={`fa-solid ${
                showPassword ? "fa-eye-slash" : "fa-eye"
              } icon`}
              onClick={togglePassword}
              style={{ cursor: "pointer" }}
            ></i>
          </div>
          <div className="input-box">
            <input
              type={showPassword ? "text" : "password"}
              placeholder="Confirm Password"
              value={confirmPassword}
              onChange={(e) => setConfirmPassword(e.target.value)}
              required
            />
            <i
              className={`fa-solid ${
                showPassword ? "fa-eye-slash" : "fa-eye"
              } icon`}
              onClick={togglePassword}
              style={{ cursor: "pointer" }}
            ></i>
          </div>
          <button type="submit">Register</button>
          <div className="register-link">
            <p>
              Already have an account? <a href="/auth/login">Login</a>
            </p>
          </div>
        </form>
      </div>
    </div>
  );
};

export default Register;
