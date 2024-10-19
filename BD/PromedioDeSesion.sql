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



IF OBJECT_ID('tempdb..#SesionesDuracion') IS NOT NULL
BEGIN
    DROP TABLE #SesionesDuracion;
END

select  
        l.User_id,
		YEAR(l.fecha) AS anio,
        MONTH(l.fecha) AS mes,
        DATEDIFF(SECOND, l.fecha, d.fecha) AS DuracionEnsegundos
		into #SesionesDuracion
    FROM 
        #Temporal1 l
    JOIN 
        #Temporal1 d ON l.User_id = d.User_id AND l.rn = d.rn - 1
		WHERE 
        l.TipoMov = 1 AND d.TipoMov = 0 -- Logeo seguido de deslogueo
------------------------------------
IF OBJECT_ID('tempdb..#PromedioDuracion') IS NOT NULL
BEGIN
    DROP TABLE #PromedioDuracion;
END

SELECT 
    User_id,
    anio,
    mes,
    AVG(DuracionEnsegundos) AS TotalSegundos -- Promedio de duración en segundos
	into #PromedioDuracion
FROM 
    #SesionesDuracion
GROUP BY 
    User_id, anio, mes



SELECT 
    'El Usuario: ' + CAST(a.User_id AS nvarchar(max)) + 
    ' | Año: ' + CAST(a.anio AS nvarchar(max)) + 
    ' | Mes: ' + CAST(a.mes AS nvarchar(max)) + 
    ' | Tiempo Total: ' + 
    CAST(a.TotalSegundos / 86400 AS nvarchar(max)) + ' días ' +
    CAST((a.TotalSegundos % 86400) / 3600 AS nvarchar(max)) + ' horas ' +
    CAST((a.TotalSegundos % 3600) / 60 AS nvarchar(max)) + ' minutos ' +
    CAST(a.TotalSegundos % 60 AS nvarchar(max)) + ' segundos'
FROM #PromedioDuracion a;