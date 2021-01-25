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
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        SettingsModel _model;
        MechanicDatabase _db;
        MainWindow _w;
        public SettingsWindow()
        {
            InitializeComponent();
        }

        public SettingsWindow(MainWindow w)
        {
            InitializeComponent();
            _w = w;
            _db = _w.Database;
            var settings = _db.GetCompanyDetails();
            _model = new SettingsModel()
            {
                CompanyName = settings.CompanyName,
                Address = settings.Address,
                Phone = settings.Phone
            };
            DataContext = _model;
        }

        private void Button_Click_ApplyChanges(object sender, RoutedEventArgs e)
        {
            if (_model.IsValid() is not true) { MessageBox.Show("ОШИБКА: проверьте правильность введённых данных"); return; }
            var settings = _db.GetCompanyDetails();
            settings.CompanyName = _model.CompanyName;
            settings.Address = _model.Address;
            settings.Phone = _model.Phone;
            _db.SaveChanges();
            _w.LoadInfoFromDb();
        }
    }
}
