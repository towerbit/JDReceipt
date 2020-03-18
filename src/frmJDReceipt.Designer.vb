<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmJDReceipt
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意:  以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmJDReceipt))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.txtUrl = New System.Windows.Forms.ToolStripTextBox()
        Me.btnGo = New System.Windows.Forms.ToolStripButton()
        Me.btnParse = New System.Windows.Forms.ToolStripButton()
        Me.btnDetailPage = New System.Windows.Forms.ToolStripButton()
        Me.btnMainPage = New System.Windows.Forms.ToolStripButton()
        Me.wb = New System.Windows.Forms.WebBrowser()
        Me.wb2 = New System.Windows.Forms.WebBrowser()
        Me.btnChangeTitle = New System.Windows.Forms.ToolStripButton()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.txtUrl, Me.btnGo, Me.btnParse, Me.btnDetailPage, Me.btnMainPage, Me.btnChangeTitle})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1605, 27)
        Me.ToolStrip1.TabIndex = 0
        '
        'txtUrl
        '
        Me.txtUrl.Name = "txtUrl"
        Me.txtUrl.Size = New System.Drawing.Size(900, 27)
        Me.txtUrl.Text = "https://myivc.jd.com/fpzz/index.action"
        '
        'btnGo
        '
        Me.btnGo.Image = CType(resources.GetObject("btnGo.Image"), System.Drawing.Image)
        Me.btnGo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(54, 24)
        Me.btnGo.Text = "Go"
        '
        'btnParse
        '
        Me.btnParse.Image = CType(resources.GetObject("btnParse.Image"), System.Drawing.Image)
        Me.btnParse.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnParse.Name = "btnParse"
        Me.btnParse.Size = New System.Drawing.Size(147, 24)
        Me.btnParse.Text = "Auto Download"
        '
        'btnDetailPage
        '
        Me.btnDetailPage.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnDetailPage.CheckOnClick = True
        Me.btnDetailPage.Image = CType(resources.GetObject("btnDetailPage.Image"), System.Drawing.Image)
        Me.btnDetailPage.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDetailPage.Name = "btnDetailPage"
        Me.btnDetailPage.Size = New System.Drawing.Size(115, 24)
        Me.btnDetailPage.Text = "Detail Page"
        '
        'btnMainPage
        '
        Me.btnMainPage.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnMainPage.Checked = True
        Me.btnMainPage.CheckOnClick = True
        Me.btnMainPage.CheckState = System.Windows.Forms.CheckState.Checked
        Me.btnMainPage.Image = CType(resources.GetObject("btnMainPage.Image"), System.Drawing.Image)
        Me.btnMainPage.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnMainPage.Name = "btnMainPage"
        Me.btnMainPage.Size = New System.Drawing.Size(109, 24)
        Me.btnMainPage.Text = "Main Page"
        '
        'wb
        '
        Me.wb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wb.Location = New System.Drawing.Point(0, 27)
        Me.wb.MinimumSize = New System.Drawing.Size(20, 20)
        Me.wb.Name = "wb"
        Me.wb.ScriptErrorsSuppressed = True
        Me.wb.Size = New System.Drawing.Size(1605, 637)
        Me.wb.TabIndex = 1
        '
        'wb2
        '
        Me.wb2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wb2.Location = New System.Drawing.Point(0, 0)
        Me.wb2.MinimumSize = New System.Drawing.Size(20, 20)
        Me.wb2.Name = "wb2"
        Me.wb2.Size = New System.Drawing.Size(1605, 664)
        Me.wb2.TabIndex = 2
        '
        'btnChangeTitle
        '
        Me.btnChangeTitle.Image = CType(resources.GetObject("btnChangeTitle.Image"), System.Drawing.Image)
        Me.btnChangeTitle.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnChangeTitle.Name = "btnChangeTitle"
        Me.btnChangeTitle.Size = New System.Drawing.Size(124, 24)
        Me.btnChangeTitle.Text = "Change Title"
        '
        'frmJDReceipt
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1605, 664)
        Me.Controls.Add(Me.wb)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.wb2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmJDReceipt"
        Me.Text = "JDReceipter"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents txtUrl As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents btnGo As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnParse As System.Windows.Forms.ToolStripButton
    Friend WithEvents wb As WebBrowser
    Friend WithEvents wb2 As WebBrowser
    Friend WithEvents btnDetailPage As ToolStripButton
    Friend WithEvents btnMainPage As ToolStripButton
    Friend WithEvents btnChangeTitle As ToolStripButton
End Class
