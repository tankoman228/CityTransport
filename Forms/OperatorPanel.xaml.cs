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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CityTransport.Forms
{
    /// <summary>
    /// Логика взаимодействия для OperatorPanel.xaml
    /// </summary>
    public partial class OperatorPanel : Page
    {
        public OperatorPanel()
        {
            InitializeComponent();

            using (var db = new DB())
            {
                var routes = db.group_route.Include("Route").Include("Route.Carrier").
                    Where(x => x.ID_Group == UserData.Account.ID_Group).ToList();

                if (UserData.userType == UserData.UserType.SuperAdmin)
                {
                    routes = db.group_route.Include("Route").Include("Route.Carrier").
                    ToList();
                }

                foreach (var route in routes)
                {
                    var item = new RouteLbItem { R = route.Route };
                    lbRoutes.Items.Add(item);
                }
            }

            btnEditRoute.Click += BtnEditRoute_Click;
        }

        private void BtnEditRoute_Click(object sender, RoutedEventArgs e)
        {
            if (lbRoutes.SelectedIndex == -1)
            {
                MessageBox.Show("Select route to edit", "can't edit unchosen route",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            RouteEdit.Route = ((RouteLbItem)lbRoutes.SelectedItem).R;

            new RouteEdit().ShowDialog();
        }
    }
}
