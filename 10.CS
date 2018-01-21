using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Diagnostics; 
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

public class registration:Form
{
    Label lbl1, lbl2, lbl3;

    TextBox tb1;

    Button btn1;
	
	public registration()
	{
        Text = "Registration Form";
        Load += new EventHandler(load);

        lbl1 = new Label { Text = "Firm Name", Location = new Point(0, 0), Parent = this };
        lbl2 = new Label { Location = new Point(100, 0), Width = 200, Parent = this };

        lbl3 = new Label { Text = "Registration Code", Location = new Point(0, 30), Parent = this };
        tb1 = new TextBox { Location = new Point(100, 30), Width = 200, Parent = this };

        btn1 = new Button { Text = "Update Key", Location = new Point(100, 60), Parent = this };
        btn1.Click += new EventHandler(updclick);		
	}
	void select()
	{
		try
		{
			gv.conn.Open();
			gv.ds = new DataSet();
			gv.sql = "select id, firmname as [Firm Name], "+
			"startdate as [Start Date], enddate as [End Date], cipher as [Cipher] from bill";
			Debug.WriteLine(gv.sql);
			gv.adap.SelectCommand = new OleDbCommand(gv.sql,gv.conn);
			gv.adap.Fill(gv.ds,"Firmname");
			if(gv.ds.Tables[0].Rows.Count>0)
			{
				gv.billid = Convert.ToInt32(gv.ds.Tables[0].Rows[0]["id"]);
				gv.companyname = gv.ds.Tables[0].Rows[0]["Firm Name"].ToString();
				gv.cipher = gv.ds.Tables[0].Rows[0]["Cipher"].ToString();
				gv.yearstartdate = Convert.ToDateTime(gv.ds.Tables[0].Rows[0]["Start Date"]);
				gv.yearenddate = Convert.ToDateTime(gv.ds.Tables[0].Rows[0]["End Date"]);
			}
			lbl2.Text = gv.companyname;
			tb1.Text = gv.cipher;
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
    void update()
    {
        try
        {
            gv.cipher = tb1.Text;
            gv.conn.Open();
            gv.sql = "Update Bill set cipher = @cipher where id = @id";
            Debug.WriteLine(gv.sql);
            gv.adap.UpdateCommand = new OleDbCommand(gv.sql, gv.conn);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@cipher", tb1.Text);
            gv.adap.UpdateCommand.Parameters.AddWithValue("@id", gv.billid);
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
	void load(object sender, EventArgs e)
	{
        select();

        if (match())
		{
			gv.authcopy=true;
			Close();
		}
	}
	void updclick(object sender, EventArgs e)
	{
        update();
		if(match())
		{
			gv.authcopy=true;
			Close();
		}
	}

    bool match()
    {
        bool retval = false;

        string plaintext = gv.companyname + gv.key;
        plaintext = plaintext.Replace(" ", "");
        plaintext = plaintext.ToUpper();
        string s = gethash_MD5(plaintext);
        int a = gethash_numeric(s);
        int b = gethash_numeric_checksum(a.ToString());
        a += b;
        Debug.WriteLine(a);
        if (gv.cipher == a.ToString())
        {
            retval = true;
            Debug.WriteLine("PASS");
        }
        else
        {
            retval = false;
            Debug.WriteLine("Fail");
        }

        return retval;
    }
    string gethash_MD5(string s)
    {
        byte[] bt = Encoding.ASCII.GetBytes(s);
        MD5 hash = new MD5CryptoServiceProvider();
        byte[] hashbyte = hash.ComputeHash(bt);
        string hashcode = "";
        for (int i = 0; i < hashbyte.Length; i++)
        {
            byte b = hashbyte[i];
            int n = (int)b;
            int n1 = n & 15;
            int n2 = (n >> 4) & 15;
            if (n2 > 9)
            {
                hashcode += ((char)(n2 - 10 + (int)'A')).ToString();
            }
            else
            {
                hashcode += n2.ToString();
            }

            if (n1 > 9)
            {
                hashcode += ((char)(n1 - 10 + (int)'A')).ToString();
            }
            else
            {
                hashcode += n1.ToString();
            }
            if ((i + 1) != hashbyte.Length && (i + 1) % 2 == 0)
            {
                hashcode += "-";
            }
        }
        return hashcode;
    }
    int gethash_numeric(string s)
    {
        int maxval = 10000;
        uint hash = 0;
        foreach (byte b in System.Text.Encoding.Unicode.GetBytes(s))
        {
            hash += b;
            hash += (hash << 10);
            hash ^= (hash >> 6);
        }
        // final avalanche
        hash += (hash << 3);
        hash ^= (hash >> 11);
        hash += (hash << 15);
        // helpfully we only want positive integer < MUST_BE_LESS_THAN
        // so simple truncate cast is ok if not perfect
        return (int)(hash % maxval);
    }
    int gethash_numeric_checksum(string s)
    {
        int iSum = 0;
        int iDigit = 0;
        for (int i = s.Length; i >= 1; i--)
        {
            iDigit = Convert.ToInt32(s.Substring(i - 1, 1));
            if (i % 2 == 0)
            { // odd  
                iSum += iDigit * 3;
            }
            else
            { // even
                iSum += iDigit * 1;
            }
        }
        return (10 - (iSum % 10)) % 10;
    }
}