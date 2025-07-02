import useBodyClass from '@/hooks/Body';

function AuthLayout({ children }) {
    useBodyClass('hold-transition login-page login-page-bg');

    return <div className="login-box">{children}</div>;
}

export default AuthLayout;
