Create Function [dbo].[GLaccountParent](@hid as hierarchyid)
returns bigint
as
begin
declare @parentHid as varbinary(892), @parent as bigint
set @parentHid=cast(@hid.GetAncestor(1) as varbinary(892))
set @parent=(select Account from GLaccounts where hid=@parentHid)
return @parent
end
GO

CREATE TABLE [dbo].[GLaccounts](
	[Account] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[hid] [varbinary](892) NULL,
	[Level]  AS (CONVERT([hierarchyid],[hid]).GetLevel()),
	[parent]  AS ([dbo].[GLaccountParent](CONVERT([hierarchyid],[hid]))),
 CONSTRAINT [PK_GLaccounts] PRIMARY KEY CLUSTERED 
(
	[Account] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
