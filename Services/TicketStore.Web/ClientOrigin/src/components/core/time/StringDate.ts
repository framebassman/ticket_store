export function formatDateFromString(origin: string): Date {
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
