import React, {useRef} from 'react';
import { useNavigate } from 'react-router-dom';
import useDataTable from '@/hooks/DataTable';

const DataTable = ({
    data = [],
    title = 'Danh sách',
    columnMap = {},
    columnHidden = [],
    detailLinkPrefix = '',
    updateLinkPrefix = '',
    tableId = 'tabledata',
    showActions = true,
    enableMultiSelect = false,
    initialSelectedIds,
    onSelectedChange,
}) => {
    
    const navigate = useNavigate();

    useDataTable({
        data,
        columnMap,
        columnHidden,
        detailLinkPrefix,
        updateLinkPrefix,
        showActions,
        enableMultiSelect,
        onSelectedChange,
        initialSelectedIds,
        tableId,
        navigate,
    });
    //Render bảng ra file html
    return (
        <div className="card">
            <div className="card-header">
                <h3 className="card-title">{title}</h3>
            </div>
            <div className="card-body">
                <table id={tableId} className="table table-bordered table-striped text-center" />
            </div>
        </div>
    );
};

export default DataTable;
