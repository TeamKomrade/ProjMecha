using ProjMecha.DAL;
using ProjMecha.DL.People;
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
    /// Логика взаимодействия для UserInfoWindow.xaml
    /// </summary>
    public partial class UserInfoWindow : Window
    {
        PersonalDataPair _pd;
        MechanicDatabase _db;
        CredentialsModel _model;
        MainWindow _w;
        bool _isEditMode = false;
        public UserInfoWindow(MainWindow w)
        {
            InitializeComponent();
            Init(w);
        }

        public UserInfoWindow(MainWindow w, Credentials creds)
        {
            InitializeComponent();
            Init(w);
            _isEditMode = true;
            _model = new CredentialsModel()
            {
                CredentialsID = creds.CredentialsID,
                PersonalData = creds.PersonalData,
                AccountType = creds.AccountType,
                FullName = creds.FullName,
                Login = creds.Login,
                Password = creds.Password
            };
            DataContext = _model;
            DataGridPairs.ItemsSource = _model.PersonalData.ToList();
        }

        private void Init(MainWindow w)
        {
            _w = w;
            _db = _w.Database;
            _pd = new PersonalDataPair();
            StackPanelPersonalData.DataContext = _pd;
            _model = new CredentialsModel() { AccountType = AccountType.Client, PersonalData = new List<PersonalDataPair>() };
            DataContext = _model;
            DataGridPairs.ItemsSource = _model.PersonalData.ToList();
        }

        private void Button_Click_SaveAll(object sender, RoutedEventArgs ev)
        {
            if (!_model.IsValid()) { MessageBox.Show("ОШИБКА"); return; }
            Credentials person;
            if (_isEditMode)
            {
                int id = _model.CredentialsID;

                person = _db.Credentials.GetByID(id);
                person.PersonalData = _model.PersonalData;
                person.AccountType = _model.AccountType;
                person.FullName = _model.FullName;
                person.Login = _model.Login;
                person.Password = _model.Password;

                _db.TryAction(DatabaseActions.UPDATE, person);
            }
            else
            {
                person = new Credentials()
                {
                    PersonalData = _model.PersonalData,
                    AccountType = _model.AccountType,
                    FullName = _model.FullName,
                    Login = _model.Login,
                    Password = _model.Password
                };
                _db.TryAction(DatabaseActions.ADD, person);
            }
            _db.SaveChanges();
            _w.LoadInfoFromDb();
        }

        private void Button_Click_AddData(object sender, RoutedEventArgs ev)
        {
            var data = _model.PersonalData;
            var pair = data.Where(d => d.Key == _pd.Key).FirstOrDefault();
            if (pair is not null) pair.Value = _pd.Value;
            else data.Add(new PersonalDataPair() { Key = _pd.Key, Value = _pd.Value });
            DataGridPairs.ItemsSource = _model.PersonalData.ToList();
        }

        private void Button_Click_DeleteData(object sender, RoutedEventArgs ev)
        {
            var data = _model.PersonalData;
            var item = ((Button)sender).DataContext as PersonalDataPair;
            data.Remove(item);
            DataGridPairs.ItemsSource = _model.PersonalData.ToList();
        }

        public UserInfoWindow ChangeAccountType(AccountType type) 
        { 
            _model.AccountType = type; 
            return this;
        }
    }
}
