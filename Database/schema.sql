DROP DATABASE IF EXISTS ProFind;
CREATE DATABASE ProFind;
USE ProFind;

################################################

CREATE TABLE Client
(
    IdC       CHAR(21) PRIMARY KEY,
    NameC     VARCHAR(50),
    EmailC    VARCHAR(50),
    PasswordC CHAR(64),
    PictureC  LONGBLOB
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
    IdA       CHAR(21) PRIMARY KEY,
    NameA     VARCHAR(50),
    EmailA    VARCHAR(50),
    TelA      VARCHAR(15),
    PasswordA CHAR(64),
    PictureA  LONGBLOB,
    IdR1      INT,
    FULLTEXT (NameA, EmailA, TelA),
    CONSTRAINT FK_Admin_Rank FOREIGN KEY (IdR1) REFERENCES `Rank` (IdR) ON DELETE CASCADE
);

DESCRIBE ADMIN;

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
    IdPFS   INT AUTO_INCREMENT PRIMARY KEY,
    NamePFS VARCHAR(50)
);

INSERT INTO Profession (NamePFS)
VALUES ('Law firm'),
       ('Automotive services'),
       ('General medicine');

################################################
CREATE TABLE Professional
(
    IdP                CHAR(21) PRIMARY KEY,
    NameP              CHAR(50),
    DateBirthP         DATE,
    EmailP             CHAR(21),
    PasswordP          CHAR(64),
    ActiveP            BOOLEAN,
    SexP               BOOLEAN,
    DUIP               VARCHAR(15),
    AFPP               VARCHAR(50),
    ISSSP              VARCHAR(50),
    ZipCodeP           VARCHAR(10),
    SalaryP            FLOAT,
    HiringDateP        DATE,
    PictureP           LONGBLOB,
    CurriculumP        LONGBLOB,
    LatitudeLocationP  DOUBLE,
    LongitudeLocationP DOUBLE,
    IdPFS1             INT,
    IdDP1              INT,
    IdWDT1             INT,
    CONSTRAINT FK_Professional_Profession FOREIGN KEY (IdPFS1) REFERENCES Profession (IdPFS) ON DELETE CASCADE,
    CONSTRAINT FK_Professional_Department FOREIGN KEY (IdDP1) REFERENCES Department (IdDP) ON DELETE CASCADE
);

SELECT *
FROM Professional;


###########################################
#Security questions
CREATE TABLE SecurityQuestion
(
    IdSQ   CHAR(21) PRIMARY KEY,
    NameSQ VARCHAR(50) UNIQUE
);

###########################################
#Security answers
CREATE TABLE SecurityAnswerClients
(
    IdSA     CHAR(21) PRIMARY KEY,
    AnswerSA VARCHAR(50),
    IdSQ1    CHAR(21),
    IdC1     CHAR(21),
    CONSTRAINT FK_SecurityAnswerClients_SecurityQuestion FOREIGN KEY (IdSQ1) REFERENCES SecurityQuestion (IdSQ) ON DELETE CASCADE,
    CONSTRAINT FK_Client_SecurityAnswerClients FOREIGN KEY (IdC1) REFERENCES Client (IdC) ON DELETE CASCADE
);

###########################################
#Security answers for professionals
CREATE TABLE SecurityAnswerProfessionals
(
    IdSA     CHAR(21) PRIMARY KEY,
    AnswerSA VARCHAR(50),
    IdSQ     CHAR(21),
    IdP      CHAR(21),
    CONSTRAINT FK_SecurityAnswerProfessionals_SecurityQuestion FOREIGN KEY (IdSQ) REFERENCES SecurityQuestion (IdSQ) ON DELETE CASCADE,
    CONSTRAINT FK_Professional_SecurityAnswerProfessionals FOREIGN KEY (IdP) REFERENCES Professional (IdP) ON DELETE CASCADE
);

###########################################
#Security answers for admins
CREATE TABLE SecurityAnswerAdmins
(
    IdSA     CHAR(21) PRIMARY KEY,
    AnswerSA VARCHAR(50),
    IdSQ1    CHAR(21),
    IdA1     CHAR(21),
    CONSTRAINT FK_SecurityAnswerAdmins_SecurityQuestion FOREIGN KEY (IdSQ1) REFERENCES SecurityQuestion (IdSQ) ON DELETE CASCADE,
    CONSTRAINT FK_Admin_SecurityAnswerAdmins FOREIGN KEY (IdA1) REFERENCES Admin (IdA) ON DELETE CASCADE
);

###############################################
# Change password codes
CREATE TABLE ChangePasswordCode
(
    IdCPC       CHAR(21) PRIMARY KEY,
    CodeCPC     CHAR(64),
    VerifiedCPC BOOLEAN,
    IdC1        CHAR(21),
    CONSTRAINT FK_Client_ChangePasswordCode FOREIGN KEY (IdC1) REFERENCES Client (IdC) ON DELETE CASCADE
);

###############################################
CREATE TABLE ProjectStatus
(
    IdPS          CHAR(21) PRIMARY KEY,
    NamePS        VARCHAR(20),
    DescriptionPS VARCHAR(150),
    ColorPS       VARCHAR(6)
);

SELECT *
FROM ProjectStatus;

###############################################
CREATE TABLE Project
(
    IdPJ          CHAR(21) PRIMARY KEY,
    TitlePJ       VARCHAR(50),
    DescriptionPJ VARCHAR(50),
    PicturePJ     LONGBLOB,
    TotalPricePJ  FLOAT,
    IdPS1         CHAR(21),
    IdP1          CHAR(21),
    IdC1          CHAR(21),
    CONSTRAINT FK_Project_ProjectStatus FOREIGN KEY (IdPS1) REFERENCES ProjectStatus (IdPS) ON DELETE CASCADE,
    CONSTRAINT FK_Project_Professional FOREIGN KEY (IdP1) REFERENCES Professional (IdP) ON DELETE CASCADE,
    CONSTRAINT FK_Project_Client FOREIGN KEY (IdC1) REFERENCES Client (IdC) ON DELETE CASCADE
);

DESCRIBE Project;

SELECT *
FROM Project;

###############################################
CREATE TABLE Tag
(
    IdT    CHAR(21) PRIMARY KEY,
    NameT  VARCHAR(50),
    ColorT BINARY(3),
    IdPJ   CHAR(21),
    CONSTRAINT FK_Tag_Project FOREIGN KEY (IdPJ) REFERENCES Project (IdPJ) ON DELETE CASCADE
);

DESCRIBE Tag;



###############################################
# ProjectTemplate
CREATE TABLE ProjectTemplate
(
    IdPT              CHAR(21) PRIMARY KEY,
    TitlePT           VARCHAR(50),
    DescriptionPT     VARCHAR(50),
    PicturePT         LONGBLOB,
    TotalPricePT      FLOAT,
    SaveTagsPT        BOOLEAN,
    SaveProjectPaysPT BOOLEAN,
    IdP1              CHAR(21),
    CONSTRAINT FK_ProjectTemplate_Professional FOREIGN KEY (IdP1) REFERENCES Professional (IdP) ON DELETE CASCADE
);

###############################################
# TagTemplate
CREATE TABLE TagTemplate
(
    IdTT    CHAR(21) PRIMARY KEY,
    NameTT  VARCHAR(50),
    ColorTT BINARY(3),
    IdPT1   CHAR(21),
    CONSTRAINT FK_TagTemplate_ProjectTemplate FOREIGN KEY (IdPT1) REFERENCES ProjectTemplate (IdPT) ON DELETE CASCADE
);

###############################################
# ProposalPayTemplate
CREATE TABLE ProjectPayTemplate
(
    IdPPT          CHAR(21) PRIMARY KEY,
    TitlePPT       VARCHAR(50),
    DescriptionPPT VARCHAR(50),
    PicturePPT     LONGBLOB,
    TotalPricePPT  FLOAT,
    IdPT1          CHAR(21),
    CONSTRAINT FK_ProjectPayTemplate_ProjectTemplate FOREIGN KEY (IdPT1) REFERENCES ProjectTemplate (IdPT) ON DELETE CASCADE
);

################################################
CREATE TABLE Activity
(
    IdA            CHAR(21) PRIMARY KEY,
    TitleA         VARCHAR(50),
    DescriptionA   VARCHAR(500),
    ExpectedBeginA DATE,
    ExpectedEndA   DATE,
    PictureA       LONGBLOB,
    IdPJ1          CHAR(21),
    IdT1           CHAR(21),
    CONSTRAINT FK_Activity_Project FOREIGN KEY (IdPJ1) REFERENCES Project (IdPJ) ON DELETE CASCADE,
    CONSTRAINT FK_Activity_Tag FOREIGN KEY (IdT1) REFERENCES Tag (IdT) ON DELETE CASCADE
);

SELECT *
FROM Activity;

###############################################
# Activity comments
CREATE TABLE ActivityComment
(
    IdAC             CHAR(21) PRIMARY KEY,
    CommentAC        VARCHAR(500),
    DateAC           DATE,
    AskToCheckChatAC BOOLEAN,
    IdA1             CHAR(21),
    IdP5             CHAR(21),
    IdC5             CHAR(21),
    CONSTRAINT FK_ActivityComment_Activity FOREIGN KEY (IdA1) REFERENCES Activity (IdA) ON DELETE CASCADE,
    CONSTRAINT FK_ActivityComment_Professional FOREIGN KEY (IdP5) REFERENCES Professional (IdP) ON DELETE CASCADE,
    CONSTRAINT FK_ActivityComment_Client FOREIGN KEY (IdC5) REFERENCES Client (IdC) ON DELETE CASCADE
);

################################################

CREATE TABLE Notification
(
    IdN             CHAR(21) PRIMARY KEY,
    TitleN          VARCHAR(50),
    DescriptionN    VARCHAR(500),
    DateTimeIssuedN DATE,
    PictureN        LONGBLOB,
    IdPJ2           CHAR(21),
    CONSTRAINT FK_Notification_Project FOREIGN KEY (IdPJ2) REFERENCES Project (IdPJ) ON DELETE CASCADE
);

################################################
CREATE TABLE Proposal
(
    IdPP           CHAR(21) PRIMARY KEY,
    TitlePP        VARCHAR(50),
    DescriptionPP  VARCHAR(500),
    PicturePP      LONGBLOB,
    SuggestedStart DATE,
    SuggestedEnd   DATE,
    Seen           BOOLEAN,
    RevisionStatus ENUM ('pending', 'planning', 'rejected', 'clientaccepted', 'topay', 'readytostart'),
    IdP3           CHAR(21),
    IdC3           CHAR(21),
    CONSTRAINT FK_Proposal_Professional FOREIGN KEY (IdP3) REFERENCES Professional (IdP) ON DELETE CASCADE,
    CONSTRAINT FK_Proposal_Client FOREIGN KEY (IdC3) REFERENCES Client (IdC) ON DELETE CASCADE
);

################################################
# Proposal Pay, include Professional, Client and Project
CREATE TABLE ProjectPay
(
    IdPPY            CHAR(21) PRIMARY KEY,
    PercentagePPY    FLOAT,
    ConceptPPY       VARCHAR(500),
    AmountPPY        FLOAT,
    CurrencyPPY      VARCHAR(3),
    HasLimitDatePPY  BOOLEAN,
    LimitDatePPY     DATE,
    DefaultAmountPPY FLOAT,
    PayStatusPPY     ENUM ('pending', 'done', 'rejected'),
    IdP3             CHAR(21),
    IdC3             CHAR(21),
    IdPJ3            CHAR(21),
    CONSTRAINT FK_ProjectPay_Professional FOREIGN KEY (IdP3) REFERENCES Professional (IdP) ON DELETE CASCADE,
    CONSTRAINT FK_ProjectPay_Client FOREIGN KEY (IdC3) REFERENCES Client (IdC) ON DELETE CASCADE,
    CONSTRAINT FK_ProjectPay_Project FOREIGN KEY (IdPJ3) REFERENCES Project (IdPJ) ON DELETE CASCADE
);

################################################
# Proposal notification
CREATE TABLE ProposalNotification
(
    IdPN      CHAR(21) PRIMARY KEY,
    ContentPN VARCHAR(500),
    ImagePN   LONGBLOB,
    ActionPN  ENUM ('chat', 'resend', 'support', 'checktoapprove'),
    IdPP1     CHAR(21),
    IdP4      CHAR(21),
    IdC4      CHAR(21),
    CONSTRAINT FK_ProposalNotification_Proposal FOREIGN KEY (IdPP1) REFERENCES Proposal (IdPP) ON DELETE CASCADE,
    CONSTRAINT FK_ProposalNotification_Professional FOREIGN KEY (IdP4) REFERENCES Professional (IdP) ON DELETE CASCADE,
    CONSTRAINT FK_ProposalNotification_Client FOREIGN KEY (IdC4) REFERENCES Client (IdC) ON DELETE CASCADE
);

################################################
CREATE TABLE Message
(
    IdM       CHAR(21) PRIMARY KEY,
    ContentM  VARCHAR(500),
    ImageM    LONGBLOB,
    DocumentM LONGBLOB,
    LocationM VARCHAR(100),
    AudioM    LONGBLOB,
    DateTimeM DATE,
    IdP4      CHAR(21),
    IdC4      CHAR(21),
    CONSTRAINT FK_Message_Professional FOREIGN KEY (IdP4) REFERENCES Professional (IdP) ON DELETE CASCADE,
    CONSTRAINT FK_Message_Client FOREIGN KEY (IdC4) REFERENCES Client (IdC) ON DELETE CASCADE
);

################################################
#Support ticket
CREATE TABLE SupportTicket
(
    IdST               CHAR(21) PRIMARY KEY,
    TitleST            VARCHAR(50),
    ContentST          VARCHAR(500),
    ImageST            LONGBLOB,
    DocumentST         LONGBLOB,
    LocationST         VARCHAR(100),
    AudioST            LONGBLOB,
    DateTimeST         DATE,
    TicketStatusST     ENUM ('taken', 'pending', 'actiontaken'),
    SuggestedActionST  ENUM ('checkproject', 'checkactivity', 'checkproposal', 'checkpayment', 'checkchat', 'closeticket'),
    ResponseContentST  VARCHAR(500),
    ResponseImageST    LONGBLOB,
    ResponseDocumentST LONGBLOB,
    ResponseLocationST VARCHAR(100),
    ResponseAudioST    LONGBLOB,
    IdP5               CHAR(21),
    IdC5               CHAR(21),
    IdA2               CHAR(21),
    IdPJ4              CHAR(21),
    IdACT1             CHAR(21),
    IdPP2              CHAR(21),
    IdPPY1             CHAR(21),
    CONSTRAINT FK_SupportTicket_Professional FOREIGN KEY (IdP5) REFERENCES Professional (IdP) ON DELETE CASCADE,
    CONSTRAINT FK_SupportTicket_Client FOREIGN KEY (IdC5) REFERENCES Client (IdC) ON DELETE CASCADE,
    CONSTRAINT FK_SupportTicket_Admin FOREIGN KEY (IdA2) REFERENCES Admin (IdA) ON DELETE CASCADE,
    CONSTRAINT FK_SupportTicket_Project FOREIGN KEY (IdPJ4) REFERENCES Project (IdPJ) ON DELETE CASCADE,
    CONSTRAINT FK_SupportTicket_Activity FOREIGN KEY (IdACT1) REFERENCES Activity (IdA) ON DELETE CASCADE,
    CONSTRAINT FK_SupportTicket_Proposal FOREIGN KEY (IdPP2) REFERENCES Proposal (IdPP) ON DELETE CASCADE,
    CONSTRAINT FK_SupportTicket_ProjectPay FOREIGN KEY (IdPPY1) REFERENCES ProjectPay (IdPPY) ON DELETE CASCADE
);