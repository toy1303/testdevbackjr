

IF OBJECT_ID('tempdb..#Temporal1') IS NOT NULL
BEGIN
    DROP TABLE #Temporal1;
END

SELECT 
        User_id,
        fecha,
		TipoMov,
        ROW_NUMBER() OVER (PARTITION BY User_id ORDER BY fecha) AS rn
		INTO #Temporal1
    FROM 
        ccloglogin a



IF OBJECT_ID('tempdb..#Sesiones') IS NOT NULL
BEGIN
    DROP TABLE #Sesiones;
END

select  
        l.User_id,
        DATEDIFF(SECOND, l.fecha, d.fecha) AS Duracionensegunndos
		into #Sesiones
    FROM 
        #Temporal1 l
    JOIN 
        #Temporal1 d ON l.User_id = d.User_id AND l.rn = d.rn - 1
		WHERE 
        l.TipoMov = 1 AND d.TipoMov = 0 -- Logeo seguido de deslogueo

-----Obtiene el Usuario con mas segundo loggeados
SELECT 
    Top 1 User_id,
    SUM(Duracionensegunndos) AS TotalDuracionEnSeggundos,
    SUM(Duracionensegunndos) / 86400 AS Dias, -- Total de días
    (SUM(Duracionensegunndos) % 86400) / 3600 AS Horas, -- Horas restantes
    (SUM(Duracionensegunndos) % 3600) / 60 AS Minutos, -- Minutos restantes
    SUM(Duracionensegunndos) % 60 AS Seconds -- Segundos restantes
FROM 
    #Sesiones
GROUP BY 
    User_id
ORDER BY 
    TotalDuracionEnSeggundos DESC


-----Obtiene el Usuario con menos segundo loggeados
SELECT 
    Top 1 User_id,
    SUM(Duracionensegunndos) AS TotalDuracionEnSeggundos,
    SUM(Duracionensegunndos) / 86400 AS Dias, -- Total de días
    (SUM(Duracionensegunndos) % 86400) / 3600 AS Horas, -- Horas restantes
    (SUM(Duracionensegunndos) % 3600) / 60 AS Minutos, -- Minutos restantes
    SUM(Duracionensegunndos) % 60 AS Seconds -- Segundos restantes
FROM 
    #Sesiones
GROUP BY 
    User_id
ORDER BY 
    TotalDuracionEnSeggundos ASC