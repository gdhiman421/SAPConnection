# SAPConnection
SAP Connection from C#

**Pre-Requisites:**
1)	SAP GUI Installation:
    A Software program that provides an interface to connect to SAP Systems. 

2)	User Credentials:
    A valid user ID and Password are required to connect to SAP System and will be provided by SAP System Admin.

3)	Authorization:
    The user ID must have required authorization to perform the required tasks in SAP. This can be done by assigning the user to the appropriate roles and authorization.
    Below is the overview of required authorization:
    
    i. Auth Object = S_RFC,
	      RFC_TYPE = FUNC,
	      ACTVT = 16,
	      RFC_NAME = RFCPING,
		               RFC_FUNCTION_SEARCH,
		               RFC_METADATA_GET,
		               BAPI_USER_GET_DETAIL,
		               BAPI_USER_CREATE1,
		               BAPI_USER_CHANGE,
		               BAPI_USER_ACTGROUPS_ASSIGN,
		               BAPI_USER_LOCK,
		               BAPI_USER_UNLOCK,
		               BAPI_USER_DELETE

    ii. Auth Object = S_USER_GRP,
	        ACTVT= 01, 02, 03, 05, 06 (Create, Change, Display, Lock & Delete User)


    iii. Auth Object = S_USER_SAS,
	        ACTVT = 22 (Enter, Include, Assign),
	        ACT_GROUP = * (Access to All Roles for User Assignment)
 

4)	SAP Server Information:
    The SAP system ID, client number & application server information need to be provided in order to connect to the required SAP System.

5)	SAP .NET Connector:
    The SAP .NET Connector (NCo) library needs to be installed on the C# Application tool (Visual Studio) in order to enable SAP connectivity through C#.

