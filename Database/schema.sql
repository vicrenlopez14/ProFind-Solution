DROP DATABASE IF EXISTS ProFind;
CREATE DATABASE ProFind;
USE ProFind;

################################################

CREATE TABLE Client
(
    IdC       CHAR(21) PRIMARY KEY,
    NameC     VARCHAR(50),
    EmailC    VARCHAR(50),
    PasswordC CHAR(64)
);

SELECT *
FROM Client;

DESCRIBE Client;

###########################################
CREATE TABLE `Rank`
(
    IdR   CHAR(21) PRIMARY KEY,
    NameR VARCHAR(50) UNIQUE
);

INSERT INTO `Rank` VALUES ("1","Empleado");

DESCRIBE `Rank`;

###########################################
CREATE TABLE Admin
(
    IdA       CHAR(21) PRIMARY KEY,
    NameA     VARCHAR(50),
    EmailA    VARCHAR(50),
    TelA      VARCHAR(15),
    PasswordA CHAR(64),
    IdR1      CHAR(21),
    FULLTEXT (NameA, EmailA, TelA),
    FOREIGN KEY (IdR1) REFERENCES `Rank` (IdR) ON DELETE CASCADE
);

DESCRIBE ADMIN;

SELECT * FROM ADMIN;

DROP TABLE Admin;

###############################################
CREATE TABLE Project
(
    IdPJ          CHAR(21) PRIMARY KEY,
    TitlePJ       VARCHAR(50),
    DescriptionPJ VARCHAR(50)
);

DESCRIBE Project;

###############################################
CREATE TABLE Tag
(
    IdT   CHAR(21) PRIMARY KEY,
    NameT VARCHAR(50),
    IdPJ1 CHAR(21),

    FOREIGN KEY (IdPJ1) REFERENCES Project (IdPJ) ON DELETE CASCADE
);

DESCRIBE Tag;

###############################################
CREATE TABLE Process
(
    IdPR          CHAR(21) PRIMARY KEY,
    TitlePR       VARCHAR(50),
    DescriptionPR VARCHAR(350),
    BeginDatePR  DATETIME,
    IdT1         CHAR(21),
    EndDatePR    DATETIME,
    FOREIGN KEY (IdT1) REFERENCES Tag (IdT) ON DELETE CASCADE
);

SELECT Process.IdPR,
       Process.TitlePR,
       Process.DescriptionPR,
       Process.BeginDatePR,
       Process.EndDatePR,
       Process.IdT1,
       NameT,
       IdPJ1,
       TitlePJ,
       DescriptionPJ
FROM Process
         INNER JOIN Tag T on Process.IdT1 = T.IdT
         INNER JOIN Project P on T.IdPJ1 = P.IdPJ
WHERE Process.IdPR = ':Id';



DESCRIBE Process;

###############################################
CREATE TABLE Curriculum
(
    IdCU          CHAR(21) PRIMARY KEY,
    StudiesCU     CHAR,
    ExperiencesCU CHAR,
    YearsCU       INT(2)
);

DESCRIBE Curriculum;

###############################################
CREATE TABLE Job
(
    IdJ   CHAR(21) PRIMARY KEY,
    NameJ CHAR(50)
);

DESCRIBE Job;

################################################
CREATE TABLE Profesional
(
    IdP         CHAR(21) PRIMARY KEY,
    NameP       CHAR(50),
    DateBirthP DATE,
    EmailP      CHAR(21),
    PasswordP   CHAR(64),
    IdCU1       CHAR(21),
    IdJ1        CHAR(21),
    FOREIGN KEY (IdCU1) REFERENCES Curriculum (IdCU) ON DELETE CASCADE,
    FOREIGN KEY (IdJ1) REFERENCES Job (IdJ) ON DELETE CASCADE
);
