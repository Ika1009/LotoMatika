CREATE TABLE Users (
    UID INT AUTO_INCREMENT PRIMARY KEY, -- Jedinstveni identifikator korisnika
    Password VARCHAR(255) NOT NULL, -- Šifra za korisnika
    Email VARCHAR(255) NOT NULL, -- Imejl korisnika
    DeviceID VARCHAR(255) NULL, -- Serijski broj uređaja
    SecondDeviceAllowed  INT DEFAULT 0, -- Da li je dozvoljen drugi Uredjaj
    SecondDeviceID VARCHAR(255) NULL, -- Opcionalno: Drugi Serijski broj uređaja
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP -- Datum kreiranja naloga
);
