import React, { useEffect } from 'react';
import SortableColumnHeader from '../utilityComponents/SortableColumnHeader ';
import { useDispatch, useSelector } from 'react-redux';
import { AppDispatch, RootState } from '../store';
import { fetchUsers, sortUsers } from '../slices/userSlice';

const UserList: React.FC = () => {
    const dispatch = useDispatch<AppDispatch>();
    const { users, loading } = useSelector((state: RootState) => state.user);
    const handleSort = (columnIndex: number, direction: 'asc' | 'desc') => {
        dispatch(sortUsers({ columnIndex, direction }));
    };

    useEffect(() => {
        dispatch(fetchUsers());
        // Dispatch fetchRoles and fetchUserRoles as well
    }, [dispatch]);

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
                    {users.map((user) => (
                        <tr key={user.idObject}>
                            <td>{user.index}</td>
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
