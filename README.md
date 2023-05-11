# 实验5 数据库程序设计

---

## 一、实验目的

掌握数据库应用开发程序设计方法

## 二、实验平台

- 数据库管理系统: SQL Server
- 开发平台: Visual Studio 2022 + winform
- 开发语言: C#

## 三、总体设计

### 1、系统架构描述

本系统主要包括**登录与注册、图书查询、借还图书、增加图书、借书证管理**五大模块

**系统处理基本流程如下：**

![1](https://raw.githubusercontent.com/Tianjiangyigeyi/img/master/202305081014509.png)



**各个功能模块说明如下：**

|  模块名称  |                           功能描述                           |
| :--------: | :----------------------------------------------------------: |
| 登录与注册 | 可以选择使用管理员身份或者使用普通用户登录，没有账号可以注册新账号，新注册的账号只能是普通用户，目前仅有一个管理员账号’root‘ |
|  图书查询  | 支持模糊匹配，可以使用图书的任意信息进行查询：序号、类别、书名、作者，查询结果会显示在界面左边的“显示图书”表中 |
|  借还图书  | 图书管理员要输入需要借还图书的用户的借书证号和图书的序号进行借还操作，普通用户只需要输入需要借还图书的序号即可 |
|  增加图书  | 只有管理员能进行此操作，输入书的四个信息：类别、书名、作者、库存，点击“增加”即可增加图书 |
| 借书证管理 | 每个普通用户必须要管理员为其注册借书证才可借书，管理员除了有为每个用户注册借书证的权限也有注销借书证的权限 |

### 2、数据库表设计

**用户表(MyUsers)**

| 字段名   | 数据类型    | 约束        | 说明   |
| -------- | ----------- | ----------- | ------ |
| UID      | varchar(50) | primary key | 用户名 |
| password | varchar(50) | not null    | 密码   |
| name     | varchar(50) | not null    | 昵称   |



**图书表(books)**

| 字段名   | 数据类型    | 约束                         | 说明 |
| -------- | ----------- | ---------------------------- | ---- |
| no       | int         | primary key & auto increment | 序号 |
| type     | varchar(50) | not null                     | 类别 |
| bookname | varchar(50) | not null                     | 书名 |
| author   | varchar(50) | not null                     | 作者 |
| remain   | int         | not null                     | 库存 |



**借书证表(card)**

| 字段名 | 数据类型    | 约束                          | 说明     |
| ------ | ----------- | ----------------------------- | -------- |
| cardno | int         | primary key && auto increment | 借书证号 |
| UID    | varchar(50) | not null                      | 用户名   |



**借书记录表(record)**

| 字段名     | 数据类型    | 约束                          | 说明     |
| ---------- | ----------- | ----------------------------- | -------- |
| id         | int         | primary key && auto increment | 标识列   |
| cardno     | int         | not null                      | 借书证号 |
| bookname   | varchar(50) | not null                      | 书名     |
| borrowdate | varchar(50) | not null                      | 借出时间 |
| returndate | varchar(50) |                               | 归还时间 |
| bookno     | int         | not null                      | (书)序号 |

### 3、所用开发技术

#### SQL Server

SQL Server是由Microsoft开发设计的一个关系数据库智能管理系统(RDBMS)，现在是全世界主流数据库之一。SQL Server具备方便使用、可伸缩性好、相关软件集成程度高等优势，能够从单一的笔记本上运行或以高倍云服务器集群为基础，或在这两者之间任何东西上运行。

SQL Server提供了一些高级功能，如数据加密、数据压缩、备份和恢复等等，可以帮助更好地保护和管理数据。同时，SQL Server还提供了一些其他的工具和功能，如SQL Server Management Studio（SSMS），可以帮助更好地管理和维护SQL Server数据库。

由于是Microsoft开发设计的，相比于其他数据库管理系统，在Visual Studio和Winform进行数据库相关开发中，SQL Server具有很好的集成性、更强大的功能以及更好的可维护性。



#### C# and Winform

C#是一种面向对象的编程语言，.NET是一个软件框架，它包括一个解析应用程序代码的类库以及一个支持程序运行的平台。.NET中所有的编程语言，比如C#、VB.NET等编写的程序必须在.NET Framework框架下运行。. C#的优点包括：

- C#是一种类型安全和高效率的语言，它可以在编译时捕获许多错误.

- C#具有丰富的类库和内置类型，可以轻松地完成许多任务。
- C#支持多线程编程，可以提高应用程序的性能。

Winform是一种基于.NET Framework的GUI框架，提供了一个易于使用的用户界面，能够帮助开发者快速构建和部署SQL Server数据库应用程序，以下是Winform在GUI设计方面的优势：

- Winform提供了一个简单易用的窗体设计器，可以快速创建和布局窗体。
- Winform提供了许多内置控件，如按钮、文本框、标签等等，可以快速创建和布局用户界面。
- Winform提供了一个事件模型，可以轻松地处理用户交互事件。
- Winform提供了一个简单易用的数据绑定模型，可以将数据绑定到控件上。



#### Visual Studio

Visual Studio是由Microsoft开发的一款集成开发环境(IDE)，由于本系统使用的SQL Server和C#语言都是由微软公司开发，IDE选用Visual Studio是最合适的选择，可以极大的提高开发者的开发效率

- Visual Studio提供了一个集成的开发环境，可以在一个地方完成所有的开发任务。
- Visual Studio提供了许多内置工具和功能，如代码编辑器、调试器、测试工具等等，可以轻松地编写和调试代码。
- Visual Studio提供了许多扩展和插件，可以将其与其他工具和服务集成在一起。
- Visual Studio提供了一个简单易用的界面，可以更轻松地管理和维护项目。

## 四、详细设计

### 0、前置说明

对数据库增删改查相关操作进行了封装, 设置类名为SQLHelper

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace bookMS
{
    public class SQLHelper
    {
        public static string connectionString = "server=.;database=Library_db;user=sa;password=123456";

        /// <summary>
        /// 执行增删改操作
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>
        /// 受影响的行数
        /// </returns>
        public static int Update(string sql)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, connection);
            try
            {
                connection.Open();
                return cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                //写入日志
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        /// 返回一个结果集的查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static SqlDataReader GetReader(string sql)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, connection);
            try
            {
                connection.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch(Exception ex)
            {
                connection.Close();
                throw ex;
            }

        }
    }
}

```



### 1、登录界面设计

![image-20230508143855266](https://raw.githubusercontent.com/Tianjiangyigeyi/img/master/202305081439486.png)

*\*目前用户表只有一张, 规定了管理员的账号只有root, 如果后续需要添加别的账号为管理员, 需要将用户表与管理员表区分开, 由于本项目并没有授权管理员权限的需求, 故简化了设计*

可以选择登录的方式: 管理员/普通用户, 会根据输入的用户名和密码去匹配`MyUsers`表中记录的信息, 检查登录是否成功, 并根据用户选择登录方式的不同, 登录后跳转到相应不同的界面。

以下是登录功能实现的代码: 

```c#
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
```



任何用户都可以注册新账号, 注册之后, 信息会自动存储到数据库用户表`MyUsers`中

![image-20230508150655639](https://raw.githubusercontent.com/Tianjiangyigeyi/img/master/202305081506759.png)



### 2、管理员界面设计

#### 表显示

登录后的初始界面如图, 通过点击下方的按钮可以显示不同的表

原理是`select * from table`并把查询的结果迭代放入视图中, 并且可以通过点击不同的按钮来确定要显示的是哪一张表

部分代码实现如下:

```c#
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
    dataGridView2.Hide();
    dataGridView3.Hide();
    dataGridView1.Show();
    freshBooks();
}
```

![image-20230508150941353](https://raw.githubusercontent.com/Tianjiangyigeyi/img/master/202305081509475.png)

![image-20230508151145918](https://raw.githubusercontent.com/Tianjiangyigeyi/img/master/202305081511050.png)

![image-20230508151153957](https://raw.githubusercontent.com/Tianjiangyigeyi/img/master/202305081511075.png)

![image-20230508151202109](https://raw.githubusercontent.com/Tianjiangyigeyi/img/master/202305081512230.png)



#### 图书查询

可以在图书查询界面里的四个textbox里面输入任意内容, 支持模糊查找, 但不支持多关键字

原理是`select * from books where xx like '%{textbox.Text}% and ...'`

*\*如果规定查询多关键字用特定的符号连接, 如 '+' 则可以实现较为简单的多关键词搜索, 然而本项目并未具体实现该功能*

部分代码实现如下:

```c#
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
```



#### 借还图书

这部分进行了一点有趣且实用的设计

图书管理员能够输入的文本框只有借书证和序号, 当输入序号时, 可以自动显示出序号对应的书的具体信息

- 好处: 能够很清晰的看到将要借/还的图书的相关信息, 防止管理员出现操作的失误, 也避免了借出库存为负的图书的情况, 提高了规范性
- 缺点: 降低了自由度, 管理员必须先在图书查询界面里找到想要借还的书的序号才能进行借还

部分代码实现如下:

```
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
```

![image-20230508152348828](https://raw.githubusercontent.com/Tianjiangyigeyi/img/master/202305081523961.png)



#### 增加图书

管理员需要将图书的各种信息填写完整才能入库

正是由于入库操作的完整性, 很大程度上避免了管理员在入库时出现误操作的情况

原理是`insert into books(type, bookname, author, remain) values ('{textBox13.Text}','{textBox12.Text}','{textBox11.Text}','{textBox15.Text}' `, 将图书信息添加到`books`表中, 然后刷新左侧表中显示的内容

部分代码实现如下:

```c#
private void button7_Click(object sender, EventArgs e)
{
    if (textBox13.Text == "" || textBox12.Text == "" || textBox11.Text == "" || textBox15.Text == "")
    {
        MessageBox.Show("请输入完整");
        return;
    }
    string sql = $"insert into books(type, bookname, author, remain) values ('{textBox13.Text}','{textBox12.Text}','{textBox11.Text}','{textBox15.Text}')";
    SQLHelper.Update(sql);
    freshBooks();
    textBox12.Clear();
    textBox11.Clear();
    textBox13.Clear();
    textBox15.Clear();
    MessageBox.Show("添加成功");
}
```

![image-20230508153136292](https://raw.githubusercontent.com/Tianjiangyigeyi/img/master/202305081531470.png)

#### 借书证管理

先简要介绍一下借书证相关逻辑:

1. 任何账号想要借还图书就必须要管理员登录账号进行借书证的注册
2. 管理员可以对任何UID进行借书证的注册和注销
3. 任何用户最多只能有一个借书证
4. 任何用户在还有尚未归还的图书时不能注销借书证
5. 借书证注销后不会有人注册到已经注册过但注销了的借书证号

实现原理: 

- 注册: 在`card`表中查询输入的UID是否已经存在, 如果已经存在则提示"该账号已注册过借书证: xxxxxxxx", 如果没有查询到结果, 则将该UID insert into `card`表
- 注销: 除了要查询UID是否在`card`中存在, 还要在`record`表中查询是否存在returndate为空的项, 如果有则说明该借书证下有未归还的图书, 提示注销失败

部分代码实现如下: 

```c#
private void button8_Click(object sender, EventArgs e)
{
    if (textBox14.Text == "")
    {
        return;
    }
    string sql = $"select * from card where UID = '{textBox14.Text}'";
    SqlDataReader objReader = SQLHelper.GetReader(sql);
    if (objReader.Read())
    {
        MessageBox.Show("该账号已经注册过借书证: " + objReader[0].ToString());
        return;
    }
    else
    {
        string sql2 = $"select * from MyUsers where UID = '{textBox14.Text}'";
        SqlDataReader objReader2 = SQLHelper.GetReader(sql2);
        if (objReader2.Read())
        {
            sql2 = $"insert into card(UID) values ('{textBox14.Text}')";
            SQLHelper.Update(sql2);
            objReader = SQLHelper.GetReader(sql);
            freshCard();
            if(objReader.Read()) MessageBox.Show("注册成功! 你的借书证号是: " + objReader[0].ToString());
            textBox14.Clear();
            return;
        }
        else
        {
            MessageBox.Show("该用户不存在");
            return;
        }
    }
}

private void button9_Click(object sender, EventArgs e)
{
    if (textBox14.Text == "")
    {
        return;
    }
    string sql = $"select * from card where UID = '{textBox14.Text}'";
    SqlDataReader objReader = SQLHelper.GetReader(sql);
    string sql2 = $"select * from MyUsers where UID = '{textBox14.Text}'";
    SqlDataReader objReader2 = SQLHelper.GetReader(sql2);
    if (!objReader2.Read())
    {
        MessageBox.Show("该账号不存在");
        return;
    }
    else
    {
        if (objReader.Read())
        {
            sql2 = $"delete from card where UID = '{textBox14.Text}'";
            string cardno = objReader[0].ToString();
            string sql3 = $"select * from record where cardno = '{cardno}' and returndate is null";
            if (SQLHelper.GetReader(sql3).Read())
            {
                MessageBox.Show("注销失败! 还有尚未归还的图书");
                return;
            }
            SQLHelper.Update(sql2);
            MessageBox.Show("借书证号: " + cardno + " 注销成功");
            textBox14.Clear();
            freshCard();
            return;
        }
        else
        {
            MessageBox.Show("该用户尚未注册借书证");
            return;
        }
    }
}
```

![image-20230508154233754](https://raw.githubusercontent.com/Tianjiangyigeyi/img/master/202305081542966.png)



#### 退出登录

- `切换用户`: 回到登录界面
- `退出`: 退出程序

实现代码如下:

```c#
//切换用户
private void button1_Click(object sender, EventArgs e)
{
    this.Close();
    this.Owner.Show();
}

//退出
private void button6_Click(object sender, EventArgs e)
{
    this.Close();
    this.Owner.Close();
}
```

![image-20230508154644989](https://raw.githubusercontent.com/Tianjiangyigeyi/img/master/202305081546125.png)

### 3、用户界面设计

大部分UI设计与管理员设计一致, 这里只列出不同的部分

1. 只能显示books表和record表, 且record表只能看到自己的借书记录
2. 借还书籍时自动显示了已有的借书证, 若没有借书证会显示"(无)"

![image-20230508162258224](https://raw.githubusercontent.com/Tianjiangyigeyi/img/master/202305081623423.png)

![image-20230508162443458](https://raw.githubusercontent.com/Tianjiangyigeyi/img/master/202305081624524.png)

![image-20230508162519270](https://raw.githubusercontent.com/Tianjiangyigeyi/img/master/202305081625327.png)

### 4、其他设计

由于winform的界面布局是固定窗口大小的, 如果使用时缩放窗口, 窗口内的组件并不会随之缩放, 这并不是一个合格的GUI设计, 于是我查阅资料, 增加了交互事件, 当用户缩放窗口时, 会自动调用函数, 重新设置每个组件的size, 由于要递归调用每个组件(组件内部可能会有子组件), 并且当页面足够复杂时, 该函数会占用大量的执行时间, 导致页面变卡, 好在, 本项目的页面布局并不复杂, 这样的时间消耗尚在可接受范围内

放大效果如图:

![image-20230508150227346](https://raw.githubusercontent.com/Tianjiangyigeyi/img/master/202305081502624.png)

![image-20230508150157758](https://raw.githubusercontent.com/Tianjiangyigeyi/img/master/202305081501039.png)



相关代码如下

```c#
private float x, y;

private void Form1_Load(object sender, EventArgs e)
{
    x = this.Width;
    y = this.Height;
    setTag(this);
}

private void setTag(Control cons)
{
    foreach (Control con in cons.Controls)//循环窗体中的控件
    {
        con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
        if (con.Controls.Count > 0)
            setTag(con);
    }
}

private void Form1_Resize(object sender, EventArgs e)
{
    float _X = (this.Width) / x;
    float _y = (this.Height) / y;
    setControls(_X, _y, this);
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
```

