using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Curs1
{
   
    public class User : IRole
    {
        public void Enter()
        {
            Console.WriteLine("1. Вхід");
            Console.WriteLine("2. Реєстрація");
            User user = new User();
            int ch = int.Parse(Console.ReadLine());
            if (ch >= 1 && ch <= 2)
            {
                switch (ch)
                {
                    case 1:
                        {
                            Login();
                            break;
                        };
                    case 2:
                        {
                            Register();
                            break;
                        };
                }
            }
            else
            {
                Console.WriteLine("Не правильно введене значення. Спробуйте ще раз");
                Enter();
            }
        }
        public void Login()
        {
            Admin admin = new Admin();
            Console.WriteLine("Введіть логін");
            string user_login = Console.ReadLine();
            if (user_login == "admin")
            {
                admin.Login();
            }
            else
            {
                Console.WriteLine("Введіть пароль");
                string user_pass = Console.ReadLine();
                XmlSerializer myDeserializer = new XmlSerializer(typeof(List<User>));

                using (FileStream fs = new FileStream("users.xml", FileMode.OpenOrCreate))
                {
                    List<User> user = ((List<User>)myDeserializer.Deserialize(fs));
                    int counter = 0;
                    foreach (User u in user)
                    {
                        if (user_login == u.user_login && user_pass == u.user_pass)
                        {
                            counter++;
                        }
                        fs.Close();
                    }
                    if (counter > 0)
                    {
                        Curs1.GlobalUser.user_log = user_login;
                        Menu(); }
                    else
                    {
                        Console.WriteLine("Не правильний логін або пароль");
                    }
                    Enter();
                }
                
            }
        }

        public User()
        { }
        public string user_login { get; set; }
        public string user_pass { get; set; }
        public string user_name { get; set; }
        public string user_surname { get; set; }
        public string user_phone { get; set; }

        public static int Id;
        public User(string user_login, string user_pass, string user_name, string user_surname, string user_phone)
        {
            this.user_login = user_login;
            this.user_pass = user_pass;
            this.user_name = user_name;
            this.user_surname = user_surname;
            this.user_phone = user_phone;

        }
        List<User> user = new List<User>();
        OrderCars order = new OrderCars();
        public void Menu()
        {
            Console.WriteLine("\n1. Перегляд особистої інформації");
            Console.WriteLine("2. Редагувати особистої інформації");
            Console.WriteLine("3. Переглянути всі автомобілі");
            Console.WriteLine("4. Замовити автомобіль");
            Console.WriteLine("5. Перегляд всіх замовлень");

            int ch = int.Parse(Console.ReadLine());
            if (ch >= 1 && ch <= 5)
            {
                switch (ch)
                {
                    case 1:
                        {
                            User_Information();
                            break;
                        };
                    case 2:
                        {
                            Change_information();
                            break;
                        };
                    case 3:
                        {
                            Show_All_Cars();
                            break;
                        };
                    case 4:
                        {
                            order.New_Order_Car();
                            break;
                        };
                    case 5:
                        {
                            order.Show_Order_User();
                            break;
                        };

                }
            }
            else {
                Console.WriteLine("Не правильно введене значення. Спробуйте ще раз");
                Menu();
            }
        }

        public void Register()
        {
            XmlSerializer myDeserializer = new XmlSerializer(typeof(List<User>));
            if (File.Exists("users.xml"))
            {
                using (FileStream myFileStream = new FileStream("users.xml", FileMode.Open))
                {
                    user = (List<User>)myDeserializer.Deserialize(myFileStream);
                }
            }
            Console.WriteLine("\nВведіть логін");
            string user_login = Console.ReadLine();
            foreach (User u in user)
            {
                if (u.user_login == user_login || user_login=="admin")
                {
                    Console.WriteLine("\nЮзер з таким логіном уже існує. Спробуйте інший логін");
                    Register();
                }
            }
            Console.WriteLine("Введіть пароль");
            string user_pass = Console.ReadLine();
            Console.WriteLine("Введіть ім'я");
            string user_name = Console.ReadLine();
            Console.WriteLine("Введіть прізвище");
            string user_surname = Console.ReadLine();
            Console.WriteLine("Введіть номер телефону");
            string user_phone = Console.ReadLine();
            Curs1.GlobalUser.user_log = user_login;
            this.user_login = user_login;
            this.user_pass = user_pass;
            this.user_name = user_name;
            this.user_surname = user_surname;
            this.user_phone = user_phone;
            File.Create(user_login + ".xml");
            user.Add(new User(user_login, user_pass, user_name, user_surname, user_phone));

            using (FileStream fs = new FileStream("users.xml", FileMode.OpenOrCreate))
            {
                myDeserializer.Serialize(fs, user);
            }
            Menu();
        }
        void Change_information()
        {
            XmlSerializer myDeserializer = new XmlSerializer(typeof(List<User>));

            using (FileStream fs = new FileStream("users.xml", FileMode.OpenOrCreate))
            {
                List<User> user = ((List<User>)myDeserializer.Deserialize(fs));

                foreach (User u in user)
                {
                    if (Curs1.GlobalUser.user_log == u.user_login)
                    {
                        Console.WriteLine("Введіть пароль");
                        string user_pass = Console.ReadLine();
                        Console.WriteLine("Введіть ім'я");
                        string user_name = Console.ReadLine();
                        Console.WriteLine("Введіть прізвище");
                        string user_surname = Console.ReadLine();
                        Console.WriteLine("Введіть номер телефону");
                        string user_phone = Console.ReadLine();
                        u.user_login = Curs1.GlobalUser.user_log;
                        if (user_pass == "")
                        {
                            continue;
                        }
                        else
                        {
                            u.user_pass = user_pass;
                        }
                        if (user_name == "")
                        {
                            continue;
                        }
                        else
                        {
                            u.user_name = user_name;
                        }
                        if (user_surname == "")
                        {
                            continue;
                        }
                        else
                        {
                            u.user_surname = user_surname;
                        }
                        if (user_phone == "")
                        {
                            continue;
                        }
                        else
                        {
                            u.user_phone = user_phone;
                        }

                        continue;
                    }
                }
                fs.Close();

                using (FileStream fs1 = new FileStream("users.xml", FileMode.OpenOrCreate))
                {
                    myDeserializer.Serialize(fs1, user);
                }

            }
            Menu();
        }
        void User_Information()
        {
            XmlSerializer myDeserializer = new XmlSerializer(typeof(List<User>));

            using (FileStream fs1 = new FileStream("users.xml", FileMode.OpenOrCreate))
            {
                List<User> user = ((List<User>)myDeserializer.Deserialize(fs1));

                foreach (User u in user)
                {
                    if (Curs1.GlobalUser.user_log == u.user_login)
                    {
                        Console.WriteLine(u);
                    }
                }
                fs1.Close();

            }
            Menu();
        }
        public void Add_User()
        {
            XmlSerializer myDeserializer = new XmlSerializer(typeof(List<User>));
            if (File.Exists("users.xml"))
            {
                using (FileStream myFileStream = new FileStream("users.xml", FileMode.Open))
                {
                    user = (List<User>)myDeserializer.Deserialize(myFileStream);
                    myFileStream.Close();
                }
            }

            Console.WriteLine("Введіть логін");
            string user_login = Console.ReadLine();
            Console.WriteLine("Введіть пароль");
            string user_pass = Console.ReadLine();
            Console.WriteLine("Введіть ім'я");
            string user_name = Console.ReadLine();
            Console.WriteLine("Введіть прізвище");
            string user_surname = Console.ReadLine();
            Console.WriteLine("Введіть номер телефону");
            string user_phone = Console.ReadLine();
            this.user_login = user_login;
            this.user_pass = user_pass;
            this.user_name = user_name;
            this.user_surname = user_surname;
            this.user_phone = user_phone;

            user.Add(new User(user_login, user_pass, user_name, user_surname, user_phone));
            File.Create(user_login+".xml");
            using (FileStream fs = new FileStream("users.xml", FileMode.OpenOrCreate))
            {
                myDeserializer.Serialize(fs, user);
                fs.Close();
            }

            Admin admin = new Admin();
            admin.Menu();
        }
        public void Change_User()
        {
            Admin admin = new Admin();
            Console.WriteLine("Введіть логін юзера");
            string user_login = Console.ReadLine();
            XmlSerializer myDeserializer = new XmlSerializer(typeof(List<User>));

            using (FileStream fs = new FileStream("users.xml", FileMode.OpenOrCreate))
            {
                List<User> user = ((List<User>)myDeserializer.Deserialize(fs));

                foreach (User u in user)
                {
                    if (user_login == u.user_login)
                    {
                        Console.WriteLine("Введіть пароль");
                        string user_pass = Console.ReadLine();
                        Console.WriteLine("Введіть ім'я");
                        string user_name = Console.ReadLine();
                        Console.WriteLine("Введіть прізвище");
                        string user_surname = Console.ReadLine();
                        Console.WriteLine("Введіть номер телефону");
                        string user_phone = Console.ReadLine();
                        //u.user_login = Curs1.GlobalUser.user_log;
                        if (user_pass == "")
                        { 
                        }
                        else
                        {
                            u.user_pass = user_pass;
                        }
                        if (user_name == "")
                        {  
                        }
                        else
                        {
                            u.user_name = user_name;
                        }
                        if (user_surname == "")
                        {   
                        }
                        else
                        {
                            u.user_surname = user_surname;
                        }
                        if (user_phone == "")
                        {    
                        }
                        else
                        {
                            u.user_phone = user_phone;
                        }
 
                    }
                }
                fs.Close();

                using (FileStream fs1 = new FileStream("users.xml", FileMode.OpenOrCreate))
                {
                    myDeserializer.Serialize(fs1, user);
                }

            }
            admin.Menu();
        }
        public void Show_Users()
        {
            XmlSerializer myDeserializer = new XmlSerializer(typeof(List<User>));

            using (FileStream fs = new FileStream("users.xml", FileMode.OpenOrCreate))
            {
                List<User> user= ((List < User > )myDeserializer.Deserialize(fs));

                foreach (User u in user)
                {
                    Console.WriteLine($" \nЛогін: {u.user_login}  \nПароль: {u.user_pass}  \nІмя: {u.user_name}   \nПрізвище: {u.user_surname}  \nТелефон {u.user_phone}" );
                }
            }
        }
        public void Show_All_Cars()
        {
            Cars cars = new Cars();
            cars.Show_All_Cars();
            Menu();
        }
        public void Delete_User()
        {
            Admin admin = new Admin();
            Console.WriteLine("Введіть логін юзера");
            string user_login = Console.ReadLine();
            XmlSerializer myDeserializer = new XmlSerializer(typeof(List<User>));

            using (FileStream fs = new FileStream("users.xml", FileMode.OpenOrCreate))
            {
                List<User> user = ((List<User>)myDeserializer.Deserialize(fs));
                int counter = 0;
                foreach (User u in user)
                {
                    if (user_login == u.user_login)
                    {
                        user.RemoveAt(counter);
                    }
                    else { counter++; }

                }
                
                fs.Close();

                using (FileStream fs1 = new FileStream("users.xml", FileMode.Create))
                {
                    myDeserializer.Serialize(fs1, user);
                    fs1.Close();
                }
            }

            admin.Menu();
        }

        public override string ToString()
        {
            return $"\nЛогін {this.user_login}  \nПароль {this.user_pass} \nІм'я {this.user_name} \nПрізвище  {this.user_surname} \nТелефон {this.user_phone}";
        }
    }
}
