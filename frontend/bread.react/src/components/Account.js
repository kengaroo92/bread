import React, { useEffect, useState } from 'react';

const Account = () => {
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    // Make a call to the account endpoint to get the users information if the JWT is validated successfully.
    fetch('http://localhost:5020/user/account', { credentials: 'include' }) // Must include credentials so that the cookie is included in the call.
      .then((response) => response.json())
      .then((data) => {
        console.log('Data received from the server: ', data); // Console log response for debugging.
        setUser(data.user);
        setLoading(false);
      })
      .catch((error) => {
        console.error('Error fetching user data: ', error);
        setLoading(false);
      });
  }, []);

  return (
    <div>
      <h1>User Account Information</h1>
      {loading ? (
        <p>Loading...</p>
      ) : (
        user && (
          <div>
            <p>Username: {user.userName}</p>
            <p>Name: {user.fullName}</p>
            <p>Email: {user.email}</p>
          </div>
        )
      )}
    </div>
  );
};

export default Account;
