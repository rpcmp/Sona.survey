import React, { useState, useEffect } from 'react';
import { InputGroup, Input } from 'reactstrap';
import { get, post } from '../../client';
import { GenreModel } from '../../models';
import { UpdateModalComponentProps } from '../tableComponent';
import UpdateModalComponent from '../updateModalComponent';

export default function UpdateGenreModalComponent(props: UpdateModalComponentProps) {
    const [name, setName] = useState<string>('');

    async function loadGenre(): Promise<void> {
        let genre = await get<GenreModel>(`api/Genres/${props.id}`);

        setName(genre.name);
    }
    
    useEffect(() => {
        if (!props.modalProps.isOpen) {
            return;
        }

        loadGenre();
    }, [props.modalProps.isOpen])

    function updateGenre(id: number): Promise<GenreModel> {
        let genre: GenreModel = {
            id: id,
            name: name
        };

        return post('api/Genres', genre);
    }

    return (
        <UpdateModalComponent<GenreModel> id={props.id} loadData={props.loadData} modalState={props.modalProps} update={updateGenre}>
            <InputGroup >
                <Input placeholder='Название' value={name} onChange={(e) => { setName(e.target.value) }} />
            </InputGroup >
        </UpdateModalComponent>
    )
}
