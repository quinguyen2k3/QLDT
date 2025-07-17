import React from 'react';
import { useNavigate } from 'react-router-dom';
import useDataTable from '@/hooks/DataTable';

const DataTable = ({
    data = [],
    title = 'Danh sách',
    columnMap = {},
    columnHidden = [],
    detailLinkPrefix = '',
    updateLinkPrefix = '',
    showActions = true,
    enableMultiSelect = false,
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
        navigate,
    });

    //Render bảng ra file html
    return (
        <div className="card">
            <div className="card-header">
                <h3 className="card-title">{title}</h3>
            </div>
            <div className="card-body">
                <table id="tabledata" className="table table-bordered table-striped text-center" />
            </div>
        </div>
    );
};

export default DataTable;
