using CityTransport.Forms.Items;
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
    /// Логика взаимодействия для RouteEdit.xaml
    /// </summary>
    public partial class RouteEdit : Window
    {
        internal static DB_Objects.Route Route { get; set; }

        public RouteEdit()
        {
            InitializeComponent();

            new TextResizer(this);

            #region basic
            tbRouteNumber.Text = Route.RouteNumber;
            tbInWayMinutes.Text = Route.TimeInWayMinutes.ToString();

            cbMonday.IsChecked = Route.onMonday;
            cbTuesday.IsChecked = Route.onTuesday;
            cbWednesday.IsChecked = Route.onTuesday;
            cbThursday.IsChecked = Route.onThursday;
            cbFriday.IsChecked = Route.onFriday;
            cbSaturday.IsChecked = Route.onSaturday;
            cbSunday.IsChecked = Route.onSunday;

            btnSaveBasic.Click += BtnSaveBasic_Click;
            #endregion

            #region schedule

            using (DB db = new DB())
            {
                var stops = db.stop;
                foreach (var stop in stops)
                {
                    StopItem item = new StopItem { S = stop };
                    cbStop.Items.Add(item);
                }

            }

            #endregion
        }

        private void BtnSaveBasic_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DB())
                {
                    DB_Objects.Route r = db.route.Where(x => x.ID_Route == Route.ID_Route).First();

                    if (tbRouteNumber.Text == "")
                        throw new Exception("Number can't be empty");

                    int inwayminutes = 0;
                    if (!int.TryParse(tbInWayMinutes.Text, out inwayminutes))
                        throw new Exception("Minutes must be integer and anything else");


                    r.RouteNumber = tbRouteNumber.Text;
                    r.TimeInWayMinutes = inwayminutes;

                    r.onMonday = cbMonday.IsChecked == true;
                    r.onTuesday = cbTuesday.IsChecked == true;
                    r.onWednesday = cbWednesday.IsChecked == true;
                    r.onThursday = cbThursday.IsChecked == true;
                    r.onFriday = cbFriday.IsChecked == true;
                    r.onSaturday = cbSaturday.IsChecked == true;
                    r.onSunday = cbSunday.IsChecked == true;

                    db.SaveChanges();
                    MessageBox.Show("Success", "Update Route",
                   MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update error: " + ex.Message, "Save to database error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
