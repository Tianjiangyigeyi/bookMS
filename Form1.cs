using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using bookMS;

namespace bookMS
{
    public partial class Form1 : Form
    {
        private float x, y;
        public Form1()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 将控件的宽，高，左边距，顶边距和字体大小暂存到tag属性中
        /// </summary>
        /// <param name="cons">递归控件中的控件</param>
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


        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "" && textBox2.Text != "")
            {
                string sql = "select * from MyUsers where UID = \'" + textBox1.Text + "\' and password = \'" + textBox2.Text + "\'";
                SqlDataReader objReader = SQLHelper.GetReader(sql);
                if (!radioButton1.Checked && !radioButton2.Checked)
                {
                    MessageBox.Show("请选择一个身份登录");
                    return;
                }
                if(objReader.Read())
                {
                    MessageBox.Show("欢迎你！" + objReader["name"].ToString());
                    if (radioButton1.Checked)
                    {
                        if (textBox1.Text == "root")
                        {
                            manage f = new manage(textBox1.Text);
                            f.Owner = this;
                            this.Hide();
                            f.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("你不是管理员, 请选择普通用户登录");
                        }
                    }
                    else
                    {
                        home f = new home(textBox1.Text);
                        f.Owner = this;
                        this.Hide();
                        f.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("用户名或密码错误");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Owner = this;
            //f.Size = new Size(this.Width, this.Height);
            this.Hide();
            f.ShowDialog();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            x = this.Width;
            y = this.Height;
            setTag(this);
        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            float _X = (this.Width) / x;
            float _y = (this.Height) / y;
            setControls(_X, _y, this);
        }
    }
}
