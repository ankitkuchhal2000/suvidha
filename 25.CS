using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Diagnostics; 
using System.ComponentModel;

public class billbook:Form
{
	int recstart = 0;
	int reccount = 0;
	int num = 0;
	
	Label lbl1;
	Label lbl2;
	
	TextBox tb1;
	TextBox tb2;
	TextBox tbnum;
	
	Button btn1;
	Button btn2;
	Button btn3;
	Button btn4;
	Button btn5;
	Button btn6;
	Button btn7;
	Button btn8;
	
	public billbook()
	{		
		Size = new Size(500,200);
		WindowState = FormWindowState.Maximized;
		
		lbl1 = new Label();
		lbl1.Location = new Point(0,0);
		lbl1.Text = "Bill Book";
		lbl1.Parent = this;

        tb1 = new TextBox();
        tb1.Location = new Point(100, 0);
        tb1.Parent = this;

        lbl2 = new Label();
		lbl2.Location = new Point(0,30);
		lbl2.Text = "Series";
		lbl2.Parent = this;
		
		tb2 = new TextBox();
		tb2.Location = new Point (100,30);
		tb2.Parent = this;
		
		btn1 = new Button();
		btn1.Text = "First";
		btn1.Location = new Point(0,60);
		btn1.Click += new EventHandler(firclick);
		btn1.Parent = this;
		
		btn2 = new Button();
		btn2.Text = "Previous";
		btn2.Location = new Point(100,60);
		btn2.Click += new EventHandler(preclick);
		btn2.Parent = this;		
		
		tbnum = new TextBox();
		tbnum.Location = new Point(200,60);
		tbnum.Width = 75;
		tbnum.Parent = this;
		
		btn3 = new Button();
		btn3.Text = "Next";
		btn3.Location = new Point(300,60);
		btn3.Click += new EventHandler(nexclick);
		btn3.Parent = this;		
		
		btn4 = new Button();
		btn4.Text = "Last";
		btn4.Location = new Point(400,60);
		btn4.Click += new EventHandler(lasclick);
		btn4.Parent = this;
		
		btn5 = new Button();
		btn5.Text = "Insert";
		btn5.Location = new Point(100,90);
		btn5.Click += new EventHandler(insclick);
		btn5.Parent = this;
		
		btn6 = new Button();
		btn6.Text = "Update";
		btn6.Location = new Point(200,90);
		btn6.Click += new EventHandler(updclick);
		btn6.Parent = this;
		
		btn7 = new Button();
		btn7.Text = "Delete";
		btn7.Location = new Point(300,90);
		btn7.Click += new EventHandler(delclick);
		btn7.Parent = this;

		btn8 = new Button();
		btn8.Text = "New";
		btn8.Location = new Point(200,120);
		btn8.Click += new EventHandler(newclick);
		btn8.Parent = this;
		
		recstartstop();
		num = reccount;
		num++;
		tbnum.Text = num + " of " + reccount;
		fillform();
	}
	
	void recstartstop()
	{
		try
		{
			gv.conn.Open();
			gv.ds = new DataSet();
			gv.sql = "select Max(id) as [Max],Min(id) as [Min] from billnum";
			Debug.WriteLine(gv.sql);
			gv.adap = new OleDbDataAdapter(gv.sql,gv.conn);
			gv.adap.Fill(gv.ds,"id");
			if(gv.ds.Tables[0].Rows.Count>0)
			{
				if(gv.ds.Tables[0].Rows[0]["Min"].GetType() != typeof(DBNull))
					recstart = Convert.ToInt32(gv.ds.Tables[0].Rows[0]["Min"]);
				if(gv.ds.Tables[0].Rows[0]["Max"].GetType() != typeof(DBNull))
					reccount = Convert.ToInt32(gv.ds.Tables[0].Rows[0]["Max"]);
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
	
	void fillform()
	{
		try
		{
			gv.conn.Open();
			gv.ds = new DataSet();
			gv.sql = "select billnum.billbook as [Bill Book],billnum.series as [Series] ,"+
			"billnum.billid "+
			"from  billnum "+
			"inner join bill on billnum.billid=bill.id "+
			"where  billnum.id = " + num ;
			Debug.WriteLine(gv.sql);
			gv.adap = new OleDbDataAdapter(gv.sql,gv.conn);
			gv.adap.Fill(gv.ds,"Bill Book");
			
			tb1.Text = "";
			tb2.Text = "";
			tbnum.Text = num + " of " + reccount;
			
			if(gv.ds.Tables[0].Rows.Count > 0)
			{
				tb1.Text = gv.ds.Tables[0].Rows[0]["Bill Book"].ToString();
				tb2.Text = gv.ds.Tables[0].Rows[0]["Series"].ToString();
				tbnum.Text = num + " of " + reccount;
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
	
	void firclick(object sender , EventArgs e)
	{
		recstartstop();
		num = recstart;
		tbnum.Text = num + " of " + reccount;
		fillform();
	}
	
	void preclick(object sender , EventArgs e)
	{
		recstartstop();
		num--;
		if (num<recstart)
		{
			num=reccount;
		}
		tbnum.Text = num + " of " + reccount;
		fillform();
	}
	
	void nexclick(object sender , EventArgs e)
	{
		recstartstop();
		num ++;
		if(num>reccount)
		{
			num=recstart;
		}
		tbnum.Text = num + " of " + reccount;
		fillform();		
	}
	
	void lasclick(object sender , EventArgs e)
	{
		recstartstop();
		num = reccount;
		tbnum.Text = num + " of " + reccount;
		fillform();
	}
	
	void newclick(object sender , EventArgs e)
	{		
		recstartstop();
		num = reccount;
		num++;
		tbnum.Text = num + " of " + reccount;
		fillform();
	}
	
	void delclick(object sender , EventArgs e)
	{
		try
		{
			gv.conn.Open();
			gv.ds = new DataSet();
			gv.sql = "delete from  billnum where  id = " + num ;
			Debug.WriteLine(gv.sql);
			gv.adap = new OleDbDataAdapter();
			gv.adap.DeleteCommand = new OleDbCommand(gv.sql,gv.conn);
			gv.adap.DeleteCommand.ExecuteNonQuery();
		}
		catch(Exception ex)
		{
			Debug.WriteLine(ex.Message);
		}
		finally
		{
			gv.conn.Close();
		}
		recstartstop();
		num = reccount;
		tbnum.Text = num + " of " + reccount;
		fillform();
	}
	
	void updclick(object sender , EventArgs e)
	{
		try
		{
			gv.conn.Open();
			gv.ds = new DataSet();
			gv.sql = "update billnum set billid = "+gv.billid+", billbook = '" + tb1.Text + "',series = '" + tb2.Text + "' where id = " + num;
			Debug.WriteLine(gv.sql);
			gv.adap = new OleDbDataAdapter();
			gv.adap.UpdateCommand = new OleDbCommand(gv.sql,gv.conn);
			gv.adap.UpdateCommand.ExecuteNonQuery();
		}
		catch(Exception ex)
		{
			Debug.WriteLine(ex.Message);
		}
		finally
		{
			gv.conn.Close();
		}
		recstartstop();
		tbnum.Text = num + " of " + reccount;
		fillform();
	}
	
	void insclick(object sender , EventArgs e)
	{
		recstartstop();
		num = reccount;
		num++;
		
		try
		{
			gv.conn.Open();
			gv.ds = new DataSet();
			gv.sql = "insert into billnum (id,billid,billbook,series) values("+num+","+gv.billid+",'"+tb1.Text+"','"+tb2.Text+"')";
			Debug.WriteLine(gv.sql);
			gv.adap = new OleDbDataAdapter();
			gv.adap.InsertCommand = new OleDbCommand(gv.sql,gv.conn);
			gv.adap.InsertCommand.ExecuteNonQuery();
		}
		catch(Exception ex)
		{
			Debug.WriteLine(ex.Message);
		}
		finally
		{
			gv.conn.Close();
		}
		recstartstop();
		num = reccount;
		tbnum.Text = num + " of " + reccount;
		fillform();
	}
}