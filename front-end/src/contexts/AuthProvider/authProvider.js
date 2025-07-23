// src/contexts/AuthContext.jsx
import { createContext, useContext, useEffect, useState } from "react";
import { getAccessToken, clearTokens, setOnLoginCallback } from "@/service/authService";
import { jwtDecode } from "jwt-decode";

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [authenticated, setAuthenticated] = useState(false);
    const [loading, setLoading] = useState(true);
    const [user, setUser] = useState(null);


    const checkToken = () => {
        const token = getAccessToken();
        if (!token) return false;
        try {
            const decoded = jwtDecode(token);
            const currentTime = Date.now() / 1000;
            if (decoded.exp > currentTime) {
                setUser({
                    name: decoded.name,
                    role: decoded.role
                });
                return true;
            }
            return false;
        } catch {
            return false;
        }
    };

    useEffect(() => {
        // Kiểm tra token khi app khởi chạy
        setAuthenticated(checkToken());
        setLoading(false);

        // Cho phép interceptor trigger khi refresh token thành công
        setOnLoginCallback(() => () => setAuthenticated(true));
    }, []);

    const login = () => {
        setAuthenticated(true);
        checkToken();
    };

    const logout = () => {
        clearTokens();
        setAuthenticated(false);
        setUser(null);
    };

    return (
        <AuthContext.Provider value={{ user, authenticated, login, logout, loading }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => useContext(AuthContext);
