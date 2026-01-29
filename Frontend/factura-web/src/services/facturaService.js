import api from './api';

// GET: /api/Invoice
export const getFacturas = async () => {
  const response = await api.get('/Invoice');
  return response.data;
};

// POST: /api/Invoice
export const createFactura = async (nuevaFactura) => {
    try {
        const response = await api.post('/Invoice', nuevaFactura);
        return response.data;
    } catch (error) {
        console.error("Error al crear invoice:", error);
        throw error;
    }
}

// DELETE: /api/Invoice/{id}
export const handleDelete = async (Id) => {
    const response = await api.delete(`/Invoice/${Id}`);
    return response.data;
}

// PUT: /api/Invoice/{id}
export const handleEditar = async (Id, facturaActualizada) => {
    const response = await api.put(`/Invoice/${Id}`, facturaActualizada);
    return response.data;
}