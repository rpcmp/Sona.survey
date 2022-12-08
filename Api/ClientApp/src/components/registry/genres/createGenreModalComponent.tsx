import React, { useState } from 'react';
import { InputGroup, Input } from 'reactstrap';
import { put } from '../../client';
import { GenreModel } from '../../models';
import CreateModalComponent from '../createModalComponent';
import { CreateModalComponentProps, ModalProps } from '../tableComponent';

export default function CreateGenreModalComponent(props: CreateModalComponentProps) {
    const [name, setName] = useState<string>('');

    function addGenre(): Promise<GenreModel> {
        let genre: GenreModel = {
            id: 0,
            name: name
        };

        return put('api/Genres', genre);
    }

    return (
        <CreateModalComponent<GenreModel> loadData={props.loadData} modalState={props.modalProps} add={addGenre}>
            <InputGroup >
                <Input placeholder='Название' onChange={(e) => { setName(e.target.value) }} />
            </InputGroup >
        </CreateModalComponent>
    )
}
