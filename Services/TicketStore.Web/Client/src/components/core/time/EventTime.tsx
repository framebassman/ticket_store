import React from 'react';
import Typography from '@material-ui/core/Typography';
import Box from '@material-ui/core/Box';

export interface EventTimeProps {
  origin: string
}

const monthNames = ["января", "февраля", "март", "апреля", "мая", "июня",
  "июля", "августа", "сентября", "октября", "ноября", "декабря"
];

export function calculateWhen(startedAt: Date): String {
  const year = startedAt.getFullYear();
  const date = startedAt.getDate();
  const month = monthNames[startedAt.getMonth()];
  return `${date} ${month} ${year}`;
}

export function calculateStart(startedAt: Date): String {
  const hoursTimezoneOffset = startedAt.getTimezoneOffset() / 60;
  const hoursUtc = startedAt.getUTCHours();
  const hoursWithoutZeros = hoursUtc - hoursTimezoneOffset;
  const minutesWithoutZeros = startedAt.getUTCMinutes();
  const hours = hoursWithoutZeros.toString().padStart(2, '0');
  const minutes = minutesWithoutZeros.toString().padStart(2, '0');
  return `Начало в ${hours}:${minutes} часов`;
}

function formatDateFromString(origin: string): Date {
  let seed: string;
  if (hasTimezone(origin)) {
    seed = origin;
  }
  else {
    seed = origin + 'Z';
  }
  return new Date(seed);
}

function hasTimezone(origin: string): boolean {
  return origin[origin.length - 1] === 'Z';
}

export function EventTime(props: EventTimeProps) {
  const { origin } = props;
  const date = formatDateFromString(origin);
  const when = calculateWhen(date);
  const start = calculateStart(date);
  return (
    <Typography style={{margin: '4px'}} component="div">
      <Box id="when" textAlign="center" fontSize="fontSize">{when}</Box>
      <Box id="start" textAlign="center" fontSize="fontSize">{start}</Box>
    </Typography>
  )
}
