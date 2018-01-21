using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Diagnostics; 
using System.ComponentModel;
using System.Media;

public class invoice:Form
{
	int recstart = 0;
	int reccount = 0;	
	int num = 0;
	int bbnum = 0;
	int partynum = 0;
	int billnum = 0;
	
	bool inclusive = false;
	
	Label lbl1;
	Label lbl2;
	Label lbl3;
	Label lbl4;
	Label lbl5;
	Label lbl6;
	Label lbl7;
	Label lbl8;
	Label lbl10;
	Label lbl12;
	Label lbl13;
	Label lbl14;
	Label lbl15;
	Label lbl16;
	Label lbl17;
	Label lbl18;
	Label lbl19;
	Label lbl21;
	Label lbl22;
	
	TextBox tb1;
	TextBox tb2;
	TextBox tb3;
	
	CheckBox chb1;
	CheckBox chb2;
	
	ComboBox cb2;
	
	Button btn1;	
	Button btn2;
	Button btn3;
	Button btn4;
	Button btn5;
	Button btn6;
	Button btn7;
	Button btn8;
	Button btn9;	
	Button btn10;
	Button btn11;
	Button btn12;
	
	ErrorProvider err;
	
	DataGridView dgv;
	
	DateTimePicker dtp1;
	
	inireader ir = new inireader(gv.inifile);	
	
	public invoice(int invoiceid)
	{
		WindowState = FormWindowState.Maximized;
		AutoScroll = true;
		
		lbl1 = new Label();
		lbl1.Text = "Bill Book";
		lbl1.Location = new Point(0,0);
		lbl1.Parent = this;
		
		lbl2 = new Label();
		lbl2.Location = new Point(100,0);
		lbl2.Text = "";
		lbl2.Parent = this;
		
		lbl3 = new Label();
		lbl3.Text = "Series";
		lbl3.Location = new Point(240,0);
		lbl3.Parent = this;
		
		lbl4 = new Label();
		lbl4.Location = new Point(340,0);
		lbl4.Text = "";
		lbl4.Parent = this;
		
		lbl5 = new Label();
		lbl5.Text = "Party Name";
		lbl5.Location = new Point(0,30);
		lbl5.Parent = this;

		lbl6 = new Label();
		lbl6.Location = new Point(100,30);
		lbl6.Parent = this;
		
		lbl7 = new Label();
		lbl7.Location = new Point(0,60);
		lbl7.Text = "Bill Number";
		lbl7.Parent = this;
		
		tb1 = new TextBox();
		tb1.Location = new Point(100,60);
		tb1.Validating += new CancelEventHandler(tb1validating);
		tb1.Validated += new EventHandler(tb1validated);
		tb1.Parent = this;
		
		err = new ErrorProvider();
		
		lbl8 = new Label();
		lbl8.Location = new Point(240,60);
		lbl8.Text = "Date";
		lbl8.Parent = this;

		dtp1 = new DateTimePicker();
		dtp1.Location = new Point(340,60);
		dtp1.Parent = this;
		
		chb1 = new CheckBox();
		chb1.Text = "Credit Invoice";
		chb1.Checked = true;
		chb1.CheckAlign = ContentAlignment.MiddleRight;
		chb1.Location = new Point(0,90);
		chb1.Parent = this;
			
		lbl10 = new Label();
		lbl10.Location = new Point(240,90);
		lbl10.Text = "Sale Type";
		lbl10.Parent = this;
	
		cb2 = new ComboBox();
		cb2.Location = new Point(340,90);
		cb2.Items.Add("VAT");
		cb2.Items.Add("CST");
		cb2.SelectedIndex = 0;
		cb2.Parent = this;
		
		chb2 = new CheckBox();
		chb2.Text = "Against C Form";
		chb2.CheckAlign = ContentAlignment.MiddleRight;
		chb2.Location = new Point(0,120);
		chb2.Parent = this;
		
		dgv = new DataGridView();
		dgv.Location = new Point(0,150);
		dgv.Width =540;
		//dgv.EditMode = DataGridViewEditMode.EditOnEnter;
		dgv.CellValidating += new DataGridViewCellValidatingEventHandler(cellvalidating);
		dgv.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(editingcontrolshowing);
		dgv.Parent = this;
		
		lbl12 = new Label();
		lbl12.Text ="Amount";
		lbl12.Location = new Point(0,320);
		lbl12.Parent = this;
		
		lbl13 = new Label();
		lbl13.Location = new Point(100,320);
		lbl13.Parent = this;
		
		lbl14 = new Label();
		lbl14.Location = new Point(0,350);
		lbl14.Text = "Tax Amount";
		lbl14.Parent = this;
		
		lbl15 = new Label();
		lbl15.Location = new Point(100,350);
		lbl15.Text = "";
		lbl15.Parent = this;
		
		lbl16 = new Label();
		lbl16.Location = new Point(0,380);
		lbl16.Text = "Surcharge Amount";
		lbl16.Parent = this;
		
		lbl17 = new Label();
		lbl17.Location = new Point(100,380);
		lbl17.Text = "";
		lbl17.Parent = this;
		
		lbl18 = new Label();
		lbl18.Location = new Point(240,320);
		lbl18.Text = "Cash Discount (Rs.)";
		lbl18.Parent = this;
		
		tb2 = new TextBox();
		tb2.Location = new Point(340,320);
		tb2.Parent = this;
		
		lbl19 = new Label();
		lbl19.Location = new Point(240,350);
		lbl19.Text = "Invoice Amount";
		lbl19.Parent = this;
		
		lbl21 = new Label();
		lbl21.Location = new Point(340,350);
		lbl21.Text = "";
		lbl21.Parent = this;
		
		lbl22 = new Label();
		lbl22.Location = new Point(0,410);
		lbl22.Text = "Error:";
		lbl22.Parent = this;
		
		btn1 = new Button();
		btn1.Text = "First";
		btn1.Location = new Point(0,440);
		btn1.Click += new EventHandler(firclick);
		btn1.Parent = this;

		btn2 = new Button();
		btn2.Text = "Previous";
		btn2.Location = new Point(100,440);
		btn2.Click += new EventHandler(preclick);
		btn2.Parent = this;
		
		tb3 = new TextBox();
		tb3.Location = new Point(200,440);
		tb3.Width = 75;
		tb3.Parent = this;
		
		btn3 = new Button();
		btn3.Text = "Next";
		btn3.Location = new Point(300,440);
		btn3.Click += new EventHandler(nexclick);
		btn3.Parent = this;		
		
		btn4 = new Button();
		btn4.Text = "Last";
		btn4.Location = new Point(400,440);
		btn4.Click += new EventHandler(lasclick);
		btn4.Parent = this;
		
		btn5 = new Button();
		btn5.Text = "Insert";
		btn5.Location = new Point(100,470);
		btn5.Click += new EventHandler(insclick);
		btn5.Parent = this;
		
		btn6 = new Button();
		btn6.Text = "Update";
		btn6.Location = new Point(200,470);
		btn6.Click += new EventHandler(updclick);
		btn6.Parent = this;
		
		btn7 = new Button();
		btn7.Text = "Delete";
		btn7.Location = new Point(300,470);
		btn7.Click += new EventHandler(delclick);
		btn7.Parent = this;

		btn8 = new Button();
		btn8.Text = "New";
		btn8.Location = new Point(200,500);
		btn8.Click += new EventHandler(newclick);
		btn8.Parent = this;
		
		btn9 = new Button();
		btn9.Text = "Goto";
		btn9.Location = new Point(200,530);
		btn9.Click += new EventHandler(gotclick);
		btn9.Parent = this;
		
		btn10 = new Button();
		btn10.Text = "Select Bill Book";
		btn10.Location = new Point(0,500);
		btn10.Width = 175;
		btn10.Click += new EventHandler(selbbclick);
		btn10.Parent = this;
		
		btn11 = new Button();
		btn11.Text = "Select Party";
		btn11.Location = new Point(0,530);
		btn11.Width = 175;
		btn11.Click += new EventHandler(selpartyclick);
		btn11.Parent = this;
		
		btn12 = new Button();
		btn12.Text = "Print";
		btn12.Location = new Point(300,500);
		btn12.Click += new EventHandler(priclick);
		btn12.Parent = this;
		
		bool.TryParse(ir.readstring("invoicing","rateinclusive"),out inclusive);

		recstartstop();
		num = reccount;
		num++;		
		if (invoiceid!=0)
		{
			num=invoiceid; //if choose to open a particular invoice externally
		}
		tb3.Text = num + " of " + reccount;
		
		fillform();
		
		if (invoiceid==0)
		{
			selectbillbook();
			selectbillnumber();
			billnum++;
			tb1.Text = billnum.ToString();
			selectparty();
		}
	}
	
	void recstartstop()
	{
		try
		{
			gv.conn.Open();
			gv.ds = new DataSet();
			gv.sql = "select Max(id) as [Max],Min(id) as [Min] from invoice ";
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
			gv.sql = "select billnum.billbook as [Bill Book], billnum.series as [Series], "+
					 "invoice.billbookid, party.partyname as [Party Name], "+
					 "invoice.partyid , invoice.billnumber as[Bill Number], "+
					 "invoice.billdate as [Bill Date], invoice.creditterm as [Credit], "+
					 "invoice.saletype as [Sale Type], invoice.againstcform as [Against C Form], "+
					 "invoice.cashdiscount "+
					 "from party inner join (billnum inner join invoice ON billnum.id = invoice.billbookid) ON party.id = invoice.partyid "+
					 "where invoice.id = "+num;
			Debug.WriteLine(gv.sql);
			gv.adap = new OleDbDataAdapter(gv.sql,gv.conn);
			gv.adap.Fill(gv.ds,"bill");
			bbnum=0;
			lbl2.Text = "";
			lbl4.Text = "";
			partynum=0;
			lbl6.Text = "";
			billnum = 0;
			tb1.Text="";			
			dtp1.Value = DateTime.Now;
			tb2.Text="";
			if(gv.ds.Tables[0].Rows.Count>0)
			{
				lbl2.Text = gv.ds.Tables[0].Rows[0]["Bill Book"].ToString();
				lbl4.Text = gv.ds.Tables[0].Rows[0]["Series"].ToString();
				bbnum = Convert.ToInt32(gv.ds.Tables[0].Rows[0]["billbookid"]);
				lbl6.Text = gv.ds.Tables[0].Rows[0]["Party Name"].ToString();
				partynum = Convert.ToInt32(gv.ds.Tables[0].Rows[0]["partyid"]);
				tb1.Text = gv.ds.Tables[0].Rows[0]["Bill Number"].ToString();
				billnum = Convert.ToInt32(gv.ds.Tables[0].Rows[0]["Bill Number"]);
				dtp1.Value = Convert.ToDateTime(gv.ds.Tables[0].Rows[0]["Bill Date"]);
				chb1.Checked = Convert.ToBoolean(gv.ds.Tables[0].Rows[0]["Credit"]);
				cb2.SelectedItem = gv.ds.Tables[0].Rows[0]["Sale Type"].ToString();
				chb2.Checked = Convert.ToBoolean(gv.ds.Tables[0].Rows[0]["Against C Form"]);
				tb2.Text = Convert.ToDouble(gv.ds.Tables[0].Rows[0]["cashdiscount"]).ToString("0.00");
			}
			 // item.purchaseac, spacc.head, spacc.taxrate, spacc.surchargerate

			gv.sql = "select item.barcode as [Bar Code], item.itemname as [Item Name], "+
					 "itemb.batchno as [Batch Number], itemb.mrp as [MRP], "+
					 "sale.qty as [Quantity], sale.includerate as [Rate + Tax], "+
					 "sale.excluderate as [Rate], sale.amount as [Amount], "+
					 "sale.taxamount as [Tax Amount], sale.surchargeamount as [Surcharge Amount], "+
					 "itemb.purchaseprice, itemb.saleprice, "+
					 "itemb.mfgdate, itemb.pkgdate, "+
					 "itemb.expdate, itemb.lock, "+
					 "spacc.head as [Sale Account], spacc.taxrate as [Tax Rate], "+
					 "spacc.surchargerate as [Surcharge Rate], sale.saleac, "+
					 "sale.batchid, itemb.itemid "+
					 "from spacc INNER JOIN ((item INNER JOIN itemb ON item.id = itemb.itemid) INNER JOIN sale ON itemb.id = sale.batchid) ON spacc.id = sale.saleac "+
					 "where sale.invoiceid = "+num;
	
			Debug.WriteLine(gv.sql);
			gv.adap.SelectCommand.CommandText = gv.sql;
			gv.adap.Fill(gv.ds,"sale");
			dgv.DataSource = gv.ds.Tables[1];
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
	
	void selectbillbook()
	{
		gv.sql = "select billbook as [Bill Book] , series as [Series] , id as [billnum] from billnum";		
		DataTable dt = new DataTable();
		searchbox.Show("Select Bill Book",this,gv.sql,ref dt,lbl2.Text);
		if (dt.Rows.Count >0)
		{
			lbl2.Text = dt.Rows[0][0].ToString();
			lbl4.Text = dt.Rows[0][1].ToString();
			bbnum = Convert.ToInt32(dt.Rows[0][2]);	
		}
	}
	
	void selectbillnumber()
	{
		try
		{
			billnum = 0;
			gv.conn.Open();
			gv.ds = new DataSet();
			gv.sql = "select max(billnumber) as [Max] from invoice where billbookid="+bbnum;
			Debug.WriteLine(gv.sql);
			gv.adap = new OleDbDataAdapter(gv.sql,gv.conn);
			gv.adap.Fill(gv.ds,"id");
			if(gv.ds.Tables[0].Rows.Count>0)
			{
				if(gv.ds.Tables[0].Rows[0]["Max"].GetType() != typeof(DBNull))
					billnum = Convert.ToInt32(gv.ds.Tables[0].Rows[0]["Max"]);
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
	bool billnumberexists()
	{
		bool exists=false;
		int count = 0;
		try
		{
			gv.conn.Open();
			gv.ds = new DataSet();
			gv.sql = "select count(id) as [count] from invoice where billnumber ="+billnum +" and billbookid = " + bbnum;
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
		return exists;
	}
	void selectparty()
	{
		gv.sql = "select partyname as [Party Name] , id  from party";
		DataTable dt = new DataTable();
		searchbox.Show("Select Party",this,gv.sql,ref dt,lbl6.Text);
		if(dt.Rows.Count >0)
		{
			lbl6.Text = dt.Rows[0][0].ToString();
			partynum = Convert.ToInt32(dt.Rows[0][1]);
		}
	}
	void delclick(object sender , EventArgs e)
	{
		try
		{
			gv.conn.Open();
			gv.sql = "delete from  sale where  invoiceid = " + num ;
			Debug.WriteLine(gv.sql);
			gv.adap = new OleDbDataAdapter();
			gv.adap.DeleteCommand = new OleDbCommand(gv.sql,gv.conn);
			gv.adap.DeleteCommand.ExecuteNonQuery();
			
			gv.sql = "delete from  invoice where  id = " + num ;
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
		tb3.Text = num + " of " + reccount;
		fillform();
	}
	
	void insclick(object sender , EventArgs e)
	{
		Debug.WriteLine(billnum);
		if(billnumberexists())
		{
			recstartstop();
			num = reccount;
			num++;
			tb3.Text = num + " of " + reccount;
			selectbillnumber();
			billnum++;
			tb1.Text = billnum.ToString();
		}		
		try
		{
			double dis;
			double.TryParse(tb2.Text,out dis);
			gv.conn.Open();
			gv.sql = "insert into invoice(id, billbookid, "+
			"billnumber, billdate, "+
			"partyid,creditterm, "+
			"saletype, againstcform, "+
			"cashdiscount) "+
			"values("+num+", "+bbnum+", "+
			billnum+", #"+dtp1.Value.ToString("MM/dd/yyyy HH:mm:ss")+"#, "+
			partynum+", "+chb1.Checked+", '"+
			cb2.SelectedItem+"', "+chb2.Checked+", "+
			dis+")";
			Debug.WriteLine(gv.sql);
			gv.adap = new OleDbDataAdapter();
			gv.adap.InsertCommand = new OleDbCommand(gv.sql,gv.conn);
			gv.adap.InsertCommand.ExecuteNonQuery();
			foreach(DataGridViewRow dgvrow in dgv.Rows)
			{
				if(dgvrow.IsNewRow)
					continue;
				if(Convert.ToDouble(dgvrow.Cells["Quantity"].Value) == 0)
					continue;
				gv.ds = new DataSet();
				gv.sql = "insert into sale (invoiceid, billdate, "+
						 "batchid, qty, "+
						 "includerate, excluderate, "+
						 "taxamount, surchargeamount, "+
						 "amount,saleac) "+
						 "values("+num+", #"+dtp1.Value.ToString("MM/dd/yyyy HH:mm:ss")+"#, "+ 
						 dgvrow.Cells["batchid"].Value+", "+dgvrow.Cells["Quantity"].Value+", "+
						 dgvrow.Cells["Rate + Tax"].Value+", "+dgvrow.Cells["Rate"].Value+", "+
						 dgvrow.Cells["Tax Amount"].Value+", "+dgvrow.Cells["Surcharge Amount"].Value+", "+
						 dgvrow.Cells["Amount"].Value+", "+dgvrow.Cells["saleac"].Value+") ";
				Debug.WriteLine(gv.sql);
				gv.adap.InsertCommand = new OleDbCommand(gv.sql,gv.conn);
				gv.adap.InsertCommand.ExecuteNonQuery();		 
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
		printinv();
		//recstartstop();
		//num = reccount;
		//num++;
		//tb3.Text = num + " of " + reccount;
		//selectbillnumber();
		//billnum++;
		//tb1.Text = billnum.ToString();
		fillform();
	}
	void updclick(object sender , EventArgs e)
	{
		try
		{
			double dis;
			double.TryParse(tb2.Text,out dis);
			
			gv.conn.Open();
			gv.ds = new DataSet();
			gv.sql = "update invoice set billbookid = " + bbnum +", billnumber = " + billnum + ", "+
					 "billdate = #" + dtp1.Value.ToString("MM/dd/yyyy HH:mm:ss") + "#, partyid = "+partynum+", "+
					 "creditterm = " + chb1.Checked + ", saletype = '"+cb2.SelectedItem+"', "+
					 "againstcform = "+chb2.Checked+", cashdiscount ="+dis+" "+
					 "where id = "+num;
			Debug.WriteLine(gv.sql);
			gv.adap = new OleDbDataAdapter();
			gv.adap.UpdateCommand = new OleDbCommand(gv.sql,gv.conn);
			gv.adap.UpdateCommand.ExecuteNonQuery();
			
			gv.sql = " delete from  sale where  invoiceid = " + num ;
			Debug.WriteLine(gv.sql);
			gv.adap.DeleteCommand = new OleDbCommand(gv.sql,gv.conn);
			gv.adap.DeleteCommand.ExecuteNonQuery();
			
			foreach(DataGridViewRow dgvrow in dgv.Rows)
			{
				if(dgvrow.IsNewRow)
					continue;
				if(Convert.ToDouble(dgvrow.Cells["Quantity"].Value) == 0)
					continue;
				gv.ds = new DataSet();
								
				gv.sql = "insert into sale (invoiceid, billdate, "+
						 "batchid, qty, "+
						 "includerate, excluderate, "+
						 "taxamount, surchargeamount, "+
						 "amount, saleac) "+
						 "values("+num+", #"+dtp1.Value.ToString("MM/dd/yyyy HH:mm:ss")+"#, "+
						 dgvrow.Cells["batchid"].Value+", "+dgvrow.Cells["Quantity"].Value+", "+
						 dgvrow.Cells["Rate + Tax"].Value+", "+dgvrow.Cells["Rate"].Value+", "+
						 dgvrow.Cells["Tax Amount"].Value+", "+dgvrow.Cells["Surcharge Amount"].Value+", "+
						 dgvrow.Cells["Amount"].Value+", "+dgvrow.Cells["saleac"].Value+") ";
				Debug.WriteLine(gv.sql);
				gv.adap.InsertCommand = new OleDbCommand(gv.sql,gv.conn);
				gv.adap.InsertCommand.ExecuteNonQuery();		 
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
		recstartstop();
		tb3.Text = num + " of " + reccount;
		fillform();
	}
	void printinv()
	{
		try
		{		
			gv.conn.Open();
			gv.sql = "select bill.godname, bill.firmname, "+
					 "bill.address, bill.vatno, "+
					 "bill.phone1, bill.phone2, "+
					 "bill.phone3, billnum.series, "+
					 "invoice.billnumber, invoice.billdate, "+
					 "invoice.cashdiscount, invoice.id "+
					 "FROM party INNER JOIN ((bill INNER JOIN billnum ON bill.id = billnum.billid) INNER JOIN invoice ON billnum.id = invoice.billbookid) ON party.id = invoice.partyid "+
					 "where invoice.id = "+num;
			Debug.WriteLine(gv.sql);
			gv.ds = new DataSet();
			gv.adap = new OleDbDataAdapter(gv.sql,gv.conn);
			gv.adap.Fill(gv.ds,"bill");
			string head = esc.ESC+"@";
			double cashdiscount = 0;
			foreach(DataRow drow in gv.ds.Tables[0].Rows)
			{
				int id = Convert.ToInt32(drow["id"]);
				string bno="BILL NO:"+drow["series"]+drow["billnumber"];
				if(drow["godname"].ToString().Length > 0)
				{
					head += gv.PadCenter(esc.ESC + "-1" + drow["godname"] + esc.ESC + "-0", 48) + esc.CR+esc.LF;
				}
				if(drow["firmname"].ToString().Length > 0)
				{
					//head +=esc.ES+"P";
					head += gv.PadCenter(esc.ESC + "E " + drow["firmname"] + esc.ESC + "F ", 48) + esc.CR+esc.LF;
					//head +=esc.ES+"M";
				}
				if(drow["address"].ToString().Length > 0)
				{
					head += gv.PadCenter(drow["address"].ToString(), 48) +esc.CR+esc.LF;
				}
				if(drow["vatno"].ToString().Length > 0)
				{	
					head += gv.PadCenter("TIN:" + drow["vatno"].ToString(), 48)+esc.CR+esc.LF;
				}
				if(drow["phone1"].ToString().Length > 0)
				{
					head += gv.PadCenter("PHONE:"+drow["phone1"].ToString(), 48)+esc.CR+esc.LF;
				}
				if(drow["phone2"].ToString().Length > 0)
				{
					head += gv.PadCenter("      "+drow["phone2"].ToString(), 48)+esc.CR+esc.LF;
				}
				if(drow["phone3"].ToString().Length > 0)
				{
					head += gv.PadCenter("      "+drow["phone3"].ToString(), 48)+esc.CR+esc.LF;
				}
				head += "-".PadRight(48,'-')+esc.CR+esc.LF;
				head += Convert.ToDateTime(drow["billdate"]).ToString("DATE dd/MM/yyyy AT HH:mm:ss");
				head += bno.PadLeft(18,' ') +esc.CR+esc.LF;
				head += "-".PadRight(48,'-')+esc.CR+esc.LF;
				head +=	"SN.DESCRIPTION               QTY    RATE  AMOUNT" + esc.CR+esc.LF ;
				head += "-".PadRight(48,'-')+esc.CR+esc.LF;
				cashdiscount = Convert.ToDouble(gv.ds.Tables[0].Rows[0]["cashdiscount"]);
				gv.sql = "SELECT sale.qty, sale.includerate, "+
						 "sale.excluderate, sale.taxamount, "+
						 "sale.surchargeamount, sale.amount, "+
						 "itemb.batchno, itemb.purchaseprice, "+
						 "itemb.saleprice, itemb.mrp, "+
						 "itemb.mfgdate, itemb.pkgdate, "+
						 "itemb.expdate, itemb.itemid, "+
						 "item.itemname, item.barcode, "+
						 "item.saleac, item.purchaseac, "+
						 "spacc.head, spacc.taxrate, "+
						 "spacc.surchargerate, spacc_1.head, "+
						 "spacc_1.taxrate, spacc_1.surchargerate "+
						 "FROM spacc AS spacc_1 INNER JOIN (spacc INNER JOIN (item INNER JOIN (itemb INNER JOIN sale ON itemb.id = sale.batchid) ON item.id = itemb.itemid) ON spacc.id = item.saleac) ON spacc_1.id = item.purchaseac "+
						 "where sale.invoiceid = "+id;
				Debug.WriteLine(gv.sql);
				gv.adap = new OleDbDataAdapter(gv.sql,gv.conn);
				DataSet detail = new DataSet();
				gv.adap.Fill(detail,"billdata");
				int i = 0;
				double amt = 0;
				foreach(DataRow row in detail.Tables[0].Rows)
				{
					double q=Convert.ToDouble(row["qty"]);
					double r=0;
					if(inclusive)
					{
						r=Convert.ToDouble(row["includerate"]);
					}
					if(!inclusive)
					{
						r=Convert.ToDouble(row["excluderate"]);
					}
					double a = q*r;
				
					i++;
					head += i.ToString().PadLeft(3,' ');
					head += row["itemname"].ToString().PadRight(22,' ');
					head += q.ToString("0.000").PadLeft(7,' ');
					head += r.ToString("0.00").PadLeft(8,' ');
					head += a.ToString("0.00").PadLeft(8 ,' ')+esc.CR+esc.LF;
					amt += a;
				}
				head += "-".PadRight(48,'-')+esc.CR+esc.LF;
				head +=	"VAT TAX INCLUDED.".PadRight(28,' ')+"TOTAL AMOUNT"+amt.ToString("0.00").PadLeft(8,' ')+esc.CR+esc.LF;
				if(cashdiscount>0)
				{
					head += " ".PadRight(27,' ')+"LESS DISCOUNT"+(cashdiscount*-1).ToString("0.00").PadLeft(8, ' ')+esc.CR+esc.LF;
				}
				double iv = amt-cashdiscount;
				head += " ".PadRight(27,' ')+"INVOICE VALUE"+esc.ESC+"G"+iv.ToString("0.00").PadLeft(8,' ')+esc.ESC+"H"+esc.CR+esc.LF;
				string w = "RUPEES "+gv.ntoc((long)iv)+"ONLY.";
				head += w + esc.CR+esc.LF;
				gv.sql = "SELECT spacc.shortname, spacc.taxrate, "+
						 "spacc.surchargerate, spacc.type, "+
						 "Sum(sale.taxamount) AS [Tax Amount], Sum(sale.surchargeamount) AS [Surcharge Amount], "+
						 "Sum(sale.amount) AS [Amount] "+
						 "FROM spacc INNER JOIN sale ON spacc.id = sale.saleac "+
						 "WHERE sale.invoiceid="+id+" "+
						 "GROUP BY spacc.shortname, spacc.taxrate, spacc.surchargerate, spacc.type";
				Debug.WriteLine(gv.sql);
				gv.adap = new OleDbDataAdapter(gv.sql,gv.conn);
				gv.adap.Fill(detail,"taxwise");
				foreach(DataRow row in detail.Tables[1].Rows)
				{
					head += row["shortname"].ToString().PadRight(7,' ')+Convert.ToDouble(row["Tax Amount"]).ToString("0.00").PadLeft(8,' ');
					head += "SURCHARGE"+Convert.ToDouble(row["Surcharge Amount"]).ToString("0.00").PadLeft(8,' ');
					head += "AMOUNT"+Convert.ToDouble(row["Amount"]).ToString("0.00").PadLeft(8,' ')+esc.CR+esc.LF;
				}
				head += esc.GS+"V"+(char)1;
			}						
			PrintDialog pd = new PrintDialog();
			if(pd.ShowDialog(this)==DialogResult.OK)
			{
				RawPrinterHelper.SendStringToPrinter(pd.PrinterSettings.PrinterName,head);
				
//				posprinter pp = new posprinter();
//				pp.print(head);

				File.WriteAllText(Application.StartupPath+"\\Temp\\output.txt",head);
				
				ProcessStartInfo startInfo = new ProcessStartInfo(Application.StartupPath+"\\dw.exe");
				startInfo.Arguments = "/pdf "+Application.StartupPath+"\\Temp\\output.txt";
				Process.Start(startInfo);

//				startInfo = new ProcessStartInfo("chrome.exe");
//				startInfo.Arguments = Application.StartupPath+"\\temp\\output.txt.pdf";
//				Process.Start(startInfo);
		
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
	void cellvalidating(object sender , DataGridViewCellValidatingEventArgs e)
	{
		dgv.Rows[e.RowIndex].ErrorText = "";
		string headertext = dgv.Columns[e.ColumnIndex].HeaderText;
		double qty = 0;
		double incrate=0;
		double excrate=0;
		double taxrate=0;
		double surrate=0;
		double taxamt=0;
		double suramt=0;
		double amount=0;
		double totamt = 0;
		double tottax = 0;
		double totsur = 0;
		double casdis = 0;
		
		if(dgv.Rows[e.RowIndex].IsNewRow)
		{
			return;
		}
		if(headertext == "Bar Code")
		{
			selectitem(dgv.CurrentRow,"",e.FormattedValue.ToString());
			selectbatch(dgv.CurrentRow,"",dgv.CurrentRow.Cells["MRP"].Value.ToString());
		}
		
		if(headertext == "Quantity")
		{	
			if (!double.TryParse(e.FormattedValue.ToString(),out qty))
    		{
    		    e.Cancel = true;
    		    dgv.Rows[e.RowIndex].ErrorText = "the value must be a number";
    		}
		}
		if(headertext =="Rate")
		{
			if (!double.TryParse(e.FormattedValue.ToString(),out excrate))
			{
				e.Cancel = true;
    		    dgv.Rows[e.RowIndex].ErrorText = "the value must be a number";
			}		
		}
		if(headertext =="Rate + Tax")
		{	
			if (!double.TryParse(e.FormattedValue.ToString(),out incrate))
			{
				e.Cancel = true;
    		    dgv.Rows[e.RowIndex].ErrorText = "the value must be a number";
			}	
		}
		double.TryParse(dgv.Rows[e.RowIndex].Cells["Tax Rate"].Value.ToString(),out taxrate);
		double.TryParse(dgv.Rows[e.RowIndex].Cells["Surcharge Rate"].Value.ToString(),out surrate);
		double.TryParse(dgv.Rows[e.RowIndex].Cells["Quantity"].Value.ToString(),out qty);
		double.TryParse(dgv.Rows[e.RowIndex].Cells["Rate"].Value.ToString(),out excrate);
		double.TryParse(dgv.Rows[e.RowIndex].Cells["Rate + Tax"].Value.ToString(),out incrate);		
		if(!inclusive)
		{
			double i = excrate * taxrate/100;
			double j = i * surrate/100;
			incrate = excrate+i+j;
			dgv.Rows[e.RowIndex].Cells["Rate + Tax"].Value = incrate.ToString("0.0000");
		}
		if(inclusive)
		{	
			double i = taxrate*surrate/100;
			double j = i+taxrate;
			double k = 100+j;
			excrate =(incrate/k)*100;
			dgv.Rows[e.RowIndex].Cells["Rate"].Value = excrate.ToString("0.0000");
		}
	
		amount = qty*excrate;
		dgv.Rows[e.RowIndex].Cells["Amount"].Value = amount.ToString("0.00");
			
		taxamt = amount * taxrate/100;
		dgv.Rows[e.RowIndex].Cells["Tax Amount"].Value = taxamt.ToString("0.00");
			
		suramt = taxamt * surrate/100;
		dgv.Rows[e.RowIndex].Cells["Surcharge Amount"].Value = suramt.ToString("0.00");
		
		foreach(DataGridViewRow dgvrow in dgv.Rows)
		{
			if(dgvrow.IsNewRow)
				continue;
			
			double i;
			double.TryParse(dgvrow.Cells["Amount"].Value.ToString(),out i);
			totamt+=i;
			
			double j;			
			double.TryParse(dgvrow.Cells["Tax Amount"].Value.ToString(),out j);
			tottax+=j;
			
			double k;
			double.TryParse(dgvrow.Cells["Surcharge Amount"].Value.ToString(),out k);
			totsur+=k;
		}
		lbl13.Text = totamt.ToString("0.00");
		lbl15.Text = tottax.ToString("0.00");
		lbl17.Text = totsur.ToString("0.00");
		double.TryParse(tb2.Text,out casdis);
		lbl21.Text = (totamt+tottax+totsur-casdis).ToString("0.00");			
	}
	
	void editingcontrolshowing(object sender , DataGridViewEditingControlShowingEventArgs e)
	{
		TextBox tbc = e.Control as TextBox;
		if(dgv.CurrentCell.OwningColumn.Name=="Item Name")
		{
			selectitem(dgv.CurrentRow,tbc.Text);	
			tbc.Text = dgv.CurrentCell.Value.ToString();
		}
		if(dgv.CurrentCell.OwningColumn.Name=="Batch Number")
		{
			selectbatch(dgv.CurrentRow,tbc.Text);	
			tbc.Text = dgv.CurrentCell.Value.ToString();
		}
	}

	void selectbatch(DataGridViewRow dgvrow,string previousitem)
	{
		gv.sql = "select itemb.batchno as [Batch Number], itemb.purchaseprice, "+
				 "itemb.saleprice, itemb.mrp, "+
				 "itemb.mfgdate, itemb.pkgdate, "+
				 "itemb.expdate, itemb.lock, "+
				 "itemb.id "+
				 "FROM itemb "+
				 "where itemb.itemid ="+dgvrow.Cells["itemid"].Value;
		Debug.WriteLine(gv.sql);
		DataTable dt = new DataTable();
		searchbox.Show("Select Batch",this,gv.sql,ref dt,previousitem);
		dgvrow.Cells["batchid"].Value = dt.Rows[0]["id"].ToString();
		dgvrow.Cells["Batch Number"].Value = dt.Rows[0]["Batch Number"].ToString();
		dgvrow.Cells["purchaseprice"].Value = dt.Rows[0]["purchaseprice"].ToString();
		dgvrow.Cells["saleprice"].Value = dt.Rows[0]["saleprice"].ToString();
		dgvrow.Cells["MRP"].Value = dt.Rows[0]["mrp"].ToString();
		dgvrow.Cells["mfgdate"].Value = dt.Rows[0]["mfgdate"].ToString();
		dgvrow.Cells["pkgdate"].Value = dt.Rows[0]["pkgdate"].ToString();
		dgvrow.Cells["expdate"].Value = dt.Rows[0]["expdate"].ToString();
		dgvrow.Cells["lock"].Value = dt.Rows[0]["lock"].ToString();
		if(inclusive)
		{
			dgvrow.Cells["Rate + Tax"].Value = dt.Rows[0]["saleprice"].ToString();
		}
		if(!inclusive)
		{
			dgvrow.Cells["Rate"].Value = dt.Rows[0]["saleprice"].ToString();
		}
	}
	void selectbatch(DataGridViewRow dgvrow,string previousitem,string mrp)
	{
		try
		{
			gv.conn.Open();
			gv.ds = new DataSet();
			gv.sql = "select itemb.batchno, itemb.purchaseprice, "+
					 "itemb.saleprice, itemb.mrp, "+
					 "itemb.mfgdate, itemb.pkgdate, "+
					 "itemb.expdate, itemb.lock, "+
					 "itemb.id "+
					 "from itemb "+
					 "where itemb.itemid = "+dgvrow.Cells["itemid"].Value+" and itemb.batchno = '"+mrp+"'"; 
			Debug.WriteLine(gv.sql);
			gv.adap = new OleDbDataAdapter();
			gv.adap.SelectCommand = new OleDbCommand(gv.sql,gv.conn);
			gv.adap.Fill(gv.ds,"batch");
			if(gv.ds.Tables[0].Rows.Count>0)
			{
				dgvrow.Cells["batchid"].Value = gv.ds.Tables[0].Rows[0]["id"].ToString();
				dgvrow.Cells["Batch Number"].Value = gv.ds.Tables[0].Rows[0]["batchno"].ToString();
				dgvrow.Cells["purchaseprice"].Value = gv.ds.Tables[0].Rows[0]["purchaseprice"].ToString();
				dgvrow.Cells["saleprice"].Value = gv.ds.Tables[0].Rows[0]["saleprice"].ToString();
				dgvrow.Cells["MRP"].Value = gv.ds.Tables[0].Rows[0]["mrp"].ToString();
				dgvrow.Cells["mfgdate"].Value = gv.ds.Tables[0].Rows[0]["mfgdate"].ToString();
				dgvrow.Cells["pkgdate"].Value = gv.ds.Tables[0].Rows[0]["pkgdate"].ToString();
				dgvrow.Cells["expdate"].Value = gv.ds.Tables[0].Rows[0]["expdate"].ToString();
				dgvrow.Cells["lock"].Value = gv.ds.Tables[0].Rows[0]["lock"].ToString();
				if(inclusive)
				{
					dgvrow.Cells["Rate + Tax"].Value = gv.ds.Tables[0].Rows[0]["saleprice"].ToString();
				}
				if(!inclusive)
				{
					dgvrow.Cells["Rate"].Value = gv.ds.Tables[0].Rows[0]["saleprice"].ToString();
				}
			}
			else
			{
				SystemSounds.Beep.Play();
				lbl22.Text = "Error Mrp wise batch not found please select batch manualy";
			}
			dgvrow.Cells["Quantity"].Value = "1";
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
	void selectitem(DataGridViewRow dgvrow,string previousitem)
	{
		gv.sql = "select item.itemname, item.barcode, "+
				 "item.saleac, spacc_1.head, "+
				 "spacc_1.taxrate, spacc_1.surchargerate, "+
				 "item.id "+
				 "FROM spacc INNER JOIN (spacc AS spacc_1 INNER JOIN item ON spacc_1.id = item.saleac) ON spacc.id = item.purchaseac ";
		Debug.WriteLine(gv.sql);
		DataTable dt = new DataTable();
		searchbox.Show("Select Item",this,gv.sql,ref dt,previousitem);
		dgvrow.Cells["Bar Code"].Value = dt.Rows[0]["barcode"].ToString();
		dgvrow.Cells["Item Name"].Value = dt.Rows[0]["itemname"].ToString();
		dgvrow.Cells["itemid"].Value = dt.Rows[0]["id"].ToString();
		dgvrow.Cells["saleac"].Value = dt.Rows[0]["saleac"].ToString();
		dgvrow.Cells["Sale Account"].Value = dt.Rows[0]["head"].ToString();
		dgvrow.Cells["Tax Rate"].Value = dt.Rows[0]["taxrate"].ToString();
		dgvrow.Cells["Surcharge Rate"].Value = dt.Rows[0]["surchargerate"].ToString();	
	}
	void selectitem(DataGridViewRow dgvrow,string previousitem,string barcode)
	{
		try
		{
			gv.conn.Open();
			gv.ds = new DataSet();
			//item.purchaseac, spacc.head, spacc.taxrate, spacc.surchargerate
			gv.sql = "select item.id, item.itemname, "+
					 "item.saleac, item.purchaseprice, "+
					 "item.saleprice, item.mrp, "+
					 "spacc_1.head, spacc_1.taxrate, "+
					 "spacc_1.surchargerate "+
					 "FROM spacc INNER JOIN (spacc AS spacc_1 INNER JOIN item ON spacc_1.id = item.saleac) ON spacc.id = item.purchaseac "+
					 "where item.barcode = '"+barcode+"'";
			Debug.WriteLine(gv.sql);
			gv.adap = new OleDbDataAdapter();
			gv.adap.SelectCommand = new OleDbCommand(gv.sql,gv.conn);
			gv.adap.Fill(gv.ds,"item");
			if(gv.ds.Tables[0].Rows.Count>0)
			{
				dgvrow.Cells["Item Name"].Value = gv.ds.Tables[0].Rows[0]["itemname"].ToString();
				dgvrow.Cells["itemid"].Value = gv.ds.Tables[0].Rows[0]["id"].ToString();
				dgvrow.Cells["saleac"].Value = gv.ds.Tables[0].Rows[0]["saleac"].ToString();
				dgvrow.Cells["Sale Account"].Value = gv.ds.Tables[0].Rows[0]["head"].ToString();
				dgvrow.Cells["Tax Rate"].Value = gv.ds.Tables[0].Rows[0]["taxrate"].ToString();
				dgvrow.Cells["Surcharge Rate"].Value = gv.ds.Tables[0].Rows[0]["surchargerate"].ToString();
				string mrp = Convert.ToDouble("0"+gv.ds.Tables[0].Rows[0]["mrp"]).ToString();
				dgvrow.Cells["MRP"].Value = mrp;
				if(mrp=="0")
				{
					lbl22.Text = "Error: Mrp of Item is Zero!! Please update mrp of the item. Select batch manualy";
				}
				//if(inclusive)
				//{
				//	dgvrow.Cells["Rate + Tax"].Value = Convert.ToDouble("0"+gv.ds.Tables[0].Rows[0]["saleprice"]).ToString();
				//}
				//if(!inclusive)
				//{
				//	dgvrow.Cells["Rate"].Value = Convert.ToDouble("0"+gv.ds.Tables[0].Rows[0]["saleprice"]).ToString();
				//}
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
		tb3.Text = num + " of " + reccount;
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
		tb3.Text = num + " of " + reccount;
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
		tb3.Text = num + " of " + reccount;
		fillform();		
	}
	
	void lasclick(object sender , EventArgs e)
	{
		recstartstop();
		num = reccount;
		tb3.Text = num + " of " + reccount;
		fillform();
	}
	
	void newclick(object sender , EventArgs e)
	{		
		recstartstop();
		num = reccount;
		num++;
		tb3.Text = num + " of " + reccount;
		fillform();
		selectbillbook();
		selectbillnumber();
		billnum++;
		tb1.Text = billnum.ToString();
		selectparty();
	}
	
	void gotclick(object sender,EventArgs e)
	{
		recstartstop();
		num = Convert.ToInt32(tb3.Text);
		tb3.Text = num + " of " + reccount;
		fillform();
	}
	
	void selpartyclick(object sender,EventArgs e)
	{
		selectparty();
	}
	
	void selbbclick(object sender,EventArgs e)
	{
		selectbillbook();
	}
	void priclick(object sender,EventArgs e)
	{
		
		printpreview pp = new printpreview(ir.readstring("reportfiles","invoice"),"{invoice.id} = " + num);
		pp.MdiParent = this.MdiParent;
		pp.Show();
		//printinv();
	}
	void tb1validating(object sender , CancelEventArgs e)
	{
		billnum=Convert.ToInt32(tb1.Text);
		if(billnumberexists())
		{
			e.Cancel=true;
			err.SetError(tb1,"Bill Already Exists!! New number generated");
			selectbillnumber();
			billnum++;
			tb1.Text = billnum.ToString();
		}	
	}
	
	void tb1validated(object sender , EventArgs e)
	{
		err.SetError(tb1,"");
	}
}