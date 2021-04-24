CREATE DATABASE StudentDB;

USE StudentDB;

CREATE TABLE `Students` (
  `studentId` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `firstName` NVARCHAR(255) NOT NULL,
  `lastName` NVARCHAR(255) NOT NULL,
  `address` NVARCHAR(150) NOT NULL,
  `zipCode` CHAR(4) NOT NULL,
  `city` NVARCHAR(50) NOT NULL,
  `cprNumber` CHAR(11) NOT NULL,
  `email` NVARCHAR(255) NOT NULL,
  `phoneNumber`NVARCHAR(16) NOT NULL,
  `nextcloudUsername` nvarchar(255),
  `nextcloudOneTimePassword` nvarchar(255),
  PRIMARY KEY (`studentId`)
)

CREATE TABLE `Subjects`(
`subjectId` INT UNSIGNED NOT NULL AUTO_INCREMENT,
`subjectName` NVARCHAR(255) NOT NULL,
PRIMARY KEY (`subjectId`)
);


CREATE TABLE `Grades`(
`gradeId` INT UNSIGNED NOT NULL AUTO_INCREMENT,
`studentId` INT UNSIGNED NOT NULL,
`subjectId` INT UNSIGNED NOT NULL,
`gradeValue` nvarchar(5) NOT NULL,
`gradeDate` DATE NOT NULL,
PRIMARY KEY (`gradeId`),
CONSTRAINT `fk_studentId` FOREIGN KEY (studentId) REFERENCES Students(studentId),
CONSTRAINT `fk_subjectId` FOREIGN KEY (subjectId) REFERENCES Subjects(subjectId) ON DELETE CASCADE
);

CREATE TABLE `Staff` (
  `staffId` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `firstName` NVARCHAR(255) NOT NULL,
  `lastName` NVARCHAR(255) NOT NULL,
  `email` NVARCHAR(255),
  `teacher` BOOL NOT NULL,
  `administration` BOOL NOT NULL,
  `username` nvarchar(255) NOT NULL,
  `password` nvarchar(255) NOT NULL,
  PRIMARY KEY (`staffId`)
);
