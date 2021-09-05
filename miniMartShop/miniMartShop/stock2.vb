Imports MySql.Data.MySqlClient
Public Class stock2

    Public con As New MySqlConnection
    Public cmd As MySqlCommand
    Public id As Integer

    Private Sub stock2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con.ConnectionString = "host=127.0.0.1; port = 3306; user=root; password=; database=minimart"
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()

        Dis_data()
    End Sub

    Public Sub Dis_data()

        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()

        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "Select * from stock"
        cmd.ExecuteNonQuery()
        Dim dt As New DataTable()
        Dim da As New MySqlDataAdapter(cmd) '
        da.Fill(dt)
        DataGridView1.DataSource = dt
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()

        If TextBox1.Text.Equals("") Then
            MessageBox.Show("Please enter product id...")

        ElseIf TextBox2.Text.Equals("") Then
            MessageBox.Show("Please enter product name...")

        ElseIf TextBox3.Text.Equals("") Then
            MessageBox.Show("Please enter product model...")

        ElseIf TextBox5.Text.Equals("") Then
            MessageBox.Show("Please enter price...")

        ElseIf TextBox6.Text.Equals("") Then
            MessageBox.Show("Please enter quantity...")

        ElseIf String.IsNullOrEmpty(ComboBox1.Text) Then
            MessageBox.Show("Select the Category")

        Else
            cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "insert into stock values('" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + ComboBox1.Text + "','" + TextBox5.Text + "','" + TextBox6.Text + "')"
            cmd.ExecuteNonQuery()

            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox5.Text = ""
            TextBox6.Text = ""
            ComboBox1.Text = ""

            Dis_data()

            MessageBox.Show("Record Added Successfuly")

        End If

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()

        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "Select * from stock where productName = '" + TextBox2.Text + "'"
        cmd.ExecuteNonQuery()
        Dim dt As New DataTable()
        Dim da As New MySqlDataAdapter(cmd) '
        da.Fill(dt)
        DataGridView1.DataSource = dt
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()

        id = Convert.ToInt32(DataGridView1.SelectedCells.Item(0).Value.ToString())
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "Select * from stock where id = " & id & ""
        cmd.ExecuteNonQuery()
        Dim dt As New DataTable()
        Dim da As New MySqlDataAdapter(cmd) '
        da.Fill(dt)
        Dim dr As MySqlDataReader
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

        While dr.Read
            TextBox1.Text = dr.GetString(0).ToString()
            TextBox2.Text = dr.GetString(1).ToString()
            TextBox3.Text = dr.GetString(2).ToString()
            ComboBox1.Text = dr.GetString(3).ToString()
            TextBox5.Text = dr.GetString(4).ToString()
            TextBox6.Text = dr.GetString(5).ToString()
        End While
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()

        Try
            cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "update stock set productName='" + TextBox2.Text + "', productModel='" + TextBox3.Text + "', category='" + ComboBox1.Text + "', price='" + TextBox5.Text + "', qty='" + TextBox6.Text + "' where id = " & id & ""
            cmd.ExecuteNonQuery()

            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            ComboBox1.Text = ""
            TextBox5.Text = ""
            TextBox6.Text = ""

            Dis_data()


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()

        Try
            cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "delete from stock where id = " & id & ""
            cmd.ExecuteNonQuery()

            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            ComboBox1.Text = ""
            TextBox5.Text = ""
            TextBox6.Text = ""

            Dis_data()


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        ComboBox1.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Me.Close()
        Main.Show()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dis_data()
    End Sub
End Class