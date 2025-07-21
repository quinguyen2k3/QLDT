import {
    ChangePassword,
    UserList,
    UserForm,
    Home,
    PartList,
    PartForm,
    DepartmentList,
    DepartmentForm,
    EUnitList,
    EUnitForm,
    TrainingTypeList,
    FormatForm,
    EmployeeList,
    EmployeeDetail,
    EmployeeForm,
    CourseForm,
    CourseList,
    ELevelList,
    ELevelForm,
    Login,
    Page404,
    ClassForm,
    ClassList
} from '@/pages';

import { AuthLayout } from '@/layout';

//public Routes
const publicRoutes = [
    { path: '/', component: Home },
    { path: '/home', component: Home },
    { path: '/login', component: Login, layout: AuthLayout },
    { path: '*', component: Page404 },
    { path: '/class/update/:id', component: ClassForm },
    { path: '/class/create', component: ClassForm },
];

//private Routes
const privateRoutes = [
    { path: '/change-password', component: ChangePassword },
    { path: '/users/list', component: UserList },
    { path: '/users/create', component: UserForm },
    { path: '/user/update/:id', component: UserForm },
    { path: '/parts/list', component: PartList },
    { path: '/part/create', component: PartForm },
    { path: '/part/update/:id', component: PartForm },
    { path: '/deparments/list', component: DepartmentList },
    { path: '/department/create', component: DepartmentForm },
    { path: '/department/update/:id', component: DepartmentForm },
    { path: '/eunits/list', component: EUnitList },
    { path: '/eunit/create', component: EUnitForm },
    { path: '/eunit/update/:id', component: EUnitForm },
    { path: '/training-types/list', component: TrainingTypeList },
    { path: '/employees/list', component: EmployeeList },
    { path: '/employee/detail', component: EmployeeDetail },
    { path: '/employee/create', component: EmployeeForm },
    { path: '/employee/update/:id', component: EmployeeForm },
    { path: '/course/create', component: CourseForm },
    { path: '/course/update/:id', component: CourseForm },
    { path: '/courses/list', component: CourseList },
    { path: '/format/create', component: FormatForm },
    { path: '/format/update/:id', component: FormatForm },
    { path: '/elevels/list', component: ELevelList },
    { path: '/elevel/create', component: ELevelForm },
    { path: '/elevel/update/:id', component: ELevelForm },
    { path: '/class/list/longterm', component: ClassList },
    { path: '/class/list/shortterm', component: ClassList}
];

export { publicRoutes, privateRoutes };
