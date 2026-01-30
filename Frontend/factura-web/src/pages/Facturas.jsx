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
    DocumentoStatusId: 'aca85f64-5717-4562-b3fc-2c963f66afa6',
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
    // 1. Probamos Clientes por separado
    const dataClientes = await getClientes();
    console.log("Clientes OK:", dataClientes);
    setListaClientes(dataClientes || []);

    // 2. Probamos Emisores por separado
    const dataEmisores = await getEmisores();
    console.log("Emisores OK:", dataEmisores);
    setListaEmisores(dataEmisores || []);

    // 3. Probamos Facturas por separado
    const dataFacturas = await getFacturas();
    console.log("Facturas OK:", dataFacturas);
    setFacturas(dataFacturas || []);

  } catch (error) {
    console.error("Error específico detectado:");
    console.error("URL llamada:", error.config?.url);
    console.error("Status:", error.response?.status);
    console.error("Data del error:", error.response?.data);
  }
};
useEffect(() => {
  cargarTodo();
  
}, []); // Se ejecuta una sola vez al cargar el componente
  const resetForm = () => {
  setNuevaFactura({
    ClienteId: '',
    EmisorId: '',
    FechaEmision: '',
    TotalFactura: '',
    // Usamos el ID de "En Proceso" que ya tienes en la DB
    DocumentoStatusId: 'aca85f64-5717-4562-b3fc-2c963f66afa6',
    DetalleFacturas: [] 
  });
  setNuevoDetalle({ ProductoId: '', Cantidad: 1, PrecioUnitario: 0 });
  setEditando(false);
};


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
      DocumentoStatusId: nuevaFactura.documentoStatusId || "aca85f64-5717-4562-b3fc-2c963f66afa6",
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
    console.error("Error capturado:", error);
    // Mejora la lectura del error
    const msg = error.response?.data?.message || "Error interno del servidor";
    Swal.fire("Error", msg, "error");
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

           <select 
                name="ClienteId" 
                onChange={handleChange} 
                value={nuevaFactura.ClienteId} 
                
              >
                <option value="">Seleccione el Cliente</option>
                {listaClientes.map(c => (
                  <option key={c.id || c.Id} value={c.id || c.Id}>
                    {/* Probamos ambas versiones para no fallar */}
                    {(c.nombres || c.Nombres || "Sin Nombre")} {(c.apellidos || c.Apellidos || "")}
                  </option>
                ))}
            </select>

            <select name="EmisorId" onChange={handleChange} value={nuevaFactura.EmisorId}>
  <option value="">Seleccione el Emisor</option>
  {listaEmisores.map(e => (
    <option key={e.Id || e.id} value={e.Id || e.id}>
      {/* Usamos RazonSocial que es el nombre real en tu DB */}
      {e.RazonSocial || e.razonSocial || "Emisor sin nombre"}
    </option>
  ))}
</select>

            <input name="FechaEmision" type="date" onChange={handleChange} value={nuevaFactura.FechaEmision} required />
            <input name="TotalFactura" type="number" placeholder="Total" onChange={handleChange} value={nuevaFactura.TotalFactura} required />
            
            <select name="DocumentoStatusId" onChange={handleChange} value={nuevaFactura.DocumentoStatusId}>
            <option value="">-- Selecciona Estado --</option>
            {/* Reemplaza estos IDs con los de tu base de datos */}
            <option value="aca85f64-5717-4562-b3fc-2c963f66afa6">⏳ En Proceso</option>
            <option value="bba85f64-5717-4562-b3fc-2c963f66afa6">✅ Aprobado</option>
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