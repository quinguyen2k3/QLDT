import React from 'react';

const FormHeader = ({ title }) => {
    return (
        <div
            className="card-header bg-white"
            style={{
                borderTop: '4px solid #28a745',
                borderBottom: '1px solid #dee2e6',
            }}
        >
            <h3 className="card-title">{title}</h3>
            <div className="card-tools">
                <button type="button" className="btn btn-tool" data-card-widget="collapse">
                    <i className="fas fa-minus"></i>
                </button>
            </div>
        </div>
    );
};

export default FormHeader;
