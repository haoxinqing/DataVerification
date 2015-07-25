using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Collections;

namespace FrmTest.Comm
{
    /// <summary>
    /// 控件类
    /// </summary>
    public class ControlClass
    {
        //命名空间
        public string _NameSpace { get; set; }
        //类
        public string _Class { get; set; }
        //控件名
        public string _Control { get; set; }
        //控件类型
        public string _ControlType { get; set; }

        public void Write(string _url)
        {
            if (!File.Exists(_url))
            {
                MessageBox.Show("文件不存在！");
                return;
            }

            XmlHelper xmlhelper = new XmlHelper();

            //TODO:判断当前命名空间，类是否已存在，

            //添加属性
            Hashtable hashAttr = new Hashtable();
            hashAttr.Add("namespace", _NameSpace);
            hashAttr.Add("class", _Class);
            hashAttr.Add("controltype", _ControlType);

            xmlhelper.InsertNode(_url, _Control, true, "//parameters//control", hashAttr,null);
        }
    }
}
