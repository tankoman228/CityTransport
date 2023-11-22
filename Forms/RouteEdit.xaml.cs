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
            upd_from_database();

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

            btnAddArrival.Click += BtnAddArrival_Click;
            btnAddStopAfter.Click += BtnAddStopAfter_Click;
            btnAddStopBefore.Click += BtnAddStopBefore_Click;
            btnDeleteArrival.Click += BtnDeleteArrival_Click;
            btnDeleteStop.Click += BtnDeleteStop_Click;
        }

        public void upd_from_database()
        {
            using (DB db = new DB())
            {
                cbStop.Items.Clear();
                lbStops.Items.Clear();
                cbStopForSchedule.Items.Clear();
                lbSchedule.Items.Clear();

                var stops = db.stop;
                foreach (var stop in stops)
                {
                    StopItem item = new StopItem { S = stop };
                    cbStop.Items.Add(item);
                }

                var route_stops = db.route_stop.Include("Stop").
                    Where(x => x.ID_Route == Route.ID_Route).ToList();
                foreach (var stop in route_stops)
                {
                    StopItem item = new StopItem { RS = stop, S = stop.Stop };
                    cbStopForSchedule.Items.Add(item);
                    lbStops.Items.Add(item);
                }

                var schedule = db.route_schedule.Include("RouteStop").Include("RouteStop.Stop").
                    Where(x => x.RouteStop.ID_Route == Route.ID_Route).ToList();
                foreach (var stop in schedule)
                {
                    lbSchedule.Items.Add(new ScheduleItem { S = stop });
                }
            }
        }

        private void BtnDeleteStop_Click(object sender, RoutedEventArgs e)
        {
            if (lbStops.SelectedIndex == -1)
            {
                MessageBox.Show("Choose stop from right list first", "Can't find item", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                using (DB db = new DB())
                {
                    var s = db.route_stop.Where(x => x.ID_Route == Route.ID_Route && 
                    x.Stop.ID_Stop == ((StopItem)lbStops.SelectedItem).S.ID_Stop).FirstOrDefault();

                    var stops = db.route_stop.Where(x => x.ID_Route == Route.ID_Route).ToList();
                    foreach (var stop in stops)
                    {
                        if (stop.NumInWay > s.NumInWay)
                            stop.NumInWay--;
                    }

                    db.route_stop.Remove(s);
                    db.SaveChanges();
                    upd_from_database();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't change this data: \n\n" + ex.Message , "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnDeleteArrival_Click(object sender, RoutedEventArgs e)
        {
            if (lbSchedule.SelectedIndex == -1)
            {
                MessageBox.Show("Choose arrival from right list first", "Can't find item", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                using (DB db = new DB())
                {
                    var s = db.route_schedule.Where(x => x.ID_RouteSchedule == 
                    ((ScheduleItem)lbSchedule.SelectedItem).S.ID_RouteSchedule).First();
                    db.route_schedule.Remove(s);

                    db.SaveChanges();
                    upd_from_database();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't change this data: \n\n" + ex.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAddStopBefore_Click(object sender, RoutedEventArgs e)
        {
            if (cbStop.SelectedIndex == -1 || lbStops.SelectedIndex == -1)
            {
                MessageBox.Show("Choose stops from lists first", "Can't find item", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var next = (StopItem)lbStops.SelectedItem;

                int id_stop = ((StopItem)cbStop.SelectedItem).S.ID_Stop;
                foreach (StopItem s in lbStops.Items)
                    if (s.S.ID_Stop == id_stop)
                        throw new Exception("this stop is already set in this route");

                using (DB db = new DB())
                {
                    var stop = new DB_Objects.RouteStop {
                        ID_Route = Route.ID_Route,
                        NumInWay = (short)(next.RS.NumInWay),
                        ID_Stop = id_stop,
                        DistanceToPreviousKm = decimal.Parse(tbDistance.Text)
                    };

                    var stops = db.route_stop.Where(x => x.ID_Route == Route.ID_Route).ToList();

                    foreach (var s in stops)
                    {
                        if (s.NumInWay >= stop.NumInWay)
                        {
                            s.NumInWay++;
                        }
                    }

                    db.route_stop.Add(stop);
                    db.SaveChanges();

                    upd_from_database();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't change this data: \n\n" + ex.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAddStopAfter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var prev = (StopItem)lbStops.SelectedItem;

                int id_stop = -1;
                if (cbStop.SelectedIndex != -1)
                    id_stop = ((StopItem)cbStop.SelectedItem).S.ID_Stop;

                int num_in_way = 1;

                foreach (StopItem s in lbStops.Items)
                {
                    if (s.S.ID_Stop == id_stop)
                        throw new Exception("this stop is already set in this route");
                    if (s.RS.NumInWay > num_in_way)
                        num_in_way = s.RS.NumInWay;
                }

                using (DB db = new DB())
                {
                    var stop = new DB_Objects.RouteStop
                    {
                        ID_Route = Route.ID_Route,
                        NumInWay = (short)num_in_way,
                        ID_Stop = id_stop,
                        DistanceToPreviousKm = decimal.Parse(tbDistance.Text)
                    };

                    var stops = db.route_stop.Where(x => x.ID_Route == Route.ID_Route).ToList();

                    foreach (var s in stops)
                    {
                        if (s.NumInWay >= stop.NumInWay)
                        {
                            stop.NumInWay++;
                        }
                    }

                    db.route_stop.Add(stop);
                    db.SaveChanges();

                    upd_from_database();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't change this data: \n\n" + ex.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAddArrival_Click(object sender, RoutedEventArgs e)
        {
            if (cbStopForSchedule.SelectedIndex == -1)
            {
                MessageBox.Show("Choose arrival from right list first", "Can't find item", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                using (DB db = new DB())
                {
                    var a = new DB_Objects.RouteSchedule
                    {
                        ID_RouteStop = ((StopItem)cbStopForSchedule.SelectedItem).RS.ID_RouteStop,
                        ArriveTime = TimeSpan.Parse(tbHoursArrive.Text + ":" + tbMinutesArrive.Text),
                        DepartureTime = TimeSpan.Parse(tbHoursDeparture.Text+":"+ tbMinutesDeparture.Text)
                    };

                    db.route_schedule.Add(a);

                    db.SaveChanges();
                    upd_from_database();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't change this data: \n\n" + ex.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
