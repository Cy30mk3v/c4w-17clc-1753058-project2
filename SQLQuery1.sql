CREATE DATABASE StudentManagement
GO 
USE StudentManagement;

CREATE TABLE Student
(
	ID INT IDENTITY(1,1),
	StudentID INT,
	Name NVARCHAR(255),
	Gender CHAR, CHECK (Gender ='F' or Gender ='M'),
	Social_ID NVARCHAR(10),
	PRIMARY KEY(StudentID,Social_ID),
)

	

INSERT INTO Student (StudentID,Name,Gender,Social_ID) VALUES (1742001,N'Nguyễn Văn A','M','123456789');
INSERT INTO Student (StudentID,Name,Gender,Social_ID) VALUES (1742002,N'Trần Văn B','M','234567891');

SELECT * FROM Student