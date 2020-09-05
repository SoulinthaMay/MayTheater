Imports MySql.Data.MySqlClient
Public Class Form2
    Dim strcon As New MySqlConnection
    Dim da As New MySqlDataAdapter
    Dim ds As New DataSet
    Dim cmd As New MySqlCommand

    Public discount As Integer = 0
    Public arr As New ArrayList
    Public ticket As Integer = 0

    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        strcon = New MySqlConnection(DBModule.strcon)
        strcon.Open()

        Button1.FlatAppearance.BorderSize = 0
        Button3.FlatAppearance.BorderSize = 0
        Button2.FlatAppearance.BorderSize = 0
        Button5.FlatAppearance.BorderSize = 0

        Panel3.Height = Panel1.Height
        Panel4.Height = Panel1.Height

        Button3.FlatAppearance.BorderSize = 0
        Button3.FlatAppearance.BorderColor = Color.Black
        DataGridView1.RowTemplate.Height = 30

        With DataGridView1
            .Columns(0).HeaderText = "ຊື່"
            .Columns(1).HeaderText = "ນາມສະກຸນ"
            .Columns(2).HeaderText = "ປະເພດບັດ"
            .Columns(3).HeaderText = "Barcode"

            .Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        discount = 0
        arr.Clear()
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            da = New MySqlDataAdapter("select memberID, cardDiscount from member join card on member.cardID = card.cardID where barcode = '" & DataGridView1.Rows(i).Cells(3).Value & "'", strcon)
            da.Fill(ds, "conclude")
            ds.Clear()
            da.Fill(ds, "conclude")

            discount += ds.Tables("conclude").Rows(0)(1)
            arr.Add(ds.Tables("conclude").Rows(0)(0))
        Next
        Me.Close()
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        Dim id, name, surname, type, barcode As String


        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If TextBox1.Text = DataGridView1.Rows(i).Cells(3).Value Then
                MsgBox("Enter new barcode")
                Return
            End If
        Next

        If TextBox1.Text.Length > 0 Then
            Try

                If ticket > DataGridView1.Rows.Count Then
                    da = New MySqlDataAdapter("select memberID, memberName, memberSurname, cardName, cardMaxTime, barcode from member join card on member.cardID = card.cardID where barcode = '" & TextBox1.Text & "' and member.expiredate >= '" & System.DateTime.Now.ToString("yyyy-MM-dd") & "'", strcon)
                    da.Fill(ds, "member")
                    ds.Clear()
                    da.Fill(ds, "member")
                    id = ds.Tables("member").Rows(0)(0)
                    name = ds.Tables("member").Rows(0)(1)
                    surname = ds.Tables("member").Rows(0)(2)
                    type = ds.Tables("member").Rows(0)(3)
                    barcode = ds.Tables("member").Rows(0)(5)
                    Dim checkCard As Integer = 0
                    checkCard = ds.Tables("member").Rows(0)(4)

                    Dim num As Integer = 0
                    da = New MySqlDataAdapter("select count(memberID) from memberuse where memberID = '" & ds.Tables("member").Rows(0)(0) & "' and date = '" & System.DateTime.Now.ToString("dd-MMM-yy") & "'", strcon)
                    da.Fill(ds, "num")
                    ds.Clear()
                    da.Fill(ds, "num")
                    num = ds.Tables("num").Rows(0)(0)
                    If num >= checkCard Then
                        MsgBox("ບັດ " + TextBox1.Text + " ໃຊ້ເກີນ " + checkCard.ToString + " ຄັ້ງ/ມື້ ແລ້ວ")
                        Return
                    End If
                    DataGridView1.Rows.Add(name, surname, type, barcode)
                End If
            Catch ex As Exception
                MsgBox("This barcode is invalid")
            End Try
        End If

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Dim i As Integer = DataGridView1.CurrentRow.Index
        DataGridView1.Rows.RemoveAt(i)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

End Class