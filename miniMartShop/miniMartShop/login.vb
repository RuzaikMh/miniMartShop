Imports MySql.Data.MySqlClient

Public Class login
    Public MySqlConnection As New MySqlConnection("host=127.0.0.1; port = 3306; user=root; password=; database=minimart")
    Public COMMAND As MySqlCommand



    Private Sub login_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            MySqlConnection.Open()

        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        MySqlConnection.Close()
        End
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim Query As String
        Query = "SELECT `username`, `password`, `accountType` FROM `login` WHERE `username` =  @username AND  `password` = @password"
        COMMAND = New MySqlCommand(Query, MySqlConnection)

        COMMAND.Parameters.Add("@username", MySqlDbType.VarChar).Value = TextBox1.Text
        COMMAND.Parameters.Add("@password", MySqlDbType.VarChar).Value = TextBox2.Text

        Dim adapter As New MySqlDataAdapter(COMMAND)
        Dim table As New DataTable()

        adapter.Fill(table)

        If TextBox1.Text.Equals("") Or TextBox1.Text.Equals("") Then
            MessageBox.Show("feild cannot empty...")

        ElseIf table.Rows.Count = 0 Then
            MessageBox.Show("Invalid Username or Password")

        Else
            clear()
            Main.Show()
        End If

    End Sub
    Private Sub clear()
        TextBox1.Clear()
        TextBox2.Clear()
    End Sub

End Class