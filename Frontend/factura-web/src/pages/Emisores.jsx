import { useEffect, useState } from 'react';
import { getEmisores, createEmisor } from '../services/emisorService';
import './Emisores.css'; // Ahora crearemos este estilo

function Emisores() {
  const [emisores, setEmisores] = useState([]);
  const [nuevoEmisor, setNuevoEmisor] = useState({
    nit: '',
    razonSocial: '',
    direccion: '',
    telefono: '',
    email: ''
  });

  const cargar = async () => {
    try {
      const data = await getEmisores();
      setEmisores(data);
    } catch (error) {
      console.error("Error al cargar emisores:", error);
    }
  };

  useEffect(() => { cargar(); }, []);

  const handleChange = (e) => {
    setNuevoEmisor({ ...nuevoEmisor, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    await createEmisor(nuevoEmisor);
    setNuevoEmisor({ nit: '', razonSocial: '', direccion: '', telefono: '', email: '' });
    cargar();
  };

  return (
    <div className="page-container">
      <div className="page-header">
        <h2>Configuración de Emisores</h2>
        <p>Administra los datos de las empresas que emitirán facturas.</p>
      </div>

      <div className="content-grid">
        {/* Formulario */}
        <div className="card form-section">
          <h3>Registrar Empresa</h3>
          <form onSubmit={handleSubmit}>
            <input name="nit" placeholder="NIT" onChange={handleChange} value={nuevoEmisor.nit} required />
            <input name="razonSocial" placeholder="Razón Social" onChange={handleChange} value={nuevoEmisor.razonSocial} required />
            <input name="direccion" placeholder="Dirección" onChange={handleChange} value={nuevoEmisor.direccion} />
            <input name="telefono" placeholder="Teléfono" onChange={handleChange} value={nuevoEmisor.telefono} />
            <input name="email" placeholder="Email" type="email" onChange={handleChange} value={nuevoEmisor.email} required />
            <button type="submit" className="btn-save">Guardar Emisor</button>
          </form>
        </div>

        {/* Tabla */}
        <div className="card table-section">
          <h3>Empresas Registradas</h3>
          <table className="custom-table">
            <thead>
              <tr>
                <th>NIT</th>
                <th>Razón Social</th>
                <th>Email</th>
              </tr>
            </thead>
            <tbody>
              {emisores.map((em) => (
                <tr key={em.id}>
                  <td>{em.nit}</td>
                  <td><strong>{em.razonSocial}</strong></td>
                  <td>{em.email}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
}

export default Emisores;