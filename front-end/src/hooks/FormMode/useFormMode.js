import { useLocation } from 'react-router-dom';

export default function useFormMode({ update = '', detail = '', title = { add: 'Thêm Mới', edit: 'Chỉnh Sửa', detail: 'Chi Tiết' } } = {}) {
  const location = useLocation();
  const isEditMode = update && location.pathname.includes(update);
  const isDetailMode = detail && location.pathname.includes(detail);
  const pageTitle = isDetailMode ? (title.detail || 'Chi Tiết') : 
                    isEditMode ? (title.edit || 'Chỉnh Sửa') : 
                    (title.add || 'Thêm Mới');

  return { pageTitle};
}