
export class ApiError extends Error {
  public statusCode: any;
  public originalError: any;
  constructor(orinalError: any, statusCode: any, message?: string, ) {
      super(message);
      this.name = this.verifyTypeError(orinalError);
      this.originalError = orinalError;
      this.statusCode = statusCode;
  }

  private verifyTypeError(orinalError: Object) {
    if (orinalError) {
      if (orinalError.hasOwnProperty('errors')) {
        return orinalError['errors'][0].message
      }
      else if (orinalError.hasOwnProperty('details')) {
        return orinalError['details'][0].message
      }
      else{
        return orinalError
      }
    }
  }
}