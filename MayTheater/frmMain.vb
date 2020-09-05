Imports MySql.Data.MySqlClient
Public Class frmMain
    Public wel As String = ""

    Dim strcon As New MySqlConnection
    Dim cmd As New MySqlCommand
    Dim da As New MySqlDataAdapter
    Dim ds As New DataSet

    'Dim frm1 As New Form1
    Dim frmSell As New FormExample
    Dim formRegister As New FrmMember

    Dim whiteSell As New System.Drawing.Bitmap(My.Resources.movie)
    Dim graySell As New System.Drawing.Bitmap(My.Resources.movie_tickets)

    Dim whiteMember As New System.Drawing.Bitmap(My.Resources.membership__1_)
    Dim grayMember As New System.Drawing.Bitmap(My.Resources.membership)

    Sub Handle4Button33(a, b, c)
        a.ForeColor = Color.White
        b.ForeColor = Color.FromArgb(111, 111, 111)
        c.ForeColor = Color.FromArgb(111, 111, 111)
    End Sub
    Sub Handle4Button(a, b, c)
        a.BackColor = Color.FromArgb(101, 240, 182)
        b.BackColor = Color.White
        c.BackColor = Color.White
    End Sub

    Private Sub ShowForm(a)
        a.TopLevel = False
        Me.Panel2.Controls.Add(a)
        a.Width = Panel2.Width
        a.Height = Panel2.Height
        a.Show()
        a.BringToFront()
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ShowForm(frmSell)
        frmSell.user = wel

        Handle4Button33(Label2, Label3, Label5)
        Handle4Button(Label2, Label3, Label5)

    End Sub

    Private Sub lbExit_Click(sender As Object, e As EventArgs) Handles lbExit.Click
        Dim a As MsgBoxResult
        Dim frmLogin As New FrmLogin
        a = MsgBox("ທ່ານຕ້ອງການອອກຈາກລະບົບບໍ່?", MsgBoxStyle.Question + MsgBoxStyle.YesNo)
        If a = MsgBoxResult.Yes Then
            frmLogin.Show()
            Me.Hide()
        End If
    End Sub


    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Handle4Button33(Label2, Label3, Label5)
        Handle4Button(Label2, Label3, Label5)
        frmSell.Hideform()
        ShowForm(frmSell)
        Me.Hide()

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Handle4Button33(Label3, Label2, Label5)
        Handle4Button(Label3, Label2, Label5)
        ShowForm(formRegister)
    End Sub

    'Private Sub Label4_Click(sender As Object, e As EventArgs)
    '    Handle4Button33(Label3, Label2, Label5)
    '    Handle4Button(Label3, Label2, Label5)
    '    ShowForm(frmSell)
    'End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Handle4Button33(Label5, Label3, Label2)
        Handle4Button(Label5, Label3, Label2)
        Dim a As MsgBoxResult
        Dim frmLogin As New FrmLogin
        a = MsgBox("ທ່ານຕ້ອງການອອກຈາກລະບົບບໍ່?", MsgBoxStyle.Question + MsgBoxStyle.YesNo)
        If a = MsgBoxResult.Yes Then
            frmLogin.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub pbMenu_Click(sender As Object, e As EventArgs) Handles pbMenu.Click

        If Label1.Text = "ເມນູ" Then
            Panel4.Width = 400
            Panel2.Enabled = False
            Label1.Text = "ກັບຄືນ"
        Else
            Panel4.Width = 0
            Panel2.Enabled = True
            Label1.Text = "ເມນູ"
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        ShowForm(formRegister)
    End Sub
End Class