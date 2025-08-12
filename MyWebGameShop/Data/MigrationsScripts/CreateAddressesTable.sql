CREATE TABLE Addresses (
    Id SERIAL PRIMARY KEY,
    Country TEXT NOT NULL,
    City TEXT NOT NULL,
    Street TEXT NOT NULL,
    Building INTEGER NOT NULL,
    PostalCode INTEGER NOT NULL
);
