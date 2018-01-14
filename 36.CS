using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.IO.Compression;
using System.Diagnostics; 
using System.ComponentModel;
public static class esc
{
	///
	///ASCII TABLE
	///
	public static string NUL = (char)0+"";	//NULL
	public static string SOH = (char)1+"";	//START OF HEADING
	public static string STX = (char)2+"";	//START OF TEXT
	public static string ETX = (char)3+"";	//END OF TEXT
	public static string EOT = (char)4+"";	//END OF TRANSMISSION
	public static string ENQ = (char)5+"";	//ENQUIRY
	public static string ACK = (char)6+"";	//ACKNOWLEDGE
	public static string BE = (char)7+"";	//BELL
	public static string BS = (char)8+"";	//BACKSPACE
	public static string HT = (char)9+"";	//HORIZONTAL TAB
	public static string LF = (char)10+"";	//LINE FEED
	public static string VT = (char)11+"";	//VERTICAL TAB
	public static string FF = (char)12+"";	//FORM FEED
	public static string CR = (char)13+"";	//CARRIAGE RETURN
	public static string SO = (char)14+"";	//SHIFT OUT
	public static string SI = (char)15+"";	//SHIFT IN
	public static string DLE = (char)16+"";	//DATA LINK ESCAPE
	public static string DC1 = (char)17+"";	//DEVICE CONTROL 1
	public static string DC2 = (char)18+"";	//DEVICE CONTROL 2
	public static string DC3 = (char)19+"";	//DEVICE CONTROL 3
	public static string DC4 = (char)20+"";	//DEVICE CONTROL 4
	public static string NAK = (char)21+"";	//NEGATIVE ACKNOWLEDGE
	public static string SYN = (char)22+"";	//SYNCHRONUS IDLE
	public static string ETB = (char)23+"";	//END OF TRANSMISSION BLOCK
	public static string CAN = (char)24+"";	//CANCEL
	public static string EM = (char)25+"";	//END OF MEDIUM
	public static string SUB = (char)26+"";	//SUBSTITUTE
	public static string ESC = (char)27+"";	//ESCAPE CHARACTER
	public static string FS = (char)28+"";	//FILE SEPARATOR 
	public static string GS = (char)29+"";	//GROUP SEPARATOR 
	public static string RS = (char)30+"";	//RECORD SEPARATOR 
	public static string US = (char)31+"";	//UNIT SEPARATOR 
	public static string DE = (char)127+"";	//DEL
}