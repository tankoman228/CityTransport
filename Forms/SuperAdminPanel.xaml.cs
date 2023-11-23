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
            btnWorkers.Click += BtnWorkers_Click;

            updUserGroupList();

            tbSearch.TextChanged += TbSearch_TextChanged;
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            updUserGroupList();
        }

        private void updUserGroupList()
        {
            lbUsersGroups.Items.Clear();

            using (var db = new DB())
            {
                List<GroupCbItem> groups = new List<GroupCbItem>();

                foreach (var x in db.group)
                {
                    groups.Add(new GroupCbItem { G = x });
                }

                foreach (var g in groups)
                {

                    var accounts = db.account.Include("Worker").
                        Where(x => x.ID_Group == g.G.ID_Group).ToList();                

                    bool not_found_a_group = !g.G.Name.Contains(tbSearch.Text);
                    bool skip_account = !not_found_a_group;
                    if (skip_account)
                    {
                        lbUsersGroups.Items.Add(g);
                    }

                    foreach (DB_Objects.Account account in accounts)
                    {
                        var item = new AccountLbItem { A = account };
                        if (skip_account || item.ToString().Contains(tbSearch.Text))
                        {
                            if (not_found_a_group)
                            {
                                lbUsersGroups.Items.Add(g);
                                not_found_a_group = false;
                            }
                            lbUsersGroups.Items.Add(item);
                        }
                    }

                    if (!not_found_a_group)
                    {
                        lbUsersGroups.Items.Add("");
                    }
                }

                var accountss = db.account.Include("Worker").
                        Where(x => x.ID_Group == null).ToList();

                lbUsersGroups.Items.Add("");
                lbUsersGroups.Items.Add("[--have no group--]");
                lbUsersGroups.Items.Add("");

                foreach (DB_Objects.Account account in accountss)
                {
                    var item = new AccountLbItem { A = account };
                    if (item.ToString().Contains(tbSearch.Text))
                        lbUsersGroups.Items.Add(item);
                }
            }
        }

        private void BtnRoutes_Click(object sender, RoutedEventArgs e)
        {
            var w = new MainMenu();
            w.Show();
            for (int i = 0; i < 1000000000; i++) { }
            w.pgContents.Content = new OperatorPanel();
        }

        private void BtnEditGroup_Click(object sender, RoutedEventArgs e)
        {
            if (!(lbUsersGroups.SelectedItem is Items.GroupCbItem))
            {
                MessageBox.Show("Choose a group to edit", "item not selected",
                    MessageBoxButton.OK, MessageBoxImage.Question);
                return;
            }

            var group = (Items.GroupCbItem)lbUsersGroups.SelectedItem;

            GroupEdit.Edited = group.G;
            new GroupEdit().ShowDialog();
        }

        private void BtnEditAccount_Click(object sender, RoutedEventArgs e)
        {
            if (!(lbUsersGroups.SelectedItem is AccountLbItem))
            {
                MessageBox.Show("Choose account to edit", "Edit account error", MessageBoxButton.OK, MessageBoxImage.Question);
                return;
            }

            AccountEdit.account = ((AccountLbItem)lbUsersGroups.SelectedItem).A;
            new AccountEdit().Show();
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
            new CarrierEdit().ShowDialog();
        }

        private void BtnWorkers_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
