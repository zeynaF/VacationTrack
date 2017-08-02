using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VacationTrack
{
    public partial class frmCategory : Form
    {
        VacationEntities VacDB = new VacationEntities();

        int position = 0;
        int maxx;
        string categType;
        string vacationCateg;

        public frmCategory()
        {
            InitializeComponent();
        }

        private void frmCategory_Load(object sender, EventArgs e)
        {

            this.Size = new Size(631, 444);
            var fillCategory = VacDB.Categories.ToList();
            dgCat.DataSource = fillCategory;
            loadGrid();
            dgCat.Hide();

            position = 1;
            maxx = fillCategory.Count;
            txtRecCount.Text = position + " of " + maxx + " records";

            txtID.Text = fillCategory[0].ID.ToString();
            txtCode.Text = fillCategory[0].CategoryCode;
            if (gbVacationType.Equals("PTO"))
            {
                rbPTO.PerformClick();
            }
            else
            {
                rbNonPTO.PerformClick();
            }
            if (gbVacationCategory.Equals("Holiday"))
            {
                rbHoliday.PerformClick();
            }
            else if (gbVacationCategory.Equals("Personal Time"))
            {
                rbPersonalTime.PerformClick();
            }
            else
            {
                rbFamMedLeave.PerformClick();
            }
            txtName.Text = fillCategory[0].Name;
            txtComments.Text = fillCategory[0].Remarks;
            btnSave.Hide();
        }

        private void loadGrid()
        {
            dgCat.Columns[0].Visible = false;
            dgCat.Columns[1].Visible = false;
            dgCat.Columns[2].Visible = false;
            dgCat.Columns[3].Visible = false;
            dgCat.Columns[4].HeaderText = "Vacation Name";
            dgCat.Columns[4].Width = 310;
            dgCat.Columns[5].Visible = false;
            dgCat.Columns[6].Visible = false;
            dgCat.Columns[7].Visible = false;
            dgCat.EnableHeadersVisualStyles = false;
            dgCat.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dgCat.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgCat.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void navRecord()
        {
            var fillCategory = VacDB.Categories.ToList();

            txtID.Text = fillCategory[position].ID.ToString();
            txtCode.Text = fillCategory[position].CategoryCode;
            txtName.Text = fillCategory[position].Name;

            if (fillCategory[position].CategoryType.Equals("PTO"))
            {
                rbPTO.PerformClick();
            }
            else if (fillCategory[position].CategoryType.Equals("Non-PTO"))
            {
                rbNonPTO.PerformClick();
            }

            if (fillCategory[position].VacationCategory.Equals("Holiday"))
            {
                rbHoliday.PerformClick();
            }
            else if (fillCategory[position].VacationCategory.Equals("Personal Time"))
            {
                rbPersonalTime.PerformClick();
            }
            else if (fillCategory[position].VacationCategory.Equals("Family and Medical Leave"))
            {
                rbFamMedLeave.PerformClick();
            }
            txtComments.Text = fillCategory[position].Remarks;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (position != maxx - 1)
            {
                position++;
                navRecord();
                txtRecCount.Text = position + " of " + maxx + " records";
            }
            else
            {
                MessageBox.Show("You've reached the last record", "INFORMATION", MessageBoxButtons.OK);
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (position > 0)
            {
                position--;
                navRecord();
                txtRecCount.Text = position + 1 + " of " + maxx + " records";
            }
            else if (position == -1)
            {
                MessageBox.Show("No record yet", "INFORMATION", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("You've reached the first record",
                    "INFORMATION", MessageBoxButtons.OK);
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            position = 0;
            navRecord();
            txtRecCount.Text = position + 1 + " of " + maxx + " records";
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            position = maxx - 1;
            navRecord();
            txtRecCount.Text = position + 1 + " of " + maxx + " records";
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            rbPTO.Checked = false;
            rbNonPTO.Checked = false;
            rbHoliday.Checked = false;
            rbFamMedLeave.Checked = false;
            rbPersonalTime.Checked = false;
            txtCode.Text = "";
            txtCode.Enabled = false;
            txtName.Text = "";
            txtComments.Text = "";
            btnSave.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            VacationEntities VE = new VacationEntities();
            using (VE)
            {
                VE.Categories.RemoveRange(VE.Categories.Where(c => c.CategoryCode == txtCode.Text));
                DialogResult result =
                            MessageBox.Show("Deletion of data is irreversible",
                            "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    MessageBox.Show("Operation Cancelled", "INFORMATION", MessageBoxButtons.OK);
                }
                else if (result == DialogResult.Yes)
                {
                    MessageBox.Show("Record deleted", "INFORMATION", MessageBoxButtons.OK);
                    VE.SaveChanges();
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            frmCategory.ActiveForm.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void rbPTO_CheckedChanged(object sender, EventArgs e)
        {
            categType = "PTO";
        }

        private void rbNonPTO_CheckedChanged(object sender, EventArgs e)
        {
            categType = "Non-PTO";
        }

        private void rbHoliday_CheckedChanged(object sender, EventArgs e)
        {
            vacationCateg = "Holiday";
        }

        private void rbPersonalTime_CheckedChanged(object sender, EventArgs e)
        {
            vacationCateg = "Personal Time";
        }

        private void rbFamMedLeave_CheckedChanged(object sender, EventArgs e)
        {
            vacationCateg = "Family and Medical Leave";
        }

        public void btnAll_Click(object sender, EventArgs e)
        {
            var fillCategory = VacDB.Categories.ToList();
            dgCat.DataSource = fillCategory;
            dgCat.Show();
            this.Size = new Size(929, 444);
            dgCat.Size = new Size(300, 396);
        }

        private void dgCat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgCat.Rows[e.RowIndex];
                txtID.Text = row.Cells["ID"].Value.ToString();

                if (row.Cells["CategoryType"].Value.Equals("PTO"))
                {
                    rbPTO.PerformClick();
                }
                else if (row.Cells["CategoryType"].Value.Equals("Non-PTO"))
                {
                    rbNonPTO.PerformClick();
                }
                if (row.Cells["VacationCategory"].Value.Equals("Holiday"))
                {
                    rbHoliday.PerformClick();
                }
                else if (row.Cells["VacationCategory"].Value.Equals("Personal Time"))
                {
                    rbPersonalTime.PerformClick();
                }
                else if (row.Cells["VacationCategory"].Value.Equals("Family and Medical Leave"))
                {
                    rbFamMedLeave.PerformClick();
                }
                txtCode.Text = row.Cells["CategoryCode"].Value.ToString();
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtComments.Text = row.Cells["Remarks"].Value.ToString();
            }
            dgCat.Hide();
            this.Size = new Size(631, 444);
            dgCat.Size = new Size(10, 396);
        }

        private void loadFilteredgrid()
        {
            dgCat.Columns[0].Visible = false;
            dgCat.Columns[1].Visible = false;
            dgCat.Columns[2].Visible = false;
            dgCat.Columns[3].Visible = false;
            dgCat.Columns[4].HeaderText = "Vacation Name";
            dgCat.Columns[4].Width = 310;
            dgCat.Columns[5].Visible = false;
            dgCat.Columns[6].Visible = false;
            dgCat.Columns[7].Visible = false;
            dgCat.EnableHeadersVisualStyles = false;
            dgCat.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dgCat.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgCat.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        private void btnA_Click(object sender, EventArgs e)
        {
            if (btnA.ContainsFocus)
            {
                var filteredList = VacDB.Categories.Where(c => c.Name.StartsWith("A")).ToList();
                dgCat.DataSource = filteredList;
                loadFilteredgrid();
                if (filteredList.Any())
                {
                    dgCat.Show();
                    this.Size = new Size(929, 444);
                    dgCat.Size = new Size(300, 396);
                }
                else
                {
                    dgCat.Hide();
                }
            }
        }

        private void btnB_Click(object sender, EventArgs e)
        {
            if (btnB.ContainsFocus)
            {
                var filteredList = VacDB.Categories.Where(c => c.Name.StartsWith("B")).ToList();
                dgCat.DataSource = filteredList;
                loadFilteredgrid();
                if (filteredList.Any())
                {
                    dgCat.Show();
                    this.Size = new Size(929, 444);
                    dgCat.Size = new Size(300, 396);
                }
                else
                {
                    dgCat.Hide();
                }
            }
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            if (btnC.ContainsFocus)
            {
                var filteredList = VacDB.Categories.Where(c => c.Name.StartsWith("C")).ToList();
                dgCat.DataSource = filteredList;
                loadFilteredgrid();
                if (filteredList.Any())
                {
                    dgCat.Show();
                    this.Size = new Size(929, 444);
                    dgCat.Size = new Size(300, 396);
                }
                else
                {
                    dgCat.Hide();
                }
            }
        }

        private void btnD_Click(object sender, EventArgs e)
        {
            if (btnD.ContainsFocus)
            {
                var filteredList = VacDB.Categories.Where(c => c.Name.StartsWith("D")).ToList();
                dgCat.DataSource = filteredList;
                loadFilteredgrid();
                if (filteredList.Any())
                {
                    dgCat.Show();
                    this.Size = new Size(929, 444);
                    dgCat.Size = new Size(300, 396);
                }
                else
                {
                    dgCat.Hide();
                }
            }
        }

        private void btnE_Click(object sender, EventArgs e)
        {
            if (btnE.ContainsFocus)
            {
                var filteredList = VacDB.Categories.Where(c => c.Name.StartsWith("E")).ToList();
                dgCat.DataSource = filteredList;
                loadFilteredgrid();
                if (filteredList.Any())
                {
                    dgCat.Show();
                    this.Size = new Size(929, 444);
                    dgCat.Size = new Size(300, 396);
                }
                else
                {
                    dgCat.Hide();
                }
            }
        }

        private void btnF_Click(object sender, EventArgs e)
        {
            if (btnF.ContainsFocus)
            {
                var filteredList = VacDB.Categories.Where(c => c.Name.StartsWith("F")).ToList();
                dgCat.DataSource = filteredList;
                loadFilteredgrid();
                if (filteredList.Any())
                {
                    dgCat.Show();
                    this.Size = new Size(929, 444);
                    dgCat.Size = new Size(300, 396);
                }
                else
                {
                    dgCat.Hide();
                }
            }
        }

        private void btnG_Click(object sender, EventArgs e)
        {
            if (btnG.ContainsFocus)
            {
                var filteredList = VacDB.Categories.Where(c => c.Name.StartsWith("G")).ToList();
                dgCat.DataSource = filteredList;
                loadFilteredgrid();
                if (filteredList.Any())
                {
                    dgCat.Show();
                    this.Size = new Size(929, 444);
                    dgCat.Size = new Size(300, 396);
                }
                else
                {
                    dgCat.Hide();
                }
            }
        }

        private void btnH_Click(object sender, EventArgs e)
        {
            if (btnH.ContainsFocus)
            {
                var filteredList = VacDB.Categories.Where(c => c.Name.StartsWith("H")).ToList();
                dgCat.DataSource = filteredList;
                loadFilteredgrid();
                if (filteredList.Any())
                {
                    dgCat.Show();
                    this.Size = new Size(929, 444);
                    dgCat.Size = new Size(300, 396);
                }
                else
                {
                    dgCat.Hide();
                }
            }
        }

        private void btnI_Click(object sender, EventArgs e)
        {
            if (btnI.ContainsFocus)
            {
                var filteredList = VacDB.Categories.Where(c => c.Name.StartsWith("I")).ToList();
                dgCat.DataSource = filteredList;
                loadFilteredgrid();
                if (filteredList.Any())
                {
                    dgCat.Show();
                    this.Size = new Size(929, 444);
                    dgCat.Size = new Size(300, 396);
                }
                else
                {
                    dgCat.Hide();
                }
            }
        }

        private void btnJ_Click(object sender, EventArgs e)
        {
            if (btnJ.ContainsFocus)
            {
                var filteredList = VacDB.Categories.Where(c => c.Name.StartsWith("J")).ToList();
                dgCat.DataSource = filteredList;
                loadFilteredgrid();
                if (filteredList.Any())
                {
                    dgCat.Show();
                    this.Size = new Size(929, 444);
                    dgCat.Size = new Size(300, 396);
                }
                else
                {
                    dgCat.Hide();
                }
            }
        }

        private void btnK_Click(object sender, EventArgs e)
        {
            if (btnK.ContainsFocus)
            {
                var filteredList = VacDB.Categories.Where(c => c.Name.StartsWith("K")).ToList();
                dgCat.DataSource = filteredList;
                loadFilteredgrid();
                if (filteredList.Any())
                {
                    dgCat.Show();
                    this.Size = new Size(929, 444);
                    dgCat.Size = new Size(300, 396);
                }
                else
                {
                    dgCat.Hide();
                }
            }
        }

        private void btnL_Click(object sender, EventArgs e)
        {
            if (btnL.ContainsFocus)
            {
                var filteredList = VacDB.Categories.Where(c => c.Name.StartsWith("L")).ToList();
                dgCat.DataSource = filteredList;
                loadFilteredgrid();
                if (filteredList.Any())
                {
                    dgCat.Show();
                    this.Size = new Size(929, 444);
                    dgCat.Size = new Size(300, 396);
                }
                else
                {
                    dgCat.Hide();
                }
            }
        }

        private void btnM_Click(object sender, EventArgs e)
        {
            if (btnM.ContainsFocus)
            {
                var filteredList = VacDB.Categories.Where(c => c.Name.StartsWith("M")).ToList();
                dgCat.DataSource = filteredList;
                loadFilteredgrid();
                if (filteredList.Any())
                {
                    dgCat.Show();
                    this.Size = new Size(929, 444);
                    dgCat.Size = new Size(300, 396);
                }
                else
                {
                    dgCat.Hide();
                }
            }
        }

        private void btnN_Click(object sender, EventArgs e)
        {
            if (btnN.ContainsFocus)
            {
                var filteredList = VacDB.Categories.Where(c => c.Name.StartsWith("N")).ToList();
                dgCat.DataSource = filteredList;
                loadFilteredgrid();
                if (filteredList.Any())
                {
                    dgCat.Show();
                    this.Size = new Size(929, 444);
                    dgCat.Size = new Size(300, 396);
                }
                else
                {
                    dgCat.Hide();
                }
            }
        }

        private void btnO_Click(object sender, EventArgs e)
        {
            if (btnO.ContainsFocus)
            {
                var filteredList = VacDB.Categories.Where(c => c.Name.StartsWith("O")).ToList();
                dgCat.DataSource = filteredList;
                loadFilteredgrid();
                if (filteredList.Any())
                {
                    dgCat.Show();
                    this.Size = new Size(929, 444);
                    dgCat.Size = new Size(300, 396);
                }
                else
                {
                    dgCat.Hide();
                }
            }
        }

        private void btnP_Click(object sender, EventArgs e)
        {
            if (btnP.ContainsFocus)
            {
                var filteredList = VacDB.Categories.Where(c => c.Name.StartsWith("P")).ToList();
                dgCat.DataSource = filteredList;
                loadFilteredgrid();
                if (filteredList.Any())
                {
                    dgCat.Show();
                    this.Size = new Size(929, 444);
                    dgCat.Size = new Size(300, 396);
                }
                else
                {
                    dgCat.Hide();
                }
            }
        }

        private void btnQ_Click(object sender, EventArgs e)
        {
            if (btnQ.ContainsFocus)
            {
                var filteredList = VacDB.Categories.Where(c => c.Name.StartsWith("Q")).ToList();
                dgCat.DataSource = filteredList;
                loadFilteredgrid();
                if (filteredList.Any())
                {
                    dgCat.Show();
                    this.Size = new Size(929, 444);
                    dgCat.Size = new Size(300, 396);
                }
                else
                {
                    dgCat.Hide();
                }
            }
        }

        private void btnR_Click(object sender, EventArgs e)
        {
            if (btnR.ContainsFocus)
            {
                var filteredList = VacDB.Categories.Where(c => c.Name.StartsWith("R")).ToList();
                dgCat.DataSource = filteredList;
                loadFilteredgrid();
                if (filteredList.Any())
                {
                    dgCat.Show();
                    this.Size = new Size(929, 444);
                    dgCat.Size = new Size(300, 396);
                }
                else
                {
                    dgCat.Hide();
                }
            }
        }

        private void btnS_Click(object sender, EventArgs e)
        {
            if (btnS.ContainsFocus)
            {
                var filteredList = VacDB.Categories.Where(c => c.Name.StartsWith("S")).ToList();
                dgCat.DataSource = filteredList;
                loadFilteredgrid();
                if (filteredList.Any())
                {
                    dgCat.Show();
                    this.Size = new Size(929, 444);
                    dgCat.Size = new Size(300, 396);
                }
                else
                {
                    dgCat.Hide();
                }
            }
        }

        private void btnT_Click(object sender, EventArgs e)
        {
            if (btnT.ContainsFocus)
            {
                var filteredList = VacDB.Categories.Where(c => c.Name.StartsWith("T")).ToList();
                dgCat.DataSource = filteredList;
                loadFilteredgrid();
                if (filteredList.Any())
                {
                    dgCat.Show();
                    this.Size = new Size(929, 444);
                    dgCat.Size = new Size(300, 396);
                }
                else
                {
                    dgCat.Hide();
                }
            }
        }

        private void btnU_Click(object sender, EventArgs e)
        {
            if (btnU.ContainsFocus)
            {
                var filteredList = VacDB.Categories.Where(c => c.Name.StartsWith("U")).ToList();
                dgCat.DataSource = filteredList;
                loadFilteredgrid();
                if (filteredList.Any())
                {
                    dgCat.Show();
                    this.Size = new Size(929, 444);
                    dgCat.Size = new Size(300, 396);
                }
                else
                {
                    dgCat.Hide();
                }
            }
        }

        private void btnV_Click(object sender, EventArgs e)
        {
            if (btnV.ContainsFocus)
            {
                var filteredList = VacDB.Categories.Where(c => c.Name.StartsWith("V")).ToList();
                dgCat.DataSource = filteredList;
                loadFilteredgrid();
                if (filteredList.Any())
                {
                    dgCat.Show();
                    this.Size = new Size(929, 444);
                    dgCat.Size = new Size(300, 396);
                }
                else
                {
                    dgCat.Hide();
                }
            }
        }

        private void btnW_Click(object sender, EventArgs e)
        {
            if (btnW.ContainsFocus)
            {
                var filteredList = VacDB.Categories.Where(c => c.Name.StartsWith("W")).ToList();
                dgCat.DataSource = filteredList;
                loadFilteredgrid();
                if (filteredList.Any())
                {
                    dgCat.Show();
                    this.Size = new Size(929, 444);
                    dgCat.Size = new Size(300, 396);
                }
                else
                {
                    dgCat.Hide();
                }
            }
        }

        private void btnX_Click(object sender, EventArgs e)
        {
            if (btnX.ContainsFocus)
            {
                var filteredList = VacDB.Categories.Where(c => c.Name.StartsWith("X")).ToList();
                dgCat.DataSource = filteredList;
                loadFilteredgrid();
                if (filteredList.Any())
                {
                    dgCat.Show();
                    this.Size = new Size(929, 444);
                    dgCat.Size = new Size(300, 396);
                }
                else
                {
                    dgCat.Hide();
                }
            }
        }

        private void btnY_Click(object sender, EventArgs e)
        {
            if (btnY.ContainsFocus)
            {
                var filteredList = VacDB.Categories.Where(c => c.Name.StartsWith("Y")).ToList();
                dgCat.DataSource = filteredList;
                loadFilteredgrid();
                if (filteredList.Any())
                {
                    dgCat.Show();
                    this.Size = new Size(929, 444);
                    dgCat.Size = new Size(300, 396);
                }
                else
                {
                    dgCat.Hide();
                }
            }
        }

        private void btnZ_Click(object sender, EventArgs e)
        {
            if (btnZ.ContainsFocus)
            {
                var filteredList = VacDB.Categories.Where(c => c.Name.StartsWith("Z")).ToList();
                dgCat.DataSource = filteredList;
                loadFilteredgrid();
                if (filteredList.Any())
                {
                    dgCat.Show();
                    this.Size = new Size(929, 444);
                    dgCat.Size = new Size(300, 396);
                }
                else
                {
                    MessageBox.Show("No record for this letter", "INFORMATION", MessageBoxButtons.OK);
                    dgCat.Hide();
                }
            }
        }
    }
}