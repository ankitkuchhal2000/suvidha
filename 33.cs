using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;

class prinbar
{
    string brandname = "";
    int recnum = 0;
    int pricounter = 0; //number of copies per label
    int nostk = 0; //total number of sticker
    bool printdate = false;
    DataTable dtchoice = null;
    PrintDocument doc;
    inireader ir = new inireader(gv.inifile);
    public void print()
    {
        try
        {
            gv.conn.Open();
            gv.ds = new DataSet();
            gv.adap = new OleDbDataAdapter();
            gv.sql = " select bill.brandname from bill where id = " + gv.billid;
            Debug.WriteLine(gv.sql);
            gv.adap.SelectCommand = new OleDbCommand(gv.sql, gv.conn);
            gv.adap.Fill(gv.ds, "firm");
            //brandname = gv.ds.Tables[0].Rows[0][0].ToString();
            //format(item.id,'00000') as [Bar Code]
            gv.sql = " select item.itemname as [Item Name],item.id ,item.barcode as [Bar Code],itemb.mrp as [MRP],itemb.pkgdate as [PKG Date]" +
            " from itemb " +
            "inner join item on item.id=itemb.itemid";
            Debug.WriteLine(gv.sql);
            gv.adap.SelectCommand = new OleDbCommand(gv.sql, gv.conn);
            gv.adap.Fill(gv.ds, "item");
            //			DataColumn boolcol = new DataColumn("Select",typeof(bool));
            //			boolcol.DefaultValue = false;
            //			gv.ds.Tables[1].Columns.Add(boolcol);
            DataColumn doublecol = new DataColumn("Number of Sticker", typeof(double));
            doublecol.DefaultValue = 0;
            gv.ds.Tables[1].Columns.Add(doublecol);

            DataTable dt = new DataTable();

            searchbox.Show("Select Item Name and Number of Sticker", null, gv.ds.Tables[1], ref dt, "[Number of Sticker] > 0", "[Item Name]", "");
            dtchoice = dt;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            gv.conn.Close();
        }

        nostk = 1;
        pricounter = 1;
        recnum = 0;
        bool.TryParse(ir.readstring("printing", "dateinlabel"), out printdate);

        doc = new PrintDocument();

        doc.BeginPrint += new PrintEventHandler(docbeginprint);
        doc.PrintPage += new PrintPageEventHandler(docprint);

        /*
		PrintPreviewDialog ppd = new PrintPreviewDialog();
		ppd.Document = doc;
		ppd.ShowDialog();
		/**/
        doc.Print();

        doc = null;
    }
    void docbeginprint(object sender, PrintEventArgs e)
    {
        if (e.PrintAction != PrintAction.PrintToPreview)
        {
            //PrintDialog printDlg = new PrintDialog();
            //printDlg.Document = doc;

            //if (printDlg.ShowDialog() == DialogResult.OK)
            //{
            //	doc.PrinterSettings = printDlg.PrinterSettings;
            doc.DocumentName = "Barcodes";
            doc.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("PaperA4", 840, 1180);
            doc.DefaultPageSettings.Margins.Top = 40;
            doc.DefaultPageSettings.Margins.Left = 40;
            doc.DefaultPageSettings.Margins.Right = 0;
            doc.DefaultPageSettings.Margins.Bottom = 0;
            doc.OriginAtMargins = true;
            //}
            //else
            //{
            //	e.Cancel = true;
            //}
        }
    }
    void docprint(object sender, PrintPageEventArgs e)
    {
        int maxrow = 13;

        int lineheight = 14;
        int linewidth = 160;

        int row = 0;

        int txt1x = 0;
        int txt1y = 0 * lineheight;

        int txt2x = 0;
        int txt2y = 1 * lineheight;

        int txt3x = 0;
        int txt3y = 2 * lineheight;

        int txt4x = 0;
        int txt4y = 3 * lineheight;

        int imgx = 0;
        int imgy = 4 * lineheight;

        int txt5x = 0;
        int txt5y = 5 * lineheight;

        Graphics g = e.Graphics;
        Font fnt = new Font("Arial", 8);
        while (recnum < dtchoice.Rows.Count)
        {
//            num = Convert.ToInt32(dtchoice.Rows[recnum]["id"]);
//            fillform();

            g.DrawString(dtchoice.Rows[recnum]["Item Name"].ToString(), fnt, Brushes.Black, txt1x, txt1y);
            g.DrawString("Mrp. " + dtchoice.Rows[recnum]["MRP"].ToString() + "/-", fnt, Brushes.Black, txt2x, txt2y);
            if (printdate)
            {
                g.DrawString("Pkg. Date " + Convert.ToDateTime(dtchoice.Rows[recnum]["PKG Date"]).ToString("dd/MM/yyyy"), fnt, Brushes.Black, txt3x, txt3y);
            }
            g.DrawString(brandname, fnt, Brushes.Black, txt4x, txt4y);
            Image img = Code128Rendering.MakeBarcodeImage(dtchoice.Rows[recnum]["Bar Code"].ToString(), 1, false);
            g.DrawImage(img, imgx, imgy);
            g.DrawString(dtchoice.Rows[recnum]["Bar Code"].ToString(), fnt, Brushes.Black, txt5x, txt5y);

            txt1x += linewidth;
            txt2x += linewidth;
            txt3x += linewidth;
            txt4x += linewidth;
            imgx += linewidth;
            txt5x += linewidth;

            if (nostk % 5 == 0)//even
            {
                txt1x = 0;
                txt1y += (6 * lineheight) - 1;

                txt2x = 0;
                txt2y += (6 * lineheight) - 1;

                txt3x = 0;
                txt3y += (6 * lineheight) - 1;

                txt4x = 0;
                txt4y += (6 * lineheight) - 1;

                imgx = 0;
                imgy += (6 * lineheight) - 1;

                txt5x = 0;
                txt5y += (6 * lineheight) - 1;

                Debug.WriteLine("row number" + row.ToString());
                row++;
            }
            nostk++;
            if (pricounter < Convert.ToInt32(dtchoice.Rows[recnum]["Number of Sticker"]))
            {
                pricounter++;
            }
            else
            {
                recnum++;
                pricounter = 1;
            }
            if (row != maxrow || recnum >= dtchoice.Rows.Count)
            {
                e.HasMorePages = false;
            }
            else
            {
                e.HasMorePages = true;
                return;
            }
        }
    }
}