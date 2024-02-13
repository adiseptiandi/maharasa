Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub KASIRToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KASIRToolStripMenuItem.Click
        Form2.Show()
        Me.Hide()
    End Sub

    Private Sub KELUARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KELUARToolStripMenuItem.Click
        Dim result As DialogResult = MessageBox.Show("Yakin ingin menutup aplikasi?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            End
        End If
    End Sub

    Private Sub ABOUTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ABOUTToolStripMenuItem.Click
        Form3.Show()
        Me.Hide()
    End Sub
End Class
