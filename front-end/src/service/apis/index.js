import axiosClient from '../apiClient/axiosClient';

const formatApi = {
    getAll: () => axiosClient.get('/format'),
    create: (formatData) => axiosClient.post('/format', formatData),
    getById: (id) => axiosClient.get(`/format/${id}`),
    update: (id, formatData) => axiosClient.put(`/format/${id}`, formatData),
};

const authApi = {
    
    login: (credentials) => axiosClient.post('/auth/login', credentials),
    refreshToken: (tokenData) => axiosClient.post('/auth/refresh-token', tokenData),
    logout: (tokenData) => axiosClient.post('/auth/logout', tokenData),

};

export {
    formatApi, 
    authApi,
};
