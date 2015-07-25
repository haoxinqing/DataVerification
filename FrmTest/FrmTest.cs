using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using UtilForDataCheck;
using System.IO;

namespace FrmTest
{
    public partial class FrmTest : Form
    {
        public FrmTest()
        {
            InitializeComponent();
        }

        private void FrmTest_Load(object sender, EventArgs e)
        {
            DataCheck.XmlPath = Directory.GetCurrentDirectory() + @"\..\..\..\UtilForDataCheck\Parameters.xml";
            DataCheck.Check(this);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //Regex reg = new Regex("^-?[1-3]?[0-6]?[0-9]?.?[0-9]*$");
            //string str = txtContent1.Text;


            //MessageBox.Show("是否是角度：\n" + reg.IsMatch(str).ToString());

            if (!DataCheck.DataValidate(btnTest))
            {
                //MessageBox.Show("您的数据没有通过验证，请检查所有必填项都已输入数据！");
                return;
            }
            else
            {
                //MessageBox.Show("您已通过数据验证！");
            }
        }

        private void 删除GridItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("确定要删除选中行", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                foreach (DataGridViewRow r in grid1.SelectedRows)
                {
                    grid1.Rows.Remove(r);
                }
            }
        }


    }
}
