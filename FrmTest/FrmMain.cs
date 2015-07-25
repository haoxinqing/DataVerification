using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UtilForDataCheck;
using System.IO;

namespace FrmTest
{
    public partial class FrmMain : Form
    {
        private static FrmMain mainFormInstance;
        public static FrmMain MainFormInstance
        {
            get
            {
                if (mainFormInstance == null)
                {
                    mainFormInstance = new FrmMain();
                }

                return mainFormInstance;
            }
        }

        private FrmMain()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            //DataCheck.RealNumberCheck(txtContent1);
            //DataCheck.IntegerCheck(txtContent2);
            //DataCheck.AngleCheck(txtContent3);

            txtContent1.KeyPress -= new KeyPressEventHandler(DataCheck.RealNumberWithSciKeyPress);
            txtContent1.KeyPress += new KeyPressEventHandler(DataCheck.RealNumberWithSciKeyPress);
        }

        private void Form_Activated(object sender, EventArgs e)
        {
            //DataCheck.XmlPath = Directory.GetCurrentDirectory() + @"\..\..\..\UtilForDataCheck\Parameters.xml";
            //DataCheck.Check(this);
        }

        private void btnRedirect_Click(object sender, EventArgs e)
        {
            FrmTest frm = new FrmTest();
            frm.Show();
            this.Hide();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //if (!DataCheck.DataValidate(btnTest))
            //{
            //    //MessageBox.Show("您的数据没有通过验证，请检查所有必填项都已输入数据！");
            //    return;
            //}
            //else
            //{
            //    //MessageBox.Show("您已通过数据验证！");
            //}

            //if (DataCheck.IsSciExpressLegal(txtContent1.Text))
            //{
            //    MessageBox.Show("有效的数值");
            //}
            //else
            //{
            //    MessageBox.Show("无效的数值");
            //}
        }

        private void btnPickUp_Click(object sender, EventArgs e)
        {
            
        }
    }
}
