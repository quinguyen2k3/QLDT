// apiClient.js
import axios from 'axios';
import { getAccessToken, getRefreshToken, setTokens, clearTokens, triggerLoginCallback } from '@/service/authService';
import { toast } from 'react-toastify';

const apiClient = axios.create({
    baseURL: `${process.env.REACT_APP_API_BASE_URL}/api`,
    headers: { 'Content-Type': 'application/json' },
    timeout: 5000,
});

let refreshPromise = null; 

async function doRefresh() {
    try {
        const res = await axios.post(`${process.env.REACT_APP_API_BASE_URL}/api/auth/refresh-token`, {
            accessToken: getAccessToken(),
            refreshToken: getRefreshToken(),
        });

        const { accessToken, refreshToken: newRefreshToken } = res.data;
        setTokens(accessToken, newRefreshToken);
        triggerLoginCallback();
        return accessToken;
    } catch (err) {
        clearTokens();
        toast.error('Phiên đăng nhập đã hết hạn, vui lòng đăng nhập lại.');
        setTimeout(() => (window.location.href = '/login'), 3000);
        throw err;
    } finally {
        refreshPromise = null;
    }
}

apiClient.interceptors.request.use(
    (config) => {
        const token = getAccessToken();
        if (token) config.headers.Authorization = `Bearer ${token}`;
        return config;
    },
    (error) => Promise.reject(error),
);

apiClient.interceptors.response.use(
    (res) => res,
    async (error) => {
        const originalRequest = error.config;

        if (originalRequest?.url?.includes('/login') || originalRequest?.url?.includes('/refresh-token')) {
            return Promise.reject(error);
        }

        if (error.response?.status === 403) {
            const reason = error.response?.data?.reason;
            if (reason === 'UserNotActive') {
                toast.error('Tài khoản bị vô hiệu hóa.');
                clearTokens();
                setTimeout(() => (window.location.href = '/login'), 3000);
            } else if (reason === 'PermissionDenied') {
                toast.error('Bạn không có quyền truy cập chức năng này.');
            } else {
                toast.error('Truy cập bị từ chối.');
            }
            return Promise.reject(error);
        }
        if (error.response?.status === 401 && !originalRequest._retry) {
            originalRequest._retry = true;

            if (!refreshPromise) {
                refreshPromise = doRefresh();
            }

            try {
                const newAccessToken = await refreshPromise;

                if (newAccessToken) {
                    originalRequest.headers.Authorization = `Bearer ${newAccessToken}`;
                }
                return apiClient(originalRequest);
            } catch (refreshErr) {
                return Promise.reject(refreshErr);
            }
        }

        return Promise.reject(error);
    },
);

export default apiClient;
