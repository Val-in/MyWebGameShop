CREATE TABLE Recommendations (
    Id SERIAL PRIMARY KEY,
    Description TEXT NOT NULL,
    GameTitle TEXT NOT NULL,
    GameVersion TEXT NOT NULL,
    GameRate REAL NOT NULL,
    RecommendationComment TEXT NOT NULL,
    UserId INTEGER NOT NULL,
    CONSTRAINT FK_Recommendations_Users FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);