using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Diagnostics; 
using System.ComponentModel;

public class claim:Form
{
	int recstart = 0;
	int reccount = 0;
	int num = 0;
	
	Label lbl1;
	Label lbl2;
	Label lbl3;
	Label lbl4;
	Label lbl5;
	Label lbl6;
	Label lbl7;
	Label lbl8;
	Label lbl9;
	Label lbl10;
	Label lbl11;
	Label lbl12;
	Label lbl13;
	Label lbl14;
	Label lbl15;
	Label lbl16;
	Label lbl17;
	Label lbl18;
	
	TextBox tb1;
	TextBox tb2;
	TextBox tb3;
	TextBox tb4;
	TextBox tb5;
	TextBox tb6;
	TextBox tb7;
	TextBox tb8;
	TextBox tb9;
	TextBox tb10;
	TextBox tb11;
	TextBox tb12;
	TextBox tb13;
	TextBox tb14;	
	TextBox tb15;
	
	DateTimePicker dtp1;
	DateTimePicker dtp2;
	DateTimePicker dtp3;
	DateTimePicker dtp4;

	Button btn1;
	Button btn2;
	Button btn3;
	Button btn4;
	Button btn5;
	Button btn6;
	Button btn7;
	Button btn8;
	Button btn9;
	
	DataGridView dgv;
	
	MainMenu mm;
	
	public claim()
	{
		MenuItem mi1 = new MenuItem("&Claim Menu");
		mi1.MenuItems.Add(new MenuItem("&Print",new EventHandler(priclick)));
		
		mm = new MainMenu();
		mm.MenuItems.Add(mi1);
		
		Menu = mm;
		Size = new Size(620,530);
		WindowState = FormWindowState.Maximized;
		
		lbl1 = new Label();
		lbl1.Text = "Name of Claiment";
		lbl1.Location = new Point(0,0);
		lbl1.Parent = this;
		
		lbl2 = new Label();
		lbl2.Text = "Father Name";
		lbl2.Location = new Point(300,0);
		lbl2.Parent = this;
		
		lbl3 = new Label();
		lbl3.Text = "Address";
		lbl3.Location = new Point(0,30);
		lbl3.Parent = this;
		
		lbl4 = new Label();
		lbl4.Text = "Designation";
		lbl4.Location=new Point(300,30);
		lbl4.Parent=this;
		
		lbl5 = new Label();
		lbl5.Text = "Department";
		lbl5.Location = new Point(0,60);
		lbl5.Parent = this;
		
		lbl6 = new Label();
		lbl6.Text = "Office of";
		lbl6.Location = new Point(300,60);
		lbl6.Parent = this;
		
		lbl7 = new Label();
		lbl7.Text = "Pay";
		lbl7.Location = new Point(0,90);
		lbl7.Parent = this;
		
		lbl8 = new Label();
		lbl8.Text = "Relation";
		lbl8.Location = new Point(300,90);
		lbl8.Parent = this;
		
		lbl9 = new Label();
		lbl9.Text = "Patient Name";
		lbl9.Location = new Point(0,120);
		lbl9.Parent = this;
		
		lbl10 = new Label();
		lbl10.Text = "Hospital Name";
		lbl10.Location = new Point(300,120);
		lbl10.Parent = this;
		
		lbl11 = new Label();
		lbl11.Text = "Disease Name";
		lbl11.Location = new Point(0,150);
		lbl11.Parent = this;
		
		lbl12 = new Label();
		lbl12.Text = "Period of Treatment";
		lbl12.Location = new Point(300,150);
		lbl12.Parent = this;
		
		lbl13 = new Label();
		lbl13.Text = "Treatment Started on";
		lbl13.Location = new Point(0,180);
		lbl13.Parent = this;
		
		lbl14 = new Label();
		lbl14.Text = "Treatment Ended on";
		lbl14.Location = new Point(300,180);
		lbl14.Parent = this;
		
		lbl15 = new Label();
		lbl15.Text = "Indoor No";
		lbl15.Location = new Point(0,210);
		lbl15.Parent = this;		
		
		lbl16 = new Label();
		lbl16.Text = "Indoor Date";
		lbl16.Location = new Point(300,210);
		lbl16.Parent = this;				
		
		lbl17 = new Label();
		lbl17.Text = "Outdoor No";
		lbl17.Location = new Point(0,240);
		lbl17.Parent = this;				
		
		lbl18 = new Label();
		lbl18.Text = "Outdoor Date";
		lbl18.Location = new Point(300,240);
		lbl18.Parent = this;				
		
		tb1 = new TextBox();
		tb1.Location = new Point(100,0);
		tb1.Parent=this;
		
		tb2 = new TextBox();
		tb2.Location = new Point(400,0);
		tb2.Parent=this;
		
		tb3 = new TextBox();
		tb3.Location = new Point(100,30);
		tb3.Parent=this;
		
		tb4 = new TextBox();
		tb4.Location = new Point(400,30);
		tb4.Parent=this;
		
		tb5 = new TextBox();
		tb5.Location = new Point(100,60);
		tb5.Parent=this;

		tb6 = new TextBox();
		tb6.Location = new Point(400,60);
		tb6.Parent=this;
		
		tb7 = new TextBox();
		tb7.Location = new Point(100,90);
		tb7.Parent=this;

		tb8 = new TextBox();
		tb8.Location = new Point(400,90);
		tb8.Parent=this;

		tb9 = new TextBox();
		tb9.Location = new Point(100,120);
		tb9.Parent=this;		
		
		tb10 = new TextBox();
		tb10.Location = new Point(400,120);
		tb10.Parent=this;
		
		tb11 = new TextBox();
		tb11.Location = new Point(100,150);
		tb11.Parent=this;

		tb12 = new TextBox();
		tb12.Location = new Point(400,150);
		tb12.Parent=this;		

		dtp1 = new DateTimePicker();
		dtp1.Location = new Point(100,180);
		dtp1.Parent=this; 

		dtp2 = new DateTimePicker();
		dtp2.Location = new Point(400,180);
		dtp2.Parent=this;
		
		tb13 = new TextBox();
		tb13.Location = new Point(100,210);
		tb13.Parent=this;

		dtp3 = new DateTimePicker();
		dtp3.Location = new Point(400,210);
		dtp3.Parent=this;

		tb14 = new TextBox();
		tb14.Location = new Point(100,240);
		tb14.Parent=this;		
		
		dtp4 = new DateTimePicker();
		dtp4.Location = new Point(400,240);
		dtp4.Parent=this;

		dgv=new DataGridView();
		dgv.Location=new Point(0,270);
		dgv.Size = new Size(600,110);
		dgv.CellFormatting += new DataGridViewCellFormattingEventHandler(cellformatting);
		dgv.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(editingcontrolshowing);
		dgv.Parent=this;

		btn1 = new Button();
		btn1.Text = "&First";
		btn1.Location = new Point(0,390);
		btn1.Click += new EventHandler(firclick);
		btn1.Parent = this;

		btn2 = new Button();
		btn2.Text = "p&Revious";
		btn2.Location = new Point(100,390);
		btn2.Click += new EventHandler(preclick);
		btn2.Parent = this;
		
		tb15 = new TextBox();
		tb15.Location = new Point(200,390);
		tb15.Width = 75;
		tb15.Parent = this;
		
		btn3 = new Button();
		btn3.Text = "n&Ext";
		btn3.Location = new Point(300,390);
		btn3.Click += new EventHandler(nexclick);
		btn3.Parent = this;		
		
		btn4 = new Button();
		btn4.Text = "&Last";
		btn4.Location = new Point(400,390);
		btn4.Click += new EventHandler(lasclick);
		btn4.Parent = this;
		
		btn5 = new Button();
		btn5.Text = "&Insert";
		btn5.Location = new Point(100,420);
		btn5.Click += new EventHandler(insclick);
		btn5.Parent = this;
		
		btn6 = new Button();
		btn6.Text = "&Update";
		btn6.Location = new Point(200,420);
		btn6.Click += new EventHandler(updclick);
		btn6.Parent = this;
		
		btn7 = new Button();
		btn7.Text = "&Delete";
		btn7.Location = new Point(300,420);
		btn7.Click += new EventHandler(delclick);
		btn7.Parent = this;

		btn8 = new Button();
		btn8.Text = "&New";
		btn8.Location = new Point(200,450);
		btn8.Click += new EventHandler(newclick);
		btn8.Parent = this;	
		
		btn9 = new Button();
		btn9.Text = "&Goto";
		btn9.Location = new Point(200,480);
		btn9.Click += new EventHandler(gotclick);
		btn9.Parent = this;
		
		recstartstop();
		num = reccount;
		num++;
		tb15.Text = num + " of " + reccount;
		fillform();		
	}
	
	void cellformatting(object sender , DataGridViewCellFormattingEventArgs e)
	{
		if(dgv.Columns[e.ColumnIndex].Name=="Prescription Date"||dgv.Columns[e.ColumnIndex].Name=="Bill Date")
		{
			e.CellStyle.Format="dd/MMM/yyyy";
		}
	}
	void editingcontrolshowing(object sender , DataGridViewEditingControlShowingEventArgs e)
	{
		TextBox tbc = e.Control as TextBox;
		if(dgv.CurrentCell.OwningColumn.Name=="Item Name")
		{
			gv.sql = "select itemname as [Item Name],id from item";
			DataTable dt = new DataTable();
			searchbox.Show("Select Item",this,gv.sql,ref dt,tbc.Text);
			tbc.Text=dt.Rows[0]["Item Name"].ToString();
			dgv.CurrentRow.Cells["Item Name"].Value = dt.Rows[0]["Item Name"].ToString();
			dgv.CurrentRow.Cells["Id"].Value = dt.Rows[0]["id"].ToString();
		}
	}
	void recstartstop()
	{
		try
		{
			gv.conn.Open();
			gv.ds = new DataSet();
			gv.sql = "select Max(id) as [Max],Min(id) as [Min] from claim";
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
			gv.sql = " select claimentname as [Claiment Name],fathername as [Father Name],"+
			"address as [Address],designation as[Designation], "+
			"department as [Department],officeof as [Office Of], "+
			"pay as [Pay],relation as [Relation], "+
			"patientname as [Patient Name],hospitalname as[Hospital Name], "+
			"diseasename as [Disease Name],periodoftreatment as [Period Of Treatment], "+
			"treatmentstartedon as [Treatment Started On] ,treatmentendedon as[Treatment Ended On], "+
			"indoorno as [Indoor No] ,indoordate as [Indoor Date] , "+
			"outdoorno as [Outdoor No],outdoordate as [Outdoor Date]"+
			" from claim "+
			" where claim.id =" + num;
			Debug.WriteLine(gv.sql);
			gv.adap = new OleDbDataAdapter(gv.sql,gv.conn);
			gv.adap.Fill(gv.ds,"claim");
			
			gv.sql = " select item.itemname as [Item Name],claimdata.quantity as [Quantity],"+
			"claimdata.rate as [Rate],claimdata.billnumber as [Bill Number],"+
			"claimdata.billdate as [Bill Date],claimdata.prescriptiondate as [Prescription Date],"+
			"item.id as [Id]"+
			" from claimdata "+
			" left join item on claimdata.itemid=item.id "+
			" where claimdata.claimid =" + num;
			Debug.WriteLine(gv.sql);
			gv.adap = new OleDbDataAdapter(gv.sql,gv.conn);
			gv.adap.Fill(gv.ds,"claim data");
			
			tb1.Text = "";
			tb2.Text = "";
			tb3.Text = "";
			tb4.Text = "";
			tb5.Text = "";
			tb6.Text = "";
			tb7.Text = "";
			tb8.Text = "";
			tb9.Text = "";
			tb10.Text = "";			
			tb11.Text = "";
			tb12.Text = "";
			dtp1.Value = DateTime.Now;
			dtp2.Value = DateTime.Now;
			tb13.Text = "";
			dtp3.Value = DateTime.Now;
			tb14.Text ="";
			dtp4.Value = DateTime.Now;
			dgv.DataSource = null;
			if(gv.ds.Tables[0].Rows.Count>0)
			{
				tb1.Text = gv.ds.Tables[0].Rows[0]["Claiment Name"].ToString();
				tb2.Text = gv.ds.Tables[0].Rows[0]["Father Name"].ToString();
				tb3.Text = gv.ds.Tables[0].Rows[0]["Address"].ToString();
				tb4.Text = gv.ds.Tables[0].Rows[0]["Designation"].ToString();
				tb5.Text = gv.ds.Tables[0].Rows[0]["Department"].ToString();
				tb6.Text = gv.ds.Tables[0].Rows[0]["Office Of"].ToString();
				tb7.Text = gv.ds.Tables[0].Rows[0]["Pay"].ToString();
				tb8.Text = gv.ds.Tables[0].Rows[0]["Relation"].ToString();
				tb9.Text = gv.ds.Tables[0].Rows[0]["Patient Name"].ToString();
				tb10.Text = gv.ds.Tables[0].Rows[0]["Hospital Name"].ToString();			
				tb11.Text = gv.ds.Tables[0].Rows[0]["Disease Name"].ToString();
				tb12.Text = gv.ds.Tables[0].Rows[0]["Period Of Treatment"].ToString();
				dtp1.Value = Convert.ToDateTime(gv.ds.Tables[0].Rows[0]["Treatment Started On"]);
				dtp2.Value = Convert.ToDateTime(gv.ds.Tables[0].Rows[0]["Treatment Ended On"]);
				tb13.Text = gv.ds.Tables[0].Rows[0]["Indoor No"].ToString();
				dtp3.Value = Convert.ToDateTime(gv.ds.Tables[0].Rows[0]["Indoor Date"]);
				tb14.Text = gv.ds.Tables[0].Rows[0]["Outdoor No"].ToString();
				dtp4.Value = Convert.ToDateTime(gv.ds.Tables[0].Rows[0]["Outdoor Date"]);
			}
			dgv.DataSource = gv.ds.Tables[1];
			//gv.ds.WriteXml("data.xml");
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
		tb15.Text = num + " of " + reccount;
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
		tb15.Text = num + " of " + reccount;
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
		tb15.Text = num + " of " + reccount;
		fillform();		
	}
	
	void lasclick(object sender , EventArgs e)
	{
		recstartstop();
		num = reccount;
		tb15.Text = num + " of " + reccount;
		fillform();
	}
	
	void newclick(object sender , EventArgs e)
	{		
		recstartstop();
		num = reccount;
		num++;
		tb15.Text = num + " of " + reccount;
		fillform();
	}
	
	void delclick(object sender , EventArgs e)
	{
		try
		{
			gv.conn.Open();
			gv.ds = new DataSet();
			gv.sql = " delete from  claimdata where  claimid = " + num ;
			Debug.WriteLine(gv.sql);
			gv.adap = new OleDbDataAdapter();
			gv.adap.DeleteCommand = new OleDbCommand(gv.sql,gv.conn);
			gv.adap.DeleteCommand.ExecuteNonQuery();
			
			gv.sql = " delete from  claim where  id = " + num ;
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
		tb15.Text = num + " of " + reccount;
		fillform();
	}
	
	void updclick(object sender , EventArgs e)
	{
		try
		{
			gv.conn.Open();
			gv.sql = "update claim set "+
			"claimentname = '" + tb1.Text + "',fathername ='"+tb2.Text+"',address='"+tb3.Text+"',designation='"+tb4.Text+"',"+
			"department = '" + tb5.Text + "',officeof='"+tb6.Text+"',pay='"+tb7.Text+"',relation='"+tb8.Text+"',"+
			"patientname= '" + tb9.Text + "',hospitalname='"+tb10.Text+"',diseasename='"+tb11.Text+"',periodoftreatment='"+tb12.Text+"',"+
			"treatmentstartedon=#"+dtp1.Value+"#,treatmentendedon=#"+dtp2.Value+"#,indoorno='"+tb13.Text+"',indoordate=#"+dtp3.Value+"#,"+
			"outdoorno='"+tb14.Text+"',outdoordate=#"+dtp4.Value+"#"+
			" where id = " + num;
			Debug.WriteLine(gv.sql);
			gv.adap = new OleDbDataAdapter();
			gv.adap.UpdateCommand = new OleDbCommand(gv.sql,gv.conn);
			gv.adap.UpdateCommand.ExecuteNonQuery();
			
			gv.sql = " delete from  claimdata where  claimid = " + num ;
			Debug.WriteLine(gv.sql);
			gv.adap = new OleDbDataAdapter();
			gv.adap.DeleteCommand = new OleDbCommand(gv.sql,gv.conn);
			gv.adap.DeleteCommand.ExecuteNonQuery();
			
			foreach(DataGridViewRow dgvrow in dgv.Rows)
			{
				if(dgvrow.IsNewRow)
					continue;
				gv.ds = new DataSet();
				int i = 0;
				gv.sql =" select max(id) as[Max] from claimdata ";
				Debug.WriteLine(gv.sql);
				gv.adap.SelectCommand = new OleDbCommand(gv.sql,gv.conn);
				gv.adap.Fill(gv.ds,"id");
				if(gv.ds.Tables[0].Rows.Count>0)
				{
					if(gv.ds.Tables[0].Rows[0]["Max"].GetType() != typeof(DBNull))
						i=Convert.ToInt32(gv.ds.Tables[0].Rows[0]["Max"] );
				}
				i++;
				gv.sql = " Insert into claimdata ("+
				"id,claimid,"+
				"itemid,quantity,"+
				"rate,billnumber,"+
				"billdate,prescriptiondate) "+
				"values ("+i+","+num+",0"+
				dgvrow.Cells["Id"].Value+",0"+dgvrow.Cells["Quantity"].Value+",0"+
				dgvrow.Cells["Rate"].Value+",'"+dgvrow.Cells["Bill Number"].Value+"',"+
				gv.AccessDateTime(dgvrow.Cells["Bill Date"].Value)+","+gv.AccessDateTime(dgvrow.Cells["Prescription Date"].Value)+")";
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
		tb15.Text = num + " of " + reccount;
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
			gv.sql = " insert into claim "+
			"(id,claimentname,fathername,address,"+
			"designation,department,officeof,pay,"+
			"relation,patientname,hospitalname,diseasename,"+
			"periodoftreatment,treatmentstartedon,treatmentendedon,indoorno,"+
			"indoordate,outdoorno,outdoordate)"+
			" values("+num+",'"+tb1.Text+"','"+tb2.Text+"','"+tb3.Text+"','"
			+tb4.Text+"','"+tb5.Text+"','"+tb6.Text+"','"+tb7.Text+"','"
			+tb8.Text+"','"+tb9.Text+"','"+tb10.Text+"','"+tb11.Text+"','"
			+tb12.Text+"',#"+dtp1.Value+"#,#"+dtp2.Value+"#,'"+tb13.Text+"',#"
			+dtp3.Value+"#,'"+tb14.Text+"',#"+dtp4.Value+"#)";
			Debug.WriteLine(gv.sql);
			gv.adap = new OleDbDataAdapter();
			gv.adap.InsertCommand = new OleDbCommand(gv.sql,gv.conn);
			gv.adap.InsertCommand.ExecuteNonQuery();
			
			foreach(DataGridViewRow dgvrow in dgv.Rows)
			{
				if(dgvrow.IsNewRow)
					continue;
				gv.ds = new DataSet();	
				int i = 0;
				gv.sql =" select max(id) as[Max] from claimdata ";
				Debug.WriteLine(gv.sql);
				gv.adap.SelectCommand = new OleDbCommand(gv.sql,gv.conn);
				gv.adap.Fill(gv.ds,"id");
				if(gv.ds.Tables[0].Rows.Count>0)
				{
					if(gv.ds.Tables[0].Rows[0]["Max"].GetType() != typeof(DBNull))
						i=Convert.ToInt32(gv.ds.Tables[0].Rows[0]["Max"] );
				}
				i++;
				gv.sql = " Insert into claimdata ("+
				"id,claimid,"+
				"itemid,quantity,"+
				"rate,billnumber,"+
				"billdate,prescriptiondate) "+
				"values ("+i+","+num+",0"+
				dgvrow.Cells["Id"].Value+",0"+dgvrow.Cells["Quantity"].Value+",0"+
				dgvrow.Cells["Rate"].Value+",'"+dgvrow.Cells["Bill Number"].Value+"',"+
				gv.AccessDateTime(dgvrow.Cells["Bill Date"].Value)+","+gv.AccessDateTime(dgvrow.Cells["Prescription Date"].Value)+")";
				
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
		num = reccount;
		tb15.Text = num + " of " + reccount;
		fillform();			
	}
	void priclick(object sender , EventArgs e)
	{
		recstartstop();
		//num = num;
		tb15.Text = num + " of " + reccount;
		fillform();
		printpreview pp = new printpreview(Application.StartupPath+"\\Reports\\bb.rpt","{claim.id} = " + num);
		pp.MdiParent = this.MdiParent;
		pp.Show();
	}
	void gotclick(object sender , EventArgs e)
	{
		recstartstop();
		num = Convert.ToInt32(tb15.Text);
		tb15.Text = num + " of " + reccount;
		fillform();
	}
}