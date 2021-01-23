using ProjMecha.DAL;
using ProjMecha.DL.Store;
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
    /// Логика взаимодействия для PurchadeDetailsWindow.xaml
    /// </summary>
    public partial class PurchaseDetailsWindow : Window
    {
        MechanicDatabase _db;
        PurchaseModel _model;
        MainWindow _w;
        bool _isEditMode = false;
        public PurchaseDetailsWindow(MainWindow w)
        {
            InitializeComponent();
            Init(w);
        }

        public PurchaseDetailsWindow(MainWindow w, Purchase purchase)
        {
            InitializeComponent();
            Init(w);
            _isEditMode = true;
            _model = new PurchaseModel()
            {
                PurchaseID = purchase.PurchaseID,
                Quantity = purchase.Quantity,
                Timestamp = purchase.Timestamp,
                TotalPrice = purchase.TotalPrice,
                Credentials = purchase.Credentials,
                StoreItem = purchase.StoreItem
            };
            DataContext = _model;
        }

        private void Init(MainWindow w)
        {
            _w = w;
            _db = _w.Database;
            _model = new PurchaseModel();
            DataContext = _model;
        }

        private void Button_Click_SaveAll(object sender, RoutedEventArgs ev)
        {
            if (!_model.IsValid()) { MessageBox.Show("ОШИБКА"); return; }
            Purchase purchase;
            if (_isEditMode)
            {
                int id = _model.PurchaseID;

                purchase = _db.Purchases.Where(p => p.PurchaseID == id).FirstOrDefault();
                purchase.Quantity = _model.Quantity;
                purchase.Timestamp = _model.Timestamp;
                purchase.TotalPrice = _model.TotalPrice;
                purchase.Credentials = _model.Credentials;
                purchase.StoreItem = _model.StoreItem;

                _db.TryAction(DatabaseActions.UPDATE, purchase);
            }
            else
            {
                purchase = new Purchase()
                {
                    Quantity = _model.Quantity,
                    Timestamp = DateTime.Now,
                    TotalPrice = _model.TotalPrice,
                    Credentials = _model.Credentials,
                    StoreItem = _model.StoreItem
                };
                _db.TryAction(DatabaseActions.ADD, purchase);
            };
            _db.SaveChanges();
            _w.LoadInfoFromDb();
        }

        private void Button_Click_FindClient(object sender, RoutedEventArgs ev)
        {
            var button = sender as Button;
            var name = _model.ClientName;
            var client = _db.Credentials.GetCredentialsByFullName(name).FirstOrDefault();
            if (client is not null)
            {
                _model.ClientName = client.FullName;
                _model.Credentials = client;
                TextBoxClient.Text = client.FullName;
                TextBoxClient.IsEnabled = false;
                button.IsEnabled = false;
            }
            else MessageBox.Show("Клиент не найден");
        }

        private void Button_Click_ClearClient(object sender, RoutedEventArgs ev)
        {
            _model.ClientName = "";
            TextBoxClient.Text = "";
            ButtonClient.IsEnabled = true;
            TextBoxClient.IsEnabled = true;
        }

        private void Button_Click_FindStoreItem(object sender, RoutedEventArgs ev)
        {
            var button = sender as Button;
            var name = _model.StoreItemName;
            var storeItem = _db.StoreItems.GetStoreItemByName(name).FirstOrDefault();
            if (storeItem is not null)
            {
                _model.StoreItemName = storeItem.Title;
                _model.StoreItem = storeItem;
                TextBoxStoreItem.Text = storeItem.Title;
                TextBoxStoreItem.IsEnabled = false;
                button.IsEnabled = false;
            }
            else MessageBox.Show("Клиент не найден");
        }

        private void Button_Click_ClearStoreItem(object sender, RoutedEventArgs ev)
        {
            _model.StoreItemName = "";
            TextBoxStoreItem.Text = "";
            ButtonStoreItem.IsEnabled = true;
            TextBoxStoreItem.IsEnabled = true;
        }

    }
}
