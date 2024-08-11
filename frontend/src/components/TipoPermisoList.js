import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Container, Typography, List, ListItem, ListItemText, CircularProgress, Alert, Paper, Box, Button, Pagination, TextField } from '@mui/material';
import { API_BASE_URL } from './endpoints.ts';
import { useNavigate } from 'react-router-dom';

const TipoPermisoList = () => {
  const [tiposPermiso, setTiposPermiso] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [currentPage, setCurrentPage] = useState(1);
  const [searchTerm, setSearchTerm] = useState(''); // Estado para el término de búsqueda
  const itemsPerPage = 10;  // Número de elementos por página
  const navigate = useNavigate();

  useEffect(() => {
    axios.get(`${API_BASE_URL}/api/tipoPermisos`)
      .then((response) => {
        const data = response.data.$values;
        setTiposPermiso(Array.isArray(data) ? data : []);
        setLoading(false);
      })
      .catch((error) => {
        console.error('Error al obtener los tipos de permiso:', error);
        setError('Error al cargar los tipos de permiso');
        setLoading(false);
      });
  }, []);

  const handlePageChange = (event, value) => {
    setCurrentPage(value);
  };

  // Filtrar los tipos de permiso según el término de búsqueda
  const filteredTiposPermiso = tiposPermiso.filter((tipo) =>
    tipo.description.toLowerCase().includes(searchTerm.toLowerCase())
  );

  // Calcular el número total de páginas
  const totalPages = Math.ceil(filteredTiposPermiso.length / itemsPerPage);

  // Obtener los elementos de la página actual
  const paginatedTiposPermiso = filteredTiposPermiso.slice(
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
      <Paper elevation={5} style={{ padding: '30px', marginTop: '40px', backgroundColor: '#f7f7f7' }}>
        <Typography variant="h4" align="center" gutterBottom style={{ color: '#333' }}>
          Lista de Tipos de Permiso
        </Typography>

        {/* Campo de búsqueda */}
        <Box mt={2} mb={3}>
          <TextField
            label="Buscar tipo de permiso"
            variant="outlined"
            fullWidth
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
          />
        </Box>

        <Box mt={2}>
          {Array.isArray(paginatedTiposPermiso) && paginatedTiposPermiso.length > 0 ? (
            <Paper elevation={3} style={{ padding: '20px', marginTop: '20px', backgroundColor: '#ffffff' }}>
              <List>
                {paginatedTiposPermiso.map((tipo) => (
                  <ListItem key={tipo.id} divider style={{ borderBottom: '1px solid #ccc' }}>
                    <ListItemText
                      primary={tipo.description}
                      primaryTypographyProps={{
                        variant: 'h6',
                        color: 'textPrimary',
                      }}
                    />
                  </ListItem>
                ))}
              </List>
            </Paper>
          ) : (
            <Typography variant="body1" align="center" color="textSecondary">
              No se encontraron tipos de permiso.
            </Typography>
          )}
        </Box>
        <Box display="flex" justifyContent="center" mt={2}>
          <Pagination
            count={totalPages}
            page={currentPage}
            onChange={handlePageChange}
            color="primary"
          />
        </Box>
        <Box display="flex" justifyContent="center" mt={3}>
          <Button variant="outlined" color="primary" onClick={() => navigate('/')}>
            Volver al Home
          </Button>
        </Box>
      </Paper>
    </Container>
  );
};

export default TipoPermisoList;
