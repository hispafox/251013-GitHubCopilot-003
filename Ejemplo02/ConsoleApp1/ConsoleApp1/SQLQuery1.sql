 CREATE PROCEDURE ObtenerDatosPaginados
            @PageNumber INT,
            @PageSize INT
        AS
        BEGIN
            SET NOCOUNT ON;
            SELECT *
            FROM NombreDeLaTabla
            ORDER BY ColumnaOrden
            OFFSET (@PageNumber - 1) * @PageSize ROWS
            FETCH NEXT @PageSize ROWS ONLY;
        END