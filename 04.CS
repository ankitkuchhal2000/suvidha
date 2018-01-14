using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Diagnostics; 
using System.ComponentModel;
public class inireader
{
	[DllImport("kernel32")]
	private static extern int GetPrivateProfileString(string section, string Key,string value, StringBuilder result, int size, string filePath);
	
	[DllImport("kernel32")]
	private static extern long WritePrivateProfileString(string section, string key,string val, string filePath);
	
	string fname = "";
	int size = 500;
	public inireader(string filename)
	{
		fname = filename;
	}
	public string readstring(string section,string key)
	{
		StringBuilder sb = new StringBuilder(size);
		GetPrivateProfileString(section,key,"",sb,size,fname);
		string retval = sb.ToString();
		if(string.IsNullOrEmpty(retval))
		{
			retval = "";
		}
		return retval;
	}
	public bool readbool(string section,string key)
	{
		bool b;
		string s = readstring(section,key);
		return bool.TryParse(s,out b);
	}
	public void writestring(string section , string key , string value)
	{	
		WritePrivateProfileString(section,key,value,fname);
	}
}