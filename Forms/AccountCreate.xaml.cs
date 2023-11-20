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
    /// Логика взаимодействия для AccountCreate.xaml
    /// </summary>
    public partial class AccountCreate : Window
    {
        public AccountCreate()
        {
            InitializeComponent();

            using (var db = new DB())
            {
                List<WorkerCbItem> listW = new List<WorkerCbItem>();

                foreach (var worker in db.worker)
                {
                    if (worker.ID_Post > 7)
                        listW.Add(new WorkerCbItem {W = worker});
                }
                cbWorker.ItemsSource = listW;


                List<GroupCbItem> listG = new List<GroupCbItem>();

                foreach (var group in db.group)
                {
                    listG.Add(new GroupCbItem { G = group});
                }
                cbGroup.ItemsSource = listG;
            }

            btnSave.Click += BtnSave_Click;
        }

        private void error(string msg)
        {
            MessageBox.Show(msg, "can't create account",
                    MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string username = tbUsername.Text;
            string pwd = pbPassword.Password;

            if (username == "")
            {
                error("No username");
                return;
            }
            if (pwd != pbPasswordConfirm.Password)
            {
                error("Passwords do not match. Confirm it in correct way");
                return;
            }
            if (cbRole.SelectedIndex == -1)
            {
                error("Select role of this user");
                return;
            }
            if (cbWorker.SelectedIndex == -1)
            {
                error("Select worker first");
                return;
            }

            using (var db = new DB())
            {
                foreach (var a in db.account)
                {
                    if (a.Username == username)
                    {
                        error("Username must be unique");
                        return;
                    }
                }

                DB_Objects.Account acc = new DB_Objects.Account
                {
                    Username = username,
                    ID_Worker = ((WorkerCbItem)cbWorker.SelectedItem).W.ID_Worker,
                    PasswordHash = PasswordHash.getHash(pwd),
                    Status = true,                
                };

                if (cbRole.SelectedItem == ciAdmin)
                    acc.ID_AccountType = 2;
                else if (cbRole.SelectedItem == ciSuperadmin)
                    acc.ID_AccountType = 3;
                else acc.ID_AccountType = 1;


                if (cbGroup.SelectedIndex != -1)
                {
                    acc.ID_Group = ((GroupCbItem)cbGroup.SelectedItem).G.ID_Group;
                }

                
                try {
                    db.account.Add(acc);          
                    db.SaveChanges();

                    MessageBox.Show("Success", "Account Created",
                       MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                catch (Exception ex)
                {
                    error("databae error: \n\n" + ex.Message);
                }
            }
        }        
    }
}
