using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PeonLib.forms
{
    public partial class AdminUser : Form
    {       
        public AdminUser()
        {
            InitializeComponent();
        }

        private void AdminUser_Load(object sender, EventArgs e)
        {
            listbox1.ReadFile(definitions.path.UserList);
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            inputForm f = new inputForm(definitions.FormTitle.AddUser,definitions.Question.AddUser);
            f.ShowDialog();
            
            if (f.DialogResult == DialogResult.OK)
            {
                Object.User u = new PeonLib.Object.User(f.Value);
                listbox1.addValue(f.Value);
            }
        }

        private void button_remove_Click(object sender, EventArgs e)
        {
            listbox1.RemoveSelectedValue();
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            listbox1.SaveFile();
        }
    }
}
