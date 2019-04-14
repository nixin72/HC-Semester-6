CREATE TABLE [Resources].[SecurityQuestions]
(
[IDSecurityQuestion] [int] NOT NULL IDENTITY(1, 1),
[Text] [varchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
GRANT SELECT ON  [Resources].[SecurityQuestions] TO [CSAdmin]
GRANT SELECT ON  [Resources].[SecurityQuestions] TO [RAC]
GO
