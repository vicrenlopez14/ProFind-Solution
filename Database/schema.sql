DROP DATABASE IF EXISTS ProFind;
CREATE DATABASE ProFind;
USE ProFind;

################################################

CREATE TABLE Client
(
    Id_C       CHAR(21) PRIMARY KEY,
    Name_C     VARCHAR(50),
    Email_C    VARCHAR(50),
    Password_C CHAR(64)
);

SELECT *
FROM Client;

DESCRIBE Client;

###########################################
CREATE TABLE `Rank`
(
    Id_R   CHAR(21) PRIMARY KEY,
    Name_R VARCHAR(50) UNIQUE
);

DESCRIBE `Rank`;

###########################################
CREATE TABLE Admin
(
    Id_A       CHAR(21) PRIMARY KEY,
    Name_A     VARCHAR(50),
    Email_A    VARCHAR(50),
    Tel_A      VARCHAR(15),
    Password_A CHAR(64),
    Id_R1      CHAR(21),
    FULLTEXT (Name_A, Email_A, Tel_A),
    FOREIGN KEY (Id_R1) REFERENCES `Rank` (Id_R) ON DELETE CASCADE
);

DESCRIBE Admin;

###############################################
CREATE TABLE Project
(
    Id_PJ          CHAR(21) PRIMARY KEY,
    Title_PJ       VARCHAR(50),
    Description_PJ VARCHAR(50)
);

DESCRIBE Project;

###############################################
CREATE TABLE Tag
(
    Id_T   CHAR(21) PRIMARY KEY,
    Name_T VARCHAR(50),
    Id_PJ1 CHAR(21),

    FOREIGN KEY (Id_PJ1) REFERENCES Project (Id_PJ) ON DELETE CASCADE
);

DESCRIBE Tag;

###############################################
CREATE TABLE Process
(
    Id_P          CHAR(21) PRIMARY KEY,
    Title_P       VARCHAR(50),
    Description_P VARCHAR(350),
    Begin_Date_P  DATETIME,
    Id_T1         CHAR(21),
    End_Date_P    DATETIME,
    FOREIGN KEY (Id_T1) REFERENCES Tag (Id_T) ON DELETE CASCADE
);

SELECT Process.Id_P,
       Process.Title_P,
       Process.Description_P,
       Process.Begin_Date_P,
       Process.End_Date_P,
       Process.Id_T1,
       Name_T,
       Id_PJ1,
       Title_PJ,
       Description_PJ
FROM Process
         INNER JOIN Tag T on Process.Id_T1 = T.Id_T
         INNER JOIN Project P on T.Id_PJ1 = P.Id_PJ
WHERE Process.Id_P = ':Id';



DESCRIBE Process;

###############################################
CREATE TABLE Curriculum
(
    Id_CU          CHAR(21) PRIMARY KEY,
    Studies_CU     CHAR,
    Experiences_CU CHAR,
    Years_CU       INT(2)
);

DESCRIBE Curriculum;

###############################################
CREATE TABLE Job
(
    Id_J   CHAR(21) PRIMARY KEY,
    Name_J CHAR(50)
);

DESCRIBE Job;

################################################
CREATE TABLE Profesional
(
    Id_P         CHAR(21) PRIMARY KEY,
    Name_P       CHAR(50),
    Date_Birth_P DATE,
    Email_P      CHAR(21),
    Password_P   CHAR(64),
    Id_CU1       CHAR(21),
    Id_J1        CHAR(21),
    FOREIGN KEY (Id_CU1) REFERENCES Curriculum (Id_CU) ON DELETE CASCADE,
    FOREIGN KEY (Id_J1) REFERENCES Job (Id_J) ON DELETE CASCADE
);
