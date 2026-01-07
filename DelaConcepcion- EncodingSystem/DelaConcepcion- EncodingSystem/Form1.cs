using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DelaConcepcion__EncodingSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //Removes the Blue Colored Grid in GDV
            dgvStudents.RowHeadersVisible = false;

            
            dgvStudents.EnableHeadersVisualStyles = false;
            dgvStudents.DefaultCellStyle.SelectionBackColor = dgvStudents.DefaultCellStyle.BackColor;
            dgvStudents.DefaultCellStyle.SelectionForeColor = dgvStudents.DefaultCellStyle.ForeColor;
            dgvStudents.ClearSelection();

            // Fix large ListView spacing by providing a small ImageList sized to font height
            var img = new ImageList();
            img.ImageSize = new Size(1, Math.Max(16, lvStudents.Font.Height + 4)); // tweak +4 to adjust padding
            lvStudents.SmallImageList = img;

            // Make ListView selection nicer
            lvStudents.FullRowSelect = true;
        }

        private void dgvStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string studentName = txtStudentName.Text.Trim();
            string studentID = txtStudentID.Text.Trim();
            string course = txtCourse.Text.Trim();

            // Checks the User Inputs
            if (string.IsNullOrWhiteSpace(studentName) || string.IsNullOrWhiteSpace(studentID) || string.IsNullOrWhiteSpace(course))
            {
                MessageBox.Show("Please fill all fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // checks if theres duplication of data in Listview
            foreach (ListViewItem item in lvStudents.Items)
            {
              
                if (item.SubItems.Count > 0 && item.SubItems[0].Text == studentID)
                {
                    MessageBox.Show("Student ID already exists.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Checks the DGV Duplications
            foreach (DataGridViewRow row in dgvStudents.Rows)
            {
                if (row.IsNewRow) continue;
                var cell = row.Cells[0].Value;
                if (cell != null && cell.ToString() == studentID)
                {
                    MessageBox.Show("Student ID already exists.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Add to ListView 
            ListViewItem listViewItem = new ListViewItem(studentID);
            listViewItem.SubItems.Add(studentName);
            listViewItem.SubItems.Add(course);
            lvStudents.Items.Add(listViewItem);

            // Add to DataGridView
            dgvStudents.Rows.Add(studentID, studentName, course);

            // Clear textboxes and set focus to StudentID
            txtStudentID.Clear();
            txtStudentName.Clear();
            txtCourse.Clear();
            txtStudentID.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // Clear only the input textboxes
            if (dgvStudents.Rows.Count == 0 && lvStudents.Items.Count == 0)
                return;

            string StudentID = dgvStudents.SelectedRows[0].Cells[0].Value.ToString();

            dgvStudents.Rows.Remove(dgvStudents.SelectedRows[0]);

            foreach (ListViewItem item in lvStudents.Items)
            {
                if (item.SubItems[0].Text == StudentID)
                {
                    lvStudents.Items.Remove(item);
                    break;
                }
            }
        }
        private void btnClearAll_Click(object sender, EventArgs e)
        {
            // Clear inputs and all displayed entries
            txtCourse.Clear();
            txtStudentID.Clear();
            txtStudentName.Clear();

            


                txtStudentID.Focus();
        }

        private void lvStudents_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
