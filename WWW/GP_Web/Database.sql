
CREATE TABLE [VirtualPlay].[Sys_User](
	[idUser] [int] NOT NULL,
	[idRole] [int] NULL,
	[idPerson] [int] NULL,
	[idEnterprise] [int] NULL,
	[nmUser] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[dsEmail] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[dsPassword] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[dtExpire] [datetime] NULL,
	[stUser] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[idUserCreate] [int] NOT NULL,
	[dtCreate] [datetime] NOT NULL,
	[idUserLastUpdate] [int] NOT NULL,
	[dtLastUpdate] [datetime] NOT NULL,
	[dsRealPass] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
 CONSTRAINT [PK_VP_Sys_User] PRIMARY KEY CLUSTERED 
(
	[idUser] ASC
)WITH FILLFACTOR = 75 ON [PRIMARY],
 CONSTRAINT [UQ_VP_dsEmail] UNIQUE NONCLUSTERED 
(
	[dsEmail] ASC
)WITH FILLFACTOR = 75 ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [VirtualPlay].[Sys_UserPasswordHistory](
	[idUserPasswordHistory] [int] IDENTITY(1,1) NOT NULL,
	[idUser] [int] NOT NULL,
	[dsPassword] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[idUserCreate] [int] NOT NULL,
	[dtCreate] [datetime] NOT NULL,
	[idUserLastUpdate] [int] NOT NULL,
	[dtLastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_VP_Sys_UserPasswordHistory] PRIMARY KEY CLUSTERED 
(
	[idUserPasswordHistory] ASC
)WITH FILLFACTOR = 75 ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [VirtualPlay].[Sys_Login](
	[idLogin] [int] IDENTITY(1,1) NOT NULL,
	[dtLogin] [datetime] NOT NULL,
	[idUser] [int] NULL,
	[dsEmail] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[flSuccess] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[cdIPAddress] [char](15) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[dsAgent] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[lGuid] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AI NULL CONSTRAINT [DF_Sys_Login_lGuid]  DEFAULT (newid()),
 CONSTRAINT [PK_VP_Sys_Login] PRIMARY KEY CLUSTERED 
(
	[idLogin] ASC
)WITH FILLFACTOR = 75 ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [VirtualPlay].[Sys_Role](
	[idRole] [int] NOT NULL,
	[nmRole] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[stRole] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[idUserCreate] [int] NOT NULL,
	[dtCreate] [datetime] NOT NULL,
	[idUserLastUpdate] [int] NOT NULL,
	[dtLastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_VP_Sys_Role_Fix] PRIMARY KEY CLUSTERED 
(
	[idRole] ASC
)WITH FILLFACTOR = 75 ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [VirtualPlay].[Sys_Module](
	[idModule] [int] IDENTITY(1,1) NOT NULL,
	[nmModule] [varchar](80) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[dsURLModule] [varchar](300) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[idRole] [int] NOT NULL,
	[idUserCreate] [int] NOT NULL,
	[dtCreate] [datetime] NULL,
	[idUserLastUpdate] [int] NULL,
	[dtLastUpdate] [datetime] NULL,
 CONSTRAINT [PK_VP_Sys_Module] PRIMARY KEY CLUSTERED 
(
	[idModule] ASC
)WITH FILLFACTOR = 75 ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [VirtualPlay].[Sys_Access](
	[idRole] [int] NOT NULL,
	[idModule] [int] NOT NULL,
	[tpAccess] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[idUserCreate] [int] NOT NULL,
	[dtCreate] [datetime] NULL,
	[idUserLastUpdate] [int] NULL,
	[dtLastUpdate] [datetime] NULL,
 CONSTRAINT [PK_VP_Sys_Access] PRIMARY KEY CLUSTERED 
(
	[idRole] ASC,
	[idModule] ASC
)WITH FILLFACTOR = 75 ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [VirtualPlay].[Sys_File](
	[idFile] [int] IDENTITY(1,1) NOT NULL,
	[nmFile] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[dsPath] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[idUserCreate] [int] NOT NULL,
	[dtCreate] [datetime] NOT NULL,
	[idUserLastUpdate] [int] NOT NULL,
	[dtLastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_VP_Sys_File] PRIMARY KEY CLUSTERED 
(
	[idFile] ASC
)WITH FILLFACTOR = 75 ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [VirtualPlay].[ForgotPassword](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdSecretAnswers] [int] NOT NULL,
	[idUser] [int] NOT NULL,
	[Answer] [varchar](5000) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[PasswordHint] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
 CONSTRAINT [PK_VP_ForgotPasswordID] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH FILLFACTOR = 75 ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [VirtualPlay].[Per_Address](
	[idAddress] [int] IDENTITY(1,1) NOT NULL,
	[idPerson] [int] NULL,
	[tpStreet] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[dsAddress] [varchar](80) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[nbAddress] [varchar](15) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[dsComplement] [varchar](70) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[nmDistrict] [varchar](60) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[dsCity] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[cdState] [varchar](2) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[nbPostalCode] [varchar](9) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[cdCountry] [varchar](2) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[tpAddress] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[idUserCreate] [int] NOT NULL,
	[dtCreate] [datetime] NOT NULL,
	[idUserLastUpdate] [int] NOT NULL,
	[dtLastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_VP_Per_Address] PRIMARY KEY CLUSTERED 
(
	[idAddress] ASC
)WITH FILLFACTOR = 75 ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [VirtualPlay].[Per_Telephone](
	[idTelephone] [int] IDENTITY(1,1) NOT NULL,
	[idPerson] [int] NOT NULL,
	[tpTelephone] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[nbAreaCode] [varchar](4) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[nbTelephone] [varchar](15) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[nbExtention] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[idUserCreate] [int] NOT NULL,
	[dtCreate] [datetime] NOT NULL,
	[idUserLastUpdate] [int] NOT NULL,
	[dtLastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_VP_Per_Telephone] PRIMARY KEY CLUSTERED 
(
	[idTelephone] ASC
)WITH FILLFACTOR = 75 ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [VirtualPlay].[Per_Document](
	[idDocument] [int] IDENTITY(1,1) NOT NULL,
	[idPerson] [int] NOT NULL,
	[cdDocument] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[cdDocumentHash] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[tpDocument] [varchar](4) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[idFile] [int] NULL,
	[idUserCreate] [int] NOT NULL,
	[dtCreate] [datetime] NOT NULL,
	[idUserLastUpdate] [int] NOT NULL,
	[dtLastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_VP_Per_Document] PRIMARY KEY CLUSTERED 
(
	[idDocument] ASC
)WITH FILLFACTOR = 75 ON [PRIMARY],
 CONSTRAINT [UQ_Per_cdDocumentHash] UNIQUE NONCLUSTERED 
(
	[cdDocumentHash] ASC
)WITH FILLFACTOR = 75 ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [VirtualPlay].[Per_DocumentType](
	[tpDocument] [varchar](4) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[dsDocumentType] [varchar](80) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[flEncrypted] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[idUserCreate] [int] NOT NULL,
	[dtCreate] [datetime] NOT NULL,
	[idUserLastUpdate] [int] NOT NULL,
	[dtLastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_VP_Per_DocumentType] PRIMARY KEY CLUSTERED 
(
	[tpDocument] ASC
)WITH FILLFACTOR = 75 ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [VirtualPlay].[Ent_Address](
	[idAddress] [int] IDENTITY(1,1) NOT NULL,
	[idEnterprise] [int] NULL,
	[tpStreet] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[dsAddress] [varchar](80) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[nbAddress] [varchar](15) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[dsComplement] [varchar](70) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[nmDistrict] [varchar](60) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[dsCity] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[cdState] [varchar](2) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[nbPostalCode] [varchar](9) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[cdCountry] [varchar](2) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[tpAddress] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[idUserCreate] [int] NOT NULL,
	[dtCreate] [datetime] NOT NULL,
	[idUserLastUpdate] [int] NOT NULL,
	[dtLastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_VP_Ent_Address] PRIMARY KEY CLUSTERED 
(
	[idAddress] ASC
)WITH FILLFACTOR = 75 ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [VirtualPlay].[Ent_Telephone](
	[idTelephone] [int] IDENTITY(1,1) NOT NULL,
	[idEnterprise] [int] NOT NULL,
	[tpTelephone] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[nbAreaCode] [varchar](4) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[nbTelephone] [varchar](15) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[nbExtention] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[idUserCreate] [int] NOT NULL,
	[dtCreate] [datetime] NOT NULL,
	[idUserLastUpdate] [int] NOT NULL,
	[dtLastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_VP_Ent_Telephone] PRIMARY KEY CLUSTERED 
(
	[idTelephone] ASC
)WITH FILLFACTOR = 75 ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [VirtualPlay].[Ent_Document](
	[idDocument] [int] IDENTITY(1,1) NOT NULL,
	[idEnterprise] [int] NOT NULL,
	[cdDocument] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[cdDocumentHash] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[tpDocument] [varchar](4) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[idFile] [int] NULL,
	[idUserCreate] [int] NOT NULL,
	[dtCreate] [datetime] NOT NULL,
	[idUserLastUpdate] [int] NOT NULL,
	[dtLastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_VP_Ent_Document] PRIMARY KEY CLUSTERED 
(
	[idDocument] ASC
)WITH FILLFACTOR = 75 ON [PRIMARY],
 CONSTRAINT [UQ_Ent_cdDocumentHash] UNIQUE NONCLUSTERED 
(
	[cdDocumentHash] ASC
)WITH FILLFACTOR = 75 ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [VirtualPlay].[Ent_DocumentType](
	[tpDocument] [varchar](4) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[dsDocumentType] [varchar](80) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[flEncrypted] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[idUserCreate] [int] NOT NULL,
	[dtCreate] [datetime] NOT NULL,
	[idUserLastUpdate] [int] NOT NULL,
	[dtLastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_VP_Ent_DocumentType] PRIMARY KEY CLUSTERED 
(
	[tpDocument] ASC
)WITH FILLFACTOR = 75 ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [VirtualPlay].[Per_Person](
	[idPerson] [int] IDENTITY(1,1) NOT NULL,
	[idPersonSource] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[nmPerson] [varchar](110) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[nmNick] [varchar](80) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[dtBirth] [datetime] NULL,
	[tpGender] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[dsEmail] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[dsOccupation] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[cdMaritalStatus] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[flStatus] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[idUserCreate] [int] NOT NULL,
	[dtCreate] [datetime] NOT NULL,
	[idUserLastUpdate] [int] NOT NULL,
	[dtLastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_VP_Per_Person] PRIMARY KEY CLUSTERED 
(
	[idPerson] ASC
)WITH FILLFACTOR = 75 ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [VirtualPlay].[Ent_Enterprise](
	[idEnterprise] [int] IDENTITY(1,1) NOT NULL,
	[idEnterpriseSource] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[nmEnterprise] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[nmNick] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[dtBirth] [datetime] NULL,
	[dsEmail] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[dsOccupation] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[flStatus] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[idUserCreate] [int] NOT NULL,
	[dtCreate] [datetime] NOT NULL,
	[idUserLastUpdate] [int] NOT NULL,
	[dtLastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_VP_Ent_Enterprise] PRIMARY KEY CLUSTERED 
(
	[idEnterprise] ASC
)WITH FILLFACTOR = 75 ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [VirtualPlay].[Sys_System](
	[idSystem] [int] NOT NULL,
	[nmSystem] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[dsSystem] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[cdSystem] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
	[dtExpire] [datetime] NULL,
	[idUserCreate] [int] NOT NULL,
	[dtCreate] [datetime] NOT NULL,
	[idUserLastUpdate] [int] NOT NULL,
	[dtLastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_VP_Sys_System] PRIMARY KEY CLUSTERED 
(
	[idSystem] ASC
)WITH FILLFACTOR = 75 ON [PRIMARY],
 CONSTRAINT [UQ_VP_cdSystem] UNIQUE NONCLUSTERED 
(
	[cdSystem] ASC
)WITH FILLFACTOR = 75 ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [VirtualPlay].[Sys_SystemType](
	[tpSystem] [varchar](4) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[dsSystemType] [varchar](80) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[flEncrypted] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[idUserCreate] [int] NOT NULL,
	[dtCreate] [datetime] NOT NULL,
	[idUserLastUpdate] [int] NOT NULL,
	[dtLastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_VP_Sys_SystemType] PRIMARY KEY CLUSTERED 
(
	[tpSystem] ASC
)WITH FILLFACTOR = 75 ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [VirtualPlay].[Sys_UserSystem](
	[idUser] [int] NOT NULL,
	[idSystem] [int] NOT NULL,
	[idRole] [int] NOT NULL,
	[flStatus] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
	[dtCreate] [datetime] NOT NULL,
	[idUserLastUpdate] [int] NOT NULL,
	[dtLastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_VP_Sys_UserSystem] PRIMARY KEY CLUSTERED 
(
	[idUser] ASC,
	[idSystem] ASC
)WITH FILLFACTOR = 75 ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [VirtualPlay].[Sys_Access] 
ADD CONSTRAINT FK_VP_Sys_Access_Role
FOREIGN KEY (idRole) REFERENCES [VirtualPlay].[Sys_Role](idRole);

GO

ALTER TABLE [VirtualPlay].[Sys_Access] 
ADD CONSTRAINT FK_VP_Sys_Access_Module
FOREIGN KEY (idModule) REFERENCES [VirtualPlay].[Sys_Module](idModule);

GO

ALTER TABLE [VirtualPlay].[Sys_User] 
ADD CONSTRAINT FK_VP_Sys_User_Role
FOREIGN KEY (idRole) REFERENCES [VirtualPlay].[Sys_Role](idRole);

GO

ALTER TABLE [VirtualPlay].[Sys_User] 
ADD CONSTRAINT FK_VP_Sys_User_Person
FOREIGN KEY (idPerson) REFERENCES [VirtualPlay].[Per_Person](idPerson);

GO

ALTER TABLE [VirtualPlay].[Sys_User] 
ADD CONSTRAINT FK_VP_Sys_User_Enterprise
FOREIGN KEY (idEnterprise) REFERENCES [VirtualPlay].[Ent_Enterprise](idEnterprise);

GO

ALTER TABLE [VirtualPlay].[Per_Address]
ADD CONSTRAINT FK_VP_Per_Address_Person
FOREIGN KEY (idPerson) REFERENCES [VirtualPlay].[Per_Person](idPerson);

GO

ALTER TABLE [VirtualPlay].[Per_Telephone]
ADD CONSTRAINT FK_VP_Per_Telephone_Person
FOREIGN KEY (idPerson) REFERENCES [VirtualPlay].[Per_Person](idPerson);

GO

ALTER TABLE [VirtualPlay].[Per_Document]
ADD CONSTRAINT FK_VP_Per_Document_Person
FOREIGN KEY (idPerson) REFERENCES [VirtualPlay].[Per_Person](idPerson);

GO

ALTER TABLE [VirtualPlay].[Per_Document]
ADD CONSTRAINT FK_VP_Per_Document_TpDocument
FOREIGN KEY (tpDocument) REFERENCES [VirtualPlay].[Per_DocumentType](tpDocument);

GO

ALTER TABLE [VirtualPlay].[Per_Document]
ADD CONSTRAINT FK_VP_Per_Document_File
FOREIGN KEY (idFile) REFERENCES [VirtualPlay].[Sys_File](idFile);

GO

ALTER TABLE [VirtualPlay].[Sys_User] 
ADD CONSTRAINT FK_VP_Sys_User_UserCreate
FOREIGN KEY (idUserCreate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Sys_UserPasswordHistory] 
ADD CONSTRAINT FK_VP_Sys_UserPasswordHistory_UserCreate
FOREIGN KEY (idUserCreate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Sys_System] 
ADD CONSTRAINT FK_VP_Sys_System_UserCreate
FOREIGN KEY (idUserCreate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Sys_SystemType] 
ADD CONSTRAINT FK_VP_Sys_SystemType_UserCreate
FOREIGN KEY (idUserCreate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Sys_Role] 
ADD CONSTRAINT FK_VP_Sys_Role_UserCreate
FOREIGN KEY (idUserCreate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Sys_Module] 
ADD CONSTRAINT FK_VP_Sys_Module_UserCreate
FOREIGN KEY (idUserCreate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Sys_File] 
ADD CONSTRAINT FK_VP_Sys_File_UserCreate
FOREIGN KEY (idUserCreate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Sys_Access] 
ADD CONSTRAINT FK_VP_Sys_Access_UserCreate
FOREIGN KEY (idUserCreate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Per_Telephone] 
ADD CONSTRAINT FK_VP_Per_Telephone_UserCreate
FOREIGN KEY (idUserCreate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Per_Person] 
ADD CONSTRAINT FK_VP_Per_Person_UserCreate
FOREIGN KEY (idUserCreate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Per_DocumentType] 
ADD CONSTRAINT FK_VP_Per_DocumentType_UserCreate
FOREIGN KEY (idUserCreate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Per_Document] 
ADD CONSTRAINT FK_VP_Per_Document_UserCreate
FOREIGN KEY (idUserCreate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Per_Address] 
ADD CONSTRAINT FK_VP_Per_Address_UserCreate
FOREIGN KEY (idUserCreate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[ForgotPassword] 
ADD CONSTRAINT FK_VP_ForgotPassword_UserCreate
FOREIGN KEY (idUserCreate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Ent_Telephone] 
ADD CONSTRAINT FK_VP_Ent_Telephone_UserCreate
FOREIGN KEY (idUserCreate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Ent_Enterprise] 
ADD CONSTRAINT FK_VP_Ent_Enterprise_UserCreate
FOREIGN KEY (idUserCreate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Ent_DocumentType] 
ADD CONSTRAINT FK_VP_Ent_DocumentType_UserCreate
FOREIGN KEY (idUserCreate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Ent_Document] 
ADD CONSTRAINT FK_VP_Ent_Document_UserCreate
FOREIGN KEY (idUserCreate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Ent_Address] 
ADD CONSTRAINT FK_VP_Ent_Address_UserCreate
FOREIGN KEY (idUserCreate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Sys_User] 
ADD CONSTRAINT FK_VP_Sys_User_UserLastUpdate
FOREIGN KEY (idUserLastUpdate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Sys_UserPasswordHistory] 
ADD CONSTRAINT FK_VP_Sys_UserPasswordHistory_UserLastUpdate
FOREIGN KEY (idUserLastUpdate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Sys_System] 
ADD CONSTRAINT FK_VP_Sys_System_UserLastUpdate
FOREIGN KEY (idUserLastUpdate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Sys_SystemType] 
ADD CONSTRAINT FK_VP_Sys_SystemType_UserLastUpdate
FOREIGN KEY (idUserLastUpdate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Sys_Role] 
ADD CONSTRAINT FK_VP_Sys_Role_UserLastUpdate
FOREIGN KEY (idUserLastUpdate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Sys_Module] 
ADD CONSTRAINT FK_VP_Sys_Module_UserLastUpdate
FOREIGN KEY (idUserLastUpdate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Sys_File] 
ADD CONSTRAINT FK_VP_Sys_File_UserLastUpdate
FOREIGN KEY (idUserLastUpdate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Sys_Access] 
ADD CONSTRAINT FK_VP_Sys_Access_UserLastUpdate
FOREIGN KEY (idUserLastUpdate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Per_Telephone] 
ADD CONSTRAINT FK_VP_Per_Telephone_UserLastUpdate
FOREIGN KEY (idUserLastUpdate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Per_Person] 
ADD CONSTRAINT FK_VP_Per_Person_UserLastUpdate
FOREIGN KEY (idUserLastUpdate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Per_DocumentType] 
ADD CONSTRAINT FK_VP_Per_DocumentType_UserLastUpdate
FOREIGN KEY (idUserLastUpdate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Per_Address] 
ADD CONSTRAINT FK_VP_Per_Address_UserLastUpdate
FOREIGN KEY (idUserLastUpdate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Per_Document] 
ADD CONSTRAINT FK_VP_Per_Document_UserLastUpdate
FOREIGN KEY (idUserLastUpdate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[ForgotPassword] 
ADD CONSTRAINT FK_VP_ForgotPassword_UserLastUpdate
FOREIGN KEY (idUserLastUpdate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Ent_Telephone] 
ADD CONSTRAINT FK_VP_Ent_Telephone_UserLastUpdate
FOREIGN KEY (idUserLastUpdate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Ent_Enterprise] 
ADD CONSTRAINT FK_VP_Ent_Enterprise_UserLastUpdate
FOREIGN KEY (idUserLastUpdate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Ent_DocumentType] 
ADD CONSTRAINT FK_VP_Ent_DocumentType_UserLastUpdate
FOREIGN KEY (idUserLastUpdate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Ent_Document] 
ADD CONSTRAINT FK_VP_Ent_Document_UserLastUpdate
FOREIGN KEY (idUserLastUpdate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Ent_Address] 
ADD CONSTRAINT FK_VP_Ent_Address_UserLastUpdate
FOREIGN KEY (idUserLastUpdate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Sys_UserSystem] 
ADD CONSTRAINT FK_VP_Sys_UserSystem_UserLastUpdate
FOREIGN KEY (idUserLastUpdate) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Sys_UserPasswordHistory] 
ADD CONSTRAINT FK_VP_Sys_UserPasswordHistory_User
FOREIGN KEY (idUser) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Sys_System] 
ADD CONSTRAINT FK_VP_Sys_System_Type
FOREIGN KEY (tpSystem) REFERENCES [VirtualPlay].[Sys_SystemType](tpSystem);

GO


ALTER TABLE [VirtualPlay].[Sys_UserSystem] 
ADD CONSTRAINT FK_VP_Sys_UserSystem_User
FOREIGN KEY (idUser) REFERENCES [VirtualPlay].[Sys_User](idUser);

GO

ALTER TABLE [VirtualPlay].[Sys_UserSystem] 
ADD CONSTRAINT FK_VP_Sys_UserSystem_System
FOREIGN KEY (idSystem) REFERENCES [VirtualPlay].[Sys_System](idSystem);

GO

ALTER TABLE [VirtualPlay].[Sys_UserSystem] 
ADD CONSTRAINT FK_VP_Sys_UserSystem_Role
FOREIGN KEY (idRole) REFERENCES [VirtualPlay].[Sys_Role](idRole);

GO

ALTER TABLE [VirtualPlay].[Ent_Document] 
ADD CONSTRAINT FK_VP_Ent_Document_Enterprise
FOREIGN KEY (idEnterprise) REFERENCES [VirtualPlay].[Ent_Enterprise](idEnterprise);

GO

ALTER TABLE [VirtualPlay].[Ent_Document] 
ADD CONSTRAINT FK_VP_Ent_Document_Type
FOREIGN KEY (tpDocument) REFERENCES [VirtualPlay].[Ent_DocumentType](tpDocument);

GO

ALTER TABLE [VirtualPlay].[Ent_Document] 
ADD CONSTRAINT FK_VP_Ent_Document_File
FOREIGN KEY (idFile) REFERENCES [VirtualPlay].[Sys_File](idFile);

GO

ALTER TABLE [VirtualPlay].[Ent_Address] 
ADD CONSTRAINT FK_VP_Ent_Address_Enterprise
FOREIGN KEY (idEnterprise) REFERENCES [VirtualPlay].[Ent_Enterprise](idEnterprise);

GO

ALTER TABLE [VirtualPlay].[Ent_Telephone] 
ADD CONSTRAINT FK_VP_Ent_Telephone_Enterprise
FOREIGN KEY (idEnterprise) REFERENCES [VirtualPlay].[Ent_Enterprise](idEnterprise);

GO


CREATE TABLE [VirtualPlay].[webmail_conf](
	[id] [int] NOT NULL IDENTITY(1,1),
	[from] [nvarchar](120) NOT NULL,
	[host] [nvarchar](120) NOT NULL,
	[port] [int] NOT NULL,
	[userName] [nvarchar](120) NOT NULL,
	[password] [nvarchar](120) NOT NULL,
	[sequence] INT NOT NULL,
	[mail_sent] [bit] NULL,
	[created_at] [datetime] NOT NULL,
	[updated_at] [datetime] NULL,
	[status] [bit] NOT NULL
)

GO

ALTER TABLE [VirtualPlay].[webmail_conf]
ADD PRIMARY KEY(id);

GO