import { UserAccess } from './user-access';

export enum ApiRoutes {
  CreateToken = '/api/users/token',
  Register = '/api/users/register',
}

interface ApiBody {
  [ApiRoutes.CreateToken]: {
    payload: {
      username: string;
      password: string;
    };
    response: UserAccess;
  };

  [ApiRoutes.Register]: {
    payload: {
      username: string;
      email: string;
      password: string;
    };
    response: UserAccess;
  };
}

export type ApiMeta<T extends ApiRoutes> = ApiBody[T];
