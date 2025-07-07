function BackButton() {
  return (
    <button className="btn btn-outline-primary" onClick={() => window.history.back()}>
      ← Quay lại
    </button>
  );
}

export default BackButton