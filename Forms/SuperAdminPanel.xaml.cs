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

            using (var x = new DB())
            {

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
