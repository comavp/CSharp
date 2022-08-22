using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditor
{
    public interface IMainForm
    {
        string FilePath { get; }
        string Content { get; set;}
        void SetSymvolCount(int count);

        event EventHandler FileOpenClick;
        event EventHandler FileSaveClick;
        event EventHandler ContentChanged;
    }
    public partial class MainForm : Form, IMainForm
    {
        public MainForm()
        {
            InitializeComponent();
            OpenFile.Click += OpenFile_Click;
            SaveContent.Click += SaveContent_Click;
            fldContent.TextChanged += fldContent_TextChanged;
            SelectFile.Click += SelectFile_Click;
            numFont.ValueChanged += numFont_ValueChanged;
        }

        #region Проброс событий
        void fldContent_TextChanged(object sender, EventArgs e)
        {
            if (FileOpenClick != null) ContentChanged(this, EventArgs.Empty);
        }

        void SaveContent_Click(object sender, EventArgs e)
        {
            if (FileOpenClick != null) FileSaveClick(this, EventArgs.Empty);
        }

        void OpenFile_Click(object sender, EventArgs e)
        {
            if (FileOpenClick != null) FileOpenClick(this, EventArgs.Empty);
        }
        #endregion

        #region IManForm
        public string FilePath 
        {
            get { return fldFilePath.Text; }
        }
        public string Content
        {
            get { return fldContent.Text; }
            set { fldContent.Text = value; }
        }
        public void SetSymvolCount(int count)
        {
            lblSymvolCount.Text = count.ToString();
        }

        public event EventHandler FileOpenClick;
        public event EventHandler FileSaveClick;
        public event EventHandler ContentChanged;
        #endregion
        private void SelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Текстовые файлы|*.txt|Все файлы|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                fldFilePath.Text = dlg.FileName;
                if (FileOpenClick != null) FileOpenClick(this, EventArgs.Empty);
            }
        }
        private void numFont_ValueChanged(object sender, EventArgs e)
        {
            fldContent.Font = new Font("Calibri", (float)numFont.Value);
        }
    }
}
