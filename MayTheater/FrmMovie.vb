Imports MySql.Data.MySqlClient
Public Class FrmMovie
    Dim strcon As New MySqlConnection
    Dim da As New MySqlDataAdapter
    Dim ds As New DataSet
    Dim cmd As New MySqlCommand

    Dim id As String

    'Dim name As String

    Dim movieDate As String = ""

    Private Sub FrmMovie_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        strcon = New MySqlConnection(DBModule.strcon)
        strcon.Open()

        txtDate.Format = DateTimePickerFormat.Custom
        txtDate.CustomFormat = "dd-MMM-yy"
        txtDate.Value = System.DateTime.Now.ToString("yyyy-MM-dd")

        Button1.FlatAppearance.BorderSize = 0
        Button2.FlatAppearance.BorderSize = 0
        btnSave.FlatAppearance.BorderSize = 0
        btnUpdate.FlatAppearance.BorderSize = 0
        Button4.FlatAppearance.BorderSize = 0
        Button5.FlatAppearance.BorderSize = 0


        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        ShowData()
    End Sub

    Sub ShowData()

        da = New MySqlDataAdapter("Select movieName, movieLang, moviePrice, movieDate, movieStatus, pic from movie ORDER BY movieName ASC", strcon)

        da.Fill(ds, "movie")
        ds.Clear()
        da.Fill(ds, "movie")

        DataGridView1.DataSource = ds.Tables("movie")

        DataGridView1.RowTemplate.Height = 150
        With DataGridView1
            .Columns(0).HeaderText = "ຊື່ໜັງ"
            .Columns(1).HeaderText = "ພາສາ"
            .Columns(2).HeaderText = "ລາຄາ"
            .Columns(3).HeaderText = "ມື້ເຂົ້າສາຍ"
            .Columns(4).HeaderText = "ສະຖານະ"
            .Columns(5).HeaderText = "ຮູບພາບ"

            Dim imageColumn = DirectCast(DataGridView1.Columns(5), DataGridViewImageColumn)
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch

            .Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill


            Dim str As String = ""
            Dim j As Integer
            For j = 0 To ds.Tables("movie").Rows.Count - 1
                DataGridView1.Rows(j).Selected = False
                str = DataGridView1.Rows(j).Cells(1).Value.ToString
                If chEN.Checked Then
                    str = chEN.Text
                ElseIf chTH.Checked Then
                    str = chTH.Text
                ElseIf chLA.Checked Then
                    str = chLA.Text
                End If
                If DataGridView1.Rows(j).Cells(0).Value.ToString = txtName.Text And DataGridView1.Rows(j).Cells(1).Value.ToString = str Then
                    DataGridView1.Rows(j).Selected = True
                    DataGridView1.FirstDisplayedScrollingRowIndex = j
                End If
            Next


        End With
        DataGridView1.Refresh()
    End Sub

    Sub Clear()

        btnSave.Enabled = True
        txtName.Text = ""
        chEN.Checked = False
        chTH.Checked = False
        chLA.Checked = False
        txtDate.Value = System.DateTime.Now.ToString("dd-MMM-yy")
        txtPrice.Text = ""
        cbxStatus.Text = "ເລືອກສະຖານະ"
        PictureBox1.Image = Nothing

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OpenFileDialog1.InitialDirectory = "C:\Users\Soulintha\Desktop\MayMovie\Movie"
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            OpenFileDialog1.Filter = "Image Only|*.jpg;*.jpeg;*.png"
            PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName)

        End If
    End Sub


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim str As String = ""
        Name = txtName.Text
        'movieDate = txtDate.Value

        If chEN.Checked Then
            str = chEN.Text
        ElseIf chTH.Checked Then
            str = chTH.Text
        ElseIf chLA.Checked Then
            str = chLA.Text
        End If

        Dim i As Integer
        For i = 0 To DataGridView1.Rows.Count() - 1
            If DataGridView1.Rows(i).Cells(0).Value.ToString() = Name And DataGridView1.Rows(i).Cells(1).Value.ToString() = str And DataGridView1.Rows(i).Cells(2).Value.ToString() = txtPrice.Text And DataGridView1.Rows(i).Cells(3).Value.ToString() = movieDate And DataGridView1.Rows(i).Cells(4).Value.ToString() = cbxStatus.Text Then
                MsgBox("ຂໍ້ມູນຊໍ້າກັບແຖວອື່ນ")
                Return
            End If
        Next

        If Name = "" Or txtPrice.Text = "" Then
            MsgBox("ກະລຸນາຕຶ່ມຂໍ້ມູນໃຫ້ຄົບຖ້ວນ")
            Return
        End If



        If chEN.Checked = False And chTH.Checked = False And chLA.Checked = False Then
            MsgBox("ກະລຸນາຕຶ່ມຂໍ້ມູນໃຫ້ຄົບຖ້ວນ")
            Return
        End If

        Dim status As String = ""
        If cbxStatus.Text <> "ເລືອກສະຖານະ" Then
            status = cbxStatus.Text
        ElseIf cbxStatus.Text = "ເລືອກສະຖານະ" Then
            MsgBox("ກະລຸນາເລືອກສະຖານະ")
            Return
        End If

        If PictureBox1.Image Is Nothing Then
            MsgBox("ກະລຸນາເລືອກຮູບພາບ")
            Return
        End If


        'Dim price As Integer
        'Int32.Parse(txtPrice.Text, price)

        Dim ms As New System.IO.MemoryStream()
        PictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
        Dim arrImage() As Byte = ms.ToArray()
        ms.Close()

        Dim c As Control
        For Each c In Panel8.Controls
            If TypeOf (c) Is CheckBox Then
                If CType(c, CheckBox).Checked = True Then
                    cmd = New MySqlCommand("Insert into movie (movieName, movieLang, moviePrice, movieDate, movieStatus, pic) values (@movieName, @movieLang, @moviePrice, @movieDate, @movieStatus, @pic)", strcon)
                    cmd.Parameters.Add("@movieName", MySqlDbType.VarChar).Value = Name
                    cmd.Parameters.Add("@movieLang", MySqlDbType.VarChar).Value = CType(c, CheckBox).Text
                    cmd.Parameters.Add("@moviePrice", MySqlDbType.Int32).Value = txtPrice.Text
                    cmd.Parameters.Add("@movieDate", MySqlDbType.Date).Value = txtDate.Value.ToString("yyyy-MM-dd")
                    cmd.Parameters.Add("@movieStatus", MySqlDbType.VarChar).Value = status
                    cmd.Parameters.Add("@pic", MySqlDbType.LongBlob).Value = arrImage

                    cmd.ExecuteNonQuery()

                End If

            End If

        Next

        ShowData()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        btnSave.Enabled = False
        Dim checkStatus As String

        txtName.Text = DataGridView1.CurrentRow.Cells(0).Value.ToString

        checkStatus = DataGridView1.CurrentRow.Cells(1).Value.ToString
        If chEN.Text = checkStatus Then
            chEN.Checked = True
            chTH.Checked = False
            chLA.Checked = False
        ElseIf chTH.Text = checkStatus Then
            chTH.Checked = True
            chEN.Checked = False
            chLA.Checked = False
        ElseIf chLA.Text = checkStatus Then
            chLA.Checked = True
            chTH.Checked = False
            chEN.Checked = False
        End If
        txtPrice.Text = DataGridView1.CurrentRow.Cells(2).Value.ToString
        txtDate.Text = DataGridView1.CurrentRow.Cells(3).Value.ToString
        cbxStatus.Text = DataGridView1.CurrentRow.Cells(4).Value.ToString

        Dim picbyte() As Byte = DataGridView1.CurrentRow.Cells(5).Value
        Dim ms As New System.IO.MemoryStream(picbyte)
        PictureBox1.Image = Image.FromStream(ms)

        da = New MySqlDataAdapter("select movieID from movie where movieName = '" & txtName.Text & "' and movieLang = '" & checkStatus & "'", strcon)
        da.Fill(ds, "id")
        ds.Clear()
        da.Fill(ds, "id")
        id = ds.Tables("id").Rows(0)(0).ToString


        ShowData()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        btnSave.Enabled = True
        Clear()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

        Dim str As String = ""
        'movieDate = txtDate.Text
        'Int32.Parse(txtPrice.Text, price)

        If chEN.Checked Then
            str = chEN.Text
        ElseIf chTH.Checked Then
            str = chTH.Text
        ElseIf chLA.Checked Then
            str = chLA.Text
        End If

        If chEN.Checked And chTH.Checked Then
            MsgBox("ເລືອກໄດ້ 1ພາສາ")
            Return
        ElseIf chEN.Checked And chLA.Checked Then
            MsgBox("ເລືອກໄດ້ 1ພາສາ")
            Return
        ElseIf chLA.Checked And chTH.Checked Then
            MsgBox("ເລືອກໄດ້ 1ພາສາ")
            Return
        End If


        If Name = "" Or movieDate = "  -  -" Or txtPrice.Text = "" Then
            MsgBox("ກະລຸນາຕຶ່ມຂໍ້ມູນໃຫ້ຄົບຖ້ວນ")
            Return
        End If

        If chEN.Checked = False And chTH.Checked = False And chLA.Checked = False Then
            MsgBox("ກະລຸນາຕຶ່ມຂໍ້ມູນໃຫ້ຄົບຖ້ວນ")
            Return
        End If
        Dim status As String = ""
        If cbxStatus.Text <> "ເລືອກສະຖານະ" Then
            status = cbxStatus.Text
        ElseIf cbxStatus.Text = "ເລືອກສະຖານະ" Then
            MsgBox("ກະລຸນາເລືອກສະຖານະ")
            Return
        End If

        If PictureBox1.Image Is Nothing Then
            MsgBox("ກະລຸນາເລືອກຮູບພາບ")
            Return
        End If

        Dim ms As New System.IO.MemoryStream()
        PictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
        Dim arrImage() As Byte = ms.ToArray()
        ms.Close()

        Dim i As Integer
        For i = 0 To DataGridView1.Rows.Count() - 1
            If DataGridView1.Rows(i).Cells(0).Value.ToString() = txtName.Text And DataGridView1.Rows(i).Cells(1).Value.ToString() = str And DataGridView1.Rows(i).Cells(2).Value.ToString() = txtPrice.Text And DataGridView1.Rows(i).Cells(3).Value.ToString() = txtDate.Text And DataGridView1.Rows(i).Cells(4).Value.ToString() = cbxStatus.Text Then
                MsgBox("ຂໍ້ມູນຊໍ້າກັບແຖວອື່ນ")
                Return
            End If
        Next

        cmd = New MySqlCommand("Update movie set moviePrice = @moviePrice, movieStatus = @movieStatus, movieDate = @movieDate where movieName = @movieName", strcon)
        cmd.Parameters.Add("@movieName", MySqlDbType.VarChar).Value = txtName.Text
        cmd.Parameters.Add("@moviePrice", MySqlDbType.Int32).Value = txtPrice.Text
        cmd.Parameters.Add("@movieStatus", MySqlDbType.VarChar).Value = status
        cmd.Parameters.Add("@movieDate", MySqlDbType.Date).Value = txtDate.Value
        cmd.ExecuteNonQuery()
        ShowData()

        cmd = New MySqlCommand("Update movie set movieName= @movieName, movieLang = @movieLang, pic = @pic where movieID = @movieID", strcon)
        cmd.Parameters.Add("@movieName", MySqlDbType.VarChar).Value = txtName.Text
        cmd.Parameters.Add("@movieLang", MySqlDbType.VarChar).Value = str
        cmd.Parameters.Add("@pic", MySqlDbType.LongBlob).Value = arrImage
        cmd.Parameters.Add("@movieID", MySqlDbType.Int32).Value = id

        cmd.ExecuteNonQuery()
        ShowData()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        btnSave.Enabled = True

        da = New MySqlDataAdapter("select count(movieID) from programe join selldetail on programe.proID = selldetail.proID where programe.movieID = '" & id & "'", strcon)
        da.Fill(ds, "count")
        ds.Clear()
        da.Fill(ds, "count")
        If ds.Tables("count").Rows(0)(0) > 0 Then
            ShowData()
            MsgBox("ບໍ່ສາມາດລຶບໄດ້ເນື່ອງຈາກມີການສາຍໜັງເລື່ອງນີ້ແລ້ວ")
            Return
        End If


        cmd = New MySqlCommand("delete from movie where movieID = '" & id & "'", strcon)
        cmd.ExecuteNonQuery()
        ShowData()
        Clear()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Dim frmPrice As New FrmType
        frmPrice.Show()
    End Sub

    Private Sub txtPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPrice.KeyPress
        If Not e.KeyChar = ChrW(Keys.Back) And Not e.KeyChar = Chr(13) Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
                MsgBox("ປ້ອນສະເພາະຕົວເລກ")
            End If
        End If
    End Sub

    Private Sub txtPrice_GotFocus(sender As Object, e As EventArgs) Handles txtPrice.GotFocus
        For Each Language As InputLanguage In InputLanguage.InstalledInputLanguages
            If Language.Culture.TwoLetterISOLanguageName.Contains("en") Then
                InputLanguage.CurrentInputLanguage = Language
            End If
        Next
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Dim frmPrice As New FrmType
        frmPrice.ShowDialog()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

        If ComboBox1.Text = "ທັງໝົດ" Then
            da = New MySqlDataAdapter("select movieName, movieLang, moviePrice, movieDate, movieStatus, pic from movie", strcon)
            da.Fill(ds, "all")
            ds.Clear()
            da.Fill(ds, "all")

            DataGridView1.DataSource = ds.Tables("all")
        Else
            da = New MySqlDataAdapter("select movieName, movieLang, moviePrice, movieDate, movieStatus, pic from movie where movieStatus = '" & ComboBox1.SelectedItem.ToString & "'", strcon)
            da.Fill(ds, "search")
            ds.Clear()
            da.Fill(ds, "search")

            DataGridView1.DataSource = ds.Tables("search")

        End If
        DataGridView1.Refresh()
    End Sub

End Class