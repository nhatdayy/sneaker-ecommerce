import "./login.css";
import React, { useState } from "react";

const Login = () => {
  const [showPassword, setShowPassword] = useState(false);

  const togglePassword = () => {
    setShowPassword(!showPassword);
  };
  return (
    <div className="wrapper">
      <form action={""}>
        <h1>Login</h1>
        <div className="input-box">
          <input type="text" placeholder="Email" required />
          <i className="fa-solid fa-user icon"></i>
        </div>
        <div className="input-box">
          <input
            type={showPassword ? "text" : "password"}
            placeholder="Password"
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
            Don't have an account? <a href="#">Register</a>
          </p>
        </div>
      </form>
    </div>
  );
};

export default Login;
