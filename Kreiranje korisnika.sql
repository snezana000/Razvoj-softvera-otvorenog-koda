CREATE TABLE [dbo].[Korisnik](
	[IDKorisnika] [int] IDENTITY(1,1) NOT NULL,
	[ImeKorisnika] [varchar](50) NULL,
	[Lozinka] [varchar](50) NULL,
 CONSTRAINT [PK_Korisnik] PRIMARY KEY CLUSTERED 
(
	[IDKorisnika] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO