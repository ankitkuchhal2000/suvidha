using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Resources;

public class baseform:Form
{
	inireader ir = new inireader(gv.inifile);
	MainMenu mm;
	StatusBar sb;
	StatusBarPanel sp1;
	
	public baseform()
	{
        if (File.Exists(Application.StartupPath + "\\back.png"))
        {
            BackgroundImage = Image.FromFile(Application.StartupPath + "\\back.png");
            BackgroundImageLayout = ImageLayout.Tile;
        }
        if (File.Exists(Application.StartupPath + "\\icon.ico"))
        {
            Icon = new Icon(Application.StartupPath + "\\icon.ico");
        }
        mm = new MainMenu();
        Menu = mm;
        IsMdiContainer = true;
        Text = gv.companyname;
        Size = new Size(640, 630);
        WindowState = FormWindowState.Maximized;

        sb = new StatusBar { ShowPanels = true, Parent = this };
        sp1 = new StatusBarPanel  { AutoSize = StatusBarPanelAutoSize.Spring};
        sb.Panels.Add(sp1);
        sp1.Text = gv.mdbfile;

        MenuItem mi1 = new MenuItem { Text = "&ENTRY" };
        MenuItem mi2 = new MenuItem { Text = "&REPORTS" };
        MenuItem mi3 = new MenuItem { Text = "&STEUP" };
        MenuItem mi4 = new MenuItem { Text = "&MISCELLANOUS" };
        MenuItem mi5 = new MenuItem { Text = "&HELP" };

        MenuItem midb = new MenuItem { Text = "&DATABASE" };
        midb.MenuItems.Add(new MenuItem("&BACKUP",new EventHandler(backup)));
        midb.MenuItems.Add(new MenuItem("&COMPACT AND REPAIR",new EventHandler(compactrepair)));
        midb.MenuItems.Add(new MenuItem("CHECK &STRUCTURE", new EventHandler(checkstructure)));

        mi3.MenuItems.Add(new MenuItem("&PARTY", new EventHandler(partysetup)));
        mi3.MenuItems.Add(new MenuItem ("&USER", new EventHandler(usersetup)));
		mi3.MenuItems.Add(new MenuItem ("&CLIENT" , new EventHandler(clientsetup)));
		mi3.MenuItems.Add(midb);
        mi5.MenuItems.Add(new MenuItem("&ABOUT", new EventHandler(about),Shortcut.F1));

        mm.MenuItems.Add(mi1);
        mm.MenuItems.Add(mi2);
        mm.MenuItems.Add(mi3);
        mm.MenuItems.Add(mi4);
        mm.MenuItems.Add(mi5);

	}

    void partysetup(object sender,EventArgs e)
    {
        viewer frm = new viewer(gv.frmtoopen.party) { MdiParent = this };
        frm.Show();
    }
    void usersetup(object sender,EventArgs e)
    {
        viewer frm = new viewer(gv.frmtoopen.user) { MdiParent = this };
        frm.Show();
	}
    void compactrepair(object sender,EventArgs e)
	{
        createdb createdb = new createdb();
        createdb.backupdatabase();
        createdb.compactandrepair(gv.connstr, gv.mdbfile);
	}
    void checkstructure(object sender, EventArgs e)
    {
        createdb createdb = new createdb();
        createdb.create();
    }
    void backup(object sender, EventArgs e)
    {
        createdb createdb = new createdb();
        createdb.backupdatabase();
    }
    void clientsetup(object sennder , EventArgs e)
	{
        Client frm = new Client( gv.mode.modify,1){ MdiParent = this};
        frm.Show();
	}
    void about(object sender, EventArgs e)
    {
        about frm = new about();
        frm.ShowDialog(this);
    }
}