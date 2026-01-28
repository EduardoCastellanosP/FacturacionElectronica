import { useEffect, useState } from 'react';
// Verifica que esta ruta sea correcta según tu estructura de carpetas
import { getDashboardStats } from '../services/dashboardService'; 
import { BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, ResponsiveContainer } from 'recharts';
import './Dashboard.css';

function Dashboard() {
  const [stats, setStats] = useState({
    ventasTotales: 0,
    facturasEmitidas: 0,
    clientesNuevos: 0,
    graficoVentas: []
  });

  useEffect(() => {
    const fetchStats = async () => {
      try {
        const data = await getDashboardStats();
        // Solo actualizamos si la data existe
        if (data) {
          setStats(data);
        }
      } catch (error) {
        console.error("Error al cargar estadísticas:", error);
      }
    };
    fetchStats();
  }, []);

  // Esto evita que el gráfico falle si graficoVentas aún no llega
  if (!stats.graficoVentas) return null;

  return (
    <div className="dashboard-container">
      <h1 className="dashboard-title">Resumen del Negocio</h1>
      
      <div className="kpi-grid">
        <div className="kpi-card">
          <h4>Ventas Totales</h4>
          <p className="kpi-value">${stats.ventasTotales?.toLocaleString()}</p>
        </div>
        <div className="kpi-card blue">
          <h4>Facturas Realizadas</h4>
          <p className="kpi-value">{stats.facturasEmitidas}</p>
        </div>
        <div className="kpi-card green">
          <h4>Clientes Nuevos</h4>
          <p className="kpi-value">{stats.clientesNuevos}</p>
        </div>
      </div>

      <div className="chart-section">
        <h3>Flujo de Facturación</h3>
        <div style={{ width: '100%', height: 300 }}>
          <ResponsiveContainer width="100%" height="100%">
            <BarChart data={stats.graficoVentas}>
              <CartesianGrid strokeDasharray="3 3" />
              <XAxis dataKey="mes" />
              <YAxis />
              <Tooltip />
              <Bar dataKey="monto" fill="#3498db" radius={[5, 5, 0, 0]} />
            </BarChart>
          </ResponsiveContainer>
        </div>
      </div>
    </div>
  );
}

export default Dashboard;