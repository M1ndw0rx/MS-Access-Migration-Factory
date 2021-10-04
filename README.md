# MS-Access-Migration-Factory
This project contains the source code for the MS Access Migration Factory. This program is able to analyze an MS Access database file and transfers it to a Dynamics 365 / Dataverse environment. 

# History

There are a lot of companies and organizations which used MS Access as a low-code / no-code platform in the past for different reasons: 
- It is very easy to create an application which supports the processes and can be a nice digital overlay to already existing systems
- The IT of the organization is not able to provider proper applications so employees look for other tools (shadow IT)

Meanwhile there are sometimes hundreds of these little applications which became a major problem. There is no governance for the data, nobody has transparency about the nature of the data, who is accessing it, what is really stored, how it is backed up or maintained, etc. Even worse, what happens if the database is not working anymore? The employees which created the application may already be somewhere else or has left the company. 

With Dynamics 365 and Power Platform, there is a real alternative to this. This low-code / no-code Platform addresses all the aspects and problems described above and even more. That's why it totally makes sense to migrate all these MS Access applications to this platform.    

But this undertaking can very tedius since there are a lot of artefacts to create like tables, fields, relationships, forms etc….not speaking about the data itself. Fortunately MS Access and Dynamics 365 have these elements in common so that a direct transfer is possible. Although there is one piece within MS Access which cannot be implemented in Dynamics 365 and that is VBScript or Macros. If an Access DB uses this, there must be a manual implementation for this in the platform.  

# Implementation

With this little tool it is possible to
- Open a MS Access database
- Analyze the database and select the tables to migrate
- Connect to a Dynamics 365 / Dataverse environment (online or on premise)
- Choose a solution or create one
- Choose a publisher or create one
- Choose to create an app for the migration
- Choose to publish all this 
- Migrate all the stuff
- Undo the migration afterwards if necessary 

![image](https://user-images.githubusercontent.com/91728344/135850463-7382f625-dc8d-49c7-8518-9174bd93cedd.png)

