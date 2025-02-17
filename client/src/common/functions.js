export function formatUtcDateAndTime(dateTimeString) {
  return new Date(dateTimeString + "Z").toLocaleString("en-GB", {
    day: "2-digit",
    month: "long",
    year: "numeric",
    hour: "2-digit",
    minute: "2-digit",
    hour12: true,
  });
}

export function formatUtcDateAndTimeForFormData(dateTimeString) {
  const localDate = new Date(dateTimeString + "Z");

  const year = localDate.getFullYear();
  const month = String(localDate.getMonth() + 1).padStart(2, "0");
  const day = String(localDate.getDate()).padStart(2, "0");
  const hour = String(localDate.getHours()).padStart(2, "0");
  const minute = String(localDate.getMinutes()).padStart(2, "0");
  const second = String(localDate.getSeconds()).padStart(2, "0");

  return `${year}-${month}-${day}T${hour}:${minute}:${second}`;
}

export function formatLocalDateAndTime(dateTimeString) {
  return new Date(dateTimeString).toLocaleString("en-GB", {
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
