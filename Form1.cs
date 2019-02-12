using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;


namespace vp_lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenDlg = new OpenFileDialog();
            if (OpenDlg.ShowDialog() == DialogResult.OK)
            {
                StreamReader Reader = new StreamReader(OpenDlg.FileName, Encoding.Default);
                richTextBox1.Text = Reader.ReadToEnd();
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveDlg = new SaveFileDialog();
            if (SaveDlg.ShowDialog() == DialogResult.OK)
            {
                StreamWriter Writer = new StreamWriter(SaveDlg.FileName);
                for (int i = 0; i < listBox2.Items.Count; i++)
                {
                    Writer.WriteLine((string)listBox2.Items[i]);
                }
                Writer.Close();
            }
            SaveDlg.Dispose();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Информация о прложении и разработчике");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            listBox1.Items.Clear();
            listBox2.Items.Clear();

            listBox1.BeginUpdate();

            string[] Strings = richTextBox1.Text.Split(new char[] { '\n', '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach(string s in Strings)
            {
                string Str = s.Trim();
                if (Str == String.Empty) continue;
                if (radioButton1.Checked) listBox1.Items.Add(Str);
                if (radioButton2.Checked)
                {
                    if (Regex.IsMatch(Str, @"\d")) listBox1.Items.Add(Str);
                }
                if (radioButton3.Checked)
                {
                    if (Regex.IsMatch(Str, @"\w+@\w+\.\w+")) listBox1.Items.Add(Str);
                }
            }

            listBox1.EndUpdate();
            

            
        }

        private void button13_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            textBox1.Clear();
            radioButton1.Checked = true;
            checkBox1.Checked = true;
            checkBox2.Checked = false;
            comboBox1.Text = "Сортировка по..";
            comboBox2.Text = "Сортировка по..";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
            string Find = textBox1.Text;

            if (checkBox1.Checked)
            {
                foreach(string String in listBox1.Items)
                {
                    if (String.Contains(Find)) listBox3.Items.Add(String);
                }
            }

            if (checkBox2.Checked)
            {
                foreach( string String in listBox2.Items)
                {
                    if (String.Contains(Find)) listBox3.Items.Add(String);
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Form2 AddRec = new Form2();
            AddRec.Owner = this;
            AddRec.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count > 0) DelSelectedString(listBox1);
            if (listBox2.SelectedItems.Count > 0) DelSelectedString(listBox2);
        }

        public void DelSelectedString( ListBox listBox)
        {
            for( int i = listBox.Items.Count-1; i >=0; i--)
            {
                if (listBox.GetSelected(i)) listBox.Items.RemoveAt(i);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox2.BeginUpdate();
            for (int i = listBox1.Items.Count - 1; i >= 0; i--)
            {
                if (listBox1.GetSelected(i))
                {
                    listBox2.Items.Add(listBox1.Items[i]);
                    listBox1.Items.RemoveAt(i);
                }

            }

            listBox2.EndUpdate();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = listBox2.Items.Count - 1; i >= 0; i--)
            {
                if (listBox2.GetSelected(i))
                {
                    listBox1.Items.Add(listBox2.Items[i]);
                    listBox2.Items.RemoveAt(i);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox2.Items.AddRange(listBox1.Items);
            listBox1.Items.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.AddRange(listBox2.Items);
            listBox2.Items.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SortList(listBox1, comboBox1);
         
        }

        public void SortCBA(string[] arr)
        {
            Array.Sort(arr);
            Array.Reverse(arr);     
        }

        public void SortUp(string[] arr)
        {

            for( int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr.Length -1; j++)
                {
                    if( arr[j].Length > arr[j+1].Length)
                    {
                        string t = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = t;
                    }
                }
            }
        }

        public void SortDown(string[] arr)
        {
            
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr.Length - 1; j++)
                {
                    if (arr[j].Length < arr[j + 1].Length)
                    {
                        string t = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = t;
                    }
                }
            }

        }

        public void SortList( ListBox listBx, ComboBox Combo)
        {
            string[] lst;
            lst = new string[listBx.Items.Count];
            for (int i = 0; i < listBx.Items.Count; i++)
            {
                lst[i] = listBx.Items[i].ToString();
            }

            switch (Combo.SelectedIndex)
            {
                case 0:
                    listBx.Sorted = true;
                    break;

                case 1:
                    listBx.Sorted = false;
                    SortCBA(lst);
                    break;
                case 2:
                    listBx.Sorted = false;
                    SortUp(lst);
                    break;
                case 3:
                    listBx.Sorted = false;
                    SortDown(lst);
                    break;
                default: break;
            }

            listBx.BeginUpdate();
            listBx.Items.Clear();
            for (int i = 0; i < lst.Length; i++)
            {
                listBx.Items.Add(lst[i]);
            }
            listBx.EndUpdate();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SortList(listBox2, comboBox2);           
        }
    }
}
