import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import "./Navbar.css";
function Navbar() {
  const [menu, setMenu] = useState("shop");
  const navigate = useNavigate();
  const handleLogin = () => {
    navigate("/auth/login");
  };
  return (
    <div className="navbar">
      <div className="nav-logo" onClick={() => navigate("/")}>
        <p>SNEAKER</p>
      </div>
      <ul className="nav-menu">
        <li onClick={() => setMenu("shop")}>
          Shop
          {menu === "shop" ? <hr /> : <></>}
        </li>
        <li onClick={() => setMenu("sneaker")}>
          Sneaker
          {menu === "sneaker" ? <hr /> : <></>}
        </li>
        <li onClick={() => setMenu("accessory")}>
          Accessory{menu === "accessory" ? <hr /> : <></>}
        </li>
        <li onClick={() => setMenu("contact")}>
          Contact{menu === "contact" ? <hr /> : <></>}
        </li>
      </ul>
      <div className="nav-login-cart">
        <i className="fas fa-shopping-cart"></i>
        <div className="nav-cart-count">0</div>
        <button onClick={handleLogin}>Login</button>
      </div>
    </div>
  );
}

export default Navbar;
