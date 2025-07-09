// src/contexts/AuthContext.jsx
import { createContext, useContext, useEffect, useState } from "react";
import { getAccessToken, clearTokens, setOnLoginCallback } from "@/service/authService";
import { jwtDecode } from "jwt-decode";

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [authenticated, setAuthenticated] = useState(false);
    const [loading, setLoading] = useState(true);

    const checkToken = () => {
        const token = getAccessToken();
        if (!token) return false;
        try {
            const decoded = jwtDecode(token);
            const currentTime = Date.now() / 1000;
            return decoded.exp > currentTime;
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

    const login = () => setAuthenticated(true);

    const logout = () => {
        clearTokens();
        setAuthenticated(false);
    };

    return (
        <AuthContext.Provider value={{ authenticated, login, logout, loading }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => useContext(AuthContext);
