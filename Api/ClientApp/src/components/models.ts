export interface Entity {
    id: number
}

export interface BookModel extends Entity {
    title: string,
    year: number,
    genre: GenreModel,
    author: AuthorModel,

}

export interface GenreModel extends Entity {
    name: string
}

export interface AuthorModel extends Entity {
    firstName: string,
    lastName: string,
    birthdate?: Date
}

export interface ModalProps {
    isOpen: boolean;
    setIsOpen: (a: boolean) => void;

}

export interface TextModalProps extends ModalProps {
    setModalText: (text: string) => void;
}