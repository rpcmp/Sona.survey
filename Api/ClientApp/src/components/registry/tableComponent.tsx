import React, { useState, useEffect, } from 'react';
import { Navigate, useNavigate } from 'react-router-dom';
import { Alert, Table, Nav, NavItem, NavLink, Button } from 'reactstrap';
import { Entity, ModalProps } from '../models';
import { FiEdit } from 'react-icons/fi'
import { AiFillDelete } from 'react-icons/ai'
import { post } from '../client';

export interface CreateModalComponentProps {
    modalProps: ModalProps;
    loadData: () => Promise<void>;
}

export interface UpdateModalComponentProps {
    id: number;
    modalProps: ModalProps;
    loadData: () => Promise<void>;
}

interface TableProps<T> {
    name: string;
    headers: string[];

    addModal: (props: CreateModalComponentProps) => JSX.Element;
    updateModal: (props: UpdateModalComponentProps) => JSX.Element;

    get: () => Promise<T[]>;
    delete: (id: number) => Promise<void>;

    mapRow: (data: T) => JSX.Element;
}

export default function TableComponent<T extends Entity>(props: TableProps<T>) {
    const [data, setData] = useState<T[]>([]);

    const [addModal, setAddModal] = useState<boolean>(false);
    const [updateModal, setUpdateModal] = useState<boolean>(false);
    const [updateId, setUpdateId] = useState<number>(0);

    async function loadData(): Promise<void> {
        let data = await props.get();
        setData(data);
    }

    useEffect(() => {
        loadData();
    }, [])

    const navigate = useNavigate();

    function toBooks() {
        navigate('/books', { replace: true });
    }

    function toAuthors() {
        navigate('/authors', { replace: true });
    }

    function toGenres() {
        navigate('/genres', { replace: true });
    }

    function update(id: number): void {
        setUpdateId(id);
        setUpdateModal(true);
    }

    async function remove(id: number): Promise<void> {
        await props.delete(id);
        await loadData();
    }

    async function logout(): Promise<void> {
        let resp = await post('/api/Account/logout', null);
        navigate('/login', {replace: true});
    }

    function checkCookie(name: string): boolean {
        let value = `; ${document.cookie}`;
        let parts = value.split(`; ${name}=`);
        return parts.length > 1;
    }

    if (!checkCookie('.AspNetCore.Cookies')) {
        return <Navigate to='/login' replace />
    }

    return (
        <>
            <div className='border-right border-left mt-5 m-auto' style={{ width: '1200px' }} >
                <Nav tabs>
                    <NavItem>
                        <NavLink href="#" active={props.name == 'books'} onClick={toBooks}>Книги</NavLink>
                    </NavItem>
                    <NavItem>
                        <NavLink href="#" active={props.name == 'authors'} onClick={toAuthors}>Авторы</NavLink>
                    </NavItem>
                    <NavItem>
                        <NavLink href="#" active={props.name == 'genres'} onClick={toGenres}>Жанры</NavLink>
                    </NavItem>
                    <NavItem>
                        <NavLink href="#" onClick={() => setAddModal(true)}>Добавить</NavLink>
                    </NavItem>
                    <NavItem>
                        <NavLink href="#" onClick={() => logout()}>Выйти</NavLink>
                    </NavItem>
                </Nav>

                <Table bordered>
                    <thead>
                        <tr>
                            <th>#</th>
                            {props.headers.map((header, index) => (<th key={index}>{header}</th>))}
                            <th>Редактировать</th>
                            <th>Удалить</th>
                        </tr>
                    </thead>
                    <tbody>
                        {data.map((item, index) => {
                            return (
                                <tr key={index}>
                                    <th scope="row">{index}</th>
                                    {props.mapRow(item)}
                                    <th>
                                        <Button color='light' className='w-100' onClick={() => update(item.id)}>
                                            <FiEdit />Изменить
                                        </Button>
                                    </th>
                                    <th>
                                        <Button color='danger' className='w-100' onClick={() => remove(item.id)}>
                                            <AiFillDelete />Удалить
                                        </Button>
                                    </th>
                                </tr>
                            )
                        })}
                    </tbody>
                </Table>
            </div>
            {props.addModal({ loadData: loadData, modalProps: { isOpen: addModal, setIsOpen: setAddModal } })}
            {props.updateModal({ id: updateId, loadData: loadData, modalProps: { isOpen: updateModal, setIsOpen: setUpdateModal } })};
        </>)
}