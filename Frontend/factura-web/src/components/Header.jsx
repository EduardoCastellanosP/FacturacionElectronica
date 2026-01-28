import { Link } from 'react-router-dom'; // 1. IMPORTANTE: Importar Link
import './Header.css';

function Header() {
  return (
    <header className="main-header">
      <div className="header-left">
        {/* Envolvemos el logo en un Link para que al hacer clic te lleve al Dashboard */}
        <Link to="/" className="logo-link" style={{ textDecoration: 'none', display: 'flex', alignItems: 'center' }}>
          <span className="logo-icon">📊</span>
          <h2 className="brand-name">Nexo<span>Factura</span></h2>
        </Link>
      </div>

      {/* 2. NUEVA SECCIÓN: Menú de navegación central */}
      <nav className="header-nav">
        <Link to="/" className="nav-item">Inicio</Link>
        <Link to="/emisores" className="nav-item">Emisores</Link>
        <Link to="/clientes" className="nav-item">Clientes</Link>
      </nav>

      <div className="header-center">
        <div className="search-bar">
          <input type="text" placeholder="Buscar factura o cliente..." />
        </div>
      </div>

      <div className="header-right">
        <button className="btn-new-invoice">+ Nueva Factura</button>
        <div className="user-profile">
          <div className="user-info">
            <span className="user-name">Admin Usuario</span>
            <span className="user-role">Administrador</span>
          </div>
          <div className="user-avatar">AD</div>
        </div>
      </div>
    </header>
  );
}

export default Header;