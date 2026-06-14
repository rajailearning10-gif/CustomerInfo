Option Strict On
Option Explicit On

Imports System.Windows.Forms
Imports System.Drawing

Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    Friend WithEvents lblTitle As Label
    Friend WithEvents lblFullName As Label
    Friend WithEvents lblEmail As Label
    Friend WithEvents lblPhone As Label
    Friend WithEvents lblAddress As Label
    Friend WithEvents lblCity As Label
    Friend WithEvents txtFullName As TextBox
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents txtPhone As TextBox
    Friend WithEvents txtAddress As TextBox
    Friend WithEvents txtCity As TextBox
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnUpdate As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnClear As Button
    Friend WithEvents dgvCustomers As DataGridView
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents lblSearch As Label
    Friend WithEvents statusStrip As StatusStrip
    Friend WithEvents lblStatus As ToolStripStatusLabel

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private Sub InitializeComponent()
        Me.lblTitle = New Label()
        Me.lblFullName = New Label()
        Me.lblEmail = New Label()
        Me.lblPhone = New Label()
        Me.lblAddress = New Label()
        Me.lblCity = New Label()
        Me.txtFullName = New TextBox()
        Me.txtEmail = New TextBox()
        Me.txtPhone = New TextBox()
        Me.txtAddress = New TextBox()
        Me.txtCity = New TextBox()
        Me.btnAdd = New Button()
        Me.btnUpdate = New Button()
        Me.btnDelete = New Button()
        Me.btnClear = New Button()
        Me.dgvCustomers = New DataGridView()
        Me.txtSearch = New TextBox()
        Me.lblSearch = New Label()
        Me.statusStrip = New StatusStrip()
        Me.lblStatus = New ToolStripStatusLabel()
        CType(Me.dgvCustomers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.statusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New Font("Segoe UI", 14.0!, FontStyle.Bold)
        Me.lblTitle.Location = New Point(20, 15)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Text = "Customer Information"
        '
        'lblFullName
        '
        Me.lblFullName.AutoSize = True
        Me.lblFullName.Location = New Point(23, 60)
        Me.lblFullName.Name = "lblFullName"
        Me.lblFullName.Text = "Full Name:"
        '
        'lblEmail
        '
        Me.lblEmail.AutoSize = True
        Me.lblEmail.Location = New Point(23, 92)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Text = "Email:"
        '
        'lblPhone
        '
        Me.lblPhone.AutoSize = True
        Me.lblPhone.Location = New Point(23, 124)
        Me.lblPhone.Name = "lblPhone"
        Me.lblPhone.Text = "Phone:"
        '
        'lblAddress
        '
        Me.lblAddress.AutoSize = True
        Me.lblAddress.Location = New Point(23, 156)
        Me.lblAddress.Name = "lblAddress"
        Me.lblAddress.Text = "Address:"
        '
        'lblCity
        '
        Me.lblCity.AutoSize = True
        Me.lblCity.Location = New Point(23, 188)
        Me.lblCity.Name = "lblCity"
        Me.lblCity.Text = "City:"
        '
        'txtFullName
        '
        Me.txtFullName.Location = New Point(110, 57)
        Me.txtFullName.Name = "txtFullName"
        Me.txtFullName.Size = New Size(250, 23)
        '
        'txtEmail
        '
        Me.txtEmail.Location = New Point(110, 89)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New Size(250, 23)
        '
        'txtPhone
        '
        Me.txtPhone.Location = New Point(110, 121)
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.Size = New Size(250, 23)
        '
        'txtAddress
        '
        Me.txtAddress.Location = New Point(110, 153)
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New Size(250, 23)
        '
        'txtCity
        '
        Me.txtCity.Location = New Point(110, 185)
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New Size(250, 23)
        '
        'btnAdd
        '
        Me.btnAdd.Location = New Point(110, 225)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New Size(75, 30)
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New Point(193, 225)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New Size(75, 30)
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New Point(276, 225)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New Size(75, 30)
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New Point(359, 225)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New Size(75, 30)
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Location = New Point(450, 60)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Text = "Search:"
        '
        'txtSearch
        '
        Me.txtSearch.Location = New Point(505, 57)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New Size(260, 23)
        '
        'dgvCustomers
        '
        Me.dgvCustomers.AllowUserToAddRows = False
        Me.dgvCustomers.AllowUserToDeleteRows = False
        Me.dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCustomers.Location = New Point(450, 92)
        Me.dgvCustomers.MultiSelect = False
        Me.dgvCustomers.Name = "dgvCustomers"
        Me.dgvCustomers.ReadOnly = True
        Me.dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Me.dgvCustomers.Size = New Size(315, 280)
        Me.dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        '
        'statusStrip
        '
        Me.statusStrip.Items.AddRange(New ToolStripItem() {Me.lblStatus})
        Me.statusStrip.Location = New Point(0, 388)
        Me.statusStrip.Name = "statusStrip"
        Me.statusStrip.Size = New Size(784, 22)
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Text = "Ready"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.ClientSize = New Size(784, 410)
        Me.Controls.Add(Me.statusStrip)
        Me.Controls.Add(Me.dgvCustomers)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.lblSearch)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.txtCity)
        Me.Controls.Add(Me.txtAddress)
        Me.Controls.Add(Me.txtPhone)
        Me.Controls.Add(Me.txtEmail)
        Me.Controls.Add(Me.txtFullName)
        Me.Controls.Add(Me.lblCity)
        Me.Controls.Add(Me.lblAddress)
        Me.Controls.Add(Me.lblPhone)
        Me.Controls.Add(Me.lblEmail)
        Me.Controls.Add(Me.lblFullName)
        Me.Controls.Add(Me.lblTitle)
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Text = "Customer Information Manager"
        CType(Me.dgvCustomers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.statusStrip.ResumeLayout(False)
        Me.statusStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub
End Class
