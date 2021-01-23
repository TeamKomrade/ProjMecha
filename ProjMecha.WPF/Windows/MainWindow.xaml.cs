using Microsoft.EntityFrameworkCore;
using ProjMecha.DAL;
using ProjMecha.DL.People;
using ProjMecha.DL.Store;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjMecha.WPF.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Credentials _credentials;
        public MechanicDatabase Database;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(Credentials creds)
        {
            InitializeComponent();

            _credentials = creds;
            Database = new MechanicDatabase();
            Database.NotifyAboutAction += (message) => { MessageBox.Show(message); };
            LoadInfoFromDb();
        }

        public void LoadInfoFromDb()
        {
            DataGridClients.ItemsSource = Database.Credentials.GetOnlyClients().Include(c => c.PersonalData).ToList();
            DataGridEmployees.ItemsSource = Database.Credentials.GetOnlyEmployees().Include(c => c.PersonalData).ToList();
            DataGridProducts.ItemsSource = Database.StoreItems.GetOnlyProducts().Include(c => c.AttributeProductPairs).ThenInclude(ap => ap.ProductAttribute).ToList();
            DataGridServices.ItemsSource = Database.StoreItems.GetOnlyServices().Include(c => c.AttributeProductPairs).ThenInclude(ap => ap.ProductAttribute).ToList();
            DataGridPurchases.ItemsSource = Database.Purchases.Include(p => p.Credentials).Include(p => p.StoreItem).ToList();
        }

        private void Button_Click_OpenClientInfo(object sender, RoutedEventArgs e) => OpenCredentialsInfo(sender, AccountType.Client);
        private void Button_Click_OpenEmployeeInfo(object sender, RoutedEventArgs e) => OpenCredentialsInfo(sender, AccountType.Employee);
        private void OpenCredentialsInfo(object sender, AccountType type)
        {
            var client = ((Button)sender).DataContext as Credentials;
            new UserInfoWindow(this, client).ChangeAccountType(type).Show();
        }
        private void Button_Click_OpenClientInfoOfPurchase(object sender, RoutedEventArgs e)
        {
            var purchase = ((Button)sender).DataContext as Purchase;
            new UserInfoWindow(this, purchase.Credentials).Show();
        }
        private void Button_Click_OpenProductInfo(object sender, RoutedEventArgs e) => OpenStoreItemsInfo(sender, StoreItemType.Product);
        private void Button_Click_OpenServiceInfo(object sender, RoutedEventArgs e) => OpenStoreItemsInfo(sender, StoreItemType.Service);
        private void OpenStoreItemsInfo(object sender, StoreItemType type)
        {
            var item = ((Button)sender).DataContext as StoreItem;
            new StoreItemInfoWindow(this, item).ChangeProductType(type).Show();
        }

        private void Button_Click_OpenPurchaseInfo(object sender, RoutedEventArgs e) => OpenPurchaseInfo(sender);
        private void OpenPurchaseInfo(object sender)
        {
            var item = ((Button)sender).DataContext as Purchase;
            new PurchaseDetailsWindow(this, item).Show();
        }

        private void AddNewCredentials(AccountType type) => new UserInfoWindow(this).ChangeAccountType(type).Show();
        private void Button_Click_AddNewClient(object sender, RoutedEventArgs e) => AddNewCredentials(AccountType.Client);
        private void Button_Click_AddNewEmployee(object sender, RoutedEventArgs e) => AddNewCredentials(AccountType.Client); 
        private void AddNewStoreItem(StoreItemType type) => new StoreItemInfoWindow(this).ChangeProductType(type).Show();
        private void Button_Click_AddNewProduct(object sender, RoutedEventArgs e) => AddNewStoreItem(StoreItemType.Product);
        private void Button_Click_AddNewService(object sender, RoutedEventArgs e) => AddNewStoreItem(StoreItemType.Service);
        private void Button_Click_AddNewPurchase(object sender, RoutedEventArgs e) => AddNewPurchase();
        private void AddNewPurchase() => new PurchaseDetailsWindow(this).Show();
        private void Button_Click_DeleteFromDB(object sender, RoutedEventArgs e)
        {
            var obj = ((Button)sender).DataContext;
            Database.TryAction(DatabaseActions.DELETE, obj);
            LoadInfoFromDb();
        }

        private void Button_Click_CheckCredentials(object sender, RoutedEventArgs e)
        {
            var person = ((Button)sender).DataContext as Credentials;
            var builder = new StringBuilder();
            builder.Append($"Данные о {person.FullName} (уровень доступа: {person.AccountType}): \n");
            foreach (var pair in person.PersonalData) builder.Append($"\n{pair.Key}: {pair.Value}");
            MessageBox.Show(builder.ToString());
        }

        private void Button_Click_CheckStoreItemData(object sender, RoutedEventArgs e)
        {
            var item = ((Button)sender).DataContext as StoreItem;
            var builder = new StringBuilder();
            builder.Append($"Данные о {item.Title} [{item.Price}]\nОписание: {item.Description}");
            foreach (var pair in item.AttributeProductPairs) builder.Append($"\n{pair.ProductAttribute.Name}: {pair.Value}");
            MessageBox.Show(builder.ToString());
        }
    }
}
