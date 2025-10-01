CREATE TABLE Games (
    Id SERIAL PRIMARY KEY,
    Title TEXT NOT NULL,
    Description TEXT NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    ImageUrl TEXT,
    CategoryId INTEGER NOT NULL,
    CONSTRAINT FK_Games_Categories FOREIGN KEY (CategoryId) REFERENCES Categories(Id) ON DELETE CASCADE
);