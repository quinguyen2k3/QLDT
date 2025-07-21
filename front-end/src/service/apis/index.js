import apiClient from '../apiClient/apiClient';

const formatApi = {
    getAll: () => apiClient.get('/format'),
    create: (formatData) => apiClient.post('/format', formatData),
    getById: (id) => apiClient.get(`/format/${id}`),
    update: (id, formatData) => apiClient.put(`/format/${id}`, formatData),
    getBasic: () => apiClient.get('/format/basic'),
};

const authApi = {
    login: (credentials) => apiClient.post('/auth/login', credentials),
    logout: (tokenData) => apiClient.post('/auth/logout', tokenData),
};

const partApi = {
    getAll: () => apiClient.get('/part'),
    create: (partData) => apiClient.post('/part', partData),
    getById: (id) => apiClient.get(`/part/${id}`),
    update: (id, partData) => apiClient.put(`/part/${id}`, partData),
};

const departmentApi = {
    getAll: () => apiClient.get('/department'),
    create: (departmentData) => apiClient.post('/department', departmentData),
    getById: (id) => apiClient.get(`/department/${id}`),
    update: (id, departmentData) => apiClient.put(`/department/${id}`, departmentData),
};

const levelApi = {
    getAll: () => apiClient.get('/education-level'),
    create: (levelData) => apiClient.post('/education-level', levelData),
    getById: (id) => apiClient.get(`/education-level/${id}`),
    update: (id, levelData) => apiClient.put(`/education-level/${id}`, levelData),
};

const unitApi = {
    getAll: () => apiClient.get('/training-unit'),
    create: (levelData) => apiClient.post('/training-unit', levelData),
    getById: (id) => apiClient.get(`/training-unit/${id}`),
    update: (id, levelData) => apiClient.put(`/training-unit/${id}`, levelData),
};

const courseApi = {
    getAll: () => apiClient.get('/course'),
    create: (courseData) =>
        apiClient.post('/course', courseData, {
            headers: {
                'Content-Type': 'multipart/form-data',
            },
        }),
    getById: (id) => apiClient.get(`/course/${id}`),
    update: (id, courseData) =>
        apiClient.put(`/course/${id}`, courseData, {
            headers: { 'Content-Type': 'multipart/form-data' },
        }),
};

const employeeApi = {
    getAll: () => apiClient.get('/employee'),
    create: (employeeData) => apiClient.post('/employee', employeeData),
    getById: (id) => apiClient.get(`/employee/${id}`),
    update: (id, employeeData) => apiClient.put(`/employee/${id}`, employeeData),
};

const classApi = {
    getAll: (id) => apiClient.get(`/class/format/${id}`),
    create: (classData) =>
        apiClient.post('/class', classData, {
            headers: {
                'Content-Type': 'multipart/form-data',
            },
        }),
    getById: (id) => apiClient.get(`/class/${id}`),
    update: (id, classData) =>
        apiClient.put(`/class/${id}`, classData, {
            headers: {
                'Content-Type': 'multipart/form-data',
            },
        }),
};

export { formatApi, authApi, partApi, departmentApi, levelApi, unitApi, courseApi, employeeApi, classApi };
