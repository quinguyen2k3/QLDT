import React, { useEffect, useCallback, useState } from 'react';
import { Navigate, useLocation } from 'react-router-dom';
import { useAuth } from '@/contexts';
import Preloader from '@/components/PreLoader';
import { toast } from 'react-toastify';

const AuthGuard = ({ children, requiredPermissions }) => {
    const { authenticated, loading, user } = useAuth();
    const location = useLocation();
    const [hasShownToast, setHasShownToast] = useState(false);
    const [hasPermission, setHasPermission] = useState(null);

    const checkPermissionsAndShowToast = useCallback(() => {
        if (!user?.permissions) {
            return null;
        }
        if (requiredPermissions && !requiredPermissions.every((perm) => user.permissions.includes(perm))) {
            if (!hasShownToast) {
                toast.error('Bạn không có quyền truy cập trang này!', {
                    preventDuplicate: true,
                    autoClose: 2000,
                    onOpen: () => setHasShownToast(true),
                });
            }
            return false;
        }
        return true;
    }, [requiredPermissions, user?.permissions, hasShownToast]);

    useEffect(() => {
        if (!loading) {
            const permissionResult = checkPermissionsAndShowToast();
            setHasPermission(permissionResult);
            if (permissionResult) {
                setHasShownToast(false);
            }
        }
    }, [checkPermissionsAndShowToast, loading]);

    if (loading || hasPermission === null) {
        return <Preloader />;
    }

    if (!authenticated && location.pathname !== '/login') {
        return <Navigate to="/login" state={{ from: location }} replace />;
    }

    if (hasPermission === false) {
        return <Navigate to="/not-permitted" replace />;
    }

    return children;
};

export default AuthGuard;