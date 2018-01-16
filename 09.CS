using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Diagnostics;

public class Client:Form
{

    Label lbl1, lbl2, lbl3, lbl4, lbl5, lbl6, lbl7, lbl8, lbl9, lbl10,
        lbl11, lbl12, lbl13, lbl14, lbl15, lbl16, lbl17, lbl18, lbl19, lbl20,
        lbl21, lbl22, lbl23, lbl24, lbl25, lbl26, lbl27, lbl28;

    TextBox tb1, tb2, tb3, tb4, tb5, tb6, tb7, tb8, tb9, tb10,
        tb11, tb12, tb13, tb14, tb15, tb16, tb17, tb18, tb19, tb20,
        tb21, tb22, tb23, tb24, tb25, tb26;

    Button btn1, btn2, btn3, btn4;

    PictureBox pcb;

    DateTimePicker dtp1, dtp2;
	
	StatusBar sb;
    StatusBarPanel sp1, sp2;

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

    public Client( gv.mode md,int num)
	{
        Num = num;
        Mode = md;

        lbl1 = new Label { Location = new Point(0, 0), Text = "Firm Name", Parent = this };
        tb1 = new TextBox { Location = new Point(100, 0), Width = 300, Parent = this };

        lbl2 = new Label { Location = new Point(0, 30), Text = "Address", Parent = this };
        tb2 = new TextBox { Location = new Point(100, 30), Width = 300, Parent = this };

        lbl3 = new Label { Location = new Point(0, 60), Text = "Address1", Parent = this };
        tb3 = new TextBox { Location = new Point(100, 60), Width = 300, Parent = this };

        lbl4 = new Label { Location = new Point(0, 90), Text = "Email Address", Parent = this };
        tb4 = new TextBox { Location = new Point(100, 90), Width = 300, Parent = this };

        lbl5 = new Label { Location = new Point(0, 120), Text = "Email Password", Parent = this };
        tb5 = new TextBox { Location = new Point(100, 120), Width = 300, Parent = this };

        lbl6 = new Label { Location = new Point(0, 150), Text = "Phone 1", Parent = this };
        tb6 = new TextBox { Location = new Point(100, 150), Width = 300, Parent = this };

        lbl7 = new Label { Location = new Point(0, 180), Text = "Phone 2", Parent = this };
        tb7 = new TextBox { Location = new Point(100, 180), Width = 300, Parent = this };

        lbl8 = new Label { Location = new Point(0, 210), Text = "Phone 3", Parent = this };

        tb8 = new TextBox { Location = new Point(100, 210), Width = 300, Parent = this };

        lbl9 = new Label { Location = new Point(0, 240), Text = "Phone 4", Parent = this };

        tb9 = new TextBox { Width = 300, Location = new Point(100, 240), Parent = this };

        lbl10 = new Label { Location = new Point(0, 270), Text = "Phone 5", Parent = this };
        tb10 = new TextBox { Width = 300, Location = new Point(100, 270), Parent = this };

        lbl11 = new Label { Location = new Point(0, 300), Text = "Phone 6", Parent = this };
        tb11 = new TextBox { Width = 300, Location = new Point(100, 300), Parent = this };

        lbl12 = new Label { Location = new Point(0, 330), Text = "STCST Number", Parent = this };
        tb12 = new TextBox { Width = 300, Location = new Point(100, 330), Parent = this };

        lbl13 = new Label { Location = new Point(0, 360), Text = "VAT Number", Parent = this };
        tb13 = new TextBox { Width = 300, Location = new Point(100, 360), Parent = this };

        lbl14 = new Label { Location = new Point(0, 390), Text = "DL Number 20B", Parent = this };
        tb14 = new TextBox { Width = 300, Location = new Point(100, 390), Parent = this };

        lbl15 = new Label { Location = new Point(0, 420), Text = "DL Number 21B", Parent = this };
        tb15 = new TextBox { Width = 300, Location = new Point(100, 420), Parent = this };

        lbl16 = new Label { Location = new Point(0, 450), Text = "Company Logo", Parent = this };
        tb16 = new TextBox { Width = 300, Location = new Point(100, 450), Text = "", Parent = this };

        btn1 = new Button { Text = "Choose File", Location = new Point(400, 450), Parent = this };
        btn1.Click += new EventHandler(choclick);

        pcb = new PictureBox { Location = new Point(100, 480), BorderStyle = BorderStyle.FixedSingle, SizeMode = PictureBoxSizeMode.StretchImage, Width = 300, Height = 150, Parent = this };

        lbl17 = new Label { Location = new Point(0, 660), Text = "Company1", Parent = this };
        tb17 = new TextBox { Width = 300, Location = new Point(100, 660), Text = "", Parent = this };

        lbl18 = new Label { Location = new Point(0, 690), Text = "Company2", Parent = this };
        tb18 = new TextBox { Width = 300, Location = new Point(100, 690), Text = "", Parent = this };

        lbl19 = new Label { Location = new Point(0, 720), Text = "Company3", Parent = this };
        tb19 = new TextBox { Width = 300, Location = new Point(100, 720), Text = "", Parent = this };

        lbl20 = new Label { Location = new Point(0, 750), Text = "Company4", Parent = this };
        tb20 = new TextBox { Width = 300, Location = new Point(100, 750), Text = "", Parent = this };

        lbl21 = new Label { Location = new Point(0, 780), Text = "Jurisdiction", Parent = this };

        tb21 = new TextBox { Width = 300, Location = new Point(100, 780), Text = "", Parent = this };

        lbl22 = new Label { Location = new Point(0, 810), Text = "Brand Name", Parent = this };
        tb22 = new TextBox { Width = 300, Location = new Point(100, 810), Text = "", Parent = this };

        lbl23 = new Label { Location = new Point(0, 840), Text = "God Name", Parent = this };
        tb23 = new TextBox { Width = 300, Location = new Point(100, 840), Text = "", Parent = this };

        lbl24 = new Label { Location = new Point(0, 870), Text = "S.M.T.P. Server", Parent = this };
        tb24 = new TextBox { Width = 300, Location = new Point(100, 870), Text = "", Parent = this };

        lbl25 = new Label { Location = new Point(0, 900), Text = "Port", Parent = this };
        tb25 = new TextBox { Width = 300, Location = new Point(100, 900), Text = "587", Parent = this };

        lbl26 = new Label { Location = new Point(0, 930), Text = "Financial Year From", Parent = this };
        dtp1 = new DateTimePicker { Location = new Point(100, 930), Parent = this };

        lbl27 = new Label { Location = new Point(0, 960), Text = "Financial Year To", Parent = this };
        dtp2 = new DateTimePicker { Location = new Point(100, 960), Parent = this };

        lbl28 = new Label { Location = new Point(0, 990), Text = "GST Number", Parent = this };
        tb26 = new TextBox { Width = 300, Location = new Point(100, 990), Parent = this };
        
        btn2 = new Button { Text = "SAVE", Location = new Point(260, 1050), Parent = this };
        btn2.Click += new EventHandler(savclick);

        btn3 = new Button { Text = "DELETE", Location = new Point(260, 1050), Parent = this };
        btn3.Click += new EventHandler(delclick);

        btn4 = new Button { Text = "PRINT", Location = new Point(260, 1080), Parent = this };
        btn4.Click += new EventHandler(repclick);

        sp1 = new StatusBarPanel { AutoSize = StatusBarPanelAutoSize.Spring, Text = gv.listfile };
        sp2 = new StatusBarPanel { AutoSize = StatusBarPanelAutoSize.Spring, Text = gv.mdbfile };

        sb = new StatusBar { ShowPanels = true, Parent = this };
        sb.Panels.Add(sp1);
		sb.Panels.Add(sp2);

        if(Mode == gv.mode.newentry || Mode == gv.mode.modify)
        {
            btn2.Visible = true;
            btn3.Visible = false;
            btn4.Visible = false;
        }
        if(Mode == gv.mode.delete)
        {
            btn2.Visible = false;
            btn3.Visible = true;
            btn4.Visible = false;
        }
        if(Mode == gv.mode.view)
        {
            btn2.Visible = false;
            btn3.Visible = false;
            btn4.Visible = true;
        }

        Size = new Size(500, 500);
        WindowState = FormWindowState.Maximized;
        AutoScroll = true;
        Load += new EventHandler(load);
	}
    void load(object sender, EventArgs e)
    {
        if (Mode != gv.mode.newentry)
        {
            select();
        }
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
	void delclick(object sender , EventArgs e)
	{
        delete();
        Close();
	}
    void repclick(object sender, EventArgs e)
    {
        printpreview frm = new printpreview(Application.StartupPath + "\\reports\\FIRMPROFILE.RPT", "")
        {
            MdiParent = this.MdiParent
        };
        frm.Show();
    }
    void choclick(object sender, EventArgs e)
    {
        OpenFileDialog ofd = new OpenFileDialog { Title = "Select Photo", RestoreDirectory = true, Filter = "JPEG files(*.jpg)|*.jpg|All files(*.*)|*.*" };
        if (ofd.ShowDialog(this) == DialogResult.OK)
        {
            tb8.Text = ofd.FileName;
            pcb.Image = Image.FromFile(tb8.Text);
        }
    }

    void update()
    {
        try
        {
            Int32.TryParse(tb25.Text, out int port);
            gv.conn.Open();
            gv.sql = "update bill set firmname = @firmname, address = @address, address1 = @address1, email = @email, emailpassword = @emailpassword, phone1 = @phone1, phone2 = @phone2, phone3 = @phone3, phone4 = @phone4, phone5 = @phone5, phone6 =@phone6, stcstno = @stcstno, vatnumber =@vatnumber, dlno20b = @dlno20b, dlno21b = @dlno21b, logofile = @logofile, company1 = @company1 , company2 = @company2 ,company3 = @company3, company4 = @company4, jurisdicti= @jurisdicti, brandname =@brandname, godname= @godname, smtp=@smtp, port = @port, startdate = @startdate, enddate = @enddate, gstnumber = @gstnumber where id = @id";
            Debug.WriteLine(gv.sql);
            gv.adap.UpdateCommand = new OleDbCommand(gv.sql, gv.conn);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@firmname", tb1.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@address", tb2.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@email", tb3.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@emailpassword", tb4.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@phone1", tb5.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@phone2", tb6.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@phone3", tb7.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@vatnumber", tb8.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@dlno20b", tb9.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@logofile", tb10.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@company1", tb11.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@brandname", tb12.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@godname", tb13.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@smtp", tb14.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@port", port);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@startdate", dtp1.Value.ToString("MM/dd/yyyy"));
            gv.adap.UpdateCommand.Parameters.AddWithValue("@enddate", dtp2.Value.ToString("MM/dd/yyyy"));
            gv.adap.UpdateCommand.Parameters.AddWithValue("@id", Num);
            gv.adap.UpdateCommand.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        finally
        {
            gv.conn.Close();
        }
    }
    void select()
    {
        try
        {
            gv.conn.Open();
            gv.sql = "select firmname as [Firm Name], address as [Address], " +
            "address1 as [Address1], " +
            "email as [Email], emailpassword as [Email Password], " +
            "phone1 as [Phone1], phone2 as [Phone2], " +
            "phone3 as [Phone3], phone4 as [Phone4], " +
            "phone5 as [Phone5], phone6 as [Phone6], " +
            "stcstno as [STCST Number], vatnumber as [VAT Number], " +
            "dlno20b as [DLNO20B], dlno21b as [DLNO21B], " +
            "logofile as [Logo], " +
            "company1 as [Company1], company2 as [Company2], " +
            "company3 as [Company3], company4 as [Company4], " +
            "jurisdicti as [Jurisdiction], " +
            "brandname as [Brand Name]," +
            "godname as [God Name], smtp as [SMTP Server], " +
            "port as [Port], startdate as [Start Date], " +
            "enddate as [End Date],gstnumber as[GST Number] from  bill where  id = " + Num;
            Debug.WriteLine(gv.sql);
            gv.ds = new DataSet();
            gv.adap.SelectCommand = new OleDbCommand(gv.sql, gv.conn);
            gv.adap.Fill(gv.ds, "Bill");
            if(gv.ds.Tables[0].Rows.Count > 0)
			{
				tb1.Text   = gv.ds.Tables[0].Rows[0]["Firm Name"].ToString();
				tb2.Text   = gv.ds.Tables[0].Rows[0]["Address"].ToString();
				tb3.Text   = gv.ds.Tables[0].Rows[0]["Address1"].ToString();
				tb4.Text   = gv.ds.Tables[0].Rows[0]["Email"].ToString();
				tb5.Text   = gv.ds.Tables[0].Rows[0]["Email Password"].ToString();
				tb6.Text   = gv.ds.Tables[0].Rows[0]["Phone1"].ToString();
				tb7.Text   = gv.ds.Tables[0].Rows[0]["Phone2"].ToString();
				tb8.Text   = gv.ds.Tables[0].Rows[0]["Phone3"].ToString();
				tb9.Text   = gv.ds.Tables[0].Rows[0]["Phone4"].ToString();
				tb10.Text  = gv.ds.Tables[0].Rows[0]["Phone5"].ToString();
				tb11.Text  = gv.ds.Tables[0].Rows[0]["Phone6"].ToString();
				tb12.Text  = gv.ds.Tables[0].Rows[0]["STCST Number"].ToString();
				tb13.Text  = gv.ds.Tables[0].Rows[0]["VAT Number"].ToString();
				tb14.Text  = gv.ds.Tables[0].Rows[0]["DLNO20B"].ToString();	
				tb15.Text  = gv.ds.Tables[0].Rows[0]["DLNO21B"].ToString();	
				tb16.Text  = gv.ds.Tables[0].Rows[0]["Logo"].ToString();	
				tb17.Text  = gv.ds.Tables[0].Rows[0]["Company1"].ToString();	
				tb18.Text  = gv.ds.Tables[0].Rows[0]["Company2"].ToString();
				tb19.Text  = gv.ds.Tables[0].Rows[0]["Company3"].ToString();	
				tb20.Text  = gv.ds.Tables[0].Rows[0]["Company4"].ToString();
				tb21.Text  = gv.ds.Tables[0].Rows[0]["Jurisdiction"].ToString();	
				tb22.Text  = gv.ds.Tables[0].Rows[0]["Brand Name"].ToString();	
				tb23.Text  = gv.ds.Tables[0].Rows[0]["God Name"].ToString();	
				tb24.Text  = gv.ds.Tables[0].Rows[0]["SMTP Server"].ToString();	
				tb25.Text  = gv.ds.Tables[0].Rows[0]["Port"].ToString();	
				dtp1.Value = Convert.ToDateTime(gv.ds.Tables[0].Rows[0]["Start Date"]);
				dtp2.Value = Convert.ToDateTime(gv.ds.Tables[0].Rows[0]["End Date"]);
				tb26.Text  = gv.ds.Tables[0].Rows[0]["GST Number"].ToString();
				if(File.Exists(tb9.Text))
				{
					pcb.Image = Image.FromFile(tb9.Text);
				}
			}
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        finally
        {
            gv.conn.Close();
        }
    }
    void insert()
    {
        SaveFileDialog dialog = new SaveFileDialog { RestoreDirectory = true, OverwritePrompt = true, Filter = "Access Database(*.mdb)|*.mdb" };
        if(dialog.ShowDialog()== DialogResult.OK)
        {
            string filename = dialog.FileName;
            insertlist(filename);

            createdb cdb = new createdb();
            cdb.create();

            try
            {
                gv.conn.Open();
                Num = 1;
                Int32.TryParse(tb15.Text, out int port);

                gv.sql = "insert into bill (id, firmname, address, email,emailpassword, phone1, phone2, phone3,vatnumber, dlno20b, logofile, company1, brandname, godname, smtp, port,startdate, enddate ) " +
                    "values(@id,@firmname, @address, @email,@emailpassword, @phone1, @phone2, @phone3,@vatnumber, @dlno20b, @logofile, @company1, @brandname, @godname, @smtp,@port,@startdate,@enddate) ";
                Debug.WriteLine(gv.sql);
                gv.adap.InsertCommand = new OleDbCommand(gv.sql, gv.conn);
                gv.adap.InsertCommand.Parameters.AddWithValue("@id", Num);
                gv.adap.InsertCommand.Parameters.AddWithValue("@firmname", tb1.Text);
                gv.adap.InsertCommand.Parameters.AddWithValue("@address", tb2.Text);
                gv.adap.InsertCommand.Parameters.AddWithValue("@email", tb3.Text);
                gv.adap.InsertCommand.Parameters.AddWithValue("@emailpassword", tb4.Text);
                gv.adap.InsertCommand.Parameters.AddWithValue("@phone1", tb5.Text);
                gv.adap.InsertCommand.Parameters.AddWithValue("@phone2", tb6.Text);
                gv.adap.InsertCommand.Parameters.AddWithValue("@phone3", tb7.Text);
                gv.adap.InsertCommand.Parameters.AddWithValue("@vatnumber", tb8.Text);
                gv.adap.InsertCommand.Parameters.AddWithValue("@dlno20b", tb9.Text);
                gv.adap.InsertCommand.Parameters.AddWithValue("@logofile", tb10.Text);
                gv.adap.InsertCommand.Parameters.AddWithValue("@company1", tb11.Text);
                gv.adap.InsertCommand.Parameters.AddWithValue("@brandname", tb12.Text);
                gv.adap.InsertCommand.Parameters.AddWithValue("@godname", tb13.Text);
                gv.adap.InsertCommand.Parameters.AddWithValue("@smtp", tb14.Text);
                gv.adap.InsertCommand.Parameters.AddWithValue("@port", tb15.Text);
                gv.adap.InsertCommand.Parameters.Add("@startdate", OleDbType.Date).Value = dtp1.Value;
                gv.adap.InsertCommand.Parameters.AddWithValue("@startdate", dtp1.Value.ToString("MM/dd/yyyy"));
                gv.adap.InsertCommand.Parameters.AddWithValue("@enddate", dtp2.Value.ToString("MM/dd/yyyy"));
                gv.adap.InsertCommand.ExecuteNonQuery();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            finally
            {
                gv.conn.Close();
            }
        }
    }
    void insertlist(string path)
    {
        try
        {
            int comid = 0;
            gv.conn.Open();
            gv.sql = "select max(id) as[Max]from list";
            Debug.WriteLine(gv.sql);
            gv.ds = new DataSet();
            gv.adap.SelectCommand = new OleDbCommand(gv.sql, gv.conn);
            gv.adap.Fill(gv.ds);

            if (gv.ds.Tables[0].Rows.Count > 0)
            {
                if (gv.ds.Tables[0].Rows[0]["Max"].GetType() != typeof(DBNull))
                    comid = Convert.ToInt32(gv.ds.Tables[0].Rows[0][0]);
            }

            comid++;

            gv.sql = "insert into list (id,firmname,datapath) values(@id,@firmname,@datapath)";
            Debug.WriteLine(gv.sql);
            gv.adap.InsertCommand = new OleDbCommand(gv.sql, gv.conn);
            gv.adap.InsertCommand.Parameters.AddWithValue("@id", comid);
            gv.adap.InsertCommand.Parameters.AddWithValue("@firmname", tb1.Text);
            gv.adap.InsertCommand.Parameters.AddWithValue("@datapath", path);
            gv.adap.InsertCommand.ExecuteNonQuery();
        }
        catch (Exception Ex)
        {
            MessageBox.Show(Ex.Message);
        }
        finally
        {
            gv.conn.Close();
            gv.mdbfile = path;
        }
    }
    void delete()
    {
        try
        {
            gv.conn.Open();
            gv.sql = "delete from  bill where  id = " + Num;
            Debug.WriteLine(gv.sql);
            gv.adap.DeleteCommand = new OleDbCommand(gv.sql, gv.conn);
            gv.adap.DeleteCommand.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        finally
        {
            gv.conn.Close();
        }
    }	
}