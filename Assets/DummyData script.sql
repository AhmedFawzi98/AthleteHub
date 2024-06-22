-- Table Athletes
declare @athleteApplicationUserId varchar(450) = (select Top 1 id from dbo.AspNetUsers where FirstName = 'athlete')
INSERT INTO Athletes (ApplicationUserId, SubscribtionId) VALUES
(@athleteApplicationUserId, 1)
Go
-- Table Coaches
declare @coachApplicationUserId varchar(450) = (select Top 1 id from dbo.AspNetUsers where FirstName = 'coach')
INSERT INTO Coaches (ApplicationUserId, Certificate, IsApproved, RatingsCount, OverallRating, IsSuspended) VALUES
(@coachApplicationUserId, 'CertifcateUrl', 1, 1, 4.5, 0)
GO
-- Table AthletesCoaches
INSERT INTO AthletesCoaches (AthleteId, CoachId, IsCurrentlySubscribed) VALUES
(1, 1, 1)
Go
-- Table Subscribtions
INSERT INTO Subscribtions (CoachId, Name, Price, DurationInMonths) VALUES
(1, 'Basic Plan', 29.99, 1),
(1, 'Standard Plan', 59.99, 3),
(1, 'Premium Plan', 99.99, 6)
GO
-- Table AthleteActiveSubscribtions
INSERT INTO AthleteActiveSubscribtions (AthleteID, SubscribtionId, SubscribtionStartDate, SubscribtionEndDate) VALUES
(1, 1, '2024-06-01', '2024-09-01')
GO
-- Table SubscribtionFeatures
INSERT INTO SubscribtionFeatures (SubscribtionId, FeatureId) VALUES
(1, 1),
(1, 5),
(1, 7),
(2, 1),
(2, 5),
(2, 7),
(2, 4),
(2, 9),
(3, 1),
(3, 5),
(3, 7),
(3, 3),
(3, 9),
(3, 11),
(3, 12)
GO


-- Table AthletesFavouriteCoaches
INSERT INTO AthletesFavouriteCoaches (AthleteId, CoachId) VALUES
(1, 1)
GO

-- Table CoachesRatings
INSERT INTO CoachesRatings (AthleteId, CoachId, Rate, Comment) VALUES
(1, 1, 4.5, 'Great coach!')
GO

-- Table Measurments
INSERT INTO Measurements (AthleteId, Date, WeightInKg, BodyFatPercentage, BMI, BenchPressWeight, SquatWeight, DeadliftWeight) VALUES
(1, '2024-06-01', 70.5, 15.5, 22.0, 100, 120, 140),
(1, '2024-06-08', 75.0, 17.0, 23.5, 110, 130, 150),
(1, '2024-06-15', 80.0, 18.0, 25.0, 120, 140, 160),
(1, '2024-06-21', 85.0, 19.0, 26.5, 130, 150, 170);
GO
