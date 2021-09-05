Imports MySql.Data.MySqlClient

Public Class RetrunOutward

    Public con As New MySqlConnection
    Public cmd As MySqlCommand
    Public id As Integer

    Private Sub RetrunOutward_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

        If RadioButton1.Checked = False And RadioButton2.Checked = False Then
            MessageBox.Show("do you want search by ID or Product name please select a radio button")

        ElseIf RadioButton1.Checked = True And RadioButton2.Checked = False Then
            cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "Select * from stock where id = '" + TextBox1.Text + "'"
            cmd.ExecuteNonQuery()
            Dim dt1 As New DataTable()
            Dim da1 As New MySqlDataAdapter(cmd) '
            da1.Fill(dt1)
            DataGridView1.DataSource = dt1

        ElseIf RadioButton2.Checked = True And RadioButton1.Checked = False Then
            cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "Select * from stock where productName = '" + TextBox1.Text + "'"
            cmd.ExecuteNonQuery()
            Dim dt2 As New DataTable()
            Dim da2 As New MySqlDataAdapter(cmd) '
            da2.Fill(dt2)
            DataGridView1.DataSource = dt2
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dis_data()
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
            TextBox7.Text = dr.GetString(0).ToString()
            TextBox2.Text = dr.GetString(1).ToString()
            TextBox8.Text = dr.GetString(2).ToString()
            TextBox6.Text = dr.GetString(3).ToString()
            TextBox9.Text = dr.GetString(5).ToString()
        End While
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TextBox9.Text = ""
        TextBox6.Text = ""
        TextBox8.Text = ""
        TextBox7.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()

        If TextBox7.Text.Equals("") Then
            MessageBox.Show("Select Stock from the list frist")

        ElseIf TextBox3.Text.Equals("") Then
            MessageBox.Show("Enter retrun quantity")

        ElseIf String.IsNullOrEmpty(ComboBox1.Text) Then
            MessageBox.Show("Select the reason")

        ElseIf Convert.ToInt32(TextBox9.Text) < Convert.ToInt32(TextBox3.Text) Then
            MessageBox.Show("Cannot retrun more than availble....")

        Else
            cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "INSERT INTO `returnout`(`ProductID`, `ReturnQuantity`, `RetrunDate`, `RetrunReason`) 
            VALUES ('" + TextBox7.Text + "','" + TextBox3.Text + "','" + DateTimePicker1.Text + "','" + ComboBox1.Text + "')"

            cmd.ExecuteNonQuery()

            Dim qty As Integer

            qty = Convert.ToInt32(TextBox9.Text) - Convert.ToInt32(TextBox3.Text)

            cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "update stock set qty = " & qty & " where id = '" + TextBox7.Text + "'"

            cmd.ExecuteNonQuery()

            MessageBox.Show("Record Added Successfuly")

        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        Me.Close()
        Main.Show()
    End Sub
End Class