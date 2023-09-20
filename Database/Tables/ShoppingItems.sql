CREATE TABLE [dbo].[ShoppingItems]
(
	[id] UNIQUEIDENTIFIER NOT NULL, 
    [shoppingListId] UNIQUEIDENTIFIER NOT NULL, 
    [userId] NVARCHAR(50) NOT NULL,
    [name] NVARCHAR(100) NOT NULL, 
    [quantity] INT NULL, 
    [creationDate] DATETIME NOT NULL DEFAULT GETDATE(), 
    [important] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_ShoppingItems] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_ShoppingItems_ShoppingLists] FOREIGN KEY ([shoppingListId]) REFERENCES [dbo].[ShoppingLists] ([id]),
);
GO

CREATE NONCLUSTERED INDEX [IX_ShoppingItems] ON [dbo].[ShoppingItems]
(
	[shoppingListId] ASC,
    [userId] ASC,
	[name] ASC
);
GO
