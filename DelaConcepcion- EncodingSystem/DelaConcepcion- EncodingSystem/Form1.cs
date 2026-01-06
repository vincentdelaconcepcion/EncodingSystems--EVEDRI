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

            // --- Simple visual fixes ---

            // Remove the blue row header block at the left
            dgvStudents.RowHeadersVisible = false;

            // Make selection visually neutral (no blue fill)
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

            // Validate inputs first
            if (string.IsNullOrWhiteSpace(studentName) || string.IsNullOrWhiteSpace(studentID) || string.IsNullOrWhiteSpace(course))
            {
                MessageBox.Show("Please fill all fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check duplicate Student ID in ListView
            foreach (ListViewItem item in lvStudents.Items)
            {
                // lvStudents columns are: StudentID, Student Name, Course
                if (item.SubItems.Count > 0 && item.SubItems[0].Text == studentID)
                {
                    MessageBox.Show("Student ID already exists.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Also check duplicate in DataGridView (defensive)
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

            // Add to ListView (columns: StudentID, Student Name, Course)
            ListViewItem listViewItem = new ListViewItem(studentID);
            listViewItem.SubItems.Add(studentName);
            listViewItem.SubItems.Add(course);
            lvStudents.Items.Add(listViewItem);

            // Add to DataGridView (columns in Designer: StudentID, StudentName, Course)
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
            txtCourse.Clear();
            txtStudentID.Clear();
            txtStudentName.Clear();

            txtStudentID.Focus();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            // Clear inputs and all displayed entries (ListView and DataGridView)
            txtCourse.Clear();
            txtStudentID.Clear();
            txtStudentName.Clear();

            lvStudents.Items.Clear();
            dgvStudents.Rows.Clear();

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
