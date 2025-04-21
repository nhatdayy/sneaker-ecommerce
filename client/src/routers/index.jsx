import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import App from "./App";
import Home from "./pages/Home";
import About from "./pages/About";
import NotFound from "./pages/NotFound";

const root = ReactDOM.createRoot(document.getElementById("root"));

root.render(
  <React.StrictMode>
    <BrowserRouter>
      <Routes>
        {/* Route chính */}
        <Route path="/" element={<App />}>
          {/* <Route index element={<Home />} />
          <Route path="about" element={<About />} />
          <Route path="*" element={<NotFound />} /> */}
        </Route>
      </Routes>
    </BrowserRouter>
  </React.StrictMode>
);
