CREATE TABLE MainPages (
    Id SERIAL PRIMARY KEY,
    ShopInfoId INTEGER NOT NULL,
    MainInformationId INTEGER NOT NULL,
    CONSTRAINT FK_MainPages_ShopInfos FOREIGN KEY (ShopInfoId) REFERENCES ShopInfos(Id) ON DELETE CASCADE,
    CONSTRAINT FK_MainPages_MainInformations FOREIGN KEY (MainInformationId) REFERENCES MainInformations(Id) ON DELETE CASCADE
);