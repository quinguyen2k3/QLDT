import React from 'react';
import { useNavigate } from 'react-router-dom';
import useDataTable from '@/hooks/DataTable';

const DataTable = ({
    data = [],
    title = 'Danh sách',
    columnMap = {},
    detailLinkPrefix = '',
    updateLinkPrefix = '',
    showActions = true,
}) => {
    
    const navigate = useNavigate();

    useDataTable({
        data,
        columnMap,
        detailLinkPrefix,
        updateLinkPrefix,
        showActions,
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
