Imports MySql.Data.MySqlClient

Public Class FrmLogin
    Dim strcon As New MySqlConnection
    Dim da As New MySqlDataAdapter
    Dim ds As New DataSet
    Dim cmd As New MySqlCommand

    Private Sub FrmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        strcon = New MySqlConnection(DBModule.strcon)
        strcon.Open()

        Button1.FlatAppearance.BorderSize = 0


        PictureBox2.Left = Panel1.Width / 2 - PictureBox2.Width / 2
        PictureBox2.Top = Panel1.Top + 170
        Label1.Left = Panel1.Width / 2 - Label1.Width / 2
        Label4.Left = Panel1.Width / 2 - Label4.Width / 2



        txtUsername.Left = Panel2.Width / 2 - txtUsername.Width / 2
        txtPassword.Left = Panel2.Width / 2 - txtPassword.Width / 2

        Label2.Left = txtUsername.Left
        Label3.Left = txtPassword.Left

        CheckBox1.Left = txtPassword.Left

        Button1.Left = Panel2.Width / 2 - Button1.Width / 2
        'PictureBox1.Left = Panel2.Width / 2 - PictureBox1.Width / 2
        PictureBox1.Top = Panel2.Top + 40
        txtPassword.UseSystemPasswordChar = True
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) 

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = False Then
            txtPassword.UseSystemPasswordChar = True
        ElseIf CheckBox1.Checked Then
            txtPassword.UseSystemPasswordChar = False
        End If
    End Sub

    Sub Login()
        'da = New MySqlDataAdapter("select * from users", strcon)
        da = New MySqlDataAdapter("select * from users where userName = '" & txtUsername.Text & "' and password = '" & txtPassword.Text & "'", strcon)
        da.Fill(ds, "login")
        ds.Clear()
        da.Fill(ds, "login")

        For i As Integer = 0 To ds.Tables("login").Rows.Count - 1

            If txtUsername.Text = ds.Tables("login").Rows(i)(1).ToString And txtPassword.Text = ds.Tables("login").Rows(i)(2).ToString Then

                If ds.Tables("login").Rows(i)(3).ToString = "Admin" Then
                    Dim frmManage As New frmManage
                    frmManage.wel = ds.Tables("login").Rows(i)(1)
                    frmManage.Show()

                    Me.Hide()
                    Return

                ElseIf ds.Tables("login").Rows(i)(3).ToString = "User" Then

                    Dim frmMain As New frmMain

                    frmMain.wel = ds.Tables("login").Rows(i)(0)
                    frmMain.Show()

                    Me.Hide()
                    Return

                End If

            End If
        Next
        'Dim i As Integer = ds.Tables("login").Rows.Count - 1
        'If i > -1 Then
        '    MsgBox("yay")
        'Else
        '    MsgBox("ຂໍ້ມູນບໍ່ຖືກຕ້ອງ")
        'End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Login()
    End Sub

    Private Sub txtUsername_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtUsername.KeyPress
        If e.KeyChar = Chr(13) Then
            txtPassword.Focus()
        End If
    End Sub

    Private Sub txtPassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPassword.KeyPress
        If e.KeyChar = Chr(13) Then
            Login()
        End If
    End Sub


    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Dim a As MsgBoxResult
        a = MsgBox("ທ່ານຕ້ອງການອອກບໍ່?", MsgBoxStyle.Question + MsgBoxStyle.YesNo)
        If a = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim a As MsgBoxResult
        a = MsgBox("ທ່ານຕ້ອງການອອກບໍ່?", MsgBoxStyle.Question + MsgBoxStyle.YesNo)
        If a = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub
End Class