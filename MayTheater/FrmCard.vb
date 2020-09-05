Imports MySql.Data.MySqlClient
Public Class FrmCard
    Dim strcon As New MySqlConnection
    'Dim strcon1 As String = "server=localhost;user id= root;password=may555;database=maytheater;Character Set=utf8;"
    Dim da As New MySqlDataAdapter
    Dim ds As New DataSet
    Dim cmd As New MySqlCommand

    Dim id As Integer

    Dim frmMember As New FormMemInfo

    Sub ShowData()
        da = New MySqlDataAdapter("select cardName, cardDiscount, cardMaxTime, cardLife, cardPrice from card ORDER BY cardDiscount ASC", strcon)
        da.Fill(ds, "card")
        ds.Clear()
        da.Fill(ds, "card")

        DataGridView1.DataSource = ds.Tables("card")

        With DataGridView1
            .Columns(0).HeaderText = "ຊື່ບັດ"
            .Columns(1).HeaderText = "ສ່ວນຫຼຸດ"
            .Columns(2).HeaderText = "ໃຊ້ໄດ້ຫຼາຍສຸດ"
            .Columns(3).HeaderText = "ອາຍຸການນຳໃຊ້"
            .Columns(4).HeaderText = "ຄ່າສະໝັກ"

            .Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

            Dim j As Integer
            For j = 0 To ds.Tables("card").Rows.Count - 1
                'DataGridView1.Rows(j).Selected = False
                If DataGridView1.Rows(j).Cells(0).Value.ToString = txtCardName.Text Then
                    DataGridView1.Rows(j).Selected = True
                    DataGridView1.FirstDisplayedScrollingRowIndex = j

                End If
            Next

        End With
        DataGridView1.Refresh()
    End Sub

    Sub ShowForm(a)
        a.TopLevel = False
        Me.Panel2.Controls.Add(a)
        a.Width = Panel2.Width
        a.Height = Panel2.Height
        a.Show()
        a.BringToFront()
    End Sub

    Private Sub CheckNumber(e As KeyPressEventArgs)
        'ເຮັດໃຫ້ປ່ຽນເປັນ Keyboard ພາສາອັງກິດ
        If Not e.KeyChar = ChrW(Keys.Back) And Not e.KeyChar = Chr(13) Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
                MsgBox("ປ້ອນສະເພາະຕົວເລກ")
            End If
        End If
    End Sub

    Sub newFocus(a As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then
            a.Focus()
        End If
    End Sub

    Sub ChangeLanguage()
        For Each Language As InputLanguage In InputLanguage.InstalledInputLanguages
            If Language.Culture.TwoLetterISOLanguageName.Contains("en") Then
                InputLanguage.CurrentInputLanguage = Language
            End If
        Next
    End Sub

    Sub Handle2Button(a, b)
        a.BackColor = Color.FromArgb(101, 240, 182)
        a.ForeColor = Color.White
        b.BackColor = Color.White
        b.ForeColor = Color.FromArgb(111, 111, 111)
    End Sub

    Sub Clear()
        txtCardName.Text = ""
        txtDiscount.Text = ""
        txtMaxTime.Text = ""
        txtLifetime.Text = ""
        txtPrice.Text = ""
    End Sub

    Private Sub FrmCard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Handle2Button(lbCard, lbMember)

        strcon = New MySqlConnection(DBModule.strcon)
        'strcon = New MySqlConnection(strcon1)
        strcon.Open()

        DataGridView1.RowTemplate.Height = 120

        btnSave.FlatAppearance.BorderSize = 0
        btnEdit.FlatAppearance.BorderSize = 0
        btnDelete.FlatAppearance.BorderSize = 0
        btnClear.FlatAppearance.BorderSize = 0



        ShowData()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        da = New MySqlDataAdapter("select cardID, cardName, cardDiscount, cardMaxTime, cardLife, cardPrice from card where cardName = '" & DataGridView1.CurrentRow.Cells(0).Value.ToString & "'", strcon)
        da.Fill(ds, "show")
        ds.Clear()
        da.Fill(ds, "show")

        'Int32.TryParse(ds.Tables("show").Rows(0)(0), id)
        txtCardName.Text = ds.Tables("show").Rows(0)(1)
        txtDiscount.Text = ds.Tables("show").Rows(0)(2)
        txtMaxTime.Text = ds.Tables("show").Rows(0)(3)
        txtLifetime.Text = ds.Tables("show").Rows(0)(4)
        txtPrice.Text = ds.Tables("show").Rows(0)(5)


        ShowData()

    End Sub


    Private Sub lbCard_Click(sender As Object, e As EventArgs) Handles lbCard.Click
        Handle2Button(lbCard, lbMember)

        Me.Panel2.Controls.Remove(frmMember)
        Panel2.Show()
    End Sub

    Private Sub txtDiscount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDiscount.KeyPress
        CheckNumber(e)
        newFocus(txtMaxTime, e)
    End Sub


    Private Sub txtMaxTime_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMaxTime.KeyPress
        CheckNumber(e)
        newFocus(txtLifetime, e)
    End Sub

    Private Sub txtLifetime_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLifetime.KeyPress
        CheckNumber(e)
        newFocus(txtPrice, e)
    End Sub

    Private Sub txtPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPrice.KeyPress
        CheckNumber(e)
    End Sub

    Private Sub txtCardName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCardName.KeyPress
        newFocus(txtDiscount, e)
    End Sub


    Private Sub txtDiscount_GotFocus(sender As Object, e As EventArgs) Handles txtDiscount.GotFocus
        ChangeLanguage()
    End Sub

    Private Sub txtMaxTime_GotFocus(sender As Object, e As EventArgs) Handles txtMaxTime.GotFocus
        ChangeLanguage()
    End Sub

    Private Sub txtLifetime_GotFocus(sender As Object, e As EventArgs) Handles txtLifetime.GotFocus
        ChangeLanguage()
    End Sub

    Private Sub txtPrice_GotFocus(sender As Object, e As EventArgs) Handles txtPrice.GotFocus
        ChangeLanguage()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If txtCardName.Text.ToLower = DataGridView1.Rows(i).Cells(0).Value.ToString.ToLower Then
                MsgBox("ຊຶ່ບັດຊ້ຳກັບແຖວອື່ນ")
                Return
            End If
        Next

        If txtCardName.Text = "" Or txtDiscount.Text = "" Or txtMaxTime.Text = "" Or txtLifetime.Text = "" Or txtPrice.Text = "" Then
            MsgBox("ຕື່ມຂໍ້ມູນໃຫ້ຄົບຖ້ວນ")
            Return
        End If

        Dim name As String
        name = txtCardName.Text
        Dim discount, maxtime, lifetime, price As Integer
        Int32.TryParse(txtDiscount.Text, discount)
        Int32.TryParse(txtMaxTime.Text, maxtime)
        Int32.TryParse(txtLifetime.Text, lifetime)
        Int32.TryParse(txtPrice.Text, price)

        cmd = New MySqlCommand("insert into card (cardName, cardDiscount, cardMaxTime, cardLife, cardPrice) values (@name, @discount, @maxtime, @lifetime, @price)", strcon)
        cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = name
        cmd.Parameters.Add("@discount", MySqlDbType.Int32).Value = discount
        cmd.Parameters.Add("@maxtime", MySqlDbType.Int32).Value = maxtime
        cmd.Parameters.Add("@lifetime", MySqlDbType.Int32).Value = lifetime
        cmd.Parameters.Add("@price", MySqlDbType.Int32).Value = price

        cmd.ExecuteNonQuery()
        ShowData()
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If txtCardName.Text = "" Or txtDiscount.Text = "" Or txtMaxTime.Text = "" Or txtLifetime.Text = "" Or txtPrice.Text = "" Then
            MsgBox("ຕື່ມຂໍ້ມູນໃຫ້ຄົບຖ້ວນ")
            Return
        End If

        Dim name As String
        name = txtCardName.Text
        Dim discount, maxtime, lifetime, price As Integer
        Int32.TryParse(txtDiscount.Text, discount)
        Int32.TryParse(txtMaxTime.Text, maxtime)
        Int32.TryParse(txtLifetime.Text, lifetime)
        Int32.TryParse(txtPrice.Text, price)

        cmd = New MySqlCommand("update card set cardName = @name, cardDiscount = @discount, cardMaxTime = @maxTime, cardLife = @lifetime, cardPrice = @price where cardID = @id", strcon)
        cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = name
        cmd.Parameters.Add("@discount", MySqlDbType.Int32).Value = discount
        cmd.Parameters.Add("@maxtime", MySqlDbType.Int32).Value = maxtime
        cmd.Parameters.Add("@lifetime", MySqlDbType.Int32).Value = lifetime
        cmd.Parameters.Add("@price", MySqlDbType.Int32).Value = price
        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id


        cmd.ExecuteNonQuery()
        ShowData()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        cmd = New MySqlCommand("delete from card where cardID = @id", strcon)
        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id
        cmd.ExecuteNonQuery()

        ShowData()
        Clear()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Clear()
    End Sub

    Private Sub lbMember_Click(sender As Object, e As EventArgs) Handles lbMember.Click
        Handle2Button(lbMember, lbCard)
        ShowForm(frmMember)
    End Sub

End Class