import axiosClient from '../apiClient/axiosClient';

const formatApi = {
    getAll: () => axiosClient.get('/format'),
};

export default formatApi;
