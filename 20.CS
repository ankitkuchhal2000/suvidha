using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Diagnostics; 
using System.ComponentModel;
public class configuration:Form
{
	TabControl tc;
	TabPage tp1;
	TabPage tp2;
	
	CheckBox chb1;
	CheckBox chb2;
	
	Label lbl1;
	Label lbl2;
	Label lbl3;
	Label lbl4;
	Label lbl5;
	Label lbl6;
	
	ComboBox cb1;
	ComboBox cb2;
	ComboBox cb3;
	
	TextBox tb1;
	TextBox tb2;
	TextBox tb3;
	
	Button btn1;
	Button btn2;
	Button btn3;
	Button btn4;
	
	inireader ir = new inireader(gv.inifile);
	OpenFileDialog ofd;
	public configuration()
	{
		WindowState = FormWindowState.Maximized;
		
		ofd = new OpenFileDialog();
		ofd.RestoreDirectory = true;
		ofd.DefaultExt = "rpt";
		ofd.Filter = "rpt files (*.rpt)|*.rpt|All files (*.*)|*.*";
		ofd.FilterIndex = 0;
		ofd.CheckFileExists = true;
		ofd.CheckPathExists = true;
		ofd.Multiselect = false;
		ofd.ReadOnlyChecked = true;
		ofd.ShowReadOnly = true;
		
		
		tc = new TabControl();
		tc.Size = new Size(600,500);
		tc.Parent = this;
		
		tp1 = new TabPage();
		tp1.Text = "General Configuration";
		tp1.Parent = tc;
		
		tp2 = new TabPage();
		tp2.Text = "Report Files";
		tp2.Parent = tc;
		
		chb1 = new CheckBox();
		chb1.Location = new Point(0,0);
		chb1.Text = "Print Date in Barcode Sticker";
		chb1.CheckAlign = ContentAlignment.MiddleRight;
		chb1.Width = 250;
		chb1.Parent = tp1;
		
		lbl1 = new Label();
		lbl1.Location = new Point(0,30);
		lbl1.Text = "Default Sale A/c";
		lbl1.Width = 125;
		lbl1.Parent= tp1;
		
		cb1 = new ComboBox();
		cb1.Location = new Point(150,30);
		cb1.Width = 300;
		cb1.Parent = tp1;
		
		lbl2 = new Label();
		lbl2.Location = new Point(0,60);
		lbl2.Text = "Default Purchase A/c";
		lbl2.Width = 125;
		lbl2.Parent = tp1;

		cb2 = new ComboBox();
		cb2.Location = new Point(150,60);
		cb2.Width = 300;
		cb2.Parent = tp1;
		
		lbl3 = new Label();
		lbl3.Location = new Point(0,90);
		lbl3.Text = "Selected Language";
		lbl3.Width = 125;
		lbl3.Parent = tp1;
		
		cb3 = new ComboBox();
		cb3.Location = new Point(150,90);
		cb3.Width = 300;
		cb3.Parent = tp1;
	
		lbl4 = new Label();
		lbl4.Location = new Point(0,0);
		lbl4.Text = "Invoice Report File";
		lbl4.Width = 125;
		lbl4.Parent = tp2;
		
		tb1 = new TextBox();
		tb1.Location = new Point(150,0);
		tb1.Width = 300;
		tb1.Parent = tp2;
		
		btn1 = new Button();
		btn1.Location = new Point(500,0);
		btn1.Text = "Open File";
		btn1.Click += new EventHandler(rptinvclick);
		btn1.Parent = tp2;
		
		lbl5 = new Label();
		lbl5.Location = new Point(0,30);
		lbl5.Text = "Claim Report File";
		lbl5.Parent = tp2;
		
		tb2 = new TextBox();
		tb2.Location = new Point(150,30);
		tb2.Width = 300;
		tb2.Parent = tp2;
		
		btn2 = new Button();
		btn2.Location = new Point(500,30);
		btn2.Text = "Open File";
		btn2.Click += new EventHandler(rptclaimclick);
		btn2.Parent = tp2;
		
		chb2 = new CheckBox();
		chb2.Location = new Point(0,120);
		chb2.Text = "Batch wise stock and sale facility ";
		chb2.CheckAlign = ContentAlignment.MiddleRight;
		chb2.Width = 250;
		chb2.Parent = tp1;
		
		lbl6 = new Label();
		lbl6.Location = new Point(0,150);
		lbl6.Text = "Data Base File";
		lbl6.Parent = tp1;
		
		tb3 = new TextBox();
		tb3.Location = new Point(150,150);
		tb3.Width = 300;
		tb3.Parent = tp1;
		
		btn3 = new Button();
		btn3.Location = new Point(500,150);
		btn3.Text = "Open File";
		btn3.Click += new EventHandler(seledbclick);
		btn3.Parent = tp1;
		
		btn4 = new Button();
		btn4.Location = new Point(100,500);
		btn4.Text = "Save";
		btn4.Click += new EventHandler(savclick);
		btn4.Parent = this;
	
	//	fill combos
		try
		{
			gv.conn.Open();
			gv.ds = new DataSet();
			gv.sql = "select head from spacc where type ='S'";
			gv.adap = new OleDbDataAdapter(gv.sql,gv.conn);
			gv.adap.Fill(gv.ds,"saleac");
			cb1.DataSource = gv.ds.Tables[0];
			cb1.DisplayMember = "head";
			
			gv.ds = new DataSet();
			gv.sql = "select head from spacc where type ='P'";
			gv.adap = new OleDbDataAdapter(gv.sql,gv.conn);
			gv.adap.Fill(gv.ds,"purchaseac");
			cb2.DataSource = gv.ds.Tables[0];
			cb2.DisplayMember = "head";
		}
		catch(Exception ex)
		{
			Debug.WriteLine(ex.Message);
		}
		finally
		{
			gv.conn.Close();
		}
		cb3.Items.Add("en-US");
		cb3.Items.Add("hi-IN");
		cb3.Items.Add("pr-BR");
	//	read settings
		bool chkb;
		bool.TryParse(ir.readstring("printing","dateinlabel"),out chkb);
		chb1.Checked = chkb;
		bool.TryParse(ir.readstring("invoicing","batchwisestockandsale"),out chkb);
		chb2.Checked = chkb;
		cb1.SelectedIndex = cb1.FindStringExact(ir.readstring("item","defaultsaleac"));
		cb2.SelectedIndex = cb2.FindStringExact(ir.readstring("item","defaultpurchaseac"));
		cb3.SelectedIndex = cb3.FindStringExact(ir.readstring("general","language"));
		tb1.Text = ir.readstring("reportfiles","invoice");
		tb2.Text = ir.readstring("reportfiles","medicalclaim");
		tb3.Text = ir.readstring("database","datafile");
	}
	void savclick(object sender, EventArgs e)
	{
		ir.writestring("printing","dateinlabel",chb1.Checked.ToString());
		ir.writestring("invoicing","batchwisestockandsale",chb2.Checked.ToString());
		ir.writestring("item","defaultsaleac",cb1.GetItemText(cb1.SelectedItem));
		ir.writestring("item","defaultpurchaseac",cb2.GetItemText(cb2.SelectedItem));
		ir.writestring("general","language",cb3.GetItemText(cb3.SelectedItem));
		ir.writestring("reportfiles","invoice",tb1.Text);
		ir.writestring("reportfiles","medicalclaim",tb2.Text);
		ir.writestring("database","datafile",tb3.Text);
		
	}
	void rptinvclick(object sender, EventArgs e)
	{
		if (ofd.ShowDialog() == DialogResult.OK)
		{
			tb1.Text = ofd.FileName;
		}
	}
	void rptclaimclick(object sender, EventArgs e)
	{
		if (ofd.ShowDialog() == DialogResult.OK)
		{
			tb2.Text = ofd.FileName;
		}
	}
	void seledbclick(object sender, EventArgs e)
	{
		ofd.Filter = "mdb files (*.mdb)|*.mdb|All files (*.*)|*.*";
		if (ofd.ShowDialog() == DialogResult.OK)
		{
			tb3.Text = ofd.FileName;
		}
	}

}