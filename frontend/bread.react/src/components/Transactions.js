import React, { useContext, useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import {
  Button,
  Container,
  TextField,
  Typography,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
} from '@mui/material';
import { AuthContext } from '../AuthContext';

const Transactions = () => {
  const navigate = useNavigate();
  const { setIsLoggedIn } = useContext(AuthContext);
  const [transactions, setTransactions] = useState();
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    // Fetches all transactions when the component is mounted.
    const fetchTransactions = async () => {
      try {
        const response = await fetch('http://localhost:5020/transactions', {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
          },
        });

        if (!response.ok) {
          throw new Error('Failed to get all transactions.');
        }

        const data = await response.json();
        setTransactions(data);
        setLoading(false);
      } catch (error) {
        setError(error.message);
        setLoading(false);
      }
    };

    fetchTransactions();
  }, []); // Will only run once when mounted.

  const handleDelete = async (transactionId) => {
    // TODO: Add delete transaction logic.
  };

  const handleEdit = async (transactionId) => {
    // TODO: Add edit transaction logic.
  };

  if (loading) {
    return <div>Loading...</div>;
    // TODO: Add cool loading animation.
  }

  if (error) {
    return <div>Error: {error}</div>;
    // TODO: Add MaterialUI snackmessage or some type of alert for errors.
  }

  return (
    <Container component={Paper}>
      <Typography variant='h4'>Your Transactions</Typography>
      <Button onClick={() => navigate('/add')}>Add Transaction</Button>
      {/* Show transactions in a table format. */}
      {/* TODO: Add Filtering, Pagination, Searching */}
      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Date</TableCell>
              <TableCell>Description</TableCell>
              <TableCell>Amount</TableCell>
              <TableCell>Category</TableCell>
              <TableCell>Action</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {transactions.map((transactions) => (
              <TableRow key={transactions.id}>
                <TableCell>{transactions.date}</TableCell>
                <TableCell>{transactions.description}</TableCell>
                <TableCell>{transactions.amount}</TableCell>
                <TableCell>{transactions.category}</TableCell>
                <TableCell>
                  <Button onClick={() => handleEdit(transactions.id)}>
                    Edit
                  </Button>
                  <Button onClick={() => handleDelete(transactions.id)}>
                    Delete
                  </Button>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </Container>
  );
};

export default Transactions;
