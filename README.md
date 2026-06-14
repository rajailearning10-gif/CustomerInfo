# Customer Information Manager (VB.NET + Access MDB)

A Windows Forms desktop app written in VB.NET that stores customer records in a
Microsoft Access `.mdb` database. Supports Add, Update, Delete, and live Search.

## What it does

- On first launch it **auto-creates** `Customers.mdb` (next to the .exe) and a
  `Customers` table — no manual database setup needed.
- Form fields: Full Name, Email, Phone, Address, City.
- Grid lists all customers; click a row to load it into the form.
- Search box filters the grid by name, email, phone, or city.
- Records persist in the `.mdb` file between runs.

## Database schema

Table `Customers`:

| Column     | Type        | Notes                  |
|------------|-------------|------------------------|
| CustomerID | COUNTER     | Primary key (autonum)  |
| FullName   | TEXT(100)   | Required               |
| Email      | TEXT(150)   |                        |
| Phone      | TEXT(40)    |                        |
| Address    | TEXT(200)   |                        |
| City       | TEXT(80)    |                        |
| CreatedOn  | DATETIME    | Set on insert          |

## Requirements

- Windows with **.NET Framework 4.7.2** (or adjust `TargetFrameworkVersion`).
- **Jet 4.0 OLEDB provider** for `.mdb` files. This ships with Windows but is
  **32-bit only**, which is why the project builds as **x86**. Keep the
  Platform target as x86, otherwise you'll get a "provider not registered" error.
- To build: Visual Studio 2019/2022 (Community is fine) **or** MSBuild.

## How to build & run

### Visual Studio
1. Open `CustomerInfo.sln`.
2. Press **F5** (or Build > Build Solution, then run).

### Command line (MSBuild)
```bat
msbuild CustomerInfo.sln /p:Configuration=Release /p:Platform="x86"
bin\Release\CustomerInfo.exe
```

The `Customers.mdb` file is created automatically in the same folder as the
executable on first run.

## Notes

- The database creation uses ADOX via late binding (`CreateObject("ADOX.Catalog")`),
  so no COM reference needs to be added manually.
- All SQL uses parameterized `OleDbCommand` queries to avoid SQL injection.
- If you prefer a newer `.accdb` database or x64, install the
  **Microsoft Access Database Engine 2016 Redistributable** and switch the
  provider to `Microsoft.ACE.OLEDB.12.0` in `CustomerDb.vb`.

## Project files

```
CustomerInfo/
├─ CustomerInfo.sln
├─ CustomerInfo.vbproj
├─ App.config              ' database file name setting
├─ CustomerDb.vb           ' data-access layer (create DB + CRUD)
├─ frmMain.vb              ' form logic
├─ frmMain.Designer.vb     ' form layout
└─ My Project/
   ├─ Application.myapp
   ├─ Application.Designer.vb
   └─ AssemblyInfo.vb
```
