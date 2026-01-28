import { Link } from 'react-router-dom';
import './Sidebar.css';

function Sidebar() {
  return (
    <div className="sidebar">
      <Link to="/" className="nav-link">🏠 Inicio</Link>
      <Link to="/emisores" className="nav-link">🏢 Emisores</Link>
      <Link to="/clientes" className="nav-link">👥 Clientes</Link>
      <Link to="/facturas" className="nav-link">📄 Facturas</Link>
    </div>
  );
}

export default Sidebar;