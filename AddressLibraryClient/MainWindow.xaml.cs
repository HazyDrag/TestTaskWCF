using System;
using System.Windows;

using AddressLibraryClient.LibraryServiceReference;

namespace AddressLibraryClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            loginButton.IsEnabled = false;

            using (LibraryClient libClient = new LibraryClient("BasicHttpBinding_ILibrary")) //новый клиент сервиса
            {
                try
                {
                    if (!string.Equals(libClient.TestConnection(), "OK", StringComparison.InvariantCultureIgnoreCase))
                        throw new Exception("Попытка соединения не удалась");
                    
                    labelConnectionStatus.Content = "Соединение успешно\nПроверка данных";

                    if (libClient.UserNameValidator(loginBox.Text, passwordBox.Password))
                    {
                        labelConnectionStatus.Content = "УСПЕШНО!\nДобро пожаловать, " + loginBox.Text;

                        Library lib = new Library();
                        lib.Show();
                        this.Close();
                    }
                    else
                        throw new Exception("Проверьте правильность введенных данных!");
                }
                catch (Exception ex)
                {
                    // выводим информацию об ошибке
                    labelConnectionStatus.Content = "Ошибка: " + ex.Message;
                }
            }
            loginButton.IsEnabled = true;
        }
    }
}
