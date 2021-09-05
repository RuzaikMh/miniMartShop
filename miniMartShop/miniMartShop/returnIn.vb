Imports MySql.Data.MySqlClient
Public Class returnIn
    Public con As New MySqlConnection
    Public cmd As MySqlCommand
    Public id As Integer

    Private Sub returnIn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con.ConnectionString = "host=127.0.0.1; port = 3306; user=root; password=; database=minimart"
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()

        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "Select * from sales where BillNumber = '" + TextBox2.Text + "'"
        cmd.ExecuteNonQuery()
        Dim dt As New DataTable()
        Dim da As New MySqlDataAdapter(cmd) '
        da.Fill(dt)
        Dim dr As MySqlDataReader
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

        While dr.Read
            TextBox3.Text = dr.GetString(2).ToString()
            TextBox8.Text = dr.GetString(3).ToString()
            TextBox1.Text = dr.GetString(7).ToString()
            TextBox4.Text = dr.GetString(8).ToString()
        End While

        cmd.Connection.Close()

        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()

        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "Select * from stock where id = '" + TextBox8.Text + "'"
        cmd.ExecuteNonQuery()
        Dim dt1 As New DataTable()
        Dim da1 As New MySqlDataAdapter(cmd) '
        da1.Fill(dt1)
        Dim dr1 As MySqlDataReader
        dr1 = cmd.ExecuteReader(CommandBehavior.CloseConnection)

        While dr1.Read
            TextBox7.Text = dr1.GetString(1).ToString()
            TextBox5.Text = dr1.GetString(4).ToString()
            TextBox6.Text = dr1.GetString(5).ToString()
        End While

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()

        If TextBox2.Text.Equals("") Or TextBox8.Text.Equals("") Then
            MessageBox.Show("Insert valid Bill number and Search first...")

        ElseIf Convert.ToInt32(TextBox9.Text) > Convert.ToInt32(TextBox4.Text) Then
            MessageBox.Show("Cannot retrun more than sold....")

        Else
            cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "INSERT INTO `returnin`(`BillNumber`, `SalesDate`, `SoldPrice`, `SoldQuantity`, `ItemNo`, `ReturnDate`, `ReturnQty`) 
            VALUES ('" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox1.Text + "','" + TextBox4.Text + "','" + TextBox8.Text + "','" + DateTimePicker1.Text + "','" + TextBox9.Text + "')"

            cmd.ExecuteNonQuery()

            Dim qty As Integer

            qty = Convert.ToInt32(TextBox6.Text) + Convert.ToInt32(TextBox9.Text)

            cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "update stock set qty = " & qty & " where id = '" + TextBox8.Text + "'"

            cmd.ExecuteNonQuery()

            MessageBox.Show("Record Added Successfuly")

        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox1.Text = ""
        TextBox4.Text = ""
        TextBox8.Text = ""
        TextBox7.Text = ""
        TextBox6.Text = ""
        TextBox5.Text = ""
        TextBox9.Text = ""
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Me.Close()
        Main.Show()
    End Sub
End Class