CREATE TABLE Users (
    UID INT AUTO_INCREMENT PRIMARY KEY, -- Jedinstveni identifikator korisnika
    Password VARCHAR(255) NOT NULL, -- Šifra za korisnika
    DeviceID VARCHAR(255) NULL, -- Opcionalno: Serijski broj uređaja
    AllowedDevices INT DEFAULT 1, -- Broj uređaja koje korisnik sme da koristi
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP -- Datum kreiranja naloga
);
