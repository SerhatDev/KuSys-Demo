# KuSys-Demo


### **To Add Migration:**


  dotnet ef migrations add "Initial" --context KuSysDbContext --project ./KuSys.DataAccess/KuSys.DataAccess.csproj --startup-project ./KuSys.Api/KuSys.Api.csproj


### **To Apply Migrations:**  


  dotnet ef database update --context KuSysDbContext --project ./KuSys.DataAccess/KuSys.DataAccess.csproj --startup-project ./KuSys.Api/KuSys.Api.csproj


### **To Test Api as Admin;**  


  **1** - *Create new user (/api/Auth/Register)*  
  **2** - *Login as newly created user (/api/Auth/Login)*  
  **3** - *Give yourself Admin rights (/api/Auth/make-admin) ** For test purposes only*
  **4** - *Re-Login to get access token with Admin Rights* 


### **To Test Api as Student**;  


  **1** - *Create new user (/api/Auth/Register)*  
  **2** - *Login as newly created user (/api/Auth/Login)*
  **3** - *Give yourself Admin rights (/api/Auth/make-student)* ** For test purposes only **  
  **4** - *Re-Login to get access token with Student Rights*  


  OR  

  
  After you logged in as an admin, create new student (/api/student) to test system as student.
