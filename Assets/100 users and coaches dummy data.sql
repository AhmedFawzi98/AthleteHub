---------------dummy users (after the 3 main user in seeder)
DECLARE @i INT = 1;

WHILE @i <= 100
BEGIN
    INSERT INTO [dbo].[AspNetUsers] (
        [Id], [FirstName], [LastName], [Gender], [DateOfBirth], [ProfilePicture], [Bio], [IsActive], [CreatedAt], 
        [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], 
        [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], 
        [LockoutEnd], [LockoutEnabled], [AccessFailedCount]
    ) VALUES (
        NEWID(), 
        N'FirstName' + CAST(@i AS NVARCHAR(3)), 
        N'LastName' + CAST(@i AS NVARCHAR(3)), 
        CASE WHEN @i % 2 = 0 THEN 1 ELSE 0 END, 
        DATEADD(DAY, -(@i * 100), GETDATE()), 
        NULL, 
        N'Bio for user ' + CAST(@i AS NVARCHAR(3)), 
        1, 
        GETDATE(), 
        N'username' + CAST(@i AS NVARCHAR(3)), 
        N'USERNAME' + CAST(@i AS NVARCHAR(3)), 
        N'user' + CAST(@i AS NVARCHAR(3)) + N'@example.com', 
        N'USER' + CAST(@i AS NVARCHAR(3)) + N'@EXAMPLE.COM', 
        1, 
        N'AQAAAAIAAYagAAAAE...' + CAST(@i AS NVARCHAR(3)), 
        NEWID(), 
        NEWID(), 
        N'123456789' + RIGHT('0' + CAST(@i AS NVARCHAR(3)), 3), 
        1, 
        0, 
        NULL, 
        0, 
        0
    );

    SET @i = @i + 1;
END;



---------- coorresponding coaches 
-- Ensure we have at least 100 users in the AspNetUsers table
-- Select exactly 100 user IDs
SELECT TOP 100 [Id] INTO #TempUserIds
FROM [dbo].[AspNetUsers]
ORDER BY NEWID(); -- Randomize the selection

-- Check how many rows were actually selected
DECLARE @UserCount INT;
SELECT @UserCount = COUNT(*) FROM #TempUserIds;

-- Declare a cursor for the user IDs
DECLARE UserCursor CURSOR FOR
SELECT [Id] FROM #TempUserIds;

-- Declare variable to hold user ID
DECLARE @UserId NVARCHAR(450);
DECLARE @i INT = 1;

-- Open the cursor
OPEN UserCursor;

-- Fetch the first user ID
FETCH NEXT FROM UserCursor INTO @UserId;

-- Loop through the cursor and insert records
WHILE @@FETCH_STATUS = 0 AND @i <= 100
BEGIN
    -- Insert the coach record
    INSERT INTO [dbo].[Coaches] (
        [ApplicationUserId], [Certificate], [IsApproved], [RatingsCount], [OverallRating], [IsSuspended]
    ) VALUES (
        @UserId, 
        N'Certificate ' + CAST(@i AS NVARCHAR(3)), 
        1, -- IsApproved = true
        FLOOR(RAND(CHECKSUM(NEWID())) * 96) + 5, -- RatingsCount = random number between 5 and 100
        CAST((RAND(CHECKSUM(NEWID())) * 4) + 1 AS DECIMAL(18, 2)), -- OverallRating = random number between 1 and 5
        0 -- IsSuspended = false
    );

    SET @i = @i + 1;

    -- Fetch the next user ID
    FETCH NEXT FROM UserCursor INTO @UserId;
END

-- Close and deallocate the cursor
CLOSE UserCursor;
DEALLOCATE UserCursor;

-- Clean up temporary table
DROP TABLE #TempUserIds;



------- profile pictures from azure  
USE [AthleteHubDb]
GO
UPDATE TOP (4) dbo.AspNetUsers
SET ProfilePicture = 'https://athletehubstorage.blob.core.windows.net/picturescontainer/1000_F_524378456_7iH4roEgy8t8351zKypjRumlIbRfwhIO.jpg'
WHERE ProfilePicture IS NULL

UPDATE TOP (4) dbo.AspNetUsers
SET ProfilePicture = 'https://athletehubstorage.blob.core.windows.net/picturescontainer/1000s.jpg'
WHERE ProfilePicture IS NULL

UPDATE TOP (4) dbo.AspNetUsers
SET ProfilePicture = 'https://athletehubstorage.blob.core.windows.net/picturescontainer/5X4A2707.jpg'
WHERE ProfilePicture IS NULL

UPDATE TOP (4) dbo.AspNetUsers
SET ProfilePicture = 'https://athletehubstorage.blob.core.windows.net/picturescontainer/african-male-fitness-trainer-in-gym-fitness-and-wellness-african-american-coach-healthy-lifestyle-photo.jpg'
WHERE ProfilePicture IS NULL

UPDATE TOP (4) dbo.AspNetUsers
SET ProfilePicture = 'https://athletehubstorage.blob.core.windows.net/picturescontainer/d31374f3-7741-43bb-9757-8a94a3f424fd_b4.jpg'
WHERE ProfilePicture IS NULL

UPDATE TOP (4) dbo.AspNetUsers
SET ProfilePicture = 'https://athletehubstorage.blob.core.windows.net/picturescontainer/images%20(1).jpeg'
WHERE ProfilePicture IS NULL

UPDATE TOP (4) dbo.AspNetUsers
SET ProfilePicture = 'https://athletehubstorage.blob.core.windows.net/picturescontainer/images%20(10).jpeg'
WHERE ProfilePicture IS NULL

UPDATE TOP (4) dbo.AspNetUsers
SET ProfilePicture = 'https://athletehubstorage.blob.core.windows.net/picturescontainer/images%20(12).jpeg'
WHERE ProfilePicture IS NULL

UPDATE TOP (4) dbo.AspNetUsers
SET ProfilePicture = 'https://athletehubstorage.blob.core.windows.net/picturescontainer/images%20(13).jpeg'
WHERE ProfilePicture IS NULL

UPDATE TOP (4) dbo.AspNetUsers
SET ProfilePicture = 'https://athletehubstorage.blob.core.windows.net/picturescontainer/images%20(14).jpeg'
WHERE ProfilePicture IS NULL

UPDATE TOP (4) dbo.AspNetUsers
SET ProfilePicture = 'https://athletehubstorage.blob.core.windows.net/picturescontainer/images%20(2).jpeg'
WHERE ProfilePicture IS NULL

UPDATE TOP (4) dbo.AspNetUsers
SET ProfilePicture = 'https://athletehubstorage.blob.core.windows.net/picturescontainer/images%20(3).jpeg'
WHERE ProfilePicture IS NULL

UPDATE TOP (4) dbo.AspNetUsers
SET ProfilePicture = 'https://athletehubstorage.blob.core.windows.net/picturescontainer/images%20(5).jpeg'
WHERE ProfilePicture IS NULL

UPDATE TOP (4) dbo.AspNetUsers
SET ProfilePicture = 'https://athletehubstorage.blob.core.windows.net/picturescontainer/images%20(6).jpeg'
WHERE ProfilePicture IS NULL

UPDATE TOP (4) dbo.AspNetUsers
SET ProfilePicture = 'https://athletehubstorage.blob.core.windows.net/picturescontainer/images%20(7).jpeg'
WHERE ProfilePicture IS NULL

UPDATE TOP (4) dbo.AspNetUsers
SET ProfilePicture = 'https://athletehubstorage.blob.core.windows.net/picturescontainer/images%20(9).jpeg'
WHERE ProfilePicture IS NULL

UPDATE TOP (4) dbo.AspNetUsers
SET ProfilePicture = 'https://athletehubstorage.blob.core.windows.net/picturescontainer/images.jpeg'
WHERE ProfilePicture IS NULL

UPDATE TOP (4) dbo.AspNetUsers
SET ProfilePicture = 'https://athletehubstorage.blob.core.windows.net/picturescontainer/istockphoto-1040501222-612x612.jpg'
WHERE ProfilePicture IS NULL

UPDATE TOP (4) dbo.AspNetUsers
SET ProfilePicture = 'https://athletehubstorage.blob.core.windows.net/picturescontainer/istockphoto-1201517718-612x612.jpg'
WHERE ProfilePicture IS NULL

UPDATE TOP (4) dbo.AspNetUsers
SET ProfilePicture = 'https://athletehubstorage.blob.core.windows.net/picturescontainer/istockphoto-675179390-612x612.jpg'
WHERE ProfilePicture IS NULL

UPDATE TOP (4) dbo.AspNetUsers
SET ProfilePicture = 'https://athletehubstorage.blob.core.windows.net/picturescontainer/istockphoto-852401732-612x612.jpg'
WHERE ProfilePicture IS NULL

UPDATE TOP (4) dbo.AspNetUsers
SET ProfilePicture = 'https://athletehubstorage.blob.core.windows.net/picturescontainer/Matt-Warland__ResizedImageWzUwMCw1MDFd.jpg'
WHERE ProfilePicture IS NULL

UPDATE TOP (4) dbo.AspNetUsers
SET ProfilePicture = 'https://athletehubstorage.blob.core.windows.net/picturescontainer/sydney-personal-training-STEVE-GRANT.jpg'
WHERE ProfilePicture IS NULL
GO

UPDATE TOP (4) dbo.AspNetUsers
SET ProfilePicture = 'https://athletehubstorage.blob.core.windows.net/picturescontainer/1000_F_524378456_7iH4roEgy8t8351zKypjRumlIbRfwhIO.jpg'
WHERE ProfilePicture IS NULL


UPDATE TOP (4) dbo.AspNetUsers
SET ProfilePicture = 'https://athletehubstorage.blob.core.windows.net/picturescontainer/istockphoto-675179390-612x612.jpg'
WHERE ProfilePicture IS NULL
