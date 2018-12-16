using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practice_3_Notepad
{
    public partial class Form1 : Form
    {

        private string fn = "无标题";
        private StringReader myReader;

        public Form1()
        {
            InitializeComponent();
        }

        //设置快捷键
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            //一组快捷键的标准设置  Ctrl+T
            //if (e.KeyCode == Keys.T && e.Control)
            //{ 
            //    button1.PerformClick(); //执行单击button1的动作  
            //}
            //if (e.KeyCode == Keys.T && e.Control)
            //{
            //    e.Handled = true;       //将Handled设置为true，指示已经处理过KeyDown事件   
            //    button1.PerformClick(); //执行单击button1的动作   
            //}

            //文件部分的快捷键
           //  打开 Ctrl + O
            if (e.KeyCode == Keys.O && e.Control)
            {
                e.Handled = true;
                打开ToolStripMenuItem.PerformClick();
                Console.WriteLine("按下文件-打开按键");
            }

            //  打开 Ctrl + N
            if (e.KeyCode == Keys.N && e.Control)
            {
                e.Handled = true;
                新建ToolStripMenuItem.PerformClick();
                Console.WriteLine("按下文件-新建按键");
            }

            //  保存 Ctrl + S
            if (e.KeyCode == Keys.S && e.Control)
            {
                e.Handled = true;
                保存ToolStripMenuItem.PerformClick();
                Console.WriteLine("按下文件-保存按键");
            }


            // 退出 Ctrl + X
            if (e.KeyCode == Keys.X && e.Control)
            {
                e.Handled = true;
                退出ToolStripMenuItem.PerformClick();
                Console.WriteLine("按下文件-退出按键");
            }

        }



        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamWriter sw;
            try
            {
                if (textBox1.Modified)
                {
                    DialogResult result = MessageBox.Show("文本发生了改变, 要保存吗？", "注意", MessageBoxButtons.YesNoCancel);
                    if (result == DialogResult.Yes)
                    {
                        if (fn == "无标题")
                        {
                            saveFileDialog1.Filter = @"李纯辉格式(*.lch)|*.lch|文本文档(*.txt)|*.txt|所有格式|*.txt; *.cs;*.lch";
                            saveFileDialog1.FilterIndex = 2;
                            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                                fn = saveFileDialog1.FileName;
                        }
                        sw = new StreamWriter(fn, false, System.Text.Encoding.Default);
                        sw.Write(textBox1.Text);
                        sw.Flush();
                        sw.Close();
                        textBox1.Clear();
                        textBox1.Modified = false;
                        this.Text = "无标题 - 记事本";
                        saveFileDialog1.FileName = fn = "无标题";
                    }
                    else if (result == DialogResult.No)
                    {
                        textBox1.Clear();
                        textBox1.Modified = false;
                        this.Text = "无标题 - 记事本";
                        saveFileDialog1.FileName = fn = "无标题";
                    }
                }
                else
                {
                    textBox1.Clear();
                    textBox1.Modified = false;
                    this.Text = "无标题 - 记事本";
                    saveFileDialog1.FileName = fn = "无标题";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("文件无法打开或读取。请确认文件名称是否正确，以及您是否有读取权限。\n异常：" + ex.Message);
            }
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;
            StreamReader sr;
            StreamWriter sw;
            try
            {
                if (textBox1.Modified)
                {
                    result = MessageBox.Show("文件 " + fn + " 的文字已经改变。\r\n\r\n想保存文件吗？", "记事本", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                    if (result == DialogResult.Cancel)
                        return;
                    if (result == DialogResult.Yes)
                    {
                        saveFileDialog1.Filter = @"李纯辉格式(*.lch)|*.lch|文本文档(*.txt)|*.txt|所有格式|*.txt; *.cs;*.lch";
                        saveFileDialog1.FilterIndex = 2;
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            fn = saveFileDialog1.FileName;
                            sw = new StreamWriter(fn, false, System.Text.Encoding.Default);
                            sw.Write(textBox1.Text);
                            this.Text = Path.GetFileName(fn) + " - 记事本";
                            textBox1.Modified = false;
                            sw.Flush();
                            sw.Close();
                        }
                        else
                            return;
                    }
                }

                openFileDialog1.Filter = @"李纯辉格式(*.lch)|*.lch|文本文档(*.txt)|*.txt|所有格式|*.txt; *.cs;*.lch";
                openFileDialog1.FilterIndex = 2;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fn = openFileDialog1.FileName;
                    sr = new StreamReader(fn, System.Text.Encoding.Default);
                    textBox1.Text = sr.ReadToEnd();
                    textBox1.Modified = false;
                    sr.Close();
                    //this.Text = Path.GetFileName(fn) + " - 记事本";
                    this.Text = openFileDialog1.SafeFileName + " - 记事本";
                    saveFileDialog1.FileName = openFileDialog1.FileName;
                }
            }
            catch { }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamWriter sw;
            try
            {
                if (textBox1.Modified)
                {
                    if (fn == "无标题")
                    {
                        saveFileDialog1.Filter = @"李纯辉格式(*.lch)|*.lch|文本文档(*.txt)|*.txt|所有格式|*.txt; *.cs;*.lch";
                        saveFileDialog1.FilterIndex = 2;
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                            fn = saveFileDialog1.FileName;
                    }
                    sw = new StreamWriter(fn, false, System.Text.Encoding.Default);
                    sw.Write(textBox1.Text);
                    textBox1.Modified = false;
                    sw.Flush();
                    sw.Close();
                    this.Text = Path.GetFileName(fn) + " - 记事本";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("文件无法打开或读取。请确认文件名称是否正确，以及您是否有读取权限。\n异常：" + ex.Message);
            }
        }

        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != string.Empty)
                {
                    saveFileDialog1.Filter = @"李纯辉格式(*.lch)|*.lch|文本文档(*.txt)|*.txt|所有格式|*.txt; *.cs;*.lch";
                    saveFileDialog1.FilterIndex = 2;
                    if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        fn = saveFileDialog1.FileName;
                        StreamWriter sw = new StreamWriter(fn, false, System.Text.Encoding.Default);
                        sw.Write(textBox1.Text);
                        textBox1.Modified = false;
                        sw.Flush();
                        sw.Close();
                        this.Text = Path.GetFileName(fn) + " - 记事本";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("文件无法打开或读取。请确认文件名称是否正确，以及您是否有读取权限。\n异常：" + ex.Message);
            }
        }

        private void 页面设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                pageSetupDialog1.Document = printDocument1;
                //pageSetupDialog1.Document.DefaultPageSettings.Color = false;
                pageSetupDialog1.ShowDialog();
            }
            catch { }
        }

        private void 打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                printDialog1.Document = printDocument1;
                myReader = new StringReader(textBox1.Text);

                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "打印出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                printDocument1.PrintController.OnEndPrint(printDocument1, new System.Drawing.Printing.PrintEventArgs());
            }
            finally
            {
                myReader.Close();
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        //编辑的菜单
        private void 撤销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.CanUndo)
            {
                textBox1.Undo();
            }
        }

        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectedText != "")
            {
                textBox1.Cut();
            }
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectedText != "")
            {
                textBox1.Copy();
            }
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Paste();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = "";
        }

        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
                textBox1.SelectAll();
        }


        //格式
        private void 自动换行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScrollBars Vertical = default(ScrollBars);
            textBox1.ScrollBars= Vertical;
        }

        private void 字体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            if (fontDialog1.Font != null)
            {
                textBox1.Font = fontDialog1.Font;
            }
        }
       

        private void 关于笔记本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("作者：李纯辉，联系方式：435298943@qq.com","关于");
        }

        private void 查看帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("请参看Windows系统之下的记事本。\n" +
                "关于李纯辉格式，可以帮你加密一些你不想让别人看到的信息！", "帮助");
        }

        private void Form1_Leave(object sender, EventArgs e)
        {
            StreamWriter sw;
            try
            {
                if (textBox1.Modified && textBox1.Text != string.Empty)
                {
                    if (fn == "无标题")
                    {
                        saveFileDialog1.Filter = @"文本文档(*.txt)|*.txt|所有格式|*.txt; *.cs";
                        saveFileDialog1.FilterIndex = 2;
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                            fn = saveFileDialog1.FileName;
                    }
                    sw = new StreamWriter(fn, false, System.Text.Encoding.Default);
                    sw.Write(textBox1.Text);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("文件无法打开或读取。请确认文件名称是否正确，以及您是否有读取权限。\n异常：" + ex.Message);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StreamWriter sw;
            try
            {
                if (textBox1.Modified && textBox1.Text != string.Empty)
                {
                    if (fn == "无标题")
                    {
                        saveFileDialog1.Filter = @"文本文档(*.txt)|*.txt|所有格式|*.txt; *.cs";
                        saveFileDialog1.FilterIndex = 2;
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                            fn = saveFileDialog1.FileName;
                    }
                    sw = new StreamWriter(fn, false, System.Text.Encoding.Default);
                    sw.Write(textBox1.Text);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("文件无法打开或读取。请确认文件名称是否正确，以及您是否有读取权限。\n异常：" + ex.Message);
            }
        }
    }

}
