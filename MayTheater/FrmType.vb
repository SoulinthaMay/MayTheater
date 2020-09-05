Imports MySql.Data.MySqlClient
Public Class FrmType
    Dim strcon As New MySqlConnection
    Dim da As New MySqlDataAdapter
    Dim ds As New DataSet
    Dim cmd As New MySqlCommand

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub FrmPrice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        strcon = New MySqlConnection(DBModule.strcon)
        strcon.Open()
        ShowData()
    End Sub

    Sub ShowData()
        da = New MySqlDataAdapter("select typeName, plusPrice from seattype", strcon)
        da.Fill(ds, "type")
        ds.Clear()
        da.Fill(ds, "type")

        txtNormal.Text = ds.Tables("type").Rows(0)(1)
        txtHoney.Text = ds.Tables("type").Rows(1)(1)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        cmd = New MySqlCommand("Update seattype set plusPrice = '" & txtHoney.Text & "' where typeName = 'Honeymoon'", strcon)
        cmd.ExecuteNonQuery()

        cmd = New MySqlCommand("Update seattype set plusPrice = '" & txtNormal.Text & "' where typeName = 'Normal'", strcon)
        cmd.ExecuteNonQuery()

        ShowData()
        MsgBox("ແກ້ໄຂຂໍ້ມູນສຳເລັດ")
    End Sub
End Class