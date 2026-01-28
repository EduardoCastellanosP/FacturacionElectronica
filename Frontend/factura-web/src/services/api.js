import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:5083/api', // Revisa que este sea el puerto donde corre tu .NET
  headers: {
    'Content-Type': 'application/json',
  },
});

export default api;