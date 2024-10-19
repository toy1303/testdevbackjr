IF OBJECT_ID('dbo.ReporteHorasTrabajdas', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.ReporteHorasTrabajdas;
END
GO
CREATE procedure ReporteHorasTrabajdas
as
BEGIN

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

 SELECT 
   a.User_id,
   b.Nombres + ' '+ b.ApellidoPaterno + ' '+ b.ApellidoMaterno as NombreCompleto,
    SUM(a.Duracionensegunndos) / 3600 AS TotalHoras, -- Total en horas*/
	c.Areaname
FROM 
    #Sesiones a
	JOIN ccUsers b On a.User_id=b.User_id
	join ccRIACat_Areas c on b.IDArea=c.IDArea
GROUP BY 
    a.User_id,b.Nombres,b.ApellidoPaterno,b.ApellidoMaterno,c.Areaname

END