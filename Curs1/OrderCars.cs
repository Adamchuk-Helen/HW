using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Curs1
{
    public class OrderCars
    {
        public OrderCars()
        { }
        public OrderCars(int id, string model, string car_number, string kuzov, string petrol, string cor_peredach, string class_cars, int prise, DateTime firstDay, DateTime lastDay, int prise_rent)
        {
            this.id = id;
            this.model = model;
            this.car_number = car_number;
            this.kuzov = kuzov;
            this.petrol = petrol;
            this.cor_peredach = cor_peredach;
            this.class_cars = class_cars;
            this.price = price;
            this.firstDay = firstDay;
            this.lastDay = lastDay;
            this.prise_rent = prise_rent;
        }
        public DateTime firstDay { get; set; }
        public DateTime lastDay { get; set; }
        public DateTime d { get; set; }
        public int prise_rent { get; set; }
        public string model { get; set; }
        public string car_number { get; set; }
        public string class_cars { get; set; }
        public int price { get; set; }
        public int id { get; set; }
        public string petrol { get; set; }
        public string cor_peredach { get; set; }
        public string kuzov { get; set; }
        public string user1 { get; set; }
        List<OrderCars> ordercars = new List<OrderCars>();
        List<Cars> cars = new List<Cars>();
        List<OrderCars> car_to_order = new List<OrderCars>();
        public OrderCars(string user1, DateTime firstDay, DateTime lastDay, int prise_rent )
        {
            this.user1 = user1;
            this.firstDay = firstDay;
            this.lastDay = lastDay;
            this.prise_rent = prise_rent;
        }
        public void New_Order_Car()
        {
            
            XmlSerializer myDeserializer1 = new XmlSerializer(typeof(List<OrderCars>));
            string file = Curs1.GlobalUser.user_log;
            int counter1 = 0;
            if (File.Exists(file + ".xml"))
            {
                using (StreamReader sr = new StreamReader(file + ".xml"))
                {
                    if (sr.ReadToEnd().Trim().Length > 0)
                    {
                        counter1++;
                        sr.Close();
                    }
                }
            }
            if (counter1 > 0)
            {
                using (FileStream fs3 = new FileStream(file + ".xml", FileMode.OpenOrCreate))
                {
                    ordercars = (List<OrderCars>)myDeserializer1.Deserialize(fs3);

                }
            }
            XmlSerializer myDeserializer = new XmlSerializer(typeof(List<Cars>));
            using (FileStream fs = new FileStream("cars.xml", FileMode.OpenOrCreate))
            {

                cars = ((List<Cars>)myDeserializer.Deserialize(fs));
                Console.WriteLine("Введіть Id автомобіля");
                int id1 = int.Parse(Console.ReadLine());
                int counter = 0;
                foreach (Cars u in cars)
                {
                    if (id1 == u.id)
                    {
                        Console.WriteLine("Введіть дату початку оренди в фоматі dd.mm.yyyy");
                        firstDay = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Введіть дату закінчення оренди в фоматі dd.mm.yyyy");
                        lastDay = DateTime.Parse(Console.ReadLine());
                        System.TimeSpan d = lastDay.Subtract(firstDay);
                        this.id = u.id;
                        this.model = u.model;
                        this.car_number = u.car_number;
                        this.kuzov = u.kuzov;
                        this.petrol = u.petrol;
                        this.cor_peredach = u.cor_peredach;
                        this.class_cars = u.class_cars;
                        this.price = u.price;
                        this.firstDay = firstDay;
                        this.lastDay = lastDay;
                        int pr= (int)d.TotalDays * u.price;
                        this.prise_rent = pr;
                        this.user1 = Curs1.GlobalUser.user_log;
                        ordercars.Add(new OrderCars(id, model, car_number, kuzov, petrol, cor_peredach, class_cars, price, firstDay, lastDay, prise_rent));
                        car_to_order.Add(new OrderCars(user1, firstDay, lastDay, prise_rent));
                        counter++;
                        using (FileStream fs2 = new FileStream(file + ".xml", FileMode.Create))
                        {
                            myDeserializer1.Serialize(fs2, ordercars);
                            fs2.Close();
                        }
                        //-------------------
                        XmlSerializer myDeserializer2 = new XmlSerializer(typeof(List<OrderCars>));
                        int counter2 = 0;
                        if (File.Exists(id1 + ".xml"))
                        {
                            using (StreamReader sr1 = new StreamReader(id1 + ".xml"))
                            {
                                if (sr1.ReadToEnd().Trim().Length > 0)
                                {
                                    counter2++;
                                    sr1.Close();
                                }
                            }
                        }
                        if (counter2 > 0)
                        {
                            using (FileStream fs4 = new FileStream(id1 + ".xml", FileMode.OpenOrCreate))
                            {
                                car_to_order = (List<OrderCars>)myDeserializer1.Deserialize(fs4);
                                fs4.Close();
                            }
                        }
                        using (FileStream fs5 = new FileStream(id1 + ".xml", FileMode.Create))
                        {
                            myDeserializer1.Serialize(fs5, car_to_order);
                            fs5.Close();
                        }
                    }
                    
                    //foreach (OrderCars order in ordercars)
                    //{ Console.WriteLine(order); }

                }
                if (counter == 0)
                { Console.WriteLine(" Id автомобіля не знайдено. Введіть Id автомобіля"); }
                fs.Close();
                
                User user = new User();
                user.Menu();
            }
        }

        public void Show_Order_User()
        {
            int counter = 0;
            XmlSerializer myDeserializer1 = new XmlSerializer(typeof(List<OrderCars>));
            string file = Curs1.GlobalUser.user_log;
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
            User user = new User();
            user.Menu();
        }

        public override string ToString()
        {
            return $"\nId: {id} \nМодель: {this.model} \nТип палива:  {this.petrol} \nТип кузова {this.kuzov} \nТип коробки передач {this.cor_peredach}\nКлас: {this.class_cars} \nНомер: {this.car_number} \nАвто орендовано з {this.firstDay} до  {this.lastDay} \nВартість оренди: {this.prise_rent} гривень";
        }

    }
}
