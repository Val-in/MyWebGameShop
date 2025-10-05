CREATE TABLE Products (
    Id SERIAL PRIMARY KEY,
    Name TEXT NOT NULL,
    Description TEXT,
    Price DECIMAL(18,2) NOT NULL,
    Stock INTEGER NOT NULL,
    ImageUrl TEXT,
    CategoryId INTEGER NOT NULL,
    CONSTRAINT FK_Products_Categories FOREIGN KEY (CategoryId) REFERENCES Categories(Id) ON DELETE CASCADE
);
