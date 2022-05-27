export interface ResponseDetails<T> {
    responseStatus: responseStatus,
    responseData: T,
    error: string,
    responseMessage: string
}

export enum responseStatus {
    success = 1,
    notfound = 2,
    error = 3,
    badRequest = 4
}

