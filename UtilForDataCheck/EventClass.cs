using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace UtilForDataCheck
{
    public class EventClass
    {
        private string EventCase = string.Empty;
        public EventClass()
        {
            backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            FrmSetXml.MainInstance.ReLoadTreeView();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            switch (EventCase)
            {
                
                default:
                    break;
            }
        }
        private System.ComponentModel.BackgroundWorker backgroundWorker1 = new System.ComponentModel.BackgroundWorker();

        public string stpFilePath = string.Empty;
        public string ugFilePath = string.Empty;
        private object SENDER;
        private TreeNodeMouseClickEventArgs TNMCEA;
        private TreeNode TREENODE;
        private Pin PIN;

        public bool DoubleClick_OK = false;
        public bool Result_OK = false;

        public void NodeClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //if (e.Node is BaseNode)
                //{
                //    this.TNMCEA = e;
                //    e.Node.ContextMenuStrip = GetRightMenu((e.Node as BaseNode).RightMenu);
                //}
            }
            else
            {
                //if (e.Node is BaseNode)
                //{
                //    string MName = (e.Node as BaseNode).Name + "_" + (e.Node as BaseNode).Click;
                //    MethodInfo methodInfo = this.GetType().GetMethods().Where(n => n.Name.Equals(MName)).FirstOrDefault();
                //    if (methodInfo != null)
                //    {
                //        methodInfo.Invoke(this, new object[] { e });
                //    }
                //    else
                //    {
                //        //MessageBox.Show("没有设置匹配的事件。");
                //    }
                //}
            }


        }

        public void NodeDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                string MName = e.Node.Name + "_NodeDoubleClick";
                MethodInfo methodInfo = this.GetType().GetMethods().Where(n => n.Name.Equals(MName)).FirstOrDefault();
                if (methodInfo != null)
                {
                    SENDER = sender;
                    TNMCEA = e;
                    TREENODE = e.Node;
                    if (e.Node.Tag is Pin)
                    {
                        PIN = e.Node.Tag as Pin;
                        NdataBaseHelper.DBFullName = PIN.DataBasePath;
                    }
                    methodInfo.Invoke(this, new object[] { });//调用MName名字的函数
                    DoubleClick_OK = true;
                }
                else
                {
                    DoubleClick_OK = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
