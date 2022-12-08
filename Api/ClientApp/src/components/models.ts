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