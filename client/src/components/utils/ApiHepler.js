import axios from "axios";
import Cookies from "js-cookie";

axios.defaults.baseURL = "https://localhost:7010/api";
axios.defaults.headers.post["Content-Type"] = "application/json";

class ApiHelper {
  setToken(token) {
    Cookies.set("token", token);
  }

  getToken() {
    return Cookies.get("token");
  }

  removeToken() {
    Cookies.remove("token");
  }

  request(method, url, data) {
    let headers = {};
    const token = this.getToken();
    if (token && token !== "null") {
      headers = {
        Authorization: `Bearer ${token}`,
      };
    }
    return axios({
      method: method,
      url: url,
      data,
      headers: headers,
    }).then((res) => res.data);
  }
}

export default new ApiHelper();
