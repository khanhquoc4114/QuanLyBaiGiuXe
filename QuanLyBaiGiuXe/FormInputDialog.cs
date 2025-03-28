using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBaiGiuXe
{
    public partial class FormInputDialog: Form
    {
        public enum InputType { TextBox, DatePicker, ComboBox , None}

        public string UserInput { get; private set; } = "";
        private Control inputControl;

        public FormInputDialog(string title, string prompt, InputType type= InputType.None, string[] comboItems = null)
        {
            InitializeComponent();
            this.Text = title;
            lbText.Text = prompt;
            this.StartPosition = FormStartPosition.CenterParent;

            switch (type)
            {
                case InputType.TextBox:
                    inputControl = new TextBox { Width = 200 };
                    break;
                case InputType.DatePicker:
                    inputControl = new DateTimePicker { Width = 200 };
                    break;
                case InputType.ComboBox:
                    inputControl = new ComboBox { Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
                    if (comboItems != null)
                        ((ComboBox)inputControl).Items.AddRange(comboItems);
                    break;
                case InputType.None:
                    break;
            }

            inputControl.Location = new Point(20, 50);
            this.Controls.Add(inputControl);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (inputControl is TextBox)
                UserInput = ((TextBox)inputControl).Text;
            else if (inputControl is DateTimePicker)
                UserInput = ((DateTimePicker)inputControl).Value.ToString("yyyy-MM-dd");
            else if (inputControl is ComboBox)
                UserInput = ((ComboBox)inputControl).SelectedItem?.ToString() ?? "";

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
