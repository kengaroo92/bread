import React, { useState } from 'react';
import {
  Button,
  Container,
  TextField,
  Typography,
  Paper,
  FormControl,
  Radio,
  RadioGroup,
  FormControlLabel,
} from '@mui/material';
import { useNavigate } from 'react-router-dom';

const AddTransaction = () => {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    date: '',
    description: '',
    amount: '',
    category: '',
    type: 'income',
  });

  const handleChange = (event) => {
    setFormData({
      ...formData,
      [event.target.name]: event.target.value,
    });
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      const payload = {
        date: formData.date,
        description: formData.description,
        category: formData.category,
        income: formData.type === 'income' ? formData.amount : 0,
        expense: formData.type === 'expense' ? formData.amount : 0,
      };

      const response = await fetch('http://localhost:5020/transaction/add', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(payload),
      });

      if (!response.ok) {
        const errorResponse = await response.json();
        console.error('Error response from server: ', errorResponse);
        throw new Error('Failed to add transaction.');
      }
    } catch (error) {
      console.error('Error adding transaction: ', error);
    }
  };

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
          style={{ marginBottom: '1em', backgroundColor: 'white' }}
          InputLabelProps={{ shrink: true }} // Shrinks the label "Date" essentially always having it hover above the "mm/dd/yyyy" text.
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
        <FormControl
          component='fieldset'
          style={{ marginBottom: '1em' }}
        >
          <RadioGroup
            row
            aria-label='type'
            name='type'
            value={formData.type}
            onChange={handleChange}
          >
            <FormControlLabel
              value='income'
              control={<Radio />}
              label='Income'
            />
            <FormControlLabel
              value='expense'
              control={<Radio />}
              label='Expense'
            />
          </RadioGroup>
        </FormControl>
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
