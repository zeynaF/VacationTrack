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
    public partial class frmEmployee : Form
    {
        VacationEntities VacDB = new VacationEntities();
        string genderType;
        int position, maxx;

        public frmEmployee()
        {
            InitializeComponent();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            txtEmpID.Text = "";
            txtCorporation.Text = "";
            txtCodePerson.Text = "";
            txtPersonType.Text = "";
            txtFName.Text = "";
            txtMName.Text = "";
            txtLName.Text = "";
            txtAddress.Text = "";
            txtBDate.Text = "";
            rbMale.Checked = false;
            rbFemale.Checked = false;
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtFax.Text = "";
            txtSSN.Text = "";
            txtWorkHours.Text = "";
            cboManager.Text = "";
            txtHireDate.Text = "";
            txtCompensation.Text = "";
            txtComments.Text = "";
            txtVacationDays.Text = "";
            txtSickDays.Text = "";
            btnSave.Show();
        }

        private void frmEmployee_Load(object sender, EventArgs e)
        {
            this.Size = new Size(913, 526);
            //loadGrid();
            dgEmployee.Hide();
            btnSave.Hide();

            var fillEmployee = VacDB.People.ToList();
            dgEmployee.DataSource = fillEmployee;

            cboManager.DataSource = fillEmployee;
            cboManager.ValueMember = "ManagerID";
            cboManager.DisplayMember = "FirstName";

            position = 1;
            maxx = fillEmployee.Count;
            txtRecCount.Text = position + " of " + maxx + "records";

            txtEmpID.Text = fillEmployee[0].PersonID.ToString();
            txtPersonType.Text = fillEmployee[0].PersonType.ToString();
            txtCodePerson.Text = fillEmployee[0].PersonCode.ToString();
            txtCompensation.Text = fillEmployee[0].Compensation.ToString();
            txtFName.Text = fillEmployee[0].FirstName.ToString();
            txtMName.Text = fillEmployee[0].MiddleName.ToString();
            txtLName.Text = fillEmployee[0].LastName.ToString();
            txtBDate.Text = String.Format("{0:MM/dd/yyyy}", fillEmployee[0].BirthDate);
            txtSSN.Text = fillEmployee[0].SSN.ToString();
            txtAddress.Text = fillEmployee[0].Address.ToString();
            if (gbGender.Equals("Male"))
            {
                rbMale.PerformClick();
            }
            else if (gbGender.Equals("Female"))
            {
                rbFemale.PerformClick();
            }

            cboManager.SelectedItem = Convert.ToInt32(fillEmployee[0].ManagerID);
            txtIsManager.Text = fillEmployee[0].isManager;
            txtHireDate.Text = String.Format("{0:MM/dd/yyyy}",fillEmployee[0].HireDate);
            txtWorkHours.Text = fillEmployee[0].WorkHours.ToString();
            txtCorporation.Text = fillEmployee[0].Corporation.ToString();
            txtVacationDays.Text = fillEmployee[0].AccumulatedVacation.ToString();
            txtSickDays.Text = fillEmployee[0].AccumulatedSickDays.ToString();
            txtPhone.Text = fillEmployee[0].Telephone;
            txtFax.Text = fillEmployee[0].Fax;
            txtEmail.Text = fillEmployee[0].Email;
            txtComments.Text = fillEmployee[0].Remarks;

            btnSave.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            {
                using (VacDB)
                {
                    var employee = VacDB.Set<Person>();
                    var maxEmpNumber = employee.Max(x => x.PersonID);
                    employee.Add(new Person
                    {
                        PersonCode = "Emp" + maxEmpNumber + 1,
                        PersonType = "employee",
                        Corporation = "people",
                        FirstName = txtFName.Text,
                        MiddleName = txtMName.Text,
                        LastName = txtLName.Text,
                        BirthDate = Convert.ToDateTime(txtBDate.Text),
                        SSN = txtSSN.Text,
                        Gender = genderType,
                        Address = txtAddress.Text,
                        Telephone = txtPhone.Text,
                        Fax = txtFax.Text,
                        Email = txtEmail.Text,
                        //ManagerID = cboManager.SelectedItem.ToString,
                        HireDate = Convert.ToDateTime(txtHireDate.Text),
                        WorkHours = Convert.ToInt32(txtWorkHours.Text),
                        Compensation = Convert.ToInt32(txtCompensation.Text),
                        AccumulatedVacation = Convert.ToInt32(txtVacationDays.Text),
                        AccumulatedSickDays = Convert.ToInt32(txtSickDays.Text),
                    });
                    VacDB.SaveChanges();
                    MessageBox.Show("Record saved", "INFORMATION", MessageBoxButtons.OK);
                }
            }
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            genderType = "Male";
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            genderType = "Female";
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (position != maxx - 1)
            {
                position++;
                navigateRecord();
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
                navigateRecord();
                txtRecCount.Text = position + 1 + " of " + maxx + " records";
            }
            else if (position == -1)
            {
                MessageBox.Show("No record yet", "INFORMATION", MessageBoxButtons.OK);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void navigateRecord()
        {
            var fillEmployee = VacDB.People.ToList();

            txtEmpID.Text = fillEmployee[position].PersonID.ToString();
            txtPersonType.Text = fillEmployee[position].PersonType.ToString();
            txtCodePerson.Text = fillEmployee[position].PersonCode;
            txtCorporation.Text = fillEmployee[position].Corporation;
            txtFName.Text = fillEmployee[position].FirstName;
            txtMName.Text = fillEmployee[position].MiddleName;
            txtLName.Text = fillEmployee[position].LastName;
            txtBDate.Text = String.Format("{0:MM/dd/yyyy}", fillEmployee[position].BirthDate);
            txtSSN.Text = fillEmployee[position].SSN;
            txtAddress.Text = fillEmployee[position].Address;
            if (fillEmployee[position].Gender.Equals("Male"))
            {
                rbMale.PerformClick();
            }
            else if (fillEmployee[position].Gender.Equals("Female"))
            {
                rbFemale.PerformClick();
            }
            cboManager.Text = fillEmployee[position].ManagerID.ToString();
            txtIsManager.Text = fillEmployee[position].isManager;
            txtHireDate.Text = String.Format("{0:MM/dd/yyyy}", fillEmployee[position].HireDate);
            txtWorkHours.Text = fillEmployee[position].WorkHours.ToString();
            txtCompensation.Text = fillEmployee[position].Compensation.ToString();
            txtVacationDays.Text = fillEmployee[position].AccumulatedVacation.ToString();
            txtSickDays.Text = fillEmployee[position].AccumulatedSickDays.ToString();
            txtPhone.Text = fillEmployee[position].Telephone;
            txtFax.Text = fillEmployee[position].Fax;
            txtEmail.Text = fillEmployee[position].Email;
            txtComments.Text = fillEmployee[position].Remarks;
        }
    }
}
