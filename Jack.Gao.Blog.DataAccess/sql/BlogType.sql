USE myblog
GO
IF OBJECT_ID('Proc_BlogType_Page','P') IS NOT NULL
	DROP PROCEDURE Proc_BlogType_Page
GO
CREATE PROC Proc_BlogType_Page
	@pageIndex INT,
	@pageRows INT,
	@pageCount INT OUTPUT,
	@total int output
AS
IF @pageIndex<1
   SET @pageIndex=1
ELSE
	SET @pageIndex=@pageIndex-1

DECLARE @totalRows INT
SELECT @totalRows=count(*) from blogtype
SET @total=@totalRows

IF @totalRows%@pageRows<>0
	SET @pageCount=@totalRows/@pageRows+1
ELSE
	SET @pageCount=@totalRows/@pageRows

SELECT id,name,createdtime,updatedtime FROM blogtype
ORDER BY id OFFSET @pageIndex*@pageRows ROW FETCH NEXT @pageRows ROWS only

GO

DECLARE @mycount INT
declare @total1 int;
EXEC Proc_BlogType_Page @pageIndex=1,@pageRows=5,@pageCount=@mycount OUTPUT,@total=@total1 output
SELECT @mycount,@total1;
GO

select * from blog order by id

select 6%6
