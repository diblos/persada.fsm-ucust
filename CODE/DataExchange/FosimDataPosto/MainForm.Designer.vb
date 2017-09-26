<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ButtonSend = New System.Windows.Forms.Button()
        Me.RadioFTP = New System.Windows.Forms.RadioButton()
        Me.RadioWS = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.CheckBoxDB = New System.Windows.Forms.CheckBox()
        Me.RadioFC = New System.Windows.Forms.RadioButton()
        Me.RadioCA = New System.Windows.Forms.RadioButton()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ButtonSend)
        Me.GroupBox1.Controls.Add(Me.RadioFTP)
        Me.GroupBox1.Controls.Add(Me.RadioWS)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox1.Location = New System.Drawing.Point(0, 122)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(2051, 116)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Services"
        '
        'ButtonSend
        '
        Me.ButtonSend.Location = New System.Drawing.Point(460, 32)
        Me.ButtonSend.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ButtonSend.Name = "ButtonSend"
        Me.ButtonSend.Size = New System.Drawing.Size(650, 55)
        Me.ButtonSend.TabIndex = 6
        Me.ButtonSend.Text = "Send"
        Me.ButtonSend.UseVisualStyleBackColor = True
        '
        'RadioFTP
        '
        Me.RadioFTP.AutoSize = True
        Me.RadioFTP.Location = New System.Drawing.Point(328, 48)
        Me.RadioFTP.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RadioFTP.Name = "RadioFTP"
        Me.RadioFTP.Size = New System.Drawing.Size(83, 29)
        Me.RadioFTP.TabIndex = 5
        Me.RadioFTP.TabStop = True
        Me.RadioFTP.Text = "FTP"
        Me.RadioFTP.UseVisualStyleBackColor = True
        '
        'RadioWS
        '
        Me.RadioWS.AutoSize = True
        Me.RadioWS.Checked = True
        Me.RadioWS.Location = New System.Drawing.Point(88, 48)
        Me.RadioWS.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RadioWS.Name = "RadioWS"
        Me.RadioWS.Size = New System.Drawing.Size(165, 29)
        Me.RadioWS.TabIndex = 4
        Me.RadioWS.TabStop = True
        Me.RadioWS.Text = "Web Service"
        Me.RadioWS.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.ListBox1)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox2.Location = New System.Drawing.Point(0, 238)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Size = New System.Drawing.Size(2051, 464)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Verbose"
        '
        'ListBox1
        '
        Me.ListBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 25
        Me.ListBox1.Location = New System.Drawing.Point(4, 28)
        Me.ListBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(2043, 432)
        Me.ListBox1.TabIndex = 1
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.CheckBoxDB)
        Me.GroupBox3.Controls.Add(Me.RadioFC)
        Me.GroupBox3.Controls.Add(Me.RadioCA)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox3.Size = New System.Drawing.Size(2051, 116)
        Me.GroupBox3.TabIndex = 5
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Data"
        '
        'CheckBoxDB
        '
        Me.CheckBoxDB.AutoSize = True
        Me.CheckBoxDB.Location = New System.Drawing.Point(916, 49)
        Me.CheckBoxDB.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CheckBoxDB.Name = "CheckBoxDB"
        Me.CheckBoxDB.Size = New System.Drawing.Size(178, 29)
        Me.CheckBoxDB.TabIndex = 2
        Me.CheckBoxDB.Text = "Read from DB"
        Me.CheckBoxDB.UseVisualStyleBackColor = True
        '
        'RadioFC
        '
        Me.RadioFC.AutoSize = True
        Me.RadioFC.Location = New System.Drawing.Point(460, 48)
        Me.RadioFC.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RadioFC.Name = "RadioFC"
        Me.RadioFC.Size = New System.Drawing.Size(262, 29)
        Me.RadioFC.TabIndex = 1
        Me.RadioFC.TabStop = True
        Me.RadioFC.Text = "Food Code Master File"
        Me.RadioFC.UseVisualStyleBackColor = True
        '
        'RadioCA
        '
        Me.RadioCA.AutoSize = True
        Me.RadioCA.Checked = True
        Me.RadioCA.Location = New System.Drawing.Point(63, 48)
        Me.RadioCA.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RadioCA.Name = "RadioCA"
        Me.RadioCA.Size = New System.Drawing.Size(363, 29)
        Me.RadioCA.TabIndex = 0
        Me.RadioCA.TabStop = True
        Me.RadioCA.Text = "Consignment Approval Response"
        Me.RadioCA.UseVisualStyleBackColor = True
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(2051, 702)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "MainForm"
        Me.Text = "Form1"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonSend As System.Windows.Forms.Button
    Friend WithEvents RadioFTP As System.Windows.Forms.RadioButton
    Friend WithEvents RadioWS As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioFC As System.Windows.Forms.RadioButton
    Friend WithEvents RadioCA As System.Windows.Forms.RadioButton
    Friend WithEvents CheckBoxDB As System.Windows.Forms.CheckBox

End Class
