SELECT
	id AS Id,
	firstname AS FirstName,
	lastname AS LastName,
	surname AS Surname,
	IF(gender = 0, 'Male', 'Female') AS Gender,
	birthday AS Birthday,
	сode AS Code
FROM employee