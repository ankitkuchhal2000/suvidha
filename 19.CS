using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

class voucher:Form
{
    gv.vouchertype VouchTyp;
    DateTime VouchDate;
    public int Num
    {
        get; set;
    }
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
    Label lbl1,lbl2,lbl3;
    Button btn1, btn2;
    ComboBox cb1;
    TextBox tb1;
    DateTimePicker dtp;
    DataGridView dgv;
    public voucher(gv.mode md, int num,gv.vouchertype typ,DateTime vouchdate)
    {
        VouchTyp = typ;
        VouchDate = vouchdate;
        Num = num;
        Mode = md;

        Size = new Size(500, 500);
        AutoScroll = true;
        WindowState = FormWindowState.Maximized;
        Load += new EventHandler(load);

        lbl1 = new Label { Text = "VOUCHER NUMBER", Parent = this, Location = new Point(0, 0), AutoSize = true };
        tb1 = new TextBox { Parent = this,Location = new Point(150,0),Width= 300};
        lbl2 = new Label { Text = "VOUCHER TYPE", Parent = this, Location = new Point(0, 30) };
        cb1 = new ComboBox { DataSource = Enum.GetValues(typeof(gv.vouchertype)) , Parent = this, Location = new Point(100,30)};
        lbl3 = new Label { Text = "VOUCHER DATE", Parent = this, Location = new Point(0, 60) };
        dtp = new DateTimePicker { Parent = this, Location = new Point(100, 60) };

        dgv = new DataGridView { Parent = this, Location = new Point(0, 90) ,Width = 475,Height = 300};

        btn1 = new Button {Text = "&SAVE",Parent = this, Location = new Point(200,390) };
        btn2 = new Button {Text = "&DELETE",Parent = this, Location = new Point(300,390) };
    }
    void load(object sender, EventArgs e)
    {
        cb1.SelectedIndex = (int)VouchTyp;
        dtp.Value = VouchDate;
        select();
    }
    void select()
    {
        try
        {
            gv.conn.Open();
            gv.ds = new DataSet();
            gv.sql = "select vouchno,vt,vouchdate from jour where vouchno = @vouchno and vt = @vt and vouchdate = @vouchdate";
            Debug.WriteLine(gv.sql);
            gv.adap.SelectCommand = new OleDbCommand(gv.sql, gv.conn);
            gv.adap.SelectCommand.Parameters.AddWithValue("@vouchno", Num);
            gv.adap.SelectCommand.Parameters.AddWithValue("@vt", getshortvouchtype(VouchTyp));
            gv.adap.SelectCommand.Parameters.AddWithValue("@vouchdate", VouchDate);
            gv.adap.Fill(gv.ds, "vouch");
            if (gv.ds.Tables[0].Rows.Count > 0)
            {
                
            }
            /**/
            gv.sql = "select partyid as[Account],narration as [Nattartion],amount as [Amount],drcr as [c/d] from jour where vouchno = @vouchno and vt = @vt and vouchdate = @vouchdate";
            Debug.WriteLine(gv.sql);
            gv.adap.SelectCommand = new OleDbCommand(gv.sql, gv.conn);
            gv.adap.SelectCommand.Parameters.AddWithValue("@vouchno", Num);
            gv.adap.SelectCommand.Parameters.AddWithValue("@vt", getshortvouchtype(VouchTyp));
            gv.adap.SelectCommand.Parameters.AddWithValue("@vouchdate", VouchDate);
            gv.adap.Fill(gv.ds, "jour");
            dgv.DataSource = gv.ds;
            dgv.DataMember = "jour";
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
    string getshortvouchtype(gv.vouchertype vt)
    {
        string v = "";
        switch (vt)
        {
            case gv.vouchertype.CASH_RECEIPT:
                v = "CR";
                break;
            case gv.vouchertype.CASH_PAYMENT:
                v = "CP";
                break;
            case gv.vouchertype.BANK_DEPOSIT:
                v = "BD";
                break;
            case gv.vouchertype.BANK_WITHDRAW:
                v = "BW";
                break;
            case gv.vouchertype.journal:
                v = "JV";
                break;

        }
        return v;
    }
    gv.vouchertype GetVouchertype(string vt)
    {
        gv.vouchertype v = gv.vouchertype.unassigned;
        switch (vt)
        {
            case "CR":
                v = gv.vouchertype.CASH_RECEIPT;
                break;
        }
        return v;
    }
}
