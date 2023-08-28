USE [DB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [BlogDisplayPost]  
@postId int  
AS  
BEGIN  
SELECT PostTitle, PostAuthor, DatePosted, PostContent  
FROM BlogPosts  
WHERE PostId = @postId  
END  
