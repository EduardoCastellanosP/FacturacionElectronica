import { useEffect, useState } from "react";
// 1. ASEGÚRATE de que estos nombres coincidan con tus archivos en la carpeta services
import { getFacturas, createFactura, handleEditar, handleDelete } from "../services/facturaService";
import { getClientes } from "../services/ClienteService"; 
import { getEmisores } from "../services/emisorService"; 
import "./Facturas.css"; 
import Swal from "sweetalert2";

function Factura() {
  // Estados para datos de la DB
  const [facturas, setFacturas] = useState([]);
  const [listaClientes, setListaClientes] = useState([]);
  const [listaEmisores, setListaEmisores] = useState([]);
  // Estados para la interfaz
  const [busqueda, setBusqueda] = useState("");
  const [editando, setEditando] = useState(false);
  
  // Estado del formulario
  const [nuevaFactura, setNuevaFactura] = useState({
    ClienteId: '',
    EmisorId: '',
    FechaEmision: '',
    TotalFactura: '',
    DocumentoStatusId: '1',
    DetalleFacturas: [] 
  });

  // Estado temporal para el producto
  const [nuevoDetalle, setNuevoDetalle] = useState({
    ProductoId: '',
    Cantidad: 1,
    PrecioUnitario: 0
  });

  // Carga inicial de datos
  const cargarTodo = async () => {
    try {
      // Usamos Promise.all para cargar todo en paralelo
      const [resFacturas, resClientes, resEmisores] = await Promise.all([
        getFacturas(),
        getClientes(),
        getEmisores()
      ]);
      setFacturas(resFacturas || []);
      setListaClientes(resClientes || []);
      setListaEmisores(resEmisores || []);
    } catch (error) {
      console.error("Fallo al cargar datos:", error);
    }
  };

  useEffect(() => {
    cargarTodo();
  }, []);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setNuevaFactura({ ...nuevaFactura, [name]: value });
  };

  const agregarDetalle = () => {
    if (!nuevoDetalle.ProductoId) return;
    setNuevaFactura({
      ...nuevaFactura,
      DetalleFacturas: [...nuevaFactura.DetalleFacturas, nuevoDetalle]
    });
    setNuevoDetalle({ ProductoId: '', Cantidad: 1, PrecioUnitario: 0 });
  };

  const handleSubmit = async (e) => {
  e.preventDefault();

  // VALIDACIÓN: Evita enviar IDs vacíos que rompen el AutoMapper
  if (!nuevaFactura.ClienteId || !nuevaFactura.EmisorId) {
    Swal.fire("Error", "Debes seleccionar un Cliente y un Emisor", "error");
    return;
  }

  try {
    // Preparamos el objeto para que coincida EXACTAMENTE con el DTO
    const datosParaEnviar = {
      ...nuevaFactura,
      TotalFactura: parseFloat(nuevaFactura.TotalFactura), // Asegurar que sea número
      DocumentoStatusId: nuevaFactura.DocumentoStatusId.toString(),
      // Mapeamos los detalles para que coincidan con CrearInvoiceDetalleDto
      DetalleFacturas: nuevaFactura.DetalleFacturas.map(d => ({
        ProductoId: d.ProductoId,
        Cantidad: parseInt(d.Cantidad),
        PrecioUnitario: parseFloat(d.PrecioUnitario || 0)
      }))
    };

    if (editando) {
      await handleEditar(nuevaFactura.Id, datosParaEnviar);
    } else {
      await createFactura(datosParaEnviar);
    }
    
    Swal.fire("¡Éxito!", "Operación completada", "success");
    resetForm();
    cargarTodo();
  } catch (error) {
    console.error("Error detallado:", error.response?.data);
    Swal.fire("Error 500", "Problema de mapeo en el servidor", "error");
  }
};

  const eliminarFactura = async (id) => {
    const result = await Swal.fire({
      title: '¿Eliminar?',
      showCancelButton: true,
      confirmButtonColor: '#d33'
    });
    if (result.isConfirmed) {
      try {
        await handleDelete(id);
        cargarTodo();
      } catch (error) {
        console.error(error);
      }
    }
  };

  const obtenerNombreCliente = (id) => {
    if (!id) return "N/A";
    const cliente = listaClientes.find(c => (c.Id || c.id) === id);
    return cliente ? `${cliente.Nombres} ${cliente.Apellidos}` : `ID: ${id}`;
  };

  return (
    <div className="factura-root">
      <div className="factura-container">
        <header className="factura-header">
          <h2>Gestión de Facturas</h2>
        </header>

        <section className="factura-card">
          <form className="factura-form" onSubmit={handleSubmit}>
            <select name="ClienteId" onChange={handleChange} value={nuevaFactura.ClienteId} required>
              <option value="">-- Cliente --</option>
              {listaClientes.map(c => (
                <option key={c.Id || c.id} value={c.Id || c.id}>{c.Nombres} {c.Apellidos}</option>
              ))}
            </select>

            <select name="EmisorId" onChange={handleChange} value={nuevaFactura.EmisorId} required>
              <option value="">-- Emisor --</option>
              {listaEmisores.map(e => (
                <option key={e.Id || e.id} value={e.Id || e.id}>{e.NombreLegal}</option>
              ))}
            </select>

            <input name="FechaEmision" type="date" onChange={handleChange} value={nuevaFactura.FechaEmision} required />
            <input name="TotalFactura" type="number" placeholder="Total" onChange={handleChange} value={nuevaFactura.TotalFactura} required />
            
            <select name="DocumentoStatusId" onChange={handleChange} value={nuevaFactura.DocumentoStatusId}>
              <option value="1">⏳ Proceso</option>
              <option value="2">✅ Aprobado</option>
              <option value="3">❌ Rechazado</option>
            </select>

            <div className="detalle-seccion">
              <div className="detalle-inputs">
                <input placeholder="ID Prod." value={nuevoDetalle.ProductoId} onChange={(e) => setNuevoDetalle({...nuevoDetalle, ProductoId: e.target.value})} />
                <button type="button" onClick={agregarDetalle} className="btn-add-item">+ Añadir</button>
              </div>
            </div>

            <button type="submit" className="btn-main">{editando ? 'Actualizar' : 'Guardar'}</button>
          </form>
        </section>

        <section className="factura-card">
          <input className="search-input" placeholder="🔍 Buscar..." value={busqueda} onChange={(e) => setBusqueda(e.target.value)} />
          <div className="table-wrapper">
            <table className="factura-table">
              <thead>
                <tr>
                  <th>Fecha</th>
                  <th>Cliente</th>
                  <th>Total</th>
                  <th>Estado</th>
                  <th>Acciones</th>
                </tr>
              </thead>
              <tbody>
                {facturas
                  .filter(f => f.ClienteId?.toString().includes(busqueda))
                  .map((fa) => (
                    <tr key={fa.Id}>
                      <td>{fa.FechaEmision}</td>
                      <td>{obtenerNombreCliente(fa.ClienteId)}</td>
                      <td className="price">${fa.TotalFactura}</td>
                      <td><span className={`badge status-${fa.DocumentoStatusId}`}>{fa.DocumentoStatusId}</span></td>
                      <td>
                        <button onClick={() => { setNuevaFactura(fa); setEditando(true); }} className="btn-edit">✏️</button>
                        <button onClick={() => eliminarFactura(fa.Id)} className="btn-delete">🗑️</button>
                      </td>
                    </tr>
                  ))}
              </tbody>
            </table>
          </div>
        </section>
      </div>
    </div>
  );
}

export default Factura;