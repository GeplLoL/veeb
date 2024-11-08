import React, { useState } from "react";
import "./App.css";

function App() {
  const [response, setResponse] = useState("");

  // Käsitlejad igale kontrollerile
  const handleToodeGet = async () => {
    const res = await fetch("https://localhost:4444/toode");
    const data = await res.json();
    setResponse(JSON.stringify(data, null, 2));
  };

  const handleToodeUpdatePrice = async () => {
    const res = await fetch("https://localhost:4444/toode/suurenda-hinda");
    const data = await res.json();
    setResponse(JSON.stringify(data, null, 2));
  };

  const handleTootedAdd = async (event) => {
    event.preventDefault();
    const formData = new FormData(event.target);
    const newToode = {
      id: formData.get("id"),
      name: formData.get("name"),
      price: parseFloat(formData.get("price")),
      isActive: formData.get("isActive") === "true",
    };
    const res = await fetch("https://localhost:4444/tooted/lisa", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(newToode),
    });
    const data = await res.json();
    setResponse(JSON.stringify(data, null, 2));
  };

  const handlePrimitiividHello = async (name) => {
    const res = await fetch(`https://localhost:4444/primitiivid/hello-variable/${name}`);
    const data = await res.text();
    setResponse(data);
  };

  return (
    <div className="App">
      <h1>Kontrollerite kasutamine</h1>

      <div className="controller-section">
        <h2>ToodeController</h2>
        <button onClick={handleToodeGet}>Kuva praegune toode</button>
        <button onClick={handleToodeUpdatePrice}>Suurenda hinda 1 võrra</button>
      </div>

      <div className="controller-section">
        <h2>TootedController</h2>
        <form onSubmit={handleTootedAdd}>
          <label>
            ID:
            <input type="number" name="id" required />
          </label>
          <label>
            Nimi:
            <input type="text" name="name" required />
          </label>
          <label>
            Hind:
            <input type="number" step="0.01" name="price" required />
          </label>
          <label>
            Aktiivne:
            <select name="isActive" required>
              <option value="true">Jah</option>
              <option value="false">Ei</option>
            </select>
          </label>
          <button type="submit">Lisa toode</button>
        </form>
      </div>

      <div className="controller-section">
        <h2>PrimitiividController</h2>
        <form
          onSubmit={(e) => {
            e.preventDefault();
            const name = e.target.elements.name.value;
            handlePrimitiividHello(name);
          }}
        >
          <label>
            Nimi:
            <input type="text" name="name" required />
          </label>
          <button type="submit">Tervita</button>
        </form>
      </div>

      <div className="response-section">
        <h2>Serveri vastus:</h2>
        <pre>{response}</pre>
      </div>
    </div>
  );
}

export default App;
