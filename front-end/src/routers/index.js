import ChangePassword from '@/pages/ChangPassword';
import UserList from '@/pages/UserList';
import UserForm from '@/pages/UserForm';
import Home from '@/pages/Home';
import DevisionList from '@/pages/DevisionList';
import DepartmentList from '@/pages/DepartmentList';
import EUnitList from '@/pages/EUnitList';
import TrainingTypeList from '@/pages/TrainingFormatList';
import EmployeeList from '@/pages/EmployeeList';
import EmployeeDetail from '@/pages/EmployeeDetail';
import EmployeeForm from '@/pages/EmployeeForm';
import CourseForm from '@/pages/CourseForm';
import CourseList from '@/pages/CourseList';
import Login from '@/pages/Login';
import FormatForm from '@/pages/TrainingFormatForm';

import { AuthLayout } from '@/layout';

//public Routes
const publicRoutes = [
    { path: '/home', component: Home },
    { path: '/change-password', component: ChangePassword },
    { path: '/users/list', component: UserList },
    { path: '/users/create', component: UserForm },
    { path: '/user/update', component: UserForm },
    { path: '/devisions/list', component: DevisionList },
    { path: '/deparments/list', component: DepartmentList },
    { path: '/eunits/list', component: EUnitList },
    { path: '/training-types/list', component: TrainingTypeList },
    { path: '/employees/list', component: EmployeeList },
    { path: '/employee/detail', component: EmployeeDetail },
    { path: '/employee/create', component: EmployeeForm },
    { path: '/employee/update', component: EmployeeForm },
    { path: '/course/create', component: CourseForm },
    { path: '/course/update', component: CourseForm },
    { path: '/courses/list', component: CourseList },
    { path: '/login', component: Login, layout: AuthLayout },
    { path: '/format/create', component: FormatForm },
    { path: '/format/update/:id', component: FormatForm },
];

//private Routes
const privateRoutes = [];

export { publicRoutes, privateRoutes };
