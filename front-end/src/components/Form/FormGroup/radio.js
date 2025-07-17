function Radio({ name, options, onChange, label }) {
    return (
        <div className="form-group">
            {label && <label className="form-label d-block">{label}</label>}
            {options.map((option) => (
                <div className="form-check" key={option.id}>
                    <input
                        className="form-check-input"
                        type="radio"
                        name={name}
                        value={option.id}
                        onChange={(e) => onChange(e.target.value)}
                    />
                    <label className="form-check-label">{option.name}</label>
                </div>
            ))}
        </div>
    );
}

export default Radio;
