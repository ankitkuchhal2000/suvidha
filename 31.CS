using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Diagnostics; 
using System.ComponentModel;
using System.Text;
using System;
using System.Collections.Generic; 
public class iconextract:Form
{
	PictureBox pcb;
	
	Label lbl1;
	
	Button btn1;
	Button btn2;
	public iconextract()
	{	
		lbl1 = new Label();
		lbl1.Parent = this;
		
		pcb = new PictureBox();
		pcb.BorderStyle = BorderStyle.FixedSingle;
		pcb.Height = 150;
		pcb.Width = 250;
		pcb.Location = new Point(0,30);
		pcb.Parent = this;
		
		btn1 = new Button();
		btn1.Location = new Point(0,210);
		btn1.Text = "&Open";
		btn1.Click += new EventHandler(opeclick);
		btn1.Parent = this;
		
		btn2 = new Button();
		btn2.Location = new Point(100,210);
		btn2.Text = "&Save";
		btn2.Click += new EventHandler(savclick); 
		btn2.Parent = this;
		
		
		
	}
	void opeclick(object sender,EventArgs e)
	{
		try
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Title = "Select Photo";
			ofd.RestoreDirectory = true;
			ofd.Filter = "JPEG files(*.jpg)|*.jpg|All files(*.*)|*.*";
			//ofd.Filter = "Icon File (*.ico)|*.ico";
			if(ofd.ShowDialog()==DialogResult.OK)
			{
				Stream mstream = ofd.OpenFile();
				if (mstream  != null)
				{
					Image image = Image.FromFile(ofd.FileName); 
					Image newimage = image.GetThumbnailImage(32, 32, null, new IntPtr()); 
					lbl1.Text = ofd.FileName;
					pcb.Image = newimage;
				} 
			}
		}
		catch(Exception ex)
		{
			Debug.WriteLine(ex.Message);
		}
	}
	void savclick(object sender , EventArgs e )
	{
		SaveFileDialog sfd;
		sfd = new SaveFileDialog();
		sfd.DefaultExt = "ico";
		sfd.FileName = "Icon";
		sfd.Filter = "Icon File (*.ico)|*.ico";
		if (sfd.ShowDialog() == DialogResult.OK) 
        {
			String fileName = sfd.FileName; 
			Stream IconStream = File.OpenWrite(fileName); 
			
			Bitmap bitmap = new Bitmap(pcb.Image); 
            bitmap.SetResolution(72, 72); 
            Icon icon = System.Drawing.Icon.FromHandle(bitmap.GetHicon()); 
            icon.Save(IconStream); 
		}	
	}
}