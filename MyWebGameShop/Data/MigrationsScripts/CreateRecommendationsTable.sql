CREATE TABLE Recommendations (
    Id SERIAL PRIMARY KEY,
    Description TEXT NOT NULL,
    RecommendationListId INTEGER NOT NULL,
    CONSTRAINT FK_Recommendations_RecommendationLists FOREIGN KEY (RecommendationListId) REFERENCES RecommendationLists(Id) ON DELETE CASCADE
);