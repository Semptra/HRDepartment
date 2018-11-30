SELECT
	CONVERT(e.id, CHAR(50)) AS `Id`,
	e.firstname AS `First Name`,
	e.lastname AS `Last Name`,
	e.surname AS `Surname`,
	IF(e.gender = 0, 'Male', 'Female') AS `Gender`,
	DATE_FORMAT(e.birthday, '%d-%m-%Y') AS `Birthday`,
	e.placeofbirth AS `Birthplace`,
	e.placeofresidence AS `Residence`,
	e.passportdata AS `Passport data`,
	e.сode AS `Code`,
	(CASE
		WHEN e.fitforarmyserve = 1 THEN 'Yes'
		WHEN e.fitforarmyserve = 0 THEN 'No'
		ELSE ''
	END) AS `Fit for army serve`,
	(CASE

		WHEN e.armyserved = 1 THEN 'Yes'
		WHEN e.armyserved = 0 THEN 'No'
		ELSE ''
	END) AS `Served in the army`,
	ed.type AS `Education Type`,
	ed.name AS `Education Place`
FROM employee e
	LEFT JOIN education ed ON ed.id = e.educationid