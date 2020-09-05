Imports MySql.Data.MySqlClient
Public Class FrmSeat

        Dim strcon As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim da As New MySqlDataAdapter
        Dim ds As New DataSet

        Dim frmShow As New FormShowTicket

        Public proID = ""
        Public movie = ""
        Public user As String = ""
        Public seat As Integer = 0

    Dim availableNormal As New System.Drawing.Bitmap(My.Resources.chair)
    Dim chooseNormal As New System.Drawing.Bitmap(My.Resources.chairChoose)
    Dim fullNormal As New System.Drawing.Bitmap(My.Resources.fullnormal)

    Dim availableHoneymoon As New System.Drawing.Bitmap(My.Resources.NewHoney)
    Dim chooseHoneymoon As New System.Drawing.Bitmap(My.Resources.honeyChoose)
    Dim fullHoneymoon As New System.Drawing.Bitmap(My.Resources.fullHoney)

    Dim p As Integer
        Dim normal As Integer
        Dim honey As Integer


        Private Sub FrmSeat_Load(sender As Object, e As EventArgs) Handles MyBase.Load

            strcon = New MySqlConnection(DBModule.strcon)
            strcon.Open()

        Label5.Text = Me.seat
        Panel1.Left = pnScheme.Width / 2 - (Panel1.Width / 2)
        pnNormal.Left = pnScheme.Width / 2 - (pnNormal.Width / 2) + 10
        pnHoneymoon.Left = pnScheme.Width / 2 - (pnHoneymoon.Width / 2) + 10

        Panel5.Left = pnNormal.Left

            PictureBox145.SizeMode = PictureBoxSizeMode.StretchImage
            PictureBox146.SizeMode = PictureBoxSizeMode.StretchImage

            ShowData()
            ShowPrice()
        End Sub

        Sub ShowData()
            Me.pnScheme.Controls.Remove(frmShow)

            Dim c As Control
            Dim j As Integer


            da = New MySqlDataAdapter("select seatID from selldetail where proID = '" & proID & "' and typeID = '2'", strcon)
            da.Fill(ds, "fullhoney")
            ds.Clear()
            da.Fill(ds, "fullhoney")


            For Each c In pnHoneymoon.Controls
                If TypeOf (c) Is PictureBox Then
                    j = pnHoneymoon.Controls.Count
                    CType(c, PictureBox).SizeMode = PictureBoxSizeMode.StretchImage
                    Dim name As String = Mid(CType(c, PictureBox).Name, 11)
                    CType(c, PictureBox).Image = availableHoneymoon
                    For h As Integer = 0 To ds.Tables("fullhoney").Rows.Count - 1
                        If name = ds.Tables("fullhoney").Rows(h)(0) Then
                            CType(c, PictureBox).Image = fullHoneymoon
                        End If
                    Next

                    AddHandler c.Click, AddressOf PictureBox1_Click
                End If
            Next

            da = New MySqlDataAdapter("select seatID from selldetail where proID = '" & proID & "' and typeID = '1'", strcon)
            da.Fill(ds, "fullnormal")
            ds.Clear()
            da.Fill(ds, "fullnormal")

            For Each c In pnNormal.Controls
                If TypeOf (c) Is PictureBox Then
                    j = pnNormal.Controls.Count
                    CType(c, PictureBox).SizeMode = PictureBoxSizeMode.StretchImage
                    Dim name As String = Mid(CType(c, PictureBox).Name, 11)

                    CType(c, PictureBox).Image = availableNormal
                    For n As Integer = 0 To ds.Tables("fullnormal").Rows.Count - 1
                        If name = ds.Tables("fullnormal").Rows(n)(0) Then
                            CType(c, PictureBox).Image = fullNormal
                        End If

                    Next

                    AddHandler c.Click, AddressOf PictureBox55_Click
                End If
            Next

        End Sub

        Sub ShowPrice()

            da = New MySqlDataAdapter("select moviePrice from movie where movieID = '" & movie & "'", strcon)
            da.Fill(ds, "price")
            ds.Clear()
            da.Fill(ds, "price")
            p = ds.Tables("price").Rows(0)(0)

            da = New MySqlDataAdapter("select plusPrice from seattype", strcon)
            da.Fill(ds, "plus")
            ds.Clear()
            da.Fill(ds, "plus")
            normal = ds.Tables("plus").Rows(0)(0)
            honey = ds.Tables("plus").Rows(1)(0)

            lbNormalPrice.Text = Format(p + normal, "###,###") + " ກີບ"
            lbHoneyPrice.Text = Format(p + honey, "###,###") + " ກີບ"
        End Sub

        Dim count1 As Integer = 0
        Dim count2 As Integer = 0

        Dim rest As Integer = 0
        Dim what As Integer = 0
        Private Sub PictureBox1_Click(sender As Object, e As EventArgs)
            Dim nor As Integer = Me.seat - count2
            If CType(sender, PictureBox).Image Is availableHoneymoon Then
                Dim nc As Integer = 0
                Int32.Parse(Label2.Text, nc)

                If count1 = nor Then
                    Return
                End If

                CType(sender, PictureBox).Image = chooseHoneymoon

                count1 += 1
                Label1.Text = nor
            Label3.Text = count2 + count1


            Dim seat As Integer = Mid(CType(sender, PictureBox).Name, 11)
                nor = nor - count1
                'nor = 5 - 1 = 4
                Dim honeycount = 0
                If nor > 0 Then
                    For Each c In pnHoneymoon.Controls
                        'i = 36 to 40
                        For i As Integer = seat To seat + nor
                            If TypeOf (c) Is PictureBox Then
                                If Mid(CType(c, PictureBox).Name, 11) = i Then
                                    If CType(c, PictureBox).Image Is availableHoneymoon Then
                                        CType(c, PictureBox).Image = chooseHoneymoon
                                        count1 += 1
                                    'count = 2
                                    Label3.Text = count2 + count1
                                    Label1.Text = count1

                                    ElseIf CType(c, PictureBox).Image Is fullHoneymoon Then
                                        honeycount += 1
                                        'honeycount = 1
                                    End If

                                End If
                            End If
                        Next
                    Next
                    For Each c In pnHoneymoon.Controls
                        'j = 40 to 42
                        'count1
                        For j As Integer = seat + nor To seat + nor + honeycount
                            If TypeOf (c) Is PictureBox Then
                                If Mid(CType(c, PictureBox).Name, 11) = j Then
                                    If CType(c, PictureBox).Image Is availableHoneymoon Then
                                        CType(c, PictureBox).Image = chooseHoneymoon
                                        count1 += 1
                                    Label1.Text = count1
                                    Label3.Text = count1 + count2
                                End If
                                End If
                            End If
                        Next
                    Next

                End If
                ' rest = nor


            ElseIf CType(sender, PictureBox).Image Is chooseHoneymoon Then
                CType(sender, PictureBox).Image = availableHoneymoon
                count1 -= 1
            Label1.Text = count1
            Label3.Text = count2 + count1

        ElseIf CType(sender, PictureBox).Image Is fullHoneymoon Then

            End If
        End Sub

        Private Sub PictureBox55_Click(sender As Object, e As EventArgs)
            'Dim nor2 As Integer = Me.seat - count1
            Dim nor2 As Integer = Me.seat - count1
            If CType(sender, PictureBox).Image Is availableNormal Then

                If count2 = nor2 Then
                    Return
                End If

                'Label3.Text = nor2
                CType(sender, PictureBox).Image = chooseNormal

                count2 += 1
            Label2.Text = count2
            Label3.Text = count2 + count1



            Dim seat As Integer = Mid(CType(sender, PictureBox).Name, 11)
                nor2 = nor2 - count2
                Dim normalcount = 0
                If nor2 > 0 Then
                    For Each c In pnNormal.Controls
                        For i As Integer = seat To seat + nor2
                            If TypeOf (c) Is PictureBox Then
                                If Mid(CType(c, PictureBox).Name, 11) = i Then
                                    If CType(c, PictureBox).Image Is availableNormal Then
                                        CType(c, PictureBox).Image = chooseNormal
                                        count2 += 1
                                    Label2.Text = count2
                                    Label3.Text = count2 + count1
                                ElseIf CType(c, PictureBox).Image Is fullNormal Then
                                        normalcount += 1
                                    End If

                                End If
                            End If
                        Next
                    Next
                    For Each c In pnNormal.Controls
                        For j As Integer = seat + nor2 To seat + nor2 + normalcount
                            If TypeOf (c) Is PictureBox Then
                                If Mid(CType(c, PictureBox).Name, 11) = j Then
                                    If CType(c, PictureBox).Image Is availableNormal Then
                                        CType(c, PictureBox).Image = chooseNormal
                                        count2 += 1
                                    Label2.Text = count2
                                    Label3.Text = count2 + count1
                                End If
                                End If
                            End If
                        Next
                    Next

                End If



            ElseIf CType(sender, PictureBox).Image Is chooseNormal Then
                CType(sender, PictureBox).Image = availableNormal
                count2 -= 1
                Label2.Text = count2
            Label3.Text = count2 + count1
        ElseIf CType(sender, PictureBox).Image Is fullNormal Then

            End If

        End Sub

        Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

            If count1 + count2 < Me.seat Then
                MsgBox("ເລືອກບ່ອນນັ່ງໃຫ້ຄົບຕາມຈຳນວນ")
                Return
            End If


            Dim c As Control
            Dim Seat As Integer
            Dim hSelect As Boolean = False
            Dim nSelect As Boolean = False

            Dim choose As String
            Dim seatNo As String = ""
            Dim seatHoney As String = ""
            Dim seatNormal As String = ""

            Dim amount1 As Integer = 0
            Dim amount2 As Integer = 0
            Dim total As Integer = 0

            Dim arrH As New ArrayList
            Dim arrN As New ArrayList

            Dim a As Integer = 0

            For Each c In pnHoneymoon.Controls
                If TypeOf (c) Is PictureBox Then
                    If CType(c, PictureBox).Image Is chooseHoneymoon Then
                        a = a + 1
                        Seat = Mid(CType(c, PictureBox).Name, 11)
                        choose = "Select letter, number from seat where seatID = '" & Seat & "'"
                        da = New MySqlDataAdapter(choose, strcon)
                        da.Fill(ds, "seat")
                        ds.Tables.Clear()
                        da.Fill(ds, "seat")

                        arrH.Add(Seat)

                    End If
                    AddHandler c.Click, AddressOf PictureBox1_Click
                End If
            Next
            Dim honeyprice As Integer = p + honey
            amount1 = a * honeyprice

            Dim b As Integer = 0
            For Each c In pnNormal.Controls
                If TypeOf (c) Is PictureBox Then
                    If CType(c, PictureBox).Image Is chooseNormal Then
                        b = b + 1
                        Seat = Mid(CType(c, PictureBox).Name, 11)

                        choose = "Select letter, number from seat where seatID = '" & Seat & "' Order by letter DESC"

                        da = New MySqlDataAdapter(choose, strcon)
                        da.Fill(ds, "seat")
                        ds.Tables.Clear()
                        da.Fill(ds, "seat")

                        arrN.Add(Seat)


                    End If
                    AddHandler c.Click, AddressOf PictureBox55_Click
                End If
            Next

            Dim normalprice As Integer = p + normal
            amount2 = b * normalprice
            total = amount1 + amount2

            For Each c In pnHoneymoon.Controls
                If TypeOf (c) Is PictureBox Then
                    If CType(c, PictureBox).Image Is chooseHoneymoon Then
                        hSelect = True

                        Exit For
                    End If
                    AddHandler c.Click, AddressOf PictureBox1_Click
                End If
            Next

            For Each c In pnNormal.Controls
                If TypeOf (c) Is PictureBox Then
                    If CType(c, PictureBox).Image Is chooseNormal Then
                        nSelect = True

                        Exit For
                    End If
                    AddHandler c.Click, AddressOf PictureBox55_Click
                End If
            Next

            If hSelect = True Or nSelect = True Then

                frmShow.honey = a
                frmShow.normal = b
                frmShow.honeyamount = amount1
                frmShow.normalamount = amount2
                frmShow.total = total
                frmShow.arrHoney = arrH
                frmShow.arrNormal = arrN
                frmShow.user = user

                frmShow.honeyprice = honeyprice
                frmShow.normalprice = normalprice

                frmShow.proID = proID
                frmShow.movieID = movie

                frmShow.TopLevel = False

                Me.pnScheme.Controls.Add(frmShow)
                frmShow.BringToFront()
                frmShow.Height = Me.Height
                frmShow.Width = Me.Width
                frmShow.Show()

            End If


            If hSelect = False And nSelect = False Then
                MsgBox("ເລືອກຢ່າງໜ້ອຍ 1 ບ່ອນ", MsgBoxStyle.Exclamation)

            End If

        End Sub

    End Class