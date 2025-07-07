import { useLocation } from 'react-router-dom';

export default function useFormMode(path = '/update', title = { add: '', edit: '' }) {
  const location = useLocation();
  const isEditMode = location.pathname.includes(path);
  const pageTitle = isEditMode ? title.edit : title.add;

  return { pageTitle };
}
