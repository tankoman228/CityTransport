using System.Windows;

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
