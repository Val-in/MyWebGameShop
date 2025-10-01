CREATE TABLE Subscriptions (
    Id SERIAL PRIMARY KEY,
    Name TEXT NOT NULL,
    Description TEXT,
    Price DECIMAL(18,2) NOT NULL,
    SubscriptionType INTEGER NOT NULL,
    DurationMonths INTEGER NOT NULL,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);