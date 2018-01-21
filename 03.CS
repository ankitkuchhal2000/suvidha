using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Diagnostics; 
using System.ComponentModel;
using System.Text;
using System.Globalization;
using System.Resources;

public class gv //global variable class
{
	public static OleDbConnection conn = null;
	public static OleDbDataAdapter adap = null;
    public static OleDbTransaction trans = null;
	public static DataSet ds;
	public static bool loginsuc = false, authcopy = false;
    public static string sql = "", connstr = "", mdbfile = "", mdwfile = "", listfile = "", inifile = "",key = "NEWTON56";
	public static string cipher ="", companyname = "Demo Company";
	public static int billid = 0;
	public static DateTime yearstartdate = DateTime.Now, yearenddate = DateTime.Now,workingdate = DateTime.Now;
    public enum frmtoopen { lotlist, milling, user, item, batch, invoice,party,voucher };
    public enum mode { view, newentry,modify, delete,import,deleterange };
    public enum vouchertype { CASH_RECEIPT,CASH_PAYMENT,BANK_DEPOSIT,BANK_WITHDRAW,journal,unassigned};
    public static ResourceManager resman;
	public static CultureInfo cul;        
	public static string AccessDateTime(object dgvValue)
	{
		string retstr="";
		if(dgvValue.GetType() ==typeof(DBNull))
		{
			retstr="NULL";
		}
		else
		{
			retstr="#"+Convert.ToDateTime(dgvValue).ToString("MM/dd/yyy")+"#";
		}
		return retstr;
	}
	
	public static string PadCenter(string text, int newWidth)
    {
        const char filler = ' ';
        int length = text.Length;
        int charactersToPad = newWidth - length;
        if (charactersToPad < 0) throw new ArgumentException("New width must be greater than string length.", "newWidth");
        int padLeft = charactersToPad/2 + charactersToPad%2;
        //add a space to the left if the string is an odd number
        int padRight = charactersToPad/2;

        StringBuilder resultBuilder = new StringBuilder(newWidth);
        for (int i = 0; i < padLeft; i++) resultBuilder.Insert(i, filler); 
        for (int i = 0; i < length; i++) resultBuilder.Insert(i + padLeft, text[i]); 
        for (int i = newWidth - padRight; i < newWidth; i++) resultBuilder.Insert(i, filler);
        return resultBuilder.ToString();
    }
	
	public static string ntoc( long number)
	{
		if (number == 0) return "ZERO";  
	    if (number < 0) return "minus " + ntoc(Math.Abs(number));  
	    string words = "";  
	    if ((number / 1000000) > 0)   
	    {  
	        words += ntoc(number / 100000) + " LAKES ";  
	        number %= 1000000;  
	    }  
	    if ((number / 1000) > 0)   
	    {  
	        words += ntoc(number / 1000) + " THOUSAND ";  
	        number %= 1000;  
	    }  
	    if ((number / 100) > 0)   
	    {  
	        words += ntoc(number / 100) + " HUNDRED ";  
	        number %= 100;  
	    }
//		if ((number / 10) > 0)  
//	    {
//			words += ntoc(number / 10) + " RUPEES ";
//			number %= 10;  
//		}  
	    if (number > 0)   
	    {  
	        if (words != "") words += "AND ";  
	        string[] unitsMap = new string[]   
	        {  
	            "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"  
	        };  
	        string[] tensMap = new string[] { "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY"};  
	        if (number < 20)
				words += unitsMap[number];  
	        else   
	        {  
	            words += tensMap[number / 10];  
	            if ((number % 10) > 0) words += " " + unitsMap[number % 10];  
	        }  
	    }  
	    return words;  
	}
}