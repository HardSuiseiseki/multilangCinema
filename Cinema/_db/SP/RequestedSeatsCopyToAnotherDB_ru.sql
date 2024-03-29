USE [cinema_course_ru]
GO
/****** Object:  Trigger [dbo].[RequestedSeatsCopyToAnotherDB]    Script Date: 04.06.2019 20:41:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TRIGGER [dbo].[RequestedSeatsCopyToAnotherDB]
   ON  [dbo].[RequestedSeats]
   AFTER INSERT AS 
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [cinema_course].dbo.RequestedSeats 
        (Row, Seat, Status, TimeslotId)
		SELECT Row, Seat, Status, TimeslotId
        FROM inserted
END
