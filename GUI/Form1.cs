using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //DTO.BusinessDTO business = new DTO.BusinessDTO()
            //{
            //    BusinessName = "aaa",
            //    Address_street = "bbbbb",
            //    Address_city = "bbbbbbb",
            //    Address_numOfBuilding = 8,
            //    Password = "abc"

            //};
            //DAL.BusinessDal.AddBusiness(BL.converters.BusinessConverters.GetBusiness(business));
            /*
            DTO.CustomerInLineDTO customerInLine = new DTO.CustomerInLineDTO()
            {
                ActivityTimeId = 8,
                CustId = 2,
                EstimatedHour = DateTime.Now,
                StatusTurn = 2,
                PreAlert = 0,

            };
            DAL.TurnDal.AddAppointment(BL.converters.CustomerInLineConvrters.GetCustomerInLine(customerInLine));
            */

           string token= BL.Token.GetToken("name", "05354345");
            //string phone = BL.Token.GetPhoneFromToken(token);
           // textBox1.Text = phone;
        }
    }
}
