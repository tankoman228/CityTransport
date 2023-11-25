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
                    pgContents.Content = new OperatorPanel();
                    break;
                case UserData.UserType.Admin:
                    pgContents.Content = new AdminPanel();
                    break;
                case UserData.UserType.SuperAdmin:
                    pgContents.Content = new SuperAdminPanel();
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
