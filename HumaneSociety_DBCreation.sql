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

INSERT INTO Animals VALUES('Marley', SELECT SpeciesId FROM Species WHERE Name='Dog', 240, 27, SELECT DietPlanId FROM DietPlans WHERE Name='Puppychow');
INSERT INTO Animals VALUES('Garfield', SELECT SpeciesId FROM Species WHERE Name='Cat', 10, 5, SELECT DietPlanId FROM DietPlans WHERE Name='Meowmix');
INSERT INTO Animals VALUES('TheVulture', SELECT SpeciesId FROM Species WHERE Name='Bird', 1, 2, SELECT DietPlanId FROM DietPlans WHERE Name='Birdchow');
INSERT INTO Animals VALUES('Dr.Teeth', SELECT SpeciesId FROM Species WHERE Name='Alligator', 150, 18, SELECT DietPlanId FROM DietPlans WHERE Name='Alligator food');
INSERT INTO Animals VALUES('Jack', SELECT SpeciesId FROM Species WHERE Name='Rabbit', 3, 6, SELECT DietPlanId FROM DietPlans WHERE Name='Rabbitchow');





