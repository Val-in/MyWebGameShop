CREATE TABLE Categories (
    Id SERIAL PRIMARY KEY,
    Name TEXT NOT NULL,
    Description TEXT,
    PlatformEnum INTEGER NOT NULL,
    GenreEnum INTEGER NOT NULL
);