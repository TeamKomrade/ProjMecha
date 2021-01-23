using ProjMecha.DAL;
using ProjMecha.DL.People;
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
    /// Логика взаимодействия для ProductInfoWindow.xaml
    /// </summary>
    public partial class StoreItemInfoWindow : Window
    {
        ProductAttributeModel _pd;
        MechanicDatabase _db;
        StoreItemModel _model;
        MainWindow _w;
        bool _isEditMode = false;
        public StoreItemInfoWindow(MainWindow w)
        {
            InitializeComponent();
            Init(w);
        }

        public StoreItemInfoWindow(MainWindow w, StoreItem item)
        {
            InitializeComponent();
            Init(w);
            _isEditMode = true;
            _model = new StoreItemModel()
            {
                StoreItemID = item.StoreItemID,
                Description = item.Description,
                AttributeProductPairs = item.AttributeProductPairs,
                Price = item.Price,
                StoreItemType = item.StoreItemType,
                Title = item.Title
            };
            DataContext = _model;
            DataGridPairs.ItemsSource = _model.AttributeProductPairs.ToList();
        }

        private void Init(MainWindow w)
        {
            _w = w;
            _db = _w.Database;
            _pd = new ProductAttributeModel();
            StackPanelAttribute.DataContext = _pd;
            _model = new StoreItemModel() { StoreItemType = StoreItemType.Product, AttributeProductPairs = new List<AttributeStoreItemPair>() };
            DataContext = _model;
            DataGridPairs.ItemsSource = _model.AttributeProductPairs.ToList();
        }

        private void Button_Click_SaveAll(object sender, RoutedEventArgs ev)
        {
            if (!_model.IsValid()) { MessageBox.Show("ОШИБКА"); return; }
            StoreItem item;
            if (_isEditMode)
            {
                int id = _model.StoreItemID;

                item = _db.StoreItems.GetByID(id);
                item.Description = _model.Description;
                item.AttributeProductPairs = _model.AttributeProductPairs;
                item.Price = _model.Price;
                item.StoreItemType = _model.StoreItemType;
                item.Title = Title;

                _db.TryAction(DatabaseActions.UPDATE, item);
            }
            else
            {
                item = new StoreItem()
                {
                    Description = _model.Description,
                    AttributeProductPairs = _model.AttributeProductPairs,
                    Price = _model.Price,
                    StoreItemType = _model.StoreItemType,
                    Title = _model.Title
                };
                _db.TryAction(DatabaseActions.ADD, item);
            }
            _db.SaveChanges();
            _w.LoadInfoFromDb();
        }

        private void Button_Click_AddData(object sender, RoutedEventArgs ev)
        {
            var data = _model.AttributeProductPairs;
            var attribute = _db.ProductAttributes.GetAttributeByName(_pd.Key);
            var pair = data.Where(d => d.ProductAttribute.Name == _pd.Key).FirstOrDefault();
            if (pair is not null) pair.Value = _pd.Value;
            else 
            {
                if (attribute is null) attribute = new ProductAttribute() { Name = _pd.Key };
                var new_pair = new AttributeStoreItemPair()
                {
                    ProductAttribute = attribute,
                    StoreItemID = _model.StoreItemID,
                    Value = _pd.Value
                };
                data.Add(new_pair);
            } 
            DataGridPairs.ItemsSource = _model.AttributeProductPairs.ToList();
        }

        private void Button_Click_DeleteData(object sender, RoutedEventArgs ev)
        {
            var data = _model.AttributeProductPairs;
            var item = ((Button)sender).DataContext as AttributeStoreItemPair;
            data.Remove(item);
            DataGridPairs.ItemsSource = _model.AttributeProductPairs.ToList();
        }

        public StoreItemInfoWindow ChangeProductType(StoreItemType type)
        {
            _model.StoreItemType = type;
            if (type == StoreItemType.Service)
            {
                StackPanelAttribute.Visibility = Visibility.Collapsed;
                DataGridPairs.Visibility = Visibility.Collapsed;
            }
            return this;
        }
    }
}
