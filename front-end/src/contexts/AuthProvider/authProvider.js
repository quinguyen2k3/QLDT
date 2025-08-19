import { createContext, useContext, useEffect, useState } from 'react';
import { getAccessToken, clearTokens, setOnLoginCallback } from '@/service/authService';
import { jwtDecode } from 'jwt-decode';
import { permissionApi } from '@/service/apis';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [authenticated, setAuthenticated] = useState(false);
    const [loading, setLoading] = useState(true);
    const [user, setUser] = useState(null);

    const checkToken = async () => {
        const token = getAccessToken();
        if (!token) {
            setLoading(false);
            return false;
        }
        try {
            const decoded = jwtDecode(token);
            const currentTime = Date.now() / 1000;
            if (decoded.exp > currentTime) {
                const permissionsResponse = await permissionApi.getAllByUser();
                const permissions = permissionsResponse.data.data?.map((item) => item.name) || [];
                const userData = {
                    id: decoded.id,
                    username: decoded.username,
                    name: decoded.name,
                    emp: decoded.emp,
                    role: decoded.role,
                    permissions: permissions,
                };
                setUser(userData);
                setLoading(false);
                return true;
            }
            setLoading(false);
            return false;
        } catch (error) {
            setLoading(false);
            return false;
        }
    };

    useEffect(() => {
        const initializeAuth = async () => {
            const isAuthenticated = await checkToken();
            setAuthenticated(isAuthenticated);
        };
        initializeAuth();
        setOnLoginCallback(() => () => setAuthenticated(true));
    }, []);

    const login = async () => {
        await checkToken();
        setAuthenticated(true);
    };

    const logout = () => {
        clearTokens();
        setAuthenticated(false);
        setUser(null);
        setLoading(false);
    };

    return (
        <AuthContext.Provider value={{ user, authenticated, login, logout, loading }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => useContext(AuthContext);