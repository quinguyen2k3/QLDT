import { useNavigate } from 'react-router-dom';

function BackButton() {
    const navigate = useNavigate();
    return (
        <button className="btn btn-outline-primary my-3" onClick={() => navigate(-1)}>
            ← Quay lại
        </button>
    );
}

export default BackButton;
