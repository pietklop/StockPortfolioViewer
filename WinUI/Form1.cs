using System;
using System.Linq;
using System.Windows.Forms;
using DAL;
using DAL.Entities;
using Services;

namespace WinUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var db = new StockDbContext())
            {
                var curr = db.GetUserCurrency();
                var sector = db.Sectors.First();

//                var ss = new StockService(db);
//                ss.GetOrCreateStock("NFLX", "123", curr, sector);

                db.SaveChanges();
            }
        }
    }
}
