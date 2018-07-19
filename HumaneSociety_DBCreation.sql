CREATE TABLE Species (SpeciesId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50));
CREATE TABLE DietPlans(DietPlanId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50), FoodType VARCHAR(50), FoodAmountInCups INTEGER);
CREATE TABLE Animals (AnimalId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50), SpeciesId INTEGER FOREIGN KEY REFERENCES Species(SpeciesId), Weight INTEGER, Age INTEGER, DietPlanId INTEGER FOREIGN KEY REFERENCES DietPlans(DietPlanId));
CREATE TABLE Rooms (RoomId INTEGER IDENTITY (1,1) PRIMARY KEY,  AnimalId INTEGER FOREIGN KEY REFERENCES Animals(AnimalId));
CREATE TABLE Shots (ShotId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50));



INSERT INTO Species VALUES('Dog');
INSERT INTO Species VALUES('Cat');
INSERT INTO Species VALUES('Bird');
INSERT INTO Species VALUES('Alligator');
INSERT INTO Species VALUES('Rabbit');



INSERT INTO DietPlans VALUES('Puppychow', 'Dog food', 2);
INSERT INTO DietPlans VALUES('Meowmix', 'Cat food', 2);
INSERT INTO DietPlans VALUES('Birdchow', 'Bird food', 1);
INSERT INTO DietPlans VALUES('Alligator food', 'Ryan', 3);
INSERT INTO DietPlans VALUES('Rabbitchow', 'Rabbit food', 1);

INSERT INTO Rooms VALUES(null);
INSERT INTO Rooms VALUES(null);
INSERT INTO Rooms VALUES(null);
INSERT INTO Rooms VALUES(null);
INSERT INTO Rooms VALUES(null);
INSERT INTO Rooms VALUES(null);
INSERT INTO Rooms VALUES(null);
INSERT INTO Rooms VALUES(null);
INSERT INTO Rooms VALUES(null);
INSERT INTO Rooms VALUES(null);


INSERT INTO Shots VALUES('Dog shot')
INSERT INTO Shots VALUES('Cat shot')
INSERT INTO Shots VALUES('Bird shot')
INSERT INTO Shots VALUES('Alligator shot')
INSERT INTO Shots VALUES('Rabbit shot')

INSERT INTO Animals VALUES('Marley', 1, 240, 27, 1, 'Cuddly', 1, 1, 'Female', 'Available', null);
INSERT INTO Animals VALUES('Garfield', 2, 10, 5, 2, 'Mean', 0, 0, 'Male', 'Available', null);
INSERT INTO Animals VALUES('TheVulture', 3, 1, 2, 3, 'Nice', 1,1, 'Male', 'Available', null);
INSERT INTO Animals VALUES('Dr.Teeth', 4, 150, 18, 4, 'Cuddly', 1, 0, 'Female', 'Available',null);
INSERT INTO Animals VALUES('Jack', 5, 3, 6, 5, 'Timid', 1,1, 'Female', 'Available',null);





