    using Microsoft.Data.SqlClient;
    using NexaPOS;
    using System;
    using System.Data;
    using System.Windows.Forms;


    namespace NexaPOS
    {
    public partial class SearchProducts : Form
    {
        DBConnect dbconn = new DBConnect();
        SqlConnection con = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        private Form1 mainForm;

        public SearchProducts(Form1 mainForm)
        {
            InitializeComponent();
            textBox1.TextChanged += new EventHandler(txtSearch_TextChanged);
            textBox2.TextChanged += new EventHandler(txtSearch2_TextChanged);
            this.mainForm = mainForm;
            dataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(dataGridView1_CellDoubleClick);
            dataGridView1.CellClick += new DataGridViewCellEventHandler(dataGridView1_CellClick); // Add CellClick event
            
        }


        private void txtSearch_TextChanged(Object sender, EventArgs e)
        {
            SearchProductsbyKeyword(textBox1.Text);

        }
        private void txtSearch2_TextChanged(Object sender, EventArgs e)
        {
            SearchProductsbyBarcode(textBox2.Text);

        }


        private void SearchProductsbyKeyword(string keyword)
        {
            using (SqlConnection connection = new SqlConnection(dbconn.myConnection()))
            {
                string query = "SELECT * FROM products WHERE productName LIKE @keyword";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.Rows.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        dataGridView1.Rows.Add(row["id"], row["pcode"], row["barcode"], row["productName"], row["brand"], row["category"], row["price"], row["qty"]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void SearchProductsbyBarcode(string barcode)
        {
            using (SqlConnection connection = new SqlConnection(dbconn.myConnection()))
            {
                string query = "SELECT * FROM products WHERE barcode LIKE @barcode";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@barcode", "%" + barcode + "%");

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.Rows.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        dataGridView1.Rows.Add(row["id"], row["pcode"], row["barcode"], row["productName"], row["brand"], row["category"], row["price"], row["qty"]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true; // Select the entire row
            }
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                int no = Convert.ToInt32(row.Cells["no"].Value);
                string pcode = row.Cells["pcode"].Value.ToString();
                string barcode = row.Cells["barcode"].Value.ToString();
                string productName = row.Cells["productName"].Value.ToString();
                string brand = row.Cells["brand"].Value.ToString();
                string category = row.Cells["category"].Value.ToString();
                decimal price = Convert.ToDecimal(row.Cells["price"].Value);

                Quantity quantityForm = new Quantity(no, pcode, barcode, productName, brand, category, price, mainForm);
                quantityForm.ShowDialog();
            }
        }


        public void LoadProduct()
        {
            // Assuming you want to load all products initially
            using (SqlConnection connection = new SqlConnection(dbconn.myConnection()))
            {
                string query = "SELECT * FROM products";
                SqlCommand cmd = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.Rows.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        dataGridView1.Rows.Add(row["no"], row["pcode"], row["barcode"], row["productName"], row["brand"], row["category"], row["price"], row["qty"]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(51, 51, 122);

            
            // Your code to handle the cell content click event
        }

        private void SearchProducts_Load(object sender, EventArgs e)
        {
            LoadProduct();
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(51, 51, 122);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void SearchProducts_Load_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
