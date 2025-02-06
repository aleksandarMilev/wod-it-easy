export function formatDate(dateString) {
  return new Date(dateString + "Z").toLocaleDateString("en-GB", {
    day: "2-digit",
    month: "long",
    year: "numeric",
  });
}

export function formatDateAndTime(dateString) {
  return new Date(dateString + "Z").toLocaleString("en-GB", {
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
