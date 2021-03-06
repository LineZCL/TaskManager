﻿CREATE DATABASE TaskManager; 
USE TaskManager; 

CREATE TABLE Role(
	Id INT NOT NULL IDENTITY PRIMARY KEY,
	Description VARCHAR(255) NOT NULL, 
	IsActive BIT
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
	Status INT NOT NULL, 
	Date DATETIME NOT NULL, 
	IsActive BIT, 
	SponsorId INT FOREIGN KEY REFERENCES Profile(Id)
)

CREATE TABLE TaskDependency(
	Id INT NOT NULL IDENTITY PRIMARY KEY,
	TaskId INT FOREIGN KEY REFERENCES Task(Id),
	PendingTaskId INT FOREIGN KEY REFERENCES Task(Id), 
	IsActive BIT
)

INSERT INTO Role VALUES ('Admin', 1); 

INSERT INTO Role VALUES ('Team', 1); 

INSERT INTO Profile(Name, Email, Password, RoleId, IsActive) VALUES ('admin', 'admin@admin.com.br', '7110eda4d09e062aa5e4a390b0a572ac0d2c0220', 1, 1);
