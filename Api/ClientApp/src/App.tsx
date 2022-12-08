import React, { useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Container, Nav, NavItem, NavLink } from 'reactstrap';
import LoginComponent from './components/accounts/loginComponent';
import { BrowserRouter as Router, Routes, Route, Link, Navigate } from 'react-router-dom';
import RegisterComponent from './components/accounts/registerComponent';
import GenresTableComponent from './components/registry/genres/genresTableComponent';
import BooksTableComponent from './components/registry/books/booksTableComponent';
import AuthorTableComponent from './components/registry/authors/authorsTableComponent';



const App = () => {
  return (
    <Container fluid>
      <Router>
        <Routes>
          <Route path='login' element={<LoginComponent />} />
          <Route path='register' element={<RegisterComponent />} />
          <Route path='/' element={<Navigate to={'/books'} />} />
          <Route path='books' element={<BooksTableComponent />} />
          <Route path='authors' element={<AuthorTableComponent />} />
          <Route path='genres' element={<GenresTableComponent />} />
        </Routes>
      </Router>
    </Container >
  );
}

export default App;
