USE [DB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [BlogAddPost]  
@pTitle varchar(200),  
@pContent nvarchar(max),  
@pAuthor varchar(150), 
@Exdate date,
@creationdate datetime
AS  
BEGIN  
INSERT INTO BlogPosts(PostTitle, DatePosted, PostContent, PostAuthor, ExpirationDate)  
VALUES (@pTitle, @creationdate, @pContent, @pAuthor, @Exdate)  
END  
