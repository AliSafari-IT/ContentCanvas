import React, { useState, FC } from 'react';

type SortableColumnHeaderProps = {
  label: string;
  sortColumnIndex: number;
  onSort: (columnIndex: number, direction: ColumnSortDirection) => void;
};

type ColumnSortDirection = 'asc' | 'desc';

const SortableColumnHeader: FC<SortableColumnHeaderProps> = ({
  label,
  sortColumnIndex,
  onSort,
}) => {
  const [ascending, setAscending] = useState(true);

  const handleClick = () => {
    const newSortDirection: ColumnSortDirection = ascending ? 'desc' : 'asc';
    onSort(sortColumnIndex, newSortDirection);
    setAscending(prevState => !prevState);
  };

  const sortText = ascending ? '⬆️' : '⬇️';

  return (
    <div onClick={handleClick} style={{ cursor: 'pointer',  display: 'flex', justifyContent: 'space-between' }} className='SortableColumnHeader-items'>
      <span>{label}</span>
      {sortText}
    </div>
  );
};

export default SortableColumnHeader;
