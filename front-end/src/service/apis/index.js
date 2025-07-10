import apiClient from '../apiClient/apiClient';

const formatApi = {
    getAll: () => apiClient.get('/format'),
    create: (formatData) => apiClient.post('/format', formatData),
    getById: (id) => apiClient.get(`/format/${id}`),
    update: (id, formatData) => apiClient.put(`/format/${id}`, formatData),
};

const authApi = {
    
    login: (credentials) => apiClient.post('/auth/login', credentials),
    logout: (tokenData) => apiClient.post('/auth/logout', tokenData),

};

export {
    formatApi, 
    authApi,
};
