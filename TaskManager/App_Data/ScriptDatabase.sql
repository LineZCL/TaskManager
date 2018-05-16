﻿CREATE DATABASE TaskManager; 
USE TaskManager; 

CREATE TABLE Role(
	Id INT NOT NULL IDENTITY PRIMARY KEY,
	Description VARCHAR(255) NOT NULL
);

CREATE TABLE Profile(
	Id INT NOT NULL IDENTITY PRIMARY KEY, 
	Name VARCHAR(255) NOT NULL, 
	Email VARCHAR(255) NOT NULL UNIQUE,
	Password VARCHAR(255) NOT NULL,
	RoleId INT FOREIGN KEY REFERENCES Role(Id),
	IsActive BIT
);

CREATE TABLE Task(
	Id INT NOT NULL IDENTITY PRIMARY KEY, 
	Description VARCHAR(500) NOT NULL, 
	Status VARCHAR(255) NOT NULL, 
	Date DATETIME NOT NULL, 
	IsActive BIT, 
	UserId INT FOREIGN KEY REFERENCES Profile(Id)
)

CREATE TABLE TaskDependency(
	TaskId INT FOREIGN KEY REFERENCES Task(Id),
	PendingTask INT FOREIGN KEY REFERENCES Task(Id)
)

INSERT INTO Role VALUES ('Admin'); 

INSERT INTO Role VALUES ('Team'); 

INSERT INTO Profile(Name, Email, Password, RoleId, IsActive) VALUES ('admin', 'admin@admin.com.br', '1234', 1, 1);