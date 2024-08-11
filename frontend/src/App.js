import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from './components/Home';
import RequestPermissionForm from './components/RequestPermissionForm';
import TipoPermisoList from './components/TipoPermisoList';
import CreateTipoPermisoForm from './components/CreateTipoPermisoForm';
import PermissionList from './components/PermissionList'
// Importa los demás componentes para el manejo de TipoPermisos

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/permissions/request" element={<RequestPermissionForm />} />
        <Route path="/permissions" element={<PermissionList />} />
        <Route path="/tipo-permisos" element={<TipoPermisoList />} />
        <Route path="/tipo-permisos/create" element={<CreateTipoPermisoForm />} /> 
        {/* Agrega las rutas para TipoPermisos */}
        {/* Ejemplo: */}
        {/* <Route path="/tipo-permisos" element={<TipoPermisoList />} /> */}
        {/* <Route path="/tipo-permisos/create" element={<CreateTipoPermisoForm />} /> */}
      </Routes>
    </Router>
  );
}

export default App;
