Imports MySql.Data.MySqlClient
Public Class FormExample
    Dim pnButton As New FlowLayoutPanel

    Dim c As Control

    Dim strcon As New MySqlConnection
    Dim da As New MySqlDataAdapter
    Dim ds As New DataSet
    Dim cmd As New MySqlCommand
    Dim dtReader As MySqlDataReader

    Dim whatt As Integer = 0

    Dim frmSeat As New FrmSeat

    Dim todayDate As String = System.DateTime.Now.ToString("dd-MM-yyyy")
    Dim tomorrowDate As String = System.DateTime.Now.AddDays(1).ToString("dd-MM-yyyy")
    Dim tomorrowDate2 As String = System.DateTime.Now.AddDays(2).ToString("dd-MM-yyyy")
    Dim tomorrowDate3 As String = System.DateTime.Now.AddDays(3).ToString("dd-MM-yyyy")
    Dim tomorrowDate4 As String = System.DateTime.Now.AddDays(4).ToString("dd-MM-yyyy")

    Public user As String = ""

    Dim arrButton As New ArrayList

    Sub Handle5Button48(a, b, c, d, e)
        a.ForeColor = Color.White
        b.ForeColor = Color.FromArgb(111, 111, 111)
        c.ForeColor = Color.FromArgb(111, 111, 111)
        d.ForeColor = Color.FromArgb(111, 111, 111)
        e.ForeColor = Color.FromArgb(111, 111, 111)
        a.BackColor = Color.FromArgb(101, 240, 182)
        b.BackColor = Color.White
        c.BackColor = Color.White
        d.BackColor = Color.White
        e.BackColor = Color.White
    End Sub


    Private Sub FormExample_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        strcon = New MySqlConnection(DBModule.strcon)
        strcon.Open()

        Label1.Text = todayDate
        Label2.Text = tomorrowDate
        Label3.Text = tomorrowDate2
        Label4.Text = tomorrowDate3
        Label5.Text = tomorrowDate4

        Button1.FlatAppearance.BorderSize = 0
        Button2.FlatAppearance.BorderSize = 0

        Handle5Button48(Label1, Label2, Label3, Label4, Label5)
        pn1.AutoScroll = True

        Button2.FlatAppearance.BorderColor = Color.Black
        Button2.FlatAppearance.BorderSize = 0
        Button2.FlatStyle = FlatStyle.Flat

        ShowData()

    End Sub

    Public Sub Hideform()

        Dim frmMain As New frmMain
        frmMain.wel = user
        frmMain.Show()

    End Sub



    Sub ShowData()

        pn1.Controls.Clear()

        Dim str As String = ""

        If Label1.ForeColor = Color.White Then
            str = Label1.Text
        ElseIf Label2.ForeColor = Color.White Then
            str = Label2.Text
        ElseIf Label3.ForeColor = Color.White Then
            str = Label3.Text
        ElseIf Label4.ForeColor = Color.White Then
            str = Label4.Text
        ElseIf Label5.ForeColor = Color.White Then
            str = Label5.Text
        End If

        da = New MySqlDataAdapter("select DISTINCT movieLang, pic, movieName from movie inner join programe on movie.movieID = programe.movieID where proDate = '" & str & "' and movieStatus <> 'ອອກຈາກໂຮງສາຍ' ORDER BY movieName ASC", strcon)
        da.Fill(ds, "name")
        ds.Clear()
        da.Fill(ds, "name")

        For i As Integer = 0 To ds.Tables("name").Rows.Count - 1
            Dim subPanel As New Panel
            subPanel.BackColor = Color.White
            subPanel.Height = 260
            subPanel.Width = Me.Width

            Dim pic1 As New PictureBox
            pic1.Width = 260
            pic1.Height = subPanel.Height

            pic1.SizeMode = PictureBoxSizeMode.StretchImage

            Dim picbyte() As Byte = ds.Tables("name").Rows(i)(1)
            Dim ms As New System.IO.MemoryStream(picbyte)
            pic1.Image = Image.FromStream(ms)

            subPanel.Controls.Add(pic1)

            Dim label As New Label
            label.Text = ds.Tables("name").Rows(i)(2)
            ''''''''''''''''''''''''''
            label.Width = subPanel.Width
            label.Height = 42
            label.Top = 40
            label.Left = 300
            label.Font = New Drawing.Font("Lucida Bright", 20, FontStyle.Bold)

            label.ForeColor = Color.FromArgb(111, 111, 111)

            subPanel.Controls.Add(label)


            Dim lang As New Label


            lang.Text = ds.Tables("name").Rows(i)(0)
            'lang.Width = 100
            'lang.Height = 42
            lang.AutoSize = True
            lang.Top = 90
            lang.Left = 300
            lang.Font = New Font(lang.Font.FontFamily, 14, FontStyle.Regular)
            lang.ForeColor = Color.FromArgb(111, 111, 111)
            lang.BorderStyle = BorderStyle.Fixed3D

            subPanel.Controls.Add(lang)


            pnButton = New FlowLayoutPanel
            pnButton.Width = subPanel.Width / 2
            pnButton.Height = subPanel.Height
            pnButton.Top = 130
            pnButton.Left = 300

            cmd = New MySqlCommand("select proTime from movie join programe on movie.movieID = programe.movieID where programe.movieID = (select movieID from movie where movieName = '" & ds.Tables("name").Rows(i)(2) & "' and movieLang = '" & lang.Text & "') and proDate = '" & str & "' ORDER BY proTime ASC", strcon)
            dtReader = cmd.ExecuteReader()
            Dim time As String = TimeOfDay.ToString("HH")
            Dim minute As String = TimeOfDay.ToString("mm")
            Dim timeNum As Double = time + (minute / 100)
            Dim timee As Integer = 0

            Dim minutee As Integer = 0
            While dtReader.Read()
                Dim button1 As New Button()

                button1.Text = dtReader("proTime")
                timee = button1.Text.Substring(0, 2)
                minutee = button1.Text.Substring(3)


                button1.Font = New Font(button1.Font.FontFamily, 18)
                button1.ForeColor = Color.FromArgb(101, 240, 182)
                button1.FlatStyle = FlatStyle.Flat
                button1.FlatAppearance.BorderSize = 2
                button1.FlatAppearance.BorderColor = Color.FromArgb(101, 240, 182)

                button1.Left = label.Left
                button1.Width = 140
                button1.Height = 50
                button1.Margin = New Padding(0, 15, 10, 0)
                pnButton.Controls.Add(button1)

                arrButton.Add(button1.Text)

                button1.Name = label.Text

                button1.Parent.Name = lang.Text



                If timeNum > (timee + (minutee / 100) + (0.3)) And str = System.DateTime.Now.ToString("dd-MM-yyyy") Then

                    button1.ForeColor = Color.FromArgb(111, 111, 111)
                    'button1.FlatAppearance.BorderSize = 2
                    button1.FlatAppearance.BorderColor = Color.FromArgb(111, 111, 111)
                    button1.FlatStyle = FlatStyle.Flat

                End If



                AddHandler button1.Click, AddressOf button1_Click


            End While
            dtReader.Close()

            subPanel.Controls.Add(pnButton)
            pn1.Controls.Add(subPanel)

            Me.Panel1.Visible = True
        Next

    End Sub

    Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs)

        If CType(sender, Button).ForeColor = Color.FromArgb(111, 111, 111) Then

        ElseIf CType(sender, Button).BackColor = Color.White Then
            'CType(sender, Button).BackColor = Color.FromArgb(153, 51, 255)
            lbTime.Text = CType(sender, Button).Text

            Dim name As String = ""
            Dim lang As String = ""
            Dim id As String = ""
            Dim movie As String = ""

            name = CType(sender, Button).Name
            lang = CType(sender, Button).Parent.Name
            da = New MySqlDataAdapter("select movieID from movie where movieName = '" & name & "' and movieLang = '" & lang & "'", strcon)
            da.Fill(ds, "movie")
            ds.Clear()
            da.Fill(ds, "movie")
            movie = ds.Tables("movie").Rows(0)(0)

            da = New MySqlDataAdapter("select proID from programe where programe.movieID = '" & movie & "' and proTime = '" & CType(sender, Button).Text & "'", strcon)
            da.Fill(ds, "id")
            ds.Clear()
            da.Fill(ds, "id")
            id = ds.Tables("id").Rows(0)(0)

            da = New MySqlDataAdapter("select count(seatID) from selldetail where proID = '" & id & "'", strcon)
            da.Fill(ds, "count")
            ds.Clear()
            da.Fill(ds, "count")
            Dim count As Integer = ds.Tables("count").Rows(0)(0)

            da = New MySqlDataAdapter("select count(seatID) from selldetail where proID = '" & id & "' and typeID = '2'", strcon)
            da.Fill(ds, "countHoney")
            ds.Clear()
            da.Fill(ds, "countHoney")
            Dim countHoney As Integer = ds.Tables("countHoney").Rows(0)(0)



            Dim left As Integer = 144 - count
            If left < 5 Then
                lbSeatLeft.ForeColor = Color.Red
            ElseIf left >= 5 Then
                lbSeatLeft.ForeColor = Color.FromArgb(111, 111, 111)
            End If
            'left 142
            'h 52
            'n 142-52
            'lbSeatLeft.Text = left
            whatt = left
            If left = 0 Then
                Button1.Enabled = False
            ElseIf left > 0 Then
                Button1.Enabled = True
            End If
            lbSeatLeft.Text = left
            lbName.Text = name
            lbHoney.Text = 54 - countHoney
            lbNormal.Text = left - (54 - countHoney)

            frmSeat.TopLevel = False
            frmSeat.proID = id
            frmSeat.movie = movie
            frmSeat.user = user

            pnTicket.Width = 500


        End If


    End Sub


    Private Sub Label2_Click(sender As Object, e As EventArgs)
        'Handle5Button48(Label2, Label1, Label3, Label4, Label5)
        'ShowData()
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs)
        'Handle5Button48(Label3, Label2, Label1, Label4, Label5)
        'ShowData()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs)
        'Handle5Button48(Label4, Label2, Label3, Label1, Label5)
        'ShowData()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs)
        'Handle5Button48(Label5, Label2, Label3, Label4, Label1)
        'ShowData()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs)
        'Handle5Button48(Label1, Label2, Label3, Label4, Label5)
        'ShowData()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click

        If NumericUpDown1.Value = 0 Or NumericUpDown1.Value > whatt Then
            MsgBox("ກະລຸນາປ້ອນຈຳນວນບ່ອນນັ່ງອີກຄັ້ງ")
        Else

            Me.Panel7.Controls.Clear()
            Me.Panel7.Controls.Add(frmSeat)
            frmSeat.BringToFront()
            frmSeat.Height = Me.Height
            frmSeat.Width = Me.Width
            frmSeat.seat = NumericUpDown1.Value
            frmSeat.Show()
        End If

    End Sub

    Private Sub NumericUpDown1_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then
            If NumericUpDown1.Value = 0 Or NumericUpDown1.Value > whatt Then
                MsgBox("ກະລຸນາປ້ອນຈຳນວນບ່ອນນັ່ງອີກຄັ້ງ")
            Else

                Me.Panel7.Controls.Clear()
                Me.Panel7.Controls.Add(frmSeat)
                frmSeat.BringToFront()
                frmSeat.Height = Me.Height
                frmSeat.Width = Me.Width
                frmSeat.seat = NumericUpDown1.Value


                frmSeat.Show()
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        pnTicket.Width = 0
    End Sub

    Private Sub Label1_Click_1(sender As Object, e As EventArgs) Handles Label1.Click
        Handle5Button48(Label1, Label2, Label3, Label4, Label5)
        ShowData()
    End Sub

    Private Sub Label2_Click_1(sender As Object, e As EventArgs) Handles Label2.Click
        Handle5Button48(Label2, Label1, Label3, Label4, Label5)
        ShowData()
    End Sub

    Private Sub Label3_Click_1(sender As Object, e As EventArgs) Handles Label3.Click
        Handle5Button48(Label3, Label2, Label1, Label4, Label5)
        ShowData()
    End Sub

    Private Sub Label4_Click_1(sender As Object, e As EventArgs) Handles Label4.Click
        Handle5Button48(Label4, Label2, Label3, Label1, Label5)
        ShowData()
    End Sub

    Private Sub Label5_Click_1(sender As Object, e As EventArgs) Handles Label5.Click
        Handle5Button48(Label5, Label2, Label3, Label4, Label1)
        ShowData()
    End Sub

End Class