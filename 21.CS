using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Diagnostics; 
using System.ComponentModel;

public class item:Form
{	
	
	int recstart = 0;
	int reccount = 0;
	int num = 0;
	int saleacnum=0;
	int purchaseacnum=0;
	
	
	ErrorProvider err;	
	ProgressBar pbr;	
	BackgroundWorker bkw;
	PictureBox pcb;
	inireader ir = new inireader(gv.inifile);	
	
	Label lbl1,lbl2,lbl3,lbl4,lbl5,lbl6,lbl7,lbl8,lbl9,lbl10;
	TextBox tb1,tb2,tb3,tb4,tb5;
	Button btn1,btn2,btn7,btn8,btn9,btn10;
	
	public item(int icode)
	{
		
		Size = new Size(500,300);
		WindowState = FormWindowState.Maximized;

        lbl1 = new Label{Text = "Item Name",Location = new Point(0, 0),Parent = this};
        tb1 = new TextBox { Location = new Point(100, 0), Width = 300, Parent = this };

        lbl2 = new Label { Text = "Bar Code", Location = new Point(0, 30), Parent = this };
        tb2 = new TextBox { Location = new Point(100, 30), Parent = this };

        lbl3 = new Label { Location = new Point(200, 30), Text = "Bar Code Image", Parent = this };
        pcb = new PictureBox { Location = new Point(300, 30), Height = 15, Parent = this };

        lbl4 = new Label
        {
            Text = "Sale A/c",
            Location = new Point(0, 60),
            Parent = this
        };

        lbl5 = new Label
        {
            Text = "",
            Location = new Point(100, 60),
            Width = 300,
            Parent = this
        };

        btn1 = new Button
        {
            Text = "Select Sale A/C",
            Location = new Point(400, 60),
            Width = 125,
            Parent = this
        };
        btn1.Click += new EventHandler(selsalclick);

        lbl6 = new Label
        {
            Text = "Purchase A/C",
            Location = new Point(0, 90),
            Parent = this
        };

        lbl7 = new Label
        {
            Text = "",
            Location = new Point(100, 90),
            Width = 300,
            Parent = this
        };

        btn2 = new Button
        {
            Text = "Select Purchase A/C",
            Location = new Point(400, 90),
            Width = 125,
            Parent = this
        };
        btn2.Click += new EventHandler(selpurclick);

        lbl8 = new Label
        {
            Location = new Point(0, 120),
            Text = "Purchase Price",
            Parent = this
        };

        tb3 = new TextBox
        {
            Location = new Point(100, 120),
            Parent = this
        };

        lbl9 = new Label
        {
            Location = new Point(0, 150),
            Text = "Sale Price",
            Parent = this
        };

        tb4 = new TextBox
        {
            Location = new Point(100, 150),
            Parent = this
        };

        lbl10 = new Label
        {
            Location = new Point(0, 180),
            Text = "MRP",
            Parent = this
        };

        tb5 = new TextBox
        {
            Location = new Point(100, 180),
            Parent = this
        };


        btn7 = new Button
        {
            Text = "Insert",
            Location = new Point(100, 240),
            Parent = this
        };
        btn7.Click += new EventHandler(insclick);

        btn8 = new Button
        {
            Text = "Update",
            Location = new Point(200, 240),
            Parent = this
        };
        btn8.Click += new EventHandler(updclick);

        btn9 = new Button
        {
            Text = "Delete",
            Location = new Point(300, 240),
            Parent = this
        };
        btn9.Click += new EventHandler(delclick);

        btn10 = new Button
        {
            Text = "New",
            Location = new Point(200, 270),
            Parent = this
        };
        btn10.Click += new EventHandler(newclick);

        recstartstop();
		num = reccount;
		num++;
		if(icode>0)
		{
			num = icode;
		}
		
		//tb6.Text = num + " of " + reccount;
		fillform();
		
		if(icode ==0)
		{
			selectsale();
			selectpurchase();
		}
	}
	
	void recstartstop()
	{
		try
		{
			gv.conn.Open();
			gv.ds = new DataSet();
			gv.sql = "select Max(id) as [Max],Min(id) as [Min] from item";
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
			gv.sql = "select item.itemname as [Item Name],item.barcode as [Bar Code], "+
			"item.saleac, item.purchaseac, "+
			"item.purchaseprice, item.saleprice, "+
			"item.mrp, spacc.taxrate, "+
			"spacc.surchargerate, spacc.head as [Sale AC], "+
			"spacc_1.head as [Purchase AC] "+
			"FROM spacc INNER JOIN (spacc AS spacc_1 INNER JOIN item ON spacc_1.id = item.purchaseac) ON spacc.id = item.saleac "+
			"where item.id =" + num;
			Debug.WriteLine(gv.sql);
			gv.adap = new OleDbDataAdapter(gv.sql,gv.conn);
			gv.adap.Fill(gv.ds,"item");
			
			tb1.Text = "";
			tb2.Text = "";			
			if(gv.ds.Tables[0].Rows.Count>0)
			{
				tb1.Text = gv.ds.Tables[0].Rows[0]["Item Name"].ToString();
				tb2.Text = gv.ds.Tables[0].Rows[0]["Bar Code"].ToString();
				//pcb.Image = Code128Rendering.MakeBarcodeImage(lbl2.Text, int.Parse("2"), false);
				pcb.Image = Code128Rendering.MakeBarcodeImage(gv.ds.Tables[0].Rows[0]["Bar Code"].ToString(), 1, false);
				saleacnum = Convert.ToInt32(gv.ds.Tables[0].Rows[0]["saleac"]);
				lbl5.Text = gv.ds.Tables[0].Rows[0]["Sale AC"].ToString();
				purchaseacnum = Convert.ToInt32(gv.ds.Tables[0].Rows[0]["purchaseac"]);
				lbl7.Text = gv.ds.Tables[0].Rows[0]["Purchase AC"].ToString();
				tb3.Text = gv.ds.Tables[0].Rows[0]["purchaseprice"].ToString();
				tb4.Text = gv.ds.Tables[0].Rows[0]["saleprice"].ToString();
				tb5.Text = gv.ds.Tables[0].Rows[0]["mrp"].ToString();
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
	void selectsale()
	{
		inireader ir = new inireader(gv.inifile);
		try
		{
			gv.conn.Open();
			gv.ds = new DataSet();
			gv.sql = "select head, id from spacc where head = '"+ir.readstring("item","defaultsaleac")+"'";
			Debug.WriteLine(gv.sql);
			gv.adap = new OleDbDataAdapter(gv.sql,gv.conn);
			gv.adap.Fill(gv.ds,"saleac");
			if(gv.ds.Tables[0].Rows.Count>0)
			{
				saleacnum = Convert.ToInt32(gv.ds.Tables[0].Rows[0]["id"]);
				lbl5.Text = gv.ds.Tables[0].Rows[0]["head"].ToString();
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
	void selectpurchase()
	{
		inireader ir = new inireader(gv.inifile);
		try
		{
			gv.conn.Open();
			gv.ds = new DataSet();
			gv.sql = "select head, id from spacc where head = '"+ir.readstring("item","defaultpurchaseac")+"'";
			Debug.WriteLine(gv.sql);
			gv.adap = new OleDbDataAdapter(gv.sql,gv.conn);
			gv.adap.Fill(gv.ds,"purchaseac");
			if(gv.ds.Tables[0].Rows.Count>0)
			{
				purchaseacnum = Convert.ToInt32(gv.ds.Tables[0].Rows[0]["id"]);
				lbl7.Text = gv.ds.Tables[0].Rows[0]["head"].ToString();
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
	void selsalclick(object sender, EventArgs e)
	{
		gv.sql = "select head as [Head] , id  from spacc where type ='S'";
		DataTable dt = new DataTable();
		searchbox.Show("Select Sale A/C",this,gv.sql,ref dt,lbl5.Text);
		if(dt.Rows.Count >0)
		{
			lbl5.Text = dt.Rows[0][0].ToString();
			saleacnum = Convert.ToInt32(dt.Rows[0][1]);
		}
	}
	void selpurclick(object sender, EventArgs e)
	{
		gv.sql = "select head as [Head] , id  from spacc where type ='P'";
		DataTable dt = new DataTable();
		searchbox.Show("Select Purchase A/C",this,gv.sql,ref dt,lbl7.Text);
		if(dt.Rows.Count >0)
		{
			lbl7.Text = dt.Rows[0][0].ToString();
			purchaseacnum = Convert.ToInt32(dt.Rows[0][1]);
		}
	}
	void firclick(object sender , EventArgs e)
	{
		recstartstop();
		num = recstart;
		//tb6.Text = num + " of " + reccount;
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
		fillform();		
	}
	
	void lasclick(object sender , EventArgs e)
	{
		recstartstop();
		num = reccount;
		fillform();
	}
	
	void newclick(object sender , EventArgs e)
	{		
		recstartstop();
		num = reccount;
		num++;
		fillform();
		selectpurchase();
		selectsale();
	}
	
	void delclick(object sender , EventArgs e)
	{
		try
		{
			gv.conn.Open();
			gv.ds = new DataSet();
			gv.sql = "delete from  item where  id = " + num ;
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
		fillform();
	}
	
	void updclick(object sender , EventArgs e)
	{
		try
		{
			string barcode =num.ToString().PadLeft(5,'0');
			if(tb2.Text!="")
			{
				barcode = tb2.Text;
			}
			gv.conn.Open();			
			gv.sql = "update item set itemname = '" + tb1.Text + "', barcode = '"+barcode+"', "+
					 "saleac = '" + saleacnum +"', purchaseac = '" + purchaseacnum + "', "+
					 "purchaseprice = '" + tb3.Text +"', saleprice = '" + tb4.Text +"', "+
					 "mrp = '" + tb5.Text + "' " +
					 "where id = " + num;
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
		fillform();
	}
	
	void insclick(object sender , EventArgs e)
	{
		recstartstop();
		num = reccount;
		num++;
		if(itemexists())
		{
			return;
		}
		try
		{
			string barcode =num.ToString().PadLeft(5,'0');
			if(tb2.Text!="")
			{
				barcode = tb2.Text;
			}
			gv.conn.Open();
			gv.sql = "insert into item (id, itemname, barcode, saleac, purchaseac, purchaseprice, saleprice, mrp) values("+num+",'"+tb1.Text+"','"+
					 barcode+"',"+saleacnum+","+purchaseacnum+",'"+tb3.Text+"','"+tb4.Text+"','"+tb5.Text+"')";
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
		num = reccount;
		fillform();			
	}
	void impclick(object sender , EventArgs e)
	{
		try
		{
			FolderBrowserDialog fbd = new FolderBrowserDialog();
			DialogResult dr = fbd.ShowDialog();
			if(dr == DialogResult.OK)
			{
				string connstr = "";
				//connstr = "provider = vfpoledb;data source = item.dbf";
				connstr = "provider = Microsoft.Jet.OLEDB.4.0;data source ="+fbd.SelectedPath+"\\;extended properties = dbase iv;";
				OleDbConnection dbfconn = new OleDbConnection(connstr);
				dbfconn.Open();
				gv.ds = new DataSet();
				gv.sql = "select * from item";
				Debug.WriteLine(gv.sql);
				OleDbDataAdapter dbfadap = new OleDbDataAdapter(gv.sql,dbfconn);
				dbfadap.Fill( gv.ds,"dbfitem");
				dbfconn.Close();
				pbr.Visible = true;
				pbr.Maximum=gv.ds.Tables[0].Rows.Count;
				bkw.RunWorkerAsync();			
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
	void canimpclick(object sender , EventArgs e)
	{
		bkw.CancelAsync();
	}
	void dowork(object sender , DoWorkEventArgs e)
	{
		try
		{
			gv.conn.Open();
			int i = 0;
			gv.adap = new OleDbDataAdapter();
			foreach(DataRow dr in gv.ds.Tables[0].Rows)
			{
				i++;
				bkw.ReportProgress(i);
				gv.sql = "insert into item (id, itemname, "+
				"barcode, saleac, "+
				"purchaseac) values(" + 
				dr["ITEMCODE"].ToString().PadLeft(5,'0')+"', '"+saleacnum+"', '"+
				purchaseacnum+"')";
				Debug.WriteLine(gv.sql);
				gv.adap.InsertCommand = new OleDbCommand(gv.sql,gv.conn);
				gv.adap.InsertCommand.ExecuteNonQuery();
				//System.Threading.Thread.Sleep(5);
				if(bkw.CancellationPending)
				{
					e.Result = "Cancelled";
					Debug.WriteLine("Process Cancelled");
					break;
				}
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
		e.Result = "Completed";
	}
	void progresschanged(object sender , ProgressChangedEventArgs e)
	{
		pbr.Value = e.ProgressPercentage;
		//lbl11.Text = pbr.Value+"/"+pbr.Maximum;
	}
	void workcompleted(object sender , RunWorkerCompletedEventArgs e)
	{
		if (e.Error != null)
		{
			Debug.WriteLine(e.Error.Message);
		}
		else if (e.Cancelled)
		{
			//lbl12.Text = "Cancelled";
			Debug.WriteLine("Cancelled");
		}
		else
		{
			//lbl12.Text = e.Result.ToString();
			Debug.WriteLine(e.Result.ToString());
		}
		pbr.Visible = false;
		System.Threading.Thread.Sleep(100);
		//lbl11.Text="";
		//lbl12.Text="";
	}
	void priclick(object sender , EventArgs e)
	{ 
		
	}
	
	bool itemexists()
	{
		bool exists=false;
		int count = 0;
		try
		{
			gv.conn.Open();
			gv.ds = new DataSet();
			gv.sql = "select count(id) as [count] from item where itemname ='"+tb1.Text+"'";
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
	void tb1validating(object sender , CancelEventArgs e)
	{
		//if(itemexists())
		//{
		//	e.Cancel=true;
		//	err.SetError(tb1,"Batch Already Exists!!");
		//}	
	}
	
	void tb1validated(object sender , EventArgs e)
	{
		if(itemexists())
		{
			err.SetError(tb1,"Item Already Exists!!");
		}
		else
		{
			err.SetError(tb1,"");
		}
	}
}