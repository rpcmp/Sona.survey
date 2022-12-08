import React, { useState } from 'react';
import { get, remove } from '../../client';
import { BookModel } from '../../models';
import TableComponent from '../tableComponent';
import CreateBookModalComponent from './createBookModalComponent';
import UpdateBookModalComponent from './updateBookModalComponent';


export default function BooksTableComponent() {
    const headers: string[] = ['Название', 'Год', 'Жанр', 'Автор']

    async function getBooks(): Promise<BookModel[]> {
        return get('api/Books');
    }

    async function deleteBook(id: number): Promise<void> {
        return remove(`/api/Books/${id}`);
    }

    function map(book: BookModel): JSX.Element {
        return (
            <>
                <td>{book.title}</td>
                <td>{book.year}</td>
                <td>{book.genre?.name}</td>
                <td>{`${book.author?.firstName} ${book.author?.lastName}`}</td>
            </>
        )
    }

    return (
        <TableComponent<BookModel>
            name='books'
            headers={headers}

            get={getBooks}
            delete={deleteBook}

            mapRow={map}
            addModal={CreateBookModalComponent}
            updateModal={UpdateBookModalComponent}
        />
    )
}