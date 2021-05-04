export class BaseResponse {
    isSucceeded: boolean;
    error: Error;
}

export interface Error {
    errorCode: string;
    message: string;
}