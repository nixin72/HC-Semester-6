SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE procedure [dbo].[uspQueryAD] (@LDAP_Query varchar(255)='', @Verbose bit=0) 
as 
  
--verify proper usage and display help if not used properly 
if @LDAP_Query ='' --argument was not passed 
       BEGIN 
       Print '' 
       Print 'spQueryAD is a stored procedure to query active directory without the default 1000 record LDAP query limit' 
       Print '' 
       Print 'usage — Exec spQueryAD ''_LDAP_Query_'', Verbose_Output(0 or 1, optional)'
       Print '' 
       Print 'example: Exec spQueryAD ''SELECT EmployeeID, SamAccountName FROM ''''LDAP://dc=domain,dc=com'''' WHERE objectCategory=''''person'''' and objectclass=''''user'''''', 1' 
       Print '' 
       Print 'spQueryAD returns records corresponding to fields specified in LDAP query.' 
       Print 'Use INSERT INTO statement to capture results in temp table.' 
       Return --'spQueryAD aborted' 
       END 
  
--declare variables 
DECLARE @ADOconn INT -- ADO Connection object 
         , @ADOcomm INT -- ADO Command object 
         , @ADOcommprop INT -- ADO Command object properties pointer 
         , @ADOcommpropVal INT -- ADO Command object properties value pointer 
         , @ADOrs INT -- ADO RecordSet object 
         , @OLEreturn INT -- OLE return value 
         , @src varchar(255) -- OLE Error Source 
         , @desc varchar(255) -- OLE Error Description 
         , @PageSize INT -- variable for paging size Setting 
         , @StatusStr char(255) -- variable for current status message for verbose output


 
  
SET @PageSize = 1000 -- IF not SET LDAP query will return max of 1000 rows 
  
--Create the ADO connection object 
IF @Verbose=1 
       BEGIN 
       Set @StatusStr = 'Create ADO connection…' 
       Print @StatusStr 
       END 
EXEC @OLEreturn = sp_OACreate 'ADODB.Connection', @ADOconn OUT 
IF @OLEreturn <> 0 
       BEGIN -- Return OLE error 
                EXEC sp_OAGetErrorInfo @ADOconn , @src OUT, @desc OUT 
                SELECT Error=CONVERT(varbinary(4),@OLEreturn), Source=@src, Description=@desc 
                RETURN 
       END 
IF @Verbose=1 Print Space(len(@StatusStr)) + 'done.' 
  
--SET the provider property to ADsDSOObject to point to Active Directory 
IF @Verbose=1 
       BEGIN 
       Set @StatusStr = 'Set ADO connection to use Active Directory driver…' 
       Print @StatusStr 
       END 
EXEC @OLEreturn = sp_OASETProperty @ADOconn , 'Provider', 'ADsDSOObject' 
IF @OLEreturn <> 0 
       BEGIN -- Return OLE error 
                EXEC sp_OAGetErrorInfo @ADOconn , @src OUT, @desc OUT 
                SELECT Error=CONVERT(varbinary(4),@OLEreturn), Source=@src, Description=@desc 
                RETURN 
       END 
IF @Verbose=1 Print Space(len(@StatusStr)) + 'done.' 
  
--Open the ADO connection 
IF @Verbose=1 
       BEGIN 
       Set @StatusStr = 'Open the ADO connection…' 
       Print @StatusStr 
       END 
EXEC @OLEreturn = sp_OAMethod @ADOconn , 'Open' 
IF @OLEreturn <> 0 
       BEGIN -- Return OLE error 
                EXEC sp_OAGetErrorInfo @ADOconn , @src OUT, @desc OUT 
                SELECT Error=CONVERT(varbinary(4),@OLEreturn), Source=@src, Description=@desc 
                RETURN 
       END 
IF @Verbose=1 Print Space(len(@StatusStr)) + 'done.' 
  
--Create the ADO command object 
IF @Verbose=1 
       BEGIN 
       Set @StatusStr = 'Create ADO command object…' 
       Print @StatusStr 
       END 
EXEC @OLEreturn = sp_OACreate 'ADODB.Command', @ADOcomm OUT 
IF @OLEreturn <> 0 
       BEGIN -- Return OLE error 
                EXEC sp_OAGetErrorInfo @ADOcomm , @src OUT, @desc OUT 
                SELECT Error=CONVERT(varbinary(4),@OLEreturn), Source=@src, Description=@desc 
                RETURN 
       END 
IF @Verbose=1 Print Space(len(@StatusStr)) + 'done.' 
  
--SET the ADO command object to use the connection object created first 
IF @Verbose=1 
       BEGIN 
       Set @StatusStr = 'Set ADO command object to use Active Directory connection…' 
       Print @StatusStr 
       END 
EXEC @OLEreturn = sp_OASETProperty @ADOcomm, 'ActiveConnection', 'Provider=''ADsDSOObject''' 
IF @OLEreturn <> 0 
       BEGIN -- Return OLE error 
                EXEC sp_OAGetErrorInfo @ADOcomm , @src OUT, @desc OUT 
                SELECT Error=CONVERT(varbinary(4),@OLEreturn), Source=@src, Description=@desc 
                RETURN 
       END 
IF @Verbose=1 Print Space(len(@StatusStr)) + 'done.' 
  
--Get a pointer to the properties SET of the ADO Command Object 
IF @Verbose=1 
       BEGIN 
       Set @StatusStr = 'Retrieve ADO command properties…' 
       Print @StatusStr 
       END 
EXEC @OLEreturn = sp_OAGetProperty @ADOcomm, 'Properties', @ADOcommprop out 
IF @OLEreturn <> 0 
       BEGIN -- Return OLE error 
                EXEC sp_OAGetErrorInfo @ADOcomm , @src OUT, @desc OUT 
                SELECT Error=CONVERT(varbinary(4),@OLEreturn), Source=@src, Description=@desc 
                RETURN 
       END 
IF @Verbose=1 Print Space(len(@StatusStr)) + 'done.' 
  
--SET the PageSize property 
IF @Verbose=1 
       BEGIN 
       Set @StatusStr = 'Set ''PageSize'' property…' 
       Print @StatusStr 
       END 
IF (@PageSize IS NOT null) -- If PageSize is SET then SET the value 
BEGIN 
       EXEC @OLEreturn = sp_OAMethod @ADOcommprop, 'Item', @ADOcommpropVal out, 'Page Size' 
       IF @OLEreturn <> 0 
              BEGIN -- Return OLE error 
                       EXEC sp_OAGetErrorInfo @ADOcommprop , @src OUT, @desc OUT 
                       SELECT Error=CONVERT(varbinary(4),@OLEreturn), Source=@src, Description=@desc 
                       RETURN 
              END 
       EXEC @OLEreturn = sp_OASETProperty @ADOcommpropVal, 'Value','1000' 
       IF @OLEreturn <> 0 
              BEGIN -- Return OLE error 
                       EXEC sp_OAGetErrorInfo @ADOcommpropVal , @src OUT, @desc OUT 
                       SELECT Error=CONVERT(varbinary(4),@OLEreturn), Source=@src, Description=@desc 
                       RETURN 
              END 
END 
IF @Verbose=1 Print Space(len(@StatusStr)) + 'done.' 
  
--SET the SearchScope property to ADS_SCOPE_SUBTREE to search the entire subtree 
IF @Verbose=1 
       BEGIN 
       Set @StatusStr = 'Set ''SearchScope'' property…' 
       Print @StatusStr 
       END 
BEGIN 
       EXEC @OLEreturn = sp_OAMethod @ADOcommprop, 'Item', @ADOcommpropVal out, 'SearchScope' 
       IF @OLEreturn <> 0 
              BEGIN -- Return OLE error 
                       EXEC sp_OAGetErrorInfo @ADOcommprop , @src OUT, @desc OUT 
                       SELECT Error=CONVERT(varbinary(4),@OLEreturn), Source=@src, Description=@desc 
                       RETURN 
              END 
       EXEC @OLEreturn = sp_OASETProperty @ADOcommpropVal, 'Value','2' --ADS_SCOPE_SUBTREE 
       IF @OLEreturn <> 0 
              BEGIN -- Return OLE error 
                       EXEC sp_OAGetErrorInfo @ADOcommpropVal , @src OUT, @desc OUT 
                       SELECT Error=CONVERT(varbinary(4),@OLEreturn), Source=@src, Description=@desc 
                RETURN 
       END 
END 
IF @Verbose=1 Print Space(len(@StatusStr)) + 'done.' 
  
--SET the Asynchronous property to True 
IF @Verbose=1 
       BEGIN 
       Set @StatusStr = 'Set ''Asynchronous'' property…' 
       Print @StatusStr 
       END 
BEGIN 
       EXEC @OLEreturn = sp_OAMethod @ADOcommprop, 'Item', @ADOcommpropVal out, 'Asynchronous' 
       IF @OLEreturn <> 0 
              BEGIN -- Return OLE error 
                       EXEC sp_OAGetErrorInfo @ADOcommprop , @src OUT, @desc OUT 
                       SELECT Error=CONVERT(varbinary(4),@OLEreturn), Source=@src, Description=@desc 
                       RETURN 
              END 
       EXEC @OLEreturn = sp_OASETProperty @ADOcommpropVal, 'Value',True 
       IF @OLEreturn <> 0 
              BEGIN -- Return OLE error 
                       EXEC sp_OAGetErrorInfo @ADOcommpropVal , @src OUT, @desc OUT 
                       SELECT Error=CONVERT(varbinary(4),@OLEreturn), Source=@src, Description=@desc 
                       RETURN 
       END 
END 
IF @Verbose=1 Print Space(len(@StatusStr)) + 'done.' 
  
--Create the ADO Recordset to hold the results of the LDAP query 
IF @Verbose=1 
       BEGIN 
       Set @StatusStr = 'Create the temporary ADO recordset for query output…' 
       Print @StatusStr 
       END 
EXEC @OLEreturn = sp_OACreate 'ADODB.RecordSET',@ADOrs out 
IF @OLEreturn <> 0 
       BEGIN -- Return OLE error 
                EXEC sp_OAGetErrorInfo @ADOrs , @src OUT, @desc OUT 
                SELECT Error=CONVERT(varbinary(4),@OLEreturn), Source=@src, Description=@desc 
                RETURN 
       END 
IF @Verbose=1 Print Space(len(@StatusStr)) + 'done.' 
  
--Pass the LDAP query to the ADO command object 
IF @Verbose=1 
       BEGIN 
       Set @StatusStr = 'Input the LDAP query…' 
       Print @StatusStr 
       END 
EXEC @OLEreturn = sp_OASETProperty @ADOcomm, 'CommandText', @LDAP_Query
IF @OLEreturn <> 0 
       BEGIN -- Return OLE error 
                EXEC sp_OAGetErrorInfo @ADOcomm , @src OUT, @desc OUT 
                SELECT Error=CONVERT(varbinary(4),@OLEreturn), Source=@src, Description=@desc 
                RETURN 
       END 
IF @Verbose=1 Print Space(len(@StatusStr)) + 'done.' 
  
--Run the LDAP query and output the results to the ADO Recordset 
IF @Verbose=1 
       BEGIN 
       Set @StatusStr = 'Execute the LDAP query…' 
       Print @StatusStr 
       END 
Exec @OLEreturn = sp_OAMethod @ADOcomm, 'Execute' ,@ADOrs OUT 
IF @OLEreturn <> 0 
       BEGIN -- Return OLE error 
                print 'Error in Execute clause of SP_OAMethod' 
                EXEC sp_OAGetErrorInfo @ADOcomm , @src OUT, @desc OUT 
                SELECT Error=CONVERT(varbinary(4),@OLEreturn), Source=@src, Description=@desc 
                RETURN 
       END

--Return the rows found 
IF @Verbose=1 
       BEGIN 
       Set @StatusStr = 'Retrieve the LDAP query results…' 
       Print @StatusStr 
       END

DECLARE @pwdlastset varchar(8)

EXEC @OLEreturn = sp_OAMethod @ADOrs, 'getrows'
       IF @OLEreturn <> 0 
       BEGIN -- Return OLE error 
                Print 'Error in Getstring of getproperty' 
                EXEC sp_OAGetErrorInfo @ADOrs , @src OUT, @desc OUT 
                SELECT Error=CONVERT(varbinary(4),@OLEreturn), Source=@src, Description=@desc 
                RETURN 
       END

IF @Verbose=1 Print Space(len(@StatusStr)) + 'done.'
GO
GRANT EXECUTE ON  [dbo].[uspQueryAD] TO [CSAdmin]
GO
