using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace School
{
    /// <summary>
    /// Логика взаимодействия для addService.xaml
    /// </summary>
    public partial class addService : Window
    {
        Service service;
        bool update = false;
        public static bool Open = false;

        public addService()
        {
            InitializeComponent();
        }

        public addService(Service ser)
        {
            InitializeComponent();
            service = ser;
            update = true;
            buttons.Visibility = Visibility.Visible;
            addImage.Visibility = Visibility.Hidden;
            saveBTN.Content = "Изменить";

            ID.Content = ser.ID;
            TitleBox.Text = ser.Title;
            CostBox.Text = ser.Cost.ToString();
            DiscountBox.Text = ser.Discount.ToString();
            DurationBox.Text = ser.DurationInSeconds.ToString();
            DescriptionBox.Text = ser.Description;

            if (!String.IsNullOrEmpty(ser.MainImagePath))
            {
                string dir = Environment.CurrentDirectory.Replace("bin\\Debug", ser.MainImagePath);
                imageI.Source = new BitmapImage(new Uri(dir));
            }
            else
            {
                string dir = Environment.CurrentDirectory.Replace("bin\\Debug", "Resources\\picture.png");
                imageI.Source = new BitmapImage(new Uri(dir));
            }
        }

        private void deletePhotoBTN_Click(object sender, RoutedEventArgs e)
        {
            service.MainImagePath = "Resources\\picture.png";
            ClassBase.BASE.SaveChanges();

            if (!String.IsNullOrEmpty(service.MainImagePath))
            {
                string dir = Environment.CurrentDirectory.Replace("bin\\Debug", service.MainImagePath);
                imageI.Source = new BitmapImage(new Uri(dir));
            }
            else
            {
                string dir = Environment.CurrentDirectory.Replace("bin\\Debug", "Resources\\picture.png");
                imageI.Source = new BitmapImage(new Uri(dir));
            }
        }

        private void backBTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void addImageBTN_Click(object sender, RoutedEventArgs e)
        {
            //Здесь должен быть код, который будет добавлять картинку
        }

        private void GaleryBTN_Click(object sender, RoutedEventArgs e)
        {
            //Здесь должен быть код, который будет открывать галерею
        }

        private void saveBTN_Click(object sender, RoutedEventArgs e)
        {
            //Здесь должен быть код, который сохраняет измененую запись
        }
    }
}
