using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AquaPark_XML
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string path_to_fileXml = @"../../XMLFile.xml";
        List<Employers> employers = new List<Employers>();

        private DataTable CreateTable()
        {
            //создаём таблицу
            DataTable dt = new DataTable("Employers");
            //создаём три колонки
            DataColumn colName = new DataColumn("Имя", typeof(String));
            DataColumn colFullName = new DataColumn("Фамилия", typeof(String));
            DataColumn colStatus = new DataColumn("Должность", typeof(String));
            
          
            //добавляем колонки в таблицу
            dt.Columns.Add(colName);
            dt.Columns.Add(colFullName);
            dt.Columns.Add(colStatus);

            return dt;

        }
        private DataTable ReadXml()
        {
            DataTable dt = null;
            try
            {
                //загружаем xml файл
                XDocument xDoc = XDocument.Load(path_to_fileXml);
                //создаём таблицу
                dt = CreateTable();
                DataRow newRow = null;
                //получаем все узлы в xml файле
                foreach (XElement elm in xDoc.Descendants("employers"))
                {
                    //создаём новую запись
                    newRow = dt.NewRow();
                  
                    //проверяем наличие xml элемента name
                    if (elm.Element("name") != null)
                    {
                        //получаем значения элемента name
                        newRow["Имя"] = elm.Element("name").Value;
                    }
                    if (elm.Element("fullname") != null)
                    {
                        newRow["Фамилия"] = elm.Element("fullname").Value;
                    }
                    if (elm.Element("status") != null)
                    {
                        newRow["Должность"] = elm.Element("status").Value;
                    }
                    //добавляем новую запись в таблицу
                    dt.Rows.Add(newRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ReadXml();
        }

        private void radioButton7_Click(object sender, EventArgs e)
        {
            ReadXml();
        }
    }
}
