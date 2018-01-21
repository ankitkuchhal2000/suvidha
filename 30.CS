using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Diagnostics; 
using System.ComponentModel;

public class batch:Form
{
	int recstart = 0;
	int reccount = 0;
	
	int num = 0;
	int itnum = 0;
	
	Label lbl1;
	Label lbl2;
	
	TextBox tb1;
	TextBox tb2;
	TextBox tb3;
	TextBox tb4;
	TextBox tb5;
	
	DateTimePicker dtp1;
	DateTimePicker dtp2;
	DateTimePicker dtp3;
	
	CheckBox chb1;
	
	ErrorProvider err;	
	
	//DataGridView dgv;
	
	
	public batch(int icode)
	{
		Size = new Size(620,530);
		WindowState = FormWindowState.Maximized;

        lbl1 = new Label { Text = "Item Name", Location = new Point(0, 0), Parent = this };

        lbl2 = new Label { Text = "", Location = new Point(100, 0), Parent = this };

        tb1 = new TextBox();
        tb1.Location = new Point(100, 30);
        tb1.Width = 300;
        tb1.Parent = this;
        tb1.Validating += new CancelEventHandler(tb1validating);
        tb1.Validated += new EventHandler(tb1validated);
        if (icode != 0)
        {
            itnum = icode;
        }
        else
        {
            selectitem();
        }
		
	}
	bool batchexists()
	{
		bool exists=false;
		int count = 0;
		try
		{
			gv.conn.Open();
			gv.ds = new DataSet();
			gv.sql = "select count(id) as [Count] from itemb where batchno='"+tb1.Text + "' and itemid = "+itnum;
			Debug.WriteLine(gv.sql);
			gv.adap = new OleDbDataAdapter(gv.sql,gv.conn);
			gv.adap.Fill(gv.ds,"id");
			if(gv.ds.Tables[0].Rows.Count>0)
			{
				if(gv.ds.Tables[0].Rows[0]["count"].GetType() != typeof(DBNull))
					count = Convert.ToInt32(gv.ds.Tables[0].Rows[0]["count"]);
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
		if (count>0)
		{
			exists =true;
		}
		Debug.WriteLine(exists);
		return exists;
	}
	void selectitem()
	{
		gv.sql = "select itemname as [Item Name], id from item";		
		DataTable dt = new DataTable();
		searchbox.Show("Select Item",this,gv.sql,ref dt,lbl2.Text);
        if (dt.Rows.Count > 0)
        { 
            lbl2.Text = dt.Rows[0][0].ToString();
		    tb2.Text = dt.Rows[0][1].ToString();
		    tb3.Text = dt.Rows[0][2].ToString();
		    tb4.Text = dt.Rows[0][3].ToString();
		    itnum = Convert.ToInt32(dt.Rows[0][4]);
        }
    }
	
	void recstartstop()
	{
		try
		{
			gv.conn.Open();
			gv.ds = new DataSet();
			gv.sql = "select Max(id) as [Max],Min(id) as [Min] from itemb";
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
			gv.sql = "select itemb.batchno as[Batch Number], itemb.purchaseprice as[P Price], "+
			"itemb.saleprice as[S Price], itemb.mrp as [MRP], " +
			"itemb.mfgdate as [MFG Date], itemb.pkgdate as [PKG Date], "+
			"itemb.expdate as [EXP Date], itemb.lock as [Lock Batch], "+
			"item.itemname as [Item Name], itemb.itemid "+
			"from item INNER JOIN itemb ON (item.id = itemb.itemid) where itemb.id = "+num;
			Debug.WriteLine(gv.sql);			
			gv.adap = new OleDbDataAdapter(gv.sql,gv.conn);
			gv.adap.Fill(gv.ds,"batch");
			if(gv.ds.Tables[0].Rows.Count>0)
			{
				lbl2.Text = gv.ds.Tables[0].Rows[0]["Item Name"].ToString();
				tb1.Text = gv.ds.Tables[0].Rows[0]["Batch Number"].ToString();
				tb2.Text = gv.ds.Tables[0].Rows[0]["P Price"].ToString();
				tb3.Text = gv.ds.Tables[0].Rows[0]["S Price"].ToString();
				tb4.Text = gv.ds.Tables[0].Rows[0]["MRP"].ToString();
				
				if(gv.ds.Tables[0].Rows[0]["MFG Date"].GetType() != typeof(DBNull))
					dtp1.Value = Convert.ToDateTime(gv.ds.Tables[0].Rows[0]["MFG Date"]);
				if(gv.ds.Tables[0].Rows[0]["PKG Date"].GetType() != typeof(DBNull))
					dtp2.Value = Convert.ToDateTime(gv.ds.Tables[0].Rows[0]["PKG Date"]);
				if(gv.ds.Tables[0].Rows[0]["EXP Date"].GetType() != typeof(DBNull))
					dtp3.Value = Convert.ToDateTime(gv.ds.Tables[0].Rows[0]["EXP Date"]);
				
				chb1.Checked = Convert.ToBoolean(gv.ds.Tables[0].Rows[0]["Lock Batch"]);
				itnum = Convert.ToInt32(gv.ds.Tables[0].Rows[0]["itemid"]);
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
	void selclick(object sender , EventArgs e)
	{
		selectitem();
		recstartstop();
		num = recstart;
		tb5.Text = num + " of " + reccount;
		fillform();
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
	
	void newclick(object sender , EventArgs e)
	{		
		recstartstop();
		num = reccount;
		num++;
		tb5.Text = num + " of " + reccount;
		fillform();
		selectitem();
	}
	
	void insclick(object sender,EventArgs e)
	{
		try
		{
			gv.conn.Open();
			gv.adap = new OleDbDataAdapter();
			gv.ds = new DataSet();				
			gv.sql = "insert into itemb (id, itemid, "+
					 "batchno, purchaseprice, "+
					 "saleprice, mrp, "+
					 "mfgdate, pkgdate, "+
					 "expdate, lock) "+
					 "values("+num+", "+itnum+" , '"+
					 tb1.Text+"', "+Convert.ToDouble("0"+tb2.Text)+", "+
					 Convert.ToDouble("0"+tb3.Text)+", "+Convert.ToDouble("0"+tb4.Text)+", #"+
					 dtp1.Value.ToString("MM/dd/yyyy HH:mm:ss")+"#, #"+dtp2.Value.ToString("MM/dd/yyyy HH:mm:ss")+"#, #"+
					 dtp3.Value.ToString("MM/dd/yyyy HH:mm:ss")+"#, "+chb1.Checked+")";
			Debug.WriteLine(gv.sql);
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
		tb5.Text = num + " of " + reccount;
		fillform();
	}
	
	void updclick(object sender,EventArgs e)
	{
		try
		{
			gv.conn.Open();
			gv.adap = new OleDbDataAdapter();
			gv.sql = "update itemb set batchno = '"+tb1.Text+"', purchaseprice = "+Convert.ToDouble("0"+tb2.Text)+
					 ", saleprice = "+Convert.ToDouble("0"+tb3.Text)+", mrp = "+Convert.ToDouble("0"+tb4.Text)+
					 ",mfgdate = #"+dtp1.Value.ToString("MM/dd/yyyy HH:mm:ss")+"#, pkgdate = #"+dtp2.Value.ToString("MM/dd/yyyy HH:mm:ss")+"#, "+
					 "expdate = #"+dtp3.Value.ToString("MM/dd/yyyy HH:mm:ss")+"#, lock = "+chb1.Checked+
					 " where id = "+num;
			Debug.WriteLine(gv.sql);
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
	void delclick(object sender,EventArgs e)
	{
		try
		{
			gv.conn.Open();
			gv.adap = new OleDbDataAdapter();
			gv.sql="Delete from itemb where id="+num;
			Debug.WriteLine(gv.sql);
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
		//num++;
		tb5.Text = num + " of " + reccount;
		fillform();
	}
	void tb1validating(object sender , CancelEventArgs e)
	{
		if(batchexists())
		{
		//	e.Cancel=true;
		//	err.SetError(tb1,"Batch Already Exists!!");
		}	
	}
	
	void tb1validated(object sender , EventArgs e)
	{
		if(batchexists())
		{
			err.SetError(tb1,"Batch Already Exists!!");
		}
		else
		{
			err.SetError(tb1,"");
		}
	}
}