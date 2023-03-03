using School.Classes;
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

namespace School
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool isAdmin = false;
        bool Open = false;
        public static bool update = false;
        public MainWindow()
        {
            InitializeComponent();

            ClassBase.BASE = new Entities();
            LV.ItemsSource = ClassBase.BASE.Service.ToList();

            countAllRecords.Text = $"Общее количество записей: {ClassBase.BASE.Service.ToList().Count()}";
            countCurrentRecords.Text = $"Текущее количество записей: {ClassBase.BASE.Service.ToList().Count()}";

            sortBox.SelectedIndex = 0;
            filterBox.SelectedIndex = 0;
            searchCBox.SelectedIndex = 0;
        }

        private void sortBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void filterBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void searchCBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        void Filter()
        {
            List<Service> service = ClassBase.BASE.Service.ToList();
            try
            {
                int costCB = sortBox.SelectedIndex, discountCB = filterBox.SelectedIndex, cbSeacrh = searchCBox.SelectedIndex;
                switch (costCB)
                {
                    case 1:
                        service = service.OrderBy(x => x.Cost).ToList();
                        break;
                    case 2:
                        service = service.OrderByDescending(x => x.Cost).ToList();
                        break;
                }
                switch (discountCB)
                {
                    case 1:
                        service = service.Where(x => x.Discount < 5 || x.Discount == null).ToList();
                        break;
                    case 2:
                        service = service.Where(x => x.Discount >= 5 && x.Discount < 15).ToList();
                        break;
                    case 3:
                        service = service.Where(x => x.Discount >= 15 && x.Discount < 30).ToList();
                        break;
                    case 4:
                        service = service.Where(x => x.Discount >= 30 && x.Discount < 70).ToList();
                        break;
                    case 5:
                        service = service.Where(x => x.Discount >= 70 && x.Discount < 100).ToList();
                        break;
                }
                if (!String.IsNullOrEmpty(searchBox.Text))
                {
                    switch (cbSeacrh)
                    {
                        case 0:
                            service = service.Where(x => x.Description.ToLower().Contains(searchBox.Text.ToLower())).ToList();
                            break;
                        case 1:
                            service = service.Where(x => x.Title.ToLower().Contains(searchBox.Text.ToLower())).ToList();
                            break;
                    }
                }
                LV.ItemsSource = service;
                countCurrentRecords.Text = $"Текущее количество записей: {service.Count()}";
                countAllRecords.Text = $"Общее количество записей: {ClassBase.BASE.Service.ToList().Count()}";
            }
            catch { }
        }

        void clearSort()
        {
            sortBox.SelectedIndex = 0;
            filterBox.SelectedIndex = 0;
            searchCBox.SelectedIndex = 0;
            searchBox.Text = "";
        }

        private void adminBTN_Click(object sender, RoutedEventArgs e)
        {
            if (!Open)
            {
                adminCon admin = new adminCon();
                Open = true;
                admin.Show();

                admin.Closing += (obj, args) =>
                {
                    if (isAdmin)
                    {
                        adminBTN.Visibility = Visibility.Collapsed;
                        addAndUpcomingBTNS.Visibility = Visibility.Visible;
                        clearSort();
                        LV.ItemsSource = ClassBase.BASE.Service.ToList();
                    }
                    else
                    {
                        adminBTN.Visibility = Visibility.Visible;
                        addAndUpcomingBTNS.Visibility = Visibility.Collapsed;
                        clearSort();
                        LV.ItemsSource = ClassBase.BASE.Service.ToList();
                    }
                    Open = false;
                };
            }
            else
            {
                MessageBox.Show("Окно уже открыто");
            }
        }

        private void addServiceBTN_Click(object sender, RoutedEventArgs e)
        {
            if (Open)
            {
                addService window = new addService();
                Open = true;
                window.Show();

                window.Closing += (obj, args) =>
                {
                    if (update)
                    {
                        clearSort();
                        LV.ItemsSource = ClassBase.BASE.Service.ToList();
                    }
                    update = false;
                    Open = false;
                    countAllRecords.Text = $"Общее количество записей: {ClassBase.BASE.Service.ToList().Count()}";
                };
            }
            else
            {
                MessageBox.Show("Окно уже открыто");
            }
        }

        private void upcomingEnteriesBTN_Click(object sender, RoutedEventArgs e)
        {
            if (!Open)
            {
                upcomingEnteries window = new upcomingEnteries();
                Open = true;
                window.Show();

                window.Closing += (obj, args) =>
                {
                    Open = false;
                };
            }
            else
            {
                MessageBox.Show("Окно уже открыто");
            }
        }

        private void previewI_Loaded(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32((sender as System.Windows.Controls.Image).Uid);
            Service s = ClassBase.BASE.Service.FirstOrDefault(x => x.ID == id);
            if (!String.IsNullOrEmpty(s.MainImagePath))
            {
                string dir = Environment.CurrentDirectory.Replace("bin\\Debug", s.MainImagePath);
                (sender as System.Windows.Controls.Image).Source = new BitmapImage(new Uri(dir));
            }
            else
            {
                string dir = Environment.CurrentDirectory.Replace("bin\\Debug", "Resources\\empty.jpg");
                (sender as System.Windows.Controls.Image).Source = new BitmapImage(new Uri(dir));
            }
        }

        private void nameServiceBox_Loaded(object sender, RoutedEventArgs e)
        {
            int tbUid = Convert.ToInt32((sender as TextBlock).Uid);
            Service service = ClassBase.BASE.Service.FirstOrDefault(x => x.ID == tbUid);
            (sender as TextBlock).Text = service.Title;
        }

        private void priceServiceBox_Loaded(object sender, RoutedEventArgs e)
        {

            int tbUid = Convert.ToInt32((sender as TextBlock).Uid);
            Service service = ClassBase.BASE.Service.FirstOrDefault(x => x.ID == tbUid);
            if (!String.IsNullOrEmpty(service.Discount.ToString()) && service.Discount != 0)
            {
                decimal cost = service.Cost - (service.Cost / 100 * Convert.ToDecimal(service.Discount));
                (sender as TextBlock).Text = Math.Round(cost, 0) + " рублей за " + (service.DurationInSeconds / 60) + " минут(ы)";
                (sender as TextBlock).Margin = new Thickness(10, 0, 0, 0);
            }
            else
            {
                (sender as TextBlock).Text = Math.Round(service.Cost, 0) + " рублей за " + (service.DurationInSeconds / 60) + " минут(ы)";
            }
        }

        private void discount_Loaded(object sender, RoutedEventArgs e)
        {
            int tbUid = Convert.ToInt32((sender as TextBlock).Uid);
            Service service = ClassBase.BASE.Service.FirstOrDefault(x => x.ID == tbUid);
            if (!String.IsNullOrEmpty(service.Discount.ToString()) && service.Discount != 0)
            {
                (sender as TextBlock).Text = "* скидка " + service.Discount + "%";
                (sender as TextBlock).Background = new SolidColorBrush(Color.FromRgb(181, 230, 29));
            }
        }

        private void costServiceBox_Loaded(object sender, RoutedEventArgs e)
        {
            int tbUid = Convert.ToInt32((sender as TextBlock).Uid);
            Service service = ClassBase.BASE.Service.FirstOrDefault(x => x.ID == tbUid);
            if (!String.IsNullOrEmpty(service.Discount.ToString()) && service.Discount != 0)
            {
                (sender as TextBlock).Text = Math.Round(service.Cost, 0).ToString();
                (sender as TextBlock).TextDecorations = TextDecorations.Strikethrough;
            }
        }

        private void buttons_Loaded(object sender, RoutedEventArgs e)
        {
            if (MainWindow.isAdmin)
            {
                (sender as StackPanel).Visibility = Visibility.Visible;
            }
        }

        private void changeBTN_Click(object sender, RoutedEventArgs e)
        {
            if (!Open)
            {
                int id = Convert.ToInt32((sender as Button).Uid);
                Service srvc = ClassBase.BASE.Service.FirstOrDefault(x => x.ID == id);
                addService window = new addService(srvc);
                Open = true;
                window.Show();

                window.Closing += (obj, args) =>
                {
                    clearSort();
                    LV.ItemsSource = ClassBase.BASE.Service.ToList();
                    Open = false;
                };
            }
            else
            {
                MessageBox.Show("Окно уже открыто");
            }
        }

        private void deleteBTN_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32((sender as Button).Uid);
            List<ClientService> cser = ClassBase.BASE.ClientService.Where(x => x.ServiceID == id).ToList();
            if (cser.Count == 0)
            {
                List<ServicePhoto> sphoto = ClassBase.BASE.ServicePhoto.Where(x => x.ServiceID == id).ToList();
                if (sphoto.Count != 0)
                {
                    foreach (ServicePhoto p in sphoto)
                    {
                        ClassBase.BASE.ServicePhoto.Remove(p);
                    }
                }
                Service s = ClassBase.BASE.Service.FirstOrDefault(x => x.ID == id);
                ClassBase.BASE.Service.Remove(s);
                ClassBase.BASE.SaveChanges();

                clearSort();
                LV.ItemsSource = ClassBase.BASE.Service.ToList();
                countAllRecords.Text = $"Общее количество записей: {ClassBase.BASE.Service.ToList().Count()}";
            }
            else
            {
                MessageBox.Show("Невозможно удалить услугу, так как на нее назаначена запись!");
            }
        }

        private void recordBTN_Click(object sender, RoutedEventArgs e)
        {
            //Здесь должен быть код, который будет записывать клиентов
        }

        private void borderB_Loaded(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32((sender as Border).Uid);
            Service s = ClassBase.BASE.Service.FirstOrDefault(x => x.ID == id);
            if (!String.IsNullOrEmpty(s.Discount.ToString()) && s.Discount != 0)
            {
                (sender as Border).BorderBrush = new SolidColorBrush(Color.FromRgb(181, 230, 29));
                (sender as Border).Background = new SolidColorBrush(Color.FromRgb(231, 250, 191));
            }
        }

    }
}
