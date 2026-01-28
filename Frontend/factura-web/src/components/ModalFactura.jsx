import './ModalFactura.css';

function ModalFactura({ isOpen, onClose }) {
  // Si isOpen es false, no dibujamos nada en la pantalla
  if (!isOpen) return null;

  return (
    <div className="modal-overlay" onClick={onClose}>
      {/* stopPropagation evita que el modal se cierre al hacer clic dentro del cuadro blanco */}
      <div className="modal-content" onClick={e => e.stopPropagation()}>
        <div className="modal-header">
          <h3>🚀 Nueva Factura</h3>
          <button className="close-btn" onClick={onClose}>&times;</button>
        </div>
        
        <form className="modal-body">
          <div className="form-group">
            <label>Cliente</label>
            <select className="modal-input">
              <option>Selecciona un cliente...</option>
            </select>
          </div>

          <div className="form-group">
            <label>Fecha</label>
            <input type="date" className="modal-input" defaultValue={new Date().toISOString().split('T')[0]} />
          </div>

          <div className="form-group">
            <label>Monto Total</label>
            <input type="number" className="modal-input" placeholder="0.00" />
          </div>
          
          <div className="modal-actions">
            <button type="button" className="btn-cancel" onClick={onClose}>Cancelar</button>
            <button type="submit" className="btn-submit">Crear Factura</button>
          </div>
        </form>
      </div>
    </div>
  );
}

export default ModalFactura;