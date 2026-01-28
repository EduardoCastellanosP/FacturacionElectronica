import api from './api';

export const getEmisores = async () => {
  const response = await api.get('/Emisor');
  return response.data;
};

// Función para guardar un nuevo emisor
export const createEmisor = async (nuevoEmisor) => {
    try {
        const response = await api.post('/Emisor', nuevoEmisor);
        return response.data;
    } catch (error) {
        console.error("Error al crear emisor:", error);
        throw error;
    }
};