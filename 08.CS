using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Diagnostics;
public class Companylist:Form
{
	DataGridView dgv;
	Button btn1;
	Button btn2;
	Button btn3;
    Button btn4;
    Button btn5;
	StatusBar sb;
	StatusBarPanel sp1;
	
	public Companylist()
	{
        

        dgv = new DataGridView
        {
            Location = new Point(10, 60),
            Width = 270,
            Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left),
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AllowUserToOrderColumns = true,
            ReadOnly = true,
            MultiSelect = false,
            Parent = this
        };
        dgv.KeyDown += new KeyEventHandler(dgvkeydown);

        btn1 = new Button
        {
            Location = new Point(10, 260),
            Text = "SELECT",
            Parent = this
        };
        btn1.Click += new EventHandler(Selclick);

        btn2 = new Button
        {
            Location = new Point(110, 260),
            Text = "DELETE",
            Parent = this
        };
        btn2.Click += new EventHandler(delclick);

        btn3 = new Button
        {
            Location = new Point(210, 260),
            Text = "CREATE",
            Parent = this
        };
        btn3.Click += new EventHandler(creclick);

        btn4 = new Button
        {
            Location = new Point(310, 260),
            Text = "REPORT",
            Parent = this
        };
        btn4.Click += new EventHandler(repclick);

        btn5 = new Button
        {
            Location = new Point(410, 260),
            Text = "RE-LOCATE",
            Parent = this
        };
        btn5.Click += new EventHandler(relclick);

        sp1 = new StatusBarPanel
        {
            AutoSize = StatusBarPanelAutoSize.Spring,
            Text = gv.listfile
        };
        sb = new StatusBar
        {
            ShowPanels = true,
            Parent = this
        };
        sb.Panels.Add(sp1);

        Size = new Size(640, 630);
        CenterToScreen();

        gv.connstr = "Provider=Microsoft.Jet.OLEDB.4.0;";
		gv.connstr += "Data Source="+gv.listfile+";";
		gv.connstr += "Jet OLEDB:System Database="+gv.mdwfile+";";
		Debug.WriteLine(gv.connstr);
		gv.conn=new OleDbConnection(gv.connstr);
        gv.adap = new OleDbDataAdapter();	
		SelectQuery();
	}
	void SelectQuery()
	{
		try
		{
            gv.ds = new DataSet();
            gv.conn.Open();
			gv.sql = "select * from list";
			Debug.WriteLine(gv.sql);
            gv.adap.SelectCommand = new OleDbCommand(gv.sql, gv.conn);
			gv.adap.Fill(gv.ds,"searchbox");
			dgv.DataSource =gv.ds;
            dgv.DataMember = "searchbox";
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
	void Selclick(object sender, EventArgs e)
	{
				opecom();
		
	}
	void delclick(object sender, EventArgs e)
	{
		try
		{
			Debug.WriteLine(gv.connstr);
			gv.conn.Open();
			gv.sql= "delete from list where id = "+dgv.SelectedRows[0].Cells["id"].Value;
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
        SelectQuery();
	}

	void creclick(object sender, EventArgs e)
	{
		Client frm = new Client( gv.mode.newentry,0);
		frm.Show();
	}
    void repclick(object sender, EventArgs e)
    {
        printpreview frm = new printpreview(Application.StartupPath+"\\reports\\firmlist.rpt","");
        frm.Show();
    }
    void relclick(object sender, EventArgs e)
    {
        string file = dgv.SelectedRows[0].Cells["datapath"].Value.ToString();
        OpenFileDialog dialog = new OpenFileDialog { CheckFileExists = true, CheckPathExists = true, RestoreDirectory = true, FileName =file };
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                Debug.WriteLine(gv.connstr);
                gv.conn.Open();
                gv.sql = "update list set datapath = '" + dialog.FileName + "' where id = " + dgv.SelectedRows[0].Cells["id"].Value;
                Debug.WriteLine(gv.sql);
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
            SelectQuery();
        }
    }
    void opecom()
	{
		Debug.WriteLine(gv.connstr);
        if (dgv.SelectedRows.Count > 0)
        {
            string file = dgv.SelectedRows[0].Cells["datapath"].Value.ToString();
            if (!File.Exists(file))
            {
                MessageBox.Show("Data file not found");
                return;
            }
            else
            {
                gv.mdbfile = file;
                gv.conn.Close();
                gv.conn.Dispose();
                gv.conn = null;
                gv.connstr = "Provider=Microsoft.Jet.OLEDB.4.0;";
                gv.connstr += "Data Source=" + gv.mdbfile + ";";
                gv.connstr += "Jet OLEDB:System Database=" + gv.mdwfile + ";";
                gv.conn = new OleDbConnection(gv.connstr);
                Debug.WriteLine(gv.connstr);
                Close();
            }
        }
    }
	void dgvkeydown(object sender, KeyEventArgs e)
	{
		if(e.KeyCode == Keys.Enter)
		{
			opecom();
		}
	}
}