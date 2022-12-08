import React, { useState } from 'react';
import { InputGroup, Input } from 'reactstrap';
import { put } from '../../client';
import { AuthorModel } from '../../models';
import CreateModalComponent from '../createModalComponent';
import { CreateModalComponentProps, ModalProps } from '../tableComponent';

export default function CreateAuthorModalComponent(props: CreateModalComponentProps) {
    const [firstName, setFirstName] = useState<string>('');
    const [lastName, setLastName] = useState<string>('');
    const [birthdate, setBirthdate] = useState<Date | undefined>(undefined);

    function addAuthor(): Promise<AuthorModel> {
        let author: AuthorModel = {
            id: 0,
            firstName: firstName,
            lastName: lastName,
            birthdate: birthdate
        };

        return put('api/Authors', author);
    }

    return (
        <CreateModalComponent<AuthorModel> loadData={props.loadData} modalState={props.modalProps} add={addAuthor}>
            <InputGroup >
                <Input placeholder='Имя' onChange={(e) => { setFirstName(e.target.value) }} />
            </InputGroup>
            <br />
            <InputGroup >
                <Input placeholder='Фамилия' onChange={(e) => { setLastName(e.target.value) }} />
            </InputGroup>
            <br />
            <InputGroup >
                <Input placeholder='Дата рождения' type='date' onChange={(e) => { setBirthdate(e.target.valueAsDate ?? new Date()) }} />
            </InputGroup>
        </CreateModalComponent>
    )
}
