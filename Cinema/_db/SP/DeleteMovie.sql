IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteMovie]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[DeleteMovie]
GO

CREATE PROCEDURE DeleteMovie
	@Id int
AS
BEGIN
	DELETE 
		Movies 
	WHERE 
		Id = @Id
END
GO
