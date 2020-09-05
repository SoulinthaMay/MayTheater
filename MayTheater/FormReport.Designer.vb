<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormReport
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ລາຍງານຍອດຂາຍToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ປະຈຳວນToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ເງອນໄຂToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.MenuStrip1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1019, 524)
        Me.Panel1.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.White
        Me.MenuStrip1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ລາຍງານຍອດຂາຍToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1019, 55)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ລາຍງານຍອດຂາຍToolStripMenuItem
        '
        Me.ລາຍງານຍອດຂາຍToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ປະຈຳວນToolStripMenuItem, Me.ເງອນໄຂToolStripMenuItem})
        Me.ລາຍງານຍອດຂາຍToolStripMenuItem.Font = New System.Drawing.Font("Noto Serif Lao", 14.0!)
        Me.ລາຍງານຍອດຂາຍToolStripMenuItem.Name = "ລາຍງານຍອດຂາຍToolStripMenuItem"
        Me.ລາຍງານຍອດຂາຍToolStripMenuItem.Size = New System.Drawing.Size(231, 51)
        Me.ລາຍງານຍອດຂາຍToolStripMenuItem.Text = "ລາຍງານຍອດຂາຍ"
        '
        'ປະຈຳວນToolStripMenuItem
        '
        Me.ປະຈຳວນToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(111, Byte), Integer), CType(CType(111, Byte), Integer), CType(CType(111, Byte), Integer))
        Me.ປະຈຳວນToolStripMenuItem.Name = "ປະຈຳວນToolStripMenuItem"
        Me.ປະຈຳວນToolStripMenuItem.Size = New System.Drawing.Size(252, 42)
        Me.ປະຈຳວນToolStripMenuItem.Text = "ປະຈຳວັນ"
        '
        'ເງອນໄຂToolStripMenuItem
        '
        Me.ເງອນໄຂToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(111, Byte), Integer), CType(CType(111, Byte), Integer), CType(CType(111, Byte), Integer))
        Me.ເງອນໄຂToolStripMenuItem.Name = "ເງອນໄຂToolStripMenuItem"
        Me.ເງອນໄຂToolStripMenuItem.Size = New System.Drawing.Size(252, 42)
        Me.ເງອນໄຂToolStripMenuItem.Text = "ເງື່ອນໄຂ"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.MenuStrip1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1019, 55)
        Me.Panel2.TabIndex = 1
        '
        'FormReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1019, 524)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "FormReport"
        Me.Text = "FormReport"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ລາຍງານຍອດຂາຍToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ປະຈຳວນToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ເງອນໄຂToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Panel2 As Panel
End Class
