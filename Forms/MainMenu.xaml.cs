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
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();

            switch(UserData.userType)
            {
                case UserData.UserType.Operator:
                    MessageBox.Show("You're operator");
                    break;
                case UserData.UserType.Admin:
                    MessageBox.Show("You're admin");
                    break;
                case UserData.UserType.SuperAdmin:
                    MessageBox.Show("You're superadmin");
                    break;
                default:
                    Close();
                    break;
            }
        }
    }
}
