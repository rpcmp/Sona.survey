import React, { useState } from 'react';
import { get, remove } from '../../client';
import { AuthorModel } from '../../models';
import TableComponent from '../tableComponent';
import CreateAuthorModalComponent from './createAuthorModalComponent';
import UpdateAuthorModalComponent from './updateAuthorModalComponent';

export default function AuthorTableComponent() {
    const headers: string[] = ['Фамилия', 'Имя', 'Дата рождения']

    function getAuthors(): Promise<AuthorModel[]> {
        return get('/api/Authors');
    }

    function deleteAuthor(id: number): Promise<void> {
        return remove(`api/Authors/${id}`);
    }

    function map(author: AuthorModel): JSX.Element {
        return (
            <>
                <td>{author.lastName}</td>
                <td>{author.firstName}</td>
                <td>{author.birthdate?.toString()}</td>
            </>
        )
    }

    return (
        <TableComponent<AuthorModel>
            name='authors'
            headers={headers}

            get={getAuthors}
            delete={deleteAuthor}

            mapRow={map}
            addModal={CreateAuthorModalComponent}
            updateModal={UpdateAuthorModalComponent}
        />
    )
}