# Sitecore.Gridify.Media
Switches the default media blobs storage (MSSQL database) to the mongoDB GridFS one for the master and web Sitecore databases. 

# Prerequisites:

Install [Sitecore.AssemblyBinding-1.0.zip](https://github.com/zigor/Sitecore-AssemblyBinding/releases/tag/v1.0) enabling assemblyBinding definitions in Sitecore include config files.

# Post Configuration Steps:

Add a new connectionString to the [WEBROOT]\App_Config\connectionStrings.config file specifying the blobs database , e.g.
```

  &lt;add name="media" connectionString="mongodb://localhost/blobs" /&gt;
  
```
