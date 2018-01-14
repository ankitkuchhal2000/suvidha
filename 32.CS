using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Diagnostics; 
using System.ComponentModel;
using System.Text;
using System;
using System.Net;
using System.Collections.Generic; 
public class update:Form
{
	string zipfile="";
	
	Label lbl1;
	Label lbl2;
	Label lbl3;
	
	ProgressBar pbar; 
	ftp ftpClient;
	
	Button btn1;
	Button btn2;
	Button btn3;
	Button btn4;
	Button btn5;
	Button btn6;
	Button btn7;
	Button btn8;
	Button btn9;
	
	public update()
	{
		ftpClient = new ftp(@"ftp://medionline.co.in/", "update@medionline.co.in", "123456");
		
		lbl1= new Label();
		lbl1.Text="1.PLEASE ENSURE THAT YOU HAVE BACKUP OF SOFTWARE BEFORE PURSUING FOR UPDATE.\n2.PLEASE ENSURE THAT MEDI IS CLOSED(NOT RUNNING). ANY WHERE IN LAN ALSO.";
		lbl1.Location= new Point(20,30);
		lbl1.AutoSize=true;
		lbl1.Parent= this;
		
		pbar = new ProgressBar();
		pbar.Maximum=110;
		pbar.Location = new Point(20,390);
		pbar.Width=550;
		pbar.Height=30;
		pbar.Parent = this;
		
		lbl2 = new Label();
		lbl2.Location = new Point(20,410);
		lbl2.AutoSize=true;
		lbl2.Parent = this;
		
		lbl3 = new Label();
		lbl3.Location = new Point(450,410);
		lbl3.AutoSize=true;
		lbl3.Parent = this;
		
		btn1 = new Button();
		btn1.Text = "CHECK FOR UPDATE !!";
		btn1.Width = 275;
		btn1.Location = new Point(20,90);
		btn1.Click += new EventHandler(chkupd);
        btn1.Visible = false;
		btn1.Parent = this;
		
		btn2= new Button();
		btn2.Text="PRINTER: LASER, INVOICE SIZE:6 INCH, BUSINESS:PHARMA WHOLESALE";
		btn2.Width=550;
		btn2.Location = new Point(20,120);
		btn2.Click += new EventHandler(las6);	
		btn2.Parent=this;	
		
		btn3= new Button();
		btn3.Text="PRINTER: DOT MATRIX, INVOICE SIZE:6 INCH, BUSINESS:PHARMA WHOLESALE";
		btn3.Width=550;
		btn3.Location = new Point(20,150);
		btn3.Click += new EventHandler(dot6);	
		btn3.Parent=this;

		btn4= new Button();
		btn4.Text="PRINTER: LASER, INVOICE SIZE:6 INCH AND 12 INCH (CHIOCE), BUSINESS:PHARMA WHOLESALE";
		btn4.Width=550;
		btn4.Location = new Point(20,180);
		btn4.Click += new EventHandler(las612);	
		btn4.Parent=this;

		btn5 = new Button();
		btn5.Text = "PRINTER: LASER, INVOICE SIZE:12 INCH (WITHOUT PTR), BUSINESS:PHARMA WHOLESALE";
		btn5.Width=550;
		btn5.Location = new Point(20,210);
		btn5.Click += new EventHandler(las12);
		btn5.Parent = this;		

		btn6 = new Button();
		btn6.Text = "PRINTER: LASER, INVOICE SIZE:12 INCH (WITH PTR), BUSINESS:PHARMA WHOLESALE";
		btn6.Width=550;
		btn6.Location = new Point(20,240);
		btn6.Click += new EventHandler(las12ptr);
		btn6.Parent = this;		
		
		btn7 = new Button();
		btn7.Text = "PRINTER: DOT MATRIX, INVOICE SIZE:12 INCH, BUSINESS:PHARMA WHOLESALE";
		btn7.Width=550;
		btn7.Location = new Point(20,270);
		btn7.Click += new EventHandler(dot12);
		btn7.Parent = this;

		btn8 = new Button();
		btn8.Text = "PRINTER: LASER, INVOICE SIZE:6 INCH, BUSINESS:PHARMA RETAIL";
		btn8.Width=550;
		btn8.Location = new Point(20,300);
		btn8.Click += new EventHandler(ret);
		btn8.Parent = this;
		
		btn9 = new Button();
		btn9.Text = "UPDATE SOFTWARE USING CUSTOMIZE/FMCG INVOICE";
		btn9.Width=550;
		btn9.Location = new Point(20,360);
        btn9.Visible = false;
		btn9.Click += new EventHandler(cust);
		btn9.Parent = this;
	}
	void chkupd(object sender,EventArgs e)
	{}
	void las6(object sender,EventArgs e)
	{
		if(!File.Exists(Application.StartupPath+"\\MEDI.EXE")||!File.Exists(Application.StartupPath+"\\BILL.DBF"))
		{
			MessageBox.Show("KINDLY COPY AND PAST THIS PROGRAM TO YOUR MEDI FOLDER.\nUNABLE TO LOCATE MEDI APPLICATION HERE\nUNABLE TO GET THE FIRM NAME FOR UPDATE.");
			return;
		}
		string m = "YOU HAD CHOOSE TO UPDATE\nWITH PHARMACEUTICAL SOFTWARE!\nARE YOU SURE TO CONTINUE?\nPRESS YES TO CONTINUE OR PRESS NO TO CANCEL. ";
		DialogResult result = MessageBox.Show(this,m,"",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
		if(result ==DialogResult.Yes)
		{
			copy(Application.StartupPath,"..\\UPDATEBOTMEDIBACK"+DateTime.Now.ToString("ddMMyyyyhhmmss"));
			senddata();
			File.Delete(Application.StartupPath+"\\TOOLS.EXE");
			downupd("las6");
			unzip();
		}
	}
	void dot6(object sender,EventArgs e)
	{
		if(!File.Exists(Application.StartupPath+"\\MEDI.EXE")||!File.Exists(Application.StartupPath+"\\BILL.DBF"))
		{
			MessageBox.Show("KINDLY COPY AND PAST THIS PROGRAM TO YOUR MEDI FOLDER.\nUNABLE TO LOCATE MEDI APPLICATION HERE\nUNABLE TO GET THE FIRM NAME FOR UPDATE.");
			return;
		}

		string m = "YOU HAD CHOOSE TO UPDATE\nWITH PHARMACEUTICAL SOFTWARE!\nARE YOU SURE TO CONTINUE?\nPRESS YES TO CONTINUE OR PRESS NO TO CANCEL. ";
		DialogResult result = MessageBox.Show(this,m,"",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
		if(result ==DialogResult.Yes)
		{
			copy(Application.StartupPath,"..\\UPDATEBOTMEDIBACK"+DateTime.Now.ToString("ddMMyyyyhhmmss"));
			senddata();		
			File.Delete(Application.StartupPath+"\\TOOLS.EXE");
			downupd("dot6");
			unzip();
		}
	}
    void las612(object sender, EventArgs e)
    {
        if (!File.Exists(Application.StartupPath + "\\MEDI.EXE") || !File.Exists(Application.StartupPath + "\\BILL.DBF"))
        {
            MessageBox.Show("KINDLY COPY AND PAST THIS PROGRAM TO YOUR MEDI FOLDER.\nUNABLE TO LOCATE MEDI APPLICATION HERE\nUNABLE TO GET THE FIRM NAME FOR UPDATE.");
            return;
        }

        string m = "YOU HAD CHOOSE TO UPDATE\nWITH PHARMACEUTICAL SOFTWARE!\nARE YOU SURE TO CONTINUE?\nPRESS YES TO CONTINUE OR PRESS NO TO CANCEL. ";
        DialogResult result = MessageBox.Show(this, m, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        if (result == DialogResult.Yes)
        {
            copy(Application.StartupPath, "..\\UPDATEBOTMEDIBACK" + DateTime.Now.ToString("ddMMyyyyhhmmss"));
            senddata();
            File.Delete(Application.StartupPath + "\\TOOLS.EXE");
            downupd("las612");
            unzip();
        }
    }
    void las12(object sender,EventArgs e)
	{
		if(!File.Exists(Application.StartupPath+"\\MEDI.EXE")||!File.Exists(Application.StartupPath+"\\BILL.DBF"))
		{
			MessageBox.Show("KINDLY COPY AND PAST THIS PROGRAM TO YOUR MEDI FOLDER.\nUNABLE TO LOCATE MEDI APPLICATION HERE\nUNABLE TO GET THE FIRM NAME FOR UPDATE.");
			return;
		}
		
		string m = "YOU HAD CHOOSE TO UPDATE\nWITH PHARMACEUTICAL SOFTWARE!\n ARE YOU SURE TO CONTINUE?\nPRESS YES TO CONTINUE OR PRESS NO TO CANCEL. ";
		DialogResult result = MessageBox.Show(this,m,"",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
		if(result ==DialogResult.Yes)
		{
			copy(Application.StartupPath,"..\\UPDATEBOTMEDIBACK"+DateTime.Now.ToString("ddMMyyyyhhmmss"));
			senddata();
			File.Delete(Application.StartupPath+"\\TOOLS.EXE");
			downupd("las12");
			unzip();
		}
	}

	void las12ptr(object sender,EventArgs e)
	{
		if(!File.Exists(Application.StartupPath+"\\MEDI.EXE")||!File.Exists(Application.StartupPath+"\\BILL.DBF"))
		{
			MessageBox.Show("KINDLY COPY AND PAST THIS PROGRAM TO YOUR MEDI FOLDER.\nUNABLE TO LOCATE MEDI APPLICATION HERE\nUNABLE TO GET THE FIRM NAME FOR UPDATE.");
			return;
		}

		string m = "YOU HAD CHOOSE TO UPDATE\nWITH PHARMACEUTICAL SOFTWARE!\n ARE YOU SURE TO CONTINUE?\nPRESS YES TO CONTINUE OR PRESS NO TO CANCEL. ";
		DialogResult result = MessageBox.Show(this,m,"",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
		if(result ==DialogResult.Yes)
		{
			copy(Application.StartupPath,"..\\UPDATEBOTMEDIBACK"+DateTime.Now.ToString("ddMMyyyyhhmmss"));
			senddata();
			File.Delete(Application.StartupPath+"\\TOOLS.EXE");		
			downupd("las12ptr");
			unzip();
		}
	}	
	void dot12(object sender,EventArgs e)
	{
		if(!File.Exists(Application.StartupPath+"\\MEDI.EXE")||!File.Exists(Application.StartupPath+"\\BILL.DBF"))
		{
			MessageBox.Show("KINDLY COPY AND PAST THIS PROGRAM TO YOUR MEDI FOLDER.\nUNABLE TO LOCATE MEDI APPLICATION HERE\nUNABLE TO GET THE FIRM NAME FOR UPDATE.");
			return;
		}
		
		string m = "YOU HAD CHOOSE TO UPDATE\nWITH PHARMACEUTICAL SOFTWARE!\nARE YOU SURE TO CONTINUE?\nPRESS YES TO CONTINUE OR PRESS NO TO CANCEL. ";
		DialogResult result = MessageBox.Show(this,m,"",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
		if(result ==DialogResult.Yes)
		{
			copy(Application.StartupPath,"..\\UPDATEBOTMEDIBACK"+DateTime.Now.ToString("ddMMyyyyhhmmss"));
			senddata();
			File.Delete(Application.StartupPath+"\\TOOLS.EXE");
			downupd("dot12");
			unzip();
		}
	}	
	void ret(object sender,EventArgs e)
	{
		if(!File.Exists(Application.StartupPath+"\\MEDI.EXE")||!File.Exists(Application.StartupPath+"\\BILL.DBF"))
		{
			MessageBox.Show("KINDLY COPY AND PAST THIS PROGRAM TO YOUR MEDI FOLDER.\nUNABLE TO LOCATE MEDI APPLICATION HERE\nUNABLE TO GET THE FIRM NAME FOR UPDATE.");
			return;
		}
		
		string m = "YOU HAD CHOOSE TO UPDATE\nWITH PHARMACEUTICAL SOFTWARE!\nARE YOU SURE TO CONTINUE?\nPRESS YES TO CONTINUE OR PRESS NO TO CANCEL.";
		DialogResult result = MessageBox.Show(this,m,"",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
		if(result ==DialogResult.Yes)
		{
			copy(Application.StartupPath,"..\\UPDATEBOTMEDIBACK"+DateTime.Now.ToString("ddMMyyyyhhmmss"));
			senddata();
			File.Delete(Application.StartupPath+"\\TOOLS.EXE");
			downupd("ret");
			unzip();
		}
	}
	void cust(object sender,EventArgs e)
	{
		if(!File.Exists(Application.StartupPath+"\\MEDI.EXE")||!File.Exists(Application.StartupPath+"\\BILL.DBF"))
		{
			MessageBox.Show("KINDLY COPY AND PAST THIS PROGRAM TO YOUR MEDI FOLDER.\nUNABLE TO LOCATE MEDI APPLICATION HERE\nUNABLE TO GET THE FIRM NAME FOR UPDATE.");
			return;
		}
		
		string m = "YOU HAD CHOOSE TO UPDATE\nWITHOUT CHANGEING INVOICE FORMAT!\nARE YOU SURE TO CONTINUE?\nPRESS YES TO CONTINUE OR PRESS NO TO CANCEL.";
		DialogResult result = MessageBox.Show(this,m,"",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
		if(result ==DialogResult.Yes)
		{
			copy(Application.StartupPath,"..\\UPDATEBOTMEDIBACK"+DateTime.Now.ToString("ddMMyyyyhhmmss"));
			senddata();
			File.Delete(Application.StartupPath+"\\TOOLS.EXE");
			zipfile = uploadinv();//not required in pre-set updates
			downupd("");
			unzip();
		}
	}

	void unzip()
	{
			Process process;
			if(zipfile!="")
			{
				process = new Process();
				process.StartInfo.FileName = Application.StartupPath+"\\fbzip.exe";
				process.StartInfo.Arguments=" -e -p "+zipfile+" \""+Application.StartupPath+"\"";
				process.Start();
				process.WaitForExit();
			}
			
			process = new Process();
			process.StartInfo.FileName = Application.StartupPath+"\\tools.exe";
			process.Start();
			process.WaitForExit();

			MessageBox.Show("UPDATE SUCCESSFULL!!\nNOTE:-PLEASE START MEDI AND DO FILE CREATION IN ALL OPTION MODE.\nPRESS OK TO EXIT THE PROGRAM" );
			Close();
	}
	string uploadinv()
	{
		string[] arr = new string[10];
		try
		{
			WebClient cl;
			cl = new WebClient();
			cl.Headers.Add("Content-Type","binary/octet-stream");
			cl.DownloadProgressChanged += new DownloadProgressChangedEventHandler(progchange);
			cl.DownloadFileCompleted += new AsyncCompletedEventHandler(progcompleted);
			byte[] result=cl.UploadFile("http://103.50.163.80/test.php","POST", Application.StartupPath+"\\INV.PRG");
			string s = System.Text.Encoding .UTF8 .GetString (result,0,result.Length );	
			arr = s.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
			MessageBox.Show(s);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
		return arr[7];
	}
	void downupd(string foldername)
	{
		
		List<string> filelist = new List<string>();
		
		if(zipfile=="")
		{
			filelist.Add("CHALP.O");
			filelist.Add("DATADICT.DBF");
			filelist.Add("DW.EXE");
			filelist.Add("inv.o");
			filelist.Add("INV.PRG");
			filelist.Add("M.BAT");
			filelist.Add("MAIL.EXE");
			filelist.Add("MEDI.O");
			filelist.Add("PPP.O");
			filelist.Add("PRINBAR.O");
			filelist.Add("RRR.O");
			filelist.Add("SSS.O");
			filelist.Add("STATE.DBF");
			filelist.Add("TMEDI.O");
			filelist.Add("TOOLS.EXE");
			filelist.Add("TTT.O");
			filelist.Add("XXX.O");
			filelist.Add("medi.exe");
			foreach(string fn in filelist)
			{
				pbar.Value = 0;
				string filnam="http://medionline.co.in/update/"+foldername+"/"+fn;
				string location =Application.StartupPath+"\\"+fn;
				lbl3.Text= location;
				download(new Uri(filnam),location);
			}
		}
		else
		{
			filelist.Add("FbZip.Exe");
			filelist.Add("FbZip.bin");
			filelist.Add(zipfile);
			foreach(string fn in filelist)
			{
				pbar.Value=0;
				string filnam="http://103.50.163.80/"+fn;
				string location = Application.StartupPath+"\\"+fn;
				download(new Uri(filnam),location);
			}
		}
	}
	void download (Uri todownload,string savelocation)
	{
		WebClient cl;
		cl = new WebClient();
		cl.Headers.Add("Content-Type","binary/octet-stream");
		cl.DownloadProgressChanged += new DownloadProgressChangedEventHandler(progchange);
		cl.DownloadFileCompleted += new AsyncCompletedEventHandler(progcompleted);
		cl.DownloadFileAsync(todownload,savelocation );
		//without this, async download will go crazy and wont download anything
		while (cl.IsBusy) {Application.DoEvents(); }
	}
	void senddata()//procedure to send firmname to server 
	{
		DateTime fdate = File.GetLastWriteTime(Application.StartupPath+"\\MEDI.EXE");
		OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=dBase III;Data Source="+Application.StartupPath);
        conn.Open();
		string sql= "select FIRMNAME,ADDRESS,ADDRESS1,PHONEO,PHONER,PHONEM,COMPANY4 from bill";
		OleDbCommand cmd = new OleDbCommand(sql, conn);
		OleDbDataAdapter adap = new OleDbDataAdapter(cmd);
		DataSet ds = new DataSet();
		adap.Fill(ds,"data");
		conn.Close();
		StringWriter wrt = new StringWriter();
		ds.WriteXml(wrt);
		string xml=wrt.ToString();
		xml=xml.Replace("&amp;","");
		string exedate = fdate.ToString();
		
		string req = "http://medionline.co.in/update/my.php?data="+xml+"&exedate="+exedate;
		HttpWebRequest request = (HttpWebRequest)WebRequest.Create(req);
    	byte[] bytes;
    	bytes = System.Text.Encoding.ASCII.GetBytes(xml);
    	request.ContentType = "text/xml; encoding='utf-8'";
    	request.ContentLength = bytes.Length;
    	request.Method = "POST";
    	Stream requestStream = request.GetRequestStream();
    	requestStream.Write(bytes, 0, bytes.Length);
    	requestStream.Close();
    	HttpWebResponse response;
    	response = (HttpWebResponse)request.GetResponse();
    	if (response.StatusCode == HttpStatusCode.OK)
    	{
    	    Stream responseStream = response.GetResponseStream();
    	    string responseStr = new StreamReader(responseStream).ReadToEnd();
    	    MessageBox.Show(responseStr);
    	}
	}

	void progchange(object sender, DownloadProgressChangedEventArgs e)
	{
		this.BeginInvoke((MethodInvoker) delegate {
        double bytesIn = double.Parse(e.BytesReceived.ToString());
        double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
        double percentage = bytesIn / totalBytes * 100;
        lbl2.Text = "Downloaded " + e.BytesReceived/1000 + "kb of " + e.TotalBytesToReceive/1000 +"kb";
        pbar.Value = int.Parse(Math.Truncate(percentage).ToString());
    	});
	}
	void progcompleted(object sender, AsyncCompletedEventArgs e)
	{
		this.BeginInvoke((MethodInvoker) delegate {
			pbar.Value=pbar.Maximum;
    	});		
	}
	
	void copy( string sourcedir,string targetdir)
	{
		DirectoryInfo source = new DirectoryInfo(sourcedir);
		DirectoryInfo target = new DirectoryInfo(targetdir);
		copyall(source,target);		
	}
	void copyall(DirectoryInfo source,DirectoryInfo target)
	{
		Directory.CreateDirectory(target.FullName);

        // Copy each file into the new directory.
        foreach (FileInfo fi in source.GetFiles())
        {
            lbl2.Text="Copying "+target.FullName+"\\"+fi.Name;
			lbl2.Refresh();
            fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
        }
		
		// Copy each subdirectory using recursion.
        foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
        {
            DirectoryInfo nextTargetSubDir =
                target.CreateSubdirectory(diSourceSubDir.Name);
            copyall(diSourceSubDir, nextTargetSubDir);
        }
	}

}