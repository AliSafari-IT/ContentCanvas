export interface IUser {
    idObject: string;
    firstName: string;
    lastName?: string;
    username: string;
    email: string;
    createdOn: Date; 
    roleNames?: string[]; // Array of role names or role IDs
  }
  