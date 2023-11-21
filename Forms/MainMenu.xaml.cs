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

            switch (UserData.userType)
            {
                case UserData.UserType.Operator:
                    MessageBox.Show("You're operator");                  
                    break;
                case UserData.UserType.Admin:
                    MessageBox.Show("You're admin");
                    pgContents.Content = new AdminPanel();
                    break;
                case UserData.UserType.SuperAdmin:
                    pgContents.Content = new SuperAdminPanel();
                    MessageBox.Show("You're superadmin");
                    break;
                default:
                    Close();
                    break;
            }

            this.SizeChanged += new TextResizer(this).Window_SizeChanged;
            this.SizeChanged += (s, v) =>
            {
                tbl_header.FontSize *= 3;
            };
        }
    }
}
