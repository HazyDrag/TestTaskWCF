using System;
using System.Text;
using System.Windows;
using System.Security.Cryptography;

using AddressLibraryClient.LibraryServiceReference;

namespace AddressLibraryClient
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            helpLabel.Content = "Логин/пароль\ntestu/pass\nuser2/1111\nadmin/admin";
        }

        /// <summary>
        /// Вычисление MD5
        /// </summary>
        /// <returns> MD5-hash строки </returns>
        private static string MD5Hash(string input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(input));
            
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
                strBuilder.Append(result[i].ToString("x2"));

            return strBuilder.ToString();
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

                    if (libClient.UserNameValidator(loginBox.Text, MD5Hash(passwordBox.Password)))
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
                    loginBox.Text = MD5Hash(passwordBox.Password);
                }
            }
            loginButton.IsEnabled = true;
        }
    }
}
