import React, { useState } from 'react';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';
import { ModalProps } from '../models';

export interface UpdateModalProps<T> {
    id: number;
    modalState: ModalProps;

    children: JSX.Element | JSX.Element[];

    update: (id: number) => Promise<T>;
    loadData: () => Promise<void>;
}

export default function UpdateModalComponent<T>(props: UpdateModalProps<T>) {
    async function update(): Promise<void> {
        await props.update(props.id);
        await props.loadData();
        props.modalState.setIsOpen(false);
    }

    return (
        <Modal isOpen={props.modalState.isOpen}>
            <ModalHeader>Редактировать</ModalHeader>
            <ModalBody>
                {props.children}
            </ModalBody>
            <ModalFooter>
                <Button color='primary' onClick={update}>Сохранить</Button>
                <Button color='secondary' onClick={() => props.modalState.setIsOpen(false)}>Закрыть</Button>
            </ModalFooter>
        </Modal>
    )
}