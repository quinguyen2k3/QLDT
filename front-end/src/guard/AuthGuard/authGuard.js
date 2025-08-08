import React, { useEffect, useCallback} from 'react'
import { Navigate, useLocation } from 'react-router-dom';
import { useAuth } from '@/contexts';
import Preloader from '@/components/PreLoader';
import { toast } from 'react-toastify';

const AuthGuard = ({ children, requiredPermissions }) => {
    const { authenticated, loading, user } = useAuth();
    const location = useLocation();
    const [hasShownToast, setHasShownToast] = React.useState(false);

    const checkPermissionsAndShowToast = useCallback(() => {
        if (requiredPermissions && !requiredPermissions.every((perm) => user?.permissions?.includes(perm))) {
            if (!hasShownToast) {
                toast.error('Bạn không có quyền truy cập trang này!', {
                    preventDuplicate: true,
                    autoClose: 2000,
                    onOpen: () => setHasShownToast(true),
                });
                return false;
            }
        }
        return true;
    }, [requiredPermissions, user?.permissions, hasShownToast]);

    useEffect(() => {
        if (!requiredPermissions || requiredPermissions.every((perm) => user?.permissions?.includes(perm))) {
            setHasShownToast(false);
        }
    }, [requiredPermissions, user?.permissions]);

    if (loading) {
        return <Preloader />;
    }

    if (!authenticated && location.pathname !== '/login') {
        return <Navigate to="/login" state={{ from: location }} replace />;
    }

    if (!checkPermissionsAndShowToast()) {
        return null;
    }

    return children;
};
export default AuthGuard;
