import React, { useState } from 'react';
import PropTypes from 'prop-types'
import { useNavigate } from 'react-router-dom'
import { Row, Col, Card, CardHeader, CardBody, CardTitle, CardText, CardFooter, Input, Button, Alert } from 'reactstrap'

interface RegisterModel {
    UserName: string,
    Password: string,
    ConfirmPassword: string
}

export default function RegisterComponent() {
    const navigate = useNavigate();

    const [alert, setAlert] = useState({
        showAlert: false,
        textAlert: ''
    });

    const [name, setName] = useState('');
    const [pass, setPass] = useState('');
    const [confirmPass, setConfirmPass] = useState('');

    async function register(): Promise<void> {
        setAlert({
            showAlert: false,
            textAlert: ''
        });

        if (!name) {
            setAlert({
                showAlert: true,
                textAlert: 'Необходимо ввести логин'
            });
            return;
        }

        if (!pass) {
            setAlert({
                showAlert: true,
                textAlert: 'Необходимо ввести пароль'
            });
            return;
        }

        if (!confirmPass) {
            if (!pass) {
                setAlert({
                    showAlert: true,
                    textAlert: 'Необходимо подтвердить пароль'
                });
                return;
            }
        }

        let body: RegisterModel = {
            UserName: name,
            Password: pass,
            ConfirmPassword: confirmPass
        };

        let response = await fetch('/api/Account/register', {
            method: 'POST',
            mode: 'cors',
            headers: new Headers({ 'Content-Type': 'application/json' }),
            body: JSON.stringify(body)
        });

        if (response.status == 200) {
            navigate('../books');
        } else {
            let message = await response.text();

            setAlert({
                showAlert: true,
                textAlert: message
            });
        }
    }

    function toLogin(): void {
        navigate('/login', { replace: true });
    }

    return (
        <Card className='m-auto mt-5' style={{
            width: '500px'
        }}>
            <CardHeader>
                Регистрация
            </CardHeader>
            <CardBody>
                <Input className='w-100 mt-3 m-auto' type='text' placeholder='Логин' onChange={e => setName(e.target.value)} />
                <Input className='w-100 mt-3 m-auto' type='text' placeholder='Пароль' onChange={e => setPass(e.target.value)} />
                <Input className='w-100 mt-3 m-auto' type='text' placeholder='Подтвердите пароль' onChange={e => setConfirmPass(e.target.value)} />
            </CardBody>
            <Alert color='danger' className='m-3 mt-0' hidden={!alert.showAlert}>{alert.textAlert}</Alert>
            <CardFooter className='text-center'>
                <Button color='primary' className='mt-1 w-100' onClick={register}>Зарегистрироваться</Button>
                <Button color='light' className='mt-1 w-100' onClick={toLogin}>У вас уже есть аккаунт?</Button>
            </CardFooter>
        </Card>
    )
}