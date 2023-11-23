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
    /// Логика взаимодействия для GroupEdit.xaml
    /// </summary>
    public partial class GroupEdit : Window
    {
        internal static DB_Objects.Group Edited;

        public GroupEdit()
        {
            InitializeComponent();

            using (DB db = new DB())
            {
                var all_routes = db.group_route.Include("Route");
                foreach (var route in all_routes)
                {
                    cbRoute.Items.Add(new Items.RouteLbItem { R = route.Route });
                }
            }
            reload_from_db_rg();

            btnAdd.Click += BtnAdd_Click;
            btnDelete.Click += BtnDelete_Click;
        }

        void reload_from_db_rg()
        {
            lbRoutes.Items.Clear();
            using (DB db = new DB())
            {
                var routes = db.group_route.Include("Route").Where(x =>
                x.ID_Group == Edited.ID_Group).ToList();

                foreach (var route in routes)
                {
                    lbRoutes.Items.Add(new Items.RouteLbItem { R = route.Route });
                }
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new DB()) {
                foreach (Items.RouteLbItem i in lbRoutes.SelectedItems)
                {
                    var found = db.group_route.Where
                        (x => x.ID_Group == Edited.ID_Group && x.ID_Route == i.R.ID_Route).ToList();

                    foreach (var x in found)
                    {
                        db.group_route.Remove(x);
                        db.SaveChanges();
                    }
                }
            }
            reload_from_db_rg();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (cbRoute.SelectedIndex == -1)
            {
                MessageBox.Show("Choose route to attach","Item not selected",
                    MessageBoxButton.OK, MessageBoxImage.Question);
                return;
            }

            var route = cbRoute.SelectedItem as Items.RouteLbItem;
            foreach (Items.RouteLbItem ae in lbRoutes.Items)
            {
                if (ae.R.ID_Route == route.R.ID_Route)
                {
                    MessageBox.Show("This route already attached", "Can't attach twice",
                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
            }


            using (var db = new DB())
            {
                var r = new DB_Objects.GroupRoute
                {
                    ID_Route = route.R.ID_Route,
                    ID_Group = Edited.ID_Group
                };
                db.group_route.Add(r);
                db.SaveChanges();
            }

            reload_from_db_rg();
        }
    }
}
