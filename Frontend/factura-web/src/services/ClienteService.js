import api from './api';

// Verifica que los nombres coincidan exactamente: getClientes y createCliente
export const getClientes = async () => {
    const response = await api.get('/Clientes'); 
    return response.data;
};

export const createCliente = async (cliente) => {
    const response = await api.post('/Clientes', cliente);
    return response.data;
};