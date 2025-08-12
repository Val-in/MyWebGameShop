CREATE TABLE UserSubscriptionInfos (
    Id SERIAL PRIMARY KEY,
    SubscriptionsId INTEGER NOT NULL,
    SubscriptionType TEXT NOT NULL,
    PaymentMethod TEXT NOT NULL,
    SubscriptionStatus TEXT NOT NULL,
    SubscriptionStartDate DATE NOT NULL,
    SubscriptionEndDate DATE NOT NULL,
    LastPaymentDate DATE NOT NULL,
    SubscriptionTier TEXT NOT NULL,
    PaymentHistory TEXT NOT NULL,
    UserId INTEGER NOT NULL,
    CONSTRAINT FK_UserSubscriptionInfos_Subscriptions FOREIGN KEY (SubscriptionsId) REFERENCES Subscriptions(Id) ON DELETE CASCADE,
    CONSTRAINT FK_UserSubscriptionInfos_Users FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);
