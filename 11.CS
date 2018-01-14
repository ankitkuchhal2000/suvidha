using System;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Diagnostics; 
using System.ComponentModel;

public class login:Form
{
	int counter;
	Label lbl1;
	Label lbl2;
	
	TextBox tb1;
	TextBox tb2;
	
	Button btn1;

	public login()
	{
        Size = new Size(620, 530);
        WindowState = FormWindowState.Maximized;
        Load += new EventHandler(winload);

        lbl1 = new Label
        {
            Location = new Point(0, 0),
            Text = "User Name",
            Parent = this
        };
        tb1 = new TextBox
        {
            Location = new Point(100, 0),
            Parent = this
        };

        lbl2 = new Label
        {
            Location = new Point(0, 30),
            Text = "Password",
            Parent = this
        };
        tb2 = new TextBox
        {
            Location = new Point(100, 30),
            Parent = this
        };

        btn1 = new Button
        {
            Text = "Login",
            Location = new Point(0, 150),
            Parent = this
        };
        btn1.Click += new EventHandler(logclick);

        

		
	}
	void fillform()
	{
		try
		{
			gv.conn.Open();
			gv.ds = new DataSet();
			gv.sql = "select count(id) as [counter] from users";
			Debug.WriteLine(gv.sql);
			gv.adap.SelectCommand = new OleDbCommand(gv.sql,gv.conn);
			gv.adap.Fill(gv.ds,"count");
			if(gv.ds.Tables[0].Rows.Count>0)
			{
				if(gv.ds.Tables[0].Rows[0]["counter"].GetType() != typeof(DBNull))
					counter = Convert.ToInt32(gv.ds.Tables[0].Rows[0]["counter"]);
			}
		}
		catch(Exception ex)
		{
			Debug.WriteLine(ex.Message);
		}
		finally
		{
			gv.conn.Close();
		}
	}
	void winload(object sender, EventArgs e)
	{
        fillform();

        if (counter == 0)
		{
			gv.loginsuc=true;
			Close();
		}
	}
	void logclick(object sender, EventArgs e)
	{
		string pass = "";
		int ucount= 0;
		try
		{
            gv.ds = new DataSet();
            gv.conn.Open();
			gv.sql = "select count(id) as[counter], pass as [Password] from users where username='"+tb1.Text+"' group by pass";
			Debug.WriteLine(gv.sql);
			gv.adap.SelectCommand = new OleDbCommand(gv.sql,gv.conn);
			gv.adap.Fill(gv.ds,"password");
			if(gv.ds.Tables[0].Rows.Count>0)
			{
				if(gv.ds.Tables[0].Rows[0]["counter"].GetType() != typeof(DBNull))
					ucount = Convert.ToInt32(gv.ds.Tables[0].Rows[0]["counter"]);
				
				pass=gv.ds.Tables[0].Rows[0]["Password"].ToString();
			}
		}
		catch(Exception ex)
		{
			Debug.WriteLine(ex.Message);
		}
		finally
		{
			gv.conn.Close();
		}
		
		if(ucount != 1)
		{
			MessageBox.Show("Wrong user name or password !!");
			return;
		}
		
		if(pass==gethash(tb2.Text))
		{
			gv.loginsuc = true;
			Close();
		}
		else
		{
			MessageBox.Show("Wrong user name or password !!");
		}
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