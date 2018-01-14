using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace AddressLibraryService
{
    [ServiceContract]
    public interface ILibrary
    {
        #region Common Methods

        /// <summary>
        /// проверка соединения
        /// </summary>
        /// <returns> OK </returns>
        [OperationContract]
        string TestConnection();

        /// <summary>
        /// аутентификация
        /// </summary>
        /// <param name="login"> Имя пользователя </param>
        /// <param name="password"> пароль </param>
        /// <returns> true/false </returns>
        [OperationContract]
        bool UserNameValidator(string login, string password);

        #endregion

        #region Library Methods

        /// <summary>
        /// Чтение БД
        /// </summary>
        /// <returns> база данных адресов из XML-файла </returns>
        [OperationContract]
        List<Address> ReadAllLibrary();

        /// <summary>
        /// Корректировка адреса
        /// </summary>
        /// <param  name="address"> Измененный адрес </params>
        /// <returns> "OK" или сообщение об данных ошибки </returns> 
        [OperationContract]
        string EditAddress(Address address);

        /// <summary>
        /// Просмотр описания адреса
        /// </summary>
        /// <param  name="id"> Связанный ключ для конкретного адреса </params>
        /// <returns> Текстовое описание адреса </returns> 
        [OperationContract]
        string ReadDescription(int id);

        /// <summary>
        /// Корректировка описания адреса
        /// </summary>
        /// <param  name="description"> Измененное описание адреса </params>
        /// <returns> "OK" или сообщение об данных ошибки </returns>  
        [OperationContract]
        string EditDescription(Description description);
        #endregion      
    }

    #region Contract Classes

    [DataContract]
    public class Description
    {
        private int id;
        private string text;

        [DataMember]
        public int Id { get => id; set => id = value; }
        [DataMember]
        public string Text { get => text; set => text = value; }
                
        public Description(int id, string text)
        {
            this.id = id;
            this.text = text;
        }
    }

    [DataContract]
    public class Address
    {
        private int id;
        private string country;
        private string region;
        private string city;
        private string street;
        private string house;

        [DataMember]
        public string Country { get => country; set => country = value; }
        [DataMember]
        public string Region { get => region; set => region = value; }
        [DataMember]
        public string City { get => city; set => city = value; }
        [DataMember]
        public string Street { get => street; set => street = value; }
        [DataMember]
        public string House { get => house; set => house = value; }
        [DataMember]
        public int Id { get => id; set => id = value; }
        
        public Address(int id, string country, string region, string city, string street, string house)
        {
            this.Id = id;
            this.Country = country;
            this.Region = region;
            this.City = city;
            this.Street = street;
            this.House = house;
        }

        public Address()
        { }
    }

    #endregion 
}
