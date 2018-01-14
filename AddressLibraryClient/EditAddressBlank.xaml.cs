using System.Windows;
using AddressLibraryClient.LibraryServiceReference;

namespace AddressLibraryClient
{
    public partial class EditAddressBlank : Window
    {
        private Address oldAddress = new Address();
        private Address newAddress = new Address();

        public EditAddressBlank(AddressClient adrClient)
        {
            InitializeComponent();

            //Конвертируем в сериализованный класс
            oldAddress.Id = adrClient.Id;
            oldAddress.Country = adrClient.Country;
            oldAddress.Region = adrClient.Region;
            oldAddress.City = adrClient.City;
            oldAddress.Street = adrClient.Street;
            oldAddress.Id = adrClient.Id;

            countryTextBox.Text = adrClient.Country;
            regionTextBox.Text = adrClient.Region;
            cityTextBox.Text = adrClient.City;
            streetTextBox.Text = adrClient.Street;
            houseTextBox.Text = adrClient.House;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            newAddress.Id = oldAddress.Id;
            //Считываем в объект введеные данные
            newAddress.Country = countryTextBox.Text;
            newAddress.Region = regionTextBox.Text;
            newAddress.City = cityTextBox.Text;
            newAddress.Street = streetTextBox.Text;
            newAddress.House = houseTextBox.Text;

            if (oldAddress != newAddress) //проверяем, вносились ли изменения в данные адреса, если вносились, регистрируем изменения на сервисе            
            {
                using (LibraryClient libClient = new LibraryClient("BasicHttpBinding_ILibrary"))
                {
                    string result = libClient.EditAddress(newAddress);
                    if (result == "OK")
                        this.Close();
                    else
                        MessageBox.Show(result, "Ошибка!");
                }
            }
            else
                this.Close();
        }

        private void descriptionBlankButton_Click(object sender, RoutedEventArgs e)
        {
            AddressClient addressClient = new AddressClient(oldAddress.Id, countryTextBox.Text, regionTextBox.Text, cityTextBox.Text, streetTextBox.Text, houseTextBox.Text);

            DescriptionBlank descrBlank = new DescriptionBlank(addressClient);
            descrBlank.ShowDialog();
        }
    }
}
