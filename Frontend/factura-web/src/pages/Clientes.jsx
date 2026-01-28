import { useEffect, useState } from 'react';
import { getClientes, createCliente } from '../services/ClienteService';

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
      
      <form onSubmit={handleSubmit} className="form-card">
        <div className="form-group">
          <input placeholder="NIT" onChange={e => setNuevoCliente({...nuevoCliente, nit: e.target.value})} value={nuevoCliente.nit} />
          <input placeholder="Nombre" onChange={e => setNuevoCliente({...nuevoCliente, nombre: e.target.value})} value={nuevoCliente.nombre} />
          <input placeholder="Email" onChange={e => setNuevoCliente({...nuevoCliente, email: e.target.value})} value={nuevoCliente.email} />
        </div>
        <button type="submit">Guardar Cliente</button>
      </form>

      <table>
        <thead>
          <tr><th>NIT</th><th>Nombre</th><th>Email</th></tr>
        </thead>
        <tbody>
          {clientes.map(c => (
            <tr key={c.id}><td>{c.nit}</td><td>{c.nombre}</td><td>{c.email}</td></tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default Clientes;