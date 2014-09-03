-- MySQLServer.sexStoreReports
CREATE TABLE `sexStoreReports` (
    `Id` integer AUTO_INCREMENT NOT NULL,   -- _id
    `nme` varchar(255) NULL,                -- _name
    `ProductCode` integer NOT NULL,         -- _productCode
    `SoldInShops` varchar(255) NULL,        -- _soldInShops
    `TotalIncomes` double NOT NULL,         -- _totalIncomes
    `TotalQuantitySold` integer NOT NULL,   -- _totalQuantitySold
    CONSTRAINT `pk_sexStoreReports` PRIMARY KEY (`Id`)
) ENGINE = InnoDB
;

