using CityTransport.DB_Objects;
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
    /// Логика взаимодействия для WorkersEdit.xaml
    /// </summary>
    public partial class WorkersEdit : Window
    {

        DB db = new DB();

        public WorkersEdit()
        {
            InitializeComponent();
            new TextResizer(this);

            reload("");

            btnSave.Click += BtnSave_Click;
            btnAdd.Click += BtnAdd_Click;
            btnDelete.Click += BtnDelete_Click;

            tbSearch.TextChanged += TbSearch_TextChanged;
            this.Closed += WorkersEdit_Closed;
        }

        private void WorkersEdit_Closed(object sender, EventArgs e)
        {
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update error : \n\n" + ex.Message, "Can't save these changes",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            reload(tbSearch.Text);
        }

        private void reload(string search)
        {
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update error : \n\n" + ex.Message, "Can't save these changes",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

            db = new DB();

            {
                List<Post> posts = db.post.ToList();
                List<Carrier> carriers = db.carrier.ToList();

                DataGridTemplateColumn postColumn = (DataGridTemplateColumn)dgTableMain.Columns[1];
                DataGridTemplateColumn carrierColumn = (DataGridTemplateColumn)dgTableMain.Columns[0];

                FrameworkElementFactory factoryPost = new FrameworkElementFactory(typeof(ComboBox));
                factoryPost.SetValue(ComboBox.ItemsSourceProperty, posts);
                factoryPost.SetValue(ComboBox.DisplayMemberPathProperty, "Name");
                factoryPost.SetValue(ComboBox.SelectedValuePathProperty, "ID_Post");
                factoryPost.SetBinding(ComboBox.SelectedValueProperty, new Binding("ID_Post") { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });

                DataTemplate dataTemplatePost = new DataTemplate { VisualTree = factoryPost };
                postColumn.CellTemplate = dataTemplatePost;

                FrameworkElementFactory factoryCarrier = new FrameworkElementFactory(typeof(ComboBox));
                factoryCarrier.SetValue(ComboBox.ItemsSourceProperty, carriers);
                factoryCarrier.SetValue(ComboBox.DisplayMemberPathProperty, "Name");
                factoryCarrier.SetValue(ComboBox.SelectedValuePathProperty, "ID_Carrier");
                factoryCarrier.SetBinding(ComboBox.SelectedValueProperty, new Binding("ID_Carrier") { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });

                DataTemplate dataTemplateCarrier = new DataTemplate { VisualTree = factoryCarrier };
                carrierColumn.CellTemplate = dataTemplateCarrier;
            }

            dgTableMain.ItemsSource = db.worker.Include("Post").Include("Carrier").
                Where(x => x.Email.Contains(search) || x.Post.Name.StartsWith(search) 
                || x.FirstName.Contains(search) || x.Sirname.Contains(search)
                || search.Contains(x.FirstName) || search.Contains(x.Sirname) ||
                x.Carrier.Name.Contains(search)).
                ToList();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (DB db = new DB())
                {
                    foreach (Worker w in dgTableMain.SelectedItems)
                    {
                        db.worker.Remove(db.worker.Where(x => x.ID_Worker == w.ID_Worker).First());
                    }
                    db.SaveChanges();
                }
                reload(tbSearch.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't delete these items : \n\n" + ex.Message, "Delete error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Worker worker = new Worker
            {
                Email = "email",
                Phone = "phone",
                Gender = '?',
                FirstName = "name",
                Sirname = "sirname",
                LastName = "lastName",
                Post = db.post.ToList()[0]
            };
            db.worker.Add(worker);
            reload("sirname");
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            reload(tbSearch.Text);
        }
    }
}
