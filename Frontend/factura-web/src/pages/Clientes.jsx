import { useEffect, useState } from 'react';
import { getClientes, createCliente } from '../services/ClienteService';
import './Clientes.css';

function Clientes() {
  const [clientes, setClientes] = useState([]);
  const [nuevoCliente, setNuevoCliente] = useState({ nit: '', nombre: '', email: '' });

  const cargar = async () => {
    const data = await getClientes();
    setClientes(data);
  };

  useEffect(() => { cargar(); }, []);

  const handleSubmit = async (e) => {
    e.preventDefault();
    await createCliente(nuevoCliente);
    setNuevoCliente({ nit: '', nombre: '', email: '' });
    cargar();
  };

  return (
  <div className="container">
    <h2>Administración de Clientes</h2>
    
    <div className="content-grid">
      {/* SECCIÓN DEL FORMULARIO */}
      <div className="form-card">
        <h3>Nuevo Cliente</h3>
        <form onSubmit={handleSubmit} className="form-group">
          <input 
            placeholder="NIT / Cédula" 
            onChange={e => setNuevoCliente({...nuevoCliente, nit: e.target.value})} 
            value={nuevoCliente.nit} 
          />
          <input 
            placeholder="Nombre Completo" 
            onChange={e => setNuevoCliente({...nuevoCliente, nombre: e.target.value})} 
            value={nuevoCliente.nombre} 
          />
          <input 
            placeholder="Correo Electrónico" 
            onChange={e => setNuevoCliente({...nuevoCliente, email: e.target.value})} 
            value={nuevoCliente.email} 
          />
          <button type="submit">Guardar Cliente</button>
        </form>
      </div>

      {/* SECCIÓN DE LA TABLA */}
      <div className="table-section">
        <table>
          <thead>
            <tr>
              <th>NIT</th>
              <th>Nombre</th>
              <th>Email</th>
            </tr>
          </thead>
          <tbody>
            {clientes.map(c => (
              <tr key={c.id}>
                <td>{c.nit}</td>
                <td>{c.nombre}</td>
                <td>{c.email}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  </div>
);}

export default Clientes;