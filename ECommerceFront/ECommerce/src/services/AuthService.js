import axios from 'axios';

export default class AuthService {
  static async Login(username, password) {
    try {
      const response = await axios.post('api/home/login', { username, password });
      return response;
    } catch (error) {
      console.log(error);
      return { err: true };
    }
  }

  static async Logout() {
    try {
      const response = await axios.post('api/home/logout');
      return response;
    } catch (error) {
      console.log(error);
      return { err: true };
    }
  }
}
