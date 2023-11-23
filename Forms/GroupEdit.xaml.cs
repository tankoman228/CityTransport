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
                var routes = db.group_route.Include("Route").Where(x => 
                x.ID_Group == Edited.ID_Group).ToList();

                foreach (var route in routes)
                {
                    lbRoutes.Items.Add(new Items.RouteLbItem { R = route.Route });
                }

                var all_routes = db.group_route.Include("Route");
                foreach (var route in all_routes)
                {
                    cbRoute.Items.Add(new Items.RouteLbItem { R = route.Route });
                }
            }

            btnAdd.Click += BtnAdd_Click;
            btnDelete.Click += BtnDelete_Click;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
