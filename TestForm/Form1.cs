using CPU;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;


namespace TestForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("11111");
            OraDB DB = new OraDB();
            MessageBox.Show("22222");
            DB.Connect();
            MessageBox.Show("3333");
            List<String> data = null;
            string sql = "select * from tab";
            MessageBox.Show( sql );
            data = DB.GetQueryResults<string>( sql , delegate(OracleDataReader reader)
            {
                MessageBox.Show("---------");
                string val = reader[0].ToString();
                MessageBox.Show(val);
                System.Diagnostics.Debug.WriteLine(val);
                return val;
            });

            MessageBox.Show("--2222-------");

            System.Diagnostics.Debug.WriteLine(data);

            MessageBox.Show("--3333-------");

            clsClassfication cls = new clsClassfication();
            MessageBox.Show("--4442-------");

            cls.load("1");
            cls.CATE_NAME = "AAAAAAAAAAAA";
            MessageBox.Show("--5552-------");
            cls.update("2222");
            List<clsClassfication> lst =  clsClassfication.getList("11", "", false);
            //cls.insert("22", "2");
            MessageBox.Show("--2666-------");
        }
    }
}
