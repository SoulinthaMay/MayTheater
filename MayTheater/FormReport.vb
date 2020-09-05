Public Class FormReport
    Dim frmShow As New FormSellReport
    Private Sub ປະຈຳວນToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ປະຈຳວນToolStripMenuItem.Click
        frmShow.TopLevel = False
        frmShow.Panel2.Width = Panel1.Width
        Me.Panel1.Controls.Add(frmShow)
        frmShow.Show()
    End Sub

    Private Sub ເງອນໄຂToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ເງອນໄຂToolStripMenuItem.Click
        frmShow.TopLevel = False
        frmShow.Panel3.Width = Panel1.Width
        frmShow.Panel2.Width = 0
        Me.Panel1.Controls.Add(frmShow)
        frmShow.Show()
    End Sub
End Class