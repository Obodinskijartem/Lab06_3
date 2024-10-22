using Lab06_3;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Lab06_3   
{
    public partial class fSubscriber : Form
    {
        internal IClient TheClient;

        internal fSubscriber(IClient client)
        {
            TheClient = client;
            InitializeComponent();
        }

        private void fSubscriber_Load(object sender, EventArgs e)
        {
            tbName.Text = TheClient.Name;
            tbPhoneNumber.Text = TheClient.PhoneNumber;
            tbAddress.Text = TheClient.Address;
            tbSMSPerMonth.Text = TheClient.CallMinutesPerMonth.ToString();
            tbCallMinutes.Text = TheClient.SMSPerMonth.ToString();
            tbMonthlyFee.Text = TheClient.MonthlyFee.ToString();

            chbHasDataPlan.Checked = TheClient.HasDataPlan;
            chbHasRoaming.Checked = TheClient.HasRoaming;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbName.Text) ||
                string.IsNullOrWhiteSpace(tbPhoneNumber.Text) ||
                string.IsNullOrWhiteSpace(tbAddress.Text) ||
                !int.TryParse(tbSMSPerMonth.Text, out var smsPerMonth) ||
                !int.TryParse(tbCallMinutes.Text, out var callMinutes) ||
                !double.TryParse(tbMonthlyFee.Text, out var monthlyFee))
            {
                MessageBox.Show("Будь ласка, заповніть всі поля правильно.");
                return;
            }

            TheClient.Name = tbName.Text;
            TheClient.PhoneNumber = tbPhoneNumber.Text;
            TheClient.Address = tbAddress.Text;
            TheClient.CallMinutesPerMonth = smsPerMonth;
            TheClient.SMSPerMonth = callMinutes;
            TheClient.MonthlyFee = monthlyFee;
            TheClient.HasRoaming = chbHasRoaming.Checked;
            TheClient.HasDataPlan = chbHasDataPlan.Checked;

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