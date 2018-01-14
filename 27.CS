using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Diagnostics; 
using System.ComponentModel;
using System.Media;
using System.Threading;
using System.Collections.Generic;
public class rojnamcha:Form
{
	int num = 0;
	int reccount = 0;
	int recstart = 0;
	
	DataGridView dgv;
	DateTimePicker dtp;
	Button btn1;
	Button btn2;
	
	public rojnamcha()
	{
		dtp = new DateTimePicker();
		dtp.Parent = this;
		
		btn1 = new Button();
		btn1.Location = new Point(200,0);
		btn1.Text = "Open";
		btn1.Click += new EventHandler(openclick);
		btn1.Parent = this;
		
		btn2 = new Button();
		btn2.Location = new Point(275,0);
		btn2.Text = "Save";
		btn2.Click += new EventHandler(saveclick);
		btn2.Parent = this;
		
		dgv = new DataGridView();
		dgv.Width = 500;
		dgv.Location = new Point(0,30);
		dgv.Parent = this;
		
		recstartstop();
		num = reccount;
		num++;
		fillform();
	}
	void recstartstop()
	{
		try
		{
			gv.conn.Open();
			gv.ds = new DataSet();
			gv.sql = "select Max(id) as [Max],Min(id) as [Min] from rojnamcha";
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
			
			gv.sql = "select sum(amount) as[amount jama] from rojnamcha where dinank <#"+dtp.Value.ToString("MM/dd/yyyy")+"# and receipt = true";
			Debug.WriteLine(gv.sql);
			gv.adap = new OleDbDataAdapter(gv.sql,gv.conn);
			gv.adap.Fill(gv.ds,"rokadjama");
			
			gv.sql = "select sum(amount) as[amount naam] from rojnamcha where dinank <#"+dtp.Value.ToString("MM/dd/yyyy")+"# and receipt = false";
			Debug.WriteLine(gv.sql);
			gv.adap = new OleDbDataAdapter(gv.sql,gv.conn);
			gv.adap.Fill(gv.ds,"rokadnaam");
			
			double jama = 0;
			double naam = 0;
			double baki = 0;
			if(gv.ds.Tables["rokadjama"].Rows[0][0].GetType() !=typeof(DBNull))
				jama = Convert.ToDouble(gv.ds.Tables["rokadjama"].Rows[0][0]);
			if(gv.ds.Tables["rokadnaam"].Rows[0][0].GetType() != typeof(DBNull))
				naam= Convert.ToDouble(gv.ds.Tables["rokadnaam"].Rows[0][0]);
			
			baki = jama-naam;
			
			DataTable dt = new DataTable("rokadbaki");
			dt.Columns.Add("Jama",typeof(string));
			dt.Columns.Add("amount jama",typeof(double));
			dt.Rows.Add("Balance b/d ", baki);
			
			gv.sql = "select narration as [Jama],amount as[amount jama] from rojnamcha where format(dinank,'dd/mm/yyyy') = '"+dtp.Value.ToString("dd/MM/yyyy")+"' and receipt = TRUE";
			Debug.WriteLine(gv.sql);
			gv.adap = new OleDbDataAdapter(gv.sql,gv.conn);
			gv.adap.Fill(gv.ds,"rojnamcha");
			gv.sql = "select narration as [Naam],amount as[amount naam] from rojnamcha where format(dinank,\"dd/MM/yyyy\") = '"+dtp.Value.ToString("dd/MM/yyyy")+"' and receipt = False";
			Debug.WriteLine(gv.sql);
			gv.adap = new OleDbDataAdapter(gv.sql,gv.conn);
			gv.adap.Fill(gv.ds,"rojnamcha2");
			
			dt.Merge(gv.ds.Tables["rojnamcha"]);
			dt.Merge(gv.ds.Tables["rojnamcha2"]);
			dgv.DataSource = dt;
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
	
	void openclick(object sender, EventArgs e)
	{
		fillform();
	}
	void saveclick(object sender, EventArgs e)
	{
		try
		{
			gv.conn.Open();
			gv.sql = "delete from  rojnamcha where format(dinank,'dd/MM/yyyy') = '" +dtp.Value.ToString("dd/MM/yyyy")+"'" ;
			Debug.WriteLine(gv.sql);
			gv.adap = new OleDbDataAdapter();
			gv.adap.DeleteCommand = new OleDbCommand(gv.sql,gv.conn);
			gv.adap.DeleteCommand.ExecuteNonQuery();
			foreach(DataGridViewRow dgvrow in dgv.Rows)
			{
				if(dgvrow.IsNewRow)
					continue;
				num =0;
				gv.ds = new DataSet();
				gv.sql =" select max(id) as[Max] from rojnamcha ";
				Debug.WriteLine(gv.sql);
				gv.adap.SelectCommand = new OleDbCommand(gv.sql,gv.conn);
				gv.adap.Fill(gv.ds,"id");
				if(gv.ds.Tables[0].Rows.Count>0)
				{
					if(gv.ds.Tables[0].Rows[0]["Max"].GetType() != typeof(DBNull))
						num=Convert.ToInt32(gv.ds.Tables[0].Rows[0]["Max"] );
				}
				num++;
				if(dgvrow.Cells["Jama"].Value.ToString() !="Balance b/d ")
				{
					gv.sql = "insert into rojnamcha (id,dinank,narration,amount,receipt) values("+num+",#"+dtp.Value.ToString("MM/dd/yyyy HH:mm:ss")+"#,'"+dgvrow.Cells["Jama"].Value+"',"+dgvrow.Cells["amount jama"].Value+",true)";
					Debug.WriteLine(gv.sql);
					gv.adap.InsertCommand = new OleDbCommand(gv.sql,gv.conn);
					gv.adap.InsertCommand.ExecuteNonQuery();
				}
			}
			
			foreach(DataGridViewRow dgvrow in dgv.Rows)
			{
				if(dgvrow.IsNewRow)
					continue;
				num =0;
				gv.ds = new DataSet();
				gv.sql =" select max(id) as[Max] from rojnamcha ";
				Debug.WriteLine(gv.sql);
				gv.adap.SelectCommand = new OleDbCommand(gv.sql,gv.conn);
				gv.adap.Fill(gv.ds,"id");
				if(gv.ds.Tables[0].Rows.Count>0)
				{
					if(gv.ds.Tables[0].Rows[0]["Max"].GetType() != typeof(DBNull))
						num=Convert.ToInt32(gv.ds.Tables[0].Rows[0]["Max"] );
				}
				num++;
				if(dgvrow.Cells["Naam"].Value.ToString().Trim() !="")
				{
					gv.sql = "insert into rojnamcha (id,dinank,narration,amount,receipt) values("+num+",#"+dtp.Value.ToString("MM/dd/yyyy HH:mm:ss")+"#,'"+dgvrow.Cells["Naam"].Value+"',"+dgvrow.Cells["amount naam"].Value+",false)";
					Debug.WriteLine(gv.sql);
					gv.adap.InsertCommand = new OleDbCommand(gv.sql,gv.conn);
					gv.adap.InsertCommand.ExecuteNonQuery();
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
	}
}