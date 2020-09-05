Imports MySql.Data.MySqlClient
Public Class FrmMember
    Dim da As New MySqlDataAdapter
    Dim ds As New DataSet
    Dim strcon As New MySqlConnection
    Dim cmd As New MySqlCommand


    Dim price As Integer = 0
    Dim id As String = ""
    Dim year As Integer = 0
    Private Sub FrmMember_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        strcon = New MySqlConnection(DBModule.strcon)
        strcon.Open()

        da = New MySqlDataAdapter("select cardName from card", strcon)
        da.Fill(ds, "card")
        ds.Clear()
        da.Fill(ds, "card")

        Button1.FlatAppearance.BorderSize = 0
        Button2.FlatAppearance.BorderSize = 0

        ComboBox1.Items.Clear()
        For i As Integer = 0 To ds.Tables("card").Rows.Count - 1
            ComboBox1.Items.Add(ds.Tables("card").Rows(i)(0).ToString)
        Next

        Label5.Left = Panel9.Width / 2 - (Label5.Width / 2)
        Label15.Left = Panel7.Width / 2 - (Label15.Width / 2)
    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs)

    End Sub

    Private Sub TextBox4_GotFocus(sender As Object, e As EventArgs)
        For Each Language As InputLanguage In InputLanguage.InstalledInputLanguages
            If Language.Culture.TwoLetterISOLanguageName.Contains("en") Then
                InputLanguage.CurrentInputLanguage = Language
            End If
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If ComboBox1.Text = "ເລືອກປະເພດບັດ" Then
            MsgBox("ກະລຸນາເລືອກປະເພດບັດ")
            Return
        End If

        If txtName.Text = "" Or txtSurname.Text = "" Or txtTel.Text = "" Then
            MsgBox("ກະລຸນາຕື່ມຂໍ້ມູນໃຫ້ຄົບ")
            Return
        End If

        da = New MySqlDataAdapter("select memberName, memberSurname from member join card on member.cardID = card.cardID where card.cardName = '" & ComboBox1.Text & "'", strcon)
        da.Fill(ds, "memm")
        ds.Clear()
        da.Fill(ds, "memm")
        For i As Integer = 0 To ds.Tables("memm").Rows.Count - 1
            If txtName.Text.ToLower = ds.Tables("memm").Rows(i)(0).ToString.ToLower Or txtSurname.Text.ToLower = ds.Tables("memm").Rows(i)(1).ToString.ToLower Then
                MsgBox("ຂໍ້ມູນຊ້ຳກັນ")
                Return
            End If
        Next

        da = New MySqlDataAdapter("select cardID, cardPrice, cardLife from card where cardName = '" & ComboBox1.SelectedItem.ToString & "'", strcon)
        da.Fill(ds, "price")
        ds.Clear()
        da.Fill(ds, "price")

        id = ds.Tables("price").Rows(0)(0)
        price = ds.Tables("price").Rows(0)(1)
        year = ds.Tables("price").Rows(0)(2)
        Panel7.Height = Panel9.Height

        lbMoney.Text = Format(price, "###,###") + " ກີບ"
        TextBox4.Text = ""
        lbChange.Text = ""


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        txtName.Text = ""
        txtSurname.Text = ""
        txtTel.Text = ""
        LabelTotal.Text = ""
        lbChange.Text = ""
        TextBox4.Text = ""
        ComboBox1.Text = "ເລືອກປະເພດບັດ"
    End Sub

    Private Sub txtTel_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTel.KeyPress
        If Not e.KeyChar = ChrW(Keys.Back) And Not e.KeyChar = Chr(13) Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
                MsgBox("ປ້ອນສະເພາະຕົວເລກ")
            End If
        End If
    End Sub

    Private Sub txtTel_GotFocus(sender As Object, e As EventArgs) Handles txtTel.GotFocus
        For Each Language As InputLanguage In InputLanguage.InstalledInputLanguages
            If Language.Culture.TwoLetterISOLanguageName.Contains("en") Then
                InputLanguage.CurrentInputLanguage = Language
            End If
        Next
    End Sub

    Private Sub txtName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtName.KeyPress
        If e.KeyChar = Chr(13) Then
            txtSurname.Focus()
        End If
    End Sub

    Private Sub txtSurname_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSurname.KeyPress
        If e.KeyChar = Chr(13) Then
            txtTel.Focus()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Panel7.Height = 0
        txtName.Text = ""
        txtSurname.Text = ""
        txtTel.Text = ""
        ComboBox1.Text = "ເລືອກປະເພດບັດ"
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Panel7.Height = 0
        txtName.Text = ""
        txtSurname.Text = ""
        txtTel.Text = ""
        ComboBox1.Text = "ເລືອກປະເພດບັດ"
    End Sub

    Private Sub TextBox4_KeyPress_1(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress

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
                Dim bar As Integer = 0
                If ds.Tables("last").Rows.Count = 0 Then
                    bar = 10000000
                ElseIf ds.Tables("last").Rows.Count = 1 Then

                    Dim may As Integer = ds.Tables("last").Rows(0)(0)
                    bar = may + 1
                End If

                cmd = New MySqlCommand("Insert into member (memberName, memberSurname, memberTel, date, expiredate, cardID, barcode) values (@name, @surname, @tel,@date, @expiredate, @id, @barcode)", strcon)
                cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = txtName.Text.ToLower()
                cmd.Parameters.Add("@surname", MySqlDbType.VarChar).Value = txtSurname.Text.ToLower()
                cmd.Parameters.Add("@tel", MySqlDbType.VarChar).Value = txtTel.Text
                cmd.Parameters.Add("@date", MySqlDbType.Date).Value = System.DateTime.Now.ToString("yyyy-MM-dd")
                cmd.Parameters.Add("@expiredate", MySqlDbType.Date).Value = System.DateTime.Now.AddYears(year).ToString("yyyy-MM-dd")
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id
                cmd.Parameters.Add("@barcode", MySqlDbType.VarChar).Value = bar
                cmd.ExecuteNonQuery()
                Button5.Enabled = True

            End If

        End If
    End Sub
End Class