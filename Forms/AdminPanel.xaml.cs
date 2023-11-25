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
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Page
    {
        public AdminPanel()
        {
            InitializeComponent();
            btnEditAccount.Click += BtnEditAccount_Click;      
            btnRoutes.Click += BtnRoutes_Click;

            updUserGroupList();

            tbSearch.TextChanged += TbSearch_TextChanged;
        }

        private void BtnRoutes_Click(object sender, RoutedEventArgs e)
        {
            var w = new MainMenu();
            w.Show();
            w.pgContents.Content = new OperatorPanel();
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

                var accounts = db.account.Include("Worker").
                        Where(x => x.ID_Group == UserData.Account.ID_Group).ToList();


                foreach (DB_Objects.Account account in accounts)
                {                  
                    var item = new AccountLbItem { A = account };
                    lbUsersGroups.Items.Add(item);                
                }
            }
                    
        }
    }
}
