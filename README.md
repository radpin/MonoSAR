# MonoSAR

Open source project for the management of search and rescue teams. 
Membership rosters, callout lists, training, and extensive API options for future work.
Hoping to make generic enough for any teams or similar orgs that want it.

Running on .Net Core 2.0, EF Core, MVC pattern. 

Notes:

All files are included *except* for AzureKeyVault.json (marked with .gitignore). We're using DI to load this file and retrieve database connection info from AzureKeyVault. In future releases it would be nice to make this a bit more generic. An example of our AzureKeyVault:

{
  "azureKeyVault": {
    "vault": "keyvault-ourcoolkeyvaultname",
    "clientId": "smbwV0nU3HAryPIiTq64",
    "clientSecret": "ftX5vrAJ7vyB0MvFOMDA82b9K5uqVHCeWdeGqgu8Cb3bhwVtkMDaLnbCT6g4k6Ln5N3s3qjwta2N1jY7fpV8NY0HvCBasTH1ukH"
  }
}

Feel free to contact me if you have any questions radpin [-at-] gmail [dotcom]. 
