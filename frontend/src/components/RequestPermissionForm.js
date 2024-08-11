import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Button, TextField, Container, Typography, Box, Paper, Grid, Snackbar, Alert, CircularProgress } from '@mui/material';
import { Autocomplete } from '@mui/material'; // Importar Autocomplete
import { API_BASE_URL } from './endpoints.ts';
import { useNavigate } from 'react-router-dom';

const RequestPermissionForm = () => {
  const [nombreEmpleado, setNombreEmpleado] = useState('');
  const [apellidoEmpleado, setApellidoEmpleado] = useState('');
  const [tipoPermisoId, setTipoPermisoId] = useState(null); // Cambia a null inicialmente
  const [fechaPermiso, setFechaPermiso] = useState('');
  const [tiposPermiso, setTiposPermiso] = useState([]); // Estado para almacenar los tipos de permiso
  const [loading, setLoading] = useState(true);
  const [openSnackbar, setOpenSnackbar] = useState(false);
  const [snackbarMessage, setSnackbarMessage] = useState('');
  const [snackbarSeverity, setSnackbarSeverity] = useState('success');
  const navigate = useNavigate();

  useEffect(() => {
    axios.get(`${API_BASE_URL}/api/tipoPermisos`)
      .then((response) => {
        const data = response.data.$values; // Acceder al array dentro de $values
        setTiposPermiso(Array.isArray(data) ? data : []);
        setLoading(false);
      })
      .catch((error) => {
        console.error('Error al obtener los tipos de permiso:', error);
        setSnackbarMessage('Error al obtener los tipos de permiso');
        setSnackbarSeverity('error');
        setOpenSnackbar(true);
        setLoading(false);
      });
  }, []);

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!tipoPermisoId) {
      setSnackbarMessage('Por favor seleccione un tipo de permiso');
      setSnackbarSeverity('warning');
      setOpenSnackbar(true);
      return;
    }

    const payload = {
      nombreEmpleado,
      apellidoEmpleado,
      tipoPermisoId,
      fechaPermiso
    };

    try {
      await axios.post(`${API_BASE_URL}/api/permissions`, payload);
      setSnackbarMessage('Permiso solicitado con éxito');
      setSnackbarSeverity('success');
    } catch (error) {
      console.error('Error al solicitar el permiso:', error);
      setSnackbarMessage('Error al solicitar el permiso');
      setSnackbarSeverity('error');
    } finally {
      setOpenSnackbar(true);
    }
  };

  const handleCloseSnackbar = () => {
    setOpenSnackbar(false);
  };

  return (
    <Container maxWidth="sm">
      <Paper elevation={4} style={{ padding: '30px', marginTop: '40px' }}>
        <Typography variant="h4" align="center" gutterBottom>
          Solicitud de Permiso
        </Typography>
        <form onSubmit={handleSubmit}>
          <Grid container spacing={4}>
            <Grid item xs={12}>
              <TextField
                label="Nombre"
                value={nombreEmpleado}
                onChange={(e) => setNombreEmpleado(e.target.value)}
                fullWidth
                required
                variant="outlined"
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                label="Apellido"
                value={apellidoEmpleado}
                onChange={(e) => setApellidoEmpleado(e.target.value)}
                fullWidth
                required
                variant="outlined"
              />
            </Grid>

            <Grid item xs={12}>
              <Autocomplete
                options={tiposPermiso}
                getOptionLabel={(option) => option.description}
                onChange={(event, newValue) => {
                  setTipoPermisoId(newValue ? newValue.id : null);
                }}
                renderInput={(params) => (
                  <TextField
                    {...params}
                    label="Tipo Permiso"
                    variant="outlined"
                    fullWidth
                    required
                  />
                )}
              />
            </Grid>

            <Grid item xs={12}>
              <TextField
                label="Fecha Permiso"
                type="date"
                value={fechaPermiso}
                onChange={(e) => setFechaPermiso(e.target.value)}
                fullWidth
                required
                InputLabelProps={{
                  shrink: true,
                }}
                variant="outlined"
              />
            </Grid>
            <Grid item xs={12}>
              <Box display="flex" justifyContent="space-between" mt={2}>
                <Button type="submit" variant="contained" color="primary" size="large">
                  Solicitar Permiso
                </Button>
                <Button variant="outlined" color="secondary" size="large" onClick={() => navigate('/permissions')}>
                  Ver Lista de Permisos
                </Button>
              </Box>
            </Grid>
          </Grid>
        </form>
      </Paper>
      <Snackbar
        open={openSnackbar}
        autoHideDuration={6000}
        onClose={handleCloseSnackbar}
      >
        <Alert onClose={handleCloseSnackbar} severity={snackbarSeverity}>
          {snackbarMessage}
        </Alert>
      </Snackbar>
    </Container>
  );
};

export default RequestPermissionForm;
