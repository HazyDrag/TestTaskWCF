using System.Windows;
using AddressLibraryClient.LibraryServiceReference;

namespace AddressLibraryClient
{
    public partial class DescriptionBlank : Window
    {
        private Description oldDescription = new Description();

        public DescriptionBlank(AddressClient adrClient)
        {
            InitializeComponent();

            addressTextBox.Text = adrClient.Country + "\n" + adrClient.Region + "\n" + adrClient.City + "\n" + adrClient.Street + "\n" + adrClient.House;

            using (LibraryClient libClient = new LibraryClient("BasicHttpBinding_ILibrary")) //Получаем описание этого адреса
            {
                oldDescription.Text = libClient.ReadDescription(adrClient.Id);
            }

            oldDescription.Id = adrClient.Id;

            descriptionTextBox.Text = oldDescription.Text;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            string newText = descriptionTextBox.Text;
            if (oldDescription.Text != newText) //Если данные менялись, регистрируем изменения на сервисе
            {
                using (LibraryClient libClient = new LibraryClient("BasicHttpBinding_ILibrary"))
                {
                    Description description = new Description();
                    description.Id = oldDescription.Id;
                    description.Text = newText;

                    string result = libClient.EditDescription(description);
                    if (result == "OK")
                        this.Close();
                    else
                        MessageBox.Show(result, "Ошибка!");
                }
            }
            else
                this.Close();
        }
    }
}
