import { createContext, useContext, useState } from "react";

import MessageDisplay from "../components/common/message/Message";

const MessageContext = createContext();

export const useMessage = () => useContext(MessageContext);

export const MessageContextProvider = ({ children }) => {
  const [message, setMessage] = useState(null);
  const [isSuccess, setIsSuccess] = useState(true);

  const showMessage = (msg, isSuccess = true) => {
    setMessage(msg);
    setIsSuccess(isSuccess);

    setTimeout(() => {
      setMessage(null);
    }, 8000);
  };

  return (
    <MessageContext.Provider value={{ showMessage }}>
      {children}
      {message && (
        <MessageDisplay
          message={message}
          isSuccess={isSuccess}
          onClose={() => setMessage(null)}
        />
      )}
    </MessageContext.Provider>
  );
};
