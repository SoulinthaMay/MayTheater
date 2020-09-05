Imports MySql.Data.MySqlClient
Imports System.IO
Public Class FrmEmployee
    Dim strcon As New MySqlConnection
    Dim da As New MySqlDataAdapter
    Dim cmd As New MySqlCommand
    Dim ds As New DataSet
    Dim reader As MySqlDataReader

    Dim userID, userName, password, status, pic As String

    Dim ms As New System.IO.MemoryStream()
    Dim arrImage() As Byte

    Private Sub FrmEmployee_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        strcon = New MySqlConnection(DBModule.strcon)
        strcon.Open()

        Button1.FlatAppearance.BorderSize = 0
        btnSave.FlatAppearance.BorderSize = 0
        btnUpdate.FlatAppearance.BorderSize = 0
        btnDelete.FlatAppearance.BorderSize = 0
        btnClear.FlatAppearance.BorderSize = 0

        OpenFileDialog1.InitialDirectory = "C:\Users\Soulintha\Desktop\MayMovie\User"
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        picSearch.SizeMode = PictureBoxSizeMode.Zoom
        ShowData()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        cmd = New MySqlCommand("Delete from users where userID = '" & txtUserID.Text & "'", strcon)
        cmd.ExecuteNonQuery()
        ShowData()
        Clear()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        da = New MySqlDataAdapter("select userID, userName, password, status,pic from users where userID = '" & DataGridView1.CurrentRow.Cells(0).Value.ToString & "'", strcon)
        da.Fill(ds, "users")
        ds.Clear()
        da.Fill(ds, "users")

        txtUserID.Text = ds.Tables("users").Rows(0)(0)
        txtUserID.Enabled = False
        txtUserName.Text = ds.Tables("users").Rows(0)(1)
        txtPassword.Text = ds.Tables("users").Rows(0)(2)
        cbxStatus.Text = ds.Tables("users").Rows(0)(3)
        Dim picbyte() As Byte = ds.Tables("users").Rows(0)(4)
        Dim ms As New System.IO.MemoryStream(picbyte)
        PictureBox1.Image = Image.FromStream(ms)
        ShowData()
    End Sub


    Sub ShowData()
        Dim i As Integer = 0

        da = New MySqlDataAdapter("Select userID, userName, password, status, pic from users", strcon)
        da.Fill(ds, "users")
        ds.Clear()
        da.Fill(ds, "users")

        DataGridView1.DataSource = ds.Tables("users")

        DataGridView1.RowTemplate.Height = 150
        With DataGridView1
            .Columns(0).HeaderText = "UserID"
            .Columns(1).HeaderText = "UserName"
            .Columns(2).HeaderText = "Password"
            .Columns(3).HeaderText = "Status"
            .Columns(4).HeaderText = "Picture"

            Dim imageColumn = DirectCast(DataGridView1.Columns(4), DataGridViewImageColumn)
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch

            .Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

            Dim j As Integer
            For j = 0 To ds.Tables("users").Rows.Count - 1
                DataGridView1.Rows(j).Selected = False
                If DataGridView1.Rows(j).Cells(0).Value.ToString = txtUserID.Text Then
                    DataGridView1.Rows(j).Selected = True
                    DataGridView1.FirstDisplayedScrollingRowIndex = j
                End If
            Next

        End With
        DataGridView1.Refresh()
    End Sub

    Sub Clear()

        txtUserID.Enabled = True
        txtUserID.Text = ""
        txtUserName.Text = ""
        txtPassword.Text = ""
        cbxStatus.Text = "ເລືອກສະຖານະ"
        PictureBox1.Image = Nothing

    End Sub


    Private Sub txtSearch_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyUp
        da = New MySqlDataAdapter("select * from users where userID like '%" & txtSearch.Text & "%' Or userName like '%" & txtSearch.Text & "%'
Or password like '%" & txtSearch.Text & "%' Or status like '%" & txtSearch.Text & "%'", strcon)
        da.Fill(ds, "search")
        ds.Clear()
        da.Fill(ds, "search")

        DataGridView1.DataSource = ds.Tables("search")
        DataGridView1.Refresh()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Clear()
    End Sub




    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        userID = txtUserID.Text
        userName = txtUserName.Text
        password = txtPassword.Text

        Dim i As Integer
        For i = 0 To DataGridView1.Rows.Count() - 1
            If DataGridView1.Rows(i).Cells(0).Value.ToString() = userID Or DataGridView1.Rows(i).Cells(1).Value.ToString() = userName Or DataGridView1.Rows(i).Cells(2).Value.ToString() = password Then
                MsgBox("ຂໍ້ມູນມີຄ່າຊໍ້າກັບແຖວອື່ນ")
                Return
            End If
        Next

        If userID = "" Or userName = "" Or password = "" Then
            MsgBox("ກະລຸນາຕຶ່ມຂໍ້ມູນໃຫ້ຄົບຖ້ວນ")
            Return
        End If

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

        cmd = New MySqlCommand("Insert into users (userID, userName, password, status, pic) values (@userID, @userName, @password, @status, @pic)", strcon)
        cmd.Parameters.Add("@userID", MySqlDbType.VarChar).Value = userID
        cmd.Parameters.Add("@userName", MySqlDbType.VarChar).Value = userName
        cmd.Parameters.Add("@password", MySqlDbType.VarChar).Value = password
        cmd.Parameters.Add("@status", MySqlDbType.VarChar).Value = status
        cmd.Parameters.Add("@pic", MySqlDbType.LongBlob).Value = arrImage

        cmd.ExecuteNonQuery()
        ShowData()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

        userID = txtUserID.Text
        userName = txtUserName.Text
        password = txtPassword.Text

        Dim i As Integer
        For i = 0 To DataGridView1.Rows.Count() - 1
            If DataGridView1.Rows(i).Cells(1).Value.ToString() = userName And DataGridView1.Rows(i).Cells(2).Value.ToString() = password Then
                If DataGridView1.Rows(i).Cells(3).Value.ToString() = cbxStatus.Text Then
                    MsgBox("Username ມີຄ່າຊໍ້າກັບແຖວອື່ນ")
                    Return
                End If

            End If
        Next
        If userID = "" Or userName = "" Or password = "" Then
            MsgBox("ກະລຸນາຕຶ່ມຂໍ້ມູນໃຫ້ຄົບຖ້ວນ")
            Return
        End If
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


        cmd = New MySqlCommand("update users set userName = @userName, password = @password, status = @status, pic = @pic where userID = @userID", strcon)
        cmd.Parameters.Add("@userName", MySqlDbType.VarChar).Value = userName
        cmd.Parameters.Add("@password", MySqlDbType.VarChar).Value = password
        cmd.Parameters.Add("@status", MySqlDbType.VarChar).Value = status
        cmd.Parameters.Add("@pic", MySqlDbType.LongBlob).Value = arrImage
        cmd.Parameters.Add("@userID", MySqlDbType.VarChar).Value = userID
        cmd.ExecuteNonQuery()
        ShowData()

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim strFolderPath As String = Application.StartupPath + "/myFile/"
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            OpenFileDialog1.Filter = "Image Only|*.jpg;*.jpeg;*.png"
            PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName)

        End If
    End Sub

End Class