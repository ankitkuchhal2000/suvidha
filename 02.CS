//A0EF-F3C9-479E-ACE2-CFAB-D5D0-029B-F8AF
using System;
using System.IO;
using System.Windows.Forms;

class Program
{
	[STAThread]
	static void Main (string[]args)
	{
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        gv.mdwfile = Application.StartupPath+"\\system.mdw";
		gv.inifile = Application.StartupPath+"\\config.ini";
		gv.listfile = Application.StartupPath+"\\list.mdb";
		if (args.Length > 0)
		{
			gv.listfile = args[0];
		}
		if(!File.Exists(gv.listfile))
		{
			MessageBox.Show("DATABASE LIST FILE NOT FOUND, PLEASE CHECK CONFIGURATION");
			return;
		}

        if (gv.listfile != "")
        {
            Application.Run(new Companylist());
        }
		if(gv.mdbfile!="")
		{
			Application.Run (new registration());
		}
		if (gv.authcopy)
		{
			Application.Run (new login());
		}
		if(gv.loginsuc)
		{
			Application.Run (new baseform());
		}
		
	}
} 