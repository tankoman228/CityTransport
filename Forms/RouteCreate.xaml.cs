using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для RouteCreate.xaml
    /// </summary>
    public partial class RouteCreate : Window
    {
        public RouteCreate()
        {
            InitializeComponent();

            using (var db = new DB())
            {
                foreach (var c in db.carrier)
                {
                    cbCarrier.Items.Add(c.Name);
                }
                foreach (var t in db.transportType)
                {
                    cbType.Items.Add(t.Name);
                }
            }

            btnSave.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cbCarrier.SelectedIndex == -1 || tbRouteNumber.Text == ""
                || cbType.SelectedIndex == -1)
            {
                MessageBox.Show("Fill all the fields to create new route", "can't create this route",
                    MessageBoxButton.OK, MessageBoxImage.Question);
                return;
            }

            using (var db = new DB())
            {
                int id_c = db.carrier.Where(x => 
                    x.Name == cbCarrier.SelectedItem.ToString()).First().ID_Carrier;

                int id_t = db.transportType.Where(x =>
                    x.Name == cbType.SelectedItem.ToString()).First().ID_TransportType;

                var r = new DB_Objects.Route
                {
                    ID_Carrier = id_c,
                    ID_TransportType = id_t,
                    RouteNumber = tbRouteNumber.Text
                };

                try
                {
                    db.route.Add(r);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "can't create this route",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            MessageBox.Show("Success!", "Route created",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
    }
}
