import React, { useEffect } from "react";
import { useSelector, useDispatch } from "react-redux";
import { hideNotification } from "./../Store/notificationSlice";

const NotificationBanner = () => {
  const dispatch = useDispatch();
  const { open, message, severity, duration } = useSelector((state) => state.notification);

  useEffect(() => {
    if (open) {
      const timer = setTimeout(() => {
        dispatch(hideNotification());
      }, duration);

      return () => clearTimeout(timer);
    }
  }, [open, duration]);

  return (
    <div hidden={!open} className={`toast-alert ${severity === "success" ? "toast-alert-success" : "toast-alert-danger"}`} role="alert">
      <b>{message}</b>
    </div>
  );
};

export default NotificationBanner;
