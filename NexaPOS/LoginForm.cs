using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NexaPOS
{
    public partial class LoginForm : Form
    {
        DB dB = new DB();
        public LoginForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxPass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPass.Checked == false)
                txtPass.UseSystemPasswordChar = true;
            else
                txtPass.UseSystemPasswordChar = false;
        }

        private void lblClear_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtPass.Clear();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit Application", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtName.Text))
            {
                MessageBox.Show("Please Enter Username.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(this.txtPass.Text))
            {
                MessageBox.Show("Please Enter Password.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtPass.Focus();
                return;
            }

            string query = string.Concat("SELECT COUNT(*) FROM users WHERE user_name='", this.txtName.Text, "' AND password='", this.txtPass.Text, "'");
            Console.WriteLine($"Executing Query: {query}"); // Log the query for debugging

            string result = dB.QueryScalar(query);
            Console.WriteLine($"Query Result: {result}"); // Log the result for debugging

            if (int.TryParse(result, out int count))
            {
                if (count <= 0)
                {
                    MessageBox.Show("Username or password is incorrect. Please put valid username and password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                // Log the result for debugging
                Console.WriteLine($"Failed to parse result: {result}");
                MessageBox.Show("An error occurred while logging in. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
