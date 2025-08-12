CREATE TABLE Users (
    Id SERIAL PRIMARY KEY,
    UserName TEXT NOT NULL,
    Login TEXT NOT NULL,
    Password TEXT NOT NULL,
    Email TEXT NOT NULL,
    WalletBalance INTEGER NOT NULL
);