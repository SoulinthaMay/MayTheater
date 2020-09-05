Imports MySql.Data.MySqlClient
Public Class FormMemInfo
    Dim strcon As New MySqlConnection
    Dim da As New MySqlDataAdapter
    Dim cmd As New MySqlCommand
    Dim ds As New DataSet

    Private Sub FormMemInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        strcon = New MySqlConnection(DBModule.strcon)
        strcon.Open()
        DataGridView1.RowTemplate.Height = 150
        ShowData()


    End Sub

    Sub ShowData()
        da = New MySqlDataAdapter("select barcode, memberName, memberSurname, memberTel, date, expiredate, cardName from member join card on member.cardID = card.cardID ", strcon)
        da.Fill(ds, "member")
        ds.Clear()
        da.Fill(ds, "member")
        DataGridView1.DataSource = ds.Tables("member")
        DataGridView1.RowTemplate.Height = 120
        With DataGridView1
            .Columns(0).HeaderText = "ລະຫັດບັດ"
            .Columns(1).HeaderText = "ຊື່"
            .Columns(2).HeaderText = "ນາມສະກຸນ"
            .Columns(3).HeaderText = "ເບີໂທ"
            .Columns(4).HeaderText = "ວັນທີສະໝັກ"
            .Columns(5).HeaderText = "ວັນໝົດອາຍຸ"
            .Columns(6).HeaderText = "ປະເພດບັດ"

            .Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(5).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(6).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

    End Sub

    Private Sub TextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyUp
        da = New MySqlDataAdapter("select barcode, memberName, memberSurname, memberTel, date, expiredate, cardName from member join card on member.cardID = card.cardID where member.barcode like '%" & TextBox1.Text & "%'", strcon)
        da.Fill(ds, "search")
        ds.Clear()
        da.Fill(ds, "search")

        DataGridView1.DataSource = ds.Tables("search")
        DataGridView1.Refresh()
    End Sub

End Class