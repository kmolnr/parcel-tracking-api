# Readme

## Development

- **IDE:** VS2022
- **Target framework:** .NET 6.0
- **DB:** EF Core Code First
- **Authentication:** JWT token

## Generate database

1. **Package Manager Console / Default project:** `ParcelTracking.EFCore`
2. **Package Manager Console / Commands:**

    - `add-migration InitMigration`
    - `update-database`

**Start web app:** Change `ConnectionStrings` in `appsettings.json` file.

## Swagger

**Login:** `/api/user/authentication`

**Sample request body:**

```
{
  "name": "user1",
  "password": "user1",
  "token": ""
}
```

Click the `Authorize` button and set JWT token.

**Sample user-password pairs:**

- `user1` - `user1`
- `user2` - `user2`

**Sample `parcelNumber`:**

- `QWER0001` (`user1`)
- `QWER0002` (`user1`)
- `QWER0003` (`user2`)
- `QWER0004` (`user2`)
