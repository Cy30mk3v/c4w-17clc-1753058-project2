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
	CodeCourse NVARCHAR(255),
	Mid_Term FLOAT DEFAULT 0,
	Final_Term FLOAT DEFAULT 0,
	Other_Grade FLOAT DEFAULT 0,
	Sum_Grade FLOAT DEFAULT 0,

)






INSERT INTO Accounts (UserName,PassWord) values ('giaovu','giaovu');

DROP TABLE Student
DROP TABLE Course
DROP TABLE Grade
SELECT * FROM Student
SELECT * FROM Course
SELECT * FROM Grade
x
use master;
drop database StudentManagement

--Lấy sinh viên có học ở lớp 18CLC1 và môn CTT011
SELECT Student.* 
FROM Student, Grade
WHERE Student.Class='17CLC1' AND Student.StudentID=Grade.StudentID AND Grade.CodeCourse ='CTT011'
	
INSERT INTO Grade(StudentID, CodeCourse)	
SELECT Student.StudentID,Course.codeName
FROM Student,Course 
WHERE Student.Class = Course.Class AND NOT EXISTS (SELECT G.StudentID,G.CodeCourse
													FROM Grade G
													WHERE G.StudentID = Student.StudentID AND G.CodeCourse = Course.codeName)
GROUP BY codeName,Student.ID,Student.StudentID
HAVING Student.ID=MAX(Student.ID)