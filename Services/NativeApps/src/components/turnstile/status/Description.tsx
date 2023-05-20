import React from "react";
import './Status.css';

export const Description = ({ message }: { message: string }) => {
  return (
    <span id="status-description" className="description">{message}</span>
  )
};
