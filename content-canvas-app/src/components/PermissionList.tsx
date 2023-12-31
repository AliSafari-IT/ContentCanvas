import React, { useEffect, useState } from 'react';
import NewPermissionForm from './NewPermissionForm';
import { IPermission } from '../interfaces/iPermission';

const PermissionList: React.FC = () => {
  const [permissions, setPermissions] = useState<IPermission[]>([]);
  const [showModal, setShowModal] = useState(false);
  const [deleteConfirmation, setDeleteConfirmation] = useState(false);
  const [permissionToDelete, setPermissionToDelete] = useState<IPermission | null>(null);
  const [isEditingAction, setIsEditingAction] = useState(false);
  const [isAddingAction, setIsAddingAction] = useState(false);
  const [isDeletingAction, setIsDeletingAction] = useState(false);
  const [currentPermission, setCurrentPermission] = useState<IPermission | null>(null);

  const endpoint = 'http://localhost:56596/api/Permissions';

  useEffect(() => {
    fetchPermissions();
  }, []);

  const fetchPermissions = async () => {
    try {
      const response = await fetch(endpoint);
      const data = await response.json();
      setPermissions(data);
    } catch (error) {
      console.error('Error fetching permissions:', error);
    }
  };

  useEffect(() => {
    if (isDeletingAction) {
      console.log('isDeleting:', isDeletingAction);
    }
  }, [isDeletingAction]);

  useEffect(() => {
    if (isEditingAction) {
      console.log('isEditing:', isEditingAction);
    }
  }, [isEditingAction]);

  useEffect(() => {
    if (isAddingAction) {
      console.log('isAddingAction:', isAddingAction);
    }
  }, [isAddingAction]);

  const handleDeletePermission = async (permission: IPermission) => {
    if (window.confirm('Are you sure you want to delete this permission?')) {
      try {
        await fetch(`${endpoint}/${permission.id}`, { method: 'DELETE' });
        await fetchPermissions(); // Refresh the list after delete
      } catch (error) {
        console.error('Error deleting permission:', error);
      }
    }
  };

  const confirmDelete = async () => {
    if (permissionToDelete) {
      try {
        console.log('confirmDelete fetch for:', permissionToDelete);
        await fetch(endpoint + `/${permissionToDelete.id}`, {
          method: 'DELETE',
        });
        console.log('After fetch:', 'Permission deleted successfully');
        setPermissions(permissions.filter((p) => p.id !== permissionToDelete.id));
      } catch (error) {
        console.error('Error deleting permission:', error);
      }
    }
    console.log('Before setting delete confirmation:', deleteConfirmation);
    setDeleteConfirmation(false);
    console.log('After setting delete confirmation:', deleteConfirmation);
    setPermissionToDelete(null);
    setIsDeletingAction(false);
  };

  const cancelDelete = () => {
    setDeleteConfirmation(false);
    setPermissionToDelete(null);
  };

  const openModal = (actionType?: string) => {
    if (actionType === 'edit') {
      setIsEditingAction(true);
    }
    if (actionType === 'add') {
      setIsAddingAction(true);
    }
    setCurrentPermission(null); // Reset current permission for new additions
    setShowModal(true);
  };

  const closeModal = () => {
    setShowModal(false);
    setCurrentPermission(null);
  };
  const handleEditPermission = (permission: IPermission) => {
    setCurrentPermission(permission);
    setShowModal(true);
  };

  const handleFormSubmit = async (obj: IPermission) => {
    const jsonObj = JSON.stringify(obj);
    console.log('handleFormSubmit obj: IPermission', obj);
    const method = currentPermission ? 'PUT' : 'POST';
    const url = currentPermission ? `${endpoint}/${obj.id}` : endpoint;
 
    try {
      const response = await fetch(url, {
        method: method,
        headers: { 'Content-Type': 'application/json' },
        body: jsonObj,
      });

      if (!response.ok) {
        throw new Error(`HTTP error! Status: ${response.status}`);
      }

      await fetchPermissions(); // Refresh the list after add/edit
    } catch (error) {
      console.error(`Error ${currentPermission ? 'updating' : 'adding'} permission:`, error);
    }

    closeModal();
  };


  return (
    <div>
      <div className="permission-list-header">
        <h2>Permission List</h2>
        <span
          role="img"
          aria-label="add"
          onClick={() => openModal('add')}
          style={{ cursor: 'pointer', marginRight: '10px' }}
          title='Add new permission'
        >
          ➕ Add
        </span>
      </div>
      {showModal && (
        <div className="modal">
          <div className="modal-content">
            <span className="close" onClick={closeModal}>
              &times;
            </span>

            {currentPermission ? <NewPermissionForm
              onEditPermission={handleFormSubmit}
              initialPermission={currentPermission}
            /> :
              <NewPermissionForm
                onAddPermission={handleFormSubmit} />}
          </div>
        </div>
      )}
      <table>
        <thead>
          <tr>
            <th>#</th>
            <th>Name</th>
            <th>Description</th>
            <th>Action Type</th>
            <th>Resource</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {permissions.map((permission, i) => (
            <tr key={permission.id}>
              <td>{i + 1}</td>
              <td>{permission.name}</td>
              <td>{permission.description}</td>
              <td>{permission.actionType}</td>
              <td>{permission.resource}</td>
              <td>
                <span
                  role="img"
                  aria-label="edit"
                  onClick={() => handleEditPermission(permission)}
                  style={{ cursor: 'pointer', marginRight: '10px' }}
                >
                  ✏️
                </span>
                <span
                  role="img"
                  aria-label="delete"
                  onClick={() => handleDeletePermission(permission)}
                  style={{ cursor: 'pointer' }}
                >
                  ❌
                </span>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      {deleteConfirmation && (
        <div className="modal">
          <div className="modal-content">
            <p>Are you sure you want to delete this permission?</p>
            <button onClick={confirmDelete}>Yes</button>
            <button onClick={cancelDelete}>No</button>
          </div>
        </div>
      )}
    </div>
  );
}
export default PermissionList;