using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

class rangeinput: Form
{
    TextBox tb1, tb2;
    Label lb1, lb2;
    Button btn1, btn2;
    public string Start
    { get; set; }
    public string Stop
    { get; set; }
    public rangeinput()
    {
        CenterToParent();
        Height = 200;
        Width = 500;

        lb1 = new Label {  Text ="FROM",Parent = this,Location = new Point(50,30) };
        tb1 = new TextBox { Parent = this ,Location = new Point(150, 30) };

        lb2 = new Label {  Text ="TO",Parent = this , Location = new Point(250, 30) };
        tb2 = new TextBox { Parent = this , Location = new Point(350, 30) };

        btn1 = new Button { Text = "OK", Parent = this, Location = new Point(100, 60) };
        btn1.Click += new EventHandler(okclick);

        btn2 = new Button { Text = "CANCEL",Parent = this, Location = new Point(250, 60) };
        btn2.Click += new EventHandler(cancelclick);
    }
    void okclick(object sender , EventArgs e)
    {
        DialogResult = DialogResult.OK;
        Start = tb1.Text;
        Stop = tb2.Text;
        Close();
    }
    void cancelclick(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}