import axios from 'axios'

export default class AuthService {
  static async Login(username, password) {
    let result = { err: false }
    try {
      result = await axios.post('api/home/login', { username, password })
    } catch (ex) {
      console.log(ex)
      result.err = true
    }
    return result
  }
}
