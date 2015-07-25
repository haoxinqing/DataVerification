using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections;

namespace UtilForDataCheck
{
    /// <summary>
    /// 数据验证类
    /// </summary>
    public class DataCheck
    {
        #region 数据验证

        #region 变量
        //Xml文件路径
        public static string XmlPath = string.Empty;
        /// <summary>
        /// 名称集合
        /// </summary>
        private static List<string> NameList = new List<string>();
        //是否显示错误信息
        public static bool IsCommentShow = true;
        ////DataGridView各列数据类型，无限制的置null
        //private static Hashtable GridDataType = new Hashtable();
        //private static Control LastCtrl;

        #endregion

        #region 内部方法

        /// <summary>
        /// 校验表达式是否为合法的科学计数法表达式
        /// </summary>
        /// <param name="basePart">基数部分</param>
        /// <param name="ePosition">E位置</param>
        /// <param name="exponent">指数部分</param>
        /// <returns></returns>
        private static bool IsSciExpressLegal(string basePart, int ePosition, string expPart)
        {
            //校验科学计数法
            if (!IsNumber(basePart) || !IsInteger(expPart))
            {
                return false;
            }
            else
            {
                if (!string.IsNullOrEmpty(basePart) && !string.IsNullOrEmpty(expPart))
                {
                    if (ePosition != -1 && ePosition == basePart.Length)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 校验是否为数值（含科学计数法）
        /// </summary>
        /// <param name="express">表达式</param>
        /// <returns></returns>
        private static bool IsSciExpressLegal(string express)
        {
            Regex reg = new Regex(@"^(-?)(\d+)$|^(-?)(\d+)[.](\d+)$|^(-?)(\d+)[E,e](\d+)$|^(-?)(\d+)[.](\d+)[E,e](\d+)$");

            return reg.IsMatch(express);
        }

        /// <summary>
        /// 是否是数值（整数、小数）
        /// </summary>
        /// <param name="express">表达式</param>
        /// <returns></returns>
        private static bool IsNumber(string express)
        {
            Regex NumberReg = new Regex(@"^(-?)([0-9]+)$|^(-?)([0-9]+)[.]([0-9]+)$");

            return NumberReg.IsMatch(express);
        }

        /// <summary>
        /// 是否是整数
        /// </summary>
        /// <param name="express">表达式</param>
        /// <returns></returns>
        private static bool IsInteger(string express)
        {
            Regex IntegeReg = new Regex(@"^([0-9]+)$");

            return IntegeReg.IsMatch(express);
        }

        #endregion
        #region 实数验证

        /// <summary>
        /// 实数输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void RealNumberKeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            //可接收的字符有数字0-9，退格，小数点以及负号
            if ((e.KeyChar > (char)47 && e.KeyChar < (char)58) || e.KeyChar == (char)8 || e.KeyChar == (char)46 || e.KeyChar == (char)45)
            {

                switch (e.KeyChar)
                {
                    //小数点
                    case (char)46:
                        if (textBox.Text.Contains((char)46))
                        {
                            textBox.SelectionStart = textBox.Text.IndexOf((char)46) + 1;
                            textBox.SelectionLength = 0;
                            e.Handled = true;
                        }
                        break;
                    //负号
                    case (char)45:
                        if (!textBox.Text.Contains((char)45))
                        {
                            textBox.Text = "-" + textBox.Text;
                        }
                        textBox.SelectionStart = textBox.Text.IndexOf((char)45) + 1;
                        textBox.SelectionLength = 0;
                        e.Handled = true;
                        break;
                    //数字
                    default:
                        if ((e.KeyChar > (char)47 && e.KeyChar < (char)58))
                        {
                            //if (textBox.SelectedText.Contains((char)46) || textBox.SelectedText.Contains((char)45))
                            //{
                            //    e.Handled = true;
                            //}

                            //add by dwq 2015-06-01 对含有负号的情况特殊处理
                            if (textBox.Text.Contains("-"))
                            {
                                //负号的索引
                                int negIdx = textBox.Text.IndexOf((char)45);

                                //不能再负号前面输入数字
                                if (textBox.SelectionStart + textBox.SelectionLength <= negIdx)
                                {
                                    e.Handled = true;
                                }
                            }
                        }
                        break;
                }
            }
            else
            {
                TipForm tf = new TipForm(textBox, "请输入合法的实数数据！");
                tf.Show();

                textBox.Focus();

                ////add by dwq 2015-05-27 激活父窗体
                //Form frm = UtilClass.SearchOwner(textBox) as Form;
                //frm.Activate();

                e.Handled = true;
            }
        }
        /// <summary>
        /// （正）实数离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void RealNumberLeave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (string.IsNullOrEmpty(textBox.Text))
                return;

            double d = 0;
            if (double.TryParse(textBox.Text, out d))
            {
                textBox.Text = d.ToString();
            }
            else
            {
                TipForm tip = new TipForm(textBox, "不合法的实数数据！");
                tip.Show();

                textBox.Text = null;
            }
        }

        /// <summary>
        /// 正实数输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void PositiveRealNumberKeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if ((e.KeyChar > (char)47 && e.KeyChar < (char)58) || e.KeyChar == (char)8 || e.KeyChar == (char)46)
            {
                switch (e.KeyChar)
                {
                    //小数点
                    case (char)46:
                        if (textBox.Text.Contains((char)46))
                        {
                            textBox.SelectionStart = textBox.Text.IndexOf((char)46) + 1;
                            textBox.SelectionLength = 0;
                            e.Handled = true;
                        }
                        break;
                    //数字
                    default:
                        if ((e.KeyChar > (char)47 && e.KeyChar < (char)58))
                        {
                            if (textBox.SelectedText.Contains((char)46))
                            {
                                e.Handled = true;
                            }
                        }
                        break;
                }
            }
            else
            {
                TipForm tf = new TipForm(textBox, "输入的不是正实数。");
                tf.Show();

                e.Handled = true;
            }
        }

        /// <summary>
        /// 实数输入（支持科学计数法）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void RealNumberWithSciKeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            //是否是科学计数法
            bool IsSciNotation = false;
            //字母E所处位置，默认-1
            int EPosition = -1;

            //输入前文本内容
            string OriText = textBox.Text;
            int SelectionStart = textBox.SelectionStart;
            int SelectionLen = textBox.SelectionLength;

            if (OriText.ToLower().Contains("e"))
            {
                IsSciNotation = true;
                EPosition = OriText.ToLower().IndexOf("e");
            }

            //可接收的字符有数字0-9，字母E或e，退格，小数点以及负号
            if ((e.KeyChar > (char)47 && e.KeyChar < (char)58) || e.KeyChar == (char)8 || e.KeyChar == (char)46 || e.KeyChar == (char)45 || e.KeyChar == 'E' || e.KeyChar == 'e')
            {

                switch (e.KeyChar)
                {
                    //小数点
                    case (char)46:
                        if (IsSciNotation)
                        {
                            if (textBox.Text.Contains((char)46))
                            {
                                textBox.SelectionStart = textBox.Text.IndexOf((char)46) + 1;
                                textBox.SelectionLength = 0;
                            }

                            //不能在指数位置输入小数
                            e.Handled = true;
                        }
                        break;
                    //负号
                    case (char)45:
                        if (!textBox.Text.Contains((char)45))
                        {
                            textBox.Text = "-" + textBox.Text;
                        }
                        textBox.SelectionStart = textBox.Text.IndexOf((char)45) + 1;
                        textBox.SelectionLength = 0;
                        e.Handled = true;
                        break;
                    //科学计数法
                    case 'E':
                        if (IsSciNotation)
                        {
                            textBox.SelectionStart = EPosition + 1;
                            textBox.SelectionLength = 0;

                            e.Handled = true;
                        }
                        else
                        {
                            IsSciNotation = true;
                        }
                        break;
                    case 'e':
                        if (IsSciNotation)
                        {
                            textBox.SelectionStart = EPosition + 1;
                            textBox.SelectionLength = 0;

                            e.Handled = true;
                        }
                        else
                        {
                            IsSciNotation = true;
                        }
                        break;
                    //数字
                    default:
                        if ((e.KeyChar > (char)47 && e.KeyChar < (char)58))
                        {
                            //if (textBox.SelectedText.Contains((char)46) || textBox.SelectedText.Contains((char)45))
                            //{
                            //    e.Handled = true;
                            //}

                            //add by dwq 2015-06-01 对含有负号的情况特殊处理
                            if (textBox.Text.Contains("-"))
                            {
                                //负号的索引
                                int negIdx = textBox.Text.IndexOf((char)45);

                                //不能再负号前面输入数字
                                if (textBox.SelectionStart + textBox.SelectionLength <= negIdx)
                                {
                                    e.Handled = true;
                                }
                            }
                        }
                        break;
                }
            }
            else
            {
                TipForm tf = new TipForm(textBox, "请输入合法的实数数据！");
                tf.Show();

                textBox.Focus();

                ////add by dwq 2015-05-27 激活父窗体
                //Form frm = UtilClass.SearchOwner(textBox) as Form;
                //frm.Activate();

                e.Handled = true;
            }
        }
        /// <summary>
        /// 实数离开（支持科学计数法）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void RealNumberWithSciLeave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (string.IsNullOrEmpty(textBox.Text))
                return;

            if (!IsSciExpressLegal(textBox.Text))
            {
                TipForm tip = new TipForm(textBox, "不合法的实数类型表达式！");
                tip.Show();

                textBox.Text = null;
            }
        }
        /// <summary>
        /// 正实数输入（支持科学计数法）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void PositiveRealNumberWithSciKeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            //是否是科学计数法
            bool IsSciNotation = false;
            //字母E所处位置，默认-1
            int EPosition = -1;

            //输入前文本内容
            string OriText = textBox.Text;
            int SelectionStart = textBox.SelectionStart;
            int SelectionLen = textBox.SelectionLength;

            if (OriText.ToLower().Contains("e"))
            {
                IsSciNotation = true;
                EPosition = OriText.ToLower().IndexOf("e");
            }

            if ((e.KeyChar > (char)47 && e.KeyChar < (char)58) || e.KeyChar == (char)8 || e.KeyChar == (char)46 || e.KeyChar == 'E' || e.KeyChar == 'e')
            {
                switch (e.KeyChar)
                {
                    //小数点
                    case (char)46:
                        if (IsSciNotation)
                        {
                            if (textBox.Text.Contains((char)46))
                            {
                                textBox.SelectionStart = textBox.Text.IndexOf((char)46) + 1;
                                textBox.SelectionLength = 0;
                            }

                            //不能在指数位置输入小数
                            e.Handled = true;
                        }
                        break;
                    //科学计数法
                    case 'E':
                        if (IsSciNotation)
                        {
                            textBox.SelectionStart = EPosition + 1;
                            textBox.SelectionLength = 0;

                            e.Handled = true;
                        }
                        else
                        {
                            IsSciNotation = true;
                        }
                        break;
                    case 'e':
                        if (IsSciNotation)
                        {
                            textBox.SelectionStart = EPosition + 1;
                            textBox.SelectionLength = 0;

                            e.Handled = true;
                        }
                        else
                        {
                            IsSciNotation = true;
                        }
                        break;
                    //数字
                    default:
                        if ((e.KeyChar > (char)47 && e.KeyChar < (char)58))
                        {
                            if (textBox.SelectedText.Contains((char)46))
                            {
                                e.Handled = true;
                            }
                        }
                        break;
                }
            }
            else
            {
                TipForm tf = new TipForm(textBox, "请输入合法的正实数数据！");
                tf.Show();

                e.Handled = true;
            }
        }

        /// <summary>
        /// 正实数离开（支持科学计数法）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void PositiveRealNumberWithSciLeave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (string.IsNullOrEmpty(textBox.Text))
                return;

            if (!IsSciExpressLegal(textBox.Text) || textBox.Text.Contains('-'))
            {
                TipForm tip = new TipForm(textBox, "不合法的正实数表达式！");
                tip.Show();

                textBox.Text = null;
            }
        }

        #endregion
        #region 整数验证

        /// <summary>
        /// 整数输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void integerNumberKeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if ((e.KeyChar > (char)47 && e.KeyChar < (char)58) || e.KeyChar == (char)8 || e.KeyChar == (char)45)
            {
                switch (e.KeyChar)
                {
                    //负号
                    case (char)45:
                        if (!textBox.Text.Contains((char)45))
                        {
                            textBox.Text = "-" + textBox.Text;
                        }
                        textBox.SelectionStart = textBox.Text.IndexOf((char)45) + 1;
                        textBox.SelectionLength = 0;
                        e.Handled = true;
                        break;
                    //数字
                    default:
                        if ((e.KeyChar > (char)47 && e.KeyChar < (char)58))
                        {
                            //if (textBox.SelectedText.Contains((char)46) || textBox.SelectedText.Contains((char)45))
                            //{
                            //    e.Handled = true;
                            //}

                            //add by dwq 2015-06-01 对含有负号的情况特殊处理
                            if (textBox.Text.Contains("-"))
                            {
                                //负号的索引
                                int negIdx = textBox.Text.IndexOf((char)45);

                                //不能再负号前面输入数字
                                if (textBox.SelectionStart + textBox.SelectionLength <= negIdx)
                                {
                                    e.Handled = true;
                                }
                            }
                        }
                        break;
                }
            }
            else
            {
                TipForm tf = new TipForm(textBox, "不合法的整数数据！");
                tf.Show();

                textBox.Focus();

                ////add by dwq 2015-05-27 激活父窗体
                //Form frm = UtilClass.SearchOwner(textBox) as Form;
                //frm.Activate();

                e.Handled = true;
            }
        }
        /// <summary>
        /// （正）整数离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void integerNumberLeave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (string.IsNullOrEmpty(textBox.Text))
                return;

            int d = 0;
            if (int.TryParse(textBox.Text, out d))
            {
                textBox.Text = d.ToString();
            }
            else
            {
                TipForm tip = new TipForm(textBox, "不合法的整数数据！");
                tip.Show();

                textBox.Text = null;
            }
        }
        /// <summary>
        /// 正整数输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void PositiveintegerNumberKeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if ((e.KeyChar > (char)47 && e.KeyChar < (char)58) || e.KeyChar == (char)8 || e.KeyChar == (char)46)
            {
                switch (e.KeyChar)
                {
                    //小数点
                    case (char)46:
                        if (textBox.Text.Contains((char)46))
                        {
                            textBox.SelectionStart = textBox.Text.IndexOf((char)46) + 1;
                            textBox.SelectionLength = 0;
                            e.Handled = true;
                        }
                        break;
                    //数字
                    default:
                        if ((e.KeyChar > (char)47 && e.KeyChar < (char)58))
                        {
                            if (textBox.SelectedText.Contains((char)46))
                            {
                                e.Handled = true;
                            }
                        }
                        break;
                }
            }
            else
            {
                TipForm tf = new TipForm(textBox, "请输入合法的正整数数据！");
                tf.Show();

                e.Handled = true;
            }
        }

        /// <summary>
        /// 整数输入（支持科学计数法）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void IntegerNumberWithSciKeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            //是否是科学计数法
            bool IsSciNotation = false;
            //字母E所处位置，默认-1
            int EPosition = -1;

            //输入前文本内容
            string OriText = textBox.Text;
            int SelectionStart = textBox.SelectionStart;
            int SelectionLen = textBox.SelectionLength;

            if (OriText.ToLower().Contains("e"))
            {
                IsSciNotation = true;
                EPosition = OriText.ToLower().IndexOf("e");
            }

            if ((e.KeyChar > (char)47 && e.KeyChar < (char)58) || e.KeyChar == (char)8 || e.KeyChar == (char)45 || e.KeyChar == 'E' || e.KeyChar == 'e')
            {
                switch (e.KeyChar)
                {
                    //负号
                    case (char)45:
                        if (!textBox.Text.Contains((char)45))
                        {
                            textBox.Text = "-" + textBox.Text;
                        }
                        textBox.SelectionStart = textBox.Text.IndexOf((char)45) + 1;
                        textBox.SelectionLength = 0;
                        e.Handled = true;
                        break;
                    //科学计数法
                    case 'E':
                        if (IsSciNotation)
                        {
                            textBox.SelectionStart = EPosition + 1;
                            textBox.SelectionLength = 0;

                            e.Handled = true;
                        }
                        else
                        {
                            IsSciNotation = true;
                        }
                        break;
                    case 'e':
                        if (IsSciNotation)
                        {
                            textBox.SelectionStart = EPosition + 1;
                            textBox.SelectionLength = 0;

                            e.Handled = true;
                        }
                        else
                        {
                            IsSciNotation = true;
                        }
                        break;
                    //数字
                    default:
                        if ((e.KeyChar > (char)47 && e.KeyChar < (char)58))
                        {
                            //if (textBox.SelectedText.Contains((char)46) || textBox.SelectedText.Contains((char)45))
                            //{
                            //    e.Handled = true;
                            //}

                            //add by dwq 2015-06-01 对含有负号的情况特殊处理
                            if (textBox.Text.Contains("-"))
                            {
                                //负号的索引
                                int negIdx = textBox.Text.IndexOf((char)45);

                                //不能再负号前面输入数字
                                if (textBox.SelectionStart + textBox.SelectionLength <= negIdx)
                                {
                                    e.Handled = true;
                                }
                            }
                        }
                        break;
                }
            }
            else
            {
                TipForm tf = new TipForm(textBox, "不合法的整数数据！");
                tf.Show();

                e.Handled = true;
            }
        }

        /// <summary>
        /// 整数离开（支持科学计数法）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void IntegerNumberWithSciLeave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (string.IsNullOrEmpty(textBox.Text))
                return;

            if (IsSciExpressLegal(textBox.Text))
            {
                //校验是否为整数
                string content = textBox.Text.ToLower();

                if (content.Contains('e'))
                {
                    if (content.Contains('-'))
                    {
                        int EPosition = content.IndexOf('e');
                        string basePart = content.Substring(1, EPosition);
                        string expPart = content.Substring(EPosition + 1);

                        if (basePart.Contains('.'))
                        {
                            //小数点位置
                            int dotPosition = basePart.IndexOf('.');
                            //小数位数
                            int decimalLen = basePart.Length - 2 - dotPosition;

                            int expNumber = Convert.ToInt32(expPart);

                            if (decimalLen > expNumber)
                            {
                                TipForm tip = new TipForm(textBox, "不合法的整数类型科学计数法表达式！");
                                tip.Show();

                                textBox.Text = null;
                            }
                        }
                    }
                    else
                    {
                        int EPosition = content.IndexOf('e');
                        string basePart = content.Substring(0, EPosition);
                        string expPart = content.Substring(EPosition + 1);

                        if (basePart.Contains('.'))
                        {
                            //小数点位置
                            int dotPosition = basePart.IndexOf('.');
                            //小数位数
                            int decimalLen = basePart.Length - 1 - dotPosition;

                            int expNumber = Convert.ToInt32(expPart);

                            if (decimalLen > expNumber)
                            {
                                TipForm tip = new TipForm(textBox, "不合法的整数类型科学计数法表达式！");
                                tip.Show();

                                textBox.Text = null;
                            }
                        }
                    }
                }
            }
            else
            {
                TipForm tip = new TipForm(textBox, "不合法的整数数据！");
                tip.Show();

                textBox.Text = null;
            }
        }

        /// <summary>
        /// 正整数输入（支持科学计数法）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void PositiveIntegerNumberWithSciKeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            //是否是科学计数法
            bool IsSciNotation = false;
            //字母E所处位置，默认-1
            int EPosition = -1;

            //输入前文本内容
            string OriText = textBox.Text;
            int SelectionStart = textBox.SelectionStart;
            int SelectionLen = textBox.SelectionLength;

            if (OriText.ToLower().Contains("e"))
            {
                IsSciNotation = true;
                EPosition = OriText.ToLower().IndexOf("e");
            }

            if ((e.KeyChar > (char)47 && e.KeyChar < (char)58) || e.KeyChar == (char)8 || e.KeyChar == (char)46 || e.KeyChar == 'E' || e.KeyChar == 'e')
            {
                switch (e.KeyChar)
                {
                    //小数点
                    case (char)46:
                        if (textBox.Text.Contains((char)46))
                        {
                            textBox.SelectionStart = textBox.Text.IndexOf((char)46) + 1;
                            textBox.SelectionLength = 0;
                            e.Handled = true;
                        }
                        break;
                    //科学计数法
                    case 'E':
                        if (IsSciNotation)
                        {
                            textBox.SelectionStart = EPosition + 1;
                            textBox.SelectionLength = 0;

                            e.Handled = true;
                        }
                        else
                        {
                            IsSciNotation = true;
                        }
                        break;
                    case 'e':
                        if (IsSciNotation)
                        {
                            textBox.SelectionStart = EPosition + 1;
                            textBox.SelectionLength = 0;

                            e.Handled = true;
                        }
                        else
                        {
                            IsSciNotation = true;
                        }
                        break;
                    //数字
                    default:
                        if ((e.KeyChar > (char)47 && e.KeyChar < (char)58))
                        {
                            if (textBox.SelectedText.Contains((char)46))
                            {
                                e.Handled = true;
                            }
                        }
                        break;
                }
            }
            else
            {
                TipForm tf = new TipForm(textBox, "请输入合法的正整数数据！");
                tf.Show();

                e.Handled = true;
            }
        }
        /// <summary>
        /// 正整数离开（支持科学计数法）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void PositiveIntegerNumberWithSciLeave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (string.IsNullOrEmpty(textBox.Text))
                return;

            if (IsSciExpressLegal(textBox.Text) && !(textBox.Text.Contains('-')))
            {
                //校验是否为整数
                string content = textBox.Text.ToLower();

                if (content.Contains('e'))
                {
                    int EPosition = content.IndexOf('e');
                    string basePart = content.Substring(0, EPosition);
                    string expPart = content.Substring(EPosition + 1);

                    if (basePart.Contains('.'))
                    {
                        //小数点位置
                        int dotPosition = basePart.IndexOf('.');
                        //小数位数
                        int decimalLen = basePart.Length - 1 - dotPosition;

                        int expNumber = Convert.ToInt32(expPart);

                        if (decimalLen > expNumber)
                        {
                            TipForm tip = new TipForm(textBox, "不合法的正整数类型科学计数法表达式！");
                            tip.Show();

                            textBox.Text = null;
                        }
                    }
                }
            }
            else
            {
                TipForm tip = new TipForm(textBox, "不合法的正整数数据！");
                tip.Show();

                textBox.Text = null;
            }
        }

        #endregion
        #region 名称验证

        /// <summary>
        /// 名称输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void NamedKeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (e.KeyChar == (char)92 || e.KeyChar == (char)47 || e.KeyChar == (char)58 || e.KeyChar == (char)42 || e.KeyChar == (char)63 || e.KeyChar == (char)34 || e.KeyChar == (char)60 || e.KeyChar == (char)62 || e.KeyChar == (char)124)
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// 重名离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void DuplicationNameLeave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.Text = textBox.Text.Trim();
            if (NameList.Contains(textBox.Text.Trim()))
            {
                //MessageBox.Show("名称重复，请重新命名。");
                TipForm tip = new TipForm(textBox, "名称重复，请重新命名");
                tip.Show();

                textBox.Focus();
            }
        }

        #endregion
        #region 角度验证

        /// <summary>
        /// 角度输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void AngleKeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if ((e.KeyChar > (char)47 && e.KeyChar < (char)58) || e.KeyChar == (char)8 || e.KeyChar == (char)46 || e.KeyChar == (char)45)
            {
                switch (e.KeyChar)
                {
                    //小数点
                    case (char)46:
                        if (textBox.Text.Contains((char)46))
                        {
                            textBox.SelectionStart = textBox.Text.IndexOf((char)46) + 1;
                            textBox.SelectionLength = 0;
                            e.Handled = true;
                        }
                        break;
                    //负号
                    case (char)45:
                        if (!textBox.Text.Contains((char)45))
                        {
                            textBox.Text = "-" + textBox.Text;
                        }
                        textBox.SelectionStart = textBox.Text.IndexOf((char)45) + 1;
                        textBox.SelectionLength = 0;
                        e.Handled = true;
                        break;
                    //数字
                    default:
                        if ((e.KeyChar > (char)47 && e.KeyChar < (char)58))
                        {
                            //add by dwq 2015-06-01 对含有负号的情况特殊处理
                            if (textBox.Text.Contains("-"))
                            {
                                //负号的索引
                                int negIdx = textBox.Text.IndexOf((char)45);

                                //不能再负号前面输入数字
                                if (textBox.SelectionStart + textBox.SelectionLength <= negIdx)
                                {
                                    e.Handled = true;
                                    break;  //不再进行后续处理
                                }
                            }

                            double num = double.Parse(textBox.Text.Remove(textBox.SelectionStart, textBox.SelectionLength).Insert(textBox.SelectionStart, e.KeyChar.ToString()));

                            if (num > 360 || num < -360)
                            {
                                e.Handled = true;
                            }
                            //if (textBox.SelectedText.Contains((char)46) || textBox.SelectedText.Contains((char)45))
                            //{
                            //    e.Handled = true;
                            //}
                        }

                        break;
                }
            }
            else
            {
                TipForm tf = new TipForm(textBox, "请输入合法的角度数据！");
                tf.Show();

                ////add by dwq 2015-05-27 激活父窗体
                //Form frm = UtilClass.SearchOwner(textBox) as Form;
                //frm.Activate();

                textBox.Focus();

                e.Handled = true;
            }
        }
        /// <summary>
        /// 角度离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void AngleLeave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (string.IsNullOrEmpty(textBox.Text))
                return;

            double d = 0;
            if (double.TryParse(textBox.Text, out d))
            {
                textBox.Text = d.ToString();
            }
            else
            {
                TipForm tip = new TipForm(textBox, "不合法的角度数据！");
                tip.Show();

                textBox.Text = null;
            }
        }

        #endregion

        #region DataGridView验证

        private static void DataGridView_EditControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //Console.WriteLine()
            DataGridView grid = sender as DataGridView;

            ////判断每次e.Control是否是同一实例
            //if (LastCtrl != null)
            //{
            //    MessageBox.Show("是否和上次对象相同：\t" + Object.ReferenceEquals(e.Control, LastCtrl));
            //}

            //LastCtrl = e.Control;

            switch (e.Control.GetType().Name)
            {
                case "DataGridViewTextBoxEditingControl":
                    TextBox txt = null;
                    txt = e.Control as TextBox;
                    int cIdx = grid.CurrentCell.ColumnIndex;

                    //获取当前列数据类型限制
                    string type = DataModels.Query(grid, cIdx);
                    if (type != null)
                    {
                        BindDataType(txt, (CheckType)Enum.Parse(typeof(CheckType), type));
                    }
                    break;
                default:

                    break;
            }
        }

        #region 行一致（DataGridView）

        /// <summary>
        /// 行一致离开
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <returns></returns>
        private static bool RowIdentical(DataGridView dataGridView)
        {
            //add by dwq 2015-05-27 先提交最后一次输入DataGridView的数据
            if (dataGridView.IsCurrentCellDirty)
            {
                dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }

            List<object> objListNull = new List<object>();
            List<object> objListNotNull = new List<object>();
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        if (row.Cells[i].Value == null || row.Cells[i].Value.ToString().Trim() == string.Empty)
                        {
                            objListNull.Add(row.Cells[i].Value);
                        }
                        else
                        {
                            objListNotNull.Add(row.Cells[i].Value);
                        }
                    }
                }
                if (objListNull.Count != 0 && objListNotNull.Count != 0)
                {
                    TipForm tip = new TipForm(dataGridView, "请完整填写当前表格整行数据");
                    tip.Show();

                    if (!dataGridView.Parent.Focused)
                    {
                        dataGridView.Parent.Focus();
                    }
                    dataGridView.Focus();

                    //Form frm = UtilClass.SearchOwner(dataGridView) as Form;
                    //frm.Activate();

                    foreach (DataGridViewCell item in row.Cells)
                    {
                        if (item.Value == null)
                        {
                            dataGridView.Focus();
                            dataGridView.CurrentCell = item;
                        }
                    }
                    return false;
                }
            }
            return true;

        }

        private static void RowIdenticalLeave(object sender, EventArgs e)
        {
            DataGridView grid = sender as DataGridView;

            RowIdentical(grid);
        }

        #endregion

        #endregion

        #region 非空验证

        /// <summary>
        /// 非空验证
        /// </summary>
        /// <param name="textBox"></param>
        /// <returns></returns>
        private static bool NotNullOrEmpty(TextBox textBox)
        {
            textBox.Text = textBox.Text.Trim();
            if (textBox.Text == null || textBox.Text.Trim() == string.Empty)
            {
                if (IsCommentShow)
                {
                    MessageBox.Show("未完成文本框输入，请完成数据。");
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 判断按钮对应的控件是否有空值
        /// </summary>
        /// <param name="btnClass">按钮控件实体类</param>
        /// <returns></returns>
        private static bool HasCtrlsNull(ButtonClass btnClass)
        {
            bool flag = false;

            if (btnClass._Controls == null || btnClass._Controls.Count < 0)
            {
                //窗体中所有控件
                Form FormOwner = UtilClass.SearchOwner(btnClass._FrmControl) as Form;
                List<Control> FormControls = UtilClass.InitControls(FormOwner);

                string[] ctrlTmps = btnClass._CtrlStr.Split(',');

                foreach (string item in ctrlTmps)
                {
                    Control FormControl = FormControls.Where(n => n.Name.Equals(item)).FirstOrDefault();

                    if (FormControl != null && FormControl.Name.Equals(item))
                    {
                        ControlClass SubCtrl = new ControlClass();
                        SubCtrl._NameSpace = btnClass._NameSpace;
                        SubCtrl._Class = btnClass._Class;
                        SubCtrl._Name = item;
                        SubCtrl._ControlType = FormControl.GetType().Name;
                        SubCtrl._FrmControl = FormControl;

                        btnClass._Controls.Add(SubCtrl);
                    }
                }
            }

            string tipFailureMsg = "请完整输入必填项信息！";
            string tipSuccessMsg = string.Empty;
            if (null != btnClass._Tips[1])
            {
                tipFailureMsg = btnClass._Tips[1].ToString();
            }
            if (null != btnClass._Tips[0])
            {
                tipSuccessMsg = btnClass._Tips[0].ToString();
            }

            foreach (ControlClass item in btnClass._Controls)
            {
                //Control ctrl = item._FrmControl;
                //获取窗体对应控件
                Control ctrl = item._FrmControl;
                if (ctrl != null)
                {
                    switch (item._ControlType.Trim().ToLower())
                    {
                        case "textbox":
                            TextBox textBox = ctrl as TextBox;
                            if (string.IsNullOrEmpty(textBox.Text))
                            {
                                if (IsCommentShow)
                                {
                                    TipForm tip = new TipForm(btnClass._FrmControl, tipFailureMsg);
                                    tip.Show();

                                    //设置焦点
                                    textBox.Focus();
                                }

                                flag = true;

                                //切换到指定TabPage
                                TabPage tabPage = UtilClass.SearchOwnerTabPage(ctrl) as TabPage;
                                TabControl tabControl = tabPage.Parent as TabControl;
                                if (tabControl.SelectedTab != tabPage)
                                {
                                    tabControl.SelectedTab = tabPage;
                                }
                                break;
                            }
                            break;
                        case "datagridview":
                            DataGridView grid = ctrl as DataGridView;
                            if (grid.Rows.Count <= 0 || grid.NewRowIndex == 0)
                            {
                                if (IsCommentShow)
                                {
                                    TipForm tip = new TipForm(btnClass._FrmControl, tipFailureMsg);
                                    tip.Show();

                                    grid.Focus();
                                }
                                flag = true;

                                //切换到指定TabPage
                                TabPage tabPage = UtilClass.SearchOwnerTabPage(ctrl) as TabPage;
                                TabControl tabControl = tabPage.Parent as TabControl;
                                if (tabControl.SelectedTab != tabPage)
                                {
                                    tabControl.SelectedTab = tabPage;
                                }
                            }

                            //判断必填数据列
                            if (grid.Rows.Count > 0)
                            {
                                int ColCount = grid.Columns.Count;

                                //获取DataGridView必填数据列索引
                                List<int> ColIndex = item._NotNullIndex;

                                foreach (DataGridViewRow row in grid.Rows)
                                {
                                    //忽略新行
                                    if (row.Index == grid.NewRowIndex)
                                        continue;

                                    for (int c = 0; c < ColIndex.Count; c++)
                                    {
                                        if (row.Cells[ColIndex[c]].Value == null || string.IsNullOrEmpty(row.Cells[ColIndex[c]].Value.ToString()))
                                        {
                                            flag = true;

                                            //切换到指定TabPage
                                            TabPage tabPage = UtilClass.SearchOwnerTabPage(ctrl) as TabPage;
                                            TabControl tabControl = tabPage.Parent as TabControl;
                                            if (tabControl.SelectedTab != tabPage)
                                            {
                                                tabControl.SelectedTab = tabPage;
                                            }

                                            TipForm tip = new TipForm(grid, string.Format("DataGridView的第{0}列不能为空！", ColIndex[c] + 1));
                                            tip.Show();

                                            grid.CurrentCell = row.Cells[ColIndex[c]];
                                            grid.BeginEdit(true);

                                            break;
                                        }
                                    }
                                    if (flag)
                                    {
                                        break;  //若一出现空数据，即刻跳出循环
                                    }
                                }
                            }
                            break;
                        default:

                            break;
                    }
                }

                if (flag)
                {
                    ////切换到指定TabPage
                    //TabPage tabPage = UtilClass.SearchOwnerTabPage(ctrl) as TabPage;

                    //TabControl tabControl = tabPage.Parent as TabControl;

                    //if (tabControl.SelectedTab != tabPage)
                    //{
                    //    tabControl.SelectedTab = tabPage;
                    //}

                    break;  //出现没有填内容的直接跳出循环
                }
            }

            if (!flag)
            {
                if (!string.IsNullOrEmpty(tipSuccessMsg))
                {
                    TipForm tip = new TipForm(btnClass._FrmControl, tipSuccessMsg);
                    tip.Show();
                }
            }

            return flag;
        }

        #endregion

        #endregion

        #region 交互方法

        #region 数据验证接口函数

        #region 数据类型验证
        //可进行数据类型验证的控件类型集
        private static string TypeCheckNameFilter = "TextBox,DataGridView,Button";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="type"></param>
        public static void CheckType(object sender, CheckType type)
        {
            string ControlType = sender.GetType().Name;
            if (TypeCheckNameFilter.Contains(ControlType))
            {
                if (ControlType.Equals("TextBox"))
                {
                    TextBox txt = sender as TextBox;
                    switch (type)
                    {
                        case UtilForDataCheck.CheckType.REALNUMBER:

                            break;
                        case UtilForDataCheck.CheckType.INTEGER:

                            break;
                        case UtilForDataCheck.CheckType.ANGLE:

                            break;
                        default:

                            break;
                    }
                }
            }
        }

        public static void CheckType(object sender, Hashtable types)
        {
            string ControlType = sender.GetType().Name;
            if (TypeCheckNameFilter.Contains(ControlType))
            {
                if (ControlType.Equals("DataGridView"))
                {
                    DataGridView grid = sender as DataGridView;

                    foreach (DictionaryEntry item in types)
                    {
                        int ColIndex = -1;
                        if (Int32.TryParse(item.Key.ToString(), out ColIndex))
                        {

                        }
                        else
                        {

                        }
                    }
                }
                else if (ControlType.Equals("Button"))
                {

                }
            }
        }

        #region 实数验证
        /// <summary>
        /// 实数验证
        /// </summary>
        /// <param name="sender">待验证控件实体</param>
        public static void RealNumberCheck(object sender)
        {
            string ControlType = sender.GetType().Name;
            if (TypeCheckNameFilter.Contains(ControlType))
            {
                switch (ControlType)
                {
                    case "TextBox":
                        {
                            TextBox txt = sender as TextBox;

                            txt.KeyPress -= new KeyPressEventHandler(RealNumberKeyPress);
                            txt.KeyPress += new KeyPressEventHandler(RealNumberKeyPress);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// 指定位数长度实数验证
        /// </summary>
        /// <param name="sender">待验证对象</param>
        /// <param name="width">位数</param>
        public static void RealNumberCheck(object sender, int width)
        {

        }

        /// <summary>
        /// 指定可输入范围的实数验证
        /// </summary>
        /// <param name="sender">待验证对象</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        public static void RealNumberCheck(object sender, double min, double max)
        {

        }

        #endregion
        #region 整数验证
        /// <summary>
        /// 整数验证
        /// </summary>
        /// <param name="sender">待验证控件</param>
        public static void IntegerCheck(object sender)
        {
            string ControlType = sender.GetType().Name;
            if (TypeCheckNameFilter.Contains(ControlType))
            {
                switch (ControlType)
                {
                    case "TextBox":
                        {
                            TextBox txt = sender as TextBox;

                            txt.KeyPress -= new KeyPressEventHandler(integerNumberKeyPress);
                            txt.KeyPress += new KeyPressEventHandler(integerNumberKeyPress);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 指定位数宽度的整数验证
        /// </summary>
        /// <param name="sender">待验证对象</param>
        /// <param name="width">位数</param>
        public static void IntegerCheck(object sender, int width)
        {

        }
        /// <summary>
        /// 指定范围的整数验证
        /// </summary>
        /// <param name="sender">待验证对象</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        public static void IntegerCheck(object sender, int min, int max)
        {

        }
        #endregion
        #region 角度验证
        /// <summary>
        /// 角度验证
        /// </summary>
        /// <param name="sender">待验证控件</param>
        public static void AngleCheck(object sender)
        {
            string ControlType = sender.GetType().Name;
            if (TypeCheckNameFilter.Contains(ControlType))
            {
                switch (ControlType)
                {
                    case "TextBox":
                        {
                            TextBox txt = sender as TextBox;

                            txt.KeyPress -= new KeyPressEventHandler(AngleKeyPress);
                            txt.KeyPress += new KeyPressEventHandler(AngleKeyPress);
                        }
                        break;
                    default:
                        break;
                }

            }
        }

        /// <summary>
        /// 带精度的角度验证
        /// </summary>
        /// <param name="sender">待验证控件</param>
        /// <param name="width">精度</param>
        public static void AngleCheck(object sender, int width)
        {

        }

        /// <summary>
        /// 指定范围的角度验证
        /// </summary>
        /// <param name="sender">待验证控件</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        public static void AngleCheck(object sender, double min, double max)
        {

        }
        #endregion
        #endregion

        #region 按钮验证（非空验证）

        #endregion

        #endregion

        /// <summary>
        /// 数据验证接口
        /// </summary>
        /// <param name="ctrl">当前窗体对象</param>
        public static void Check(Control ctrl)
        {
            string _namespace = ctrl.GetType().Namespace;
            string _class = ctrl.GetType().Name;
            string _control = string.Empty;

            ////Xml文件中当前类的所有控件
            //List<BaseCtrl> ctrls = UtilClass.GetAllFromXml(XmlPath, _namespace, _class);
            //窗体中的所有控件
            List<Control> frmCtrls = UtilClass.InitControls(ctrl);

            List<BaseCtrl> InnerCtrls = GetInterSection(_namespace, _class, frmCtrls);

            //筛选过后的数据控件集合
            List<ControlClass> filteredCtrls = GetCtrlClass(InnerCtrls);

            //绑定验证事件
            AddDataTypeCheck(filteredCtrls);
            //AddNNullCheck(filterBtns);

        }

        /// <summary>
        /// 验证必填数据控件的内容是否有空，若无空值，则返回true，否则返回false
        /// </summary>
        /// <param name="ctrl">按钮</param>
        /// <returns></returns>
        public static bool DataValidate(Control ctrl)
        {
            if (!(ctrl is Button))
            {
                if (IsCommentShow)
                {
                    MessageBox.Show("DataValidate方法的参数必须是Button！");
                }
                return false;
            }
            else
            {
                Control frm = UtilClass.SearchOwner(ctrl);
                string _namesapce = frm.GetType().Namespace;
                string _class = frm.Name;

                //获取当前按钮的实体类
                ButtonClass BtnClass = UtilClass.GetAllFromXml(XmlPath, _namesapce, _class, new List<Control> { ctrl })[0] as ButtonClass;

                if (BtnClass != null && BtnClass is ButtonClass)
                {
                    return !(HasCtrlsNull(BtnClass));
                }
                else
                {
                    if (IsCommentShow)
                    {
                        MessageBox.Show("配置文件中没有Name为" + ctrl.Name + "的按钮，请核实后再试！");
                    }
                    return false;
                }
            }
        }

        /// <summary>
        /// 为所有数据控件绑定数据类型验证
        /// </summary>
        ///<param name="ctrls"></param>
        private static void AddDataTypeCheck(List<ControlClass> ctrls)
        {
            //为所有公共控件绑定事件
            foreach (ControlClass item in ctrls)
            {
                //绑定事件
                BindEventHandler(item);
            }
        }

        #region 未使用
        ///// <summary>
        ///// 为所有按钮对应的数据控件绑定非空验证
        ///// </summary>
        ///// <param name="btns"></param>
        //private static void AddNNullCheck(List<ButtonClass> btns)
        //{
        //    //TODO:待修改
        //    foreach (ButtonClass item in btns)
        //    {
        //        BindEventHandler(item);
        //    }
        //}

        ///// <summary>
        ///// 生成Xml文件
        ///// </summary>
        //public static void GeneateXml()
        //{
        //    string _xmlpath = UtilClass.GenerateXml();

        //    if (!string.IsNullOrEmpty(_xmlpath))
        //    {
        //        XmlHelper xmlhelper = new XmlHelper();

        //        Hashtable attr = new Hashtable();
        //        xmlhelper.InsertNode(_xmlpath, "namespace1", false, "//parameters//controls", null, null);

        //        attr.Clear();
        //        attr.Add("namespace", "namespace1");
        //        xmlhelper.InsertNode(_xmlpath, "class1", true, "//parameters//controls//namespace1", attr, null);
        //        attr.Add("class", "class1");
        //        attr.Add("controltype", "TextBox");
        //        attr.Add("datatype", "type1");
        //        xmlhelper.InsertNode(_xmlpath, "ctrl1", true, "//parameters//controls//namespace1//class1", attr, null);

        //        MessageBox.Show("Xml文件已经创建，路径为：" + _xmlpath);
        //    }
        //}
        #endregion

        #region 已删除

        ///// <summary>
        ///// 为指定窗体的指定名称的控件绑定验证事件
        ///// </summary>
        ///// <param name="obj">窗体对象</param>
        ///// <param name="name">控件名称</param>
        ///// <param name="type">验证类型</param>
        //public static void BindDataCheck(object obj, string name, HandlerType type)
        //{
        //    Control ctrl = GetControl(obj, name);
        //    if (ctrl == null)
        //    {
        //        MessageBox.Show("获取指定控件失败！");
        //        return;
        //    }

        //    //绑定验证事件
        //    BindEventHandler(ctrl, type);
        //}

        ///// <summary>
        ///// 获取指定窗体，指定名称控件
        ///// </summary>
        ///// <param name="obj">窗体对象</param>
        ///// <param name="name">控件名称</param>
        ///// <returns></returns>
        //private static Control GetControl(object obj, string name)
        //{
        //    UtilClass.InitControls(obj);

        //    return UtilClass.Search(name);
        //}

        #endregion

        /// <summary>
        /// 为指定源绑定指定事件
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="handler"></param>
        private static void BindEventHandler(BaseCtrl ctrl)
        {
            //TODO:区分不同的控件类型绑定
            //BaseCtrl ctrlClass = UtilClass.GetControlFromXml(XmlPath, ctrl);

            if (ctrl == null || ctrl._FrmControl == null)
            {
                if (IsCommentShow)
                {
                    MessageBox.Show("没有找到名为" + ctrl._Name + "的控件信息！");
                }

                return;
            }

            //控件类型
            string ctrlType = ctrl._ControlType.ToLower().Trim();
            if (ctrlType == "textbox" || ctrlType == "datagridview")
            {
                Control frmCtrl = ctrl._FrmControl;
                ControlClass ctrlClass = ctrl as ControlClass;

                switch (ctrlType)
                {
                    case "textbox":
                        AttrTextBox attr = ctrlClass._Attr as AttrTextBox;
                        CheckType type = (CheckType)Enum.Parse(typeof(CheckType), attr.Attr.DataType);
                        EventType etype = (EventType)Enum.Parse(typeof(EventType), ctrlClass._EventType);
                        BindDataType(frmCtrl, type, etype);
                        break;
                    case "datagridview":
                        //对DataGridView需区分（是对整体，还是对每列的数据验证）

                        DataGridView grid = frmCtrl as DataGridView;

                        //是否具有列数据限制
                        bool flag = false;

                        //存储DataGridView各列数据信息
                        flag = DataModels.Store(ctrlClass);

                        if (flag)
                        {
                            grid.EditingControlShowing -= new DataGridViewEditingControlShowingEventHandler(DataCheck.DataGridView_EditControlShowing);
                            grid.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(DataCheck.DataGridView_EditControlShowing);
                        }

                        AttrDataGridView a = ctrlClass._Attr as AttrDataGridView;
                        ////如果含有一致性检测信息
                        //var querylist = from p in a.Attr
                        //                where p.Index == -1 || p.DataType.Equals("CONSISTENCY")
                        //                select p;
                        //if (querylist != null && querylist.Count() > 0)
                        //{
                        //    BindDataType(frmCtrl, CheckType.CONSISTENCY);
                        //}

                        break;
                    default:

                        break;
                }
            }
        }

        //add by dwq 2015-05-28 为已确定类型的控件绑定数据类型验证
        private static void BindDataType(Control ctrl, CheckType type, EventType etype = EventType.TEXTCHANGE)
        {
            //对于禁用的控件不绑定
            if (ctrl.Enabled == false)
                return;

            ctrl.KeyPress -= new KeyPressEventHandler(DataCheck.RealNumberWithSciKeyPress);
            ctrl.Leave -= new EventHandler(DataCheck.RealNumberWithSciLeave);
            ctrl.KeyPress -= new KeyPressEventHandler(DataCheck.IntegerNumberWithSciKeyPress);
            ctrl.Leave -= new EventHandler(DataCheck.IntegerNumberWithSciLeave);
            ctrl.KeyPress -= new KeyPressEventHandler(DataCheck.AngleKeyPress);
            ctrl.Leave -= new EventHandler(DataCheck.AngleLeave);
            //ctrl.Leave -= new EventHandler(DataCheck.RowIdenticalLeave);

            switch (type)
            {
                case UtilForDataCheck.CheckType.REALNUMBER:
                    //对于TextBox
                    if (EventType.TEXTCHANGE == etype)
                    {
                        ctrl.KeyPress += new KeyPressEventHandler(DataCheck.RealNumberWithSciKeyPress);
                        ctrl.Leave += new EventHandler(DataCheck.RealNumberWithSciLeave);
                    }
                    else if (EventType.LEAVE == etype)
                    {
                        ctrl.Leave += new EventHandler(DataCheck.RealNumberWithSciLeave);
                    }
                    break;
                case UtilForDataCheck.CheckType.INTEGER:
                    //对TextBox
                    if (EventType.TEXTCHANGE == etype)
                    {
                        ctrl.KeyPress += new KeyPressEventHandler(DataCheck.IntegerNumberWithSciKeyPress);
                        ctrl.Leave += new EventHandler(DataCheck.IntegerNumberWithSciLeave);
                    }
                    else if (EventType.LEAVE == etype)
                    {
                        ctrl.Leave += new EventHandler(DataCheck.IntegerNumberWithSciLeave);
                    }
                    break;
                case UtilForDataCheck.CheckType.ANGLE:
                    //对TextBox
                    if (EventType.TEXTCHANGE == etype)
                    {
                        ctrl.KeyPress += new KeyPressEventHandler(DataCheck.AngleKeyPress);
                    }
                    else if (EventType.LEAVE == etype)
                    {
                        ctrl.Leave += new EventHandler(DataCheck.AngleLeave);
                    }
                    break;
                //case UtilForDataCheck.CheckType.CONSISTENCY:
                //    //对DataGridView
                //    if (!(ctrl is DataGridView))
                //        return;
                //    ctrl.Leave += new EventHandler(DataCheck.RowIdenticalLeave);
                //    break;
                //case HandlerType.NOTNULL:
                //    //对TextBox等

                //    break;
                default:

                    break;
            }
        }
        /// <summary>
        /// 获取Xml中当前窗体中的控件
        /// </summary>
        /// <param name="_NameSpace">命名空间名</param>
        /// <param name="_Class">类名</param>
        /// <param name="frmCtrls">当前窗体中控件集合</param>
        /// <param name="xmlCtrls">XML中控件集合</param>
        /// <returns></returns>
        private static List<BaseCtrl> GetInterSection(string _NameSpace, string _Class, List<Control> frmCtrls, List<BaseCtrl> xmlCtrls)
        {
            //进行筛选，（同时，将窗体控件赋值给数据模型）
            List<BaseCtrl> ctrls = new List<BaseCtrl>();

            //筛选Xml和窗体集合中公有的控件
            foreach (Control item in frmCtrls)
            {
                BaseCtrl tmp = xmlCtrls.Where(n => n._Name.Equals(item.Name) && n._Class.Equals(_Class) && n._NameSpace.Equals(_NameSpace)).FirstOrDefault();

                if (tmp != null)
                {
                    switch (item.GetType().Name)
                    {
                        case "TextBox":
                            tmp._ControlType = "TextBox";
                            break;
                        case "DataGridView":
                            tmp._ControlType = "DataGridView";
                            break;
                        case "Button":
                            tmp._ControlType = "Button";
                            break;
                        default:
                            tmp._ControlType = "UnKnown";
                            break;
                    }

                    tmp._FrmControl = item;
                    ctrls.Add(tmp);
                }
            }

            return ctrls;
        }

        /// <summary>
        /// 获取Xml中当前窗体中的控件
        /// </summary>
        /// <param name="_NameSpace">命名空间名</param>
        /// <param name="_Class">类名</param>
        /// <param name="frmCtrls">当前窗体中控件集合</param>
        /// <returns></returns>
        private static List<BaseCtrl> GetInterSection(string _NameSpace, string _Class, List<Control> frmCtrls)
        {
            ////进行筛选，（同时，将窗体控件赋值给数据模型）
            //List<BaseCtrl> xmlCtrls = UtilClass.GetAllFromXml(XmlPath, _NameSpace, _Class, frmCtrls);

            //List<BaseCtrl> ctrls = new List<BaseCtrl>();
            ////筛选Xml和窗体集合中公有的控件
            //foreach (Control item in frmCtrls)
            //{
            //    BaseCtrl tmp = xmlCtrls.Where(n => n._Name.Equals(item.Name) && n._Class.Equals(_Class) && n._NameSpace.Equals(_NameSpace)).FirstOrDefault();

            //    if (tmp != null)
            //    {
            //        switch (item.GetType().Name)
            //        {
            //            case "TextBox":
            //                tmp._ControlType = "TextBox";
            //                break;
            //            case "DataGridView":
            //                tmp._ControlType = "DataGridView";
            //                break;
            //            case "Button":
            //                tmp._ControlType = "Button";
            //                break;
            //            default:
            //                tmp._ControlType = "UnKnown";
            //                break;
            //        }

            //        tmp._FrmControl = item;
            //        ctrls.Add(tmp);
            //    }
            //}

            //return ctrls;


            //获取窗体与xml公有的控件集合
            return UtilClass.GetAllFromXml(XmlPath, _NameSpace, _Class, frmCtrls);
        }

        /// <summary>
        /// 获取Button对应的实体类
        /// </summary>
        /// <param name="_NameSpace">命名空间名</param>
        /// <param name="_Class">类名</param>
        /// <param name="frmCtrl">控件</param>
        /// <returns></returns>
        private static BaseCtrl GetInterSection(string _NameSpace, string _Class, Control frmCtrl)
        {
            //按钮对应的实体类
            ButtonClass BtnClass = UtilClass.GetAllFromXml(XmlPath, _NameSpace, _Class, new List<Control> { frmCtrl })[0] as ButtonClass;

            return BtnClass;
        }

        /// <summary>
        /// 获取交集中的数据控件集合
        /// </summary>
        /// <param name="baseCtrls">交集</param>
        /// <returns></returns>
        private static List<ControlClass> GetCtrlClass(List<BaseCtrl> baseCtrls)
        {
            //获取交集中的数据控件集合，（同时，将窗体控件赋值给数据模型）
            List<ControlClass> ctrls = new List<ControlClass>();

            foreach (BaseCtrl item in baseCtrls)
            {
                if (item is ControlClass)
                {
                    ControlClass cTemp = item as ControlClass;
                    if (cTemp != null)
                    {
                        ctrls.Add(cTemp);
                    }
                }
            }

            return ctrls;
        }

        ///// <summary>
        ///// 获取交集中的按钮集合
        ///// </summary>
        ///// <param name="baseCtrls"></param>
        ///// <returns></returns>
        //private static List<ButtonClass> GetBtnClass(List<BaseCtrl> baseCtrls)
        //{
        //    //获取交集中的按钮集合，（同时，将窗体控件赋值给数据模型）
        //    List<ButtonClass> btns = new List<ButtonClass>();

        //    foreach (BaseCtrl item in baseCtrls)
        //    {
        //        if (item is ButtonClass)
        //        {
        //            ButtonClass bTemp = item as ButtonClass;
        //            if (bTemp != null)
        //            {
        //                btns.Add(bTemp);
        //            }
        //        }
        //    }

        //    return btns;
        //}

        #endregion
    }

    /// <summary>
    /// 用于存储存储DataCheck的数据
    /// </summary>
    public class DataModels
    {
        #region 数据

        //命名空间集合
        //（储存DataGridView各列数据类型，为唯一表示，将其分为四层，命名空间名-类名-控件名-列数据类型）
        private static Hashtable nTable = new Hashtable();
        ////当前控件
        //private static ControlClass currentCtrl;
        #endregion

        #region 方法

        /// <summary>
        /// 存储控件信息
        /// </summary>
        /// <param name="_namespace"></param>
        /// <param name="_class"></param>
        /// <param name="_control"></param>
        /// <param name="_index"></param>
        /// <param name="_type"></param>
        public static bool Store(string _namespace, string _class, string _control, int _index, string _type)
        {
            //分别对应类集合，控件集合，类型集合
            Hashtable cTable, ctrlTable, typeTable;

            //如果包含当前控件信息，则更新对应数据（先删除，后添加）
            //否则，新建控件信息
            if (nTable.ContainsKey(_namespace))
            {
                cTable = nTable[_namespace] as Hashtable;
                if (cTable.ContainsKey(_class))
                {
                    ctrlTable = cTable[_class] as Hashtable;
                    if (ctrlTable.ContainsKey(_control))
                    {
                        typeTable = ctrlTable[_control] as Hashtable;
                        if (typeTable.ContainsKey(_index))
                        {
                            //删除原始记录
                            typeTable.Remove(_index);
                        }

                        //新建当前信息
                        typeTable.Add(_index, _type);
                    }
                    else  //控件集合中不存在当前控件
                    {
                        typeTable = new Hashtable();
                        typeTable.Add(_index, _type);

                        ctrlTable.Add(_control, typeTable);
                    }
                }
                else  //类集合中不存在当前类
                {
                    typeTable = new Hashtable();
                    typeTable.Add(_index, _type);

                    ctrlTable = new Hashtable();
                    ctrlTable.Add(_control, typeTable);

                    cTable.Add(_class, ctrlTable);
                }
            }
            else  //命名空间集合中不存在当前命名空间
            {
                typeTable = new Hashtable();
                typeTable.Add(_index, _type);

                ctrlTable = new Hashtable();
                ctrlTable.Add(_control, typeTable);

                cTable = new Hashtable();
                cTable.Add(_class, ctrlTable);

                nTable.Add(_namespace, cTable);
            }

            return true;
        }

        /// <summary>
        /// 存储控件信息
        /// </summary>
        /// <param name="_ctrl"></param>
        public static bool Store(ControlClass _ctrl)
        {
            //是否对DataGridView具有列数据限制
            bool flag = false;

            if (_ctrl == null)
            {
                return false;
            }

            string _namespace = _ctrl._NameSpace;
            string _class = _ctrl._Class;
            string _control = _ctrl._Name;
            int _index = 0;
            string _type = string.Empty;

            switch (_ctrl._ControlType)
            {
                case "TextBox":
                    //当前不存储TextBox数据类型
                    //AttrTextBox attr = _ctrl._Attr as AttrTextBox;
                    //_index = attr.Attr.Index;
                    //_type = attr.Attr.DataType;
                    //Store(_namespace, _class, _control, _index, _type);
                    flag = false;
                    break;
                case "DataGridView":
                    AttrDataGridView attr = _ctrl._Attr as AttrDataGridView;

                    for (int i = 0; i < attr.Attr.Count; i++)
                    {
                        _index = attr.Attr[i].Index;
                        _type = attr.Attr[i].DataType;

                        //忽略一致性检测
                        if (_index == -1 || _type.Equals("CONSISTENCY"))
                            continue;

                        //具有列数据限制
                        flag = Store(_namespace, _class, _control, _index, _type);
                    }
                    break;
                default:
                    flag = false;
                    break;
            }

            return flag;

        }

        /// <summary>
        /// 获取控件数据信息
        /// </summary>
        /// <param name="_namespace">命名空间名</param>
        /// <param name="_class">类名</param>
        /// <param name="_control">控件名</param>
        /// <param name="_index">列索引</param>
        /// <returns></returns>
        public static string Query(string _namespace, string _class, string _control, int _index)
        {
            //通过四次哈希，查找类型信息

            string type = string.Empty;

            Hashtable cTable = nTable[_namespace] as Hashtable;
            Hashtable ctrlTable = cTable[_class] as Hashtable;
            Hashtable typeTable = ctrlTable[_control] as Hashtable;

            if (cTable != null && ctrlTable != null && typeTable != null)
            {
                if (typeTable.ContainsKey(_index))
                {
                    type = typeTable[_index].ToString();
                }
                else
                {
                    type = null;
                }
            }
            else
            {
                type = null;
            }

            return type;
        }

        /// <summary>
        /// 查询数据类型
        /// </summary>
        /// <param name="_ctrl">指定控件实体类</param>
        /// <param name="_index">列索引</param>
        /// <returns></returns>
        public static string Query(ControlClass _ctrl, int _index)
        {
            string _namespace = _ctrl._NameSpace;
            string _class = _ctrl._Class;
            string _control = _ctrl._Name;

            return Query(_namespace, _class, _control, _index);
        }

        /// <summary>
        /// 通过控件查询其数据类型
        /// </summary>
        /// <param name="ctrl">指定控件</param>
        /// <param name="_index">列索引</param>
        /// <returns></returns>
        public static string Query(Control ctrl, int _index)
        {
            Control Form = UtilClass.SearchOwner(ctrl);

            string _namespace = Form.GetType().Namespace;
            string _class = Form.GetType().Name;
            string _control = ctrl.Name;

            return Query(_namespace, _class, _control, _index);
        }

        #endregion
    }
}
