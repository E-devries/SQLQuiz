--
-- FILE : rdA4Data.sql
-- PROJECT : rdA4
-- PROGRAMMER : ELizabeth deVries
-- FIRST VERSION : 2021-12-08
-- DESCRIPTION   : This script creates the database for rdA4, 
--                 which stores questions, multiple choice answers, players, and scores
--


-- Create/Reset the database

DROP DATABASE IF EXISTS `SQLQuizDB`;
CREATE DATABASE IF NOT EXISTS `SQLQuizDB`;
use `SQLQuizDB`;


-- User Data --
DROP USER IF EXISTS 'user'@'127.0.0.1';
CREATE USER 'user'@'127.0.0.1' IDENTIFIED BY 'rda4';
GRANT INSERT, UPDATE, SELECT ON rdA4Data.* TO 'user'@'127.0.0.1';


-- Table Data --


CREATE TABLE `Player` (
	`playerID` int NOT NULL AUTO_INCREMENT,
    `playerName` VARCHAR(40) NOT NULL,
    `totalScore` int NOT NULL DEFAULT 0,
    `totalGameTime` int NOT NULL DEFAULT 0,
    PRIMARY KEY (`playerID`));

    

CREATE TABLE `Question` (
	`questionID` int NOT NULL,
    `questionText` VARCHAR(255) NOT NULL,
    PRIMARY KEY (`questionID`));
 
 

CREATE TABLE `QuestionScore` (
	`playerID` int NOT NULL,
    `questionID` int NOT NULL,
    `answerTime` int NOT NULL DEFAULT 0,
    `score` int NOT NULL DEFAULT 0,
    PRIMARY KEY (`playerID`, `questionID`),
    FOREIGN KEY (`playerID`) REFERENCES `Player` (`playerID`),
    FOREIGN KEY (`questionID`) REFERENCES `Question` (`questionID`));
 
 

CREATE TABLE `Answer` ( 
	`answerID` int NOT NULL AUTO_INCREMENT,
    `questionID` int NOT NULL,
    `isCorrect` bool NOT NULL,
    `answerText` VARCHAR(255) NOT NULL,
    PRIMARY KEY (`answerID`),
    FOREIGN KEY (`questionID`) REFERENCES `Question` (`questionID`));

    

-- Insert Question Data -- 

INSERT INTO `Question` ( `questionID`, `questionText`) VALUES 
	(1, "What folders should you always clean from your projects before submission?"),
    (2, "When should you use method header comments?"),
    (3, "Which is NOT required in a typical SET standards report?"),
    (4, "Which is true about code spacing?"),
    (5, "How should magic numbers be used?"),
    (6, "How should global variables be treated?"),
    (7, "What is the preferred format for most report or document related assignments?"),
    (8, "When should assignments be started?"),
    (9, "How important is your assignment feedback?"),
    (10, "What is the best way to understand the material?");
    
 -- Insert Answer Data for each question -- 
 
 INSERT INTO `Answer` ( `questionID`, `isCorrect`, `answerText`) VALUES
	(1, true, ".vs, bin, and obj"),
    (1, false, ".vs, packages, bin"),
    (1, false, "bin, packages"),
    (1, false, "If your code is clean then your project is clean :)"),
    
    (2, true, "For all methods, unless allowed differently"),
    (2, false, "When the function has a lot of logic"),
    (2, false, "When you want to re-use the code later"),
    (2, false, "Only in Sean's classes"),
    
    (3, true, "Student number"),
    (3, false, "Course identifier"),
    (3, false, "The student's full name"),
    (3, false, "Date of submission"),
    
    (4, true, "A consistent amount of spacing is important for code readability"),
    (4, false, "Spacing is bad because it makes the code too long"),
    (4, false, "There should be three blank lines between every line of code"),
    (4, false, "You should mix up your standards within the same file"),
    
    (5, true, "You should not use them unless there is a reasonable exception"),
    (5, false, "Magic is fun, and so are magic numbers!"),
    (5, false, "Every literal number should be a constant, no matter what"),
    (5, false, "Only use if you are using the same number multiple times "),
    
    (6, true, "Unless you know for sure that you have an exception, burn them"),
    (6, false, "Use them when they are more convenient for you"),
    (6, false, "All the time - they are clearly the superior kind of variable"),
    (6, false, "Using them is illegal and you will go to jail"),
    
    (7, true, "pdf"),
    (7, false, "txt"),
    (7, false, "odt"),
    (7, false, "html"),
    
    (8, true, "The day you get them ideally, or as early as you can"),
    (8, false, "The day before they are due"),
    (8, false, "Only as long before as you need - there's a grace period!"),
    (8, false, "Before they are assigned!"),
    
    (9, true, "Very!"),
    (9, false, "Somewhat, read the feedback at your own discretion "),
    (9, false, "Maybe a little, but feedback is scary so it's better to avoid it"),
    (9, false, "You probably know better than the teachers ;)"),
    
    (10, true, "Attend lectures, labs, and ask questions"),
    (10, false, "Commit academic integrity violations"),
    (10, false, "Sleep while the lecture plays, absorb information via osmosis"),
    (10, false, "Attend lectures and labs but never ask questions");



    
    


