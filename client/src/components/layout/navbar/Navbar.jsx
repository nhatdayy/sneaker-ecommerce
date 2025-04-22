import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import Cookies from "js-cookie";
import authService from "../../../services/Auth/authService";
import "./Navbar.css";
function Navbar() {
  const [menu, setMenu] = useState("shop");
  const [user, setUser] = useState(null);
  const navigate = useNavigate();
  const handleLogin = () => {
    navigate("/auth/login");
  };
  const [dropdownOpen, setDropdownOpen] = useState(false);

  const toggleDropdown = () => {
    setDropdownOpen((prev) => !prev);
  };
  useEffect(() => {
    const token = Cookies.get("token");
    const infoUser = authService.parseToken(token);
    console.log(infoUser);

    if (token && infoUser.name) {
      setUser({ name: infoUser.name });
    }
  }, []);
  const handleLogout = () => {
    authService.logout();
    setUser(null);
    setDropdownOpen(false);
    navigate("/");
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
        {user ? (
          <div className="nav-user" onClick={toggleDropdown}>
            <i className="fas fa-user"></i>
            <span className="nav-user-name">{user.name}</span>
            {dropdownOpen && (
              <div className="nav-user-dropdown">
                <button onClick={handleLogout}>Đăng xuất</button>
              </div>
            )}
          </div>
        ) : (
          <button onClick={handleLogin}>Login</button>
        )}
      </div>
    </div>
  );
}

export default Navbar;
