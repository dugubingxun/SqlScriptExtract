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

        BackgroundWorker bgWorker = new System.ComponentModel.BackgroundWorker();
        // 

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
                bgWorker.WorkerReportsProgress = true;
                bgWorker.WorkerSupportsCancellation = true;
                bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
                bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
                bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);

                string strtype = cmbdatatype.Text;
                string strlk = "";
                switch (strtype)
                {
                    case "SqlServer":
                        strlk = "Server=" + txtserver.Text + (txtport.Text == "" ? "" : "," + txtport.Text) + ";Database=" + cmbdatabase.Text + ";User ID=" + txtuser.Text + ";Password='" + txtpwd.Text + "'"; //sqlserver
                        break;
                    case "MySql":
                        strlk = "server=" + txtserver.Text + "; port=" + txtport.Text + "; user id=" + txtuser.Text + "; password=" + txtpwd.Text + "; database=" + cmbdatabase.Text + ";"; //mysql  // database="+cmbdatabase.Text +";
                        break;
                    case "Oracle":
                        strlk = "Provider=OraOLEDB.Oracle.1;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = " + txtserver.Text + ")(PORT = " + txtport.Text + "))) (CONNECT_DATA =(SERVICE_NAME=" + cmbdatabase.Text + ")));User Id=" + txtuser.Text + ";Password=" + txtpwd.Text + ";";
                        break;
                }
                DBReadOrWrite mylink = new DBReadOrWrite();
                mylink.dbType = strtype;
                mylink.connstr = strlk;
                mylink.BeginConn();

                if (mylink.dbState == true)
                {
                    mylink.EndConn();
                    txtscrip.Clear();
                    #region//数据
                    //string strtype = "SqlServer";
                    //string strlk = "Server=" + txtserver.Text + ";Database=" + cmbdatabase.Text + ";User ID=" + txtuser.Text + ";Password='" + txtpwd.Text + "'"; //sqlserver
                    string strsqlall= txtsql.Text;
                    string[] sqlitemall = strsqlall.ToLower().Split(';'); //多条语名用 分号 分开
                    ArrayList notcols = new ArrayList();
                    foreach (string strsqlone in sqlitemall)
                    {
                        string strsql = strsqlone;    // string strlk = "Provider=OraOLEDB.Oracle.1;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = "+txtserver.Text +")(PORT = 1521))) (CONNECT_DATA =(SERVICE_NAME=cmshn)));User Id="+txtuser.Text +";Password="+txtpwd.Text +";"; //oracle
                        string strfieldlt = txtfieldflt.Text;
                                                                                                                                                                                        // string strlk ="server="+txtserver.Text +"; user id="+txtuser.Text +"; password="+txtpwd.Text +"; database=plusoft_test;"; //mysql
                        string[] sqlitemkg = strsql.ToLower().Split(' ');
                        //string[] sqlitemhh = strsql.ToLower().Split('\n');
                        ArrayList sqlitem = new ArrayList();
                        foreach (string strfg in sqlitemkg)
                        {
                            string[] sqlitemhh = strfg.Split('\n');
                            foreach (string strh in sqlitemhh)
                            {
                                sqlitem.Add(strh);
                            }
                        }

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

                                string strchar = stritm.Replace(" ", "").Replace("\t", "").Replace("\n", "").Replace("\r", "");
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
                                                    strtablename += sqlfromT[i].Replace(" ", "").Replace("\t", "").Replace("\n", "").Replace("\r", "");
                                                }
                                            }
                                            else
                                            {
                                                string[] sqlfromN = stritm.ToLower().Split('\t');
                                                if (sqlfromN.Length > 1)
                                                {
                                                    for (int i = 1; i < sqlfromN.Length; i++)
                                                    {
                                                        strtablename += sqlfromN[i].Replace(" ", "").Replace("\t", "").Replace("\n", "").Replace("\r", "");
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            strtablename += stritm.Replace(" ", "").Replace("\t", "").Replace("\n", "").Replace("\r", "");
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
                            if (strtablename != "")
                            {
                                notcols = GetNotCols(strtablename, mylink);
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
                                try
                                {
                                    ArrayList tmpdatas = mylink.Select(strsql);

                                    if (chkmemo.Checked)
                                    {
                                        txtscrip.AppendText(Environment.NewLine + "--插入数据到表" + strtablename);
                                    }
                                    txtscrip.AppendText(Environment.NewLine + "--数据总行数：" + tmpdatas.Count.ToString());
                                    //long i = 0;
                                    #endregion
                                    #region //生成脚本



                                    if (bgWorker.IsBusy)
                                    {
                                        return;
                                    }
                                    this.prgbar.Maximum = tmpdatas.Count;
                                    Hashtable myrefdata = new Hashtable();
                                    myrefdata["datas"] = tmpdatas;
                                    myrefdata["tablename"] = strtablename;
                                    myrefdata["notcols"] = notcols;
                                    bgWorker.RunWorkerAsync(myrefdata);
                                }
                                catch
                                {
                                    ;
                                }
                                #endregion
                            }

                        }
                    }
                    #endregion

                    #region//菜单
                    
                    ArrayList tablemeun = mylink.Select("select * from Sysobjects where type='U' and name='HY_Menu' ");
                    if (tablemeun.Count > 0)
                    {
                        notcols.Clear();
                        notcols=GetNotCols("HY_Menu", mylink);
                                          
                        ArrayList authnotcols = new ArrayList();
                        authnotcols = GetNotCols("HY_Auth", mylink);
                       
                        if (chkdel.Checked)
                        {

                            foreach (TreeNode mynode in treeMenu.Nodes)
                            {
                                if (mynode.Checked)
                                {
                                    if (chkmemo.Checked)
                                    {
                                        txtscrip.AppendText(Environment.NewLine + "--删除菜单表HY_Menu中菜单[" + mynode.Tag.ToString() + "]" + mynode.Text);
                                    }
                                    txtscrip.AppendText(Environment.NewLine + "delete  HY_Menu where cmenuid='" + mynode.Tag.ToString() + "'");
                                    if (chkmemo.Checked)
                                    {
                                        txtscrip.AppendText(Environment.NewLine + "--删除权表HY_Auth中权限[" + mynode.Tag.ToString() + "]" + mynode.Text);
                                    }
                                    txtscrip.AppendText(Environment.NewLine + "delete  HY_Auth where cauthid ='" + mynode.Tag.ToString() + "' or  cparent='" + mynode.Tag.ToString() + "'");
                                   
                                }
                                DelChildMenuScrip(mynode, mylink, notcols);

                            }
                        }
                        foreach (TreeNode mynode in treeMenu.Nodes)
                        {
                            if (mynode.Checked)
                            {
                                ArrayList tmpdatas = mylink.Select("select * from HY_Menu where cmenuid='" + mynode.Tag.ToString() + "'");
                                if (chkmemo.Checked)
                                {
                                    txtscrip.AppendText(Environment.NewLine + "--插入菜单表HY_Menu中菜单[" + mynode.Tag.ToString() + "]" + mynode.Text);
                                }
                                #region //生成脚本
                                CreateScripM(tmpdatas, "HY_Menu", notcols);
                                #endregion
                                tmpdatas = mylink.Select("select * from HY_Auth where cauthid='" + mynode.Tag.ToString() + "' or  cparent='" + mynode.Tag.ToString() + "'");
                                if (chkmemo.Checked)
                                {
                                    txtscrip.AppendText(Environment.NewLine + "--插入菜单表HY_Auth 中权限[" + mynode.Tag.ToString() + "]" + mynode.Text);
                                }
                                #region //生成脚本
                                CreateScripM(tmpdatas, "HY_Auth", authnotcols);
                                #endregion
                               
                            }
                            GetChildMenuScrip(mynode, mylink, notcols, authnotcols);

                        }



                    }
                    else
                    {
                        txtscrip.AppendText("没有菜单表");
                        return;
                    }


                    #endregion
                    #region//单据
                     foreach( DataGridViewRow  myxrow in dataGridVou.Rows)
                    {
                        string gdischeck = (myxrow.Cells["ccheck"].Value == null ? "false" : myxrow.Cells["ccheck"].Value.ToString().ToLower());
                        if(gdischeck =="true")
                        {
                            string strvoucode = myxrow.Cells["cvoucode"].Value.ToString();
                            DelVouchScrip(strvoucode, mylink);
                            CreateVouchScrip(strvoucode, mylink);
                        }
                    }
                    #endregion
                }
                else
                {
                    MessageBox.Show("连接失败！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }
        private void DelVouchScrip(string strvoucode, DBReadOrWrite mylink)
        {
            /*
             * AA_Voucher	AA_VoucherButton	AA_VoucherDataAuth	AA_VoucherExpressionField	
AA_VoucherExpressionRelation	AA_VoucherFliter  AA_VoucherGroup	 AA_VoucherHistory
AA_VoucherItem  AA_VoucherItem_Body  AA_VoucherItem_List  AA_VoucherItem_User  AA_VoucherNumber	
AA_VoucherOtherSql   AA_VoucherPrint  AA_VoucherPrintItem   AA_VoucherPrintModule    AA_VoucherRunDll	
AA_VoucherRunDllParams	AA_VoucherRunSql	AA_VoucherTable  AA_VoucherTableField	AA_VoucherUrl	
             * */

            if (chkmemo.Checked)
            {
                txtscrip.AppendText(Environment.NewLine + "--删除表AA_VoucherUrl[单据Url]中数据");
            }
            txtscrip.AppendText(Environment.NewLine + "delete  AA_VoucherUrl where cvoucode='" + strvoucode + "'");
            if (chkmemo.Checked)
            {
                txtscrip.AppendText(Environment.NewLine + "--删除表AA_VoucherTableField[单据字段]中数据");
            }
            txtscrip.AppendText(Environment.NewLine + "delete  AA_VoucherTableField where cvoucode='" + strvoucode + "'");
            if (chkmemo.Checked)
            {
                txtscrip.AppendText(Environment.NewLine + "--删除表AA_VoucherTable[单据表]中数据");
            }
            txtscrip.AppendText(Environment.NewLine + "delete  AA_VoucherTable where cvoucode='" + strvoucode + "'");
            if (chkmemo.Checked)
            {
                txtscrip.AppendText(Environment.NewLine + "--删除表AA_VoucherRunSql[执行与判断]中数据");
            }
            txtscrip.AppendText(Environment.NewLine + "delete  AA_VoucherTableField where cvoucode='" + strvoucode + "'");
            if (chkmemo.Checked)
            {
                txtscrip.AppendText(Environment.NewLine + "--删除表AA_VoucherRunDllParams[无用]中数据");
            }
            txtscrip.AppendText(Environment.NewLine + "delete  AA_VoucherRunDllParams where cvoucode='" + strvoucode + "'");
            if (chkmemo.Checked)
            {
                txtscrip.AppendText(Environment.NewLine + "--删除表AA_VoucherRunDll[单据执行外部dll]中数据");
            }
            txtscrip.AppendText(Environment.NewLine + "delete  AA_VoucherRunDll where cvoucode='" + strvoucode + "'");
            if (chkmemo.Checked)
            {
                txtscrip.AppendText(Environment.NewLine + "--删除表AA_VoucherPrintModule[打印模板");
            }
            txtscrip.AppendText(Environment.NewLine + "delete  AA_VoucherPrintModule where cvoucode='" + strvoucode + "'");
            if (chkmemo.Checked)
            {
                txtscrip.AppendText(Environment.NewLine + "--删除表AA_VoucherPrintItem[打印项目]中数据");
            }
            txtscrip.AppendText(Environment.NewLine + "delete  AA_VoucherPrintItem where cvoucode='" + strvoucode + "'");
            if (chkmemo.Checked)
            {
                txtscrip.AppendText(Environment.NewLine + "--删除表AA_VoucherPrint[打印主表]中数据");
            }
            txtscrip.AppendText(Environment.NewLine + "delete  AA_VoucherPrint where cvoucode='" + strvoucode + "'");
            if (chkmemo.Checked)
            {
                txtscrip.AppendText(Environment.NewLine + "--删除表AA_VoucherOtherSql[其他sql脚本暂无用]中数据");
            }
            txtscrip.AppendText(Environment.NewLine + "delete  AA_VoucherOtherSql where cvoucode='" + strvoucode + "'");
            if (chkmemo.Checked)
            {
                txtscrip.AppendText(Environment.NewLine + "--删除表AA_VoucherNumber[单据编码方式表]中数据");
            }
            txtscrip.AppendText(Environment.NewLine + "delete  AA_VoucherNumber where cardnumber='" + strvoucode + "'");
            if (chkmemo.Checked)
            {
                txtscrip.AppendText(Environment.NewLine + "--删除表AA_VoucherItem [单据项目表]中数据");
            }
            txtscrip.AppendText(Environment.NewLine + "delete  AA_VoucherItem  where cvoucode='" + strvoucode + "'");
            if (chkmemo.Checked)
            {
                txtscrip.AppendText(Environment.NewLine + "--删除表AA_VoucherFliter[单据条件查询]中数据");
            }
            txtscrip.AppendText(Environment.NewLine + "delete  AA_VoucherFliter where cvoucode='" + strvoucode + "'");
            if (chkmemo.Checked)
            {
                txtscrip.AppendText(Environment.NewLine + "--删除表AA_VoucherExpressionRelation[公式指定表]中数据");
            }
            txtscrip.AppendText(Environment.NewLine + "delete  AA_VoucherExpressionRelation where cvoucode='" + strvoucode + "'");
            if (chkmemo.Checked)
            {
                txtscrip.AppendText(Environment.NewLine + "--删除表AA_VoucherExpressionField[公式可用字段表]中数据");
            }
            txtscrip.AppendText(Environment.NewLine + "delete  AA_VoucherExpressionField where cvoucode='" + strvoucode + "'");
            if (chkmemo.Checked)
            {
                txtscrip.AppendText(Environment.NewLine + "--删除表AA_VoucherDataAuth[数据权限表]中数据");
            }
            txtscrip.AppendText(Environment.NewLine + "delete  AA_VoucherDataAuth where cvoucode='" + strvoucode + "'");
            if (chkmemo.Checked)
            {
                txtscrip.AppendText(Environment.NewLine + "--删除表AA_VoucherButton[单据按钮表]中数据");
            }
            txtscrip.AppendText(Environment.NewLine + "delete  AA_VoucherButton where cvoucode='" + strvoucode + "'");
            if (chkmemo.Checked)
            {
                txtscrip.AppendText(Environment.NewLine + "--删除表AA_Voucher[单据主表]中数据");
            }
            txtscrip.AppendText(Environment.NewLine + "delete  AA_Voucher where cvoucode='" + strvoucode + "'");



        }
        private void CreateVouchScrip(string strvoucode, DBReadOrWrite mylink)
        {
            /*
            * AA_Voucher	AA_VoucherButton	AA_VoucherDataAuth	AA_VoucherExpressionField	
AA_VoucherExpressionRelation	AA_VoucherFliter  AA_VoucherGroup	 AA_VoucherHistory
AA_VoucherItem  AA_VoucherItem_Body  AA_VoucherItem_List  AA_VoucherItem_User  AA_VoucherNumber	 cardnumber
AA_VoucherOtherSql   AA_VoucherPrint  AA_VoucherPrintItem   AA_VoucherPrintModule    AA_VoucherRunDll	
AA_VoucherRunDllParams	AA_VoucherRunSql	AA_VoucherTable  AA_VoucherTableField	AA_VoucherUrl	
            * */


            string strtablename = "";
            string strql = "";
            string strfild = "cvoucode";
            strtablename = "AA_Voucher";
            strql="select * from "+ strtablename + " where "+ strfild + "='" + strvoucode + "'";
            CreateVouchOneScrip(strql, strtablename, mylink);
  
            strtablename = "AA_VoucherButton";
            strql = "select * from " + strtablename + " where " + strfild + "='" + strvoucode + "'";
            CreateVouchOneScrip(strql, strtablename, mylink);

            strtablename = "AA_VoucherDataAuth";
            strql = "select * from " + strtablename + " where " + strfild + "='" + strvoucode + "'";
            CreateVouchOneScrip(strql, strtablename, mylink);

            strtablename = "AA_VoucherExpressionField";
            strql = "select * from " + strtablename + " where " + strfild + "='" + strvoucode + "'";
            CreateVouchOneScrip(strql, strtablename, mylink);

            strtablename = "AA_VoucherExpressionRelation";
            strql = "select * from " + strtablename + " where " + strfild + "='" + strvoucode + "'";
            CreateVouchOneScrip(strql, strtablename, mylink);

            strtablename = "AA_VoucherFliter";
            strql = "select * from " + strtablename + " where " + strfild + "='" + strvoucode + "'";
            CreateVouchOneScrip(strql, strtablename, mylink);

            strtablename = "AA_VoucherGroup";
            strql = "select * from " + strtablename + " where " + strfild + "='" + strvoucode + "'";
            CreateVouchOneScrip(strql, strtablename, mylink);

            strtablename = "AA_VoucherItem";
            strql = "select * from " + strtablename + " where " + strfild + "='" + strvoucode + "'";
            CreateVouchOneScrip(strql, strtablename, mylink);

            strfild = "cardnumber";
            strtablename = "AA_VoucherNumber";
            strql = "select * from " + strtablename + " where " + strfild + "='" + strvoucode + "'";
            CreateVouchOneScrip(strql, strtablename, mylink);

            strfild = "cvoucode";
            strtablename = "AA_VoucherOtherSql";
            strql = "select * from " + strtablename + " where " + strfild + "='" + strvoucode + "'";
            CreateVouchOneScrip(strql, strtablename, mylink);

            strtablename = "AA_VoucherPrint";
            strql = "select * from " + strtablename + " where " + strfild + "='" + strvoucode + "'";
            CreateVouchOneScrip(strql, strtablename, mylink);

            strtablename = "AA_VoucherPrintItem";
            strql = "select * from " + strtablename + " where " + strfild + "='" + strvoucode + "'";
            CreateVouchOneScrip(strql, strtablename, mylink);

            strtablename = "AA_VoucherPrintModule";
            strql = "select * from " + strtablename + " where " + strfild + "='" + strvoucode + "'";
            CreateVouchOneScrip(strql, strtablename, mylink);

            strtablename = "AA_VoucherRunDll";
            strql = "select * from " + strtablename + " where " + strfild + "='" + strvoucode + "'";
            CreateVouchOneScrip(strql, strtablename, mylink);

            strtablename = "AA_VoucherRunDllParams";
            strql = "select * from " + strtablename + " where " + strfild + "='" + strvoucode + "'";
            CreateVouchOneScrip(strql, strtablename, mylink);

            strtablename = "AA_VoucherRunSql";
            strql = "select * from " + strtablename + " where " + strfild + "='" + strvoucode + "'";
            CreateVouchOneScrip(strql, strtablename, mylink);

            strtablename = "AA_VoucherTable";
            strql = "select * from " + strtablename + " where " + strfild + "='" + strvoucode + "'";
            CreateVouchOneScrip(strql, strtablename, mylink);

            strtablename = "AA_VoucherTableField";
            strql = "select * from " + strtablename + " where " + strfild + "='" + strvoucode + "'";
            CreateVouchOneScrip(strql, strtablename, mylink);

            strtablename = "AA_VoucherUrl";
            strql = "select * from " + strtablename + " where " + strfild + "='" + strvoucode + "'";
            CreateVouchOneScrip(strql, strtablename, mylink);

        }

        private void CreateVouchOneScrip(string strdtasql,string strtalbname, DBReadOrWrite mylink)
        {
            ArrayList notcols = new ArrayList();
            ArrayList tmpdatas = mylink.Select(strdtasql);
            if (chkmemo.Checked)
            {
                txtscrip.AppendText(Environment.NewLine + "--插入菜单表"+ strtalbname);
            }
            notcols = GetNotCols(strtalbname, mylink);
            CreateScripM(tmpdatas, strtalbname, notcols);

        }
        private void DelChildMenuScrip(TreeNode currNode, DBReadOrWrite mylink, ArrayList notcols)
        {
            TreeNodeCollection nodes = currNode.Nodes;
            if (nodes.Count > 0)
            {
                foreach (TreeNode tn in nodes)
                {
                    if (tn.Checked)
                    {
                        if (chkmemo.Checked)
                        {
                            txtscrip.AppendText(Environment.NewLine + "--删除菜单表HY_Menu中菜单[" + tn.Tag.ToString() + "]" + tn.Text);
                        }
                        txtscrip.AppendText(Environment.NewLine + "delete  HY_Menu where cmenuid='" + tn.Tag.ToString() + "'");
                        if (chkmemo.Checked)
                        {
                            txtscrip.AppendText(Environment.NewLine + "--删除权表HY_Auth中权限[" + tn.Tag.ToString() + "]" + tn.Text);
                        }
                        txtscrip.AppendText(Environment.NewLine + "delete  HY_Auth where cauthid ='" + tn.Tag.ToString() + "' or  cparent='" + tn.Tag.ToString() + "'");
                    }
                    DelChildMenuScrip(tn, mylink, notcols);
                }
            }

        }
        private void GetChildMenuScrip(TreeNode currNode,DBReadOrWrite mylink, ArrayList notcols, ArrayList authnotcols)
        {
            TreeNodeCollection nodes = currNode.Nodes;
            if (nodes.Count > 0)
            {
                foreach (TreeNode tn in nodes)
                {
                    if(tn.Checked)
                    {
                        ArrayList tmpdatas = mylink.Select("select * from HY_Menu where cmenuid='" + tn.Tag.ToString() + "'");
                        if (chkmemo.Checked)
                        {
                            txtscrip.AppendText(Environment.NewLine + "--插入菜单表HY_Menu中菜单[" + tn.Tag.ToString() + "]" + tn.Text);
                        }

                        CreateScripM(tmpdatas, "HY_Menu", notcols);


                        tmpdatas = mylink.Select("select * from HY_Auth where cauthid='" + tn.Tag.ToString() + "' or  cparent='" + tn.Tag.ToString() + "'");
                        if (chkmemo.Checked)
                        {
                            txtscrip.AppendText(Environment.NewLine + "--插入菜单表HY_Auth 中权限[" + tn.Tag.ToString() + "]" + tn.Text);
                        }
                        #region //生成脚本
                        CreateScripM(tmpdatas, "HY_Auth", authnotcols);
                        //Application.DoEvents();
                        #endregion 
                    }
                    GetChildMenuScrip(tn, mylink, notcols, authnotcols);
                }
            }

        }
        //创建脚本
        private void CreateScrip(BackgroundWorker bgWorker, ArrayList tmpdatas,string strtablename,ArrayList notcols,ref bool iscancel)
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
                            strvalue += (row[fieldkey] == null ? "null" : "'" + row[fieldkey].ToString().Replace("'", "''") + "'") + ",";
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
        private void CreateScripM( ArrayList tmpdatas, string strtablename, ArrayList notcols)
        {
            #region //生成脚本
            //txtscrip.AppendText(Environment.NewLine + "--插入数据到表" + strtablename);
            int i = 0;
            foreach (Hashtable row in tmpdatas)
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
                            strvalue += (row[fieldkey] == null ? "null" : "'" + row[fieldkey].ToString().Replace("'", "''") + "'") + ",";
                        }

                    }
                    string strisr = "insert into " + strtablename + "(" + stristfield.Substring(0, stristfield.Length - 1) + ")";
                    string stristvalue = "    values(" + strvalue.Substring(0, strvalue.Length - 1) + ")";

                    AppendScrip(Environment.NewLine + strisr);
                    AppendScrip(Environment.NewLine + stristvalue);

               
            }
            #endregion
        }
        private ArrayList GetNotCols(string strtablename, DBReadOrWrite mylink)
        {
            ArrayList notcols = new ArrayList();
            switch (cmbdatatype.Text)
            {
                case "SqlServer":
                    #region //获取不插入字段 Sqlserver
                    //获取表ID
                    ArrayList tableinfos = mylink.Select("select * from Sysobjects where type='U' and name='" + strtablename + "' ");
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
                        //return;
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


            return notcols;


           
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

        void LoadMenu(DBReadOrWrite dbrw) //加载菜单tree
        {
            try
            {
                setTreeView(dbrw, treeMenu, "");
            }
            catch(Exception ex)
            {
                ;
            }

        }
        private void setTreeView(DBReadOrWrite dbrw,  TreeView tr1, String parentId)
        {
            ArrayList rows = new ArrayList();
            rows = dbrw.Select("select cmenuid,cmenuname,cparent from HY_Menu where  isnull(cparent,'')='" + parentId.ToString() + "' order by iOrder,cmenuid ");
            if (rows.Count > 0)
            {
                String pId = "";
                foreach (Hashtable row in rows)
                {
                    TreeNode node = new TreeNode();
                    node.Text = row["cmenuname"].ToString();
                    node.Tag = row["cmenuid"].ToString();
                    pId = (row["cparent"]==null?"":row["cparent"].ToString());
                    if (pId == "")
                    {
                        //添加根节点
                        treeMenu.Nodes.Add(node);
                    }
                    else
                    {
                        //添加根节点之外的其他节点
                        RefreshChildNode(treeMenu, node, pId);
                    }
                    //查找以node为父节点的子节点
                    setTreeView(dbrw,tr1, node.Tag.ToString());

                }
            }
        }
        //处理根节点的子节点
        private void RefreshChildNode(TreeView tr1, TreeNode treeNode, String parentId)
        {
            foreach (TreeNode node in tr1.Nodes)
            {
                if (node.Tag.ToString() == parentId)
                {
                    node.Nodes.Add(treeNode);
                    return;
                }
                else if (node.Nodes.Count > 0)
                {
                    FindChildNode(node, treeNode, parentId);
                }
            }
        }

        //处理根节点的子节点的子节点
        private void FindChildNode(TreeNode tNode, TreeNode treeNode, String parentId)
        {
            foreach (TreeNode node in tNode.Nodes)
            {
                if (node.Tag.ToString() == parentId)
                {
                    node.Nodes.Add(treeNode);
                    return;
                }
                else if (node.Nodes.Count > 0)
                {
                    FindChildNode(node, treeNode, parentId);
                }

            }

        }


  

        public Boolean InitGrid(ref DataGridView TmpGrid)
        {
            try
            {
                TmpGrid.Rows.Clear();
                TmpGrid.Columns.Clear();
                DataGridViewCheckBoxColumn columnfst = new DataGridViewCheckBoxColumn();
                columnfst.HeaderText = "选择";
                columnfst.Name = "ccheck";
                columnfst.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                columnfst.Width = 38;
                //columnfst.Visible = false;
                TmpGrid.Columns.Insert(0, columnfst);
                DataGridViewColumn column = new DataGridViewTextBoxColumn();
                column.HeaderText = "单据编号";
                column.Name = "cvoucode";
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Width = 80;
                //column.Visible = false;
                TmpGrid.Columns.Insert(1, column);

                DataGridViewColumn column1 = new DataGridViewTextBoxColumn();
                column1.HeaderText = "单据名称";
                column1.Name = "cname";
                column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column1.Width = 85;
                //column.Visible = false;
                TmpGrid.Columns.Insert(2, column1);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public Boolean BindGrid(ref DataGridView TmpGrid, DBReadOrWrite dbrw)
        {
            try
            {
                String strsql = "select  cvoucode,cname from    AA_Voucher  order by cvoucode ";
                ArrayList rsDataTemp = dbrw.Select(strsql);
                TmpGrid.Rows.Clear();
                if (rsDataTemp.Count > 0)
                {
                    int i = 0;
                    foreach (Hashtable row in rsDataTemp)
                    {
                        TmpGrid.Rows.Add();
                        TmpGrid.Rows[i].Cells["ccheck"].Value = false;
                        TmpGrid.Rows[i].Cells["cvoucode"].Value = row["cvoucode"];
                        TmpGrid.Rows[i].Cells["cname"].Value = row["cname"];
                        i++;
                   }
                }
                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return false;
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
            BackgroundWorker bgWorker = (BackgroundWorker)sender;
            bgWorker.CancelAsync();
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bgWorker = (BackgroundWorker)sender;
            Hashtable myrefdata = (Hashtable) e.Argument;
            ArrayList tmpdatas = (ArrayList)myrefdata["datas"];
            string strtablename = myrefdata["tablename"].ToString();
            ArrayList notcols = (ArrayList)myrefdata["notcols"];
            //myrefdata["datas"] = tmpdatas;
            //myrefdata["tablename"] = strtablename;
            //myrefdata["notcols"] = notcols;
            bool iscancel = false;

            CreateScrip(bgWorker,tmpdatas, strtablename, notcols,ref iscancel);
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
                //MessageBox.Show(e.Error.ToString());
                return;
            }
            if (!e.Cancelled)
            {
                //MessageBox.Show("处理完毕!");
            }
            else
            {
                //MessageBox.Show("处理终止!");
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

        private void cmbdatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkldef.Checked)
            {
                string strtype = cmbdatatype.Text;
                string strlk = "";
                switch (strtype)
                {
                    case "SqlServer":
                        strlk = "Server=" + txtserver.Text + (txtport.Text == "" ? "" : "," + txtport.Text) + ";Database=" + cmbdatabase.Text + ";User ID=" + txtuser.Text + ";Password='" + txtpwd.Text + "'"; //sqlserver
                        break;
                    case "MySql":
                        strlk = "server=" + txtserver.Text + "; port=" + txtport.Text + "; user id=" + txtuser.Text + "; password=" + txtpwd.Text + "; database=" + cmbdatabase.Text + ";"; //mysql  // database="+cmbdatabase.Text +";
                        break;
                    case "Oracle":
                        strlk = "Provider=OraOLEDB.Oracle.1;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = " + txtserver.Text + ")(PORT = " + txtport.Text + "))) (CONNECT_DATA =(SERVICE_NAME=" + cmbdatabase.Text + ")));User Id=" + txtuser.Text + ";Password=" + txtpwd.Text + ";";
                        break;
                }
                DBReadOrWrite mylink = new DBReadOrWrite();
                mylink.dbType = strtype;
                mylink.connstr = strlk;
                mylink.BeginConn();

                if (mylink.dbState == true)
                {
                    mylink.EndConn();
                    LoadMenu(mylink);
                    InitGrid(ref dataGridVou);
                    BindGrid(ref dataGridVou, mylink);
                }
                else
                {
                    MessageBox.Show("连接失败！");
                }
            }
        }


        private void setChildNodeCheckedState(TreeNode currNode, bool state)
        {
            TreeNodeCollection nodes = currNode.Nodes;
            if (nodes.Count > 0)
            {
                foreach (TreeNode tn in nodes)
                {
                    tn.Checked = state;
                    setChildNodeCheckedState(tn, state);
                }
            }

        }

        private void treeMenu_AfterCheck(object sender, TreeViewEventArgs e)
        {
            
            TreeNode mynode= e.Node;
            setChildNodeCheckedState(mynode, mynode.Checked);
        }

        private void chkldef_CheckedChanged(object sender, EventArgs e)
        {
            if (chkldef.Checked && cmbdatabase.Text !="")
            {

                if (chkldef.Checked)
                {
                    string strtype = cmbdatatype.Text;
                    string strlk = "";
                    switch (strtype)
                    {
                        case "SqlServer":
                            strlk = "Server=" + txtserver.Text + (txtport.Text == "" ? "" : "," + txtport.Text) + ";Database=" + cmbdatabase.Text + ";User ID=" + txtuser.Text + ";Password='" + txtpwd.Text + "'"; //sqlserver
                            break;
                        case "MySql":
                            strlk = "server=" + txtserver.Text + "; port=" + txtport.Text + "; user id=" + txtuser.Text + "; password=" + txtpwd.Text + "; database=" + cmbdatabase.Text + ";"; //mysql  // database="+cmbdatabase.Text +";
                            break;
                        case "Oracle":
                            strlk = "Provider=OraOLEDB.Oracle.1;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = " + txtserver.Text + ")(PORT = " + txtport.Text + "))) (CONNECT_DATA =(SERVICE_NAME=" + cmbdatabase.Text + ")));User Id=" + txtuser.Text + ";Password=" + txtpwd.Text + ";";
                            break;
                    }
                    DBReadOrWrite mylink = new DBReadOrWrite();
                    mylink.dbType = strtype;
                    mylink.connstr = strlk;
                    mylink.BeginConn();

                    if (mylink.dbState == true)
                    {
                        mylink.EndConn();
                        LoadMenu(mylink);
                        InitGrid(ref dataGridVou);
                        BindGrid(ref dataGridVou, mylink);
                    }
                    else
                    {
                        MessageBox.Show("连接失败！");
                    }
                }
            }
        }
    }
}
