using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.IO.Compression;
using System.Diagnostics; 
using System.ComponentModel;

public class spacc:Form
{
	int recstart = 0;
	int reccount = 0;
	int num = 0;

	Label lbl1;
	Label lbl2;
	Label lbl3;
	Label lbl4;
	Label lbl5;
	
	TextBox tb1;
	TextBox tb2;
	TextBox tb3;
	TextBox tb4;
	TextBox tb5;
	
	ComboBox cb1;
	
	Button btn1;
	Button btn2;
	Button btn3;
	Button btn4;
	Button btn5;
	Button btn6;
	Button btn7;
	Button btn8;
	
	public spacc()
	{
		Size = new Size(500,500);
		WindowState = FormWindowState.Maximized;
		AutoScroll = true;
		
		lbl1 = new Label();
		lbl1.Text = "Head Name";
		lbl1.Location = new Point(0,0);
		lbl1.Parent = this;
		
		lbl2 = new Label();
		lbl2.Text = "Tax %";
		lbl2.Location = new Point(0,30);	
		lbl2.Parent = this;
		
		lbl3 = new Label();
		lbl3.Text = "Surcharge %";
		lbl3.Location = new Point(0,60);
		lbl3.Parent = this;
		
		lbl4 = new Label();
		lbl4.Text = "Type";
		lbl4.Location = new Point(0,90);
		lbl4.Parent = this;
		
		lbl5 = new Label();
		lbl5.Text = "Short Name";
		lbl5.Location = new Point(0,120);
		lbl5.Parent = this;
		
		tb1 = new TextBox();
		tb1.Location = new Point(100,0);
		tb1.Width = 300;
		tb1.Parent = this;
		
		tb2 = new TextBox();
		tb2.Location = new Point(100,30);
		tb2.Parent = this;
		
		tb3 = new TextBox();
		tb3.Location = new Point(100,60);
		tb3.Parent = this;

		cb1 = new ComboBox();
		cb1.Items.Add("SALE");
		cb1.Items.Add("PURCHASE");
		cb1.Location = new Point(100,90);
		cb1.Parent = this;
		
		tb4 = new TextBox();
		tb4.Location = new Point(100,120);
		tb4.Parent = this;
		
		btn1 = new Button();
		btn1.Text = "First";
		btn1.Location = new Point(0,150);
		btn1.Click += new EventHandler(firclick);
		btn1.Parent = this;
		
		btn2 = new Button();
		btn2.Text = "Previous";
		btn2.Location = new Point(100,150);
		btn2.Click += new EventHandler(preclick);
		btn2.Parent = this;	

		tb5 = new TextBox();
		tb5.Location = new Point(200,150);
		tb5.Width = 75;
		tb5.Parent = this;		
		
		btn3 = new Button();
		btn3.Text = "Next";
		btn3.Location = new Point(300,150);
		btn3.Click += new EventHandler(nexclick);
		btn3.Parent = this;		
		
		btn4 = new Button();
		btn4.Text = "Last";
		btn4.Location = new Point(400,150);
		btn4.Click += new EventHandler(lasclick);
		btn4.Parent = this;
		
		btn5 = new Button();
		btn5.Text = "Insert";
		btn5.Location = new Point(100,180);
		btn5.Click += new EventHandler(insclick);
		btn5.Parent = this;
		
		btn6 = new Button();
		btn6.Text = "Update";
		btn6.Location = new Point(200,180);
		btn6.Click += new EventHandler(updclick);
		btn6.Parent = this;
		
		btn7 = new Button();
		btn7.Text = "Delete";
		btn7.Location = new Point(300,180);
		btn7.Click += new EventHandler(delclick);
		btn7.Parent = this;

		btn8 = new Button();
		btn8.Text = "New";
		btn8.Location = new Point(200,210);
		btn8.Click += new EventHandler(newclick);
		btn8.Parent = this;
		
		recstartstop();
		num = reccount;
		num++;
		tb5.Text = num + " of " + reccount;
		fillform();
	}	
	void recstartstop()
	{
		try
		{
			gv.conn.Open();
			gv.ds = new DataSet();
			gv.sql = "select Max(id) as [Max],Min(id) as [Min] from spacc";
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
			gv.sql = "select spacc.head, spacc.taxrate, " +
			"spacc.surchargerate, spacc.type, " +
			"spacc.shortname "+
			"from  spacc " +
			"where  spacc.id = " + num ;
			Debug.WriteLine(gv.sql);
			gv.adap = new OleDbDataAdapter(gv.sql,gv.conn);
			gv.adap.Fill(gv.ds,"SPACC");
			
			tb1.Text = "";
			tb2.Text = "";
			tb3.Text = "";
			tb4.Text = "";
			if(gv.ds.Tables[0].Rows.Count > 0)
			{
				tb1.Text = gv.ds.Tables[0].Rows[0]["head"].ToString();
				tb2.Text = gv.ds.Tables[0].Rows[0]["taxrate"].ToString();
				tb3.Text = gv.ds.Tables[0].Rows[0]["surchargerate"].ToString();
				if(gv.ds.Tables[0].Rows[0]["type"].ToString()=="S")
				{
					cb1.SelectedItem ="SALE";
				}
				else
				{
					cb1.SelectedItem ="PURCHASE";
				}
				tb4.Text = gv.ds.Tables[0].Rows[0]["shortname"].ToString();
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
		tb5.Text = num + " of " + reccount;
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
		tb5.Text = num + " of " + reccount;
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
		tb5.Text = num + " of " + reccount;
		fillform();		
	}
	void lasclick(object sender , EventArgs e)
	{
		recstartstop();
		num = reccount;
		tb5.Text = num + " of " + reccount;
		fillform();
	}
	void insclick(object sender , EventArgs e)
	{
		recstartstop();
		num = reccount;
		num++;
		
		try
		{
			string sp = "";
			if(cb1.SelectedItem.ToString() == "SALE")
			{
				sp = "S";
			}
			else
			{
				sp = "P";
			}
			gv.conn.Open();
			gv.ds = new DataSet();
			gv.sql = "insert into spacc (id, head, "+
			"taxrate, surchargerate, "+
			"type, shortname) "+
			"values("+num+", '"+tb1.Text+"', '"+
			tb2.Text+"', '"+tb3.Text+"', '"+
			sp+"', '"+tb4.Text+"')";
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
		tb5.Text = num + " of " + reccount;
		fillform();
	}
	void updclick(object sender , EventArgs e)
	{
		try
		{
			string sp = "";
			if(cb1.SelectedItem.ToString() == "SALE")
			{
				sp = "S";
			}
			else
			{
				sp = "P";
			}
			gv.conn.Open();
			gv.ds = new DataSet();
			gv.sql = "update spacc set head = '"+tb1.Text+"', taxrate = '" + tb2.Text + 
			"', surchargerate = '" + tb3.Text + "',type ='"+sp+"', shortname = '"+tb4.Text+"' where id = " + num;
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
		tb5.Text = num + " of " + reccount;
		fillform();
	}
	void delclick(object sender , EventArgs e)
	{
		try
		{
			gv.conn.Open();
			gv.ds = new DataSet();
			gv.sql = "delete from  spacc where  id = " + num ;
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
		tb5.Text = num + " of " + reccount;
		fillform();
	}
	void newclick(object sender , EventArgs e)
	{		
		recstartstop();
		num = reccount;
		num++;
		tb5.Text = num + " of " + reccount;
		fillform();
	}
}