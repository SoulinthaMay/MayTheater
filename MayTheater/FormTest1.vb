Imports MySql.Data.MySqlClient
Public Class FormTest1
    Dim da As New MySqlDataAdapter
    Dim ds As New DataSet
    Dim strcon As New MySqlConnection
    Dim cmd As New MySqlCommand


    Public price As Integer = 0
    Public name1, surname, tel, id As String

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    End Sub

    Private Sub FormTest1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        strcon = New MySqlConnection(DBModule.strcon)
        'Panel2.Left = Panel1.Width / 2 - Panel2.Width / 2
        'Panel2.Top = Panel1.Height / 2 - Panel2.Height / 2
        Label5.Width = Panel1.Width / 2 - (Label5.Width / 2)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'D
        'frmTest.lbMoney.Text = Format(price, "###,###") + " ກີບ"
        'frmTest.TopLevel = False
        'Me.Panel9.Controls.Add(frmTest)
        'frmTest.price = price
        'frmTest.name1 = txtName.Text.ToLower
        'frmTest.surname = txtSurname.Text.ToLower()
        'frmTest.tel = txtTel.Text
        'frmTest.id = id

        'frmTest.Width = Panel9.Width
        'frmTest.Height = Panel9.Height
        'frmTest.Show()
        'frmTest.BringToFront()
    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        If Not e.KeyChar = ChrW(Keys.Back) And Not e.KeyChar = Chr(13) Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
                MsgBox("ປ້ອນສະເພາະຕົວເລກ")
            End If
        End If

        If e.KeyChar = Chr(13) Then
            If lbChange.Text.Length > 0 Then

                Return
            End If


            Dim money As Integer = TextBox4.Text

            Dim change As Integer
            change = money - price
            If change < 0 Then
                MsgBox("ຮັບເງິນມາບໍ່ພໍ")
            ElseIf change >= 0 Then
                If change = 0 Then
                    lbChange.Text = "0"
                ElseIf change > 0 Then
                    lbChange.Text = Format(change, "###,###") + " ກີບ"
                End If


                Dim rnd As New Random()
                Dim randomNumber As Integer = rnd.Next(100000, 10000000)

                'Label1.Text = randomNumber

                'Dim writer As New ZXing.BarcodeWriter
                'writer.Format = ZXing.BarcodeFormat.CODE_128
                'PictureBox1.Image = writer.Write(randomNumber)
                da = New MySqlDataAdapter("select barcode from member order by memberID DESC limit 1", strcon)
                da.Fill(ds, "last")
                ds.Clear()
                da.Fill(ds, "last")
                Dim may As Integer = ds.Tables("last").Rows(0)(0)

                'cmd = New MySqlCommand("Insert into member (memberName, memberSurname, memberTel, date, cardID, status, barcode) values (@name, @surname, @tel,@date, @id, @status, @barcode)", strcon)
                'cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = txtName.Text.ToLower()
                'cmd.Parameters.Add("@surname", MySqlDbType.VarChar).Value = txtSurname.Text.ToLower()
                'cmd.Parameters.Add("@tel", MySqlDbType.VarChar).Value = txtTel.Text
                'cmd.Parameters.Add("@date", MySqlDbType.VarChar).Value = System.DateTime.Now.ToString("dd-MMM-yy")
                'cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id
                'cmd.Parameters.Add("@status", MySqlDbType.VarChar).Value = "valid"
                'cmd.Parameters.Add("@barcode", MySqlDbType.VarChar).Value = may + 1
                'cmd.ExecuteNonQuery()
                'MsgBox("ບັນທຶກຂໍິມູນສຳເລັດ")
            End If

        End If
    End Sub
End Class