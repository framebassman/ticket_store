import React from 'react';
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import TurnstileMenu from './components/turnstile/TurnstileMenu';
import Camera from './components/turnstile/camera/TurnstileCamera';
import Manual from './components/turnstile/manual/TurnstileManual';
import App from './App';
import { FirstLoaderRemover } from './components/core/FirstLoaderRemover/FirstLoaderRemover';
const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
  },
  {
    path: "/turnstile",
    element: <TurnstileMenu />,
    children: [
      {
        path: "camera",
        element: <Camera />,
      },
      {
        path: "manual",
        element: <Manual />,
      },
    ]
  },
]);

export const WebRouter = () => {
  return (
    <>
      <FirstLoaderRemover />
      <RouterProvider router={router} />
    </>
  )
}
