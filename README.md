# 📊 NexoFactura - Sistema de Gestión de Facturación

![NexoFactura Banner](https://img.shields.io/badge/Status-En_Desarrollo-blue?style=for-the-badge&logo=react)
![Backend](https://img.shields.io/badge/Backend-.NET_8_Core-purple?style=for-the-badge&logo=dotnet)
![Frontend](https://img.shields.io/badge/Frontend-React_Vite-61DAFB?style=for-the-badge&logo=react)
![Database](https://img.shields.io/badge/Database-PostgreSQL-336791?style=for-the-badge&logo=postgresql)

**NexoFactura** es una solución integral para la gestión de emisores, clientes y facturación electrónica. Diseñada con una arquitectura moderna que separa el poder de **.NET Core** en el servidor y la agilidad de **React** en la interfaz.

---

## 🚀 Características Principales

-   ✅ **Dashboard Interactivo:** Resumen en tiempo real de ventas y estados de facturación.
-   🏢 **Gestión de Emisores:** Registro completo de empresas emisoras (NIT, Razón Social, etc.).
-   👥 **Módulo de Clientes:** Administración de base de datos de clientes.
-   📄 **Generación de Facturas:** Sistema de creación rápida mediante modales dinámicos.
-   ⚡ **Arquitectura SPA:** Navegación instantánea sin recarga de página con React Router.
-   🐳 **Docker Ready:** Base de datos PostgreSQL contenida para un despliegue rápido.

---

## 📂 Estructura del Proyecto

### 💻 Frontend (React + Vite)
```text
src/
 ├── components/        # Componentes reutilizables (Header, ModalFactura, etc.)
 ├── pages/             # Páginas principales (Dashboard, Clientes, Emisores)
 ├── services/          # Conexión a la API (api.js, emisorService.js, etc.)
 ├── assets/            # Imágenes y estilos globales
 └── App.jsx            # Enrutador y control de estados globales
