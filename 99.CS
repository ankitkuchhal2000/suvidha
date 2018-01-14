using System;
using System.Diagnostics;
using Microsoft.PointOfService;

public class Posprinter
{
	PosExplorer pe;
	DeviceInfo di;
	PosPrinter printer;
	public Posprinter()
	{
		pe = new PosExplorer();
		di = pe.GetDevice(DeviceType.PosPrinter);
		printer = pe.CreateInstance(di) as PosPrinter;
	}
	public void Print(string data)
	{
		try
		{
			printer.Open();
			printer.Claim(10000);
			if (printer.Claimed) 
			{
				printer.DeviceEnabled = true;
				printer.PrintNormal(PrinterStation.Receipt,data);
				printer.DeviceEnabled = false;   
			}
			printer.Release();
			printer.Close();
		}
		catch(Exception ex)
		{
			Debug.WriteLine(ex.Message);
		}
		finally
		{
		}
	}
}