import { useState } from 'react';
import { createEmisor } from '../services/emisorService';

function FormularioEmisor({ onEmisorCreado }) {
  const [formData, setFormData] = useState({
    nit: '',
    razonSocial: '',
    direccion: '',
    telefono: '',
    email: ''
  });

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    await createEmisor(formData);
    alert("Emisor guardado con éxito");
    onEmisorCreado(); // Esto refrescará la lista automáticamente
    setFormData({ nit: '', razonSocial: '', direccion: '', telefono: '', email: '' }); // Limpiar formulario
  };

  return (
    <form onSubmit={handleSubmit} style={{ marginBottom: '20px', padding: '15px', border: '1px solid #ccc' }}>
      <h3>Nuevo Emisor</h3>
      <input className="form-input" name="nit" placeholder="NIT" onChange={handleChange} value={formData.nit} required />
      <input className="form-input" name="razonSocial" placeholder="Razón Social" onChange={handleChange} value={formData.razonSocial} required />
      <input className="form-input" name="email" placeholder="Email" onChange={handleChange} value={formData.email} required />
      <button type="submit">Guardar Emisor</button>
    </form>
  );
}

export default FormularioEmisor;