import React from "react";

export const TicketInfo = ({ label }: { label: string, status?: string }) => {
  return (
    <div id="ticket-info" className="info">
      <span><b>Event:</b> {label}</span>
    </div>
  )
};
