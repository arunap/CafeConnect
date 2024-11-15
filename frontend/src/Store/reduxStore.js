import { configureStore } from "@reduxjs/toolkit";
import notificationSlice from "./notificationSlice";

export const reduxStore = configureStore({
  reducer: {
    notification: notificationSlice,
  },
});
