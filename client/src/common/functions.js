export function formatDateAndTime(dateTimeString) {
  return new Date(dateTimeString + "Z").toLocaleString("en-GB", {
    day: "2-digit",
    month: "long",
    year: "numeric",
    hour: "2-digit",
    minute: "2-digit",
    hour12: true,
  });
}

export function getDateTimeNow() {
  return new Date().toISOString().slice(0, 23);
}
