using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Curs1
{
    public class Cars
    {
        public Cars()
        { }

        public Cars(int id, string model, string car_number, string kuzov, string petrol, string cor_peredach, string class_cars, int price)
        {
            this.model = model;
            this.car_number = car_number;
            this.kuzov = kuzov;
            this.petrol = petrol;
            this.cor_peredach = cor_peredach;
            this.class_cars = class_cars;
            this.price = price;
            this.id = carId;
        }

        public string model { get; set; }
        public string car_number { get; set; }
        public string class_cars { get; set; }
        public int price { get; set; }
        public int id { get; set; }
        public string petrol { get; set; }
        public string cor_peredach { get; set; }
        public string kuzov { get; set; }
        public static int carId = 0;
        List<Cars> cars = new List<Cars>();
        public void Add_Cars()
        {
            XmlSerializer myDeserializer = new XmlSerializer(typeof(List<Cars>));
            if (File.Exists("cars.xml")) 
            {
                using (FileStream myFileStream = new FileStream("cars.xml", FileMode.Open))
                {
                    cars = (List<Cars>)myDeserializer.Deserialize(myFileStream);
                    foreach (Cars u in cars)
                    {
                        carId = u.id;
                    }
                    
                    myFileStream.Close();
                }
            }
            Console.WriteLine("\nВведіть модель автомобіля");
            string model = Console.ReadLine();
            Console.WriteLine("Введіть номер автомобіля");
            string car_number = Console.ReadLine();
            Console.WriteLine("Введіть тип кузова");
            string kuzov = Console.ReadLine();
            Console.WriteLine("Введіть тип палива");
            string petrol = Console.ReadLine();
            Console.WriteLine("Введіть тип коробки передач");
            string cor_peredach = Console.ReadLine();
            Console.WriteLine("Введіть клас автомобіля ");
            string class_cars = Console.ReadLine();
            Console.WriteLine("Введіть ціну прокату за добу");
            int price = int.Parse(Console.ReadLine());
            carId++;
            this.model = model;
            this.car_number = car_number;
            this.kuzov = kuzov;
            this.petrol = petrol;
            this.cor_peredach = cor_peredach;
            this.class_cars = class_cars;
            this.price = price;
            this.id = carId;
            cars.Add(new Cars(id, model, car_number, kuzov, petrol, cor_peredach, class_cars, price));

            using (FileStream fs = new FileStream("cars.xml", FileMode.Create))
            {
                myDeserializer.Serialize(fs, cars);
                fs.Close();
            }
            string file = carId.ToString();
            File.Create(file +".xml");
            Admin admin = new Admin();
            admin.Menu();
        }
        public void Show_All_Cars()
        {
            XmlSerializer myDeserializer = new XmlSerializer(typeof(List<Cars>));

            using (FileStream fs = new FileStream("cars.xml", FileMode.OpenOrCreate))
            {
                List<Cars> cars = ((List<Cars>)myDeserializer.Deserialize(fs));

                foreach (Cars u in cars)
                {
                    Console.WriteLine($"\nId: {u.id} \nМодель: {u.model} \nТип палива:  {u.petrol} \nТип кузова: {u.kuzov} \nТип коробки передач: {u.cor_peredach}\nКлас: {u.class_cars} \nНомер: {u.car_number} \nЦіна в грн за добу: {u.price}");
                }
                fs.Close();
            }
          
        }
        public void Change_Car()
        {
            Admin admin = new Admin();
            Console.WriteLine("Введіть Id автомобіля");
            int car_n = int.Parse(Console.ReadLine());
            XmlSerializer myDeserializer = new XmlSerializer(typeof(List<Cars>));

            using (FileStream fs3 = new FileStream("cars.xml", FileMode.OpenOrCreate))
            {
                List<Cars> cars = ((List<Cars>)myDeserializer.Deserialize(fs3));

                foreach (Cars u in cars)
                {
                    if (car_n == u.id)
                    {
                        Console.WriteLine("\nВведіть модель автомобіля");
                        string model = Console.ReadLine();
                        Console.WriteLine("Введіть номер автомобіля");
                        string car_number = Console.ReadLine();
                        Console.WriteLine("Введіть тип кузова");
                        string kuzov = Console.ReadLine();
                        Console.WriteLine("Введіть тип палива");
                        string petrol = Console.ReadLine();
                         Console.WriteLine("Введіть тип коробки передач");
                        string cor_peredach = Console.ReadLine();
                        Console.WriteLine("Введіть клас автомобіля ");
                        string class_cars = Console.ReadLine();
                        Console.WriteLine("Введіть ціну прокату за добу");
                        int price = -1;
                        if (!int.TryParse(Console.ReadLine(), out price))
                        { price = -1; }

                        if (model == "")
                        {

                        }
                        else
                        {
                            u.model = model;
                        }
                        if (car_number == "")
                        {

                        }
                        else
                        {
                            u.car_number = car_number;
                        }
                        if (kuzov == "")
                        {

                        }
                        else
                        {
                            u.kuzov = kuzov;
                        }
                        if (petrol == "")
                        {

                        }
                        else
                        {
                            u.petrol = petrol;
                        }
                        if (cor_peredach == "")
                        {

                        }
                        else
                        {
                            u.cor_peredach = cor_peredach;
                        }
                        if (class_cars == "")
                        {

                        }
                        else
                        {
                            u.class_cars = class_cars;
                        }

                        if (price == -1)
                        {

                        }
                        else
                        {
                            u.price = price;
                        }
                    }
                }
                fs3.Close();

                using (FileStream fs1 = new FileStream("cars.xml", FileMode.Create))
                {
                    myDeserializer.Serialize(fs1, cars);
                    fs1.Close();
                }
            }

            admin.Menu();
        }
        public void Delete_Car()  
        {
            Admin admin = new Admin();
            Console.WriteLine("Введіть номер автомобіля");
            string car_n = Console.ReadLine();
            XmlSerializer myDeserializer = new XmlSerializer(typeof(List<Cars>));

            using (FileStream fs3 = new FileStream("cars.xml", FileMode.OpenOrCreate))
            {
                List<Cars> cars = ((List<Cars>)myDeserializer.Deserialize(fs3));
                int counter = 0;

                foreach (Cars u in cars)
                {

                    if (car_n == u.car_number)
                    {
                        cars.RemoveAt(counter);
                        break;
                    }
                    else
                    { counter++; }
                    
                }
                
                foreach (Cars u in cars)
                {
                    Console.WriteLine(u);

                }

                fs3.Close();

                using (FileStream fs1 = new FileStream("cars.xml", FileMode.Create))
                {
                    myDeserializer.Serialize(fs1, cars);
                    fs1.Close();
                }
            }

            admin.Menu();
        }
        public override string ToString()
        {
            return $"\nId: {id} \nМодель: {this.model} \nТип палива:  {this.petrol} \nТип кузова {this.kuzov} \nТип коробки передач {this.cor_peredach}\nКлас: {this.class_cars} \nНомер: {this.car_number} \nЦіна в грн за добу: {this.price}";
        }
    }
}
