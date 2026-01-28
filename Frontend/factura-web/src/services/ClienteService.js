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

export const deleteCliente = async (Id) => {
    const response = await api.delete(`/Clientes/${Id}`);
    return response.data;
}

export const editarCliente = async (Id, clienteActualizado) => {
    const response = await api.put(`/Clientes/${Id}`, clienteActualizado);
    return response.data;
}