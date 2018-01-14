using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.ComponentModel;

public class party:Form
{
	int recstart = 0;
	int reccount = 0;
	int num = 0;
	
	ProgressBar pbr;	
	BackgroundWorker bkw;
	
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
    Label lbl19;
    Label lbl20;

    Label lblpercent;
	Label lblprogress;
	
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
    TextBox tb16;
    TextBox tb17;
    TextBox tb18;
    TextBox tb19;
    TextBox tb20;
    TextBox tbnum;
	
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

    public party()
	{
		Size = new Size(500,500);
        AutoScroll = true;
        WindowState = FormWindowState.Maximized;

        lbl1 = new Label{ Text = "Party Name", Location = new Point(0, 0), Parent = this };
        tb1 = new TextBox{ Location = new Point(100, 0), Width = 300, Parent = this };

        lbl2 = new Label{ Text = "Address",Location = new Point(0, 30), Parent = this };
        tb2 = new TextBox{ Location = new Point(100, 30), Width = 300, Parent = this };

        lbl3 = new Label { Text = "Address1", Location = new Point(0, 60), Parent = this };
        tb3 = new TextBox { Location = new Point(100, 60), Width = 300, Parent = this };

        lbl4 = new Label { Text = "Station", Location = new Point(0, 90), Parent = this };
        tb4 = new TextBox { Location = new Point(100, 90), Width = 300, Parent = this };

        lbl5 = new Label { Text = "Phone1", Location = new Point(0, 120), Parent = this };
        tb5 = new TextBox { Location = new Point(100, 120), Width = 300, Parent = this };

        lbl6 = new Label { Text = "Phone2", Location = new Point(0, 120), Parent = this };
        tb6 = new TextBox  { Location = new Point(100, 120), Width = 300, Parent = this };

        lbl7 = new Label { Text = "Phone3", Location = new Point(0, 150), Parent = this };
        tb7 = new TextBox { Location = new Point(100, 150), Width = 300, Parent = this };

        lbl8 = new Label { Text = "Phone4", Location = new Point(0, 180), Parent = this };
        tb8 = new TextBox { Location = new Point(100, 180), Width = 300, Parent = this };

        lbl9 = new Label { Text = "Phone5", Location = new Point(0, 210), Parent = this };
        tb9 = new TextBox { Location = new Point(100, 210), Width = 300, Parent = this };

        lbl10 = new Label { Text = "Phone6", Location = new Point(0, 240), Parent = this };
        tb10 = new TextBox { Location = new Point(100, 240), Width = 300, Parent = this };

        lbl11 = new Label { Text = "email", Location = new Point(0, 270), Parent = this };
        tb11 = new TextBox { Location = new Point(100, 270), Width = 300, Parent = this };

        lbl12 = new Label { Text = "email1", Location = new Point(0, 300), Parent = this };
        tb12 = new TextBox { Location = new Point(100, 300), Width = 300, Parent = this };

        lbl13 = new Label { Text = "email2", Location = new Point(0, 330), Parent = this };
        tb13 = new TextBox { Location = new Point(100, 330), Width = 300, Parent = this };

        lbl14 = new Label { Text = "Contact Person", Location = new Point(0, 360), Parent = this };
        tb14 = new TextBox { Location = new Point(100, 360), Width = 300, Parent = this };

        lbl15 = new Label { Text = "Designation", Location = new Point(0, 390), Parent = this };
        tb15 = new TextBox { Location = new Point(100, 390), Width = 300, Parent = this };

        lbl16 = new Label { Text = "STCST Number", Location = new Point(0, 420), Parent = this };
        tb16 = new TextBox { Location = new Point(100, 420), Width = 300, Parent = this };

        lbl17 = new Label { Text = "VAT Number", Location = new Point(0, 450), Parent = this };
        tb17 = new TextBox { Location = new Point(100, 450), Width = 300, Parent = this };

        lbl18 = new Label { Text = "Drug Lic. Number 20B", Location = new Point(0, 480), Parent = this };
        tb18 = new TextBox { Location = new Point(100, 480), Width = 300, Parent = this };

        lbl19 = new Label { Text = "Drug Lic. Number 21B", Location = new Point(0, 510), Parent = this };
        tb19 = new TextBox { Location = new Point(100, 510), Width = 300,  Parent = this };

        lbl20 = new Label { Text = "State Code", Location = new Point(0, 540), Parent = this };
        tb20 = new TextBox { Location = new Point(100, 540), Width = 300, Parent = this };

        btn1 = new Button{Text = "First",Location = new Point(0, 630),Parent = this};
        btn1.Click += new EventHandler(firclick);

        btn2 = new Button{Text = "Previous",Location = new Point(100, 630),Parent = this};
        btn2.Click += new EventHandler(preclick);

        tbnum = new TextBox{ Location = new Point(200, 630), Width = 75, Parent = this };

        btn3 = new Button { Text = "Next", Location = new Point(300, 630), Parent = this };
        btn3.Click += new EventHandler(nexclick);

        btn4 = new Button {Text = "Last",Location = new Point(400, 630), Parent = this};
        btn4.Click += new EventHandler(lasclick);

        btn5 = new Button { Text = "Insert", Location = new Point(100, 660), Parent = this };
        btn5.Click += new EventHandler(insclick);

        btn6 = new Button{ Text = "Update", Location = new Point(200, 660), Parent = this };
        btn6.Click += new EventHandler(updclick);

        btn7 = new Button { Text = "Delete", Location = new Point(300, 660), Parent = this };
        btn7.Click += new EventHandler(delclick);

        btn8 = new Button { Text = "New", Location = new Point(200, 690), Parent = this };
        btn8.Click += new EventHandler(newclick);
		
        btn9 = new Button { Text = "Import", Location = new Point(300, 690), Parent = this };
        btn9.Click += new EventHandler(impclick);

        btn10 = new Button { Text = "Cancel Import", Location = new Point(400, 690), Parent = this };
        btn10.Click += new EventHandler(canimpclick);

        btn11 = new Button { Text = "Report", Location = new Point(500, 690), Parent = this };

        pbr = new ProgressBar{ Visible = false, Location = new Point(0, 720), Width = 475, Parent = this };

        lblpercent = new Label{ Location = new Point(200, 750), Parent = this };

        lblprogress = new Label{ Location = new Point(200, 780), Parent = this };

        bkw = new BackgroundWorker { WorkerReportsProgress = true };
        bkw.DoWork += new DoWorkEventHandler(dowork);
		bkw.ProgressChanged += new ProgressChangedEventHandler(progresschanged);
		bkw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workcompleted);

		recstartstop();
		num = reccount;
		num++;
		tbnum.Text=num+" of "+reccount;
		fillform();
	}
	void recstartstop()
	{
		try
		{
			gv.conn.Open();
			gv.ds = new DataSet();
			gv.sql = "select Max(id) as [Max],Min(id) as [Min] from party";
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
			gv.sql = "select partyname as [Party Name], "+
                "address as [Address], address1 as [Address1], "+
                "station as [Station], phone1 as [Phone1], "+
                "phone2 as[Phone2], phone3 as [Phone3], "+
                "phone4 as[Phone4], phone5 as [Phone5], "+
                "phone6 as[Phone6], email as [Email], " +
                "email1 as[Email1],email2 as [Email2], "+
                "contactperson as[Contact Person], designation as [Designation], " +
                "stcstno as[STCST Number], vatnumber as [VAT Number], "+
                "dlno20b as[DLNO20B] ,dlno21b as [DLNO21B], "+
                "statecode as[State Code] "+
                "from party " +
                "where party.id = " + num;
			Debug.WriteLine(gv.sql);
			gv.adap = new OleDbDataAdapter(gv.sql,gv.conn);
			gv.adap.Fill(gv.ds,"party");
			
			tb1.Text="";
			tb2.Text="";
			tb3.Text="";
			tb4.Text="";
			tb5.Text="";
			tb6.Text="";
			tb7.Text="";
			tb8.Text="";
			tb9.Text="";
			tb10.Text="";
			tb11.Text="";
			tb12.Text="";
			tb13.Text="";
			tb14.Text="";
			tb15.Text="";
			tb16.Text="";
			tb17.Text="";
            tb18.Text="";
            tb19.Text="";
            tb20.Text="";
			if(gv.ds.Tables[0].Rows.Count>0)
			{
				tb1.Text = gv.ds.Tables[0].Rows[0]["Party Name"].ToString();
                tb2.Text = gv.ds.Tables[0].Rows[0]["Address"].ToString();
                tb3.Text = gv.ds.Tables[0].Rows[0]["Address1"].ToString();
                tb4.Text = gv.ds.Tables[0].Rows[0]["Station"].ToString();
                tb5.Text = gv.ds.Tables[0].Rows[0]["Phone1"].ToString();
                tb6.Text = gv.ds.Tables[0].Rows[0]["Phone2"].ToString();
                tb7.Text = gv.ds.Tables[0].Rows[0]["Phone3"].ToString();
                tb8.Text = gv.ds.Tables[0].Rows[0]["Phone4"].ToString();
                tb9.Text = gv.ds.Tables[0].Rows[0]["Phone5"].ToString();
                tb10.Text = gv.ds.Tables[0].Rows[0]["Phone6"].ToString();
                tb11.Text = gv.ds.Tables[0].Rows[0]["Email"].ToString();
                tb12.Text = gv.ds.Tables[0].Rows[0]["Email1"].ToString();
                tb13.Text = gv.ds.Tables[0].Rows[0]["Email2"].ToString();
                tb14.Text = gv.ds.Tables[0].Rows[0]["Contact Person"].ToString();
                tb15.Text = gv.ds.Tables[0].Rows[0]["Designation"].ToString();
                tb16.Text = gv.ds.Tables[0].Rows[0]["STCST Number"].ToString();
                tb17.Text = gv.ds.Tables[0].Rows[0]["VAT Number"].ToString();
                tb18.Text = gv.ds.Tables[0].Rows[0]["DLNO20B"].ToString();
                tb19.Text = gv.ds.Tables[0].Rows[0]["DLNO21B"].ToString();
                tb20.Text = gv.ds.Tables[0].Rows[0]["State Code"].ToString();
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
    void delclick(object sender, EventArgs e)
    {
        try
        {
            gv.conn.Open();
            gv.ds = new DataSet();
            gv.sql = "delete from  party where  id = " + num;
            Debug.WriteLine(gv.sql);
            gv.adap = new OleDbDataAdapter();
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
        recstartstop();
        num = reccount;
        tbnum.Text = num + " of " + reccount;
        fillform();
    }

    void updclick(object sender, EventArgs e)
    {
        try
        {
            gv.conn.Open();
            gv.ds = new DataSet();
            gv.sql = "select id as [ID] from party where partyname = '" + tb1.Text + "'";
            Debug.WriteLine(gv.sql);
            gv.adap = new OleDbDataAdapter(gv.sql, gv.conn);
            gv.adap.Fill(gv.ds, "id");
            if (gv.ds.Tables[0].Rows.Count > 0)
            {
                int id = 0;
                if (gv.ds.Tables[0].Rows[0]["ID"].GetType() != typeof(DBNull))
                    id = Convert.ToInt32(gv.ds.Tables[0].Rows[0]["ID"]);
                if (id != 0 && id != num)
                {
                    MessageBox.Show("Party already exits");
                    return;
                }
            }
            gv.sql = "update party set partyname = '" + tb1.Text + "', address='"+tb2.Text +"', "+
                "address1='"+tb3.Text+"', station='"+tb4.Text+"' ,"+
                "phone1='"+tb5.Text+"', phone2='"+tb6.Text+"', "+
                "phone3='"+tb7.Text+"', phone4='"+tb8.Text+"', "+
                "phone5='"+tb9.Text+"', phone6='"+tb10.Text+"', "+
                "email='"+tb11.Text+"', email1='"+tb12.Text+"', "+
                "email2='"+tb13.Text+"', contactperson='"+tb14.Text+"', "+
                "designation='"+tb15.Text+"', stcstno='"+tb16.Text+"', "+
                "vatnumber='"+tb17.Text+"', dlno20b='"+tb18.Text+"', "+
                "dlno21b='"+tb19.Text+"', statecode='"+tb20.Text+"' "+
                "where id = " + num;
            Debug.WriteLine(gv.sql);
            gv.adap = new OleDbDataAdapter();
            gv.adap.UpdateCommand = new OleDbCommand(gv.sql, gv.conn);
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
        recstartstop();
        tbnum.Text = num + " of " + reccount;
        fillform();
    }

    void insclick(object sender, EventArgs e)
    {
        recstartstop();
        num = reccount;
        num++;
        try
        {
            gv.conn.Open();
            gv.ds = new DataSet();
            gv.sql = "select id as [ID] from party where partyname = '" + tb1.Text + "'";
            Debug.WriteLine(gv.sql);
            gv.adap = new OleDbDataAdapter(gv.sql, gv.conn);
            gv.adap.Fill(gv.ds, "id");
            if (gv.ds.Tables[0].Rows.Count > 0)
            {
                int id = 0;
                if (gv.ds.Tables[0].Rows[0]["ID"].GetType() != typeof(DBNull))
                    id = Convert.ToInt32(gv.ds.Tables[0].Rows[0]["ID"]);
                if (id != 0)
                {
                    num = id;
                    MessageBox.Show("Cannot create party with same name");
                }
            }
            gv.sql = "insert into party (id,partyname) values(" + num + ",'" + tb1.Text + "')";
            Debug.WriteLine(gv.sql);
            gv.adap = new OleDbDataAdapter();
            gv.adap.InsertCommand = new OleDbCommand(gv.sql, gv.conn);
            gv.adap.InsertCommand.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            MessageBox.Show(ex.Message);
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
	

	void impclick(object sender , EventArgs e)
	{
		try
		{
			FolderBrowserDialog fbd = new FolderBrowserDialog();
			DialogResult dr = fbd.ShowDialog();
			if(dr == DialogResult.OK)
			{
				string connstr = "";
				connstr = "provider = Microsoft.Jet.OLEDB.4.0;data source ="+fbd.SelectedPath+"\\;extended properties = dbase iv;";
				OleDbConnection dbfconn = new OleDbConnection(connstr);
				dbfconn.Open();
				gv.ds = new DataSet();
				gv.sql = "select * from party where pcode>0 and type = 'A'";
				Debug.WriteLine(gv.sql);
				OleDbDataAdapter dbfadap = new OleDbDataAdapter(gv.sql,dbfconn);
				dbfadap.Fill( gv.ds,"dbfparty");
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
				lblprogress.Text = dr["PNAME"].ToString().Replace("'","''");
				gv.sql = "insert into party (id,partyname) values(" + dr["PCODE"] + ",'" + lblprogress.Text + "')";
				Debug.WriteLine(gv.sql);
				gv.adap.InsertCommand = new OleDbCommand(gv.sql,gv.conn);
				gv.adap.InsertCommand.ExecuteNonQuery();				
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
		lblpercent.Text = pbr.Value+"/"+pbr.Maximum;
	}
	void workcompleted(object sender , RunWorkerCompletedEventArgs e)
	{
		if (e.Error != null)
		{
			Debug.WriteLine(e.Error.Message);
		}
		else if (e.Cancelled)
		{
			lblprogress.Text = "Cancelled";
			Debug.WriteLine("Cancelled");
		}
		else
		{
			lblprogress.Text = e.Result.ToString();
			Debug.WriteLine(e.Result.ToString());
		}
		pbr.Visible = false;
		System.Threading.Thread.Sleep(100);
		lblpercent.Text="";
		lblprogress.Text="";
	}
}