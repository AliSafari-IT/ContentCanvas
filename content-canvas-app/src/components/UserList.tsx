import React, { useState, useEffect } from 'react';
import { IUser } from '../interfaces/iUser';
import { IUserRole } from '../interfaces/iUserRole';
import { IRole } from '../interfaces/iRole';
import SortableColumnHeader from '../utilityComponents/SortableColumnHeader ';

const UserList: React.FC = () => {
    const [users, setUsers] = useState<IUser[]>([]);
    const [loading, setLoading] = useState<boolean>(true);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const allResponses = await Promise.allSettled([
                    fetch('http://localhost:56596/api/Users'),
                    fetch('http://localhost:56596/api/Roles'),
                    fetch('http://localhost:56596/api/UserRoles')
                ]);

                const [usersResponse, rolesResponse, userRolesResponse] = allResponses;

                if (usersResponse.status === 'fulfilled' && rolesResponse.status === 'fulfilled' && userRolesResponse.status === 'fulfilled') {
                    const usersData = await usersResponse.value.json() as IUser[];
                    const rolesData = await rolesResponse.value.json() as IRole[];
                    const userRolesData = await userRolesResponse.value.json() as IUserRole[];

                    const roleIdToName = rolesData.reduce((acc, role) => {
                        acc[role.idObject] = role.name;
                        return acc;
                    }, {} as { [key: string]: string });

                    const updatedUsersData = usersData.map(user => ({
                        ...user,
                        roleNames: userRolesData
                            .filter(ur => ur.userIdObject === user.idObject)
                            .map(ur => roleIdToName[ur.roleIdObject])
                            .filter(name => name)
                    }));

                    setUsers(updatedUsersData);
                } else {
                    console.log('HTTP error while fetching data');
                    throw new Error('HTTP error while fetching data');
                }
            } catch (error) {
                console.error('Error fetching data:', error);
            } finally {
                setLoading(false);
            }
        };

        fetchData();
    }, []);

    const handleSort = (columnIndex: number, direction: 'asc' | 'desc') => {
        const sortedUsers = [...users].sort((a, b) => {
            let valueA, valueB;

            // For Roles (assuming it's an array of strings)
            if (columnIndex === 5) {
                // Concatenate role names into a single string for comparison
                const rolesA = a.roleNames?.join(', ') ?? '';
                const rolesB = b.roleNames?.join(', ') ?? '';

                return direction === 'asc' ? rolesA.localeCompare(rolesB) : rolesB.localeCompare(rolesA);
            }

            switch (columnIndex) {
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

                case 6: // Created On (as date objects for proper comparison)
                    valueA = new Date(a.createdOn);
                    valueB = new Date(b.createdOn);
                    break;
                default:
                    return 0;
            }

            // For string comparison
            if (typeof valueA === 'string' && typeof valueB === 'string') {
                return direction === 'asc' ? valueA.localeCompare(valueB) : valueB.localeCompare(valueA);
            }

            // For date comparison
            if (columnIndex === 6) { // Assuming 6 is the index for date column
                const dateA = new Date(a.createdOn);
                const dateB = new Date(b.createdOn);

                // Check if both values are valid dates
                if (!isNaN(dateA.getTime()) && !isNaN(dateB.getTime())) {
                    return direction === 'asc' ? dateA.getTime() - dateB.getTime() : dateB.getTime() - dateA.getTime();
                }
            }

            return 0;
        });

        setUsers(sortedUsers);
    };



    if (loading) return <p>Loading users...</p>;

    return (
        <div>
            <h2 className='panel-heading'>User List</h2>
            <table border={1} cellPadding={5} cellSpacing={0} width="100%">
                <thead className='theadDark'>
                    <tr>
                        <th><SortableColumnHeader label="#" sortColumnIndex={0} onSort={handleSort} /></th>
                        <th><SortableColumnHeader label="First Name" sortColumnIndex={1} onSort={handleSort} /></th>
                        <th><SortableColumnHeader label="Last Name" sortColumnIndex={2} onSort={handleSort} /></th>
                        <th><SortableColumnHeader label="Username" sortColumnIndex={3} onSort={handleSort} /></th>
                        <th><SortableColumnHeader label="Email" sortColumnIndex={4} onSort={handleSort} /></th>
                        <th><SortableColumnHeader label="Roles" sortColumnIndex={5} onSort={handleSort} /></th>
                        <th><SortableColumnHeader label="Created On" sortColumnIndex={6} onSort={handleSort} /></th>
                    </tr>
                </thead>
                <tbody className='tbodyLight hoverLightGray'>
                    {users.map((user, i) => (
                        <tr key={user.idObject}>
                            <td>{i + 1}</td>
                            <td>{user.firstName}</td>
                            <td>{user.lastName}</td>
                            <td>{user.username}</td>
                            <td>{user.email}</td>
                            <td>{user?.roleNames?.join(', ')}</td>
                            <td>{new Date(user.createdOn).toLocaleString()}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default UserList;
