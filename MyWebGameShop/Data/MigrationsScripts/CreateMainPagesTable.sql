CREATE TABLE MainPages (
    Id SERIAL PRIMARY KEY,
    History TEXT NOT NULL,
    Facts TEXT NOT NULL,
    Specialization TEXT NOT NULL,
    ShopInfoId INTEGER,
    CONSTRAINT FK_MainPages_ShopInfos FOREIGN KEY (ShopInfoId) REFERENCES ShopInfos(Id) ON DELETE SET NULL
);