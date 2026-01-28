import { useState } from 'react'; // <--- ¡ESTA ES LA LÍNEA QUE FALTA!
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
// ... tus otros imports (Header, Dashboard, etc.)
import Header from './components/Header';
import Dashboard from './pages/Dashboard';
import Clientes from './pages/Clientes';
import Emisores from './pages/Emisores';
import ModalFactura from './components/ModalFactura';

function App() {
  const [isModalOpen, setIsModalOpen] = useState(false);

  return (
    // 1. EL ROUTER DEBE ENVOLVER TODO LO QUE USE NAVEGACIÓN
    <Router> 
      <div className="app-wrapper">
        
        {/* 2. Ahora el Header sí puede usar <Link> porque está dentro del Router */}
        <Header onOpenModal={() => setIsModalOpen(true)} />

        <div className="main-content">
          <Routes>
            <Route path="/" element={<Dashboard />} />
            <Route path="/clientes" element={<Clientes />} />
            <Route path="/emisores" element={<Emisores />} />
          </Routes>
        </div>

        <ModalFactura 
          isOpen={isModalOpen} 
          onClose={() => setIsModalOpen(false)} 
        />
      </div>
    </Router>
  );
}

export default App;