import { useEffect, useState } from 'react';
import './App.css';

function App() {
  const [omnivaPakiautomaadid, setOmnivaPakiautomaadid] = useState([]);
  const [smartpostPakiautomaadid, setSmartpostPakiautomaadid] = useState([]);

  useEffect(() => {
    fetch("https://localhost:4444/parcelmachine/omniva")
      .then(res => res.json())
      .then(json => setOmnivaPakiautomaadid(json));

    fetch("https://localhost:4444/parcelmachine/smartpost")
      .then(res => res.json())
      .then(json => setSmartpostPakiautomaadid(json));
  }, []);

  return (
    <div className="App">
      <h2>Omniva Pakiautomaadid</h2>
      <select>
        {omnivaPakiautomaadid.map(automaat => 
          <option key={automaat.NAME}>
            {automaat.NAME}
          </option>
        )}
      </select>
      
      <h2>SmartPost Pakiautomaadid</h2>
      <select>
        {smartpostPakiautomaadid.map(automaat => 
          <option key={automaat.NAME}>
            {automaat.NAME}
          </option>
        )}
      </select>
    </div>
  );
}

export default App;
