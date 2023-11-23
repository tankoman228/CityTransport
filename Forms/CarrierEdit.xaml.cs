using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CityTransport.Forms
{
    /// <summary>
    /// Логика взаимодействия для CarrierEdit.xaml
    /// </summary>
    public partial class CarrierEdit : Window
    {
        DB db;

        public CarrierEdit()
        {
            InitializeComponent();

            reload();
            
            Closing += (sender, args) =>
            {
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            };
        }

        public void reload()
        {
            if (db != null)
                db.SaveChanges();

            db = new DB();
            var c = db.carrier.ToList();
            dgTableMain.ItemsSource = c;
        }
    }
}
