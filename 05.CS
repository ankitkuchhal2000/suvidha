using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;

public class searchbox
{
	static Form  frm;
	static Label lbl1;
	static TextBox tb1;
	static RadioButton[] radb;
	static DataGridView dgv;
	static Button btn1,btn2;
	
	static BindingSource bs;

	static string defaultvalue;
	
	public static DialogResult Show(string title,IWin32Window owner,string sql,ref DataTable value,string previousvalue)
	{
		try
		{
			gv.conn.Open();
			gv.ds = new DataSet();
			gv.sql = sql;
			Debug.WriteLine(gv.sql);
			gv.adap.SelectCommand= new OleDbCommand(gv.sql,gv.conn);
			gv.adap.Fill(gv.ds,"searchbox");
				
		}
		catch(Exception ex)
		{
			Debug.WriteLine(ex.Message);
		}
		finally
		{
			gv.conn.Close();
		}
		return Show(title,owner,gv.ds.Tables[0],ref value,"","",previousvalue);
	}
	public static DialogResult Show(string title, IWin32Window owner,DataTable dt,ref DataTable value,string filterstring,string sorting,string previousvalue )
	{				
		frm = new Form();

        bs = new BindingSource
        {
            Filter = "",
            DataSource = dt
        };

        lbl1 = new Label
        {
            Location = new Point(0, 0),
            Text = "Looking For",
            Parent = frm
        };

        tb1 = new TextBox
        {
            Location = new Point(100, 0),Parent = frm
        };
        tb1.TextChanged += new EventHandler(txtchanged);

        radb = new RadioButton[dt.Columns.Count];
		int x = 0;
		for(int i = 0 ; i < dt.Columns.Count ; i++)
		{
            radb[i] = new RadioButton
            {
                Location = new Point(x, 30),
                Text = dt.Columns[i].Caption,
                Parent = frm
            };
            x +=120;
			
			if (i==0)
			{
				radb[i].Checked=true;
			}
		}


        dgv = new DataGridView
        {
            Location = new Point(0, 60),
            Width = 270,
            Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left),
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            //dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            DataSource = bs,
            Parent = frm
        };
        if (filterstring != "")
		{
			dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;	
		}

        btn1 = new Button
        {
            Text = "Ok",
            DialogResult = DialogResult.OK,
            Location = new Point(0, 230),
            Parent = frm
        };

        btn2 = new Button
        {
            Text = "Cancel",
            DialogResult = DialogResult.Cancel,
            Location = new Point(100, 230),
            Parent = frm
        };

        frm.Text = title;
		frm.FormBorderStyle = FormBorderStyle.FixedDialog;
		frm.StartPosition = FormStartPosition.CenterParent;
		frm.Width = 600;
		frm.Load += new EventHandler(frmload);
		frm.AcceptButton = btn1;
		frm.CancelButton = btn2;
		
		defaultvalue = previousvalue;
		
		DialogResult diares = frm.ShowDialog(owner);
		
		foreach(DataGridViewColumn column in dgv.Columns)
		{
			value.Columns.Add(column.Name);
		}
		try
		{
			if(filterstring == "")
			{
				if(dgv.SelectedRows.Count > 0)
				{
					foreach(DataGridViewRow dgvrow in dgv.SelectedRows)
					{
						DataRow drow = value.NewRow();
						int i =0;
						foreach(DataGridViewCell cell in dgvrow.Cells)
						{
							drow[i] = cell.Value;
							i++;
						}
						value.Rows.Add(drow);
					}
				}
			}
			else
			{
				DataView dv = new DataView(dt,filterstring,sorting,DataViewRowState.CurrentRows);
				Debug.WriteLine(dv.RowFilter);
				dgv.DataSource = dv;
				foreach(DataGridViewRow dgvrow in dgv.Rows)
				{
					if(dgvrow.IsNewRow)
						continue;
					
					DataRow drow = value.NewRow();
					int i =0;
					foreach(DataGridViewCell cell in dgvrow.Cells)
					{
						drow[i] = cell.Value;
						i++;
					}
					value.Rows.Add(drow);
				}
			}
		}
		catch(Exception ex)
		{
			Debug.WriteLine(ex.Message);
		}
		finally
		{}
		return diares;
	}
	static void frmload(object sender, EventArgs e)
	{
		tb1.Text = defaultvalue;
	}
	static void txtchanged (object sender , EventArgs e)
	{
		string columnname="";
		for(int i = 0 ; i < dgv.Columns.Count ; i++)
		{
			if (radb[i].Checked)
			{
				columnname = radb[i].Text;
			}
		}
		string filter="Convert(["+columnname+"],System.String) like '%"+tb1.Text+"%'";
		Debug.WriteLine(filter);
		Debug.WriteLine(columnname);
		bs.Filter = filter;
	}
}