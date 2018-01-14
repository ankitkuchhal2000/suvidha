using System.Windows.Forms;
using System.Drawing;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Windows.Forms;
using CrystalDecisions.Shared;

public class printpreview : Form
{
    CrystalReportViewer crv;
    ReportDocument doc;
	public printpreview()
    {
        MessageBox.Show("Please provide report file and crieteria");
    }
	public printpreview(string rptfilename,string recordselectionformula)
	{
			MessageBox.Show(rptfilename);
			StartPosition = FormStartPosition.CenterParent;
			Size = new Size(550,400);
		
			ConnectionInfo cni = new ConnectionInfo();
            if (gv.mdbfile == "")
            {
                cni.ServerName = gv.listfile;
            }
            else
            {
                cni.ServerName = gv.mdbfile;
            }
            doc = new ReportDocument();
			doc.Load(rptfilename);
			doc.RecordSelectionFormula=recordselectionformula;
			foreach(Table crtb in doc.Database.Tables)
			{
				TableLogOnInfo li = crtb.LogOnInfo;
				li.ConnectionInfo = cni;
				crtb.ApplyLogOnInfo(li);
			}
			crv = new CrystalReportViewer();
			crv.Dock = DockStyle.Fill;
			crv.Size = new Size(550,350);
			crv.ToolPanelView = ToolPanelViewType.None;
			crv.ReportSource = doc;
			crv.RefreshReport();
			crv.Parent = this;
	}
}