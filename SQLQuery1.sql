	CREATE DATABASE StudentManagement
	GO 
	USE StudentManagement;

CREATE TABLE Student
(
	ID INT IDENTITY(1,1),
	StudentID INT,
	Name NVARCHAR(255),
	Gender CHAR, CHECK (Gender ='F' or Gender ='M' or Gender = 'N'),
	Social_ID NVARCHAR(15),
	Class NVARCHAR(255),
	Courses NVARCHAR(255),
	birthday VARCHAR(10),
	PRIMARY KEY(StudentID),
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
	ID INT IDENTITY (1,1),
	codeName NVARCHAR(15),
	FullName NVARCHAR(255),
	Room NVARCHAR(255),
	Class NVARCHAR(15),
	PRIMARY KEY(CodeName,Class),
)

CREATE TABLE Grade
(
	ID INT IDENTITY(1,1),
	StudentID INT,
	StudentName NVARCHAR(255),
	CodeCourse NVARCHAR(255),
	Mid_Term FLOAT DEFAULT 0,
	Final_Term FLOAT DEFAULT 0,
	Other_Grade FLOAT DEFAULT 0,
	Sum_Grade FLOAT DEFAULT 0,
	Class NVARCHAR(15) DEFAULT 'NONE',
	Sub_Class NVARCHAR(15) DEFAULT 'NONE',
	PRIMARY KEY(StudentID,CodeCourse),

)






INSERT INTO Accounts (UserName,PassWord) values ('giaovu','giaovu');
INSERT INTO Accounts (UserName,PassWord) values ('1742005','11111999');

SELECT * FROM Student ORDER BY ID
SELECT * FROM Course
SELECT * FROM Grade WHERE Grade.StudentID=1842001

SELECT * FROM Accounts

DELETE FROM STUDENT  WHERE Student.StudentID=1753058
DELETE Accounts
use master;
drop database StudentManagement

