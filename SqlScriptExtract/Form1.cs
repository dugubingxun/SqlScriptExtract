using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SqlScriptExtract
{
    public partial class Form1 : Form
    {

       
        public Form1()
        {
            InitializeComponent();
        }
        bool ischang = true;
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try {
                //--  SqlServer  MySql  Oracle
                //string strtype = "SqlServer";
                string strtype = cmbdatatype.Text;
                string strlk = "";
                switch (strtype)
                {
                    case "SqlServer":
                         strlk = "Server=" + txtserver.Text + (txtport.Text ==""?"": ","+txtport.Text) + ";Database=master;User ID=" + txtuser.Text + ";Password='" + txtpwd.Text + "'"; //sqlserver
                        break;
                    case "MySql":
                        strlk = "server=" + txtserver.Text + "; port=" + txtport.Text + "; user id=" + txtuser.Text + "; password=" + txtpwd.Text + "; database=" + cmbdatabase.Text + ";"; //mysql  // database="+cmbdatabase.Text +";
                        break;
                    case "Oracle":
                        strlk = "Provider=OraOLEDB.Oracle.1;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = " + txtserver.Text + ")(PORT = " + txtport.Text + "))) (CONNECT_DATA =(SERVICE_NAME=" + cmbdatabase.Text + ")));User Id=" + txtuser.Text + ";Password=" + txtpwd.Text + ";";
                        break;
                }
                
               // 1521   3306
                // string strlk = "Provider=OraOLEDB.Oracle.1;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = "+txtserver.Text +")(PORT = 1521))) (CONNECT_DATA =(SERVICE_NAME=cmshn)));User Id="+txtuser.Text +";Password="+txtpwd.Text +";"; //oracle
                // string strlk ="server="+txtserver.Text +"; user id="+txtuser.Text +"; password="+txtpwd.Text +"; database=plusoft_test;"; //mysql

                DBReadOrWrite mylink = new DBReadOrWrite();
                mylink.dbType = strtype;
                mylink.connstr = strlk;
                mylink.BeginConn();
                if (mylink.dbState == true)
                {
                    //MessageBox.Show("登陆成功！");
                    btnLogin.Enabled = false;
                    mylink.EndConn();
                    ArrayList mydatabase = mylink.Select("SELECT * FROM Master..SysDatabases");//sqlserver
                    ischang = false;
                    cmbdatabase.Items.Clear();
                    foreach(Hashtable ondata in mydatabase)
                    {
                        string onname = (ondata["name"]==null?"": ondata["name"].ToString());
                        cmbdatabase.Items.Add(onname);
                    }


                }
                else
                {
                    MessageBox.Show("数据库登陆失败，请检查配制信息");
                }

            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message );
            }
           
        }
        //计划做个连接，后还想想没必要
        //private void btnlink_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string strtype = "SqlServer";
        //        string strlk = "Server=" + txtserver.Text + ";Database=" + cmbdatabase.Text + ";User ID=" + txtuser.Text + ";Password='" + txtpwd.Text + "'"; //sqlserver
        //                                                                                                                                                      // string strlk = "Provider=OraOLEDB.Oracle.1;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = "+txtserver.Text +")(PORT = 1521))) (CONNECT_DATA =(SERVICE_NAME=cmshn)));User Id="+txtuser.Text +";Password="+txtpwd.Text +";"; //oracle
        //                                                                                                                                                      // string strlk ="server="+txtserver.Text +"; user id="+txtuser.Text +"; password="+txtpwd.Text +"; database=plusoft_test;"; //mysql
        //        if (ischang == true)
        //        {
        //            MessageBox.Show("请重新登陆后再连接！");
        //        }
        //        else
        //        {

        //            DBReadOrWrite mylink = new DBReadOrWrite();
        //            mylink.dbType = strtype;
        //            mylink.connstr = strlk;
        //            mylink.BeginConn();
        //            if (mylink.dbState == true)
        //            {
        //                MessageBox.Show("连接成功！");
        //                btnLogin.Enabled = false;
        //                mylink.EndConn();
        //            }
        //            else
        //            {
        //                MessageBox.Show("连接失败！");
        //            }
        //        }
        //    }
        //    catch(Exception ex )
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void btnextract_Click(object sender, EventArgs e)
        {
            try
            {
                #region//数据

                string strtype = cmbdatatype.Text;
                string strlk = "";
                switch (strtype)
                {
                    case "SqlServer":
                        strlk = "Server=" + txtserver.Text + (txtport.Text == "" ? "" : "," + txtport.Text) + ";Database=master;User ID=" + txtuser.Text + ";Password='" + txtpwd.Text + "'"; //sqlserver
                        break;
                    case "MySql":
                        strlk = "server=" + txtserver.Text + "; port=" + txtport.Text + "; user id=" + txtuser.Text + "; password=" + txtpwd.Text + "; database=" + cmbdatabase.Text + ";"; //mysql  // database="+cmbdatabase.Text +";
                        break;
                    case "Oracle":
                        strlk = "Provider=OraOLEDB.Oracle.1;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = " + txtserver.Text + ")(PORT = " + txtport.Text + "))) (CONNECT_DATA =(SERVICE_NAME=" + cmbdatabase.Text + ")));User Id=" + txtuser.Text + ";Password=" + txtpwd.Text + ";";
                        break;
                }
                //string strtype = "SqlServer";
                //string strlk = "Server=" + txtserver.Text + ";Database=" + cmbdatabase.Text + ";User ID=" + txtuser.Text + ";Password='" + txtpwd.Text + "'"; //sqlserver
                string strsql = txtsql.Text;    // string strlk = "Provider=OraOLEDB.Oracle.1;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = "+txtserver.Text +")(PORT = 1521))) (CONNECT_DATA =(SERVICE_NAME=cmshn)));User Id="+txtuser.Text +";Password="+txtpwd.Text +";"; //oracle
                string strfieldlt = txtfieldflt.Text;
                ArrayList notcols = new ArrayList();                                                                                                                                                                   // string strlk ="server="+txtserver.Text +"; user id="+txtuser.Text +"; password="+txtpwd.Text +"; database=plusoft_test;"; //mysql
                string[] sqlitem = strsql.ToLower().Split(' ');
                string[] fieldflt = strfieldlt.ToLower().Split(',');
                foreach (string strfield in fieldflt)
                {
                    string strfieldflt = strfield.ToLower();
                    notcols.Add(strfieldflt);
                }
                
                if (ischang == true)
                {
                    MessageBox.Show("请重新登陆后再进行取数！");
                }
                else
                {
                    string strtablename = ""; ///获取表名
                    string strwhere = ""; ///获取条件
                    string strorder = ""; //获取排序

                    ///
                    bool isfrom = false;
                    bool iswhere = false;
                    bool isorder = false;
                    int isby = 0;
                    //获取循环
                    foreach (string stritm in sqlitem)
                    {
                        bool isbj = false;
                        if (stritm.IndexOf("from") > -1)
                        {
                            isfrom = true;
                            isbj = true;
                        }
                        if (stritm.IndexOf("where") > -1)
                        {
                            isfrom = false;
                            iswhere = true;
                            isbj = true;
                        }
                        if (stritm.IndexOf("order") > -1)
                        {
                            isfrom = false;
                            iswhere = false;
                            isorder = true;
                            isbj = true;
                            isby = 1;
                        }

                        string strchar = stritm.Replace(" ", "").Replace("\t", "").Replace("\n", "");
                        #region//from
                        if (isfrom)
                        {
                            ///  \t   \n
                            if (strchar != "from")
                            {
                                if (isbj)
                                {
                                    string[] sqlfromT = stritm.ToLower().Split('\t');
                                    if (sqlfromT.Length > 1)
                                    {
                                        for (int i = 1; i < sqlfromT.Length; i++)
                                        {
                                            strtablename += sqlfromT[i];
                                        }
                                    }
                                    else
                                    {
                                        string[] sqlfromN = stritm.ToLower().Split('\t');
                                        if (sqlfromN.Length > 1)
                                        {
                                            for (int i = 1; i < sqlfromN.Length; i++)
                                            {
                                                strtablename += sqlfromN[i];
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    strtablename += stritm;
                                }
                            }
                        }
                        #endregion

                        #region//where
                        if (iswhere)
                        {
                            ///  \t   \n
                            if (strchar != "where")
                            {
                                if (isbj)
                                {
                                    string[] sqlfromT = stritm.ToLower().Split('\t');
                                    if (sqlfromT.Length > 1)
                                    {
                                        for (int i = 1; i < sqlfromT.Length; i++)
                                        {
                                            strwhere += " " + sqlfromT[i];
                                        }
                                    }
                                    else
                                    {
                                        string[] sqlfromN = stritm.ToLower().Split('\t');
                                        if (sqlfromN.Length > 1)
                                        {
                                            for (int i = 1; i < sqlfromN.Length; i++)
                                            {
                                                strwhere += " " + sqlfromN[i];
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    strwhere += " " + stritm;
                                }
                            }
                        }
                        #endregion

                        #region//order 
                        if (isorder)
                        {

                            if (stritm == "by" && isby == 1)
                            {
                                isby++;
                                continue;
                            }

                            ///  \t   \n
                            if (isby == 1)
                            {
                                string[] sqlfromT = stritm.ToLower().Split('\t');
                                if (sqlfromT.Length > 1)
                                {
                                    for (int i = 1; i < sqlfromT.Length; i++)
                                    {
                                        strorder += " " + sqlfromT[i];
                                    }
                                }
                                else
                                {
                                    string[] sqlfromN = stritm.ToLower().Split('\t');
                                    if (sqlfromN.Length > 1)
                                    {
                                        for (int i = 1; i < sqlfromN.Length; i++)
                                        {
                                            strorder += " " + sqlfromN[i];
                                        }
                                    }
                                }
                            }
                            else
                            {
                                strorder += " " + stritm;
                            }
                        }
                        #endregion



                    }




                    DBReadOrWrite mylink = new DBReadOrWrite();
                    mylink.dbType = strtype;
                    mylink.connstr = strlk;
                    mylink.BeginConn();

                    if (mylink.dbState == true)
                    {
                        mylink.EndConn();
                        switch (cmbdatatype.Text)
                        {
                            case "SqlServer":
                                #region //获取不插入字段 Sqlserver
                                //获取表ID
                                ArrayList tableinfos = mylink.Select("select *  from Sysobjects where  type='U' and  name='" + strtablename + "' ");
                                if (tableinfos.Count > 0)
                                {
                                    Hashtable tableinfo = (Hashtable)tableinfos[0];
                                    string strtableid = tableinfo["id"].ToString();
                                    //根据表ID查找是否有自增以及时间戳
                                    ArrayList tmpnotcols = mylink.Select("select * from  syscolumns  where id=" + strtableid + " and status in(128,40) ");
                                    foreach (Hashtable notc in tmpnotcols)
                                    {
                                        string strcol = notc["name"].ToString().ToLower();
                                        notcols.Add(strcol);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("获取表信息失败！");
                                    return;
                                }
                                #endregion
                                break;
                            case "Oracle":
                                #region //获取不插入字段 oracle

                                //表的查询语句
                                //select * from all_tables where owner='用户名' all_tables查出来是查得所有用户下的表，当然也包括你登录的用下的表，然后加一个where你要查的那个用户名就可以了。（记得用户名要大写）
                                //;   select * from user_tables; 查的单纯是你所登录的用户下的表，不会显示其他用户下的表。;
                                //select * from tabs;
                                //根据表ID查找是否有自增以及时间戳
                                //select t.table_name,  --表名 t.column_name, --字段名 t.data_type,   --字段类型  t.data_length-- 字段长度 from  user_tab_columns t where   t.table_name = '表名';
                                //没有查出怎么样就不让往进插入值
                                //ArrayList tmpnotcolsora = mylink.Select("SELECT *  FROM user_tab_columns WHERE  table_name = '" + strtablename + "' ");// AND  (extra='auto_increment' OR data_type='timestamp')
                                //foreach (Hashtable notc in tmpnotcolsora)
                                //    {
                                //        string strcol = notc["column_name"].ToString().ToLower();
                                //        notcols.Add(strcol);
                                //    }
                              
                                #endregion
                                break;
                            case "MySql":
                                #region //获取不插入字段 mysql
                                ////select  table_name, table_type, `engine` from information_schema.tables where 1 = 1  and table_schema = '数据库' ;
                                ArrayList tmpnotcolsmysql = mylink.Select("SELECT *  FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = '" + cmbdatabase.Text + "' AND TABLE_NAME = '" + strtablename + "' AND  (extra='auto_increment' OR data_type='timestamp') ");
                                foreach (Hashtable notc in tmpnotcolsmysql)
                                {
                                    string strcol = notc["column_name"].ToString().ToLower();
                                    notcols.Add(strcol);
                                }
                                #endregion
                                break;
                                
                        }




                        //
                        if (chkdel.Checked)
                        {
                            if (chkmemo.Checked)
                            {
                                txtscrip.AppendText(Environment.NewLine + "--删除表" + strtablename + "中数据");
                            }
                            if (strwhere != "")
                            {
                                txtscrip.AppendText(Environment.NewLine + "delete  " + strtablename + " where " + strwhere);
                            }
                            else
                            {
                                txtscrip.AppendText(Environment.NewLine + "delete  " + strtablename);
                            }
                        }
                        #region //获取数据
                        ArrayList tmpdatas = mylink.Select(strsql);
                        if (chkmemo.Checked)
                        {
                            txtscrip.AppendText(Environment.NewLine + "--插入数据到表" + strtablename);
                        }
                        txtscrip.AppendText(Environment.NewLine + "--数据总行数：" + tmpdatas.Count.ToString());
                        long i = 0;
                        #endregion
                        #region //生成脚本

                        if (bgWorker.IsBusy)
                            return;
                        this.prgbar.Maximum = tmpdatas.Count;
                        Hashtable myrefdata = new Hashtable();
                        myrefdata["datas"] = tmpdatas;
                        myrefdata["tablename"] = strtablename;
                        myrefdata["notcols"] = notcols;

                        bgWorker.RunWorkerAsync(myrefdata);
                        //txtscrip.AppendText(Environment.NewLine + "--插入数据到表" + strtablename);
                        //foreach (Hashtable row in tmpdatas)
                        //{
                        //    //txtscrip.AppendText(Environment.NewLine +"--"+ i.ToString());
                        //    i++;
                        //    if (isstop == true)
                        //    {
                        //        isstop = false;
                        //        break;
                        //    }
                        //    string stristfield = "";
                        //    string strvalue = "";
                        //    foreach (string fieldkey in row.Keys)
                        //    {
                        //        bool isnot = false;
                        //        foreach (string notkey in notcols)
                        //        {
                        //            if (notkey == fieldkey)
                        //            {
                        //                isnot = true;
                        //            }
                        //        }
                        //        if (isnot == false)
                        //        {
                        //            stristfield += fieldkey + ",";
                        //            strvalue += (row[fieldkey] == null ? "null" : "'" + row[fieldkey].ToString() + "'") + ",";
                        //        }

                        //    }
                        //    string strisr = "insert into " + strtablename + "(" + stristfield.Substring(0, stristfield.Length - 1) + ")";
                        //    string stristvalue = "values(" + strvalue.Substring(0, strvalue.Length - 1) + ")";
                        //    txtscrip.AppendText(Environment.NewLine + strisr);
                        //    txtscrip.AppendText(Environment.NewLine + stristvalue);

                        //}
                        #endregion

                    }
                    else
                    {
                        MessageBox.Show("连接失败！");
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

           

        }
        //创建脚本
        private void CreateScrip(ArrayList tmpdatas,string strtablename,ArrayList notcols,ref bool iscancel)
        {
            #region //生成脚本
            //txtscrip.AppendText(Environment.NewLine + "--插入数据到表" + strtablename);
            int i = 0;
            foreach (Hashtable row in tmpdatas)
            {
                //txtscrip.AppendText(Environment.NewLine +"--"+ i.ToString());

                if (bgWorker.CancellationPending)
                {
                    iscancel = true;
                    break;
                }
                else
                {

                    i++;
                    string stristfield = "";
                    string strvalue = "";
                    foreach (string fieldkey in row.Keys)
                    {
                        bool isnot = false;
                        foreach (string notkey in notcols)
                        {
                            if (notkey == fieldkey)
                            {
                                isnot = true;
                            }
                        }
                        if (isnot == false)
                        {
                            stristfield += fieldkey + ",";
                            strvalue += (row[fieldkey] == null ? "null" : "'" + row[fieldkey].ToString() + "'") + ",";
                        }

                    }
                    string strisr = "insert into " + strtablename + "(" + stristfield.Substring(0, stristfield.Length - 1) + ")";
                    string stristvalue = "    values(" + strvalue.Substring(0, strvalue.Length - 1) + ")";

                    AppendScrip(Environment.NewLine + strisr);
                    AppendScrip(Environment.NewLine + stristvalue);
                    //txtscrip.AppendText(Environment.NewLine + strisr);
                    //txtscrip.AppendText(Environment.NewLine + stristvalue);
                    //返回行号
                    bgWorker.ReportProgress(i, "Working");
                    //System.Threading.Thread.Sleep(10);
                }

            }
            #endregion
        }
        delegate void CallBack(string str); //定义一个委托类型,返回值和参数和方法类型一致
        Delegate AppendText_delegate; //定义一个委托类型的函数
        void AppendScrip(string str) //追加脚本，处理不同线程委托
        { 
            //获取线程是否正在被调用: InvokeRequired 当前线程不是创建控件的线程时为true(此时必须调用Invoke函数) 
            if (txtscrip.InvokeRequired)
            {
                //绑定方法
                AppendText_delegate = new CallBack(AppendScrip);
                object[] params_obj = new object[] { str };
                txtscrip.Invoke(AppendText_delegate, params_obj);
            }
            else
            {
                txtscrip.AppendText(str );
            }

        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            txtscrip.Text = "";
        }

        private void txtserver_TextChanged(object sender, EventArgs e)
        {
            btnLogin.Enabled = true;
            ischang = true;
        }

        private void txtuser_TextChanged(object sender, EventArgs e)
        {
            btnLogin.Enabled = true;
            ischang = true;
        }

        private void txtpwd_TextChanged(object sender, EventArgs e)
        {
            btnLogin.Enabled = true;
            ischang = true;
        }

        private void btnstop_Click(object sender, EventArgs e)
        {
            bgWorker.CancelAsync();
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Hashtable myrefdata = (Hashtable) e.Argument;
            ArrayList tmpdatas = (ArrayList)myrefdata["datas"];
            string strtablename = myrefdata["tablename"].ToString();
            ArrayList notcols = (ArrayList)myrefdata["notcols"];
            //myrefdata["datas"] = tmpdatas;
            //myrefdata["tablename"] = strtablename;
            //myrefdata["notcols"] = notcols;
            bool iscancel = false;

            CreateScrip(tmpdatas, strtablename, notcols,ref iscancel);
            e.Cancel = iscancel;

        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //处理进度  //Convert.ToString(e.ProgressPercentage)
            //string state = (string)e.UserState;//接收ReportProgress方法传递过来的userState
            prgbar.Value = e.ProgressPercentage;
           
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.ToString());
                return;
            }
            if (!e.Cancelled)
            {
                MessageBox.Show("处理完毕!");
            }
            else
            {
                MessageBox.Show("处理终止!");
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbdatatype.Items.Clear();
            cmbdatatype.Items.Add("SqlServer");
            cmbdatatype.Items.Add("Oracle");
            cmbdatatype.Items.Add("MySql");
            cmbdatatype.Text = "SqlServer";

            //txtport.Text = "1433";
        }

        private void cmbdatatype_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbdatatype.Text )
            {
                case "SqlServer":
                    txtport.Text = "1433";
                    break;
                case "Oracle":
                    txtport.Text = "1521";
                    break;
                case "MySql":
                    txtport.Text = "3306";
                    break;
            }
        }
    }
}
