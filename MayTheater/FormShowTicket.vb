Imports MySql.Data.MySqlClient
Public Class FormShowTicket
    Dim strcon As New MySqlConnection
    Dim da As New MySqlDataAdapter
    Dim ds As New DataSet
    Dim cmd As New MySqlCommand


    Dim ticket As Integer = 0

    Public user As String = ""

    Public arr1 As New ArrayList

    Public honeyprice As Integer = 0
    Public normalprice As Integer = 0

    Public honey As Integer = 0
    Public normal As Integer = 0
    Public honeyamount As Integer = 0
    Public normalamount As Integer = 0

    Public total As Integer = 0
    Public discount As Integer = 0
    Public percent As Integer = 0

    Public proID As String = ""
    Public movieID As String = ""

    Public member As String = ""

    Public arrHoney As New ArrayList
    Public arrNormal As New ArrayList

    Dim checkCard As Integer = 0

    Dim billID As String = ""
    Private Sub FormShowTicket_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        strcon = New MySqlConnection(DBModule.strcon)
        strcon.Open()

        btnYes.FlatAppearance.BorderSize = 0

        Timer1.Enabled = True



        panel.Width = Panel1.Width / 2 - 40
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage

        Button3.FlatAppearance.BorderSize = 0
        Button2.FlatAppearance.BorderSize = 0

        btnYes.Left = panel2.Width / 2 - btnYes.Width / 2
        Button3.FlatAppearance.BorderSize = 0
        Button1.FlatAppearance.BorderSize = 0

        Label5.Text = honey
        Label6.Text = Format(honeyamount, "###,###") + " ກີບ"

        Label7.Text = Format(normalamount, "###,###") + " ກີບ"
        Label8.Text = normal

        If honey < 1 Then
            Panel4.Visible = False
        End If
        If normal < 1 Then
            Panel5.Visible = False
        End If

        lbTotal.Text = Format(total, "###,###") + " ກີບ"
        lbNeed.Text = Format(total - percent, "###,###") + " ກີບ"

        da = New MySqlDataAdapter("select pic from movie join programe on movie.movieID = programe.movieID where programe.proID = '" & proID & "'", strcon)
        da.Fill(ds, "pic")
        ds.Clear()
        da.Fill(ds, "pic")
        Dim picbyte() As Byte = ds.Tables("pic").Rows(0)(0)
        Dim ms As New System.IO.MemoryStream(picbyte)
        PictureBox3.Image = Image.FromStream(ms)
        PictureBox3.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub

    Private Sub txtMoney_KeyPress(sender As Object, e As KeyPressEventArgs)

    End Sub

    Private Sub btnYes_Click(sender As Object, e As EventArgs) Handles btnYes.Click

        Dim frmTicket As New FormTicket
        frmTicket.billID = billID
        frmTicket.Show()

    End Sub

    Private Sub txtMoney_GotFocus(sender As Object, e As EventArgs)
        For Each Language As InputLanguage In InputLanguage.InstalledInputLanguages
            If Language.Culture.TwoLetterISOLanguageName.Contains("en") Then
                InputLanguage.CurrentInputLanguage = Language
            End If
        Next
    End Sub


    Private Sub Button3_Click_1(sender As Object, e As EventArgs)
        'Add()
        'Dim tickets As Integer = honey + normal

        Dim frm2 As New Form2
        frm2.ticket = honey + normal
        frm2.ShowDialog(Me)
        Me.percent = frm2.discount
        If percent = 0 Then
            lbDiscount.Text = 0
        ElseIf percent > 0 Then
            Me.lbDiscount.Text = Format(percent, "###,###") + " ກີບ"
        End If

        Me.lbNeed.Text = Format(total - percent, "###,###") + " ກີບ"
        Me.arr1 = frm2.arr


    End Sub

    Sub calculate()

        If lbChange.Text.Length > 0 Then
            Return
        End If

        If txtMoney.Text = "" Then
            Return
        End If

        Dim money As Integer = txtMoney.Text

        Dim change As Integer
        change = money - (total - percent)
        If change < 0 Then
            MsgBox("ຮັບເງິນມາບໍ່ພໍ")
        ElseIf change >= 0 Then
            If change = 0 Then
                lbChange.Text = "0"
            ElseIf change > 0 Then
                lbChange.Text = Format(change, "###,###") + " ກີບ"
            End If


            Dim time As String = ""
            Dim date1 As String = ""
            date1 = System.DateTime.Now.ToString("yyyy-MM-dd")
            time = TimeOfDay.ToString("hh:mm:ss tt")

            cmd = New MySqlCommand("Insert into bill (billAmount, billDiscount, billDate, time, userID) values (@billAmount, @billDiscount, @billDate, @billTime, @userID)", strcon)
            cmd.Parameters.Add("@billAmount", MySqlDbType.Int32).Value = total
            cmd.Parameters.Add("@billDiscount", MySqlDbType.Int32).Value = percent
            cmd.Parameters.Add("@billDate", MySqlDbType.Date).Value = date1
            cmd.Parameters.Add("@billTime", MySqlDbType.VarChar).Value = time
            cmd.Parameters.Add("@userID", MySqlDbType.VarChar).Value = user
            cmd.ExecuteNonQuery()



            da = New MySqlDataAdapter("select billID from bill where billDate='" & date1 & "' and time = '" & time & "'", strcon)
            da.Fill(ds, "bill")
            ds.Clear()
            da.Fill(ds, "bill")
            billID = ds.Tables("bill").Rows(0)(0).ToString
            For i As Integer = 0 To arrHoney.Count - 1

                cmd = New MySqlCommand("Insert into selldetail (billID, proID, typeID, seatID, sellPrice) values ('" & billID & "', '" & proID & "', '2', '" & arrHoney(i) & "', '" & honeyprice & "')", strcon)

                cmd.ExecuteNonQuery()

            Next

            For i As Integer = 0 To arrNormal.Count - 1
                cmd = New MySqlCommand("Insert into selldetail (billID, proID, typeID, seatID, sellPrice) values ('" & billID & "', '" & proID & "', '1', '" & arrNormal(i) & "', '" & normalprice & "')", strcon)
                cmd.ExecuteNonQuery()
            Next
            btnYes.Enabled = True

            If arr1.Count < 0 Then
                Return
            End If

            If arr1.Count > 0 Then

                For i As Integer = 0 To arr1.Count - 1
                    cmd = New MySqlCommand("Insert into memberUse(billID, memberID, date ) values ('" & billID & "', '" & arr1(i).ToString & "', '" & date1 & "')", strcon)
                    cmd.ExecuteNonQuery()
                Next
            End If
        End If

        Button1.Enabled = False
        Button3.Enabled = False
        Button2.Enabled = False
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub txtMoney_KeyPress_1(sender As Object, e As KeyPressEventArgs)

    End Sub

    Private Sub pbSell_Click(sender As Object, e As EventArgs) Handles pbSell.Click

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label13.Text = System.DateTime.Now.ToString("dd-MMM-yy  hh:mm:ss")
    End Sub


    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        calculate()
    End Sub

    Private Sub txtMoney_KeyPress_2(sender As Object, e As KeyPressEventArgs) Handles txtMoney.KeyPress
        If e.KeyChar = Chr(13) Then
            calculate()
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim frm2 As New Form2
        frm2.ticket = honey + normal
        frm2.ShowDialog(Me)
        Me.percent = frm2.discount
        If percent = 0 Then
            lbDiscount.Text = 0
        ElseIf percent > 0 Then
            Me.lbDiscount.Text = Format(percent, "###,###") + " ກີບ"
        End If

        Me.lbNeed.Text = Format(total - percent, "###,###") + " ກີບ"
        Me.arr1 = frm2.arr

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim frmMain As New frmMain
        frmMain.wel = user
        frmMain.Show()
        Me.ParentForm.ParentForm.ParentForm.Hide()
    End Sub

    'select movie.movieName, movie.movieLang, programe.proTime, programe.proRoom, programe.proDate, concat(seat.letter,seat.number) as seatNO from selldetail join bill on selldetail.billID = bill.billID join programe on programe.proID = selldetail.proID join movie on programe.movieID = movie.movieID join seat on selldetail.seatID = seat.seatID where bill.billID = '128'
End Class