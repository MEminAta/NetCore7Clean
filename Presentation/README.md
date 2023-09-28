# .Net Core 7.0 Clean Architecture

Project [Github Address](https://github.com/)

# Entity Framework Commands
(All commands will be written from inside the Presentation layer)

Add Migration:

```bash
dotnet ef migrations add <NAME> --project ../Application
```

<br>

Update Database:

```bash
dotnet ef database update
```

<br>

Entity = Database de tutulan
<br>
Dto = Entity kırpılırsa
<br>
Model = Entity veya Dto'ya yeni colon eklenirse

<br>

Example `Example`