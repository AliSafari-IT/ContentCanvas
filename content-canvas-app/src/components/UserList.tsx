import React, { useState, useEffect } from 'react';
import { IUser } from '../interfaces/iUser';
import { IUserRole } from '../interfaces/iUserRole';
import { IRole } from '../interfaces/iRole';

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
    if (loading) return <p>Loading users...</p>;

    return (
        <div>
            <h2 className='panel-heading'>User List</h2>
            <table border={1} cellPadding={5} cellSpacing={0} width="100%" >
                <thead className='theadDark'>
                    <tr>
                        <th>#</th>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>Username</th>
                        <th>Email</th>
                        <th>Roles</th>
                        <th>Created On</th>
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
