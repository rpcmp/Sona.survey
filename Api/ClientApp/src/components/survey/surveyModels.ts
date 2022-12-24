export enum AppointmentType {
    First = 'First',
    NotFirst = 'NotFirst'
}

export enum WalkingToesType {
    MoreThen50 = 'MoreThen50',
    LessThen50 = 'LessThen50'
}

export enum CureType {
    Pyramidal = 'Pyramidal',
    Night = 'Night'
}

export enum SuccessTherapyType {
    Success = 'Success',
    NotSuccess = 'NotSuccess'
}

export interface SurveyDto {
    firstName: string;
    middleName: string;
    lastName: string;
    birthdate: string;
    appointment: AppointmentType | null;
    walkingToes: WalkingToesType | null;
    cure: CureType | null;
    successTherapy: SuccessTherapyType | null
}