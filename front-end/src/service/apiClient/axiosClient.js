import axios from 'axios';

const axiosClient = axios.create({
    baseURL: 'http://localhost:5015/api',
    headers: {
        'Content-Type': 'application/json',
    },
    timeout: 10000, 
});


axiosClient.interceptors.response.use(
    (response) => response.data,
    (error) => Promise.reject(error)
);

export default axiosClient;
