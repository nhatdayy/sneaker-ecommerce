import "./login.css";
import React, { useState } from "react";
import authService from "../../services/Auth/authService";
import { message } from "antd";
import { Link, useNavigate } from "react-router-dom";
const Login = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [showPassword, setShowPassword] = useState(false);
  const togglePassword = () => {
    setShowPassword(!showPassword);
  };
  const [messageApi, contextHolder] = message.useMessage();
  const success = () => {
    messageApi.open({
      type: "success",
      content: "Login successful",
    });
  };
  const error = () => {
    messageApi.open({
      type: "error",
      content: "Login failed",
    });
  };
  const navigate = useNavigate();
  const login = async (e) => {
    e.preventDefault();

    authService
      .login(email, password)
      .then((value) => {
        if (value !== null) {
          success();
          return navigate("/home");
        }
      })
      .catch(() => {
        error();
        return navigate("/auth/login");
      });
  };

  return (
    <div className="login-wrapper">
      <div className="wrapper">
        <form onSubmit={login}>
          <h1>Login</h1>
          {contextHolder}
          <div className="input-box">
            <input
              type="text"
              placeholder="Email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
            />
            <i className="fa-solid fa-user icon"></i>
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
          <div className="remember-forgot">
            <label>
              <input type="checkbox" /> Remember me
            </label>
            <a href="#">Forgot password</a>
          </div>

          <button type="submit">Login</button>
          <div className="register-link">
            <p>
              Don't have an account? <Link to="/auth/register">Register</Link>
            </p>
          </div>
        </form>
      </div>
    </div>
  );
};

export default Login;
