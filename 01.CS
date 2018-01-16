using System;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using System.Collections.Generic;
using adox;
public class table
{
	string _name;
	bool _exists = false;
	List<field> _fields;
	public string Name
	{
		get
		{
			return _name;
		}
		set
		{
			_name = value;
		}
	}
	public List<field> Fields
	{
		get
		{
			return _fields;
		}
		set
		{
			_fields = value;
		}
	}
	public bool Exists
	{
		get
		{
			return _exists;
		}
		set
		{
			_exists = value;
		}
	}
}
public class field
{
	string _name;
	string _type;
	string _size;
	string _default;
	bool _isprimay;
    bool _unique;
	bool _exists = false;
	public string Name
	{
		get
		{
			return _name;
		}
		set
		{
			_name = value;
		}
	}
	public string Type
	{
		get
		{
			return _type;
		}
		set
		{
			_type = value;
		}
	}
	public string Size
	{
		get
		{
			return _size;
		}
		set
		{
			_size=value;
		}
	}
	public string DefaultValue
	{
		get
		{
			return _default;
		}
		set
		{
			_default=value;
		}
	}
	public bool Exists
	{
		get
		{
			return _exists;
		}
		set
		{
			_exists = value;
		}
	}
	public bool IsPrimary
	{
		get
		{
			return _isprimay;
		}
		set
		{
			_isprimay = value;
		}
	}
    public bool IsUnique
    {
        get
        {
            return _unique;
        }
        set
        {
            _unique = value;
        }
    }
}
public class relation
{
	string _table;
	string _foreignkey;
	string _referencetable;
	string _name;
    string _referencekey;
	bool _exists;
	public string Table
	{
		get
		{
			return _table;
		}
		set
		{
			_table = value;
		}		
	}
	public string ForeignKey
	{
		get
		{
			return _foreignkey;
		}
		set
		{
			_foreignkey = value;
		}		
	}
    public string ReferenceKey
    {
        get
        {
            return _referencekey;
        }
        set
        {
            _referencekey = value;
        }
    }
	public string ReferenceTable
	{
		get
		{
			return _referencetable;
		}
		set
		{
			_referencetable=value;
		}		
	}
	public string Name
	{
		get
		{
			return _name;			
		}
		set
		{
			_name = value;
		}
	}
	public bool Exists
	{
		get
		{
			return _exists;
		}
		set
		{
			_exists = value;
		}
	}
}
public class index
{
    bool _exists;
    string _name;
    string _field;
    string _table;
    bool _withprimary;
    public string Field
    {
        get { return _field; }
        set { _field = value; }
    }
    public string Table
    {
        get { return _table; }
        set { _table = value; }
    }
    public bool WithPrimary
    {
        get { return _withprimary; }
        set { _withprimary = value; }
    }
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }
    public bool Exists
    {
        get
        {
            return _exists;
        }
        set
        {
            _exists = value;
        }
    }
}
public class createdb
{
	ListBox lb;
	List<relation> relations;
	List<table> tables;
    List<index> indexes;
	void structure()

    {
        List<field> colfield = new List<field>
        {
            new field{ Name="id",Type= "Number", IsPrimary= true, IsUnique = true },
            new field{ Name="firmname", Type="Text",    Size= "100" },
            new field{ Name="address",  Type="Text",  Size="100" },
            new field{ Name="address1", Type="Text", Size="100" },
            new field{ Name= "email", Type= "Text", Size="100"},
            new field{ Name="emailpassword",  Type="Text", Size="100"},
            new field{ Name="phone1", Type= "Text", Size="100"},
            new field{ Name="phone2", Type= "Text",  Size="100"},
            new field{ Name="phone3",  Type="Text",  Size="100"},
            new field{ Name="phone4",  Type="Text",  Size="100"},
            new field{ Name="phone5",  Type="Text", Size="100"},
            new field{ Name="phone6", Type="Text", Size="100"},
            new field{ Name="stcstno", Type="Text", Size="100"},
            new field{ Name="vatnumber", Type="Text", Size="100"},
            new field{ Name="dlno20b",  Type="Text",  Size="100"},
            new field{ Name="dlno21b",  Type="Text",  Size="100"},
            new field{ Name="logofile",  Type="Text", Size="100"},
            new field{ Name="company1",  Type="Text", Size="100"},
            new field{ Name="company2",  Type="Text", Size="100"},
            new field{ Name="company3",  Type="Text", Size="100"},
            new field{ Name="company4",  Type="Text", Size="100"},
            new field{ Name="jurisdicti",  Type="Text", Size="100"},
            new field{ Name="brandname",  Type="Text", Size= "100"},
            new field{ Name="godname",  Type="Text", Size= "100"},
            new field{ Name="startdate",  Type="DateTime"},
            new field{ Name="enddate", Type= "DateTime"},
            new field{ Name="smtp", Type="Text", Size= "100"},
            new field{ Name="port", Type="Number"},
            new field{ Name="cipher", Type="Text", Size="100"},
            new field{ Name="gstnumber", Type="Text", Size="100"}
        };
        tables.Add(new table { Name = "bill", Fields = colfield });

        colfield = new List<field>
        {
            new field{ Name="id", Type= "Number", IsPrimary=true, IsUnique = true },
            new field{ Name="billid", Type="Number" },
            new field{ Name="billbook", Type= "Text", Size= "100" },
            new field{ Name="series", Type="Text", Size="100" }
        };
        tables.Add(new table { Name = "billnum", Fields = colfield });

        colfield = new List<field>
        {
            new field{Name="id", Type="Number",  IsPrimary=true, IsUnique=true },
            new field{Name="partyname", Type = "Text",Size= "100" },
            new field{Name="address", Type= "Text", Size="100" },
            new field{Name="address1",Type= "Text", Size= "100" },
            new field{Name="station", Type= "Text", Size= "100" },
            new field{Name="phone1",  Type= "Text",  Size= "100" },
            new field{Name="phone2",  Type= "Text", Size="100" },
            new field{Name="phone3",  Type="Text",  Size="100" },
            new field{Name="phone4",  Type="Text",  Size="100"},
            new field{Name="phone5",  Type="Text",  Size="100"},
            new field{Name="phone6", Type="Text",  Size="100"},
            new field{Name="email", Type="Text", Size="100"},
            new field{Name="email1", Type="Text", Size="100"},
            new field{Name="email2", Type= "Text", Size="100"},
            new field{Name="contactperson", Type="Text",  Size="100"},
            new field{Name="designation", Type= "Text",  Size="100"},
            new field{Name="stcstno", Type= "Text",  Size="100"},
            new field{Name="vatnumber",  Type="Text", Size="100"},
            new field{Name="gstnumber",  Type="Text", Size="100"},
            new field{Name="dlno20b",  Type="Text",  Size="100"},
            new field{Name="dlno21b", Type= "Text", Size= "100"},
            new field{Name="statecode",Type= "Text",Size= "100" },
            new field{Name="groupid",Type="Number"}
        };
        tables.Add(new table { Name = "party", Fields = colfield });

        colfield = new List<field>
        {
            new field{Name="id", Type= "Number", IsPrimary= true, IsUnique=true },
            new field{Name="itemname", Type= "Text", Size= "100"},
            new field{Name="barcode", Type= "Text", Size="100"},
            new field{Name="saleac", Type= "Number"},
            new field{Name="purchaseac", Type= "Number"}
        };
        tables.Add(new table { Name = "item", Fields = colfield });

        colfield = new List<field>
        {
            new field{ Name="id", Type= "Text", Size="100" },
            new field{ Name="itemid", Type= "Number" },
            new field{ Name="purchaseprice", Type= "Number" },
            new field{ Name="saleprice", Type= "Number" },
            new field{ Name="mrp", Type= "Number" },
            new field{ Name="mfgdate", Type= "DateTime" },
            new field{ Name="pkgdate",  Type="DateTime" },
            new field{ Name="expdate", Type="DateTime" },
            new field{ Name="lock", Type="YesNo", DefaultValue= "FALSE" }
        };
        tables.Add(new table { Name = "itemb", Fields = colfield });

        colfield = new List<field>
        {
            new field { Name = "id", Type = "Number", IsPrimary = true, IsUnique = true}
        };
        tables.Add(new table { Name = "pbill", Fields = colfield });

        colfield = new List<field>
        {
            new field { Name = "id", Type = "Number", IsPrimary = true, IsUnique=true },
            new field { Name = "billbookid", Type = "Number" },
            new field { Name = "billnumber", Type = "Number" },
            new field { Name = "billdate", Type = "DateTime" },
            new field { Name = "creditterm", Type = "YesNo", DefaultValue = "TRUE" },
            new field { Name = "saletype", Type = "Text", Size = "100" },
            new field { Name = "againstcform", Type = "YesNo", DefaultValue = "FALSE" },
            new field { Name = "partyid", Type = "Number" },
            new field { Name = "cashdiscount", Type = "Number" },
            new field { Name = "customername", Type = "Text", Size = "100" },
            new field { Name = "customeraddr", Type = "Text", Size = "100" },
            new field { Name = "customerphone", Type = "Text", Size = "100" }
        };
        tables.Add(new table { Name = "invoice", Fields = colfield });

        colfield = new List<field>
        {
            new field { Name = "invoiceid", Type = "Number" },
            new field { Name = "challanid", Type = "Number" },
            new field { Name = "batchid", Type = "Text", Size="100" },
            new field { Name = "itemid", Type = "Number" },
            new field { Name = "saleac", Type = "Number" },
            new field { Name = "qty", Type = "Number" },
            new field { Name = "includerate", Type = "Number" },
            new field { Name = "excluderate", Type = "Number" },
            new field { Name = "amount", Type = "Number" },
            new field { Name = "taxamount", Type = "Number" },
            new field { Name = "surchargeamount", Type = "Number" },
            new field { Name = "billdate", Type = "DateTime" }
        };
        tables.Add(new table { Name = "sale", Fields = colfield });

        colfield = new List<field>
        {
            new field { Name = "invoiceid", Type = "Number" },
            new field { Name = "challanid", Type = "Number" },
            new field { Name = "batchid", Type = "Text", Size="100" },
            new field { Name = "itemid", Type = "Number" },
            new field { Name = "purac", Type = "Number" },
            new field { Name = "qty", Type = "Number" },
            new field { Name = "includerate", Type = "Number" },
            new field { Name = "excluderate", Type = "Number" },
            new field { Name = "amount", Type = "Number" },
            new field { Name = "taxamount", Type = "Number" },
            new field { Name = "surchargeamount", Type = "Number" },
            new field { Name = "billdate", Type = "DateTime" }
        };
        tables.Add(new table { Name = "purchase", Fields = colfield });

        colfield = new List<field>
        {
            new field{ Name="id",  Type="Number",  IsPrimary=true, IsUnique= true },
            new field{ Name="claimentname", Type= "Text", Size= "100" },
            new field{ Name="fathername", Type= "Text", Size= "100" },
            new field{ Name="address", Type="Text", Size="100" },
            new field{ Name="designation", Type="Text", Size= "100" },
            new field{ Name="department", Type="Text",  Size="100" },
            new field{ Name="officeof", Type ="Text", Size= "100" },
            new field{ Name="pay", Type="Text", Size="100" },
            new field{ Name= "relation",  Type="Text", Size= "100" },
            new field{ Name= "patientname", Type= "Text", Size= "100" },
            new field{ Name="hospitalname",  Type="Text",  Size="100" },
            new field{ Name="diseasename",  Type="Text",  Size="100" },
            new field{ Name="periodoftreatment",  Type="Text",  Size="100" },
            new field{ Name="treatmentstartedon",  Type="DateTime" },
            new field{ Name="treatmentendedon", Type= "DateTime" },
            new field{ Name="indoorno", Type= "Text", Size= "100" },
            new field{ Name="indoordate",  Type="DateTime" },
            new field{ Name="outdoorno",  Type="Text",  Size="100" },
            new field{ Name="outdoordate",  Type="DateTime" }
        };
        tables.Add(new table { Name = "claim", Fields = colfield });


        colfield = new List<field>
        {
            new field{ Name= "claimid", Type= "Number" },
            new field{ Name="itemid", Type= "Number" },
            new field{ Name=  "quantity", Type= "Number" },
            new field{ Name="rate", Type= "Number" },
            new field{ Name="billnumber",  Type="Text", Size= "100" },
            new field{ Name="billdate",  Type="DateTime" },
            new field{ Name="prescriptiondate",  Type="DateTime" }
        };
        tables.Add(new table { Name = "claimdata", Fields = colfield });

        colfield = new List<field>
        {
            new field{ Name="id",  Type="Number", IsPrimary= true, IsUnique=true },
            new field{ Name="head",  Type= "Text",  Size="100"},
            new field{ Name="taxrate", Type= "Number" },
            new field{ Name="surchargerate", Type= "Number" },
            new field{ Name="type", Type= "Text", Size= "100" },
            new field{ Name="shortname", Type= "Text", Size= "100" }
        };
        tables.Add(new table { Name = "spacc", Fields = colfield });

        colfield = new List<field>
        {
            new field{ Name="id", Type= "Number", IsPrimary= true, IsUnique=true },
            new field{ Name="username", Type= "Text",  Size="100" },
            new field{ Name="pass",  Type="Text", Size= "100" }
        };
        tables.Add(new table { Name = "users", Fields = colfield });

        colfield = new List<field>
        {
            new field { Name = "id", Type = "Number", IsPrimary = true, IsUnique=true },
            new field { Name = "connotedate", Type = "DateTime" },
            new field { Name = "consignee", Type = "Number" },
            new field { Name = "consignor", Type = "Number" },
            new field { Name = "deliveryaddress", Type = "Text", Size = "100" }
        };
        tables.Add(new table { Name = "connote", Fields = colfield });

        colfield = new List<field>
        {
            new field { Name = "id", Type = "Number", IsPrimary = true, IsUnique=true },
            new field { Name = "dinank", Type = "DateTime" },
            new field { Name = "narration", Type = "Text", Size = "100" },
            new field { Name = "amount", Type = "number" },
            new field { Name = "receipt", Type = "YesNo", DefaultValue = "TRUE" }
        };
        tables.Add(new table { Name = "rojnamcha", Fields = colfield });

        indexes.Add(new index { Name = "ITEMBIDX", Table = "itemb ", Field="(id,itemid)", WithPrimary = true });

        relations.Add(new relation { Name = "fkbillnumbillid", Table = "billnum", ForeignKey = "billid", ReferenceTable = "bill", ReferenceKey = "id" });
        relations.Add(new relation { Name = "fkinvoicebillbookid", Table = "invoice", ForeignKey = "billbookid", ReferenceTable = "billnum", ReferenceKey = "id" });
        relations.Add(new relation { Name = "fkinvoicepartyid", Table = "invoice", ForeignKey = "partyid", ReferenceTable = "party", ReferenceKey = "id" });

        relations.Add(new relation { Name = "fksalebatchiditemid", Table = "sale", ForeignKey = "batchid,itemid", ReferenceTable = "itemb", ReferenceKey = "id,itemid" });
        relations.Add(new relation { Name = "fksalesaleac", Table = "sale", ForeignKey = "saleac", ReferenceTable = "spacc", ReferenceKey = "id" });
        relations.Add(new relation { Name = "fksaleinvoiceid", Table = "sale", ForeignKey = "invoiceid", ReferenceTable = "invoice", ReferenceKey = "id" });

        relations.Add(new relation { Name = "fkpurchasebatchiditemid", Table = "purchase", ForeignKey = "batchid,itemid", ReferenceTable = "itemb", ReferenceKey = "id,itemid" });
        relations.Add(new relation { Name = "fkpurchasepurac", Table = "purchase", ForeignKey = "purac", ReferenceTable = "spacc", ReferenceKey = "id" });
        //      relations.Add(new relation { Name = "fkpurchaseinvoiceid", Table = "purchase", ForeignKey = "invoiceid", ReferenceTable = "invoice", ReferenceKey = "id" });

        relations.Add(new relation { Name = "fkitembitemid", Table = "itemb", ForeignKey = "itemid", ReferenceTable = "item", ReferenceKey = "id" });

        relations.Add(new relation { Name = "fkclaimdataitemid", Table = "claimdata", ForeignKey = "itemid", ReferenceTable = "item", ReferenceKey = "id" });
        relations.Add(new relation { Name = "fkclaimdataclaimid", Table = "claimdata", ForeignKey = "claimid", ReferenceTable = "claim", ReferenceKey = "id" });

        relations.Add(new relation { Name = "fkitemsaleac", Table = "item", ForeignKey = "saleac", ReferenceTable = "spacc", ReferenceKey = "id" });
        relations.Add(new relation { Name = "fkitempurchaseac", Table = "item", ForeignKey = "purchaseac", ReferenceTable = "spacc", ReferenceKey = "id" });
    }
    public void create()
	{
        Form frm = new Form();
        lb = new ListBox { Parent = frm, Width = 500, Height = 270 };
        frm.Show();
        relations = new List<relation>();
        tables = new List<table>();
        indexes = new List<index>();

        structure();
		try
		{	
			lb.Items.Clear();
			if(!File.Exists(gv.mdbfile))
			{
				gv.connstr = "Provider=Microsoft.Jet.OLEDB.4.0;";
				gv.connstr += "Data Source="+gv.mdbfile+";";
				gv.connstr += "Jet OLEDB:System Database="+gv.mdwfile+";";
		
				Catalog cat = new Catalog();
				cat.Create(gv.connstr);
				cat = null;

                lb.Items.Add("New File "+gv.mdbfile+" Created");
			}
			gv.conn=new OleDbConnection(gv.connstr);
			gv.conn.Open();
			gv.ds = new DataSet();
			gv.ds.Tables.Add(gv.conn.GetSchema("tables"));				
			
			foreach(table t in tables)
			{
				foreach(DataRow drow in gv.ds.Tables["tables"].Rows)
				{
					string r =drow["TABLE_NAME"].ToString();
					if(r==t.Name)
					{
						t.Exists=true;
					}
				}
				if(!t.Exists)
				{
					Debug.Write(t.Name+" not exists");
                    gv.sql = "Create Table ["+t.Name+"](["+t.Fields[0].Name+"] "+t.Fields[0].Type;
                    if (t.Fields[0].Type == "Text")
                    {
                        gv.sql += " (" + t.Fields[0].Size + ")";
                    }
                    if (t.Fields[0].IsPrimary)
                    {
                        gv.sql += " NOT NULL PRIMARY KEY";
                    }
                    if (t.Fields[0].IsUnique)
                    {
                        gv.sql += " Unique";
                    }
                    gv.sql += ")";
					gv.cmd = new OleDbCommand(gv.sql,gv.conn);
					gv.cmd.ExecuteNonQuery();
					lb.Items.Add(gv.sql);
				}
				gv.sql="select * from "+t.Name;
				gv.adap.SelectCommand= new OleDbCommand(gv.sql,gv.conn);
				gv.adap.Fill(gv.ds,t.Name);
				Debug.WriteLine(gv.sql);
				if(t.Fields != null)
				{
					foreach(field f in t.Fields)
					{
						int k = gv.ds.Tables[t.Name].Columns.IndexOf(f.Name);
						if(k>-1)
						{
							f.Exists=true;
						}
						if(!f.Exists)
						{
							Debug.WriteLine(f.Name+" not exists");
							gv.sql="Alter Table "+t.Name+" add column "+f.Name +" "+f.Type ;
							if(f.Type=="Text")
							{
								gv.sql += " ("+f.Size+")";
							}
							if(f.Type=="YesNo")
							{
								gv.sql += " DEFAULT "+f.DefaultValue;
							}
							gv.cmd = new OleDbCommand(gv.sql,gv.conn);
							gv.cmd.ExecuteNonQuery();
							lb.Items.Add(gv.sql);
						}
					}
				}
			}
            gv.ds.Tables.Add(gv.conn.GetSchema("indexes"));
            foreach (DataColumn dc in gv.ds.Tables["Indexes"].Columns)
            {
//                MessageBox.Show(dc.ColumnName);
            }

            foreach (index i in indexes)
            {
                foreach (DataRow drow in gv.ds.Tables["Indexes"].Rows)
                {
                    if (drow["INDEX_NAME"].ToString() == i.Name)
                    {
                        i.Exists = true;
                    }
                }
                if(!i.Exists)
                {
                    gv.sql = "CREATE UNIQUE INDEX " + i.Name + " ON " + i.Table + i.Field;
                    if (i.WithPrimary)
                    {
                        gv.sql += " WITH PRIMARY";
                    }
                    gv.cmd = new OleDbCommand(gv.sql, gv.conn);
                    gv.cmd.ExecuteNonQuery();
                    lb.Items.Add(gv.sql);
                }
            }
            
            

            gv.sql = "grant select on msysrelationships to Admin";
			gv.cmd = new OleDbCommand(gv.sql,gv.conn);
			gv.cmd.ExecuteNonQuery();
			lb.Items.Add(gv.sql);
			foreach(relation r in relations)
			{
				gv.sql = "select * from msysrelationships"; 
				gv.adap.SelectCommand = new OleDbCommand(gv.sql,gv.conn);
				gv.adap.Fill(gv.ds,"relation");
				foreach(DataRow drow in gv.ds.Tables["relation"].Rows)
				{
					string t = drow["szrelationship"].ToString();
					if(t == r.Name)
					{
						r.Exists = true;
					}
				}
				if(!r.Exists)
				{
					gv.sql = "Alter Table "+ r.Table +" ADD CONSTRAINT "+r.Name+" FOREIGN KEY ("+r.ForeignKey+") REFERENCES "+r.ReferenceTable +"("+r.ReferenceKey+")";
					//+" ON UPDATE SET NULL ON DELETE SET NULL";
					gv.cmd = new OleDbCommand(gv.sql,gv.conn);
					gv.cmd.ExecuteNonQuery();
					lb.Items.Add(gv.sql);
				}
			}
			gv.sql = "Revoke select on msysrelationships from Admin";
			gv.cmd = new OleDbCommand(gv.sql,gv.conn);
			gv.cmd.ExecuteNonQuery();
			lb.Items.Add(gv.sql);
		}
		catch(Exception ex)
		{
			Debug.WriteLine(ex.Message);
            MessageBox.Show(gv.sql+"\n"+ex.Message);
		}
		finally
		{
			gv.conn.Close();
			lb.Items.Add("Connection Closed!!");
		}
	}
    public void backupdatabase()
    {
        FileStream sfilestream = null;
        FileStream dfileStream = null;
        GZipStream stream = null;
        string sfile = "";
        string dfile = "";
        try
        {
            sfile = Application.StartupPath + "\\Backup\\" + DateTime.Now.DayOfWeek + ".bak";
            dfile = sfile + ".gz";

            File.Copy(gv.mdbfile, sfile, true);
            File.Delete(dfile);

            sfilestream = File.OpenRead(sfile);
            dfileStream = File.Create(dfile);
            stream = new GZipStream(dfileStream, CompressionMode.Compress);
            byte[] bytes = new byte[2048];
            int bytesread;
            while ((bytesread = sfilestream.Read(bytes, 0, bytes.Length)) != 0)
            {
                stream.Write(bytes, 0, bytesread);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            sfilestream.Close();
            stream.Close();
            dfileStream.Close();
            File.Delete(sfile);
            MessageBox.Show("Backup Completed Successfully !!");

        }
    }
    void restoredatabase()
    {
        ///
        ///sample code
        ///
        FileStream sourceFileStream = File.OpenRead("sitemap.xml.gz");
        FileStream destFileStream = File.Create("sitemap.xml");

        GZipStream decompressingStream = new GZipStream(sourceFileStream,
        CompressionMode.Decompress);
        int byteRead;
        while ((byteRead = decompressingStream.ReadByte()) != -1)
        {
            destFileStream.WriteByte((byte)byteRead);
        }

        decompressingStream.Close();
        sourceFileStream.Close();
        destFileStream.Close();
    }
    public void compactandrepair(string connstring, string mdbfilename)
    {
        string compactdbstring = "Provider = Microsoft.Jet.OLEDB.4.0 ; Jet OLEDB:Engine Type=5 ; Data Source = " + Application.StartupPath + "\\tempdb.mdb ";
        // filling parameter array 
        object[] oparams = new object[] { connstring, compactdbstring };
        //create an instance of jet replication object
        object objjro = Activator.CreateInstance(Type.GetTypeFromProgID("JRO.JetEngine"));
        // invoking CompactDatabase method of jro object
        objjro.GetType().InvokeMember("CompactDatabase", System.Reflection.BindingFlags.InvokeMethod, null, objjro, oparams);
        //database is compacted now ,to a new file tempdb.mdb, now delete the old file and copy tempdb over mdb
        File.Delete(mdbfilename);
        File.Move(Application.StartupPath + "\\tempdb.mdb", mdbfilename);

        //clean the memory
        System.Runtime.InteropServices.Marshal.ReleaseComObject(objjro);
        objjro = null;

        MessageBox.Show("Database Compacted Successfully !!");
    }
}
