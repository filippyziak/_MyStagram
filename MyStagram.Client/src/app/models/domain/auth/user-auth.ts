
export interface UserAuth {
    id: string;
    email: string;
    userName: string;
    name: string;
    surname: string;
    description: string;
    photoUrl: string;
    created: Date;
    emailConfirmed: boolean;
    isPrivate: boolean;
    // photos?: Photo[];
}