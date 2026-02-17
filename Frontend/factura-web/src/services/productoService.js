import api from './api';

export const getProducto = async () => {
    const response = await api.get('/Productos'); 
    return response.data;
}

export const createProducto = async (producto) => {
    const response = await api.post('/Productos', producto);
    return response.data;
}

export const deleteProducto = async (codigo) => {
    const response = await api.delete(`/Productos/${codigo}`);
    return response.data;
}
export const editarProducto = async (codigo, productoActualizado) => {
    const response = await api.put(`/Productos/${codigo}`, productoActualizado);
    return response.data;   
 }