import React, { useState } from 'react';

const Registration = () => {
  const [formData, setFormData] = useState({
    username: '',
    firstname: '',
    lastname: '',
    email: '',
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
      const response = await fetch(
        // Make a call to the PostUser endpoint to register the information provided in the formData object and save it to the database.
        'http://localhost:5020/user/register',
        {
          method: 'POST', // Declare the method type you are calling.
          headers: {
            // Declare any headers required by the API, make sure these are accurate or you will experience CORS policy issues if they are set.
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(formData), // The body data type must match the 'Content-Type' header. In this case, we need to stringify the formData object into a JSON object.
        }
      );
      const data = await response.json(); // The API will return a response as JSON, so we will need to parse the response into a native JavaScript object.
    } catch (error) {
      console.error(error); // Catch any errors returned by the API. Fetch doesn't properly handle HTTP responses, it can be confusing. It only rejects the Promise on network failures, so you won't see HTTP status codes 404 or 500 for example.
      // TODO: Add in logic or an external library like Axios, to handle API calls to allow for better error handling.
    }
  };

  return (
    <div>
      <h1>Register for a new account using the form below.</h1>
      <form onSubmit={handleSubmit}>
        <label>
          Username:
          <input
            type='text'
            name='username'
            value={formData.username}
            onChange={handleChange}
            required
          />
        </label>
        <label>
          First Name:
          <input
            type='text'
            name='firstname'
            value={formData.firstname}
            onChange={handleChange}
            required
          />
        </label>
        <label>
          Last Name:
          <input
            type='text'
            name='lastname'
            value={formData.lastname}
            onChange={handleChange}
            required
          />
        </label>
        <label>
          Email:
          <input
            type='email'
            name='email'
            value={formData.email}
            onChange={handleChange}
            required
          />
        </label>
        <label>
          Password:
          <input
            type='password'
            name='password'
            value={formData.password}
            onChange={handleChange}
            required
          />
        </label>
        <button type='submit'>Register</button>
      </form>
    </div>
  );
};

export default Registration;
