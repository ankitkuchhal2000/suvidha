using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.IO.Compression;
using System.Diagnostics; 
using System.ComponentModel;
using System.Runtime.InteropServices;
using DataGridViewAutoFilter;

class viewer : Form
{

    gv.frmtoopen frmtoopen;
    public gv.frmtoopen FormToOpen
    {
        get
        {
            return frmtoopen;
        }
        set
        {
            frmtoopen = value;
        }
    }
    Form frm;
    DataGridView dgv;
    ToolStripStatusLabel ssl1;
    ToolStripStatusLabel ssl2;
    StatusStrip ss;

    Button btn1;
    Button btn2;
    Button btn3;
    Button btn4;
    Button btn5;
    Button btn6;

    public viewer(gv.frmtoopen form)
    {
        FormToOpen = form;
        Size = new Size(620, 530);
        WindowState = FormWindowState.Maximized;

        dgv = new DataGridView { Anchor = (AnchorStyles.Top |AnchorStyles.Bottom| AnchorStyles.Right | AnchorStyles.Left), Location = new Point(0, 0), Width = 600, Height = 210, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, Parent = this };
        dgv.KeyDown += new KeyEventHandler(dgvkeydown);
        dgv.BindingContextChanged += new EventHandler(dgvbindingcontextchanged);
        dgv.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dgvbindingcomplete);

        btn1 = new Button { Parent = this, Text = "&VIEW", Location = new Point(0, 440) };
        btn1.Click += new EventHandler(viewclick);

        btn2 = new Button { Parent = this, Text = "ADD &NEW", Location = new Point(100, 440) };
        btn2.Click += new EventHandler(newclick);

        btn3 = new Button { Parent = this, Text = "&MODIFY", Location = new Point(200, 440) };
        btn3.Click += new EventHandler(modclick);

        btn4 = new Button { Parent = this, Text = "&DELETE", Location = new Point(300, 440) };
        btn4.Click += new EventHandler(delclick);

        btn5 = new Button { Parent = this, Text = "&IMPORT EXCEL FILE", Location = new Point(400, 440) };
        btn5.Click += new EventHandler(impclick);

        btn6 = new Button { Parent = this, Text = "&REFRESH DATA", Location = new Point(500, 440) };
        btn6.Click += new EventHandler(refclick);

        ssl1 = new ToolStripStatusLabel { IsLink = true, LinkBehavior = LinkBehavior.HoverUnderline, Text = "SHOW &ALL" };
        ssl1.Click += new EventHandler(remclick);

        ssl2 = new ToolStripStatusLabel();

        ss = new StatusStrip() { Parent = this };
        ss.Items.AddRange(new ToolStripItem[] { ssl1, ssl2 });
        
        refresh();
    }
    
    void remclick(object sender, EventArgs e)
    {
        DataGridViewAutoFilterColumnHeaderCell.RemoveFilter(dgv);
    }

    void refclick(object sender, EventArgs e)
    {
        refresh();
    }
    void viewclick(object sender, EventArgs e)
    {
        view();
    }
    void modclick(object sender, EventArgs e)
    {
        modify();
    }
    void newclick(object sender, EventArgs e)
    {
        addnew();
    }
    void delclick(object sender, EventArgs e)
    {
        delete();
    }
    void impclick(object sender, EventArgs e)
    {
        import();
    }
    
    void dgvkeydown(object sender, KeyEventArgs e)
    {
        if (e.Alt && (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up))
        {
            if (dgv.CurrentCell.OwningColumn.HeaderCell is DataGridViewAutoFilterColumnHeaderCell filterCell)
            {
                filterCell.ShowDropDownList();
                e.Handled = true;
            }
        }
        if (e.KeyCode == Keys.Enter)
        {
            view();
        }
    }
    void dgvbindingcontextchanged(object sender, EventArgs e)
    {
        if (dgv.DataSource == null)
        {
            return;
        }
        foreach (DataGridViewColumn col in dgv.Columns)
        {
            col.HeaderCell = new DataGridViewAutoFilterColumnHeaderCell(col.HeaderCell);
        }
        dgv.AutoResizeColumns();
    }
    void dgvbindingcomplete(object sender, DataGridViewBindingCompleteEventArgs e)
    {
        string filterstatus = DataGridViewAutoFilterColumnHeaderCell.GetFilterStatus(dgv);
        if (string.IsNullOrEmpty(filterstatus))
        {
            ssl1.Visible = false;
            ssl2.Visible = false;
        }
        else
        {
            ssl1.Visible = true;
            ssl2.Visible = true;
            ssl2.Text = filterstatus;
        }
    }

    void refresh()
    {
        try
        {
            gv.ds = new DataSet();
            gv.conn.Open();

            if (FormToOpen == gv.frmtoopen.user)
            {
                gv.sql = "select * from users";
            }
            if (FormToOpen == gv.frmtoopen.party)
            {
                gv.sql = "SELECT * from party";
            }
            if (FormToOpen == gv.frmtoopen.lotlist)
            {
                gv.sql = "select * from lotdetail";
            }
            if(FormToOpen == gv.frmtoopen.milling)
            {
                gv.sql = "SELECT lotdetail.id, mill.DATEE,mill.MILLNO,mill.SUPERVISOR,mill.difference,lotdetail.BARCODE, mill.CUTLENGTH, mill.VOLUMEINDCBM, mill.VOLUMEINDCBFT FROM lotdetail INNER JOIN mill ON lotdetail.id = mill.lotid";
            }
            Debug.WriteLine(gv.sql);
            gv.adap.SelectCommand = new OleDbCommand(gv.sql, gv.conn);
            gv.adap.Fill(gv.ds, "viewdata");

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            gv.conn.Close();
            BindingSource bd = new BindingSource() { DataSource = gv.ds, DataMember = "viewdata" };
            dgv.DataSource = bd;
        }

    }
    void view()
    {
        
    }
    void addnew()
    {
        if (FormToOpen == gv.frmtoopen.user)
        {
            frm = new user(gv.mode.newentry, 0) { MdiParent = MdiParent };
            frm.Show();
        }
    }
    void modify()
    {
        if(FormToOpen == gv.frmtoopen.user)
        {
            if (dgv.SelectedRows[0].Cells["id"].Value != null)
            {
                int.TryParse(dgv.SelectedRows[0].Cells["id"].Value.ToString(), out int result);
                frm = new user(gv.mode.modify, result) { MdiParent = MdiParent };
                frm.Show();
            }
        }
        

    }
    void delete()
    {
        if (FormToOpen == gv.frmtoopen.user)
        {
            if (dgv.SelectedRows[0].Cells["id"].Value != null)
            {
                int.TryParse(dgv.SelectedRows[0].Cells["id"].Value.ToString(), out int result);
                frm = new user(gv.mode.delete, result) { MdiParent = MdiParent };
                frm.Show();
            }
        }
        
    }
    void import()
    {
        
    }
    
}