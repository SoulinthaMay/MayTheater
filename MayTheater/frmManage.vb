Imports MySql.Data.MySqlClient
Public Class frmManage
    Dim strcon As New MySqlConnection
    Dim da As New MySqlDataAdapter
    Dim ds As New DataSet
    Dim cmd As New MySqlCommand


    Dim path As String

    Dim frmEmployee As New FrmEmployee
    Dim frmMovie As New FrmMovie
    Dim frmCard As New FrmCard
    Dim frm1 As New Form1
    Dim frmReport As New FormReport
    Dim ActiveButton As Color = Color.FromArgb(153, 51, 255)
    Dim BaseButton As Color = Color.FromArgb(66, 66, 66)

    Public wel As String = ""



    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        strcon = New MySqlConnection(DBModule.strcon)
        strcon.Open()

        pnHome.Show()
        Label4.Text = "Welcome " + wel


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

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        If Label1.Text = "ເມນູ" Then
            pnMenu.Width = 393

            pnHome.Enabled = False
            Label1.Text = "ກັບຄືນ"
        Else
            pnMenu.Width = 0

            pnHome.Enabled = True
            Label1.Text = "ເມນູ"
        End If
    End Sub

    Private Sub Handle4Button(a, b, c, d, e)
        a.BackColor = Color.FromArgb(101, 240, 182)
        b.BackColor = Color.White
        c.BackColor = Color.White
        d.BackColor = Color.White
        e.BackColor = Color.White
        a.ForeColor = Color.White
        b.ForeColor = Color.FromArgb(111, 111, 111)
        c.ForeColor = Color.FromArgb(111, 111, 111)
        d.ForeColor = Color.FromArgb(111, 111, 111)
        e.ForeColor = Color.FromArgb(111, 111, 111)
    End Sub


    Private Sub ShowForm(a)
        a.TopLevel = False
        Me.pnHome.Controls.Add(a)
        a.Width = pnHome.Width
        a.Height = pnHome.Height
        a.Show()
        a.BringToFront()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles l1.Click
        Handle4Button(l1, l2, l3, l4, l5)
        ShowForm(frmEmployee)

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles l2.Click
        Handle4Button(l2, l1, l3, l4, l5)
        ShowForm(frmMovie)



    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles l4.Click
        Handle4Button(l4, l1, l2, l3, l5)

        ShowForm(frmCard)

    End Sub

    Private Sub lbRound_Click(sender As Object, e As EventArgs) Handles l3.Click
        Handle4Button(l3, l4, l1, l2, l5)
        ShowForm(frm1)


        frm1.DateTimePicker1.Value = System.DateTime.Now.ToString("dd-MMM-yy")
        frm1.cbxName.Text = "ເລືອກຊື່ໜັງ"
        frm1.cbxLang.Text = "ເລືອກພາສາ"

        frm1.cbxName.Items.Clear()
        Dim i As Integer
        da = New MySqlDataAdapter("select distinct movieName from movie where movieStatus <> 'ອອກຈາກໂຮງສາຍ'  and movieDate <= '" & System.DateTime.Now.ToString("yyyy-MM-dd") & "' order by movieName ASC", strcon)
        da.Fill(ds, "name")
        ds.Clear()
        da.Fill(ds, "name")

        For i = 0 To ds.Tables("name").Rows.Count - 1

            frm1.cbxName.Items.Add(ds.Tables("name").Rows(i)(0))
        Next


    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles l5.Click
        Handle4Button(l5, l1, l3, l4, l2)
        ShowForm(frmReport)
    End Sub
End Class