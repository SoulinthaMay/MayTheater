Imports MySql.Data.MySqlClient
Public Class FormTicket
    Dim strcon As New MySqlConnection
    Dim da As New MySqlDataAdapter
    Dim ds As New DataSet

    Public billID As String = ""
    Private Sub FormTicket_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        strcon = New MySqlConnection(DBModule.strcon)
        strcon.Open()

        da = New MySqlDataAdapter("select movie.movieName, movie.movieLang, programe.proTime, programe.proDate, programe.proRoom, concat(seat.letter, seat.number) as seatNo from selldetail join bill on selldetail.billID = bill.billID join programe on programe.proID = selldetail.proID join movie on programe.movieID = movie.movieID join seat on selldetail.seatID = seat.seatID where bill.billID = '" & billID & "'", strcon)
        da.Fill(ds, "bill")
        ds.Clear()
        da.Fill(ds, "bill")

        Dim report As New CrystalReport123
        report.SetDataSource(ds.Tables("bill"))
        CrystalReportViewer1.ReportSource = report
        CrystalReportViewer1.Refresh()
    End Sub
End Class