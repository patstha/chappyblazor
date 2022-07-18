# Chappy Blazor

Welcome to Chappy Blazor! 
This is a work in progress. 

##Get Started

To get started, first set up your postgresql environment. 
I use ElephantSQL but any postgresql server running latest version should work. 
Set up your secrets.json file in the ChappyBlazor project as well as the DataAccessTests project. 
Your secrets.json file should look something like this. 

```json
{
  "ConnectionStrings": {
    "Default":  "Host=myhost;Database=mydatabase;User Id=myusername;Password=mypassword;"
  }
}
```

My DbUp connection string is not correct. 
For now, please run the scripts manually. 
On your database server, execute the sql files from the create database project.
[CreateDatabase\Scripts\0001-InitialCreation\0001-CreateMyPerson.sql](CreateDatabase/Scripts/0001-InitialCreation/0001-CreateMyPerson.sql)
[CreateDatabase\Scripts\0001-InitialCreation\0002-CreateMyProgram.sql](CreateDatabase/Scripts/0001-InitialCreation/0002-CreateMyProgram.sql)

