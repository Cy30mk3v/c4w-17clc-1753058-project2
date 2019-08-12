CREATE DATABASE StudentManagement
GO 
USE StudentManagement;

CREATE TABLE Student
(
	ID INT IDENTITY(1,1),
	StudentID INT,
	Name NVARCHAR(255),
	Gender CHAR, CHECK (Gender ='F' or Gender ='M' or Gender = 'N'),
	Social_ID NVARCHAR(10),
	Class NVARCHAR(255),
	Courses NVARCHAR(255),
	PRIMARY KEY(StudentID,Social_ID),
)

CREATE TABLE Accounts
(
	UserName  NVARCHAR(255) UNIQUE,
	PassWord NVARCHAR (255),
)

CREATE TABLE Class
(
	Name NVARCHAR(255)
)

CREATE TABLE Course
(
	codeName NVARCHAR(255) UNIQUE,
	FullName NVARCHAR(255),
	Room NVARCHAR(255)
)

CREATE TABLE Grade
(
	StudentID INT,
	CodeCourse NVARCHAR(255),

)
INSERT INTO Student (StudentID,Name,Gender,Social_ID) VALUES (1742001,N'Nguyễn Văn A','M','123456789');
INSERT INTO Student (StudentID,Name,Gender,Social_ID) VALUES (1742002,N'Trần Văn B','M','234567891');


INSERT INTO Accounts (UserName,PassWord) values ('giaovu','giaovu');

DROP TABLE Student
SELECT * FROM Student
SELECT * FROM Class