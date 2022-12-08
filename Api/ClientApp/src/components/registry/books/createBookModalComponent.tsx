import React, { useState, useEffect } from 'react';
import { InputGroup, Input, InputGroupText } from 'reactstrap';
import { get, put } from '../../client';
import { AuthorModel, BookModel, GenreModel } from '../../models';
import CreateModalComponent from '../createModalComponent';
import { CreateModalComponentProps, ModalProps } from '../tableComponent';

export default function CreateBookModalComponent(props: CreateModalComponentProps) {
    const [title, setTitle] = useState<string>('');
    const [year, setYear] = useState<number>(0);
    const [genreId, setGenreId] = useState<number>(0);
    const [authorId, setAuthorId] = useState<number>(0);

    const [authors, setAuthors] = useState<AuthorModel[]>([]);
    const [genres, setGenres] = useState<GenreModel[]>([]);

    useEffect(() => {
        loadAuthors();
        loadGenres();
    }, [])

    async function loadAuthors(): Promise<void> {
        let authors = await get<AuthorModel[]>('api/Authors');
        setAuthors(authors);

        if (authors.length > 0) {
            setAuthorId(authors[0].id as number);
        }
    }

    async function loadGenres(): Promise<void> {
        let genres = await get<GenreModel[]>('api/Genres');
        setGenres(genres);

        if (genres.length > 0) {
            setGenreId(genres[0].id as number);
        }
    }

    function addBook(): Promise<BookModel> {
        let book: BookModel = {
            id: 0,
            title: title,
            year: year,
            author: authors.find(x => x.id == authorId) as AuthorModel,
            genre: genres.find(x => x.id == genreId) as GenreModel
        };

        return put('api/Books', book);
    }

    return (
        <CreateModalComponent<BookModel> loadData={props.loadData} modalState={props.modalProps} add={addBook}>
            <InputGroup>
                <Input placeholder='Название' onChange={(e) => setTitle(e.target.value)} />
            </InputGroup>
            <br />
            <InputGroup>
                <Input placeholder='Год' pattern='[0-9]*' value={year} type='number' onChange={(e) => setYear(e.target.valueAsNumber)} />
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
        </CreateModalComponent>
    )
}
