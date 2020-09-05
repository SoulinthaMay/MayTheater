Imports MySql.Data.MySqlClient
Public Class FormSellReport
    Dim strcon As New MySqlConnection
    Dim da As New MySqlDataAdapter
    Dim ds As New DataSet
    Dim cmd As New MySqlCommand

    Private Sub FormSellReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        strcon = New MySqlConnection(DBModule.strcon)
        Button1.Width = 189
        Button1.Height = 57

        Button2.Width = 189
        Button2.Height = 57

        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "dd-MMM-yy"
        DateTimePicker1.Value = System.DateTime.Now.ToString("yyyy-MM-dd")

        DateTimePicker2.Format = DateTimePickerFormat.Custom
        DateTimePicker2.CustomFormat = "dd-MMM-yy"
        DateTimePicker2.Value = System.DateTime.Now.ToString("yyyy-MM-dd")

        DateTimePicker3.Format = DateTimePickerFormat.Custom
        DateTimePicker3.CustomFormat = "dd-MMM-yy"
        DateTimePicker3.Value = System.DateTime.Now.ToString("yyyy-MM-dd")

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If DateTimePicker1.Value > System.DateTime.Now.ToString("yyyy-MM-dd") Then
            MsgBox("ກະລຸນາເລືອກວັນທີທີ່ຕ້ອງການຄືນໃໝ່")
            Return
        End If
        da = New MySqlDataAdapter("select billID, billAmount, billDiscount, billDate, time, users.userName from bill join users on bill.userID = users.userID where bill.billDate = '" & DateTimePicker1.Value.ToString("yyyy-MM-dd") & "'", strcon)
        da.Fill(ds, "bill")
        ds.Clear()
        da.Fill(ds, "bill")


        Dim report As New CrystalReportDaily
        'report.SetParameterValue("date1", DateTimePicker1.Value.ToString("dd-MM-yyyy"))
        report.SetDataSource(ds)
        report.SetParameterValue("date1", DateTimePicker1.Value.ToString("dd-MM-yyyy"))
        CrystalReportViewer1.ReportSource = report
        CrystalReportViewer1.Refresh()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If DateTimePicker2.Value > System.DateTime.Now.ToString("yyyy-MM-dd") Or DateTimePicker3.Value > System.DateTime.Now.ToString("yyyy-MM-dd") Or DateTimePicker3.Value < DateTimePicker2.Value Then
            MsgBox("ກະລຸນາເລືອກວັນທີທີ່ຕ້ອງການຄືນໃໝ່")
            Return
        End If

        da = New MySqlDataAdapter("select billID, billAmount, billDiscount, billDate, time, users.userName from bill join users on bill.userID = users.userID where bill.billDate >= '" & DateTimePicker2.Value.ToString("yyyy-MM-dd") & "' and bill.billDate <= '" & DateTimePicker3.Value.ToString("yyyy-MM-dd") & "'", strcon)
        da.Fill(ds, "bill")
        ds.Clear()
        da.Fill(ds, "bill")


        Dim report As New CrystalReportCondition
        'report.SetParameterValue("date1", DateTimePicker1.Value.ToString("dd-MM-yyyy"))
        report.SetDataSource(ds)
        report.SetParameterValue("date1", DateTimePicker2.Value.ToString("dd-MM-yyyy"))
        report.SetParameterValue("date2", DateTimePicker3.Value.ToString("dd-MM-yyyy"))
        CrystalReportViewer1.ReportSource = report
        CrystalReportViewer1.Refresh()
    End Sub


End Class