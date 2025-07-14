import React, { forwardRef, useImperativeHandle } from 'react';
import useFileUpload from '@/hooks/FileInput';

const FileInput = forwardRef(({ initialFiles = [] }, ref) => {
  const {
    uploadedFiles,
    newFiles,
    addNewFiles,
    removeUploaded,
    removeNew,
    setUploadedFiles,
    clearFiles
  } = useFileUpload(initialFiles);

  const handleChange = (e) => {
    const newSelectedFiles = Array.from(e.target.files);
    addNewFiles(newSelectedFiles);
  };

  useImperativeHandle(ref, () => ({
    uploadedFiles,
    newFiles,
    reset: () => clearFiles(),
    setUploadedFiles,
  }));

  return (
    <div className="form-group">
      <label htmlFor="multiFileUpload">
        <strong>Tệp Tin Đính Kèm</strong>
      </label>
      <div className="custom-file">
        <input
          type="file"
          name="attachments"
          className="custom-file-input"
          id="multiFileUpload"
          multiple
          onChange={handleChange}
        />
        <label className="custom-file-label" htmlFor="multiFileUpload">
          Chọn nhiều tệp
        </label>
      </div>

      {uploadedFiles.length > 0 && (
        <div className="mt-3">
          <strong>Đã upload:</strong>
          <ul className="list-unstyled">
            {uploadedFiles.map((file, index) => (
              <li key={index}>
                <a href={file.fileUrl} target="_blank" rel="noopener noreferrer">
                  {file.fileName}
                </a>
                <button
                  type="button"
                  className="btn btn-sm btn-outline-danger ml-2"
                  onClick={() => removeUploaded(index)}
                >
                  ❌
                </button>
              </li>
            ))}
          </ul>
        </div>
      )}

      {newFiles.length > 0 && (
        <div className="mt-3">
          <strong>Tệp mới:</strong>
          <ul className="list-unstyled">
            {newFiles.map((file, index) => (
              <li key={index}>
                <a
                  href={URL.createObjectURL(file)}
                  target="_blank"
                  rel="noopener noreferrer"
                >
                  {file.name}
                </a>
                <button
                  type="button"
                  className="btn btn-sm btn-outline-danger ml-2"
                  onClick={() => removeNew(index)}
                >
                  ❌
                </button>
              </li>
            ))}
          </ul>
        </div>
      )}
    </div>
  );
});

FileInput.displayName = 'FileInput';
export default FileInput;
