import React, { useState } from 'react';
import { Button, Container, TextField, Typography, Paper } from '@mui/material';
import { useNavigate } from 'react-router-dom';

const AddTransaction = () => {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    date: '',
    description: '',
    amount: '',
    category: '',
  });

  const handleChange = (event) => {};

  const handleSubmit = async (event) => {};

  return (
    <Container
      component={Paper}
      style={{ padding: '2em' }}
    >
      <Typography
        variant='h4'
        style={{ marginBottom: '1em' }}
      >
        Add Transaction
      </Typography>
      <form onSubmit={handleSubmit}>
        <TextField
          fullWidth
          label='Date'
          name='date'
          type='date'
          variant='outlined'
          value={formData.date}
          onChange={handleChange}
          style={{ marginBottom: '1em' }}
        />
        <TextField
          fullWidth
          label='Description'
          name='description'
          variant='outlined'
          value={formData.description}
          onChange={handleChange}
          style={{ marginBottom: '1em' }}
        />
        <TextField
          fullWidth
          label='Amount'
          name='amount'
          type='number'
          variant='outlined'
          value={formData.amount}
          onChange={handleChange}
          style={{ marginBottom: '1em' }}
        />
        <TextField
          fullWidth
          label='Category'
          name='category'
          variant='outlined'
          value={formData.category}
          onChange={handleChange}
          style={{ marginBottom: '1em' }}
        />
        <Button
          type='submit'
          variant='contained'
          color='primary'
          fullWidth
          style={{ marginBottom: '1em' }}
        >
          Add Transaction
        </Button>
      </form>
    </Container>
  );
};

export default AddTransaction;
