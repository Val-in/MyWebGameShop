CREATE TABLE RecommendationLists (
    Id SERIAL PRIMARY KEY,
    Description TEXT NOT NULL,
    GameTitle TEXT NOT NULL,
    GameVersion TEXT NOT NULL,
    GameRate REAL NOT NULL,
    RecommendationComment TEXT NOT NULL,
    "User" TEXT NOT NULL
);