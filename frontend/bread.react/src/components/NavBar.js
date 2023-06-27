import React, { useContext } from 'react';
import { AppBar, Toolbar, Typography, Button } from '@mui/material';
import { useNavigate, Link } from 'react-router-dom';
import { AuthContext } from '../AuthContext';

const NavBar = () => {
  const navigate = useNavigate(); // React hook that allows navigation to other pages.
  const { setIsLoggedIn } = useContext(AuthContext); // Access AuthContext to update setLoggedIn state.

  // Logout function.
  const handleLogout = async () => {
    try {
      const response = await fetch('http://localhost:5020/user/logout', {
        method: 'POST',
        credentials: 'include',
      });

      if (response.ok) {
        // Clear the JWT if the response is Ok from the backend.
        document.cookie =
          'token=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;'; // Set token to empty string expiry date to a previous date to ensure the token is cleared from the cookie.
        setIsLoggedIn(false); // Set isLoggedIn to false, this clears the NavBar status.
        navigate('/login'); // Navigate to /login.
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
          <Link
            to='/'
            style={{ textDecoration: 'none', color: 'inherit' }}
          >
            Finance Manager App
          </Link>
        </Typography>
        <Button
          color='inherit'
          component={Link}
          to='/dashboard'
        >
          Dashboard
        </Button>
        <Button
          color='inherit'
          component={Link}
          to='/transactions'
        >
          Transactions
        </Button>
        <Button
          color='inherit'
          component={Link}
          to='/account'
        >
          Account
        </Button>
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
