import React, { useEffect, useState } from 'react';
import jwt_decode from 'jwt-decode'; // https://www.npmjs.com/package/jwt-decode

const Account = () => {
  const [user, setUser] = useState(null);

  useEffect(() => {
    // Function to get cookie.
    const getCookie = (name) => {
      const value = `; ${document.cookie}`;
      const parts = value.split(`; ${name}=`);
      if (parts.length === 2) return parts.pop().split(';').shift();
    };

    // Retrieve the JWT from the cookie.
    const token = getCookie('authToken');
    console.log('Token retrieved from cookie: ', token);

    // Decode the JWT.
    if (token) {
      const decodedToken = jwt_decode(token);
      setUser(decodedToken);
      console.log(decodedToken);
    }
  }, []);

  return (
    <div>
      <h1>User Account Information</h1>
      {user ? (
        <div>
          <p>Name: {user.name}</p>
          <p>Email: {user.email}</p>
        </div>
      ) : (
        <p>Loading...</p>
      )}
    </div>
  );
};

export default Account;
