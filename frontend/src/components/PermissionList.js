import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Container, Typography, List, ListItem, ListItemText, CircularProgress, Alert, Paper, Pagination, Box, Button, TextField, Grid } from '@mui/material';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { API_BASE_URL } from './endpoints.ts';
import { useNavigate } from 'react-router-dom';
import dayjs from 'dayjs';

const PermissionList = () => {
  const [permissions, setPermissions] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [currentPage, setCurrentPage] = useState(1);
  const itemsPerPage = 10;
  const [searchNombre, setSearchNombre] = useState('');
  const [searchApellido, setSearchApellido] = useState('');
  const [searchTipoPermiso, setSearchTipoPermiso] = useState('');
  const [searchFecha, setSearchFecha] = useState(null); // Estado para la búsqueda por fecha
  const navigate = useNavigate();

  useEffect(() => {
    axios.get(`${API_BASE_URL}/api/permissions`)
      .then((response) => {
        const data = response.data.$values;
        setPermissions(Array.isArray(data) ? data : []);
        setLoading(false);
      })
      .catch((error) => {
        console.error('Error al obtener la lista de permisos:', error);
        setError('Error al cargar la lista de permisos');
        setLoading(false);
      });
  }, []);

  const handlePageChange = (event, value) => {
    setCurrentPage(value);
  };

  // Filtrar los permisos según los criterios de búsqueda
  const filteredPermissions = permissions.filter((permission) => 
    permission.nombreEmpleado.toLowerCase().includes(searchNombre.toLowerCase()) &&
    permission.apellidoEmpleado.toLowerCase().includes(searchApellido.toLowerCase()) &&
    (permission.tipoPermisoDescripcion || '').toLowerCase().includes(searchTipoPermiso.toLowerCase()) &&
    (!searchFecha || dayjs(permission.fechaPermiso).isSame(searchFecha, 'day'))
  );

  // Calcular el número total de páginas
  const totalPages = Math.ceil(filteredPermissions.length / itemsPerPage);

  // Obtener los elementos de la página actual
  const paginatedPermissions = filteredPermissions.slice(
    (currentPage - 1) * itemsPerPage,
    currentPage * itemsPerPage
  );

  if (loading) {
    return (
      <Box display="flex" justifyContent="center" alignItems="center" minHeight="80vh">
        <CircularProgress />
      </Box>
    );
  }

  if (error) {
    return (
      <Box display="flex" justifyContent="center" alignItems="center" minHeight="80vh">
        <Alert severity="error">{error}</Alert>
      </Box>
    );
  }

  return (
    <Container maxWidth="md">
      <Paper elevation={3} style={{ padding: '20px', marginTop: '30px', backgroundColor: '#f9f9f9' }}>
        <Typography variant="h5" align="center" gutterBottom>
          Lista de Usuarios con Permisos
        </Typography>

        {/* Campos de búsqueda */}
        <Grid container spacing={2} justifyContent="center" style={{ marginBottom: '20px' }}>
          <Grid item xs={12} sm={3}>
            <TextField
              label="Buscar por Nombre"
              variant="outlined"
              fullWidth
              value={searchNombre}
              onChange={(e) => setSearchNombre(e.target.value)}
            />
          </Grid>
          <Grid item xs={12} sm={3}>
            <TextField
              label="Buscar por Apellido"
              variant="outlined"
              fullWidth
              value={searchApellido}
              onChange={(e) => setSearchApellido(e.target.value)}
            />
          </Grid>
          <Grid item xs={12} sm={3}>
            <TextField
              label="Buscar por Tipo de Permiso"
              variant="outlined"
              fullWidth
              value={searchTipoPermiso}
              onChange={(e) => setSearchTipoPermiso(e.target.value)}
            />
          </Grid>
          <Grid item xs={12} sm={3}>
            <LocalizationProvider dateAdapter={AdapterDayjs}>
              <DatePicker
                label="Buscar por Fecha"
                value={searchFecha}
                onChange={(newValue) => setSearchFecha(newValue)}
                renderInput={(params) => <TextField {...params} variant="outlined" fullWidth />}
              />
            </LocalizationProvider>
          </Grid>
        </Grid>

        <List>
          {Array.isArray(paginatedPermissions) && paginatedPermissions.length > 0 ? (
            paginatedPermissions.map((permission) => (
              <ListItem key={permission.id} divider>
                <ListItemText
                  primary={`${permission.nombreEmpleado} ${permission.apellidoEmpleado}`}
                  secondary={`Permiso: ${permission.tipoPermisoDescripcion || 'Sin descripción'} - Fecha: ${new Date(permission.fechaPermiso).toLocaleDateString()}`}
                />
              </ListItem>
            ))
          ) : (
            <Typography variant="body1" align="center">
              No se encontraron usuarios con permisos.
            </Typography>
          )}
        </List>

        <Box display="flex" justifyContent="center" mt={2}>
          <Pagination
            count={totalPages}
            page={currentPage}
            onChange={handlePageChange}
            color="primary"
          />
        </Box>
        <Box display="flex" justifyContent="center" mt={2}>
          <Button variant="outlined" color="primary" onClick={() => navigate('/')}>
            Volver al Home
          </Button>
        </Box>
      </Paper>
    </Container>
  );
};

export default PermissionList;
