import { useNavigate } from 'react-router-dom';

function BackButton() {
    const navigate = useNavigate();
    return (
        <button className="btn btn-outline-primary my-3" onClick={() => navigate('/home')}>
            ← Quay lại
        </button>
    );
}

export default BackButton;
