import apiClient from '../apiClient/apiClient';

const formatApi = {
    getAll: () => apiClient.get('/format'),
    create: (formatData) => apiClient.post('/format', formatData),
    getById: (id) => apiClient.get(`/format/${id}`),
    update: (id, formatData) => apiClient.put(`/format/${id}`, formatData),
    getBasic: () => apiClient.get('/format/basic'),
};

const authApi = {
    changePassword: (payload) => apiClient.put('/auth/change-password', payload),
    login: (credentials) => apiClient.post('/auth/login', credentials),
    logout: (tokenData) => apiClient.post('/auth/logout', tokenData),
};

const partApi = {
    getAll: () => apiClient.get('/part'),
    getAllByMe: () => apiClient.get('/part/me'),
    getAllActive: () => apiClient.get('/part/active'),
    create: (partData) => apiClient.post('/part', partData),
    getById: (id) => apiClient.get(`/part/${id}`),
    update: (id, partData) => apiClient.put(`/part/${id}`, partData),
};

const departmentApi = {
    getAll: () => apiClient.get('/department'),
    getAllByMe: () => apiClient.get('/department/me'),
    getAllActive: () => apiClient.get('/department/active'),
    create: (departmentData) => apiClient.post('/department', departmentData),
    getById: (id) => apiClient.get(`/department/${id}`),
    update: (id, departmentData) => apiClient.put(`/department/${id}`, departmentData),
};

const levelApi = {
    getAll: () => apiClient.get('/education-level'),
    getAllActive: () => apiClient.get('/education-level/active'),
    create: (levelData) => apiClient.post('/education-level', levelData),
    getById: (id) => apiClient.get(`/education-level/${id}`),
    update: (id, levelData) => apiClient.put(`/education-level/${id}`, levelData),
};

const unitApi = {
    getAll: () => apiClient.get('/training-unit'),
    getAllActive: () => apiClient.get('/training-unit/active'),
    create: (levelData) => apiClient.post('/training-unit', levelData),
    getById: (id) => apiClient.get(`/training-unit/${id}`),
    update: (id, levelData) => apiClient.put(`/training-unit/${id}`, levelData),
};

const courseApi = {
    getAll: () => apiClient.get('/course'),
    getAllActive: () => apiClient.get('/course/active'),
    getAllByMe: () => apiClient.get('/course/me'),
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
    getAllByMe: () => apiClient.get('/employee/me'),
    getMyEmployeeInfo: () => apiClient.get('/employee/my-info'),
    getAllActive: () => apiClient.get('/employee/active'),
    getAllByDepartmentMe: () => apiClient.get('/employee/department/me'),
    create: (employeeData) => apiClient.post('/employee', employeeData),
    getById: (id) => apiClient.get(`/employee/${id}`),
    getDetail: (id) => apiClient.get(`/employee/detail/${id}`),
    update: (id, employeeData) => apiClient.put(`/employee/${id}`, employeeData),
};

const classApi = {
    getAll: () => apiClient.get('/class'),
    getAllByEmployee: (id) => apiClient.get(`/class/employee/${id}`),
    getAllUserStudied: () => apiClient.get('/class/studied'),
    getAllByMe: () => apiClient.get('/class/me'),
    getAllByFormat: (id) => apiClient.get(`/class/format/${id}`),
    getAllByUserAndFormat: (id) => apiClient.get(`/class/format/me/${id}`),
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

const roleApi = {
    getAll: () => apiClient.get('/role'),
};

const permissionApi = {
    getAllByUser: () => apiClient.get('/permission'),
};

const userApi = {
    getAll: () => apiClient.get('/user'),
    create: (userData) => apiClient.post('/user', userData),
    getById: (id) => apiClient.get(`/user/${id}`),
    update: (id, userData) => apiClient.put(`/user/${id}`, userData),
};

const majorApi = {
    getAll: () => apiClient.get('/major'),
    getAllActive: () => apiClient.get('/major/active'),
    create: (majorData) => apiClient.post('/major', majorData),
    getById: (id) => apiClient.get(`/major/${id}`),
    update: (id, majorData) => apiClient.put(`/major/${id}`, majorData),
};

const hourApi = {
    getAll: () => apiClient.get('/credit-hourse'),
    getAllActive: () => apiClient.get('/credit-hourse/active'),
    create: (hourData) => apiClient.post('/credit-hourse', hourData),
    getById: (id) => apiClient.get(`/credit-hourse/${id}`),
    update: (id, hourData) => apiClient.put(`/credit-hourse/${id}`, hourData),
};

const certificateApi = {
    getAllByMe: () => apiClient.get('/certificate/me'),
    getAllByEmployee: (id) => apiClient.get(`/certificate/employee/${id}`),
    create: (certificateData) =>
        apiClient.post('/certificate', certificateData, {
            headers: {
                'Content-Type': 'multipart/form-data',
            },
        }),
    getById: (id) => apiClient.get(`/certificate/${id}`, id),
    update: (id, certificateData) =>
        apiClient.put(`/certificate/${id}`, certificateData, {
            headers: {
                'Content-Type': 'multipart/form-data',
            },
        }),
};
export {
    formatApi,
    authApi,
    partApi,
    departmentApi,
    levelApi,
    unitApi,
    courseApi,
    employeeApi,
    classApi,
    roleApi,
    userApi,
    majorApi,
    hourApi,
    certificateApi,
    permissionApi
};
