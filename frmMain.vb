Option Strict On
Option Explicit On

Imports System.Data
Imports System.Windows.Forms

''' <summary>
''' Main form. Provides add / update / delete / search of customer records
''' that are persisted in the Customers.mdb Access database.
''' </summary>
Public Class frmMain

    ' Holds all customers; a DataView gives us live filtering for the search box.
    Private _table As DataTable
    Private _view As DataView

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            CustomerDb.EnsureDatabase()
            LoadGrid()
        Catch ex As Exception
            MessageBox.Show("Startup error: " & ex.Message, "Customer Information",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Loads (or reloads) all customers into the grid.
    Private Sub LoadGrid()
        _table = CustomerDb.GetCustomers()
        _view = _table.DefaultView
        If Not _view Is Nothing Then
            dgvCustomers.DataSource = _view
            FormatGrid()
        End If
        lblStatus.Text = $"{_table.Rows.Count} customer(s)"
    End Sub

    Private Sub FormatGrid()
        If dgvCustomers.Columns.Contains("CustomerID") Then
            dgvCustomers.Columns("CustomerID").HeaderText = "ID"
            dgvCustomers.Columns("CustomerID").FillWeight = 30
        End If
        If dgvCustomers.Columns.Contains("FullName") Then dgvCustomers.Columns("FullName").HeaderText = "Full Name"
        If dgvCustomers.Columns.Contains("CreatedOn") Then dgvCustomers.Columns("CreatedOn").HeaderText = "Created On"
    End Sub

    ' --- Buttons -----------------------------------------------------------

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If Not ValidateInput() Then Return
        Try
            CustomerDb.AddCustomer(txtFullName.Text.Trim(), txtEmail.Text.Trim(),
                                   txtPhone.Text.Trim(), txtAddress.Text.Trim(), txtCity.Text.Trim())
            LoadGrid()
            ClearForm()
            lblStatus.Text = "Customer added."
        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim id As Integer = SelectedCustomerId()
        If id <= 0 Then
            MessageBox.Show("Select a customer in the grid first.", "Update",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        If Not ValidateInput() Then Return
        Try
            CustomerDb.UpdateCustomer(id, txtFullName.Text.Trim(), txtEmail.Text.Trim(),
                                      txtPhone.Text.Trim(), txtAddress.Text.Trim(), txtCity.Text.Trim())
            LoadGrid()
            lblStatus.Text = "Customer updated."
        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim id As Integer = SelectedCustomerId()
        If id <= 0 Then
            MessageBox.Show("Select a customer in the grid first.", "Delete",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        If MessageBox.Show("Delete the selected customer?", "Confirm",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return
        Try
            CustomerDb.DeleteCustomer(id)
            LoadGrid()
            ClearForm()
            lblStatus.Text = "Customer deleted."
        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearForm()
        dgvCustomers.ClearSelection()
    End Sub

    ' --- Grid + search -----------------------------------------------------

    Private Sub dgvCustomers_SelectionChanged(sender As Object, e As EventArgs) Handles dgvCustomers.SelectionChanged
        If dgvCustomers.CurrentRow Is Nothing Then Return
        Dim row As DataRowView = TryCast(dgvCustomers.CurrentRow.DataBoundItem, DataRowView)
        If row Is Nothing Then Return
        txtFullName.Text = FieldToString(row("FullName"))
        txtEmail.Text = FieldToString(row("Email"))
        txtPhone.Text = FieldToString(row("Phone"))
        txtAddress.Text = FieldToString(row("Address"))
        txtCity.Text = FieldToString(row("City"))
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _view Is Nothing Then Return
        Dim term As String = txtSearch.Text.Replace("'", "''").Trim()
        If term.Length = 0 Then
            _view.RowFilter = String.Empty
        Else
            _view.RowFilter =
                $"FullName LIKE '%{term}%' OR Email LIKE '%{term}%' OR " &
                $"Phone LIKE '%{term}%' OR City LIKE '%{term}%'"
        End If
        lblStatus.Text = $"{_view.Count} match(es)"
    End Sub

    ' --- Helpers -----------------------------------------------------------

    Private Function SelectedCustomerId() As Integer
        If dgvCustomers.CurrentRow Is Nothing Then Return 0
        Dim row As DataRowView = TryCast(dgvCustomers.CurrentRow.DataBoundItem, DataRowView)
        If row Is Nothing OrElse IsDBNull(row("CustomerID")) Then Return 0
        Return Convert.ToInt32(row("CustomerID"))
    End Function

    Private Function ValidateInput() As Boolean
        If txtFullName.Text.Trim().Length = 0 Then
            MessageBox.Show("Full Name is required.", "Validation",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtFullName.Focus()
            Return False
        End If
        Return True
    End Function

    Private Sub ClearForm()
        txtFullName.Clear()
        txtEmail.Clear()
        txtPhone.Clear()
        txtAddress.Clear()
        txtCity.Clear()
        txtFullName.Focus()
    End Sub

    Private Shared Function FieldToString(value As Object) As String
        Return If(value Is Nothing OrElse IsDBNull(value), String.Empty, value.ToString())
    End Function

    Private Sub ShowError(ex As Exception)
        MessageBox.Show("Error: " & ex.Message, "Customer Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Error)
        lblStatus.Text = "Error."
    End Sub

End Class
