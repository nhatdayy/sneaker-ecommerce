import ApiHelper from "../../components/utils/ApiHepler";
import { jwtDecode } from "jwt-decode";
import Cookies from "js-cookie";

class authenticationService {
  static async login(email, password) {
    const response = await ApiHelper.request("POST", "/auth/login", {
      email,
      password,
    });
    if (response.isSuccess === true) {
      ApiHelper.setToken(response.data.token);

      return response.data.token;
    } else {
      throw new Error("Login failed");
    }
  }

  static async register(name, email, password) {
    const response = await ApiHelper.request("POST", "/auth/register", {
      name,
      email,
      password,
    });
    if (response.isSuccess === true) {
      return response.data.token;
    } else {
      throw new Error("Registration failed");
    }
  }

  static logout() {
    Cookies.remove("token");
  }

  static getCurrentUser() {
    const token = ApiHelper.getToken();
    if (token) {
      return JSON.parse(atob(token.split(".")[1]));
    }
    return null;
  }
  static parseToken = () => {
    try {
      const token = ApiHelper.getToken();
      const decoded = jwtDecode(token);
      const id = decoded.Id;
      const name = decoded.Name;
      const role = decoded.Role;
      return { id, name, role };
    } catch (err) {
      console.log(err);
    }
  };
}

export default authenticationService;
