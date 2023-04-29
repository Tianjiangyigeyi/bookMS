using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bookMS
{
    
    public partial class Form2 : Form
    {
        private float x, y;
        private bool tagSeted;
        public Form2()
        {
            InitializeComponent();
            x = 0; y = 0;
            tagSeted = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox_name.Text == "")
            {
                MessageBox.Show("请输入昵称");
            }
            else if(textBox_user.Text == "")
            {
                MessageBox.Show("请输入用户名");
            }else if(textBox_psw.Text == "")
            {
                MessageBox.Show("请输入密码");
            }else if(textBox_confirm.Text == "")
            {
                MessageBox.Show("请确认密码");
            }else if(textBox_psw.Text != textBox_confirm.Text)
            {
                MessageBox.Show("两次输入的密码不一致!");
            }
            else
            {
                string sql = "insert into MyUsers (UID, password, name) values(\'" + textBox_user.Text + "\', \'" + textBox_psw.Text + "\', \'" + textBox_name.Text + "\');";
                if(SQLHelper.Update(sql) == 1)
                {
                    MessageBox.Show("注册成功");
                    this.Close();
                    this.Owner.Show();
                }
                else
                {
                    MessageBox.Show("用户名已存在");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Owner.Show();
        }

        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height+ ":" +con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                {
                    setTag(con);
                }
            }

            tagSeted = true;
        }

        private void setControls(float newx, float newy, Control cons)
        {
            if (tagSeted == false)
            {
                return;
            }
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

        private void Form2_Load(object sender, EventArgs e)
        {
            //this.Width = this.Owner == null ? this.Width : this.Owner.Width;
            //this.Height = this.Owner == null ? this.Height : this.Owner.Height;
            x = this.Width;
            y = this.Height;
            setTag(this);
        }

        private void Form2_Resize(object sender, EventArgs e)
        {
            float _x = (x != 0) ? (this.Width) / x : 1;
            float _y = (y != 0) ? (this.Height) / y : 1;
            setControls(_x, _y, this);
        }
    }
}
