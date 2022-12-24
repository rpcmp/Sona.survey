import React, { useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Container, Modal, ModalHeader, ModalBody, ModalFooter, Button, Nav, NavItem, NavLink } from 'reactstrap';
import LoginComponent from './components/accounts/loginComponent';
import { BrowserRouter as Router, Routes, Route, Link, Navigate } from 'react-router-dom';
import SurveyComponent from './components/survey/surveyComponent';



const App = () => {
  const [modalIsOpen, setModalIsOpen] = useState<boolean>(false);
  const [modalText, setModalText] = useState<string | null>(null);
  const toggle = () => setModalIsOpen(!modalIsOpen);

  return (
    <>
      <Container>
        <Router>
          <Routes>
            <Route path='login' element={<LoginComponent />} />
            <Route path='/' element={<SurveyComponent isOpen={modalIsOpen} setIsOpen={setModalIsOpen} setModalText={setModalText} />} />
          </Routes>
        </Router>
      </Container >
      <Modal isOpen={modalIsOpen} toggle={toggle}>
        <ModalHeader toggle={toggle}>Результат сохранен</ModalHeader>
        <ModalBody>{modalText}</ModalBody>
        <ModalFooter>
          <Button color="secondary" onClick={toggle}>Закрыть</Button>
        </ModalFooter>
      </Modal>
    </>
  );
}

export default App;
