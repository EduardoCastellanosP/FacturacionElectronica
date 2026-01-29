import { useEffect, useState } from 'react';
import { getEmisores, createEmisor,deleteEmisor,editarEmisor } from '../services/emisorService';
import './Emisores.css'; // Ahora crearemos este estilo
import Swal from 'sweetalert2';

function Emisores() {
  const [emisores, setEmisores] = useState([]);
  const [editando, setEditando] = useState(false);
  const [nuevoEmisor, setNuevoEmisor] = useState({
    Id: '',
    Nit: '',
    RazonSocial: '',
    Direccion: '',
    Telefono: '',
    Email: ''
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
  // Desestructuramos para mayor claridad
  const { name, value } = e.target;
  
  setNuevoEmisor({ 
    ...nuevoEmisor, // Mantiene los otros campos (importante)
    [name]: value   // Actualiza solo el que estás escribiendo
  });
};

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (editando) {
        // MODO EDICIÓN
        await editarEmisor(nuevoEmisor.Id, nuevoEmisor);
        Swal.fire({
          title: '¡Actualizado!',
          text: 'Emisor modificado correctamente.',
          icon: 'success',
          confirmButtonColor: '#3498db',
          timer: 1500
        });
      } else {
        // MODO CREACIÓN
        await createEmisor(nuevoEmisor);
        Swal.fire({
          title: '¡Éxito!',
          text: 'Emisor guardado correctamente.',
          icon: 'success',
          confirmButtonColor: '#3498db',
          timer: 1500
        });
      }

      // --- LIMPIEZA COMÚN ---
      setNuevoEmisor({ Id: '', Nit: '', RazonSocial: '', Direccion: '', Telefono: '', Email: '' });
      setEditando(false);
      cargar();
    } catch (error) {
      console.error("Error al guardar emisor:", error);
      Swal.fire({
        title: 'Error',
        text: editando ? 'Hubo un problema al actualizar el emisor.' : 'Hubo un problema al guardar el emisor.',
        icon: 'error',
        confirmButtonColor: '#e74c3c',
        timer: 1500
      });
    }
  };

  const handleEditar = (em) => {
    setNuevoEmisor({
      Id: em.id,
      Nit: em.nit,
      RazonSocial: em.razonSocial,
      Direccion: em.direccion,
      Telefono: em.telefono,
      Email: em.email
    });
    setEditando(true);
  }

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
            await deleteEmisor(id);
            Swal.fire('¡Eliminado!', 'El emisor ha sido borrado.', 'success');
            cargar();
          } catch (error) {
          Swal.fire('Error', 'No se pudo eliminar el Emisor', 'error');
          }
        } 
      });
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
            <input name="Nit" placeholder="NIT" onChange={handleChange} value={nuevoEmisor.Nit} required />
            <input name="RazonSocial" placeholder="Razón Social" onChange={handleChange} value={nuevoEmisor.RazonSocial} required />
            <input name="Direccion" placeholder="Dirección" onChange={handleChange} value={nuevoEmisor.Direccion} />
            <input name="Telefono" placeholder="Teléfono" onChange={handleChange} value={nuevoEmisor.Telefono} />
            <input name="Email" placeholder="Email" type="email" onChange={handleChange} value={nuevoEmisor.Email} required />
            <button type="submit" className="btn-save">{editando ? 'Actualizar Empresa' : 'Guardar Emisor'}</button>

            {/* Botón para salir del modo edición sin guardar */}
            {editando && (
              <button type="button" onClick={() => { setEditando(false); setNuevoEmisor({ Id: '', Nit: '', RazonSocial: '', Direccion: '', Telefono: '', Email: '' }); }}
                style={{ marginTop: '10px', backgroundColor: '#95a5a6' }}
              >
                Cancelar Edición
              </button>
            )}
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
                <th>Dirección</th>
                <th>Teléfono</th>
                <th>Email</th>
              </tr>
            </thead>
            <tbody>
              {emisores.map((em) => (
                <tr key={em.id}>
                  <td>{em.nit}</td>
                  <td><strong>{em.razonSocial}</strong></td>
                  <td>{em.direccion}</td>
                  <td>{em.telefono}</td>
                  <td>{em.email}</td>
                  <td>
                    <button className="btn-editar" type="button" onClick={() => handleEditar(em)}>Editar</button>
                    <button className="btn-delete" type="button" onClick={() => handleEliminar(em.id)}>Eliminar</button>
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

export default Emisores;