import { useNavigate } from 'react-router-dom';
import { FaArrowLeft } from 'react-icons/fa';

function BackButton() {
  const navigate = useNavigate();
  return (
    <button className="btn btn-outline-primary my-3" onClick={() => navigate(-1)}>
      <FaArrowLeft className="mr-2" /> Quay lại
    </button>
  );
}

export default BackButton;
