import api from './api';

// Asegúrate de que diga "export const getDashboardStats"
export const getDashboardStats = async () => {
    try {
        const response = await api.get('/dashboard');
        return response.data;
    } catch (error) {
        console.error("Error en el servicio dashboard:", error);
        throw error;
    }
};