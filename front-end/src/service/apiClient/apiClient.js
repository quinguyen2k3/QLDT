import axios from 'axios';
import { getAccessToken, getRefreshToken, setTokens, clearTokens } from '@/service/authService';
import { toast } from 'react-toastify';
import { triggerLoginCallback } from '@/service/authService';

const apiClient = axios.create({
    baseURL: 'http://localhost:5015/api',
    headers: {
        'Content-Type': 'application/json',
    },
    timeout: 5000,
});

apiClient.interceptors.request.use(
    (config) => {
        const token = getAccessToken();
        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    },
    (error) => Promise.reject(error),
);

apiClient.interceptors.response.use(
    (response) => response,
    async (error) => {
        const originalRequest = error.config;
        if (originalRequest.url.includes('/login') || originalRequest.url.includes('/refresh-token')) {
            return Promise.reject(error);
        }
        
        if (error.response?.status === 401 && !originalRequest._retry) {
            originalRequest._retry = true;
            try {
                const refreshToken = getRefreshToken();
                const res = await axios.post('http://localhost:5015/api/auth/refresh-token', {
                    accessToken: getAccessToken(),
                    refreshToken: refreshToken,
                });

                const { accessToken, refreshToken: newRefreshToken } = res.data;
                setTokens(accessToken, newRefreshToken);

                //Set lại trạng thái sau khi refresh token
                triggerLoginCallback();

                originalRequest.headers.Authorization = `Bearer ${accessToken}`;
                return apiClient(originalRequest);
            } catch (refreshError) {
                clearTokens();

                toast.error('Phiên đăng nhập đã hết hạn, vui lòng đăng nhập lại.');
                setTimeout(() => {
                    window.location.href = '/login';
                }, 2000);

                return Promise.reject(refreshError);
            }
        }
        return Promise.reject(error);
    },
);

export default apiClient;
