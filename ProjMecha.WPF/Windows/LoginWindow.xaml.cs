using ProjMecha.DAL;
using ProjMecha.WPF.Models;
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

namespace ProjMecha.WPF.Windows
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        MechanicDatabase _db;
        LoginModel _model;
        public LoginWindow()
        {
            InitializeComponent();
            _db = new MechanicDatabase();
            _db.NotifyAboutAction += (message) => { MessageBox.Show(message); };
            _db.FillDefaultInfo();

            _model = new LoginModel();
            this.DataContext = _model;
        }

        private void Button_Click_CheckCredentials(object sender, RoutedEventArgs ev)
        {
            if (_model.IsValid())
            {
                var user = _db.Credentials
                    .GetCredentialsByLoginPassword(_model.Login, _model.Password)
                    .FirstOrDefault();

                if (user == null)
                {
                    MessageBox.Show("Пользователь не найден)");
                    return;
                }
                new MainWindow(user).Show();
                _db.Dispose();
                this.Close();
            }
        }
    }
}
