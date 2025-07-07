import axiosClient from '../apiClient/axiosClient';

const formatApi = {
    getAll: () => axiosClient.get('/format'),
    create: (formatData) => axiosClient.post('/format', formatData),
    getById: (id) => axiosClient.get(`/format/${id}`),
    update: (id, formatData) => axiosClient.put(`/format/${id}`, formatData),
};

export default formatApi;
