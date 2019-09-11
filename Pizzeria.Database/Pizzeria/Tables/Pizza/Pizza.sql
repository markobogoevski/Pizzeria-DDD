CREATE TABLE [Pizzeria].[Pizza] (
	[Id]					INT  IDENTITY (1, 1)			NOT NULL,
	[Uid]					UNIQUEIDENTIFIER				NOT NULL,
	[CreatedOn]				SMALLDATETIME					NOT NULL,
	[DeletedOn]				SMALLDATETIME					NULL,
	[Name]					NVARCHAR (150)					NULL,
	[Size]					INT          					NOT NULL,
	[PerformedByEmail]		NVARCHAR(255)					NULL, 
	[PerformedByAppName]	NVARCHAR(150)					NULL, 
	[SysStartTime]			datetime2 (2) GENERATED ALWAYS AS ROW START HIDDEN,   
	[SysEndTime]			datetime2 (2) GENERATED ALWAYS AS ROW END HIDDEN,   
	PERIOD FOR SYSTEM_TIME (SysStartTime, SysEndTime),

	CONSTRAINT [PK_Pizza] PRIMARY KEY CLUSTERED ([Id] ASC)
)