CREATE TABLE SubscriptionUserInfos (
    Id SERIAL PRIMARY KEY,
    UserId INTEGER NOT NULL,
    SubscriptionId INTEGER NOT NULL,
    PaymentMethod TEXT NOT NULL,
    SubscriptionStatus TEXT NOT NULL,
    SubscriptionStartDate TIMESTAMP NOT NULL,
    SubscriptionEndDate TIMESTAMP NOT NULL,
    LastPaymentDate TIMESTAMP NOT NULL,
    PaymentHistory TEXT,
    CONSTRAINT FK_SubscriptionUserInfos_Users FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    CONSTRAINT FK_SubscriptionUserInfos_Subscriptions FOREIGN KEY (SubscriptionId) REFERENCES Subscriptions(Id) ON DELETE CASCADE
);