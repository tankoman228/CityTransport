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
            new TextResizer(this);

            db = new DB();
            reload();
            
            Closing += (sender, args) =>
            {
                reload();
            };

            btnSave.Click += (sender, args) => reload();
            btnAdd.Click += BtnAdd_Click;
            btnDelete.Click += BtnDelete_Click;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (DB db = new DB())
                {
                    foreach (DB_Objects.Carrier c in dgTableMain.SelectedItems)
                    {
                        db.carrier.Remove(db.carrier.
                            Where(x => x.ID_Carrier == c.ID_Carrier).First());
                    }
                    db.SaveChanges();
                }
                reload();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't delete these items : \n\n"+ex.Message, "Delete error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            using (DB db = new DB())
            {
                var c = new DB_Objects.Carrier
                {
                    Head = "head",
                    Name = "name",
                    Phone = "phone"
                };
                db.carrier.Add(c);
                db.SaveChanges();
            }
            reload();
        }

        public void reload()
        {
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update error : \n\n" + ex.Message, "Can't save these changes",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            db = new DB();
            var c = db.carrier.ToList();
            dgTableMain.ItemsSource = c;
        }
    }
}
