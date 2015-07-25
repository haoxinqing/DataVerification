using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace FrmTest.Comm
{
    public class UtilClass
    {
        #region 字段
        ////是否找到指定控件
        //private static bool flag = false;
        //控件集合
        private static List<Control> controls = new List<Control>();
        //容器集合
        private static List<Control> containers = new List<Control>();

        //数据验证
        static DataCheck check = new DataCheck();
        #endregion

        public static void Test(object obj, string name)
        {
            //初始化控件库
            InitControls(obj);

            //查询
            Control ctrl = Search(name);

            if (ctrl == null)
            {
                MessageBox.Show("未找到指定控件！");
                return;
            }

            //添加事件处理
            BindEventHandler(ctrl);
        }

        #region 已删除

        //public static void Test(object obj,string name)
        //{
        //    //对Form，遍历其Controls
        //    if (obj is Form)
        //    {
        //        Form frm = obj as Form;

        //        flag = false;   //是否查找到名为name的控件

        //        foreach (Control item in frm.Controls)
        //        {
        //            if (item.Name.Equals(name))
        //            {
        //                flag = true;
        //                BindEventHandler(item);
        //            }

        //            //将容器控件或者Tab控件添加到容器集合
        //            if (item is Panel || item is GroupBox || item is TabControl)
        //            {
        //                containers.Add(item);
        //            }
        //        }
        //    }
        //    else if(obj is Control)
        //    {
        //        Control ctrl = obj as Control;

        //        if (ctrl.Name.Equals(name))
        //        {
        //            flag = true;
        //            BindEventHandler(ctrl);
        //        }
        //        else if (ctrl is TabControl)
        //        {
        //            TabControl tab = ctrl as TabControl;
        //            containers.Remove(tab);

        //            foreach (TabPage item in tab.TabPages)
        //            {
        //                containers.Add(item);
        //            }
        //        }
        //        else if (ctrl is TabPage)
        //        {
        //            TabPage page = ctrl as TabPage;
        //            containers.Remove(page);

        //            foreach (Control item in page.Controls)
        //            {
        //                Test(item, name);
        //            }
        //        }
        //        else if (ctrl is Panel)
        //        {
        //            Panel pnl = ctrl as Panel;
        //            containers.Remove(ctrl);

        //            foreach (Control item in pnl.Controls)
        //            {
        //                Test(item, name);
        //            }
        //        }
        //        else if (ctrl is GroupBox)
        //        {
        //            GroupBox group = ctrl as GroupBox;
        //            containers.Remove(ctrl);

        //            foreach (Control item in group.Controls)
        //            {
        //                Test(item, name);
        //            }
        //        }
        //        else if (false == flag)
        //        {
        //            return;
        //        }
        //    }

        //    //如果一级Controls中没有找到指定控件，遍历其子节点
        //    if (false == flag)
        //    {
        //        if (containers.Count > 0)
        //        {
        //            Test(containers[0], name);
        //        }
        //        else
        //        {
        //            return;
        //        }
        //    }
        //}

        #endregion

        //为指定源，绑定事件
        public static void BindEventHandler(Control source)
        {
            try
            {
                switch ((source).GetType().Name)
                {
                    case "TextBox":
                        TextBox txt = source as TextBox;
                        txt.KeyPress +=new KeyPressEventHandler(check.integerNumberKeyPress);
                        break;
                    case "RichTextBox":
                        RichTextBox rtxt = source as RichTextBox;
                        rtxt.KeyPress += new KeyPressEventHandler(check.integerNumberKeyPress);
                        break;
                    //case "DataGridView":
                    //    DataGridView grid = source as DataGridView;
                    //    grid.Validating += new System.ComponentModel.CancelEventHandler(DataGridView_Validating);
                    //    break;
                    case "ComboBox":
                        ComboBox cmb = source as ComboBox;
                        cmb.Validating += new System.ComponentModel.CancelEventHandler(ComboBox_Validating);
                        break;
                    default:

                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        //初始化控件库
        private static void InitControls(object obj)
        {
            if (obj is Form)
            {
                Form frm = obj as Form;

                AddControls(frm);
            }
        }

        //迭代添加控件
        private static void AddControls(Control ctrl)
        {
            if (ctrl is Form)
            {
                Form frm = ctrl as Form;
                foreach (Control item in frm.Controls)
                {
                    AddControls(item);
                }
            }
            else if (ctrl is TabControl)
            {
                TabControl tab = ctrl as TabControl;
                foreach (TabPage item in tab.TabPages)
                {
                    AddControls(item);
                }
            }
            else if (ctrl is TabPage)
            {
                TabPage page = ctrl as TabPage;
                foreach (Control item in page.Controls)
                {
                    AddControls(item);
                }
            }
            else if (ctrl is Panel)
            {
                Panel pnl = ctrl as Panel;
                foreach (Control item in pnl.Controls)
                {
                    AddControls(item);
                }
            }
            else if (ctrl is GroupBox)
            {
                GroupBox group = ctrl as GroupBox;
                foreach (Control item in group.Controls)
                {
                    AddControls(item);
                }
            }
            else
            {
                controls.Add(ctrl);
            }
        }

        //从控件库里面查找名为name的控件，没找到返回null
        private static Control Search(string name)
        {
            bool flag = false;

            Control ctrl = null;
            if (false == flag && controls.Count > 0)
            {
                foreach (Control item in controls)
                {
                    if (item.Name.Equals(name))
                    {
                        flag = true;
                        ctrl = item;
                        break;
                    }
                }
            }

            return ctrl;
        }

        #region 事件处理

        public static void IsNumber(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;

            string str = txt.Text;
            if (!IsNumber(str))
            {
                MessageBox.Show("请输入数字！");
            }
        }

        public static void TextBox_Validating(object sender, EventArgs e)
        {
            string str = string.Empty;
            TextBox txt = sender as TextBox;
            str = txt.Text;

            if (!IsNumber(str))
            {
                MessageBox.Show("请输入数字！");
                txt.Clear();
            }
        }

        public static void DataGridView_Validating(object sender, EventArgs e)
        {
            string str = string.Empty;
            DataGridView grid = sender as DataGridView;
            str = grid.Rows[0].Cells[0].Value.ToString();

            if (!IsNumber(str))
            {
                MessageBox.Show("请输入数字！");
                
            }
        }

        public static void ComboBox_Validating(object sender, EventArgs e)
        {
            string str = string.Empty;
            ComboBox cmb = sender as ComboBox;
            str = cmb.Text;

            if (!IsNumber(str))
            {
                MessageBox.Show("请输入数字！");
                
            }
        }

        public static void RichTextBox_Validating(object sender, EventArgs e)
        {
            string str = string.Empty;
            RichTextBox rtxt = sender as RichTextBox;
            str = rtxt.Text;

            if (!IsNumber(str))
            {
                MessageBox.Show("请输入数字！");
                rtxt.Clear();
            }
        }

        #endregion

        #region 判断

        //判断是否为数字
        private static bool IsNumber(object obj)
        {
            Regex reg = new Regex("^[0-9]*$");

            string tmp = obj.ToString();

            return reg.IsMatch(tmp);
        }



        #endregion
    }
}
