using EFDatabaseFirst.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EFDatabaseFirst
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        NorthwindEntities db = new NorthwindEntities();

        public void Cleaner()
        {
            if (dataGridView1.DataSource != null)
            {
                dataGridView1.DataSource = "";
            }
        }
        private void btnProductAndSupplier_Click(object sender, EventArgs e)
        {
            Cleaner();

            dataGridView1.DataSource = db.Products.OrderByDescending
                (d => d.UnitPrice).Select(x => new
                {
                    SupplierCompanyName = x.Supplier.CompanyName,
                    SupplierContactName = x.Supplier.ContactName,
                    x.ProductName,
                    x.UnitPrice,
                    x.UnitsInStock
                }).ToList();
        }

        private void btnTotalPrice_Click(object sender, EventArgs e)
        {
            Cleaner();

            dataGridView1.DataSource = db.Products.OrderByDescending
                (i => i.UnitPrice).
                GroupBy(x => x.Category.CategoryName).Select(y => new
                {
                    CategoryName = y.Key,
                    TotalPrice = y.Sum(u => u.UnitPrice)
                }).ToList();
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            Cleaner();

            dataGridView1.DataSource = db.Orders.Select(o => new
            {
                o.Customer.CompanyName,
                FullName = o.Employee.FirstName + " " + o.Employee.LastName,
                o.OrderID,
                o.OrderDate,
                ShipperCompanyName = o.Shipper.CompanyName
            }).ToList();
        }

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            Cleaner();

            dataGridView1.DataSource = db.Employees.OrderBy
                (o => o.BirthDate).Select(x => new
                {
                    FullName = x.FirstName + " " + x.LastName,
                    x.BirthDate,
                    Age = SqlFunctions.DateDiff("Year", x.BirthDate, DateTime.Now)
                }).ToList();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Cleaner();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show
                  ("Do you want to quit?", "EFDatabaseFirst", MessageBoxButtons.YesNo);

            if (dialog == DialogResult.Yes)
            {
                Application.Exit();
            }

            else if (dialog == DialogResult.No)
            {

            }
        }
    }

}
