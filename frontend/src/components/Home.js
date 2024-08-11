import React from 'react';
import { Container, Typography, Box, Button, Grid, Paper } from '@mui/material';
import { useNavigate } from 'react-router-dom';

const Home = () => {
  const navigate = useNavigate();

  return (
    <Box 
      sx={{
        backgroundColor: '#e0e0e0', // Color neutro más oscuro para el fondo
        minHeight: '100vh',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        padding: '20px',
      }}
    >
      <Container maxWidth="md">
        <Paper 
          elevation={6} 
          sx={{ 
            padding: '40px', 
            backgroundColor: '#ffffff',
            borderRadius: '15px',
            boxShadow: '0 8px 16px rgba(0,0,0,0.2)',
          }}
        >
          <Typography 
            variant="h3" 
            align="center" 
            gutterBottom 
            sx={{ 
              color: '#333', 
              fontWeight: 'bold',
              textTransform: 'uppercase',
            }}
          >
            Administración de Permisos
          </Typography>
          <Typography 
            variant="h6" 
            align="center" 
            gutterBottom 
            sx={{ 
              color: '#666', 
              fontStyle: 'italic',
            }}
          >
            Selecciona una opción para continuar:
          </Typography>
          <Box mt={4}>
            <Grid container spacing={3} justifyContent="center">
              <Grid item xs={12} sm={6} md={6}>
                <Button 
                  variant="contained" 
                  color="primary" 
                  onClick={() => navigate('/permissions')}
                  sx={{
                    width: '100%',
                    padding: '12px 24px', 
                    fontSize: '18px',
                    fontWeight: 'bold',
                    transition: 'transform 0.3s, background-color 0.3s',
                    '&:hover': {
                      backgroundColor: '#1565c0',
                      transform: 'scale(1.1)',
                    },
                  }}
                >
                  Listar Permisos
                </Button>
              </Grid>
              <Grid item xs={12} sm={6} md={6}>
                <Button 
                  variant="contained" 
                  color="secondary" 
                  onClick={() => navigate('/permissions/request')}
                  sx={{
                    width: '100%',
                    padding: '12px 24px', 
                    fontSize: '18px',
                    fontWeight: 'bold',
                    transition: 'transform 0.3s, background-color 0.3s',
                    '&:hover': {
                      backgroundColor: '#c62828',
                      transform: 'scale(1.1)',
                    },
                  }}
                >
                  Solicitar Permiso
                </Button>
              </Grid>
              <Grid item xs={12} sm={6} md={6}>
                <Button 
                  variant="contained" 
                  color="primary" 
                  onClick={() => navigate('/tipo-permisos')}
                  sx={{
                    width: '100%',
                    padding: '12px 24px', 
                    fontSize: '18px',
                    fontWeight: 'bold',
                    transition: 'transform 0.3s, background-color 0.3s',
                    '&:hover': {
                      backgroundColor: '#1565c0',
                      transform: 'scale(1.1)',
                    },
                  }}
                >
                  Listar Tipos de Permisos
                </Button>
              </Grid>
              <Grid item xs={12} sm={6} md={6}>
                <Button 
                  variant="contained" 
                  color="secondary" 
                  onClick={() => navigate('/tipo-permisos/create')}
                  sx={{
                    width: '100%',
                    padding: '12px 24px', 
                    fontSize: '18px',
                    fontWeight: 'bold',
                    transition: 'transform 0.3s, background-color 0.3s',
                    '&:hover': {
                      backgroundColor: '#c62828',
                      transform: 'scale(1.1)',
                    },
                  }}
                >
                  Crear Tipo de Permiso
                </Button>
              </Grid>
            </Grid>
          </Box>
        </Paper>
      </Container>
    </Box>
  );
};

export default Home;
