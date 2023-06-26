import React, { useContext } from 'react';
import { AppBar, Toolbar, Typography, Button } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { AuthContext } from '../AuthContext';

const NavBar = () => {
  const navigate = useNavigate();
  const { setIsLoggedIn } = useContext(AuthContext);

  const handleLogout = async () => {
    try {
      const response = await fetch('http://localhost:5020/user/logout', {
        method: 'POST',
        credentials: 'include',
      });

      if (response.ok) {
        document.cookie =
          'token=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;';
        setIsLoggedIn(false);
        navigate('/login');
      } else {
        console.error('Failed to logout.');
      }
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <AppBar position='static'>
      <Toolbar>
        <Typography
          variant='h6'
          style={{ flexGrow: 1 }}
        >
          Finance Manager App
        </Typography>
        <Button
          color='inherit'
          onClick={handleLogout}
        >
          Logout
        </Button>
      </Toolbar>
    </AppBar>
  );
};

export default NavBar;
