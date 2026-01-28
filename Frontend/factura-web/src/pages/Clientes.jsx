import { useEffect, useState } from 'react';
// 1. IMPORTANTE: Añadir editarCliente al import
import { getClientes, createCliente, deleteCliente, editarCliente } from '../services/ClienteService';
import './Clientes.css';
import Swal from 'sweetalert2';

function Clientes() {
  const [clientes, setClientes] = useState([]);
  // 2. Estado para saber si estamos editando
  const [editando, setEditando] = useState(false);
  
  const [nuevoCliente, setNuevoCliente] = useState({ 
    id: '', // Agregamos id para el modo edición
    Nit: '', 
    Nombres: '', 
    Apellidos: '', 
    Direccion: '', 
    Telefono: '', 
    Email: '' 
  });

  const cargar = async () => {
    try {
      const data = await getClientes();
      setClientes(data);
    } catch (error) {
      console.error("Error al cargar lista", error);
    }
  };

  useEffect(() => { cargar(); }, []);

  // --- SUBMIT ACTUALIZADO CON LÓGICA DOBLE ---
  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (editando) {
        // MODO EDICIÓN
        await editarCliente(nuevoCliente.id, nuevoCliente);
        Swal.fire({
          title: '¡Actualizado!',
          text: 'Cliente modificado correctamente.',
          icon: 'success',
          confirmButtonColor: '#3498db',
          timer: 1500
        });
      } else {
        // MODO CREACIÓN
        await createCliente(nuevoCliente);
        Swal.fire({
          title: '¡Éxito!',
          text: 'Cliente guardado correctamente.',
          icon: 'success',
          confirmButtonColor: '#3498db',
          timer: 1500
        });
      }

      // --- LIMPIEZA COMÚN ---
      setNuevoCliente({ id: '', Nit: '', Nombres: '', Apellidos: '', Direccion: '', Telefono: '', Email: '' });
      setEditando(false);
      cargar();

    } catch (error) {
      console.error("Error detallado:", error.response?.data);
      Swal.fire({
        title: 'Error',
        text: editando ? 'No se pudo actualizar el cliente.' : 'Faltan datos o el Email ya existe.',
        icon: 'error',
        confirmButtonColor: '#e74c3c'
      });
    }
  };

  const handleEliminar = (id) => {
    Swal.fire({
      title: '¿Estás seguro?',
      text: "¡No podrás revertir esto!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Sí, eliminarlo',
      cancelButtonText: 'Cancelar'
    }).then(async (result) => {
      if (result.isConfirmed) {
        try {
          await deleteCliente(id);
          Swal.fire('¡Eliminado!', 'El cliente ha sido borrado.', 'success');
          cargar(); 
        } catch (error) {
          Swal.fire('Error', 'No se pudo eliminar el cliente', 'error');
        }
      }
    });
  };

  const handleditar = (c) => {
    setNuevoCliente({
      id: c.id, // Guardamos el id real para el PUT
      Nit: c.nit,
      Nombres: c.nombres,
      Apellidos: c.apellidos,
      Direccion: c.direccion,
      Telefono: c.telefono,
      Email: c.email
    });
    setEditando(true);
  };

  return (
    <div className="container">
      <h2>Administración de Clientes</h2>
      <div className="content-grid">
        <div className="form-card">
          {/* El título cambia según el modo */}
          <h3>{editando ? 'Editar Cliente' : 'Nuevo Cliente'}</h3>
          <form onSubmit={handleSubmit} className="form-group">
            <input 
              placeholder="NIT / Cédula" 
              value={nuevoCliente.Nitit}
              onChange={e => setNuevoCliente({...nuevoCliente, Nit: e.target.value})} 
            />
            <input 
              placeholder="Nombres"
              value={nuevoCliente.Nombres} 
              onChange={e => setNuevoCliente({...nuevoCliente, Nombres: e.target.value})} 
            />
            <input 
              placeholder="Apellidos"
              value={nuevoCliente.Apellidos} 
              onChange={e => setNuevoCliente({...nuevoCliente, Apellidos: e.target.value})} 
            />
            <input 
              placeholder="Dirección"
              value={nuevoCliente.Direccion} 
              onChange={e => setNuevoCliente({...nuevoCliente, Direccion: e.target.value})} 
            />
            <input 
              placeholder="Teléfono" 
              value={nuevoCliente.Telefono}
              onChange={e => setNuevoCliente({...nuevoCliente, Telefono: e.target.value})} 
            />
            <input 
              type="email"
              placeholder="Correo Electrónico" 
              value={nuevoCliente.Email}
              onChange={e => setNuevoCliente({...nuevoCliente, Email: e.target.value})} 
            />
            
            {/* El color del botón y el texto cambian según el modo */}
            <button 
              type="submit" 
              style={{ backgroundColor: editando ? '#3498db' : '#2ecc71' }}
            >
              {editando ? 'Actualizar Cliente' : 'Guardar Cliente'}
            </button>

            {/* Botón para salir del modo edición sin guardar */}
            {editando && (
              <button 
                type="button" 
                onClick={() => { setEditando(false); setNuevoCliente({ id: '', Nit: '', Nombres: '', Apellidos: '', Direccion: '', Telefono: '', Email: '' }); }}
                style={{ marginTop: '10px', backgroundColor: '#95a5a6' }}
              >
                Cancelar Edición
              </button>
            )}
          </form>
        </div>

        <div className="table-section">
          <table>
            <thead>
              <tr>
                <th>Nit</th>
                <th>Nombres</th>
                <th>Apellidos</th>
                <th>Dirección</th>
                <th>Teléfono</th>
                <th>Email</th>
                <th>Acciones</th>
              </tr>
            </thead>
            <tbody>
              {clientes.map(c => (
                <tr key={c.id}>
                  <td>{c.nit}</td>
                  <td>{c.nombres}</td>
                  <td>{c.apellidos}</td>
                  <td>{c.direccion}</td>
                  <td>{c.telefono}</td>
                  <td>{c.email}</td>
                  <td>
                    <button className="btn-editar" type="button" onClick={() => handleditar(c)}>Editar</button>
                    <button className="btn-delete" type="button" onClick={() => handleEliminar(c.id)}>Eliminar</button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
}

export default Clientes;