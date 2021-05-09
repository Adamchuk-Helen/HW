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

        public Cars(string model, string car_number, string kuzov, string petrol, string cor_peredach, string class_cars, int price)
        {
            this.model = model;
            this.car_number = car_number;
            this.kuzov = kuzov;
            this.petrol = petrol;
            this.cor_peredach = cor_peredach;
            this.class_cars = class_cars;
            this.price = price;
           
        }

        public string model { get; set; }
        public string car_number { get; set; }
        public string class_cars { get; set; }
        public int price { get; set; }
    
        public string petrol { get; set; }
        public string cor_peredach { get; set; }
        public string kuzov { get; set; }
    
        List<Cars> cars = new List<Cars>();
        public void Add_Cars()
        {
            XmlSerializer myDeserializer = new XmlSerializer(typeof(List<Cars>));

            using (FileStream myFileStream = new FileStream("cars.xml", FileMode.Open))
            {
                cars = (List<Cars>)myDeserializer.Deserialize(myFileStream);
                myFileStream.Close();
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
 
            this.model = model;
            this.car_number = car_number;
            this.kuzov = kuzov;
            this.petrol = petrol;
            this.cor_peredach = cor_peredach;
            this.class_cars = class_cars;
            this.price = price;


            cars.Add(new Cars(model, car_number, kuzov, petrol, cor_peredach, class_cars, price));

            using (FileStream fs = new FileStream("cars.xml", FileMode.OpenOrCreate))
            {
                myDeserializer.Serialize(fs, cars);
                fs.Close();
            }

            
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
                    Console.WriteLine($"\nМодель: {u.model} \nТип палива:  {u.petrol} \nТип кузова {u.kuzov} \nТип коробки передач {u.cor_peredach}\nКлас: {u.class_cars} \nНомер: {u.car_number} \nЦіна в грн за добу: {u.price}");
                }
               
            }
          
        }

        public void Change_Car()
        {
            Admin admin = new Admin();
            Console.WriteLine("Введіть номери автомобіля");
            string car_n = Console.ReadLine();
            XmlSerializer myDeserializer = new XmlSerializer(typeof(List<Cars>));

            using (FileStream fs3 = new FileStream("cars.xml", FileMode.OpenOrCreate))
            {
                List<Cars> cars = ((List<Cars>)myDeserializer.Deserialize(fs3));

                foreach (Cars u in cars)
                {
                    if (car_n == u.car_number)
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
                        int price = int.Parse(Console.ReadLine());
                        if (model == "")
                        {
                            u.model = u.model;
                        }
                        else 
                        {
                            u.model = model;
                        }
                        if (car_number == "")
                        {
                            u.car_number = u.car_number;
                        }
                        else
                        {
                            u.car_number = car_number;
                        }
                        if (kuzov == "")
                        {
                            u.kuzov = u.kuzov;
                        }
                        else
                        {
                            u.kuzov = kuzov;
                        }
                        if (petrol == "")
                        {
                            u.petrol = u.petrol;
                        }
                        else
                        {
                            u.petrol = petrol;
                        }
                        if (cor_peredach == "")
                        {
                            u.cor_peredach = u.cor_peredach;
                        }
                        else
                        {
                            u.cor_peredach = cor_peredach;
                        }
                        if (class_cars == "")
                        {
                            u.class_cars = u.class_cars;
                        }
                        else
                        {
                            u.class_cars = class_cars;
                        }
                        if (price !<0 )
                        {
                            u.price = u.price;
                        }
                        else
                        {
                            u.price = price;
                        }
                        
                        Console.WriteLine(u);
                    }
                   
                     
                }
                fs3.Close();
                
                using (FileStream fs1 = new FileStream("cars.xml", FileMode.OpenOrCreate))
                {
                    myDeserializer.Serialize(fs1, cars);
                }

            }
           
            admin.Menu();
        }
        public override string ToString()
        {
            return $" \nМодель: {this.model} \nТип палива:  {this.petrol} \nТип кузова {this.kuzov} \nТип коробки передач {this.cor_peredach}\nКлас: {this.class_cars} \nНомер: {this.car_number} \nЦіна в грн за добу: {this.price}";
        }
    }
}
