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
                else { Console.WriteLine("Не правильний пароль"); }
            }
        }
        void Change_Pass()
        {
            XDocument xdox = XDocument.Load("admin.xml");
            Console.WriteLine("Введіть новий пароль");
            string new_pass = Console.ReadLine();
   

            Menu();
        }
        public void Show_All_Cars()
        {
            Cars cars = new Cars();
            cars.Show_All_Cars();
            Menu();
        }


        public void Menu()
        {
            Console.WriteLine("\n----------Menu---------------");
            Console.WriteLine("1. Перегляд профілю адміністратора");
            Console.WriteLine("2. Змінити пароль");
            Console.WriteLine("3. Перегляд інформації про всіх юзерів");
            Console.WriteLine("4. Добавити юзера");
            Console.WriteLine("5. Редагування інформації про юзера");
            Console.WriteLine("6. Добавити автомобіль");
            Console.WriteLine("7. Переглянути всі автомобілі");
            Console.WriteLine("8. Редагувати інформацію про автомобіль");
            User user = new User();
            Cars cars = new Cars();
            int ch = int.Parse(Console.ReadLine());
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
                        cars.Add_Cars();
                        break;
                    }
                case 7:
                    {
                        Show_All_Cars();
                        break;
                    }
                case 8:
                    {
                        cars.Change_Car();
                        break;
                    }
            }
        }
    }
}
