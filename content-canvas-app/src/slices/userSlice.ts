// src/features/userSlice.ts
import { createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IUser } from '../interfaces/iUser';
import { IRole } from '../interfaces/iRole';
import { IUserRole } from '../interfaces/iUserRole';

interface UserState {
    users: IUser[];
    roles: IRole[];
    userRoles: IUserRole[];
    loading: boolean;
    sort: {
        columnIndex: number | null;
        direction: 'asc' | 'desc' | null;
    };
}

const initialState: UserState = {
    users: [],
    loading: false,
    sort: {
        columnIndex: null,
        direction: null,
    },
    userRoles: [],
    roles: [],
};
export const fetchUsers = createAsyncThunk<IUser[]>(
    'user/fetchUsers',
    async () => {
      const response = await fetch('http://localhost:56596/api/Users');
      const users = await response.json();
      return users as IUser[];
    }
  );
  
  export const fetchRoles = createAsyncThunk<IRole[]>(
    'user/fetchRoles',
    async () => {
      const response = await fetch('http://localhost:56596/api/Roles');
      const roles = await response.json();
      return roles as IRole[];
    }
);

export const fetchUserRoles = createAsyncThunk<IUserRole[]>(
    'user/fetchUserRoles',
    async () => {
        const response = await fetch('http://localhost:56596/api/UserRoles');
        const userroles = await response.json();
        return userroles as IUserRole[];    }
);

export const userSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {
        // Reducers for other user actions
        sortUsers: (state, action: PayloadAction<{ columnIndex: number; direction: 'asc' | 'desc' }>) => {
            const { columnIndex, direction } = action.payload;
            state.sort = { columnIndex, direction };
            // Implement the sorting logic here
            state.users = getUsersSorted(state.users, columnIndex, direction);
        },

    },
    extraReducers: (builder) => {
        builder
            .addCase(fetchUsers.pending, (state) => {
                state.loading = true;
            })
            .addCase(fetchUsers.rejected, (state) => {
                state.loading = false;
                // Handle the error state
            }).addCase(fetchUsers.fulfilled, (state, action) => {
                state.users = action.payload.map((user, index) => ({
                    ...user,
                    index: index + 1
                }));
                state.loading = false;
            }).addCase(fetchRoles.fulfilled, (state, action) => {
                state.roles = action.payload;
            })
            .addCase(fetchUserRoles.fulfilled, (state, action) => {
                state.userRoles = action.payload;
            });
    },
});

// Implement a function to sort users based on column index and direction
function getUsersSorted(users: IUser[], columnIndex: number, direction: 'asc' | 'desc'): IUser[] {
    return users.sort((a, b) => {
        let valueA: any, valueB: any;

        // For Roles (assuming it's an array of strings)
        if (columnIndex === 5) {
            // Concatenate role names into a single string for comparison
            const rolesA = a.roleNames?.join(', ') ?? '';
            const rolesB = b.roleNames?.join(', ') ?? '';

            return direction === 'asc' ? rolesA.localeCompare(rolesB) : rolesB.localeCompare(rolesA);
        }

        switch (columnIndex) {
            case 0:  // Index
                valueA = a.index;
                valueB = b.index;
                break;
            case 1: // First Name
                valueA = a.firstName;
                valueB = b.firstName;
                break;
            case 2: // Last Name
                valueA = a.lastName;
                valueB = b.lastName;
                break;
            case 3: // Username
                valueA = a.username;
                valueB = b.username;
                break;
            case 4: // Email
                valueA = a.email;
                valueB = b.email;
                break;
                case 6: // Created On
                valueA = new Date(a.createdOn);
                valueB = new Date(b.createdOn);
                break;
            default:
                // Default case if no valid columnIndex is provided
                return 0;
        }

        if (typeof valueA === 'string' && typeof valueB === 'string') {
            return direction === 'asc' ? valueA.localeCompare(valueB) : valueB.localeCompare(valueA);
        }

        if (typeof valueA === 'number' && typeof valueB === 'number') {
            return direction === 'asc' ? valueA - valueB : valueB - valueA;
        }

        return 0;
    });
};

export const { sortUsers } = userSlice.actions;
export default userSlice.reducer;
