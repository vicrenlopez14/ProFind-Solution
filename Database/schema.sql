SET collation_connection = 'utf8_general_ci';
SET collation_server = 'utf8_general_ci';
SET character_set_client = 'utf8';
SET character_set_connection = 'utf8';
SET character_set_database = 'utf8';
SET character_set_results = 'utf8';
SET character_set_server = 'utf8';
SET collation_database = 'utf8_general_ci';
SET collation_server = 'utf8_general_ci';
DROP DATABASE IF EXISTS ProFind;
CREATE DATABASE ProFind;
USE ProFind;

################################################

CREATE TABLE Client
(
    IdC               CHAR(21) PRIMARY KEY,
    NameC             VARCHAR(50),
    EmailC            VARCHAR(255),
    PasswordC         CHAR(64),
    RegistrationDateC DATETIME,
    PictureC          LONGTEXT,
    CommunicationIdC  VARCHAR(255)
);

SELECT *
FROM Client;

DESCRIBE Client;

###########################################
CREATE TABLE `Rank`
(
    IdR   INT AUTO_INCREMENT PRIMARY KEY,
    NameR VARCHAR(50) UNIQUE
);

DESCRIBE `Rank`;

INSERT INTO `Rank` (NameR)
VALUES ('Principal'),
       ('Administrator');

SELECT *
FROM `Rank`;

###########################################
CREATE TABLE Admin
(
    IdA              CHAR(21) PRIMARY KEY,
    NameA            VARCHAR(50),
    EmailA           VARCHAR(255) UNIQUE,
    TelA             VARCHAR(15),
    PasswordA        CHAR(64),
    PictureA         LONGTEXT,
    CreationDateA    DATETIME,
    IdR1             INT,
    CommunicationIdA VARCHAR(255),
    FULLTEXT (NameA, EmailA, TelA),
    CONSTRAINT FK_Admin_Rank FOREIGN KEY (IdR1) REFERENCES `Rank` (IdR) ON DELETE CASCADE
);

DESCRIBE Admin;

SELECT *
FROM Admin;

################################################
CREATE TABLE Department
(
    IdDP   INT AUTO_INCREMENT PRIMARY KEY,
    NameDP VARCHAR(30)
);

INSERT INTO Department (NameDP)
VALUES ('Ahuachapán'),
       ('Cabañas'),
       ('Chalatenango'),
       ('Cuscatlán'),
       ('La Libertad'),
       ('Morazán'),
       ('La Paz'),
       ('Santa Ana'),
       ('San Miguel'),
       ('San Salvador'),
       ('San Vicente'),
       ('Sonsonate'),
       ('La Unión'),
       ('Usulután');

SELECT *
FROM Department;

################################################
CREATE TABLE Profession
(
    IdPFS          INT AUTO_INCREMENT PRIMARY KEY,
    NamePFS        VARCHAR(50),
    DescriptionPFS VARCHAR(255),
    PicturePFS     LONGTEXT,
    BannerPFS      LONGTEXT
);

# INSERT INTO Profession (NamePFS)
# VALUES ('Law firm'),
#        ('Automotive services'),
#        ('General medicine');

SELECT *
FROM Profession;

################################################
CREATE TABLE Professional
(
    IdP              CHAR(21) PRIMARY KEY,
    NameP            CHAR(50),
    DateBirthP       DATETIME,
    MottoP           TEXT,
    EmailP           CHAR(255),
    PasswordP        CHAR(64),
    ActiveP          BOOLEAN,
    PhoneP           CHAR(15),
    SexP             BOOLEAN,
    DUIP             VARCHAR(15),
    AFPP             VARCHAR(50),
    ISSSP            VARCHAR(50),
    ZipCodeP         VARCHAR(10),
    SalaryP          FLOAT,
    HiringDateP      DATETIME,
    PictureP         LONGTEXT,
    CurriculumP      LONGBLOB,
    CommunicationIdP VARCHAR(255),
    IdPFS1           INT,
    IdDP1            INT,
    CONSTRAINT FK_Professional_Profession FOREIGN KEY (IdPFS1) REFERENCES Profession (IdPFS) ON DELETE CASCADE,
    CONSTRAINT FK_Professional_Department FOREIGN KEY (IdDP1) REFERENCES Department (IdDP) ON DELETE CASCADE
);

SELECT *
FROM Professional;

###############################################

# Change password codes
CREATE TABLE ChangePasswordCode
(
    IdCPC    CHAR(21) PRIMARY KEY,
    CodeCPC  CHAR(4),
    ValidCPC BOOLEAN,
    IdC1     CHAR(21),
    IdA1     CHAR(21),
    IdP1     CHAR(21),
    CONSTRAINT FK_Client_ChangePasswordCode FOREIGN KEY (IdC1) REFERENCES Client (IdC) ON DELETE CASCADE,
    CONSTRAINT FK_Admin_ChangePasswordCode FOREIGN KEY (IdA1) REFERENCES Admin (IdA) ON DELETE CASCADE,
    CONSTRAINT FK_Professional_ChangePasswordCode FOREIGN KEY (IdP1) REFERENCES Professional (IdP) ON DELETE CASCADE
);

SELECT *
FROM ChangePasswordCode;

################################################
# Duration of a project
CREATE TABLE TimeRequired
(
    IdTR   INT AUTO_INCREMENT PRIMARY KEY,
    NameTR VARCHAR(50)
);

###############################################
CREATE TABLE Project
(
    IdPJ            CHAR(21) PRIMARY KEY,
    TitlePJ         VARCHAR(50),
    DescriptionPJ   VARCHAR(150),
    PicturePJ       LONGTEXT,
    StartDate       DATETIME,
    EndDate         DATETIME,
    TotalPricePJ    FLOAT,
    IsActive        BOOLEAN,
    Completed       BOOLEAN,
    IdP1            CHAR(21),
    IdC1            CHAR(21),
    TimeRequiredTR1 INT,
    CONSTRAINT FK_Project_Professional FOREIGN KEY (IdP1) REFERENCES Professional (IdP) ON DELETE CASCADE,
    CONSTRAINT FK_Project_Client FOREIGN KEY (IdC1) REFERENCES Client (IdC) ON DELETE CASCADE,
    CONSTRAINT FK_TimeRequired_Project FOREIGN KEY (TimeRequiredTR1) REFERENCES TimeRequired (IdTR) ON DELETE CASCADE
);

DESCRIBE Project;

SELECT *
FROM Project;

################################################
CREATE TABLE Proposal
(
    IdPP                  CHAR(21) PRIMARY KEY,
    TitlePP               VARCHAR(50),
    DescriptionPP         VARCHAR(500),
    PicturePP             LONGTEXT,
    SuggestedStart        DATETIME,
    SuggestedEnd          DATETIME,
    ReferenceIllustration LONGTEXT,
    ReferenceAudio        LONGTEXT,
    ReferenceFile         LONGTEXT,
    LatitudePP            FLOAT,
    LongitudePP           FLOAT,
    IdP3                  CHAR(21),
    IdC3                  CHAR(21),
    CONSTRAINT FK_Proposal_Professional FOREIGN KEY (IdP3) REFERENCES Professional (IdP) ON DELETE CASCADE,
    CONSTRAINT FK_Proposal_Client FOREIGN KEY (IdC3) REFERENCES Client (IdC) ON DELETE CASCADE
);

SELECT *
FROM Proposal;

################################################
# Price and conditions proposal
CREATE TABLE PriceAndConditionsProposal
(
    IdPCP         CHAR(21) PRIMARY KEY,
    PricePCP      FLOAT,
    ConditionsPCP VARCHAR(500),
    IdPP1         CHAR(21),
    IdPJ1         CHAR(21),
    CONSTRAINT FK_PriceAndConditionsProposal_Proposal FOREIGN KEY (IdPP1) REFERENCES Proposal (IdPP) ON DELETE CASCADE
);

################################################
CREATE TABLE ThreadIds
(
    IdT VARCHAR(255) PRIMARY KEY,
    IdC CHAR(21),
    IdP CHAR(21),
    CONSTRAINT FK_ThreadIds_Client FOREIGN KEY (IdC) REFERENCES Client (IdC) ON DELETE CASCADE,
    CONSTRAINT FK_ThreadIds_Professional FOREIGN KEY (IdP) REFERENCES Professional (IdP) ON DELETE CASCADE
);



################################################
CREATE TABLE GraphicPreference
(
    IdPRF          VARCHAR(255) PRIMARY KEY,
    NamePRF        VARCHAR(50),
    DescriptionPRF VARCHAR(255),
    PicturePRF     LONGTEXT,
    BannerPRF      LONGTEXT
);

################################################
#Professional ratings
CREATE TABLE Rating
(
    IdRT      CHAR(21) PRIMARY KEY,
    RatingRT  FLOAT,
    CommentRT VARCHAR(500),
    IdP1      CHAR(21),
    IdC1      CHAR(21),
    CONSTRAINT FK_Rating_Professional FOREIGN KEY (IdP1) REFERENCES Professional (IdP) ON DELETE CASCADE,
    CONSTRAINT FK_Rating_Client FOREIGN KEY (IdC1) REFERENCES Client (IdC) ON DELETE CASCADE
);
