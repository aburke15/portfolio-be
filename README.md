# Portfolio Backend

Just the backend API and services that support my [portfolio](https://www.aburke.tech) page.

## Installation

You can clone this [repo](https://github.com/aburke15/portfolio-be.git) on your local machine. Add your values to the appsettings.json.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "GitHub": {
    "User": "Your_GitHub_User",
    "PAT": "Your_GitHub_Personal_Access_Token"
  },
  "AzureStorage": {
    "ConnectionString": "Your_Azure_Storage_Connection_String"
  }
}
```

Then run command the following in the terminal.
```bash
dotnet run
```

## API Usage

Once connected you can hit the following endpoints.

* **URL**

  _api/github/repos_

* **Method:**
  
  _The request type_

  `GET`
* **Success Response:**

    _Returns a list of your github repos_
* 
    * **Code:** 200 <br />
      **Content:** `[{repos}]`