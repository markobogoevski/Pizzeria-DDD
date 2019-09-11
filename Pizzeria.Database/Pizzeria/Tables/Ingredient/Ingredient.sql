CREATE TABLE [Pizzeria].[Ingredient] (
	[Id]					INT														NOT NULL,
	[CreatedOn]				SMALLDATETIME											NOT NULL,
	[DeletedOn]				SMALLDATETIME											NULL,
	[PizzaFk]				INT														NOT NULL,
	[SysStartTime]			datetime2 (2) GENERATED ALWAYS AS ROW START HIDDEN,   
	[SysEndTime]			datetime2 (2) GENERATED ALWAYS AS ROW END HIDDEN,   
	[PerformedByEmail]		NVARCHAR(255)											NULL, 
	[PerformedByAppName]	NVARCHAR(150)											NULL, 

	PERIOD FOR SYSTEM_TIME (SysStartTime, SysEndTime),
	CONSTRAINT [PK_Ingredient] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Pizza] FOREIGN KEY ([PizzaFk]) REFERENCES [Pizzeria].[Pizza] ([Id])
)