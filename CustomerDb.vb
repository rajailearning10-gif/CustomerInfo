Option Strict On
Option Explicit On

Imports System.Configuration
Imports System.Data.OleDb
Imports System.IO

''' <summary>
''' Data-access layer for the customer information stored in a Microsoft Access
''' (.mdb) database. Uses the Jet OLEDB 4.0 provider. The database and its
''' Customers table are created automatically on first run via ADOX (late bound,
''' so no COM reference is required).
''' </summary>
Public Module CustomerDb

    ' Full path to the .mdb file (sits next to the executable).
    Private ReadOnly DbPath As String =
        Path.Combine(Application.StartupPath,
                     If(ConfigurationManager.AppSettings("DatabaseFile"), "Customers.mdb"))

    ' Connection string for the Jet 4.0 provider (works with classic .mdb files).
    Private ReadOnly ConnString As String =
        "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DbPath & ";"

    ''' <summary>
    ''' Ensures the .mdb file and the Customers table exist. Safe to call on
    ''' every application start.
    ''' </summary>
    Public Sub EnsureDatabase()
        If Not File.Exists(DbPath) Then
            ' Late-bound ADOX creation lives in MdbCreator.vb (Option Strict Off).
            MdbCreator.CreateMdb(ConnString)
        End If
        EnsureCustomersTable()
    End Sub

    ' Creates the Customers table if it is not already present.
    Private Sub EnsureCustomersTable()
        Using conn As New OleDbConnection(ConnString)
            conn.Open()

            Dim schema As DataTable =
                conn.GetSchema("Tables", New String() {Nothing, Nothing, "Customers", "TABLE"})

            If schema.Rows.Count = 0 Then
                Const sql As String =
                    "CREATE TABLE Customers (" &
                    " CustomerID COUNTER PRIMARY KEY," &
                    " FullName   TEXT(100)," &
                    " Email      TEXT(150)," &
                    " Phone      TEXT(40)," &
                    " Address    TEXT(200)," &
                    " City       TEXT(80)," &
                    " CreatedOn  DATETIME" &
                    ")"
                Using cmd As New OleDbCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                End Using
            End If
        End Using
    End Sub

    ''' <summary>Returns all customers as a DataTable for binding to a grid.</summary>
    Public Function GetCustomers() As DataTable
        Dim dt As New DataTable()
        Using conn As New OleDbConnection(ConnString)
            Const sql As String =
                "SELECT CustomerID, FullName, Email, Phone, Address, City, CreatedOn " &
                "FROM Customers ORDER BY CustomerID"
            Using adapter As New OleDbDataAdapter(sql, conn)
                adapter.Fill(dt)
            End Using
        End Using
        Return dt
    End Function

    ''' <summary>Inserts a new customer and returns the new CustomerID.</summary>
    Public Function AddCustomer(fullName As String, email As String, phone As String,
                                address As String, city As String) As Integer
        Using conn As New OleDbConnection(ConnString)
            conn.Open()

            Const sql As String =
                "INSERT INTO Customers (FullName, Email, Phone, Address, City, CreatedOn) " &
                "VALUES (?, ?, ?, ?, ?, ?)"
            Using cmd As New OleDbCommand(sql, conn)
                AddCustomerParams(cmd, fullName, email, phone, address, city)
                cmd.Parameters.Add(New OleDbParameter("@CreatedOn", OleDbType.Date) With {.Value = DateTime.Now})
                cmd.ExecuteNonQuery()
            End Using

            ' Retrieve the auto-generated identity value.
            Using idCmd As New OleDbCommand("SELECT @@IDENTITY", conn)
                Return Convert.ToInt32(idCmd.ExecuteScalar())
            End Using
        End Using
    End Function

    ''' <summary>Updates an existing customer. Returns rows affected.</summary>
    Public Function UpdateCustomer(customerId As Integer, fullName As String, email As String,
                                   phone As String, address As String, city As String) As Integer
        Using conn As New OleDbConnection(ConnString)
            conn.Open()
            Const sql As String =
                "UPDATE Customers SET FullName = ?, Email = ?, Phone = ?, " &
                "Address = ?, City = ? WHERE CustomerID = ?"
            Using cmd As New OleDbCommand(sql, conn)
                AddCustomerParams(cmd, fullName, email, phone, address, city)
                cmd.Parameters.Add(New OleDbParameter("@CustomerID", OleDbType.Integer) With {.Value = customerId})
                Return cmd.ExecuteNonQuery()
            End Using
        End Using
    End Function

    ''' <summary>Deletes a customer by id. Returns rows affected.</summary>
    Public Function DeleteCustomer(customerId As Integer) As Integer
        Using conn As New OleDbConnection(ConnString)
            conn.Open()
            Using cmd As New OleDbCommand("DELETE FROM Customers WHERE CustomerID = ?", conn)
                cmd.Parameters.Add(New OleDbParameter("@CustomerID", OleDbType.Integer) With {.Value = customerId})
                Return cmd.ExecuteNonQuery()
            End Using
        End Using
    End Function

    ' OleDb uses positional parameters; order must match the SQL placeholders.
    ' Types are declared explicitly: AddWithValue lets the provider guess the
    ' OLE DB type, which causes "Data type mismatch in criteria expression"
    ' errors with the Jet provider. Declaring OleDbType avoids that.
    Private Sub AddCustomerParams(cmd As OleDbCommand, fullName As String, email As String,
                                  phone As String, address As String, city As String)
        AddText(cmd, "@FullName", fullName, 100)
        AddText(cmd, "@Email", email, 150)
        AddText(cmd, "@Phone", phone, 40)
        AddText(cmd, "@Address", address, 200)
        AddText(cmd, "@City", city, 80)
    End Sub

    ' Adds a typed text parameter, sending NULL when the value is empty.
    Private Sub AddText(cmd As OleDbCommand, name As String, value As String, size As Integer)
        Dim p As New OleDbParameter(name, OleDbType.VarWChar, size)
        p.Value = If(String.IsNullOrEmpty(value), CObj(DBNull.Value), value)
        cmd.Parameters.Add(p)
    End Sub

End Module
