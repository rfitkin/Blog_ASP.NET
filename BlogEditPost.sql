USE [DB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [BlogEditPost]  
@pTitle varchar(200),  
@pContent nvarchar(max),  
@pAuthor varchar(150),
@pid int,
@Exdate date,
@editdate datetime
AS  
BEGIN  
UPDATE BlogPosts SET PostTitle=@pTitle, DateEdited=@editdate, PostContent=@pContent, PostAuthor=@pAuthor, ExpirationDate=@Exdate WHERE PostId=@pid 
END  
