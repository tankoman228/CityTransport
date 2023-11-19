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
    /// Логика взаимодействия для GroupCreate.xaml
    /// </summary>
    public partial class GroupCreate : Window
    {
        public GroupCreate()
        {
            InitializeComponent();

            btnSave.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            
            if (tbName.Text.Length < 3)
            {
                MessageBox.Show("Can't save this group: Name is too short",
                    "Error Group Create", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            try
            {
                using (var db = new DB())
                {
                    db.group.Add(new DB_Objects.Group
                    {
                        Name = tbName.Text,
                        Description = tdDestr.Text
                    });

                    db.SaveChanges();

                    MessageBox.Show("Success", "Group Created",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't save this group: " + ex.Message,
                    "Error Group Create", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
