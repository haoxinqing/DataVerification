using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace UtilForDataCheck
{
    /// <summary>
    /// 控件类，用于数据类型验证
    /// </summary>
    public class ControlClass : BaseCtrl
    {
        ////控件名
        //public string _Control { get; set; }
        //事件类型
        public string _EventType { get; set; }
        ////要求类型
        //public string _RequireType { get; set; }

        //数据类型节点名
        public string _TypeName { get; set; }
        //控件数据类型信息
        private CtrlAttr _attr;
        public CtrlAttr _Attr
        {
            get
            {
                return _attr;
            }
            set
            {
                _attr = value;
            }
        }
        //数据列可空类型（对DataGridView）
        private List<int> _notNullIndex;
        public List<int> _NotNullIndex
        {
            get
            {
                if (_notNullIndex == null)
                {
                    _notNullIndex = new List<int>();
                }

                return _notNullIndex;
            }
            set
            {
                _notNullIndex = value;
            }
        }
        //private List<AttrInfo> _nullType;
        //public List<AttrInfo> _NullType
        //{
        //    get
        //    {
        //        if(_nullType == null)
        //        {
        //            _nullType = new List<AttrInfo>();
        //        }

        //        return _nullType;
        //    }
        //    set
        //    {
        //        _nullType = value;
        //    }
        //}

        public ControlClass()
        {
            _TypeName = string.Empty;
        }

    }

    /// <summary>
    /// 按钮实体类，主要用于各数据非空验证
    /// </summary>
    public class ButtonClass : BaseCtrl
    {
        //控件字符串
        public string _CtrlStr { get; set; }
        //提示节点名称
        public string _TipName { get; set; }
        //错误提示信息
        private Hashtable _tips = new Hashtable();
        public Hashtable _Tips
        {
            get
            {
                if (_tips == null)
                {
                    _tips = new Hashtable();
                }

                return _tips;
            }
            set
            {
                _tips = value;
            }
        }
        //窗体控件集合
        private List<ControlClass> _controls;
        public List<ControlClass> _Controls
        {
            get
            {
                if (_controls == null)
                {
                    _controls = new List<ControlClass>();
                }

                return _controls;
            }
            set
            {
                _controls = value;
            }
        }

        public ButtonClass()
        {
            _TipName = string.Empty;
        }
    }

    /// <summary>
    /// 控件基类
    /// </summary>
    public class BaseCtrl
    {
        //命名空间
        public string _NameSpace { get; set; }
        //类
        public string _Class { get; set; }
        //控件名
        public string _Name { get; set; }
        //对应窗体控件
        public Control _FrmControl { get; set; }
        //控件类型
        public string _ControlType { get; set; }
    }

    /// <summary>
    /// 控件属性基类
    /// </summary>
    public class CtrlAttr
    {

    }

    /// <summary>
    /// TextBox类型控件类型信息
    /// </summary>
    public class AttrTextBox : CtrlAttr
    {
        //数据属性
        private AttrInfo attr;
        public AttrInfo Attr
        {
            get
            {
                if (attr == null)
                {
                    attr = new AttrInfo();
                }

                return attr;
            }
            set { attr = value; }
        }
    }

    /// <summary>
    /// DataGridView控件类型信息
    /// </summary>
    public class AttrDataGridView : CtrlAttr
    {
        //数据属性
        private List<AttrInfo> attr;
        public List<AttrInfo> Attr
        {
            get
            {
                if (attr == null)
                {
                    attr = new List<AttrInfo>();
                }

                return attr;
            }
            set { attr = value; }
        }
    }

    /// <summary>
    /// 数据类型信息
    /// </summary>
    public class AttrInfo
    {
        //列索引
        public int Index { get; set; }
        //数据类型
        public string DataType { get; set; }

        public AttrInfo()
        {

        }

        public AttrInfo(string type)
        {
            Index = -1;
            DataType = type.ToUpper();
        }

        public AttrInfo(int idx, string type)
        {
            Index = idx;
            DataType = type.ToUpper();
        }
    }
}
