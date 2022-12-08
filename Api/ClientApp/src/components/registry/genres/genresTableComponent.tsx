import React, { useState } from 'react';
import { GenreModel } from '../../models';
import CreateGenreModalComponent from './createGenreModalComponent';
import TableComponent from '../tableComponent';
import UpdateGenreModalComponent from './updateGenreModalComponent';
import { get, remove } from '../../client';


export default function GenresTableComponent() {
    const headers: string[] = ['Название']

    async function getGenres(): Promise<GenreModel[]> {
        return get('/api/Genres');
    }

    async function deleteGenre(id: number): Promise<void> {
        return remove(`/api/Genres/${id}`);
    }

    function mapRow(genre: GenreModel): JSX.Element {
        return (<td>{genre.name}</td>)
    }

    return (
        <TableComponent<GenreModel>
            name='genres'
            headers={headers}

            get={getGenres}
            delete={deleteGenre}

            mapRow={mapRow}
            addModal={CreateGenreModalComponent}
            updateModal={UpdateGenreModalComponent}
        />
    )
}