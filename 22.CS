using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Diagnostics; 
using System.ComponentModel;
using System.Net.Mail;

public class email:Form
{
	Label lbl1;
	Label lbl2;
	Label lbl3;
	Label lbl4;
	Label lbl5;
	Label lbl6;
	Label lbl7;
	Label lbl8;
	Label lbl9;
	Label lbl10;
	
	TextBox tb1;
	TextBox tb2;
	TextBox tb3;
	TextBox tb4;
	TextBox tb5;
	TextBox tb6;
	TextBox tb7;
	TextBox tb8;
	TextBox tb9;
	TextBox tb10;
	
	Button btn1;
	Button btn2;
	
	OpenFileDialog ofd;
	
	MailMessage mail;
	
	public email()
	{
		Size = new Size(620,530);
		WindowState = FormWindowState.Maximized;
		
		lbl1 = new Label();
		lbl1.Text = "From";
		lbl1.Location = new Point(0,0);
		lbl1.Parent = this;
		
		lbl2 = new Label();
		lbl2.Text = "To";
		lbl2.Location = new Point(0,30);
		lbl2.Parent = this;
		
		lbl3 = new Label();
		lbl3.Text = "CC";
		lbl3.Location = new Point(0,60);
		lbl3.Parent = this;
		
		lbl4 = new Label();
		lbl4.Text = "BCC";
		lbl4.Location = new Point(0,90);
		lbl4.Parent = this;
		
		lbl5 = new Label();
		lbl5.Text = "Subject";
		lbl5.Location = new Point(0,120);
		lbl5.Parent = this;
		
		lbl6 = new Label();
		lbl6.Text = "Body";
		lbl6.Location = new Point(0,150);
		lbl6.Parent = this;
		
		lbl7 = new Label();
		lbl7.Text = "Attachments";
		lbl7.Location = new Point(0,180);
		lbl7.Parent = this;
		
		lbl8 = new Label();
		lbl8.Text = "Email Password";
		lbl8.Location = new Point(0,210);
		lbl8.Parent = this;
	
		lbl9 = new Label();
		lbl9.Text = "SMTP Server";
		lbl9.Location = new Point(0,240);
		lbl9.Parent = this;
		
		lbl10 = new Label();
		lbl10.Text = "Port";
		lbl10.Location = new Point(0,270);
		lbl10.Parent = this;
		
		tb1 = new TextBox();
		tb1.Text = "kanwalhisar@rediffmail.com";
		tb1.Location = new Point(100,0);
		tb1.Parent = this;
		
		tb2 = new TextBox();
		tb2.Text = "ankitkuchhal2000@gmail.com";
		tb2.Location = new Point(100,30);
		tb2.Parent = this;
		
		tb3 = new TextBox();
		tb3.Location = new Point(100,60);
		tb3.Parent = this;
		
		tb4 = new TextBox();
		tb4.Location = new Point(100,90);
		tb4.Parent = this;
		
		tb5 = new TextBox();
		tb5.Location = new Point(100,120);
		tb5.Text = "Test";
		tb5.Parent = this;
		
		tb6 = new TextBox();
		tb6.Location = new Point(100,150);
		tb6.Text = "Test";
		tb6.Parent = this;
		
		tb7 = new TextBox();
		tb7.Location = new Point(100,180);
		tb7.Parent = this;
		
		btn1 = new Button();
		btn1.Location = new Point(200,180);
		btn1.Text = "&Open File";
		btn1.Click += new EventHandler(opeclick);
		btn1.Parent = this;
		
		tb8 = new TextBox();
		tb8.Location = new Point(100,210);
		tb8.Text = "kanwal";
		tb8.Parent = this;
		
		tb9 = new TextBox();
		tb9.Location = new Point(100,240);
		tb9.Text = "smtp.rediffmail.com";
		tb9.Parent = this;
		
		tb10 = new TextBox();
		tb10.Location = new Point(100,270);
		tb10.Text = "587";
		tb10.Parent = this;
		
		btn2 = new Button();
		btn2.Location = new Point(0,300);
		btn2.Text="Send";
		btn2.Click += new EventHandler(senclick);
		btn2.Parent = this;
		
		mail = new MailMessage();
	}
	void opeclick(object sender, EventArgs e)
	{
		tb7.Text = "";
		ofd = new OpenFileDialog();
		ofd.Multiselect = true;
		if(ofd.ShowDialog()==DialogResult.OK)
		{
			tb7.Text = String.Join(",",ofd.FileNames);
		}
	}
	void senclick(object sender, EventArgs e)
	{
		mail.From = new MailAddress(tb1.Text);
		
		string[] to = tb2.Text.Split(','); 
		for(int i = 0; i<to.Length;i++)
		{
			if(to[i]!="")
			{
				mail.To.Add(to[i]);
			}
		}
		string[] cc = tb3.Text.Split(',');
		for(int i = 0; i<cc.Length;i++)
		{
			if(cc[i]!="")
			{
				mail.CC.Add(cc[i]);
			}
		}
		string[] bcc = tb4.Text.Split(',');
		for(int i = 0; i<bcc.Length;i++)
		{
			if(bcc[i]!="")
			{
				mail.Bcc.Add(bcc[i]);
			}
		}
		mail.Subject = tb5.Text;
		mail.Body = tb6.Text;
		string[] attach = tb7.Text.Split(',');
		for(int i = 0; i <attach.Length;i++ )
		{
			if(File.Exists(attach[i]))
			{
				mail.Attachments.Add(new Attachment(ofd.FileNames[i]));
			}
		}
		
		SmtpClient smtpserver = new SmtpClient(tb9.Text);
		smtpserver.Port = Convert.ToInt32(tb10.Text);
		smtpserver.DeliveryMethod = SmtpDeliveryMethod.Network;
		smtpserver.Credentials = new System.Net.NetworkCredential(tb1.Text, tb8.Text);
		smtpserver.EnableSsl = true;
		//smtpserver.Timeout = 30000;
		smtpserver.SendCompleted += new SendCompletedEventHandler(sendcomplete);
		object userstate = mail;
		smtpserver.SendAsync(mail,userstate);
	}
	
	void sendcomplete(object sender, AsyncCompletedEventArgs e)
	{
		if(e.Error != null)
		{
			MessageBox.Show("Attachment are greater then 3MB"+e.ToString());
		}
		else
		{
			MessageBox.Show("Mail Send");
		}
	}
}
