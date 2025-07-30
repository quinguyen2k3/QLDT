import React from 'react';
import { BarLoader } from 'react-spinners';

const Preloader = ({ src, alt = 'Logo' }) => {
    return (
        <div className="preloader flex-column justify-content-center align-items-center">
            <img src={src} alt={alt} height={150} width={150} style={{ marginBottom: '20px' }} />
            <BarLoader heighrt={4} width={150} color={'#00962d'} speedMultiplier={1} />
        </div>
    );
};

export default Preloader;
