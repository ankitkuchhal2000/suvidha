using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Diagnostics; 
using System.ComponentModel;
public class about:Form
{
	Label lbl1;
	Label lbl2;
	
	LinkLabel lkl1;
	
	Button btn1;
	
	public about()
	{
		MaximizeBox = false;
		Width = 330;
		Height = 330;
		Text = "About";
        CenterToScreen();

        lbl1 = new Label
        {
            Text = "SUVIDHA",
            Location = new Point(100, 0),
            AutoSize = true,
            Font = new Font("Times New Roman", 24.0F),
            Parent = this
        };

        lbl2 = new Label
        {
            Location = new Point(100, 50),
            BorderStyle = BorderStyle.Fixed3D,
            Height = 2,
            Width = 175,
            Parent = this
        };

        btn1 = new Button();
		btn1.Text = "&Ok";
		btn1.Location = new Point (200,260);
		btn1.Parent = this;
        btn1.Click += new EventHandler(cloclick);

        lkl1 = new LinkLabel
        {
            Location = new Point(100, 60),
            Text = "Indo Technologies\n" +
                    "House No. 8, Priti Nagar,\n" +
                    "Hisar, Haryana,India\n" +
                    "Pin - 125001\n" +
                    "Phone: +91-89509-45909\n" +
                    "E-mail: ankitkuchhal2000@gmail.com\n" +
                    "website: www.ankitkuchhal.com\n\n" +
                    "SYSTEM REQUIREMENTS\n" +
                    "1) .Net Framework 4.0\n" +
                    "2) Crystal Reports (Re-distributables)\n" +
                    "3) Reports (Zip)\n" +
                    "4) Configuration File",
            Width = 200,
            Height = 170,
            Parent = this
        };
        lkl1.Links.Add(190, 19, "www.medionline.co.in");
        lkl1.Links.Add(212, 35, "www.medionline.co.in");
        lkl1.Links.Add(251, 14, "www.medionline.co.in");
        lkl1.Links.Add(268, 20, "www.medionline.co.in");
        lkl1.LinkClicked += new LinkLabelLinkClickedEventHandler(linclick);

    }

    void linclick(object sender, LinkLabelLinkClickedEventArgs e)
	{
		
		lkl1.Links[lkl1.Links.IndexOf(e.Link)].Visited = true;
		string target = e.Link.LinkData as string;
		if(target != null && target.StartsWith("www"))
		{
			Process.Start(target);
		}
	}
	void cloclick(object sender, EventArgs e)
	{
		Close();
	}
}