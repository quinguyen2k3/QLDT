import React from 'react';

const Preloader = ({ src, alt = 'Logo' }) => {
    return (
        <div className="preloader flex-column justify-content-center align-items-center">
            <img className="animation__shake" src={src} alt={alt} height={150} width={150} />
            <div
                className="spinner-border text-success mt-4"
                role="status"
                style={{ width: '2.5rem', height: '2.5rem' }}
            ></div>
        </div>
    );
};

export default Preloader;
