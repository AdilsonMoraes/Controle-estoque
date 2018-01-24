
USE [Controle_Estoque]
GO

/****** Object:  Table [dbo].[Grupo_Produto]    Script Date: 24/01/2018 16:45:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Grupo_Produto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](max) NULL,
	[Ativo] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Grupo_Produto] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


USE [Controle_Estoque]
GO

/****** Object:  Table [dbo].[SobreInformacoes]    Script Date: 24/01/2018 16:45:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SobreInformacoes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LinguagemDev] [nvarchar](max) NULL,
	[BancoDados] [nvarchar](max) NULL,
	[Buildversao] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.SobreInformacoes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


USE [Controle_Estoque]
GO

/****** Object:  Table [dbo].[Usuario]    Script Date: 24/01/2018 16:46:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Usuario](
	[UsuarioId] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](max) NULL,
	[Senha] [nvarchar](max) NULL,
	[Nome] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Usuario] PRIMARY KEY CLUSTERED 
(
	[UsuarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


DECLARE @VERSAO VARCHAR(100) 
SELECT @VERSAO = @@VERSION 

INSERT INTO SobreInformacoes ([LINGUAGEMDEV], [BANCODADOS], [BUILDVERSAO])
VALUES ('.Net Framework 4.5.2',@VERSAO, 1.0)
GO
INSERT INTO [GRUPO_PRODUTO]([Nome], [Ativo]) VALUES ('MOUSES', 1);
GO
INSERT INTO [USUARIO]([LOGIN], [SENHA], [NOME]) VALUES ('Adm', '698d51a19d8a121ce581499d7b701668', 'Administrador'); --111
GO
INSERT INTO [USUARIO]([LOGIN], [SENHA], [NOME]) VALUES ('Adislon', '698d51a19d8a121ce581499d7b701668','Adilson Moraes'); -- 111




