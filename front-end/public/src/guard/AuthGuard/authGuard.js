import { Navigate, useLocation } from "react-router-dom";
import { useAuth } from "@/contexts";
import Preloader from "@/components/PreLoader"

const AuthGuard = ({ children }) => {
    const { authenticated, loading } = useAuth();
    const location = useLocation();

    if (loading) 
        return <Preloader />;

    if (!authenticated) {
        return <Navigate to="/login" state={{ from: location }} replace />;
    }

    return children;
};
export default AuthGuard;
