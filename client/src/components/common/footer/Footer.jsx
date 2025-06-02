import "./Footer.css";

export default function Footer() {
  const year = new Date().getFullYear();

  return (
    <footer className="footer">
      <p>
        &copy; {year + " "}
        Wod It Easy
        <a
          href="https://github.com/aleksandarMilev/wod-it-easy"
          target="_blank"
          rel="noopener noreferrer"
        >
          Open source project
        </a>
      </p>
    </footer>
  );
}
