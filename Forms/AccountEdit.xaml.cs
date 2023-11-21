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
    /// Логика взаимодействия для AccountEdit.xaml
    /// </summary>
    /// 
    public partial class AccountEdit : Window
    {
        internal static DB_Objects.Account account;

        public AccountEdit()
        {
            InitializeComponent();
            new TextResizer(this);

            tbUser.Text = account.Username;
            tbWorker.Text = account.Worker.FirstName + " " + account.Worker.LastName;

            switch(account.ID_AccountType) {
                case 1:
                    cbRole.SelectedItem = ciOperator;
                    break;
                case 2:
                    cbRole.SelectedItem = ciAdmin;
                    break;
                case 3:
                    cbRole.SelectedItem = ciSuperadmin;
                    break;
            }

            if (account.Status)
                btnToggleActive.Content = "Deactivate";
            else
                btnToggleActive.Content = "Activate";

            using (var db = new DB())
            {
                cbGroup.Items.Clear();

                List<object> listG = new List<object>();

                foreach (var group in db.group)
                {
                    listG.Add(new GroupCbItem { G = group });
                }
                listG.Add(cbiNoGroup);
                cbGroup.ItemsSource = listG;
             
                if (account.Group == null)
                    cbGroup.SelectedItem = cbiNoGroup;
                else
                    cbGroup.SelectedItem = new GroupCbItem { G = account.Group };
            }

            btnSave.Click += BtnSave_Click;
            btnToggleActive.Click += BtnToggleActive_Click;
            btnNewPassword.Click += BtnNewPassword_Click;

            if (account.ID_AccountType >= UserData.Account.ID_AccountType && UserData.Account.ID_AccountType != 3)
            {
                MessageBox.Show("You can't edit this user", "user rdit error",
                    MessageBoxButton.OK, MessageBoxImage.Stop);
                btnNewPassword.IsEnabled = false;
                btnSave.IsEnabled = false;
                btnToggleActive.IsEnabled = false;
            }
            if (UserData.Account.ID_AccountType == 2)
            {
                cbRole.Items.Clear();
                cbRole.SelectedItem = ciOperator;
                cbRole.IsEnabled = false;
                cbGroup.IsEnabled = false;

                cbRole.Visibility = Visibility.Hidden;
                cbGroup.Visibility = Visibility.Hidden;
            }
        }

        private void BtnNewPassword_Click(object sender, RoutedEventArgs e)
        {
            string pwd = pbPassword.Password;

            if (pwd != pbPasswordConfirm.Password)
            {
                MessageBox.Show("Passwords do not match. Confirm it in correct way",
                    "Error password update",
                    MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            using (var db = new DB())
            {
                DB_Objects.Account a = db.account.Where(x => x.ID_Account == account.ID_Account).FirstOrDefault();
                a.PasswordHash = PasswordHash.getHash(pwd);
                db.SaveChanges();

                MessageBox.Show("Success",
                    this.Title,
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnToggleActive_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new DB())
            {
                DB_Objects.Account a = db.account.Where(x => x.ID_Account == account.ID_Account).FirstOrDefault();
                a.Status = !a.Status;
                db.SaveChanges();

                if (a.Status)
                {
                    MessageBox.Show("User Activated",
                        this.Title,
                        MessageBoxButton.OK, MessageBoxImage.Information);
                        btnToggleActive.Content = "Deactivate";
                }
                else
                {
                    MessageBox.Show("User Deactivated",
                        this.Title,
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    btnToggleActive.Content = "Activate";
                }
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new DB())
            {
                DB_Objects.Account a = db.account.Where(x => x.ID_Account == account.ID_Account).FirstOrDefault();

                if (cbGroup.SelectedItem != cbiNoGroup)
                    a.ID_Group = ((GroupCbItem)cbGroup.SelectedItem).G.ID_Group;
                else
                    a.ID_Group = null;

                if (cbRole.SelectedItem == ciSuperadmin)
                {
                    a.ID_AccountType = 3;
                }
                else if (cbRole.SelectedItem == ciAdmin)
                {
                    a.ID_AccountType = 2;
                }
                else
                {
                    a.ID_AccountType = 1;
                }

               db.SaveChanges();

                MessageBox.Show("Success",
                    this.Title,
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
