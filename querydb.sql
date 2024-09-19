Create database bts_test_db;

CREATE TABLE LoginRequest (
    LoginID INT PRIMARY KEY IDENTITY(1,1),  
    Username NVARCHAR(255) NOT NULL,        
    Password NVARCHAR(255) NOT NULL         
);

CREATE TABLE RegistrationRequest (
    RegistrationID INT PRIMARY KEY IDENTITY(1,1),  
    Email NVARCHAR(255) NOT NULL,                  
    Username NVARCHAR(255) NOT NULL,               
    Password NVARCHAR(255) NOT NULL                
);

CREATE TABLE Titles (
    TitleID INT PRIMARY KEY IDENTITY(1,1),
    TitleName NVARCHAR(255)
);

CREATE TABLE Tasks (
    TaskID INT PRIMARY KEY IDENTITY(1,1),
    TaskDescription NVARCHAR(255),
    IsCompleted BIT,
    TitleID INT,
    CONSTRAINT FK_Title FOREIGN KEY (TitleID) REFERENCES Titles(TitleID)
);
