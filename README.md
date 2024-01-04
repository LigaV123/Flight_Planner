# Flight Planner

This is an API project with CRUD operations.

### To Run
Activate the **FlightPlanner.sln** (it is located at FlightPlanner folder) solution file using Windows Visual Studio.
Press the **F5** button or click the **run/debug** button to launch the project(it should open the Swagger page in the browser, displaying CRUD operations).

### Description

There are three main sections located there:
- AdminApi
- Cleanup
- CustomerApi

#### AdminApi
There are three request methods which can be used only by using username: **codelex-admin** and password: **Password123**
In the Swagger page it is not possible to use username and password so I recomend to test them out in [postman API platform](https://www.postman.com/downloads/) or [insomnia API platform](https://insomnia.rest/download) by downloading one of them. By trying to use any of these three requests in the Swagger page, there will be 401 Unauthorized responses.

With PUT function new flight gets created: Try creating the below provided flight in json format. Entity Framework takes care of automated Id generation, there is no need to create one on your own.
By creating two identical flights there will be 409 Conflict response, and 400 Bad Request error if AirportCodes are the same in airport from and airport to, if any value is null or empty or if arrival time is greater than departure time (unfortunately we can’t return to the past).

```
{
      "from": { "country": "Russia", 
	 					"city": "Moscow", 
	 					"airport": "DME" },

      "to": { "country": "Sweden", 
					"city": "Stockholm", 
					"airport": "ARN" },

      "carrier": "Ryanair",
      "departureTime": "2019-01-01 00:00",
      "arrivalTime": "2019-01-02 00:00"
    }
```

With GET function it is possible to get the flight by its Id. Providing the wrong Id will result in a 404 Not Found error.

With DELETE function flight can be deleted by its Id.

#### Cleanup
This section has only one function - POST, it clears all of the data from the database. And it works on the Swagger page as well.
After clearing the database or deleting the flight by Id as administrator, I suggest creating and adding a new flight as administrator through insomnia or postman so it is possible to test CustomerApi requests.

#### CustomerApi
These functions work just fine in the Swagger page.
The first GET function will display all the airports which match the inserted AirportCode, Country or City name. Providing the wrong AirportCode, Country or City name will result in a 404 Not Found error, for null or empty value it will display 400 Bad Request response.

The POST function will make a list of searched flights by searching with AirportCode and departure date. Providing wrong, empty or null values there will be 400 Bad Request error.

```
{
  "from": "DME",
  "to": "ARN",
  "departureDate": "2019-01-01"
}
```

The second GET function will find flight by its Id number. Providing the wrong Id will result in a 404 Not Found error.
