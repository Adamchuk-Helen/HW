using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

namespace Curs1
{
    public class Admin: IRole
    {
        void Admin_log()
        {
            XDocument xdox = new XDocument();
            XElement log_admin = new XElement("login");
            XAttribute name = new XAttribute("name", "admin");
            XElement pass = new XElement("password", "8888");
            log_admin.Add(name);
            log_admin.Add(pass);
            xdox.Add(log_admin);
            xdox.Save("admin.xml");
        }

        public void Show_admin()
        {
            XDocument xdox = XDocument.Load("admin.xml");
            foreach (XElement element in xdox.Elements("login"))
            {
                XAttribute log_admin = element.Attribute("name");
                XElement pass = element.Element("password");
                if (log_admin != null && pass != null)
                {
                    Console.WriteLine($"Name: {log_admin.Value}");
                    Console.WriteLine($"Pass: {pass.Value}");
                }
            }
            Menu();
        }

        public void Login()
        {
            Console.WriteLine("Введіть пароль");
            string user_pass = Console.ReadLine();
            XDocument xdox = XDocument.Load("admin.xml");
            foreach (XElement element in xdox.Elements("login"))
            {
                XAttribute log_admin = element.Attribute("name");
                XElement pass = element.Element("password");
                if (pass.Value == user_pass)
                {
                    Menu();
                }
                else 
                { Console.WriteLine("Не правильний пароль");
                  Login();

                }
            }
        }
        void Change_Pass()
        {
            XDocument xdox = XDocument.Load("admin.xml");
            Console.WriteLine("Введіть новий пароль");
            string new_pass = Console.ReadLine();
            xdox.Root.Element("password").Value = new_pass;
            xdox.Save("admin.xml");

            Menu();
        }
        public void Show_All_Cars()
        {
            Cars cars = new Cars();
            cars.Show_All_Cars();
            Menu();
        }
        public void Show_Cars_Order()
        {
            int counter = 0;
            Console.WriteLine("Введіть Id автомобіля");
            string file = Console.ReadLine();
            XmlSerializer myDeserializer1 = new XmlSerializer(typeof(List<OrderCars>));
            List<OrderCars> car_to_order = new List<OrderCars>();
            if (File.Exists(file + ".xml"))
            {
                using (StreamReader sr = new StreamReader(file + ".xml"))
                {
                    if (sr.ReadToEnd().Trim().Length > 0)
                    {
                        counter++;
                        sr.Close();
                    }

                    else
                    { Console.WriteLine("Поки що не було жодного замовлення"); }

                }
                if (counter > 0)
                {
                    using (FileStream fs = new FileStream(file + ".xml", FileMode.OpenOrCreate))
                    {
                        car_to_order = (List<OrderCars>)myDeserializer1.Deserialize(fs);
                        foreach (OrderCars order in car_to_order)
                        { Console.WriteLine($"\n\nЗамовник: {order.user1} \nЗамовлено з {order.firstDay} до {order.lastDay}  \nВартість замовлення: {order.prise_rent}"); }
                    }
                }
            }
            else { Console.WriteLine("Автомобіля з таким id не існує"); }
            Menu();
        }
        void Show_User_Orders()
        {
            Console.WriteLine("Введіть логін юзера");
            string file = Console.ReadLine();
            int counter = 0;
            XmlSerializer myDeserializer1 = new XmlSerializer(typeof(List<OrderCars>));
            List<OrderCars> ordercars = new List<OrderCars>();
            if (File.Exists(file + ".xml"))
            {
                using (StreamReader sr = new StreamReader(file + ".xml"))
                {
                    if (sr.ReadToEnd().Trim().Length > 0)
                    {
                        counter++;
                        sr.Close();
                    }

                    else
                    { Console.WriteLine("Поки що не було жодного замовлення"); }

                }
                if (counter > 0)
                {
                    using (FileStream fs = new FileStream(file + ".xml", FileMode.OpenOrCreate))
                    {
                        ordercars = (List<OrderCars>)myDeserializer1.Deserialize(fs);
                        foreach (OrderCars order in ordercars)
                        { Console.WriteLine(order); }
                    }
                }
            }
            else { Console.WriteLine("Юзер з таким іменем не існує"); }
            Menu();
        }
        public void Menu()
        {
            Console.WriteLine("\n----------Menu---------------");
            Console.WriteLine("1. Перегляд профілю адміністратора");
            Console.WriteLine("2. Змінити пароль");
            Console.WriteLine("3. Переглянути всіх юзерів");
            Console.WriteLine("4. Добавити юзера");
            Console.WriteLine("5. Редагування інформації про юзера");
            Console.WriteLine("6. Видалити юзера");
            Console.WriteLine("7. Добавити автомобіль");
            Console.WriteLine("8. Переглянути всі автомобілі");
            Console.WriteLine("9. Редагувати інформацію про автомобіль");
            Console.WriteLine("10. Видалити автомобіль");
            Console.WriteLine("11. Перегляд замовлень юзера");
            Console.WriteLine("12. Перегляд замовлень по автомобілю");
            User user = new User();
            Cars cars = new Cars();
            int ch = int.Parse(Console.ReadLine());
            if (ch >= 1 && ch <= 12)
            {
                switch (ch)
                {
                    case 1:
                        {
                            Show_admin();
                            break;
                        };
                    case 2:
                        {
                            Change_Pass();
                            break;
                        };
                    case 3:
                        {
                            user.Show_Users();
                            Menu();
                            break;
                        };
                    case 4:
                        {
                            user.Add_User();
                            break;
                        };
                    case 5:
                        {
                            user.Change_User();
                            break;
                        };
                    case 6:
                        {
                            user.Delete_User();
                            break;
                        };
                    case 7:
                        {
                            cars.Add_Cars();
                            break;
                        }
                    case 8:
                        {
                            Show_All_Cars();
                            break;
                        }
                    case 9:
                        {
                            cars.Change_Car();
                            break;
                        }
                    case 10:
                        {
                            cars.Delete_Car();
                            break;
                        }
                    case 11:
                        {
                            Show_User_Orders();
                            break;
                        }
                    case 12:
                        {
                            Show_Cars_Order();
                            break;
                        }
                }
            }
            else {
                Console.WriteLine("Не правильно введене значення. Спробуйте ще раз");
                Menu();
            }
        }
    }
}
