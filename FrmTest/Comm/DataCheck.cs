using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrmTest.Comm
{
    /// <summary>
    /// 数据验证类
    /// </summary>
    public class DataCheck
    {
        #region 数据验证

        #region 变量

        /// <summary>
        /// 名称集合
        /// </summary>
        public List<string> NameList = new List<string>();

        #endregion

        #region 实数验证

        /// <summary>
        /// 实数输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RealNumberKeyPress(object sender, KeyPressEventArgs e)
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
                            if (textBox.SelectedText.Contains((char)46) || textBox.SelectedText.Contains((char)45))
                            {
                                e.Handled = true;
                            }
                        }
                        break;
                }
            }
            else
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// （正）实数离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RealNumberLeave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            double d = 0;
            if (double.TryParse(textBox.Text, out d))
            {
                textBox.Text = d.ToString();
            }
            else
            {
                textBox.Text = null;
            }
        }
        /// <summary>
        /// 正实数输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PositiveRealNumberKeyPress(object sender, KeyPressEventArgs e)
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
                e.Handled = true;
            }
        }

        #endregion
        #region 整数验证

        /// <summary>
        /// 整数输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void integerNumberKeyPress(object sender, KeyPressEventArgs e)
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
                            if (textBox.SelectedText.Contains((char)46) || textBox.SelectedText.Contains((char)45))
                            {
                                e.Handled = true;
                            }
                        }
                        break;
                }
            }
            else
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// （正）整数离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void integerNumberLeave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            int d = 0;
            if (int.TryParse(textBox.Text, out d))
            {
                textBox.Text = d.ToString();
            }
            else
            {
                textBox.Text = null;
            }
        }
        /// <summary>
        /// 正整数输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PositiveintegerNumberKeyPress(object sender, KeyPressEventArgs e)
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
                e.Handled = true;
            }
        }

        #endregion
        #region 名称验证

        /// <summary>
        /// 名称输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NamedKeyPress(object sender, KeyPressEventArgs e)
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
        public void DuplicationNameLeave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.Text = textBox.Text.Trim();
            if (NameList.Contains(textBox.Text.Trim()))
            {
                MessageBox.Show("名称重复，请重新命名。");
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
        public void AngleKeyPress(object sender, KeyPressEventArgs e)
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
                            double num = double.Parse(textBox.Text.Remove(textBox.SelectionStart, textBox.SelectionLength).Insert(textBox.SelectionStart, e.KeyChar.ToString()));

                            if (num > 360 || num < -360)
                            {
                                e.Handled = true;
                            }
                            if (textBox.SelectedText.Contains((char)46) || textBox.SelectedText.Contains((char)45))
                            {
                                e.Handled = true;
                            }
                        }

                        break;
                }
            }
            else
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// 角度离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AngleLeave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            double d = 0;
            if (double.TryParse(textBox.Text, out d))
            {
                textBox.Text = d.ToString();
            }
            else
            {
                textBox.Text = null;
            }
        }

        #endregion

        #region 行一致（DataGridView）

        /// <summary>
        /// 行一致离开
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <returns></returns>
        public bool RowIdentical(DataGridView dataGridView)
        {
            //dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
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
                    MessageBox.Show("未完成数据表格数据，请完成数据或清空。");
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

        #endregion

        #region 非空验证

        /// <summary>
        /// 非空验证
        /// </summary>
        /// <param name="textBox"></param>
        /// <returns></returns>
        public bool NotNullOrEmpty(TextBox textBox)
        {
            textBox.Text = textBox.Text.Trim();
            if (textBox.Text == null || textBox.Text.Trim() == string.Empty)
            {
                MessageBox.Show("未完成文本框输入，请完成数据。");
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #endregion

        #region 交互方法



        #endregion
    }
}
