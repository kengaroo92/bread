import React, { createContext, useState, useEffect } from 'react';

// AuthContext used to provide and consume data.
export const AuthContext = createContext();
// AuthProvider component. Provides context data to children.
export const AuthProvider = ({ children }) => {
  // State Variable.
  const [user, setUser] = useState(null); // 'user' tracks user data, 'setUser' is the function used to update the data.
  const [loading, setLoading] = useState(true); // 'loading' tracks loading state, 'setLoading' is the function used to update the state.

  // useEffect hook runs the function when a component is mounted. In this case it fetches user data from the endpoint.
  useEffect(() => {
    fetch('http://localhost:5020/user/account', { credentials: 'include' }) // Must include credentials so that the cookie is included in the call.
      .then((response) => response.json())
      .then((data) => {
        // Set user data in the state, and set loading to false.
        setUser(data.user);
        setLoading(false);
      })
      .catch((error) => {
        console.error('Error fetching user data: ', error);
        setLoading(false);
      });
  }, []); // The empty array means useEffect will only run once when the component is mounted and not on any other renders.

  return (
    // Render the AuthContext component making user data and loading state available to all child components.
    <AuthContext.Provider value={{ user, setUser, loading }}>
      {children}
    </AuthContext.Provider>
  );
};