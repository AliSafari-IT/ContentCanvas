// src/store.ts
import { configureStore } from '@reduxjs/toolkit';
import userReducer from '../src/slices/userSlice';

export const store = configureStore({
  reducer: {
    user: userReducer,
    // Add your reducers here
  },
  middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(/* other middleware if needed */),
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
