USE TodoListDb;

DECLARE @i INT;
SET @i = 0;

WHILE @i < 20

BEGIN
	SET @i += 1;
	INSERT INTO Todos (Name, Completed, IsDeleted) 
	VALUES ('A Todo ' + str(@i), 0, 0)
END

SELECT * FROM Todos;