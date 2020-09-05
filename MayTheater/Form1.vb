Imports MySql.Data.MySqlClient

Public Class Form1


    Dim strcon As New MySqlConnection
        Dim da As New MySqlDataAdapter
        Dim ds As New DataSet
        Dim cmd As New MySqlCommand

        Dim c As Control

        Dim ii As Integer = 0
        Dim id, proID As Integer

        Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            strcon = New MySqlConnection(DBModule.strcon)
            strcon.Open()

            DateTimePicker1.Format = DateTimePickerFormat.Custom
            DateTimePicker1.CustomFormat = "dd-MMM-yy"
        DateTimePicker1.Value = System.DateTime.Now.ToString("dd-MM-yyyy")

        btnSave.FlatAppearance.BorderSize = 0
        btnUpdate.FlatAppearance.BorderSize = 0
        Button1.FlatAppearance.BorderSize = 0
        Button2.FlatAppearance.BorderSize = 0

        cbxRoom.Text = 1

            cbxName.Items.Clear()
            Dim i As Integer
            'c, mcslckfv
            '    sklsmclksd
            da = New MySqlDataAdapter("select distinct movieName from movie where movieStatus <> 'ອອກຈາກໂຮງສາຍ' and movieDate <= '" & System.DateTime.Now.ToString("yyyy-MM-dd") & "' order by movieName ASC", strcon)
            da.Fill(ds, "name")
            ds.Clear()
            da.Fill(ds, "name")
            For i = 0 To ds.Tables("name").Rows.Count - 1
                cbxName.Items.Add(ds.Tables("name").Rows(i)(0))
            Next

            For Each c In TableLayoutPanel2.Controls
                If TypeOf (c) Is Label Then
                CType(c, Label).BackColor = Color.White
                CType(c, Label).ForeColor = Color.FromArgb(111, 111, 111)
                AddHandler c.Click, AddressOf Label8_Click
                End If
            Next


            ShowData()
            setButton()

        End Sub

        Sub setButton()

            For Each c In TableLayoutPanel2.Controls
                If TypeOf (c) Is Label Then
                CType(c, Label).BackColor = Color.White
                CType(c, Label).ForeColor = Color.FromArgb(111, 111, 111)
            End If
            Next

        End Sub


        Sub ShowData()

        'da = New MySqlDataAdapter("select A.proID, movieName, movieLang, proRoom, proDate, proTime, movieStatus from movie join programe on movie.movieID = programe.movieID where proDate = '" & DateTimePicker1.Value.ToString("dd-MM-yyyy") & "' and proRoom = '" & cbxRoom.Text & "' ORDER BY proTime ASC", DBModule.strcon)
        da = New MySqlDataAdapter("select B.movieName, B.movieLang, A.proRoom, A.proDate, A.proTime, B.movieStatus,A.proID from programe A join movie B on A.movieID = B.movieID where A.proDate='" & DateTimePicker1.Value.ToString("dd-MM-yyyy") & "' and A.proRoom = '" & cbxRoom.Text & "' ORDER BY A.proTime ASC", DBModule.strcon)
        da.Fill(ds, "1")
        ds.Clear()
            da.Fill(ds, "1")
            DataGridView1.DataSource = ds.Tables("1")
            DataGridView1.RowTemplate.Height = 100

            For Each c In TableLayoutPanel2.Controls
                If TypeOf (c) Is Label Then
                    For i As Integer = 0 To ds.Tables("1").Rows.Count - 1

                        If CType(c, Label).Text = ds.Tables("1").Rows(i)(4).ToString Then
                            CType(c, Label).Enabled = False
                        End If
                    Next
                End If
            Next

        With DataGridView1

            If DataGridView1.Columns.Count - 1 = 6 Then
                .Columns(0).HeaderText = "ຊື່ໜັງ"
                .Columns(1).HeaderText = "ພາສາ"
                .Columns(2).HeaderText = "ໂຮງໜັງ"
                .Columns(3).HeaderText = "ວັນທີ"
                .Columns(4).HeaderText = "ເວລາ"
                .Columns(5).HeaderText = "ສະຖານະ"
                .Columns(6).HeaderText = "ID"
                .Columns(6).Visible = False
            End If



            For j As Integer = 0 To ds.Tables("1").Rows.Count - 1
                DataGridView1.Rows(j).Selected = False
                For Each c In TableLayoutPanel2.Controls
                    If TypeOf (c) Is Label Then

                        If CType(c, Label).BackColor = Color.FromArgb(101, 240, 182) Then

                            If DataGridView1.Rows(j).Cells(0).Value.ToString = cbxName.Text And DataGridView1.Rows(j).Cells(1).Value.ToString = cbxLang.Text And DataGridView1.Rows(j).Cells(2).Value.ToString = cbxRoom.Text And DataGridView1.Rows(j).Cells(3).Value.ToString = DateTimePicker1.Value.ToShortDateString And DataGridView1.Rows(j).Cells(4).Value.ToString = CType(c, Label).Text Then
                                DataGridView1.Rows(j).Selected = True
                                DataGridView1.FirstDisplayedScrollingRowIndex = j

                            End If
                            'AddHandler c.Click, AddressOf Label8_Click
                        End If
                    End If
                Next

            Next



        End With

        DataGridView1.Refresh()

        End Sub


        Private Sub Label8_Click(sender As Object, e As EventArgs)
        If CType(sender, Label).BackColor = Color.White Then
            CType(sender, Label).BackColor = Color.FromArgb(101, 240, 182)
            CType(sender, Label).ForeColor = Color.White
        ElseIf CType(sender, Label).BackColor = Color.FromArgb(101, 240, 182) Then
            CType(sender, Label).BackColor = Color.White
            CType(sender, Label).ForeColor = Color.FromArgb(111, 111, 111)
        End If
        End Sub

        Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        For Each c In TableLayoutPanel2.Controls
            If TypeOf (c) Is Label Then
                CType(c, Label).BackColor = Color.White
                CType(c, Label).ForeColor = Color.FromArgb(111, 111, 111)
            End If
        Next

        ii = DataGridView1.CurrentRow.Index

        Dim name, room, lang, datee, time As String
        room = DataGridView1.CurrentRow.Cells(2).Value
        name = DataGridView1.CurrentRow.Cells(0).Value
        lang = DataGridView1.CurrentRow.Cells(1).Value
        datee = DataGridView1.CurrentRow.Cells(3).Value
        time = DataGridView1.CurrentRow.Cells(4).Value
        cbxRoom.Text = room
        cbxName.Text = name
        cbxLang.Text = lang
        DateTimePicker1.Text = datee
        proID = DataGridView1.CurrentRow.Cells(6).Value

        For Each c In TableLayoutPanel2.Controls
            If TypeOf (c) Is Label Then
                If CType(c, Label).Enabled = False Then

                    If CType(c, Label).Text = DataGridView1.Rows(ii).Cells(4).Value.ToString Then
                        CType(c, Label).ForeColor = Color.White
                        CType(c, Label).BackColor = Color.FromArgb(101, 240, 182)

                    End If
                End If

            End If
        Next



        'da = New MySqlDataAdapter("select movieID from movie where movieName = '" & name & "' and movieLang = '" & lang & "'", strcon)
        'da.Fill(ds, "id")
        'ds.Clear()
        'da.Fill(ds, "id")
        'Dim id As Integer
        'Int32.TryParse(ds.Tables("id").Rows(0)(0), id)

        'da = New MySqlDataAdapter("select proID from programe where movieID = '" & id & "' and proRoom = '" & room & "' and proTime = '" & time & "' and proDate = '" & datee & "'", strcon)
        'da.Fill(ds, "proid")
        'ds.Clear()
        'da.Fill(ds, "proid")
        'Int32.TryParse(ds.Tables("proid").Rows(0)(0), proID)

        ShowData()
        DataGridView1.Rows(ii).Selected = True


    End Sub

        Private Sub cbxRoom_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxRoom.SelectedIndexChanged

            For Each c In TableLayoutPanel2.Controls
                If TypeOf (c) Is Label Then
                    CType(c, Label).Enabled = True
                CType(c, Label).BackColor = Color.White
                CType(c, Label).ForeColor = Color.FromArgb(111, 111, 111)
            End If
            Next

            ShowData()
            setButton()
        End Sub

        Private Sub cbxName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxName.SelectedIndexChanged

            da = New MySqlDataAdapter("select distinct movieLang from movie where movieName = '" & cbxName.Text & "' order by movieLang ASC ", strcon)
            da.Fill(ds, "lang")
            ds.Clear()
            da.Fill(ds, "lang")

            cbxLang.Items.Clear()

            For i = 0 To ds.Tables("lang").Rows.Count - 1
                cbxLang.Items.Add(ds.Tables("lang").Rows(i)(0))
            Next
            cbxLang.Text = ds.Tables("lang").Rows(0)(0)

            ShowData()
        End Sub

        Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged

            For Each c In TableLayoutPanel2.Controls
                If TypeOf (c) Is Label Then
                    CType(c, Label).Enabled = True
                CType(c, Label).BackColor = Color.White
                CType(c, Label).ForeColor = Color.FromArgb(111, 111, 111)
            End If
            Next

            cbxName.Items.Clear()
            Dim i As Integer
        da = New MySqlDataAdapter("select distinct movieName from movie where movieStatus <> 'ອອກຈາກໂຮງສາຍ'  and movieDate <= '" & DateTimePicker1.Value.ToString("yyyy-MM-dd") & "' order by movieName ASC", DBModule.strcon)
        da.Fill(ds, "change")
            ds.Clear()
            da.Fill(ds, "change")

            For i = 0 To ds.Tables("change").Rows.Count - 1
                cbxName.Items.Add(ds.Tables("change").Rows(i)(0))
            Next

            ShowData()
            setButton()
        End Sub

        Private Sub cbxLang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxLang.SelectedIndexChanged
            ShowData()
        End Sub

        Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

            da = New MySqlDataAdapter("select count(proID) from selldetail where selldetail.proID = '" & proID & "'", strcon)
            da.Fill(ds, "countt")
            ds.Clear()
            da.Fill(ds, "countt")
        If ds.Tables("countt").Rows.Count > 0 Then
            ShowData()
            MsgBox("ບໍ່ສາມາດລຶບ ຫຼື ແກ້ໄຂຂໍ້ມູນໄດ້ເພາະມີການຂາຍປີ້ຮອບນີ້ແລ້ວ")

            Return
        End If

        cmd = New MySqlCommand("delete from programe where proID = @proID", strcon)
            cmd.Parameters.Add("@proID", MySqlDbType.Int32).Value = proID
            cmd.ExecuteNonQuery()
            For Each c In TableLayoutPanel2.Controls
                If TypeOf (c) Is Label Then
                    CType(c, Label).Enabled = True
                CType(c, Label).BackColor = Color.White
                CType(c, Label).ForeColor = Color.FromArgb(111, 111, 111)
            End If
            Next
            cbxName.Text = "ເລືອກຊື່ໜັງ"
            cbxLang.Text = "ເລືອກພາສາ"
            ShowData()
            setButton()
        End Sub

        Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

            If cbxRoom.Text = "ເລືອກໂຮງໜັງ" Then
                MsgBox("ກະລຸນາເລືອກໂຮງໜັງ")
                Return
            End If

            If cbxName.Text = "ເລືອກຊື່ໜັງ" Then
                MsgBox("ກະລຸນາເລືອກຊື່ໜັງ")
                Return
            End If

            da = New MySqlDataAdapter("select count(proID) from selldetail where selldetail.proID = '" & proID & "'", strcon)
            da.Fill(ds, "countt")
            ds.Clear()
            da.Fill(ds, "countt")
        If ds.Tables("countt").Rows.Count > 0 Then
            ShowData()
            MsgBox("ບໍ່ສາມາດລຶບ ຫຼື ແກ້ໄຂຂໍ້ມູນໄດ້ເພາະມີການຂາຍປີ້ຮອບນີ້ແລ້ວ")

            Return
        End If

        Dim i As Integer = 0
            Dim time1 As String = ""
            For Each c In TableLayoutPanel2.Controls
                If TypeOf (c) Is Label Then
                If CType(c, Label).BackColor = Color.FromArgb(101, 240, 182) And CType(c, Label).Enabled = True Then
                    i += 1
                    time1 = CType(c, Label).Text

                End If
            End If
            Next
            If i > 1 Then
                MsgBox("ເລືອກປ່ຽນໄດ້ເວລາດຽວ")
                Return
            End If

            If i = 1 Then
            da = New MySqlDataAdapter("select proTime from programe where movieID = '" & id & "' and proDate = '" & DateTimePicker1.Value.ToString("dd-MM-yyyy") & "'", strcon)
            da.Fill(ds, "check")
                ds.Clear()
                da.Fill(ds, "check")
                For n As Integer = 0 To ds.Tables("check").Rows.Count - 1
                    If time1 = ds.Tables("check").Rows(n)(0) Then
                        ShowData()
                        setButton()
                        MsgBox("ໜັງເລື່ອງນີ້ສາຍເວລາທີ່ເລືອກແລ້ວ ກະລຸນາເລືອກເວລາໃໝ່")
                        Return
                    End If
                Next


                cmd = New MySqlCommand("update programe set proTime = @time where proID = @proID", strcon)
                cmd.Parameters.Add("@time", MySqlDbType.VarChar).Value = time1
                cmd.Parameters.Add("@proID", MySqlDbType.Int32).Value = proID
                cmd.ExecuteNonQuery()

                For Each c In TableLayoutPanel2.Controls
                    If TypeOf (c) Is Label Then
                        CType(c, Label).Enabled = True
                    CType(c, Label).BackColor = Color.White
                    CType(c, Label).ForeColor = Color.FromArgb(111, 111, 111)
                End If
                Next
                ShowData()
                setButton()
            End If

            If i = 0 Then

                da = New MySqlDataAdapter("select movieID from movie where movieName = '" & cbxName.Text & "' and movieLang = '" & cbxLang.Text & "'", strcon)
                da.Fill(ds, "id")
                ds.Clear()
                da.Fill(ds, "id")
                Dim id As Integer
                Int32.TryParse(ds.Tables("id").Rows(0)(0), id)

                cmd = New MySqlCommand("update programe set movieID = @id, proRoom = @room, proDate = @date where proID = @proID", strcon)
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id
                cmd.Parameters.Add("@room", MySqlDbType.VarChar).Value = cbxRoom.Text
            cmd.Parameters.Add("@date", MySqlDbType.VarChar).Value = DateTimePicker1.Value.ToString("dd-MM-yyyy")
            cmd.Parameters.Add("@proID", MySqlDbType.Int32).Value = proID
                cmd.ExecuteNonQuery()

                ShowData()
                setButton()
            End If

        End Sub

        Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
            cbxName.Text = "ເລືອກຊື່ໜັງ"
            cbxLang.Text = "ເລືອກພາສາ"
            ShowData()
            setButton()
        End Sub

        Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
            If cbxRoom.Text = "ເລືອກໂຮງໜັງ" Then
                MsgBox("ກະລຸນາເລືອກໂຮງໜັງ")
                Return
            End If

            If cbxName.Text = "ເລືອກຊື່ໜັງ" Then
                MsgBox("ກະລຸນາເລືອກຊື່ໜັງ")
                Return
            End If


            Dim time As String = ""
            Dim Tselect As Boolean = False


            da = New MySqlDataAdapter("select movieID from movie where movieName = '" & cbxName.Text & "' and movieLang = '" & cbxLang.Text & "'", strcon)
            da.Fill(ds, "id")
            ds.Clear()
            da.Fill(ds, "id")
            Dim id As Integer
            Int32.TryParse(ds.Tables("id").Rows(0)(0), id)


        da = New MySqlDataAdapter("select proTime from programe where movieID = '" & id & "' and proDate = '" & DateTimePicker1.Value.ToString("dd-MM-yyyy") & "'", strcon)
        da.Fill(ds, "check")
            ds.Clear()
            da.Fill(ds, "check")


            For Each c In TableLayoutPanel2.Controls
                If TypeOf (c) Is Label Then
                If CType(c, Label).BackColor = Color.FromArgb(101, 240, 182) Then
                    Tselect = True
                    If CType(c, Label).Enabled = True Then

                        time = CType(c, Label).Text

                        For n As Integer = 0 To ds.Tables("check").Rows.Count - 1
                            If time = ds.Tables("check").Rows(n)(0) Then
                                ShowData()
                                setButton()
                                MsgBox("ໜັງເລື່ອງນີ້ສາຍເວລາທີ່ເລືອກແລ້ວ ກະລຸນາເລືອກເວລາໃໝ່")

                                Return
                            End If
                        Next




                        cmd = New MySqlCommand("insert into programe (movieID, proRoom, proTime, proDate) values (@id, @room, @time, @date)", strcon)
                        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id
                        cmd.Parameters.Add("@room", MySqlDbType.VarChar).Value = cbxRoom.Text
                        cmd.Parameters.Add("@time", MySqlDbType.VarChar).Value = time
                        cmd.Parameters.Add("@date", MySqlDbType.VarChar).Value = DateTimePicker1.Value.ToString("dd-MM-yyyy")
                        cmd.ExecuteNonQuery()


                    End If
                End If
            End If
            Next

            ShowData()
            setButton()

            If Tselect = False Then
                MsgBox("ກະລຸນາເລືອກເວລາສາຍໜັງ")
                Return
            End If


        End Sub


End Class
