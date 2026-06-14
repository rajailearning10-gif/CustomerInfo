Option Strict Off
Option Explicit On

Imports System

''' <summary>
''' Creates a new, empty Access .mdb file using ADOX (Microsoft ADO Ext.) via
''' late binding. Late binding requires Option Strict Off, so this single piece
''' of functionality is isolated in its own file while the rest of the project
''' keeps Option Strict On.
''' </summary>
Friend Module MdbCreator

    Public Sub CreateMdb(connString As String)
        Dim catalog As Object = Nothing
        Try
            catalog = CreateObject("ADOX.Catalog")
            catalog.Create(connString)
        Catch ex As Exception
            Throw New ApplicationException(
                "Could not create the Access database. Make sure the Microsoft " &
                "Access Database Engine / Jet 4.0 provider is installed." &
                Environment.NewLine & ex.Message, ex)
        Finally
            If catalog IsNot Nothing Then
                Runtime.InteropServices.Marshal.ReleaseComObject(catalog)
            End If
        End Try
    End Sub

End Module
