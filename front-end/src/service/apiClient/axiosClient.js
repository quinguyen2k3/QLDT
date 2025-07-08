import axios from 'axios';
import { getAccessToken, getRefreshToken, setTokens, clearTokens } from '@/service/authService';

const axiosClient = axios.create({
    baseURL: 'http://localhost:5015/api',
    headers: {
        'Content-Type': 'application/json',
    },
    timeout: 10000,
});

axiosClient.interceptors.request.use(
    (config) => {
        const token = getAccessToken();
        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    },
    (error) => Promise.reject(error)
);

axiosClient.interceptors.response.use(
    (response) => response,
    async (error) => {
        const originalRequest = error.config;
        if (error.response?.status === 401 && !originalRequest._retry) {
            originalRequest._retry = true;
            try {
                const refreshToken = getRefreshToken();
                const res = await axios.post("http://localhost:5015/api/auth/refresh-token", {
                    accessToken: getAccessToken(),
                    refreshToken: refreshToken,
                });

                const { accessToken, refreshToken: newRefreshToken } = res.data;
                setTokens(accessToken, newRefreshToken);

                // Gắn accessToken mới vào header và retry request
                originalRequest.headers.Authorization = `Bearer ${accessToken}`;
                return axiosClient(originalRequest);
            } catch (refreshError) {
                clearTokens();
                window.location.href = "/login";
                return Promise.reject(refreshError);
            }
        }
        return Promise.reject(error);
    }
);

export default axiosClient;
