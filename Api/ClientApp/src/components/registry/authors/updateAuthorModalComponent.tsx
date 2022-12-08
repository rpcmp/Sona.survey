import React, { useState, useEffect } from 'react';
import { InputGroup, Input } from 'reactstrap';
import { get, post } from '../../client';
import { AuthorModel } from '../../models';
import { ModalProps, UpdateModalComponentProps } from '../tableComponent';
import UpdateModalComponent from '../updateModalComponent';

export default function UpdateAuthorModalComponent(props: UpdateModalComponentProps) {
    const [firstName, setFirstName] = useState<string>('');
    const [lastName, setLastName] = useState<string>('');
    const [birthdate, setBirthdate] = useState<Date | undefined>(undefined);

    async function loadAuthor(): Promise<void> {
        let author = await get<AuthorModel>(`api/Authors/${props.id}`);

        setFirstName(author.firstName);
        setLastName(author.lastName);
        setBirthdate(author.birthdate);
    }

    useEffect(() => {
        if (!props.modalProps.isOpen) {
            return;
        }

        loadAuthor();
    }, [props.modalProps.isOpen])

    function updateGenre(id: number): Promise<AuthorModel> {
        let author: AuthorModel = {
            id: id,
            firstName: firstName,
            lastName: lastName,
            birthdate: birthdate
        };

        return post('api/Genres', author);
    }

    return (
        <UpdateModalComponent<AuthorModel> id={props.id} loadData={props.loadData} modalState={props.modalProps} update={updateGenre}>
            <InputGroup >
                <Input placeholder='Имя' value={firstName} onChange={(e) => { setFirstName(e.target.value) }} />
            </InputGroup >
            <br />
            <InputGroup>
                <Input placeholder='Фамилия' value={lastName} onChange={(e) => { setLastName(e.target.value) }} />
            </InputGroup>
            <br />
            <InputGroup>
                <Input placeholder='Дата рождения' value={birthdate?.toString()} onChange={(e) => { setBirthdate(e.target.valueAsDate ?? new Date()) }} />
            </InputGroup>
        </UpdateModalComponent>
    )
}
