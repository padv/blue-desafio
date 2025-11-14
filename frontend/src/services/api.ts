import axios, { AxiosError, type InternalAxiosRequestConfig } from 'axios'

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL || '/api/',
  headers: {
    'Content-Type': 'application/json'
  }
})

api.interceptors.request.use((config: InternalAxiosRequestConfig) => {
  const token = localStorage.getItem('token')
  if (token) {
    config.headers.Authorization = `Bearer ${token}`
  }
  return config
})

api.interceptors.response.use(
  response => response,
  (error: AxiosError) => {
    if (error.response?.status === 401) {
      // Token expirado ou inválido - limpar storage e redirecionar para login
      localStorage.removeItem('token')
      // Aqui você pode disparar um evento ou usar um router para redirecionar
    }
    return Promise.reject(error)
  }
)
 
export default api