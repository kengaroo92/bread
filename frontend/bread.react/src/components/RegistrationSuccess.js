import React, { useContext } from 'react';
import { UserContext } from '../UserContext';

const RegistrationSuccess = () => {
  const { user } = useContext(UserContext);

  return (
    <div>
      <h1>Registration Successful!</h1>
      <p>
        Thank you for registering {user.fullname}. In the future to login,
        please use your Username: {user.username} or your Email Address:{' '}
        {user.email}
      </p>
      <table>
        <thead>
          <tr>
            <th>Name</th>
            <th>Username</th>
            <th>Email Address</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>{user.fullname}</td>
            <td>{user.username}</td>
            <td>{user.email}</td>
          </tr>
        </tbody>
      </table>
    </div>
  );
};

export default RegistrationSuccess;
