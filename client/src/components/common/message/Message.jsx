import './Message.css'

export default function MessageDisplay({ message, isSuccess, onClose }) {
    return (
        <div className={`message ${isSuccess ? 'success' : 'error'}`}>
            <p>{message}</p>
            <button className="close-btn" onClick={onClose}>
                &times;
            </button>
        </div>
    )
}
