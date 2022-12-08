import React, { useState, useEffect } from 'react';
import { InputGroup, Input, InputGroupText } from 'reactstrap';
import { get, post } from '../../client';
import { AuthorModel, BookModel, GenreModel } from '../../models';
import { ModalProps, UpdateModalComponentProps } from '../tableComponent';
import UpdateModalComponent from '../updateModalComponent';

export default function UpdateBookModalComponent(props: UpdateModalComponentProps) {
    const [title, setTitle] = useState<string>('');
    const [year, setYear] = useState<number>(0);
    const [genreId, setGenreId] = useState<number>(0);
    const [authorId, setAuthorId] = useState<number>(0);

    const [authors, setAuthors] = useState<AuthorModel[]>([]);
    const [genres, setGenres] = useState<GenreModel[]>([]);

    useEffect(() => {
        if (!props.modalProps.isOpen) {
            return;
        }

        loadBook();
        loadAuthors();
        loadGenres();
    }, [props.modalProps.isOpen])

    async function loadBook() {
        let book = await get<BookModel>(`api/Books/${props.id}`);

        setTitle(book.title);
        setYear(book.year);
        setGenreId(book.genre.id);
        setAuthorId(book.author.id);
    }

    async function loadAuthors(): Promise<void> {
        let authors = await get<AuthorModel[]>('api/Authors');
        setAuthors(authors);
    }

    async function loadGenres(): Promise<void> {
        let genres = await get<GenreModel[]>('api/Genres');
        setGenres(genres);
    }

    function updateBook(id: number): Promise<BookModel> {
        let book: BookModel = {
            id: id,
            title: title,
            year: year,
            author: authors.find(x => x.id == authorId) as AuthorModel,
            genre: genres.find(x => x.id == genreId) as GenreModel
        };

        return post('api/Authors', book);
    }

    return (
        <UpdateModalComponent<BookModel> id={props.id} loadData={props.loadData} modalState={props.modalProps} update={updateBook}>
            <InputGroup>
                <Input placeholder='Название' value={title} onChange={(e) => setTitle(e.target.value)} />
            </InputGroup>
            <br />
            <InputGroup>
                <Input placeholder='Год' pattern='[0-9]*' value={year} type='number' onChange={(e) => setYear(parseInt(e.target.value))} />
            </InputGroup>
            <br />
            <InputGroup>
                <InputGroupText>Жанр</InputGroupText>
                <Input type='select' value={genreId} onChange={e => setGenreId(parseInt(e.target.value))}>
                    {genres.map(x => <option key={x.id} value={x.id}>{x.name}</option>)}
                </Input>
            </InputGroup>
            <br />
            <InputGroup>
                <InputGroupText>Автор</InputGroupText>
                <Input type='select' value={genreId} onChange={e => setAuthorId(parseInt(e.target.value))}>
                    {authors.map(x => <option key={x.id} value={x.id}>{`${x.firstName} ${x.lastName}`}</option>)}
                </Input>
            </InputGroup>
        </UpdateModalComponent>
    )
}
