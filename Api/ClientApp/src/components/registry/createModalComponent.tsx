import React, { useState } from 'react';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';
import { ModalProps } from './tableComponent';

interface CreateModalProps<T> {
    modalState: ModalProps;

    children: JSX.Element | JSX.Element[];

    add: () => Promise<T>;
    loadData: () => Promise<void>;
}

export default function CreateModalComponent<T>(props: CreateModalProps<T>) {
    async function create(): Promise<void> {
        await props.add();
        await props.loadData();
        props.modalState.setIsOpen(false);
    }

    return (
        <Modal isOpen={props.modalState.isOpen}>
            <ModalHeader>Добавить</ModalHeader>
            <ModalBody>
                {props.children}
            </ModalBody>
            <ModalFooter>
                <Button color='primary' onClick={create}>Сохранить</Button>
                <Button color='secondary' onClick={() => props.modalState.setIsOpen(false)}>Закрыть</Button>
            </ModalFooter>
        </Modal>
    )
}