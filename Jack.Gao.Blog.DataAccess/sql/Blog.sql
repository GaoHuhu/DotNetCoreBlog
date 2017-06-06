USE myblog
GO
IF OBJECT_ID('Proc_Blog_Page','P') IS NOT NULL
	DROP PROCEDURE Proc_Blog_Page
GO
CREATE PROC Proc_Blog_Page
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
SELECT @totalRows=count(*) from blog
SET @total=@totalRows

IF @totalRows%@pageRows<>0
	SET @pageCount=@totalRows/@pageRows+1
ELSE
	SET @pageCount=@totalRows/@pageRows

select @pageCount=@pageCount

SELECT id,title,content,createdtime,updatedtime FROM blog
ORDER BY id OFFSET @pageIndex*@pageRows ROW FETCH NEXT @pageRows ROWS only

GO

DECLARE @mycount INT
declare @total1 int;
EXEC proc_blog_page @pageIndex=3,@pageRows=5,@pageCount=@mycount OUTPUT,@total=@total1 output
SELECT @mycount,@total1;
GO

select * from blog order by id

select 6%6

select  top 10 b.id,b.title,b.content,count(c.blogid) as commentcount 
                        from blog b left join comment c on b.id=c.blogid group by b.title,b.content,
                    c.blogid,b.id order by commentcount desc
