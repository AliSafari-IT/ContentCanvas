import React, { useState } from 'react';
import { IPermission } from '../interfaces/iPermission';
import { v4 as uuidv4 } from 'uuid';
interface NewPermissionFormProps {
  onAddPermission?: (permission: IPermission) => void;
  onEditPermission?: (permission: IPermission) => void;
  initialPermission?: IPermission;
}

const NewPermissionForm: React.FC<NewPermissionFormProps> = ({ onAddPermission, onEditPermission, initialPermission }) => {
  const [idObject, ] = useState(initialPermission?.idObject || uuidv4());
  const [name, setName] = useState(initialPermission?.name || '');
  const [description, setDescription] = useState(initialPermission?.description || '');
  const [actionType, setActionType] = useState(initialPermission?.actionType || '');
  const [resource, setResource] = useState(initialPermission?.resource || '');

  const handleNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setName(event.target.value);
  };

  const handleDescriptionChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setDescription(event.target.value);
  };

  const handleActionTypeChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setActionType(event.target.value);
  };

  const handleResourceChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setResource(event.target.value);
  };

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    const newPermission: IPermission = {
      name,
      description,
      actionType,
      resource,
      idObject,    
    };
    if(initialPermission){
      newPermission.id = initialPermission.id;
    }
    console.log('New Permission:', newPermission);

    onAddPermission && onAddPermission(newPermission);
    onEditPermission && onEditPermission(newPermission);

    // Reset the form inputs
    setName('');
    setDescription('');
    setActionType('');
    setResource('');
  };

  return (
    <form onSubmit={handleSubmit} className="new-permission-form">
      <div className="form-group">
        <label htmlFor="name" className='form-label'>Name:</label>
        <input type="text" id="name" value={name} onChange={handleNameChange} className='form-input' />
      </div>
      <div className="form-group">
        <label htmlFor="description" className='form-label'>Description:</label>
        <input type="text" id="description" value={description} onChange={handleDescriptionChange} className='form-input' />
      </div>
      <div className="form-group">
        <label htmlFor="actionType" className='form-label'>Action Type:</label>
        <input type="text" id="actionType" value={actionType} onChange={handleActionTypeChange} className='form-input' />
      </div>
      <div className="form-group">
        <label htmlFor="resource" className='form-label'>Resource:</label>
        <input type="text" id="resource" value={resource} onChange={handleResourceChange} className='form-input' />
      </div>
      <button type="submit" className={initialPermission ? 'update-button' : 'add-button'}>{initialPermission ? 'Update Permission' : 'Add Permission'}</button>
    </form>
  );
};

export default NewPermissionForm;