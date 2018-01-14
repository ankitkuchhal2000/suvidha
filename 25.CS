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
public class mandelbort:Form
{
	Bitmap b = null;
	string[] colorfile = Directory.GetFiles(Application.StartupPath+"\\ColorMaps", "*.ColorMap");
	ListBox lb;
	public mandelbort()
	{
		Size = new Size(620,530);
		WindowState = FormWindowState.Maximized;
		
		lb = new ListBox();
		lb.Location = new Point(0,0);
		lb.Parent = this;
		lb.Visible= false;
		drawmandelbort(0);
	}
	void drawmandelbort(int mapindex)
	{
		List<Color> c = getcolors(colorfile[mapindex]);
		b = new Bitmap(Width,Height);
		lb.DataSource = c ;
		double sx = -2.1;
		double sy = -1.3;
		double fx = 1;
		double fy = 1.3;
		double minx = sx;
		double miny = sy;
		double maxx= fx;
		double maxy= fy;
		double intigralx = (maxx-minx)/Width;
		double intigraly = (maxy-miny)/Height;
		double x = minx;
		for(int s = 1 ;s<Width;s++)
		{
			double y = miny;
			for(int z = 1;z<Height;z++)
			{
				int looper = 0;
				double x1 = 0;
				double y1 = 0;
				while(looper<100&&Math.Sqrt((x1*x1)+(y1*y1))<2)
				{
					looper++;
					double xx =(x1*x1)-(y1*y1)+x;
					y1 = 2 * x1 * y1 + y;
					x1 = xx;
				}
				double perc = looper/(100.0);
				int val = (int)(perc*255);
				
				b.SetPixel(s,z,c[val]);
				y+=intigraly;
			}
			x+=intigralx;
		}
		BackgroundImage = (Image)b;
	}
	
	List<Color> getcolors(string path)
	{
		Debug.WriteLine(path);
		List<Color> c = new List<Color>();
		StreamReader sr = new StreamReader(path);
		string s = sr.ReadLine();
		while(s!=null)
		{
			int r = int.Parse(s.Split(' ')[0]);
			int g = int.Parse(s.Split(' ')[1]);
			int b = int.Parse(s.Split(' ')[2]);
			Color ctmp =Color.FromArgb(r,g,b);
			c.Add(ctmp);
			s = sr.ReadLine();
		}
		int i=0;
		for (i=0;i<Math.Min(256,c.Count);i++)
		{}
		for (int j = i ; j<256;j++)
		{
			c.Add(Color.White);
		}
		return c;
	}
}