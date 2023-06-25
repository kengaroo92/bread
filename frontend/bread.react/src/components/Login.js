import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Button, Container, TextField, Typography, Paper } from '@mui/material';
import { styled } from '@mui/material/styles';

const StyledPaper = styled(Paper)(({ theme }) => ({
  marginTop: theme.spacing(8),
  display: 'flex',
  flexDirection: 'column',
  alignItems: 'center',
}));

const StyledForm = styled('form')(({ theme }) => ({
  width: '100%',
  marginTop: theme.spacing(1),
}));

const StyledButton = styled(Button)(({ theme }) => ({
  margin: theme.spacing(3, 0, 2),
}));
const Login = () => {
  const navigate = useNavigate();

  const [formData, setFormData] = useState({
    username: '',
    password: '',
  });

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await fetch('http://localhost:5020/user/login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(formData),
        credentials: 'include',
      });

      if (response.ok) {
        navigate('/account');
      } else {
        console.error('Failed to login.');
      }
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <Container
      component='main'
      maxWidth='xs'
    >
      <StyledPaper elevation={3}>
        <Typography
          component='h1'
          variant='h5'
        >
          Please login to your account.
        </Typography>
        <StyledForm onSubmit={handleSubmit}>
          <TextField
            variant='outlined'
            margin='normal'
            required
            fullWidth
            label='Username'
            name='username'
            value={formData.username}
            onChange={handleChange}
            autoFocus
          />
          <TextField
            variant='outlined'
            margin='normal'
            required
            fullWidth
            label='Password'
            name='password'
            type='password'
            value={formData.password}
            onChange={handleChange}
          />
          <StyledButton
            type='submit'
            fullWidth
            variant='contained'
            color='primary'
          >
            Login
          </StyledButton>
        </StyledForm>
      </StyledPaper>
    </Container>
  );
};

export default Login;
