﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace CityTransport
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            Database.SetInitializer<DB>(null);

            this.SizeChanged += new TextResizer(this).Window_SizeChanged;
            this.SizeChanged += (s, v) =>
            {
                tbl_header.FontSize *= 3.52;
            };

            btnLogin.Click += BtnLogin_Click;
            btnExit.Click += BtnExit_Click;
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new DB())
            {
                DB_Objects.Account account = db.account.Where(
                x => x.Username == tbLogin.Text).SingleOrDefault();

                if (account == null)
                {
                    MessageBox.Show("User doesn't exist!", "Login Error",
                        MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }
                else
                {
                    if (!PasswordHash.verifyPassword(tbPassword.Password, account.PasswordHash))
                    {
                        MessageBox.Show("Wrong password!", "Login Error",
                            MessageBoxButton.OK, MessageBoxImage.Stop);
                        return;
                    }
                    else
                    {
                        if (account.Status != true)
                        {
                            MessageBox.Show("Admins disabled your account!", "Login Error",
                                MessageBoxButton.OK, MessageBoxImage.Stop);
                            return;
                        }
                        else
                        {
                            switch(account.ID_AccountType)
                            {
                                case 1:
                                    UserData.userType = UserData.UserType.Operator;
                                    break;
                                case 2:
                                    UserData.userType = UserData.UserType.Admin;
                                    break;
                                case 3:
                                    UserData.userType = UserData.UserType.SuperAdmin;
                                    break;
                                default:
                                    UserData.userType = UserData.UserType.Unknown;
                                    MessageBox.Show("Unknown user role!", "Login Error",
                                        MessageBoxButton.OK, MessageBoxImage.Error);
                                    return; //RETURN! Don't open new window
                            }

                            UserData.Account = account;

                            new Forms.MainMenu().Show();
                            Close();
                        }
                    }
                }
            }
        }
    }
}
