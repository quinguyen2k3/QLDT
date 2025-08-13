import { useLocation } from 'react-router-dom';

export default function useFormMode(update = '/update', detail = '/detail', title = { add: '', edit: '', detail: '' }) {
  const location = useLocation();
  const isEditMode = location.pathname.includes(update);
  const isDetailMode = location.pathname.includes(detail);
  const pageTitle = isDetailMode ? title.detail || 'Chi Tiết' :
                    isEditMode ? title.edit || 'Chỉnh Sửa' :
                    title.add || 'Thêm Mới';

  return { pageTitle };
}
