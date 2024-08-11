import React, { useState } from 'react';
import axios from 'axios';
import { Container, Typography, TextField, Button, Paper, Box, Snackbar, Alert, Grid } from '@mui/material';
import { API_BASE_URL } from './endpoints.ts';
import { useNavigate } from 'react-router-dom';

const CreateTipoPermisoForm = () => {
  const [descripcion, setDescripcion] = useState('');
  const [openSnackbar, setOpenSnackbar] = useState(false);
  const [snackbarMessage, setSnackbarMessage] = useState('');
  const [snackbarSeverity, setSnackbarSeverity] = useState('success');
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!descripcion.trim()) {
      setSnackbarMessage('La descripción no puede estar vacía');
      setSnackbarSeverity('error');
      setOpenSnackbar(true);
      return;
    }

    const payload = { description: descripcion };

    try {
      await axios.post(`${API_BASE_URL}/api/tipoPermisos`, payload);
      setSnackbarMessage('Tipo de permiso creado con éxito');
      setSnackbarSeverity('success');
      setDescripcion('');
    } catch (error) {
      console.error('Error al crear el tipo de permiso:', error);
      setSnackbarMessage('Error al crear el tipo de permiso');
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
      <Paper elevation={4} style={{ padding: '30px', marginTop: '40px', backgroundColor: '#f7f7f7' }}>
        <Typography variant="h4" align="center" gutterBottom style={{ color: '#333' }}>
          Crear Tipo de Permiso
        </Typography>
        <form onSubmit={handleSubmit}>
          <Box mb={3}>
            <TextField
              label="Descripción del Tipo de Permiso"
              value={descripcion}
              onChange={(e) => setDescripcion(e.target.value)}
              fullWidth
              required
              variant="outlined"
            />
          </Box>
          <Grid container spacing={2} justifyContent="center">
            <Grid item>
              <Button type="submit" variant="contained" color="primary" size="large">
                Crear Tipo de Permiso
              </Button>
            </Grid>
            <Grid item>
              <Button variant="outlined" color="secondary" size="large" onClick={() => navigate('/tipo-permisos')}>
                Ver Lista de Tipos de Permisos
              </Button>
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

export default CreateTipoPermisoForm;
