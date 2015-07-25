using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Xml;
using System.Collections;
using System.IO;

namespace UtilForDataCheck
{
    /// <summary>
    /// 控件基本操作，以及Xml文件操作
    /// </summary>
    internal class UtilClass
    {
        #region 控件操作
        #region 字段
        //控件集合
        private static List<Control> controls = new List<Control>();
        //容器集合
        private static List<Control> containers = new List<Control>();

        ////数据验证
        //static DataCheck check = new DataCheck();

        //Xml辅助类
        static XmlHelper xmlHelper = new XmlHelper();
        #endregion

        //初始化控件库
        public static List<Control> InitControls(object obj)
        {
            containers.Clear();
            controls.Clear();

            if (obj is Form)
            {
                Form frm = obj as Form;

                AddControls(frm);
            }

            return controls;
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
        public static Control Search(string name)
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

        /// <summary>
        /// 获取控件父窗体
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        public static Control SearchOwner(Control ctrl)
        {
            //获取顶级父窗体
            if (ctrl is Form && ctrl.Parent == null)
            {
                Form frm = ctrl as Form;

                return frm;
            }
            else
            {
                return SearchOwner(ctrl.Parent);
            }
        }

        /// <summary>
        /// 向上找寻TabPage（最邻近）
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        public static Control SearchOwnerTabPage(Control ctrl)
        {
            if (ctrl is TabPage)
            {
                return ctrl as TabPage;
            }
            else
            {
                return SearchOwnerTabPage(ctrl.Parent);
            }
        }

        #endregion

        #region 控件Xml操作



        /// <summary>
        /// 从Xml文件中获取控件信息
        /// </summary>
        /// <param name="_xmlpath">Xml路径</param>
        /// <param name="_namespace">命名空间名</param>
        /// <param name="_class">类名</param>
        /// <param name="_control">控件名</param>
        /// <returns></returns>
        public static BaseCtrl GetControlFromXml(string _xmlpath, string _namespace, string _class, string _control)
        {
            //TODO:待修改（获取控件时，判断类型是数据控件，还是按钮）
            StringBuilder builder = new StringBuilder();
            builder.Append("//parameters//controls");
            builder.Append("//" + _namespace);
            builder.Append("//" + _class);
            builder.Append("//" + _control);

            XmlNode node = xmlHelper.GetXmlNodeByXpath(_xmlpath, builder.ToString());

            BaseCtrl ctrl = GetControl(_xmlpath, node);

            return ctrl;
        }

        /// <summary>
        /// 获取与给定控件的信息
        /// </summary>
        /// <param name="_xmlpath"></param>
        /// <param name="_ctrl"></param>
        /// <returns></returns>
        public static BaseCtrl GetControlFromXml(string _xmlpath, Control _ctrl)
        {
            //获取控件父窗体
            Control Pctrl = SearchOwner(_ctrl);

            string _namespace = Pctrl.GetType().Namespace;
            string _class = Pctrl.GetType().Name;
            string _control = _ctrl.Name;

            return GetControlFromXml(_xmlpath, _namespace, _class, _control);
        }

        #region 不用
        ///// <summary>
        ///// 获取Xml中所有控件
        ///// </summary>
        ///// <param name="_xmlpath"></param>
        ///// <returns></returns>
        //public static List<BaseCtrl> GetAllFromXml(string _xmlpath)
        //{
        //    List<BaseCtrl> ctrls = new List<BaseCtrl>();

        //    BaseCtrl c;

        //    StringBuilder builder = new StringBuilder();
        //    builder.Append("//parameters//controls");

        //    XmlNode rootCtrl = xmlHelper.GetXmlNodeByXpath(_xmlpath, builder.ToString());

        //    if (rootCtrl.HasChildNodes)
        //    {
        //        //遍历所有命名空间
        //        foreach (XmlNode root1 in rootCtrl.ChildNodes)
        //        {
        //            if (root1.HasChildNodes)
        //            {
        //                //遍历所有类
        //                foreach (XmlNode root2 in root1.ChildNodes)
        //                {
        //                    if (root2.HasChildNodes)
        //                    {
        //                        //遍历所有控件
        //                        foreach (XmlNode item in root2.ChildNodes)
        //                        {
        //                            //获取控件信息
        //                            c = GetControl(_xmlpath, item);

        //                            ctrls.Add(c);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return ctrls;
        //}

        ///// <summary>
        ///// 获取Xml文件中指定命名空间下的所有控件信息
        ///// </summary>
        ///// <param name="_xmlpath">Xml文件路径</param>
        ///// <param name="_namespace">命名空间名</param>
        ///// <returns></returns>
        //public static List<BaseCtrl> GetAllFromXml(string _xmlpath, string _namespace)
        //{
        //    //TODO:待修改（获取控件时，判断类型是数据控件，还是按钮）
        //    List<BaseCtrl> ctrls = new List<BaseCtrl>();

        //    BaseCtrl c;

        //    StringBuilder builder = new StringBuilder();
        //    builder.Append("//parameters//controls");

        //    XmlNode rootCtrl = xmlHelper.GetXmlNodeByXpath(_xmlpath, builder.ToString());

        //    if (rootCtrl.HasChildNodes)
        //    {
        //        builder.Append("//");
        //        builder.Append(_namespace);

        //        //获取指定命名空间节点
        //        XmlNode root1 = xmlHelper.GetXmlNodeByXpath(_xmlpath, builder.ToString());
        //        if (root1 != null)
        //        {
        //            if (root1.HasChildNodes)
        //            {
        //                //遍历所有类
        //                foreach (XmlNode root2 in root1.ChildNodes)
        //                {
        //                    if (root2.HasChildNodes)
        //                    {
        //                        //遍历所有控件
        //                        foreach (XmlNode item in root2.ChildNodes)
        //                        {
        //                            //获取控件信息
        //                            c = GetControl(_xmlpath, item);

        //                            ctrls.Add(c);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return ctrls;
        //}
        #endregion

        /// <summary>
        /// 获取Xml文件中指定命名空间的指定类名下的所有控件信息
        /// </summary>
        /// <param name="_xmlpath">Xml文件路径</param>
        /// <param name="_namespace">命名空间名</param>
        /// <param name="_class">类名</param>
        /// <param name="frmCtrls">当前窗体中所有控件</param>
        /// <returns></returns>
        public static List<BaseCtrl> GetAllFromXml(string _xmlpath, string _namespace, string _class, List<Control> frmCtrls = null)
        {
            //TODO:待修改（获取控件时，判断类型是数据控件，还是按钮）
            List<BaseCtrl> ctrls = new List<BaseCtrl>();

            BaseCtrl c;

            StringBuilder builder = new StringBuilder();
            builder.Append("//parameters//controls");

            XmlNode rootCtrl = xmlHelper.GetXmlNodeByXpath(_xmlpath, builder.ToString());

            if (rootCtrl.HasChildNodes)
            {
                builder.Append("//");
                builder.Append(_namespace);

                //获取指定命名空间节点
                XmlNode root1 = xmlHelper.GetXmlNodeByXpath(_xmlpath, builder.ToString());
                if (root1 != null)
                {
                    if (root1.HasChildNodes)
                    {
                        builder.Append("//");
                        builder.Append(_class);

                        //获取指定类节点
                        XmlNode root2 = xmlHelper.GetXmlNodeByXpath(_xmlpath, builder.ToString());
                        if (root2 != null)
                        {
                            if (root2.HasChildNodes)
                            {
                                //遍历所有控件
                                foreach (XmlNode item in root2.ChildNodes)
                                {
                                    if (frmCtrls != null && frmCtrls.Count > 0)
                                    {
                                        Control frmCtrl = frmCtrls.Where(n => n.Name.Equals(item.Name)).FirstOrDefault();

                                        if (frmCtrl != null && frmCtrl.Name.Equals(item.Name))
                                        {
                                            //获取控件信息
                                            c = GetControl(_xmlpath, item, frmCtrl);
                                            ctrls.Add(c);
                                        }
                                    }
                                    else if (xmlHelper.IsAttrExist(item, "controltype"))
                                    {
                                        //获取控件信息
                                        c = GetControl(_xmlpath, item);

                                        ctrls.Add(c);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return ctrls;
        }

        #region Xml读写数据模型（暂时不用）
        ///// <summary>
        ///// 将控件信息写入Xml
        ///// </summary>
        ///// <param name="_xmlpath"></param>
        ///// <param name="ctrl"></param>
        ///// <returns></returns>
        //public static bool WriteToXml(string _xmlpath, BaseCtrl ctrl)
        //{
        //    if (ctrl == null)
        //        return false;

        //    //是否写入成功
        //    bool flag = false;

        //    if (ctrl is ControlClass)
        //    {
        //        ControlClass c = ctrl as ControlClass;

        //        //类型名称
        //        string typename = string.Empty;
        //        if (!string.IsNullOrEmpty(c._TypeName))
        //        {
        //            typename = c._TypeName;
        //        }
        //        else
        //        {
        //            typename = GetNewTypeName(_xmlpath);
        //        }

        //        //属性信息
        //        Hashtable attr = new Hashtable();
        //        attr.Add("namespace", c._NameSpace);
        //        attr.Add("class", c._Class);
        //        attr.Add("controltype", c._ControlType);
        //        attr.Add("datatype", typename);
        //        if (!string.IsNullOrEmpty(c._EventType))
        //        {
        //            attr.Add("eventtype", c._EventType);
        //        }
        //        if (c._NotNullIndex != null && c._NotNullIndex.Count > 0)
        //        {
        //            attr.Add("notnullindex", string.Join(",", c._NotNullIndex));
        //        }

        //        //写入类型信息
        //        WriteAttr(_xmlpath, typename, c._Attr);

        //        //判断父节点是否存在
        //        if (IsNodeExist(_xmlpath, c))
        //        {
        //            StringBuilder builder = new StringBuilder();
        //            builder.Append("//parameters");
        //            builder.Append("//" + c._NameSpace);
        //            builder.Append("//" + c._Class);

        //            builder.Append("//" + c._Name);

        //            flag = xmlHelper.UpdateNode(_xmlpath, builder.ToString(), attr);
        //        }
        //        else
        //        {
        //            StringBuilder builder = new StringBuilder();
        //            builder.Append("//parameters");
        //            builder.Append("//" + ctrl._NameSpace);

        //            //判断命名空间节点是否存在，不存在则新建
        //            if (!xmlHelper.IsNodeExist(_xmlpath, builder.ToString()))
        //            {
        //                xmlHelper.InsertNode(_xmlpath, c._NameSpace, false, "//parameters", null, null);
        //            }

        //            //判断类节点是否存在，不存在则新建
        //            builder.Append("//" + c._Class);
        //            if (!xmlHelper.IsNodeExist(_xmlpath, builder.ToString()))
        //            {
        //                Hashtable cattr = new Hashtable();
        //                cattr.Add("namespace", c._NameSpace);
        //                xmlHelper.InsertNode(_xmlpath, c._Class, true, "//parameters//" + c._NameSpace, cattr, null);
        //            }

        //            //添加控件节点
        //            flag = xmlHelper.InsertNode(_xmlpath, c._Name, true, builder.ToString(), attr, null);
        //        }
        //    }
        //    else if (ctrl is ButtonClass)
        //    {
        //        ButtonClass btn = ctrl as ButtonClass;

        //        string tipname = string.Empty;
        //        if (btn._Tips != null)
        //        {
        //            if (!string.IsNullOrEmpty(btn._TipName))
        //            {
        //                tipname = btn._TipName;
        //            }
        //            else
        //            {
        //                //获取新提示信息名
        //                tipname = GetNewTipName(_xmlpath);
        //            }

        //            //写入Tip信息
        //            WriteTip(_xmlpath, tipname, btn._Tips);
        //        }

        //        List<string> cnames = new List<string>();
        //        foreach (ControlClass item in btn._Controls)
        //        {
        //            if (item == null)
        //                continue;

        //            cnames.Add(item._Name);
        //        }
        //        string ctrlName = string.Join(",", cnames);

        //        //属性表
        //        Hashtable battr = new Hashtable();
        //        battr.Add("namespace", btn._NameSpace);
        //        battr.Add("class", btn._Class);
        //        battr.Add("controltype", btn._ControlType);
        //        if (btn._Controls.Count > 0)
        //        {
        //            battr.Add("controls", ctrlName);
        //        }
        //        if (!string.IsNullOrEmpty(tipname))
        //        {
        //            battr.Add("tip", tipname);
        //        }

        //        if (IsNodeExist(_xmlpath, btn))
        //        {
        //            StringBuilder builder = new StringBuilder();
        //            builder.Append("//parameters");
        //            builder.Append("//" + btn._NameSpace);
        //            builder.Append("//" + btn._Class);

        //            builder.Append("//" + btn._Name);

        //            flag = xmlHelper.UpdateNode(_xmlpath, builder.ToString(), battr);
        //        }
        //        else
        //        {
        //            StringBuilder builder = new StringBuilder();
        //            builder.Append("//parameters");
        //            builder.Append("//" + btn._NameSpace);

        //            //判断命名空间节点是否存在，不存在则新建
        //            if (!xmlHelper.IsNodeExist(_xmlpath, builder.ToString()))
        //            {
        //                xmlHelper.InsertNode(_xmlpath, btn._NameSpace, false, "//parameters", null, null);
        //            }

        //            //判断类节点是否存在，不存在则新建
        //            builder.Append("//" + btn._Class);
        //            if (!xmlHelper.IsNodeExist(_xmlpath, builder.ToString()))
        //            {
        //                Hashtable cattr = new Hashtable();
        //                cattr.Add("namespace", btn._NameSpace);
        //                xmlHelper.InsertNode(_xmlpath, btn._Class, true, "//parameters//" + btn._NameSpace, cattr, null);
        //            }

        //            //添加节点
        //            flag = xmlHelper.InsertNode(_xmlpath, btn._Name, true, builder.ToString(), battr, null);
        //        }
        //    }

        //    return flag;
        //}

        ///// <summary>
        ///// 生成数据验证Xml文件
        ///// </summary>
        ///// <returns></returns>
        //public static string GenerateXml()
        //{
        //    SaveFileDialog dlg = new SaveFileDialog();
        //    dlg.Filter = "XML文件|*.xml";


        //    string filepath = string.Empty;
        //    if (DialogResult.OK == dlg.ShowDialog())
        //    {
        //        filepath = dlg.FileName;

        //        //Xml文件创建成功
        //        if (xmlHelper.CreateXmlDocument(filepath, "parameters", "UTF-8"))
        //        {
        //            //添加默认节点controls和types
        //            xmlHelper.InsertNode(filepath, "controls", false, "//parameters", null, null);
        //            xmlHelper.InsertNode(filepath, "types", false, "//parameters", null, null);
        //            xmlHelper.InsertNode(filepath, "tipmessages", false, "//parameters", null, null);
        //        }
        //        else
        //        {
        //            filepath = null;
        //        }
        //    }
        //    else
        //    {
        //        filepath = null;
        //    }


        //    return filepath;
        //}
        #endregion

        #region 私有方法

        /// <summary>
        /// 判断控件节点是否存在
        /// </summary>
        /// <param name="_xmlpath">Xml路径</param>
        /// <param name="ctrl">控件</param>
        /// <returns></returns>
        private static bool IsNodeExist(string _xmlpath, BaseCtrl ctrl)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("//parameters");
            builder.Append("//" + ctrl._NameSpace);
            builder.Append("//" + ctrl._Class);
            builder.Append("//" + ctrl._Name);

            return xmlHelper.IsNodeExist(_xmlpath, builder.ToString());
        }

        /// <summary>
        /// 从Node获取控件实体
        /// </summary>
        /// <param name="_xmlpath">Xml文件路径</param>
        /// <param name="node">节点信息</param>
        /// <param name="frmCtrl">对应窗体中的控件</param>
        /// <returns></returns>
        private static BaseCtrl GetControl(string _xmlpath, XmlNode node, Control frmCtrl = null)
        {
            BaseCtrl ctrl;

            //查找到节点
            if (node != null)
            {
                if (xmlHelper.IsAttrExist(node, "controltype"))
                {

                    #region 以前版本
                    string controlType = node.Attributes["controltype"].Value.Trim();
                    switch (controlType.ToLower())
                    {
                        case "textbox":
                            {
                                ControlClass c = new ControlClass();

                                //命名空间
                                if (xmlHelper.IsAttrExist(node, "namespace"))
                                {
                                    c._NameSpace = node.Attributes["namespace"].Value.Trim();
                                }
                                else if (node.ParentNode != null && node.ParentNode.ParentNode != null)
                                {
                                    XmlNode NameSpaceNode = node.ParentNode.ParentNode;
                                    c._NameSpace = NameSpaceNode.Name;
                                }
                                //类
                                if (xmlHelper.IsAttrExist(node, "class"))
                                {
                                    c._Class = node.Attributes["class"].Value.Trim();
                                }
                                else if (node.ParentNode != null)
                                {
                                    XmlNode ClassNode = node.ParentNode;
                                    c._Class = ClassNode.Name;
                                }
                                //控件
                                if (!string.IsNullOrEmpty(node.Name))
                                {
                                    c._Name = node.Name.Trim();
                                }
                                //控件类型
                                c._ControlType = controlType;
                                //数据类型
                                if (xmlHelper.IsAttrExist(node, "datatype"))
                                {
                                    string typename = node.Attributes["datatype"].Value.Trim();

                                    CtrlAttr attr = GetAttr(_xmlpath, c._ControlType, typename);

                                    if (attr != null)
                                    {
                                        c._TypeName = typename;
                                        c._Attr = attr;
                                    }
                                    else
                                    {
                                        MessageBox.Show("没有找到指定类型名的数据类型！");
                                    }
                                }
                                //事件类型
                                if (xmlHelper.IsAttrExist(node, "eventtype"))
                                {
                                    c._EventType = node.Attributes["eventtype"].Value.Trim().ToUpper();
                                }
                                //if (!string.IsNullOrEmpty(node.Attributes["requiretype"].Value))
                                //{
                                //    c._RequireType = node.Attributes["requiretype"].Value.Trim();
                                //}

                                if (frmCtrl != null && frmCtrl.Name.Equals(c._Name))
                                {
                                    c._FrmControl = frmCtrl;
                                }

                                ctrl = c;
                            }
                            break;
                        case "datagridview":
                            {
                                ControlClass c = new ControlClass();

                                //命名空间
                                if (xmlHelper.IsAttrExist(node, "namespace"))
                                {
                                    c._NameSpace = node.Attributes["namespace"].Value.Trim();
                                }
                                else if (node.ParentNode != null && node.ParentNode.ParentNode != null)
                                {
                                    XmlNode NameSpaceNode = node.ParentNode.ParentNode;
                                    c._NameSpace = NameSpaceNode.Name;
                                }
                                //类
                                if (xmlHelper.IsAttrExist(node, "class"))
                                {
                                    c._Class = node.Attributes["class"].Value.Trim();
                                }
                                else if (node.ParentNode != null)
                                {
                                    XmlNode ClassNode = node.ParentNode.ParentNode;
                                    c._Class = ClassNode.Name;
                                }
                                //控件
                                if (!string.IsNullOrEmpty(node.Name))
                                {
                                    c._Name = node.Name.Trim();
                                }
                                //控件类型
                                c._ControlType = controlType;
                                //数据类型
                                if (xmlHelper.IsAttrExist(node, "datatype"))
                                {
                                    string typename = node.Attributes["datatype"].Value.Trim();

                                    CtrlAttr attr = GetAttr(_xmlpath, c._ControlType, typename);

                                    if (attr != null)
                                    {
                                        c._TypeName = typename;
                                        c._Attr = attr;
                                    }
                                    else
                                    {
                                        MessageBox.Show("没有找到指定类型名的数据类型！");
                                    }
                                }
                                //获取DataGridView数据列可空限制
                                if (!string.IsNullOrEmpty(node.Attributes["notnullindex"].Value.Trim()))
                                {
                                    string tmp = node.Attributes["notnullindex"].Value.Trim();
                                    tmp.Replace('，', ',');

                                    string[] indexTemp = tmp.Split(',');

                                    foreach (string item in indexTemp)
                                    {
                                        int idx;
                                        if (Int32.TryParse(item.Trim(), out idx))
                                        {
                                            c._NotNullIndex.Add(idx);
                                        }
                                        else
                                        {
                                            MessageBox.Show("DataGridView：" + c._Name + "非空列索引有误！");
                                        }
                                    }

                                    //string typename = node.Attributes["nulltype"].Value.Trim();

                                    //CtrlAttr atemp = GetAttr(_xmlpath, c._ControlType, typename);

                                    //if (atemp != null && atemp is AttrDataGridView)
                                    //{
                                    //    AttrDataGridView gTemp = atemp as AttrDataGridView;

                                    //    foreach (AttrInfo item in gTemp.Attr)
                                    //    {
                                    //        c._NullType.Add(item);
                                    //    }
                                    //}

                                }

                                if (frmCtrl != null && frmCtrl.Name.Equals(c._Name))
                                {
                                    c._FrmControl = frmCtrl;
                                }

                                ctrl = c;
                            }
                            break;
                        case "button":
                            {
                                ButtonClass btn = new ButtonClass();

                                //命名空间
                                if (xmlHelper.IsAttrExist(node, "namespace"))
                                {
                                    btn._NameSpace = node.Attributes["namespace"].Value.Trim();
                                }
                                else if (node.ParentNode != null && node.ParentNode.ParentNode != null)
                                {
                                    XmlNode NameSpaceNode = node.ParentNode.ParentNode;
                                    btn._NameSpace = NameSpaceNode.Name;
                                }
                                //类
                                if (xmlHelper.IsAttrExist(node, "class"))
                                {
                                    btn._Class = node.Attributes["class"].Value.Trim();
                                }
                                else if (node.ParentNode != null)
                                {
                                    XmlNode ClassNode = node.ParentNode;
                                    btn._Class = ClassNode.Name;
                                }
                                //控件
                                if (!string.IsNullOrEmpty(node.Name))
                                {
                                    btn._Name = node.Name.Trim();
                                }
                                //控件类型
                                btn._ControlType = controlType;
                                //验证控件
                                if (xmlHelper.IsAttrExist(node, "controls"))
                                {
                                    //edit by dwq 2015-06-12 按钮数据实体需要关联当前窗体控件
                                    btn._CtrlStr = node.Attributes["controls"].Value.Trim();

                                    //窗体中所有控件
                                    Form FormOwner = UtilClass.SearchOwner(frmCtrl) as Form;
                                    List<Control> FormControls = UtilClass.InitControls(FormOwner);

                                    string[] ctrlTmps = node.Attributes["controls"].Value.Trim().Split(',');

                                    foreach (string item in ctrlTmps)
                                    {
                                        Control FormControl = FormControls.Where(n => n.Name.Equals(item)).FirstOrDefault();

                                        if (FormControl != null && FormControl.Name.Equals(item))
                                        {
                                            ControlClass SubCtrl = new ControlClass();
                                            SubCtrl._NameSpace = btn._NameSpace;
                                            SubCtrl._Class = btn._Class;
                                            SubCtrl._Name = item;
                                            SubCtrl._ControlType = FormControl.GetType().Name;
                                            SubCtrl._FrmControl = FormControl;

                                            btn._Controls.Add(SubCtrl);
                                        }
                                    }
                                }
                                //提示信息
                                if (xmlHelper.IsAttrExist(node, "tip"))
                                {
                                    string tipname = node.Attributes["tip"].Value.ToString().Trim();

                                    btn._TipName = tipname;
                                    btn._Tips = GetTip(_xmlpath, tipname);
                                }

                                if (frmCtrl != null && frmCtrl.Name.Equals(btn._Name))
                                {
                                    btn._FrmControl = frmCtrl;
                                }

                                ctrl = btn;
                            }
                            break;
                        default:
                            ctrl = null;
                            break;
                    }
                    #endregion
                }
                else
                {
                    #region Xml精简后
                    if (frmCtrl != null)
                    {
                        string ctrlType = frmCtrl.GetType().Name;
                        switch (ctrlType.ToLower())
                        {
                            case "textbox":
                                {
                                    ControlClass c = new ControlClass();

                                    //命名空间
                                    if (xmlHelper.IsAttrExist(node, "namespace"))
                                    {
                                        c._NameSpace = node.Attributes["namespace"].Value.Trim();
                                    }
                                    else if (node.ParentNode != null && node.ParentNode.ParentNode != null)
                                    {
                                        XmlNode NameSpaceNode = node.ParentNode.ParentNode;
                                        c._NameSpace = NameSpaceNode.Name;
                                    }
                                    //类
                                    if (xmlHelper.IsAttrExist(node, "class"))
                                    {
                                        c._Class = node.Attributes["class"].Value.Trim();
                                    }
                                    else if (node.ParentNode != null)
                                    {
                                        XmlNode ClassNode = node.ParentNode;
                                        c._Class = ClassNode.Name;
                                    }
                                    //控件
                                    if (!string.IsNullOrEmpty(node.Name))
                                    {
                                        c._Name = node.Name.Trim();
                                    }
                                    //控件类型
                                    c._ControlType = ctrlType;
                                    //数据类型
                                    if (xmlHelper.IsAttrExist(node, "datatype"))
                                    {
                                        string typename = node.Attributes["datatype"].Value.Trim();

                                        CtrlAttr attr = GetAttr(_xmlpath, c._ControlType, typename);

                                        if (attr != null)
                                        {
                                            c._TypeName = typename;
                                            c._Attr = attr;
                                        }
                                        else
                                        {
                                            MessageBox.Show("没有找到指定类型名的数据类型！");
                                        }
                                    }
                                    //事件类型
                                    if (xmlHelper.IsAttrExist(node, "eventtype"))
                                    {
                                        c._EventType = node.Attributes["eventtype"].Value.Trim().ToUpper();
                                    }
                                    //if (!string.IsNullOrEmpty(node.Attributes["requiretype"].Value))
                                    //{
                                    //    c._RequireType = node.Attributes["requiretype"].Value.Trim();
                                    //}
                                    if (frmCtrl != null && frmCtrl.Name.Equals(c._Name))
                                    {
                                        c._FrmControl = frmCtrl;
                                    }

                                    ctrl = c;
                                }
                                break;
                            case "datagridview":
                                {
                                    ControlClass c = new ControlClass();

                                    //命名空间
                                    if (xmlHelper.IsAttrExist(node, "namespace"))
                                    {
                                        c._NameSpace = node.Attributes["namespace"].Value.Trim();
                                    }
                                    else if (node.ParentNode != null && node.ParentNode.ParentNode != null)
                                    {
                                        XmlNode NameSpaceNode = node.ParentNode.ParentNode;
                                        c._NameSpace = NameSpaceNode.Name;
                                    }
                                    //类
                                    if (xmlHelper.IsAttrExist(node, "class"))
                                    {
                                        c._Class = node.Attributes["class"].Value.Trim();
                                    }
                                    else if (node.ParentNode != null)
                                    {
                                        XmlNode ClassNode = node.ParentNode.ParentNode;
                                        c._Class = ClassNode.Name;
                                    }
                                    //控件
                                    if (!string.IsNullOrEmpty(node.Name))
                                    {
                                        c._Name = node.Name.Trim();
                                    }
                                    //控件类型
                                    c._ControlType = ctrlType;
                                    //数据类型
                                    if (xmlHelper.IsAttrExist(node, "datatype"))
                                    {
                                        string typename = node.Attributes["datatype"].Value.Trim();

                                        CtrlAttr attr = GetAttr(_xmlpath, c._ControlType, typename);

                                        if (attr != null)
                                        {
                                            c._TypeName = typename;
                                            c._Attr = attr;
                                        }
                                        else
                                        {
                                            MessageBox.Show("没有找到指定类型名的数据类型！");
                                        }
                                    }
                                    //获取DataGridView数据列可空限制
                                    if (!string.IsNullOrEmpty(node.Attributes["notnullindex"].Value.Trim()))
                                    {
                                        string tmp = node.Attributes["notnullindex"].Value.Trim();
                                        tmp.Replace('，', ',');

                                        string[] indexTemp = tmp.Split(',');

                                        foreach (string item in indexTemp)
                                        {
                                            int idx;
                                            if (Int32.TryParse(item.Trim(), out idx))
                                            {
                                                c._NotNullIndex.Add(idx);
                                            }
                                            else
                                            {
                                                MessageBox.Show("DataGridView：" + c._Name + "非空列索引有误！");
                                            }
                                        }

                                        //string typename = node.Attributes["nulltype"].Value.Trim();

                                        //CtrlAttr atemp = GetAttr(_xmlpath, c._ControlType, typename);

                                        //if (atemp != null && atemp is AttrDataGridView)
                                        //{
                                        //    AttrDataGridView gTemp = atemp as AttrDataGridView;

                                        //    foreach (AttrInfo item in gTemp.Attr)
                                        //    {
                                        //        c._NullType.Add(item);
                                        //    }
                                        //}

                                    }

                                    if (frmCtrl != null && frmCtrl.Name.Equals(c._Name))
                                    {
                                        c._FrmControl = frmCtrl;
                                    }

                                    ctrl = c;
                                }
                                break;
                            case "button":
                                {
                                    ButtonClass btn = new ButtonClass();

                                    //命名空间
                                    if (xmlHelper.IsAttrExist(node, "namespace"))
                                    {
                                        btn._NameSpace = node.Attributes["namespace"].Value.Trim();
                                    }
                                    else if (node.ParentNode != null && node.ParentNode.ParentNode != null)
                                    {
                                        XmlNode NameSpaceNode = node.ParentNode.ParentNode;
                                        btn._NameSpace = NameSpaceNode.Name;
                                    }
                                    //类
                                    if (xmlHelper.IsAttrExist(node, "class"))
                                    {
                                        btn._Class = node.Attributes["class"].Value.Trim();
                                    }
                                    else if (node.ParentNode != null)
                                    {
                                        XmlNode ClassNode = node.ParentNode;
                                        btn._Class = ClassNode.Name;
                                    }
                                    //控件
                                    if (!string.IsNullOrEmpty(node.Name))
                                    {
                                        btn._Name = node.Name.Trim();
                                    }
                                    //控件类型
                                    btn._ControlType = ctrlType;
                                    //验证控件
                                    if (xmlHelper.IsAttrExist(node, "controls"))
                                    {
                                        //edit by dwq 2015-06-12 按钮数据实体需要关联当前窗体控件
                                        btn._CtrlStr = node.Attributes["controls"].Value.Trim();

                                        //窗体中所有控件
                                        Form FormOwner = UtilClass.SearchOwner(frmCtrl) as Form;
                                        List<Control> FormControls = UtilClass.InitControls(FormOwner);

                                        string[] ctrlTmps = node.Attributes["controls"].Value.Trim().Split(',');

                                        foreach (string item in ctrlTmps)
                                        {
                                            Control FormControl = FormControls.Where(n => n.Name.Equals(item)).FirstOrDefault();

                                            if (FormControl != null && FormControl.Name.Equals(item))
                                            {
                                                if (FormControl.GetType().Name.Equals("TextBox"))
                                                {
                                                    ControlClass SubCtrl = new ControlClass();
                                                    SubCtrl._NameSpace = btn._NameSpace;
                                                    SubCtrl._Class = btn._Class;
                                                    SubCtrl._Name = item;
                                                    SubCtrl._ControlType = FormControl.GetType().Name;
                                                    SubCtrl._FrmControl = FormControl;

                                                    btn._Controls.Add(SubCtrl);
                                                }
                                                else if (FormControl.GetType().Name.Equals("DataGridView"))
                                                {
                                                    ControlClass SubCtrl = GetControl(_xmlpath, btn._NameSpace, btn._Class, FormControl) as ControlClass;

                                                    btn._Controls.Add(SubCtrl);
                                                }
                                            }
                                        }
                                    }
                                    //提示信息
                                    if (xmlHelper.IsAttrExist(node, "tip"))
                                    {
                                        string tipname = node.Attributes["tip"].Value.ToString().Trim();

                                        btn._TipName = tipname;
                                        btn._Tips = GetTip(_xmlpath, tipname);
                                    }

                                    if (frmCtrl != null && frmCtrl.Name.Equals(btn._Name))
                                    {
                                        btn._FrmControl = frmCtrl;
                                    }

                                    ctrl = btn;
                                }
                                break;
                            default:
                                ctrl = null;
                                break;
                        }
                    }
                    else
                    {
                        ctrl = null;
                    }
                    #endregion
                }
            }
            else
            {
                //node==null

                ctrl = null;
            }

            return ctrl;
        }

        /// <summary>
        /// 获取控件实体
        /// </summary>
        /// <param name="_xmlpath">Xml文件路径</param>
        /// <param name="_namespace">命名空间名</param>
        /// <param name="_class">类名</param>
        /// <param name="_frmCtrl">对应窗体实例</param>
        /// <returns></returns>
        private static BaseCtrl GetControl(string _xmlpath, string _namespace, string _class, Control _frmCtrl)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("//parameters");
            builder.Append("//controls");
            builder.Append("//" + _namespace);
            builder.Append("//" + _class);
            builder.Append("//" + _frmCtrl.Name);

            XmlNode node = xmlHelper.GetXmlNodeByXpath(_xmlpath, builder.ToString());

            return GetControl(_xmlpath, node, _frmCtrl);
        }

        /// <summary>
        /// 获取类型信息
        /// </summary>
        /// <param name="_xmlpath">Xml路径</param>
        /// <param name="_controltype">控件类型</param>
        /// <param name="typename">类型名</param>
        /// <returns></returns>
        private static CtrlAttr GetAttr(string _xmlpath, string _controltype, string typename)
        {
            CtrlAttr attr = null;
            StringBuilder builder = new StringBuilder();
            builder.Append("//parameters//types");
            builder.Append("//" + typename);
            string xpath = builder.ToString();

            //判定类型是否存在，不存在则返回空值
            if (xmlHelper.IsNodeExist(_xmlpath, xpath))
            {
                XmlNode node = xmlHelper.GetXmlNodeByXpath(_xmlpath, xpath);

                //索引
                List<string> idxs = new List<string>();
                //类型
                List<string> types = new List<string>();

                if (xmlHelper.IsAttrExist(node, "colIdx"))
                {
                    string tmp = node.Attributes["colIdx"].Value;
                    tmp.Replace('，', ',');

                    if (tmp.Contains(','))
                    {
                        foreach (string item in tmp.Split(',').ToList<string>())
                        {
                            idxs.Add(item.Trim());
                        }
                    }
                    else
                    {
                        idxs.Add(tmp.Trim());
                    }
                }

                if (xmlHelper.IsAttrExist(node, "datatype"))
                {
                    string tmp = node.Attributes["datatype"].Value;
                    tmp.Replace('，', ',');

                    if (tmp.Contains(','))
                    {
                        foreach (string item in tmp.Split(',').ToList<string>())
                        {
                            types.Add(item.Trim());
                        }
                    }
                    else
                    {
                        types.Add(tmp.Trim());
                    }
                }

                //对于TextBox
                if (_controltype.ToUpper() == "TEXTBOX")
                {
                    if (types.Count == 1)
                    {
                        AttrTextBox a = new AttrTextBox();

                        //将UNLIMITED换为NULL，无限制
                        if (types[0].Trim().ToUpper().Equals("UNLIMITED"))
                        {
                            a.Attr = new AttrInfo("NULL");
                        }
                        else
                        {
                            a.Attr = new AttrInfo(types[0].Trim());
                        }
                        attr = a;

                    }
                    else
                    {
                        attr = null;
                    }
                }
                //对于DataGridView
                else if (_controltype.ToUpper() == "DATAGRIDVIEW")
                {
                    if (idxs.Count == types.Count && idxs.Count > 0)
                    {
                        AttrDataGridView a = new AttrDataGridView();

                        for (int i = 0; i < idxs.Count; i++)
                        {
                            //对于对列数据无限制的，统一改为NULL
                            string type = string.Empty;
                            if (types[i].Trim().ToUpper().Equals("UNLIMITED"))
                            {
                                type = "NULL";
                            }
                            else
                            {
                                type = types[i].Trim();
                            }

                            a.Attr.Add(new AttrInfo(Convert.ToInt32(idxs[i].Trim()), type));
                        }
                        attr = a;
                    }
                    else
                    {
                        MessageBox.Show("DataGridView配置信息有误，索引与类型限制不匹配！");
                    }
                }
                //异常
                else
                {
                    MessageBox.Show("控件类型有误！");
                }
            }

            return attr;
        }

        /// <summary>
        /// 写入类型信息
        /// </summary>
        /// <param name="_xmlpath">Xml路径</param>
        /// <param name="typename">类型名称</param>
        /// <param name="attr">待写入属性信息</param>
        /// <returns></returns>
        private static bool WriteAttr(string _xmlpath, string typename, CtrlAttr attr)
        {
            //是否写入成功
            bool flag = true;

            StringBuilder builder = new StringBuilder();
            builder.Append("//parameters//types");
            builder.Append("//" + typename);
            string xpath = builder.ToString();

            //获取属性表
            Hashtable table = new Hashtable();

            if (attr is AttrTextBox)
            {
                AttrTextBox atxt = attr as AttrTextBox;

                table.Add("colIdx", atxt.Attr.Index);
                table.Add("datatype", atxt.Attr.DataType);
            }
            else if (attr is AttrDataGridView)
            {
                AttrDataGridView agrid = attr as AttrDataGridView;

                List<int> idxs = (from a in agrid.Attr select a.Index).ToList<int>();
                List<string> types = (from a in agrid.Attr select a.DataType).ToList<string>();

                table.Add("colIdx", string.Join(",", idxs));
                table.Add("datatype", string.Join(",", types));
            }
            else
            {
                table = null;
            }

            //判断是否已经存在类型节点
            if (xmlHelper.IsNodeExist(_xmlpath, xpath))
            {
                flag = xmlHelper.UpdateNode(_xmlpath, xpath, table);
            }
            else
            {
                if (table != null)
                {
                    flag = xmlHelper.InsertNode(_xmlpath, typename, true, "//parameters//types", table, null);
                }
                else
                {
                    //异常情况
                    MessageBox.Show("代写入属性值异常！");
                    flag = false;
                }
            }


            return flag;
        }

        /// <summary>
        /// 获取提示信息
        /// </summary>
        /// <param name="_xmlpath">XML文件路径</param>
        /// <param name="_tipname">提示信息名</param>
        /// <returns></returns>
        private static Hashtable GetTip(string _xmlpath, string _tipname)
        {
            Hashtable tips = new Hashtable();

            StringBuilder builder = new StringBuilder();
            builder.Append("//parameters");
            builder.Append("//tipmessages");
            builder.Append("//");
            builder.Append(_tipname);

            if (xmlHelper.IsNodeExist(_xmlpath, builder.ToString()))
            {
                XmlNode tipNode = xmlHelper.GetXmlNodeByXpath(_xmlpath, builder.ToString());

                List<int> eNos = new List<int>();
                List<string> eMessages = new List<string>();

                if (xmlHelper.IsAttrExist(tipNode, "errorno"))
                {
                    string errorNo = tipNode.Attributes["errorno"].Value.ToString();
                    string[] errorNos = errorNo.Split(',');

                    foreach (string item in errorNos)
                    {
                        if (string.IsNullOrEmpty(item))
                            continue;

                        int eno = -1;

                        if (Int32.TryParse(item, out eno))
                        {
                            eNos.Add(eno);
                        }
                        else
                        {
                            MessageBox.Show("错误编号配置有误！");
                        }
                    }
                }

                if (xmlHelper.IsAttrExist(tipNode, "errormessage"))
                {
                    string errorMessage = tipNode.Attributes["errormessage"].Value.ToString();
                    string[] errorMessages = errorMessage.Split(',');

                    foreach (string item in errorMessages)
                    {
                        if (string.IsNullOrEmpty(item))
                            continue;

                        eMessages.Add(item);
                    }
                }

                if (eNos.Count > 0 && eNos.Count == eMessages.Count)
                {
                    for (int i = 0; i < eNos.Count; i++)
                    {
                        tips.Add(eNos[i], eMessages[i]);
                    }
                }
                else if (eMessages.Count ==1 && (eNos == null || eNos.Count <= 0))
                {
                    tips.Add(1, eMessages[0]);
                }
                else
                {
                    MessageBox.Show("提示信息节点：[" + _tipname + "]配置有误，错误编号与提示信息不匹配！");
                }
            }

            return tips;
        }

        /// <summary>
        /// 写入提示信息
        /// </summary>
        /// <param name="_xmlpath">XML文件路径</param>
        /// <param name="_tipname">提示信息名</param>
        /// <param name="_tips">提示信息</param>
        /// <returns></returns>
        private static bool WriteTip(string _xmlpath, string _tipname, Hashtable _tips)
        {
            bool flag = true;

            StringBuilder builder = new StringBuilder();
            builder.Append("//parameters");
            builder.Append("//tipmessages");

            List<int> eNos = new List<int>();
            List<string> eMessages = new List<string>();
            foreach (DictionaryEntry item in _tips)
            {
                if (item.Key == null || item.Value == null)
                {
                    continue;
                }

                eNos.Add(Convert.ToInt32(item.Key));
                eMessages.Add(Convert.ToString(item.Value));
            }

            Hashtable attrs = new Hashtable();
            attrs.Add("errorno", string.Join(",", eNos));
            attrs.Add("errormessage", string.Join(",", eMessages));

            builder.Append("//" + _tipname);
            string xpath = builder.ToString();

            if (xmlHelper.IsNodeExist(_xmlpath, xpath))
            {
                flag = xmlHelper.UpdateNode(_xmlpath, xpath, attrs);
            }
            else
            {
                flag = xmlHelper.InsertNode(_xmlpath, _tipname, true, "//parameters//tipmessages", attrs, null);
            }

            return flag;
        }

        /// <summary>
        /// 获取新类型的名称
        /// </summary>
        /// <param name="_xmlpath">Xml路径</param>
        /// <returns></returns>
        private static string GetNewTypeName(string _xmlpath)
        {
            string typename = string.Empty;

            string xpath = "//parameters//types";
            //为简单起见，不考虑删除类型的情况，类型名直接由type和总类型数目组成
            XmlNode typeRoot = xmlHelper.GetXmlNodeByXpath(_xmlpath, xpath);
            int count = typeRoot.ChildNodes.Count;

            typename = "type" + (count + 1);

            return typename;
        }

        private static string GetNewTipName(string _xmlpath)
        {
            string tipname = string.Empty;

            string xpath = "//parameters//tipmessages";
            XmlNode tipRoot = xmlHelper.GetXmlNodeByXpath(_xmlpath, xpath);
            int count = tipRoot.ChildNodes.Count;

            tipname = "tip" + (count + 1);

            return tipname;
        }

        ///// <summary>
        ///// 获取提示信息内容
        ///// </summary>
        ///// <param name="_xmlpath">Xml文件路径</param>
        ///// <param name="_tipname">提示信息节点名</param>
        ///// <returns></returns>
        //private static Hashtable GetTip(string _xmlpath, string _tipname)
        //{
        //    Hashtable tip = new Hashtable();

        //    StringBuilder builder = new StringBuilder();
        //    builder.Append("//parameters//tipmessages");
        //    builder.Append("//");
        //    builder.Append(_tipname);

        //    XmlNode attrNode = xmlHelper.GetXmlNodeByXpath(_xmlpath, builder.ToString());
        //    if (attrNode != null && xmlHelper.IsAttrExist(attrNode, "errorno") && xmlHelper.IsAttrExist(attrNode, "errormessage"))
        //    {
        //        string eno = attrNode.Attributes["errorno"].Value.ToString().Trim();
        //        string eMessage = attrNode.Attributes["errormessage"].Value.ToString().Trim();

        //        List<int> errorNo = new List<int>();
        //        List<string> errorMessage = new List<string>();

        //        string[] enoTmps = eno.Split(',');
        //        foreach (string no in enoTmps)
        //        {
        //            if (IsInt32Text(no.Trim()))
        //            {
        //                errorNo.Add(Int32.Parse(no.Trim()));
        //            }
        //        }

        //        string[] eMxTmps = eMessage.Split(',');
        //        foreach (string m in eMxTmps)
        //        {
        //            errorMessage.Add(m);
        //        }

        //        int upLimit = 0;
        //        if (errorNo.Count >= errorMessage.Count)
        //        {
        //            upLimit = errorMessage.Count;
        //        }
        //        else
        //        {
        //            upLimit = errorNo.Count;
        //        }

        //        for (int i = 0; i < upLimit; i++)
        //        {
        //            tip.Add(errorNo[i], errorMessage[i]);
        //        }
        //    }

        //    return tip;
        //}

        /// <summary>
        /// 判断是否是整数文本
        /// </summary>
        /// <param name="content">文本内容</param>
        /// <returns></returns>
        private static bool IsInt32Text(string content)
        {
            Regex reg = new Regex("^[0-9]*$");

            if (reg.IsMatch(content))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #endregion
    }
}
