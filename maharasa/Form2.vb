Imports System.IO
Imports System.Windows.Forms


Public Class Form2
    Dim listPesanan As New List(Of String) 'List Pesanan
    Dim foundPatokan As Boolean = False
    'Procedure untuk Set Tanggal dan format Nota
    Private Sub SetTanggalDanNota()
        Ttanggal.Text = Today
        Dim coba As String = DateString & TimeOfDay
        Tnota.Text = coba.Replace(":", ".").Replace("-", ".").Replace("PM", "").Replace("AM", "").Replace(" ", "")
    End Sub
    'Ketika Form2 dibuka
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetTanggalDanNota()
        Ttanggal.Enabled = False
        Tnota.Enabled = False
        Hmie.Enabled = False
        Hayam.Enabled = False
        Hkacang.Enabled = False
        Hkopi.Enabled = False
        Tharga.Enabled = False
        Tkembalian.Enabled = False
    End Sub
    'Input Nama Pemesan
    Private Sub Tnama_KeyDown(sender As Object, e As KeyEventArgs) Handles Tnama.KeyDown
        If e.KeyCode = Keys.Enter Then
            Tmie.Focus()
        End If
    End Sub
    'Procedure untuk Menghitung Total Harga
    Private Sub HitungTotalHarga()
        Dim totalHarga As Integer = Val(Hmie.Text) + Val(Hayam.Text) + Val(Hkacang.Text) + Val(Hkopi.Text)
        Tharga.Text = totalHarga.ToString()
    End Sub
    'Input Pesanan Mie
    Private Sub Tmie_KeyDown(sender As Object, e As KeyEventArgs) Handles Tmie.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Not String.IsNullOrEmpty(Tmie.Text) Then
                Hmie.Text = Tmie.Text * 12000
            Else
                Hmie.Text = ""
            End If
            HitungTotalHarga()
            Tayam.Focus()
        End If
    End Sub
    'Input Pesanan Bubur Ayam
    Private Sub Tayam_KeyDown(sender As Object, e As KeyEventArgs) Handles Tayam.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Not String.IsNullOrEmpty(Tayam.Text) Then
                Hayam.Text = Tayam.Text * 6000
            Else
                Hayam.Text = ""
            End If
            HitungTotalHarga()
            Tkacang.Focus()
        End If
    End Sub
    'Input Pesanan Bubur Kacang
    Private Sub Tkacang_KeyDown(sender As Object, e As KeyEventArgs) Handles Tkacang.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Not String.IsNullOrEmpty(Tkacang.Text) Then
                Hkacang.Text = Tkacang.Text * 6000
            Else
                Hkacang.Text = ""
            End If
            HitungTotalHarga()
            Tkopi.Focus()
        End If
    End Sub
    'Input Pesanan Kopi/Susu
    Private Sub Tkopi_KeyDown(sender As Object, e As KeyEventArgs) Handles Tkopi.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Not String.IsNullOrEmpty(Tkopi.Text) Then
                Hkopi.Text = Tkopi.Text * 3000
            Else
                Hkopi.Text = ""
            End If
            Tharga.Focus()
            HitungTotalHarga()
            Tbayar.Focus()
        End If
    End Sub
    'Input Jumlah Pembayaran
    Private Sub Tbayar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tbayar.KeyPress
        If e.KeyChar = Chr(13) Then
            If Val(Tbayar.Text) < Val(Tharga.Text) Then
                MsgBox("Uang Tidak Cukup", vbInformation + vbOKOnly, "Informasi")
            Else
                Tkembalian.Text = FormatCurrency(Val(Tbayar.Text) - Val(Tharga.Text)).Replace("$", "Rp")
                MsgBox("Pembayaran Berhasil", vbInformation + vbOKOnly, "Informasi")
            End If
        End If
    End Sub
    'Procedure untuk Update List
    Private Sub AddOrUpdateItemToList(item As String)
        ' Cek apakah pesanan sudah ada dalam list
        Dim existingIndex As Integer = IsItemInList(item)

        If existingIndex <> -1 Then
            ' Jika pesanan sudah ada, list
            listPesanan.RemoveAt(existingIndex)
        End If

        ' Tambahkan pesanan yang baru ke list
        listPesanan.Add(item)
    End Sub
    'Procedure untuk Mengecek Item List apakah ada yang sama
    Private Function IsItemInList(item As String) As Integer
        ' Temukan indeks item yang sudah ada dalam list
        Return listPesanan.FindIndex(Function(existingItem) existingItem.StartsWith(item.Split(vbTab)(0)))
    End Function
    'Tekan Tombol TAMPILKAN
    Private Sub BtnTampilkan_Click(sender As Object, e As EventArgs) Handles BtnTampilkan.Click
        ListBox1.Items.Clear()
        HitungTotalHarga()
        'Menambahkan data pesanan ke listPesanan
        If Not String.IsNullOrEmpty(Tmie.Text) Then
            Hmie.Text = Tmie.Text * 12000
            Dim pesananMie As String = "Mie Goreng/Kuah	12000	" + Tmie.Text + "	" + Hmie.Text
            AddOrUpdateItemToList(pesananMie)
        End If

        If Not String.IsNullOrEmpty(Tayam.Text) Then
            Hayam.Text = Tayam.Text * 6000
            Dim pesananAyam As String = "Bubur Ayam	6000	" + Tayam.Text + "	" + Hayam.Text
            AddOrUpdateItemToList(pesananAyam)
        End If

        If Not String.IsNullOrEmpty(Tkacang.Text) Then
            Hkacang.Text = Tkacang.Text * 6000
            Dim pesananKacang As String = "Bubur Kacang	6000	" + Tkacang.Text + "	" + Hkacang.Text
            AddOrUpdateItemToList(pesananKacang)
        End If

        If Not String.IsNullOrEmpty(Tkopi.Text) Then
            Hkopi.Text = Tkopi.Text * 3000
            Dim pesananKopi As String = "Kopi/Susu	3000	" + Tkopi.Text + "	" + Hkopi.Text
            AddOrUpdateItemToList(pesananKopi)
        End If

        'Tampilkan Struk
        ListBox1.Items.Add("	   Warkop Maharasa")
        ListBox1.Items.Add("=====================================")
        ListBox1.Items.Add("No. Nota	: " + Tnota.Text)
        ListBox1.Items.Add("Tanggal		: " + Ttanggal.Text)
        ListBox1.Items.Add("Nama Pemesan	: " + Tnama.Text)
        ListBox1.Items.Add("-------------------------------------")
        ListBox1.Items.Add("Pesanan		Harga	Qty	Total")
        ListBox1.Items.Add("-------------------------------------")
        For Each pesanan As String In listPesanan
            ListBox1.Items.Add(pesanan)
        Next
        ListBox1.Items.Add("-------------------------------------")
        ListBox1.Items.Add("Total Harga	: " + Tharga.Text)
        ListBox1.Items.Add("Jumlah Bayar	: " + Tbayar.Text)
        ListBox1.Items.Add("Kembalian	: " + Tkembalian.Text)
        ListBox1.Items.Add("=====================================")
        ListBox1.Items.Add("	    Terima Kasih")
    End Sub
    'Tekan Tombol BARU
    Private Sub BtnBaru_Click(sender As Object, e As EventArgs) Handles BtnBaru.Click
        SetTanggalDanNota()
        Tmie.Text = ""
        Tayam.Text = ""
        Tkacang.Text = ""
        Tkopi.Text = ""
        Hmie.Text = ""
        Hayam.Text = ""
        Hkacang.Text = ""
        Hkopi.Text = ""
        Tharga.Text = ""
        Tbayar.Text = ""
        Tkembalian.Text = ""
        Tnama.Text = ""
        listPesanan.Clear()
        ListBox1.Items.Clear()
        Tnama.Focus()
    End Sub
    'Strip Menu Kembali ke menu utama
    Private Sub KembaliKeMenuUtamaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KembaliKeMenuUtamaToolStripMenuItem.Click
        Form1.Show()
        Me.Hide()
    End Sub
    'Tekan Tombol PRINT
    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        ' Munculkan dialog untuk memilih folder tujuan
        Dim folderBrowserDialog As New FolderBrowserDialog()
        folderBrowserDialog.Description = "Pilih folder untuk menyimpan file"

        ' Jika pengguna memilih folder dan menekan tombol OK
        If folderBrowserDialog.ShowDialog() = DialogResult.OK Then
            ' Tentukan nama file untuk menyimpan konten ListBox
            Dim fileOutput As String = Path.Combine(folderBrowserDialog.SelectedPath, "pesanan" + Tnota.Text + ".txt")

            ' Buat instance StreamWriter untuk menulis ke file
            Using writer As New StreamWriter(fileOutput)
                ' Tulis setiap item dalam ListBox ke file
                For Each item As String In ListBox1.Items
                    writer.WriteLine(item)
                Next
            End Using

            ' Tampilkan pesan berhasil setelah selesai
            MessageBox.Show("Pesanan berhasil disimpan ke file.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
    'Procedure untuk mengatur nilai TextBox dari File txt
    Private Sub SetValueFromLine(line As String, patokan As String, targetTextBox As Windows.Forms.TextBox)
        ' Cek apakah baris mengandung patokan yang diinginkan
        If line.StartsWith(patokan & ":") Then
            ' Pisahkan nilai yang diinginkan
            Dim value As String = line.Replace(patokan & ":", "")

            ' Masukkan nilai ke dalam TextBox target
            targetTextBox.Text = value.Trim()

            ' Set variabel foundPatokan menjadi True setelah menemukan patokan yang diinginkan
            foundPatokan = True
        End If
    End Sub
    'Procedure untuk mengatur nilai TextBox(khusus pesanan) dari File txt
    Private Sub SetValueFromLine2(line As String, patokan As String, targetTextBox1 As Windows.Forms.TextBox, targetTextBox2 As Windows.Forms.TextBox)
        ' Cek apakah baris mengandung patokan yang diinginkan
        If line.StartsWith(patokan) Then
            ' Pisahkan nilai yang diinginkan
            Dim value As String = line.Replace(patokan, "")
            Dim pisah() As String = value.Split(ControlChars.Tab)

            ' Masukkan nilai ke dalam TextBox target
            targetTextBox1.Text = pisah(1).Trim()
            targetTextBox2.Text = pisah(2).Trim()

            ' Set variabel foundPatokan menjadi True setelah menemukan patokan yang diinginkan
            foundPatokan = True
        End If
    End Sub
    'Tekan Tombol OPEN
    Private Sub BtnOpen_Click(sender As Object, e As EventArgs) Handles BtnOpen.Click
        ' Tampilkan dialog untuk memilih file
        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.Title = "Pilih File"

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            ' Ambil nama file yang dipilih oleh pengguna
            Dim namaFile As String = openFileDialog.FileName

            ' Buat instance StreamReader untuk membaca file
            Using reader As New StreamReader(namaFile)
                ' Baca baris-baris dari file
                Dim line As String
                Dim foundPatokan As Boolean = False
                While Not reader.EndOfStream
                    line = reader.ReadLine()

                    ' Panggil prosedur SetValueFromLine untuk mengisi tiap TextBox
                    SetValueFromLine(line, "No. Nota	", Tnota)

                    SetValueFromLine(line, "Tanggal		", Ttanggal)

                    SetValueFromLine(line, "Nama Pemesan	", Tnama)

                    SetValueFromLine2(line, "Mie Goreng/Kuah	", Tmie, Hmie)

                    SetValueFromLine2(line, "Bubur Ayam	", Tayam, Hayam)

                    SetValueFromLine2(line, "Bubur Kacang	", Tkacang, Hkacang)

                    SetValueFromLine2(line, "Kopi/Susu	", Tkopi, Hkopi)

                    SetValueFromLine(line, "Total Harga	", Tharga)

                    SetValueFromLine(line, "Jumlah Bayar	", Tbayar)

                    SetValueFromLine(line, "Kembalian	", Tkembalian)
                    If foundPatokan Then
                        Exit While
                    End If
                End While
            End Using
        End If
    End Sub
End Class