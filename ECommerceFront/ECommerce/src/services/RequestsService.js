import axios from 'axios'

export default class RequestsService {
  static getDefaultSuccessConfig(result) {
    return {
      title: 'Command executed successfully.',
      text: result?.msg ?? result?.Message ?? '',
      type: 'success',
    }
  }
  static getDefaultErrorConfig(result) {
    return {
      title: 'Error(s) occurred during command execution.',
      text: result?.msg ?? result?.Message ?? '',
      type: 'warning',
    }
  }

  static async Get(path, params) {
    let result = null
    try {
      result = await axios.get(path, params)
    } catch (ex) {
      result.error = true
    }
    return result
  }

  static async Post(path, params) {
    let result = null
    try {
      result = await axios.post(path, params)
    } catch (ex) {
      result.error = true
    }
    return result
  }

  static async Put(path, params) {
    let result = null
    try {
      result = await axios.put(path, params)
    } catch (ex) {
      result.error = true
    }
    return result
  }

  static async Delete(path, params) {
    let result = null
    try {
      result = await axios.delete(path, params)
    } catch (ex) {
      result.error = true
    }
    return result
  }
}
