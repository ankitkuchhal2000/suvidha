using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.ComponentModel;

public class party : Form
{
    ProgressBar pbr;
    BackgroundWorker bkw;

    Label lbl1, lbl2, lbl3, lbl4, lbl5, lbl6, lbl7, lbl8, lbl9, lbl10,
        lbl11, lbl12, lbl13, lbl14, lbl15, lbl16, lbl17, lbl18, lbl19, lbl20, lbl21, lbl22, lblpercent, lblprogress;

    TextBox tb1, tb2, tb3, tb4, tb5, tb6, tb7, tb8, tb9, tb10,
        tb11, tb12, tb13, tb14, tb15, tb16, tb17, tb18, tb19, tb20, tb21, tb22;

    Button btn1, btn2;

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
    int groupnumber
    {
        get;set;
    }
    public party(gv.mode md, int num)
	{
        Num = num;
        Mode = md;

        Size = new Size(500,500);
        AutoScroll = true;
        WindowState = FormWindowState.Maximized;
        Load += new EventHandler(load);

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

        lbl18 = new Label { Text = "GST Number", Location = new Point(0, 480), Parent = this };
        tb18 = new TextBox { Location = new Point(100, 480), Width = 300, Parent = this };

        lbl19 = new Label { Text = "Drug Lic. Number 20B", Location = new Point(0, 510), Parent = this };
        tb19 = new TextBox { Location = new Point(100, 510), Width = 300, Parent = this };

        lbl20 = new Label { Text = "Drug Lic. Number 21B", Location = new Point(0, 540), Parent = this };
        tb20 = new TextBox { Location = new Point(100, 540), Width = 300,  Parent = this };

        lbl21 = new Label { Text = "State Code", Location = new Point(0, 570), Parent = this };
        tb21 = new TextBox { Location = new Point(100, 570), Width = 300, Parent = this };

        lbl22 = new Label { Text = "Group Code", Location = new Point(0, 600), Parent = this };
        tb22 = new TextBox { Location = new Point(100, 600), Width = 300, Parent = this };

        pbr = new ProgressBar{ Visible = false, Location = new Point(0, 720), Width = 475, Parent = this };

        lblpercent = new Label{ Location = new Point(200, 750), Parent = this };

        lblprogress = new Label{ Location = new Point(200, 780), Parent = this };

        bkw = new BackgroundWorker { WorkerReportsProgress = true };
        bkw.DoWork += new DoWorkEventHandler(dowork);
		bkw.ProgressChanged += new ProgressChangedEventHandler(progresschanged);
		bkw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workcompleted);

        btn1 = new Button { Text = "SAVE", Location = new Point(0, 630), Parent = this };
        btn1.Click += new EventHandler(savclick);

        btn2 = new Button { Text = "DELETE", Location = new Point(100, 630), Parent = this };
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
    void load (object sender, EventArgs e)
    {
        select();
    }
    void delclick(object sender, EventArgs e)
    {

        delete();
        Close();
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
    void select()
    {
        try
        {
            gv.conn.Open();
            gv.ds = new DataSet();
            gv.sql = "select partyname as [Party Name], " +
                "address as [Address], address1 as [Address1], " +
                "station as [Station], phone1 as [Phone1], " +
                "phone2 as[Phone2], phone3 as [Phone3], " +
                "phone4 as[Phone4], phone5 as [Phone5], " +
                "phone6 as[Phone6], email as [Email], " +
                "email1 as[Email1],email2 as [Email2], " +
                "contactperson as[Contact Person], designation as [Designation], " +
                "stcstno as[STCST Number], vatnumber as [VAT Number], " +
                "dlno20b as[DLNO20B] ,dlno21b as [DLNO21B], " +
                "statecode as[State Code] " +
                "from party " +
                "where party.id = @id";
            Debug.WriteLine(gv.sql);
            gv.adap.SelectCommand = new OleDbCommand(gv.sql, gv.conn);
            gv.adap.SelectCommand.Parameters.AddWithValue("@id", Num);
            gv.adap.Fill(gv.ds, "party");

            if (gv.ds.Tables[0].Rows.Count > 0)
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
            gv.conn.Open();
            gv.ds = new DataSet();
            gv.sql = "select Max(id) as [Max],Min(id) as [Min] from party";
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
    void delete()
    {
        try
        {
            gv.conn.Open();
            gv.sql = "delete from  party where  id = " + Num;
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
    void update()
    {
        try
        {
            gv.conn.Open();
            gv.sql = "update party set partyname = @partyname, address=@address, address1=@address1, station=@station ,phone1=@phone1, phone2=@phone2, phone3=@phone3, phone4=@phone4, phone5=@phone5, phone6=@phone6, email=@email, email1=@email1, email2=@email2, contactperson=@contactperson, designation=@designation, stcstno=@stcstno, vatnumber=@vatnumber,gstnumber@gstnumber, dlno20b=@dlno20b, dlno21b=@dlno21b, statecode=@statecode where id = @id";
            Debug.WriteLine(gv.sql);
            gv.adap.UpdateCommand = new OleDbCommand(gv.sql, gv.conn);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@partyname", tb1.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@address", tb2.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@address1", tb3.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@station", tb4.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@phone1", tb5.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@phone2", tb6.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@phone3", tb7.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@phone4", tb8.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@phone5", tb9.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@phone6", tb10.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@email", tb11.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@email1", tb12.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@email2", tb13.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@contactperson", tb14.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@designation", tb15.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@stcstno", tb16.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@vatnumber", tb17.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@gstnumber", tb18.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@dlno20b", tb19.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@dlno21b", tb20.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@statecode", tb21.Text);
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
    void insert()
    {
        selectid();
        try
        {
            gv.conn.Open();
            gv.sql = "insert into party (id,partyname,address) values(@id,@partyname,@address)";
            Debug.WriteLine(gv.sql);
            gv.adap.InsertCommand = new OleDbCommand(gv.sql, gv.conn);
            gv.adap.InsertCommand.Parameters.AddWithValue("@id", Num);
            gv.adap.InsertCommand.Parameters.AddWithValue("@partyname", tb1.Text);
            gv.adap.InsertCommand.Parameters.AddWithValue("@address", tb2.Text);
            gv.adap.InsertCommand.Parameters.AddWithValue("@address1", tb3.Text);
            gv.adap.InsertCommand.Parameters.AddWithValue("@station", tb4.Text);
            gv.adap.InsertCommand.Parameters.AddWithValue("@phone1", tb5.Text);
            gv.adap.InsertCommand.Parameters.AddWithValue("@phone2", tb6.Text);
            gv.adap.InsertCommand.Parameters.AddWithValue("@phone3", tb7.Text);
            gv.adap.InsertCommand.Parameters.AddWithValue("@phone4", tb8.Text);
            gv.adap.InsertCommand.Parameters.AddWithValue("@phone5", tb9.Text);
            gv.adap.InsertCommand.Parameters.AddWithValue("@phone6", tb10.Text);
            gv.adap.InsertCommand.Parameters.AddWithValue("@email", tb11.Text);
            gv.adap.InsertCommand.Parameters.AddWithValue("@email1", tb12.Text);
            gv.adap.InsertCommand.Parameters.AddWithValue("@email2", tb13.Text);
            gv.adap.InsertCommand.Parameters.AddWithValue("@contactperson", tb14.Text);
            gv.adap.InsertCommand.Parameters.AddWithValue("@designation", tb15.Text);
            gv.adap.InsertCommand.Parameters.AddWithValue("@stcstno", tb16.Text);
            gv.adap.InsertCommand.Parameters.AddWithValue("@vatnumber", tb17.Text);
            gv.adap.InsertCommand.Parameters.AddWithValue("@gstnumber", tb18.Text);
            gv.adap.InsertCommand.Parameters.AddWithValue("@dlno20b", tb19.Text);
            gv.adap.InsertCommand.Parameters.AddWithValue("@dlno21b", tb20.Text);
            gv.adap.InsertCommand.Parameters.AddWithValue("@statecode", tb21.Text);
            gv.adap.InsertCommand.Parameters.AddWithValue("@groupcode", groupnumber);
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