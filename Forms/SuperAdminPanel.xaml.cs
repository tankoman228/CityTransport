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
using CityTransport.Forms.Items;

namespace CityTransport.Forms
{
    /// <summary>
    /// Логика взаимодействия для SuperAdminPanel.xaml
    /// </summary>
    public partial class SuperAdminPanel : Page
    {
        public SuperAdminPanel()
        {
            InitializeComponent();

            btnCarriers.Click += BtnCarriers_Click;
            btnCreateAccount.Click += BtnCreateAccount_Click;
            btnCreateGroup.Click += BtnCreateGroup_Click;
            btnEditAccount.Click += BtnEditAccount_Click;
            btnEditGroup.Click += BtnEditGroup_Click;
            btnRoutes.Click += BtnRoutes_Click;
            btnStats.Click += BtnStats_Click;
            btnWorkers.Click += BtnWorkers_Click;

            using (var db = new DB())
            {
                List<GroupCbItem> groups = new List<GroupCbItem>();

                foreach (var x in db.group)
                {
                    groups.Add(new GroupCbItem {G = x});
                }

                foreach (var g in groups)
                {

                    var accounts = db.account.Include("Worker").
                        Where(x => x.ID_Group == g.G.ID_Group).ToList();

                    lbUsersGroups.Items.Add(g);

                    foreach (DB_Objects.Account account in accounts)
                    {
                        var item = new AccountLbItem { A = account };
                        lbUsersGroups.Items.Add(item);
                    }
                }

                var accountss = db.account.Include("Worker").
                        Where(x => x.ID_Group == null).ToList();

                lbUsersGroups.Items.Add("[--have no group--]");
                foreach (DB_Objects.Account account in accountss)
                {
                    var item = new AccountLbItem { A = account };
                    lbUsersGroups.Items.Add(item);
                }


            }
        }

        

        private void BtnWorkers_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnStats_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnRoutes_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnEditGroup_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnEditAccount_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnCreateGroup_Click(object sender, RoutedEventArgs e)
        {
            new GroupCreate().ShowDialog();
        }

        private void BtnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            new AccountCreate().ShowDialog();
        }

        private void BtnCarriers_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
