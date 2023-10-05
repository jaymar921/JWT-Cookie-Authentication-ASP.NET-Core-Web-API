# JWT Cookie Authentication ASP.NET Core Web API
A basic Authentication API with Json Web Token, practice program by [Jayharron Abejar](https://jayharronabejar.info)

### Run the app
If you are using Visual Studio code, just run the app directly in IIS Express

### Login
Since this is a simple app, we just hard coded the user data, let's login using Jay's credential. To test the API, use POSTMAN or any API testing tools
```
POST https://localhost:7075/Authentication
BODY
username="jay123"
password="jay456"
```
Once the request is authenticated, it will return a response. If you check the `Cookies` tab in Postman, there will be a cookie attached with the response.
```
RESPONSE BODY
message="Logged in successfully"
```

### Get All user
To test if the token is validated, I have created a route that will return the list of users registered in the APP. Note that you have to include the `Cookie` that was attached when you logged in with the API.
```
GET https://localhost:7075/User
```
If the token is authorized to retrieve the resource in the server, it will return a `json` response.
```json
[
    {
        "id": 1,
        "name": "Jayharron Mar Abejar",
        "email": "jay@email.com",
        "favoriteColor": "Blue",
        "username": "jay123"
    },
    {
        "id": 2,
        "name": "Pia Abellana",
        "email": "pia@email.com",
        "favoriteColor": "Red",
        "username": "pia123"
    },
    {
        "id": 3,
        "name": "Rey Vincent De los Reyes",
        "email": "rey@email.com",
        "favoriteColor": "Green",
        "username": "rey123"
    },
    {
        "id": 4,
        "name": "James Dylan Caramonte",
        "email": "james@email.com",
        "favoriteColor": "Red",
        "username": "james123"
    }
]
```

### Logout
Calling the post method in the route `Authentication/Logout` will logout the user, if you will check the `Cookies` tab in the Postman, it will be empty.
```
POST https://localhost:7075/Authentication/Logout
```