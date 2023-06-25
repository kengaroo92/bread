// App.js is the main component of the React app.
// Add more components here so that they will be rendered by the DOM when you run npm start.

// Import the required components from the react-router-dom package.
// BrowserRouter is a router implementation that uses HTML5 to keep your UI synced with the URL.
// Router, Route, and Routes are components used to define the routes of the application.
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
// Provide Context
import { AuthProvider } from './AuthContext';

import React, { useState } from 'react';
import ReactDOM from 'react-dom';
// Import component files. Each component represents a different page.
import Home from './components/Home';
import Registration from './components/Registration';
import Login from './components/Login';
import Account from './components/Account';
import RegistrationSuccess from './components/RegistrationSuccess';
// Import the CSS files to style the app.
import './App.css';

// Define the main 'App' component of the application.
function App() {
  const [user, setUser] = useState(null);

  return (
    <AuthProvider>
      {/* Wrap your application inside a Router component. Router listens to changes in the URL and renders the appropriate page (Route component). */}
      <Router>
        {/* Routes component is just a container to hold multiple Route components.*/}
        <Routes>
          {/* Each Route component represents a different page.
            The path prop defines the URL path.
            The element prop defines the actual component file that will be rendered.*/}
          <Route
            path='/'
            element={<Home />}
          />
          <Route
            path='/registration'
            element={<Registration />}
          />
          <Route
            path='/login'
            element={<Login />}
          />
          <Route
            path='/account'
            element={<Account />}
          />
          <Route
            path='/registrationsuccess'
            element={<RegistrationSuccess />}
          />
        </Routes>
      </Router>
    </AuthProvider>
  );
}
// Export the App component so that it can be imported and used in other files.
export default App;
