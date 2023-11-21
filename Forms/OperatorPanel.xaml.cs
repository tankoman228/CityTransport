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
                List<GroupCbItem> groups = new List<GroupCbItem>();

                var routes = db.group_route.Include("Route").
                    Where(x => x.ID_Group == UserData.Account.ID_Group).ToList();

                foreach (var route in routes)
                {
                    var item = new RouteLbItem { R = route.Route };
                    lbRoutes.Items.Add(item);
                }
            }
        }
    }
}
