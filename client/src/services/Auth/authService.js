import {
  setToken,
  getToken,
  request,
} from "../../components/utils/ApiHepler.js";
import jwtDecode from "jwt-decode";
export class authenticationService {
  static async login(email, password) {
    const response = await request("POST", "/auth/login", {
      email,
      password,
    });
    if (response.ok) {
      setToken(response.data.token);
      return response.data.token;
    } else {
      throw new Error("Login failed");
    }
  }

  static async register(name, email, password) {
    const response = await request("/auth/register", {
      name,
      email,
      password,
    });
    if (response.ok) {
      return response.data.token;
    } else {
      throw new Error("Registration failed");
    }
  }

  static logout() {
    window.Cookies.remove("token");
  }

  static getCurrentUser() {
    const token = getToken();
    if (token) {
      return JSON.parse(atob(token.split(".")[1]));
    }
    return null;
  }
  static parseToken = () => {
    try {
      const token = getToken();
      const decoded = jwtDecode(token);
      const id = decoded.id;
      const name = decoded.name;
      const role = decoded.role;
      return { id, name, role };
    } catch (err) {
      console.log(err);
    }
  };
}
