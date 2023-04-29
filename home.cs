using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Data.SqlClient;

namespace bookMS
{
    public partial class home : Form
    {
        private float x, y;
        private string UIDFromOwner;
        private string mycardno;
        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)//循环窗体中的控件
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                    setTag(con);
            }
        }
        private void setControls(float newx, float newy, Control cons)
        {
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
            {

                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });//获取控件的Tag属性值，并分割后存储字符串数组
                float a = System.Convert.ToSingle(mytag[0]) * newx;//根据窗体缩放比例确定控件的值，宽度
                con.Width = (int)a;//宽度
                a = System.Convert.ToSingle(mytag[1]) * newy;//高度
                con.Height = (int)(a);
                a = System.Convert.ToSingle(mytag[2]) * newx;//左边距离
                con.Left = (int)(a);
                a = System.Convert.ToSingle(mytag[3]) * newy;//上边缘距离
                con.Top = (int)(a);
                Single currentSize = System.Convert.ToSingle(mytag[4]) * newy;//字体大小
                con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                if (con.Controls.Count > 0)
                {
                    setControls(newx, newy, con);
                }
            }
        }
        public home(string str)
        {
            InitializeComponent();
            UIDFromOwner = str;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Owner.Show();
        }

        private void home_Load(object sender, EventArgs e)
        {
            x = this.Width;
            y = this.Height;
            setTag(this);
            string sql = $"select * from card where UID = '{UIDFromOwner}'";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            if (objReader.Read())
            {
                mycardno = objReader[0].ToString();
                textBox10.Text = mycardno;
            }
            else
            {
                mycardno = "";
                textBox10.Text = "(无)";
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            string sql = $"select * from books where no like '%{textBox1.Text}%' and type like '%{textBox2.Text}%' and bookname like '%{textBox3.Text}%' and author like '%{textBox4.Text}%'";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            while (objReader.Read())
            {
                dataGridView1.Rows.Add(objReader[0].ToString(), objReader[1].ToString(), objReader[2].ToString(),
                    objReader[3].ToString(), objReader[4].ToString());

            }
        }

        private void freshBooks()
        {
            dataGridView1.Rows.Clear();
            string sql = "select * from books order by no";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            while (objReader.Read())
            {
                dataGridView1.Rows.Add(objReader[0].ToString(), objReader[1].ToString(), objReader[2].ToString(),
                    objReader[3].ToString(), objReader[4].ToString());

            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Hide();
            dataGridView1.Show();
            freshBooks();
        }


        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Hide();
            dataGridView3.Hide();
        }

        private void freshRecord()
        {
            dataGridView3.Rows.Clear();
            if (mycardno == "")
            {
                return;
            }
            string sql = $"select * from record where cardno = {mycardno} order by cardno";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            while (objReader.Read())
            {
                dataGridView3.Rows.Add(objReader[1].ToString(), objReader[2].ToString(), objReader[3].ToString(),
                    objReader[4].ToString());

            }
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Hide();
            dataGridView3.Show();
            freshRecord();
        }

        private void fresh_2()
        {
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            string sql = $"select * from books where no = {textBox5.Text}";
            try
            {
                SqlDataReader objReader = SQLHelper.GetReader(sql);
                if (objReader.Read())
                {
                    textBox6.Text = objReader[1].ToString();
                    textBox7.Text = objReader[2].ToString();
                    textBox8.Text = objReader[3].ToString();
                    textBox9.Text = objReader[4].ToString();
                }
            }
            catch (Exception)
            {
                return;
            }

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            fresh_2();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox10.Text == "(无)")
            {
                MessageBox.Show("没有注册借书证! 请联系管理员");
                return;
            }
            if (textBox6.Text == "")
            {
                return;
            }
            if (textBox9.Text == "0")
            {
                MessageBox.Show("借出失败, 库存不足");
            }

            string sql = "";
            string cardno = "";
            if (textBox10.Text == "")
            {
                //sql = $"select * from card where UID = '{UIDFromOwner}'";
                //SqlDataReader objReader = SQLHelper.GetReader(sql);
                //if (objReader.Read())
                //{
                //    cardno = objReader[0].ToString();
                //}
                MessageBox.Show("请输入借书证号");
            }
            else
            {
                sql = $"select * from card where cardno = {textBox10.Text}";
                if (SQLHelper.GetReader(sql).Read())
                {
                    cardno = textBox10.Text;
                }
                else
                {
                    MessageBox.Show("请输入正确的借书证号");
                    return;
                }
            }
            
            sql = $"update books set remain = remain-1 where no = {textBox5.Text}";
            SQLHelper.Update(sql);
            sql = $"insert into record(cardno, bookname, borrowdate) values ({cardno},'{textBox7.Text}',CONVERT(varchar,GETDATE(),120) )";
            SQLHelper.Update(sql);
            fresh_2();
            freshRecord();
            freshBooks();
            MessageBox.Show("借出成功!");
        }

        private void home_Resize(object sender, EventArgs e)
        {
            float _X = (this.Width) / x;
            float _y = (this.Height) / y;
            setControls(_X, _y, this);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Owner.Close();
        }


        private void home_Load_1(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox10.Text == "(无)")
            {
                MessageBox.Show("没有注册借书证! 请联系管理员");
                return;
            }
            string sql = "";
            string cardno = "";
            if (textBox10.Text == "")
            {
                MessageBox.Show("请输入借书证号");
            }
            else
            {
                sql = $"select * from card where cardno = {textBox10.Text}";
                if (SQLHelper.GetReader(sql).Read())
                {
                    cardno = textBox10.Text;
                }
                else
                {
                    MessageBox.Show("请输入正确的借书证号");
                    return;
                }
                
            }
            sql = $"update record set returndate = CONVERT(varchar,GETDATE(),120) where cardno = {cardno} and returndate is null";
            int influenced = SQLHelper.Update(sql);
            sql = $"update books set remain = remain + {influenced} where no = {textBox5.Text}";
            SQLHelper.Update(sql);
            fresh_2();
            freshRecord();
            freshBooks();
            MessageBox.Show($"归还{influenced}本图书成功!");
        }
    }
}
