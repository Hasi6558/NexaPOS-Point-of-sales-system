using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace NexaPOS
{
    public partial class UserModuleForm : Form
    {
        private void checkBoxPass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPass.Checked == false)
            {
                txtNewPwd.UseSystemPasswordChar = true;
                txtOldPwd.UseSystemPasswordChar = true;
            }
            else
            {
                txtNewPwd.UseSystemPasswordChar = false;
                txtOldPwd.UseSystemPasswordChar = false;
            }

        }
        public UserModuleForm()
        {
            InitializeComponent();
        }

        private void UserModuleForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit Password Reset", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                Form1 home = new Form1();
                home.Show();
                this.Close();

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate Old Password
            string query = "SELECT COUNT(*) FROM users WHERE password=@oldPassword";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConString"].ToString());
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@oldPassword", txtOldPwd.Text);

            try
            {
                conn.Open();
                object result = cmd.ExecuteScalar();
                if (Convert.ToInt32(result) <= 0)
                {
                    MessageBox.Show("Old password is incorrect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Update Password
                string updateQuery = "UPDATE users SET password=@newPassword WHERE password=@oldPassword";
                cmd.CommandText = updateQuery;
                cmd.Parameters.AddWithValue("@newPassword", txtNewPwd.Text);
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Password updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to update the password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtNewPwd.Clear();
            txtOldPwd.Clear();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
