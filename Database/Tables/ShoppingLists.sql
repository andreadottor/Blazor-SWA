CREATE TABLE [dbo].[ShoppingLists]
(
	[id] UNIQUEIDENTIFIER NOT NULL, 
    [userId] NVARCHAR(50) NOT NULL, 
    [title] NVARCHAR(100) NOT NULL, 
    [creationDate] DATETIME NOT NULL DEFAULT GETDATE(), 
    [completedDate] DATETIME NULL, 
    [favorite] BIT NOT NULL DEFAULT 0,
    CONSTRAINT [PK_ShoppingLists] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

CREATE NONCLUSTERED INDEX [IX_ShoppingLists] ON [dbo].[ShoppingLists]
(
    [userId] ASC,
	[title] ASC
);
GO
