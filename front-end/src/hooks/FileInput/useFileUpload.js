import { useState, useEffect } from 'react';

export default function useFileUpload(initialFiles = []) {
  const [uploadedFiles, setUploadedFiles] = useState(initialFiles);
  const [newFiles, setNewFiles] = useState([]);

  useEffect(() => {
    setUploadedFiles(initialFiles || []);
  }, [initialFiles]); 

  const addNewFiles = (files) => {
    setNewFiles(prev => [...prev, ...files]);
  };

  const removeUploaded = (index) => {
    setUploadedFiles(prev => prev.filter((_, i) => i !== index));
  };

  const removeNew = (index) => {
    setNewFiles(prev => prev.filter((_, i) => i !== index));
  };

   const clearFiles = () => {
        setUploadedFiles([]);
        setNewFiles([]);
    };

  return {
    uploadedFiles,
    newFiles,
    addNewFiles,
    removeUploaded,
    removeNew,
    clearFiles,
    setUploadedFiles,
  };
}
