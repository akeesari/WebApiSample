	--IF OBJECT_ID('dbo.Task', 'U') IS NOT NULL
	--	BEGIN	
	--		DROP TABLE [dbo].[Task]	
	--	END
	--	CREATE TABLE [dbo].[Task]
	--	(
	--		[Id] [int] IDENTITY NOT  NULL,
	--		[Name] [varchar](200) NOT NULL, 
	--		[IsClosed] bit NOT NULL,
	
	--	 CONSTRAINT [PK_TaskId] PRIMARY KEY CLUSTERED 
	--	(
	--		[Id]  ASC
	--	))


	--	select * from Task