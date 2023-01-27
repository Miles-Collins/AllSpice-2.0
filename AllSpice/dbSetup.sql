CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS mcrecipes (
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  title VARCHAR(255) NOT NULL,
  instructions VARCHAR(255) NOT NULL,
  img VARCHAR(255) NOT NULL,
  category VARCHAR(255) NOT NULL,
  creatorId VARCHAR(255) NOT NULL,
  Foreign Key (creatorId) REFERENCES accounts (id)
) default charset utf8 COMMENT '';

DROP TABLE mcrecipes;

      SELECT
      r.*,
      i.*,
      a.*
      FROM mcrecipes r
      LEFT JOIN mcingredients i ON r.id = i.recipeId
      JOIN accounts a ON r.creatorId = a.Id
      WHERE r.id = @id;


CREATE TABLE IF NOT EXISTS mcingredients (
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  name VARCHAR(100) NOT NULL,
  quantity VARCHAR(100) NOT NULL,
  recipeId INT NOT NULL,
  creatorId VARCHAR(255) NOT NULL,
  Foreign Key (creatorId) REFERENCES accounts (id),
  Foreign Key (recipeId) REFERENCES mcrecipes (id)
) default charset utf8 COMMENT '';

DROP TABLE mcingredients;

CREATE TABLE IF NOT EXISTS mcfavorites (
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  accountId VARCHAR(255) NOT NULL,
  recipeId INT NOT NULL,
  Foreign Key (accountId) REFERENCES accounts (id) ON DELETE CASCADE,
  Foreign Key (recipeId) REFERENCES mcrecipes (id) ON DELETE CASCADE
) default charset utf8 comment '';

DROP TABLE mcfavorites;