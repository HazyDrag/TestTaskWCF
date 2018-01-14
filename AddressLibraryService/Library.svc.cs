using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace AddressLibraryService
{
    public class Library : ILibrary
    {
        #region Common Methods

        /// <summary>
        /// проверка соединения
        /// </summary>
        /// <returns> OK </returns>
        public string TestConnection()
        {
            return "OK";
        }

        /// <summary>
        /// аутентификация
        /// </summary>
        /// <param name="login"> Имя пользователя </param>
        /// <param name="password"> пароль </param>
        /// <returns> true/false </returns>
        public bool UserNameValidator(string login, string password)
        {
            var listOfUsers = ReadUsers();
            bool switcher = false;

            if (listOfUsers.Count > 0)
                foreach (User user in listOfUsers)
                {
                    if (user.Login == login)
                        if (user.Password == password)
                        {
                            switcher = true;
                            break;
                        }
                }

            return switcher;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Считываем из XML-файла данные всех пользователей и добавляем их в List
        /// </summary>
        /// <returns> список зарегистрированных пользователей </returns>
        private List<User> ReadUsers()
        {
            var listOfUsers = new List<User>();
            string path = @"C:\AddressLibrary";
            string filename = path + @"\usersdata.xml";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            if (!File.Exists(filename))
                CreateUsersFile();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(filename);
            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlNode usersNodes in xRoot)
            {
                XmlNode attr = usersNodes.Attributes.GetNamedItem("login");
                XmlNode attr2 = usersNodes.Attributes.GetNamedItem("password");

                User newUser = new User(attr.Value, attr2.Value);

                listOfUsers.Add(newUser);
            }
            
            return listOfUsers;
        }

        /// <summary>
        /// Создаем пример данных пользователей для аутентификации по пути "C:/AddressLibrary/usersdata.xml"
        /// </summary>
        private void CreateUsersFile()
        {
            List<User> users = new List<User> { new User("testu", "pass"), new User("user2", "1111"), new User("admin", "admin"), }; //List с примерами пользователей
            string path = @"C:\AddressLibrary";
            string filename = path + @"\usersdata.xml";
            
            XmlDocument xDoc = new XmlDocument();

            XmlNode docNode = xDoc.CreateXmlDeclaration("1.0", "windows-1251", null); //образуем заголовок
            xDoc.AppendChild(docNode);

            XmlNode usersNode = xDoc.CreateElement("users"); //теги users
            xDoc.AppendChild(usersNode);

            int counter = 0;
            foreach (User us in users)
            {
                counter++;

                XmlNode userNode = xDoc.CreateElement("user"); 
                XmlAttribute userAttribute = xDoc.CreateAttribute("id"); //запись данных пользователя в аттрибуты user
                userAttribute.Value = counter.ToString();
                userNode.Attributes.Append(userAttribute);

                userAttribute = xDoc.CreateAttribute("login");
                userAttribute.Value = us.Login;
                userNode.Attributes.Append(userAttribute);

                userAttribute = xDoc.CreateAttribute("password");
                userAttribute.Value = us.Password;
                userNode.Attributes.Append(userAttribute);

                usersNode.AppendChild(userNode);
            }

            xDoc.Save(filename);
        }

        /// <summary>
        /// Создаем пример БД по пути "C:/AddressLibrary/data.xml"
        /// </summary>
        private void CreateDataFile()
        {
            List<Address> addresses = new List<Address> {
                new Address(1, "Россия", "Ростовская обл.", "Ростов-на-Дону", "Ленина", "д.1, кв. 7"),
                new Address(2, "Россия", "Белгородская обл.", "Белгород", "Краснознаменная", "д.153"),
                new Address(3, "Россия", "Воронежская обл.", "Лиски", "Советская", "д.26"),
                new Address(4, "Россия", "Белгородская обл.", "Белгород", "проспект Победы", "д.3, кв. 14") }; //List с примерами адресов
            string path = @"C:\AddressLibrary";
            string filename = path + @"\data.xml";

            XmlDocument xDoc = new XmlDocument();

            XmlNode docNode = xDoc.CreateXmlDeclaration("1.0", "windows-1251", null); //образуем заголовок
            xDoc.AppendChild(docNode);

            XmlNode libraryNode = xDoc.CreateElement("library"); //теги library
            xDoc.AppendChild(libraryNode);
            
            foreach (Address adr in addresses)
            {
                XmlNode addressNode = xDoc.CreateElement("address");

                XmlAttribute addressAttribute = xDoc.CreateAttribute("id"); //запись данных адреса в аттрибуты address
                addressAttribute.Value = adr.Id.ToString();
                addressNode.Attributes.Append(addressAttribute);

                addressAttribute = xDoc.CreateAttribute("country");
                addressAttribute.Value = adr.Country;
                addressNode.Attributes.Append(addressAttribute);

                addressAttribute = xDoc.CreateAttribute("region");
                addressAttribute.Value = adr.Region;
                addressNode.Attributes.Append(addressAttribute);

                addressAttribute = xDoc.CreateAttribute("city");
                addressAttribute.Value = adr.City;
                addressNode.Attributes.Append(addressAttribute);

                addressAttribute = xDoc.CreateAttribute("street");
                addressAttribute.Value = adr.Street;
                addressNode.Attributes.Append(addressAttribute);

                addressAttribute = xDoc.CreateAttribute("house");
                addressAttribute.Value = adr.House;
                addressNode.Attributes.Append(addressAttribute);

                libraryNode.AppendChild(addressNode);
            }

            xDoc.Save(filename);
        }

        /// <summary>
        /// Создаем пример описаний адресов по пути "C:/AddressLibrary/description.xml"
        /// </summary>
        private void CreateDescriptionFile()
        {
            List<Description> descriptions = new List<Description> { new Description(1, "Вход со стороны двора"), new Description(3, "Код домофона - 367") }; //List с примерами описаний
            string path = @"C:\AddressLibrary";
            string filename = path + @"\description.xml";

            XmlDocument xDoc = new XmlDocument();

            XmlNode docNode = xDoc.CreateXmlDeclaration("1.0", "windows-1251", null); //образуем заголовок
            xDoc.AppendChild(docNode);

            XmlNode libraryNode = xDoc.CreateElement("library"); //теги library
            xDoc.AppendChild(libraryNode);

            foreach (Description desc in descriptions)
            {
                XmlNode descriptionNode = xDoc.CreateElement("description");

                XmlAttribute descriptionAttribute = xDoc.CreateAttribute("id");
                descriptionAttribute.Value = desc.Id.ToString();
                descriptionNode.Attributes.Append(descriptionAttribute);

                descriptionNode.InnerText = desc.Text;

                libraryNode.AppendChild(descriptionNode);
            }

            xDoc.Save(filename);
        }

        #endregion

        #region Library Methods

        /// <summary>
        /// Чтение БД
        /// </summary>
        /// <returns> база данных адресов из XML-файла </returns>        
        public List<Address> ReadAllLibrary()
        {
            var addresses = new List<Address>();
            string path = @"C:\AddressLibrary";
            string filename = path + @"\data.xml";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            if (!File.Exists(filename))
                CreateDataFile();
            
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(filename);
            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlNode addressNodes in xRoot)
            {
                Address addr = new Address();

                XmlNode attr = addressNodes.Attributes.GetNamedItem("id");
                addr.Id = Convert.ToInt32(attr.Value);
                attr = addressNodes.Attributes.GetNamedItem("country");
                addr.Country = attr.Value;
                attr = addressNodes.Attributes.GetNamedItem("region");
                addr.Region = attr.Value;
                attr = addressNodes.Attributes.GetNamedItem("city");
                addr.City = attr.Value;
                attr = addressNodes.Attributes.GetNamedItem("street");
                addr.Street = attr.Value;
                attr = addressNodes.Attributes.GetNamedItem("house");
                addr.House = attr.Value;

                addresses.Add(addr);
            }           

            return addresses;
        }

        /// <summary>
        /// Корректировка адреса
        /// </summary>
        /// <param  name="address"> Измененный адрес </params>
        /// <returns> "OK" или сообщение об данных ошибки </returns>    
        public string EditAddress(Address address)
        {
            try
            {
                string path = @"C:\AddressLibrary";
                string filename = path + @"\data.xml";

                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(filename);
                XmlElement xRoot = xDoc.DocumentElement;
                foreach (XmlNode xnodeAddresses in xRoot)
                {
                    XmlNode attr = xnodeAddresses.Attributes.GetNamedItem("id");
                    if (attr.Value == address.Id.ToString())
                    {
                        attr = xnodeAddresses.Attributes.GetNamedItem("country");
                        attr.Value = address.Country;
                        attr = xnodeAddresses.Attributes.GetNamedItem("region");
                        attr.Value = address.Region;
                        attr = xnodeAddresses.Attributes.GetNamedItem("city");
                        attr.Value = address.City;
                        attr = xnodeAddresses.Attributes.GetNamedItem("street");
                        attr.Value = address.Street;
                        attr = xnodeAddresses.Attributes.GetNamedItem("house");
                        attr.Value = address.House;

                        xDoc.Save(filename);
                        break;
                    }
                }

                return "OK";
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }
        
        /// <summary>
        /// Просмотр описания адреса
        /// </summary>
        /// <param  name="id"> Связанный ключ для конкретного адреса </params>
        /// <returns> Текстовое описание адреса </returns>   
        public string ReadDescription(int id)
        {
            string description = "Нет данных";

            string path = @"C:\AddressLibrary";
            string filename = path + @"\description.xml";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            if (!File.Exists(filename))
                CreateDescriptionFile();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(filename);
            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlNode descriptionNodes in xRoot)
            {
                XmlNode attr = descriptionNodes.Attributes.GetNamedItem("id");
                if (attr.Value == id.ToString())
                {
                    description = descriptionNodes.InnerText;
                    break;
                }
            }

            return description;
        }

        /// <summary>
        /// Корректировка описания адреса
        /// </summary>
        /// <param  name="description"> Измененное описание адреса </params>
        /// <returns> "OK" или сообщение об данных ошибки </returns>    
        public string EditDescription(Description description)
        {
            try
            {
                string path = @"C:\AddressLibrary";
                string filename = path + @"\description.xml";
                bool switcher = false;

                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(filename);
                XmlElement xRoot = xDoc.DocumentElement;
                foreach (XmlNode xnodeDescriptions in xRoot) //ищем, есть ли запись для этого адреса, и если есть, меняем ее
                {
                    XmlNode attr = xnodeDescriptions.Attributes.GetNamedItem("id");
                    if (attr.Value == description.Id.ToString())
                    {
                        attr.InnerText = description.Text;

                        xDoc.Save(filename);

                        switcher = true;
                        break;
                    }
                }
                if (!switcher)//если существующих записей для этого адреса не обнаружено, создаем новую
                {
                    XmlNode descriptionNode = xDoc.CreateElement("description");

                    XmlAttribute descriptionAttribute = xDoc.CreateAttribute("id");
                    descriptionAttribute.Value = description.Id.ToString();
                    descriptionNode.Attributes.Append(descriptionAttribute);

                    descriptionNode.InnerText = description.Text;

                    xRoot.AppendChild(descriptionNode);
                    xDoc.Save(filename);
                }
                return "OK";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
        #endregion        
    }
}
