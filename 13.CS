using System;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Diagnostics; 
using System.ComponentModel;

public class user:Form
{
    Label lbl1,lbl2;
	
	TextBox tb1,tb2;
	
	Button btn1,btn2;

    gv.mode _mode;
    public gv.mode Mode
    {
        get
        {
            return _mode;
        }
        set
        {
            _mode = value;
        }
    }
    public int Num
    {
        get; set;
    }
    public user(gv.mode md, int num)
	{
        Num = num;
        Mode = md;
        WindowState = FormWindowState.Maximized;
        Load += new EventHandler(load);
        lbl1 = new Label { Location = new Point(0, 0), Text = "User Name", Parent = this };
        tb1 = new TextBox { Location = new Point(100, 0), Parent = this };

        lbl2 = new Label { Location = new Point(0, 30), Text = "Password", Parent = this };
        tb2 = new TextBox { Location = new Point(100, 30), Parent = this };

        btn1 = new Button { Text = "SAVE", Location = new Point(100, 180), Parent = this };
        btn1.Click += new EventHandler(savclick);

        btn2 = new Button { Text = "Delete", Location = new Point(300, 180), Parent = this };
        btn2.Click += new EventHandler(delclick);

        if (Mode == gv.mode.modify || Mode == gv.mode.newentry)
        {
            btn1.Visible = true;
            btn2.Visible = false;
        }
        if (Mode == gv.mode.delete)
        {
            btn1.Visible = false;
            btn2.Visible = true;
        }        
    }
    void load(object sender, EventArgs e)
    {
        select();
    }
    void savclick(object sender, EventArgs e)
    {
        if (Mode == gv.mode.newentry)
        {
            insert();
            Close();
        }
        if (Mode == gv.mode.modify)
        {
            update();
            Close();
        }
    }
    void delclick(object sender, EventArgs e)
    {
        delete();
    }
    void select()
    {
        try
        {
            gv.conn.Open();
            gv.ds = new DataSet();
            gv.sql = "select username,pass from  users where  users.id = @id";
            Debug.WriteLine(gv.sql);
            gv.adap.SelectCommand = new OleDbCommand(gv.sql, gv.conn);
            gv.adap.SelectCommand.Parameters.AddWithValue("@id", Num);
            gv.adap.Fill(gv.ds, "users");
            if (gv.ds.Tables[0].Rows.Count > 0)
            {
                tb1.Text = gv.ds.Tables[0].Rows[0]["username"].ToString();
                tb2.Text = getstring(gv.ds.Tables[0].Rows[0]["pass"].ToString());
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            gv.conn.Close();
        }
    }
    void selectid()
    {
        try
        {
            int recstart = 0, reccount = 0;
            gv.ds = new DataSet();
            gv.conn.Open();
            gv.sql = "select Max(id) as [Max],Min(id) as [Min] from users";
            Debug.WriteLine(gv.sql);
            gv.adap.SelectCommand = new OleDbCommand(gv.sql, gv.conn);
            gv.adap.Fill(gv.ds, "id");
            if (gv.ds.Tables[0].Rows.Count > 0)
            {
                if (gv.ds.Tables[0].Rows[0]["Min"].GetType() != typeof(DBNull))
                    recstart = Convert.ToInt32(gv.ds.Tables[0].Rows[0]["Min"]);
                if (gv.ds.Tables[0].Rows[0]["Max"].GetType() != typeof(DBNull))
                    reccount = Convert.ToInt32(gv.ds.Tables[0].Rows[0]["Max"]);
                Num = reccount;
                Num++;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            gv.conn.Close();
        }
    }
    void insert()
    {
        selectid();
        try
        {
            gv.conn.Open();
            gv.sql = "insert into users (id, username, pass) values(@id,@username,@password)";
            Debug.WriteLine(gv.sql);
            gv.adap.InsertCommand = new OleDbCommand(gv.sql, gv.conn);
            gv.adap.InsertCommand.Parameters.AddWithValue("@id", Num);
            gv.adap.InsertCommand.Parameters.AddWithValue("@usename", tb1.Text);
            gv.adap.InsertCommand.Parameters.AddWithValue("@password", gethash(tb2.Text));
            gv.adap.InsertCommand.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            gv.conn.Close();
        }
    }
    void update()
    {
        try
        {
            gv.conn.Open();
            gv.sql = "update users set username = @username, pass = @password where id =  @id";
            Debug.WriteLine(gv.sql);
            gv.adap.UpdateCommand = new OleDbCommand(gv.sql, gv.conn);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@usename", tb1.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@password", gethash(tb2.Text));
            gv.adap.UpdateCommand.Parameters.AddWithValue("@id", Num);
            gv.adap.UpdateCommand.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            gv.conn.Close();
        }
    }
    void delete()
    {
        try
        {
            gv.conn.Open();
            gv.sql = "delete from  users where  id = " + Num;
            Debug.WriteLine(gv.sql);
            gv.adap.DeleteCommand = new OleDbCommand(gv.sql, gv.conn);
            gv.adap.DeleteCommand.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            gv.conn.Close();
        }
    }
    string getstring(string hash)
	{
		string retval = "";
		byte[]array = Encoding.ASCII.GetBytes(hash);
		byte[]stringarray = new byte[array.Length];
		int i = 0;
		foreach(byte element in array)
		{
			stringarray[i] =(byte)(element + 32);
			i++;
		}
		retval = Encoding.ASCII.GetString(stringarray);
		return retval;
	}
	string gethash(string pass)
	{
		string retval = "";
		byte[]array = Encoding.ASCII.GetBytes(pass);
		byte[]hasharray = new byte[array.Length];
		int i = 0;
		foreach(byte element in array)
		{
			hasharray[i] =(byte)(element - 32);
			i++;
		}
		retval = Encoding.ASCII.GetString(hasharray);
		return retval;
	}
}