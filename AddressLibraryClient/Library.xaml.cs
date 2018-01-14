using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using AddressLibraryClient.LibraryServiceReference;

namespace AddressLibraryClient
{
    public partial class Library : Window
    {
        public Library()
        {
            InitializeComponent();
        }

        private void editAddressBlankButton_Click(object sender, RoutedEventArgs e)
        {
            if (libraryDataGrid.SelectedIndex != -1)
            {
                AddressClient adrClient = libraryDataGrid.SelectedItem as AddressClient; //получаем выбранный элемент в таблице

                EditAddressBlank editAddrBlank = new EditAddressBlank(adrClient);
                editAddrBlank.ShowDialog();
                UploadData();
            }
            else
                MessageBox.Show("Выберите адрес для просмотра и корректировки его атрибутов!");
        }

        private void libraryDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            UploadData();
        }

        private void UploadData()
        {
            List<AddressClient> addresses = new List<AddressClient>();

            //Переписываем полученный List с сервиса в собственный класс клиента, так как напрямую полученный List не привязать, потому что полученный класс сериализован
            using (LibraryClient libClient = new LibraryClient("BasicHttpBinding_ILibrary")) //новый клиент сервиса
                foreach (Address adr in libClient.ReadAllLibrary())//считываем данные об адресах из сервиса
                {
                    AddressClient adrClient = new AddressClient(adr.Id, adr.Country, adr.Region, adr.City, adr.Street, adr.House);
                    addresses.Add(adrClient);
                }
            if (addresses.Count > 0)
            {
                libraryDataGrid.ItemsSource = addresses;
            }
            else
            {
                MessageBox.Show("Данных в библиотеке нет");
            }
        }

        private void descriptionBlankButton_Click(object sender, RoutedEventArgs e)
        {
            if (libraryDataGrid.SelectedIndex != -1)
            {
                AddressClient adrClient = libraryDataGrid.SelectedItem as AddressClient; //получаем выбранный элемент в таблице

                DescriptionBlank descrBlank = new DescriptionBlank(adrClient);
                descrBlank.ShowDialog();
            }
            else
                MessageBox.Show("Выберите адрес для просмотра и корректировки его описания!");
        }

        private void printButton_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {

                using (LibraryClient libClient = new LibraryClient("BasicHttpBinding_ILibrary"))
                {
                    int countOfAddresses = libClient.ReadAllLibrary().Count();
                    int height = 30 + countOfAddresses * 20;

                    double heightDataGrid = libraryDataGrid.Height; //изменяем размеры окна, чтобы туда попали все записи
                    double heightWindow = libraryWindow.Height;

                    libraryDataGrid.Height = height;
                    libraryWindow.Height = height + 50;
                    libraryDataGrid.Margin = new Thickness(0, 30, 0, 0);

                    printDialog.PrintVisual(libraryDataGrid, "Печать списка всех адресов");

                    libraryDataGrid.Height = heightDataGrid;//возвращаем первоначальные значения размеров
                    libraryWindow.Height = heightWindow;
                    libraryDataGrid.Margin = new Thickness(0, 0, 0, 0);
                }
            }
        }
    }
}
