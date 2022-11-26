using ConsoleTools;
using L3AQTN_HFT_202231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace L3AQTN_HFT_202231.Client
{
    class Program
    {
        static RestService rest;

        static void Create(string entity)
        {
            if (entity == "Bus")
            {
                Console.Write("Enter Bus Model: ");
                string model = Console.ReadLine();
                Console.Write("Enter Bus BrandId: ");
                int brandid = int.Parse(Console.ReadLine());
                Console.Write("Enter Bus OwnerId: ");
                int owner =int.Parse( Console.ReadLine());
                Console.Write("Enter Bus Price: ");
                int price =int.Parse( Console.ReadLine());
                rest.Post(new Bus() { Model = model,BrandId=brandid,OwnerId=owner,Price=price }, "bus");
            }
            if (entity == "Brand")
            {
                Console.Write("Enter Brand Name: ");
                string name = Console.ReadLine();
                Console.Write("Enter Country: ");
                string country = (Console.ReadLine());
               
                rest.Post(new Brand() {Name=name,Country=country}, "brand");
            }
            if (entity == "Owner")
            {
                Console.Write("Enter Owner Name: ");
                string name = Console.ReadLine();
                Console.Write("Enter ZIPCode : ");
                int zip = int.Parse(Console.ReadLine());
                Console.Write("Enter HasMustache (true/false): ");
                bool mustache = bool.Parse(Console.ReadLine());
              
                rest.Post(new Owner() {Name=name,ZIPCode=zip,HasMustache=mustache  }, "owner");
            }
        }
        static void List(string entity)
        {
            if (entity == "Bus")
            {
                List<Bus> buses = rest.Get<Bus>("bus");
                foreach (var item in buses)
                {
                    Console.WriteLine(item.Brand.Name + ": " + item.Model);
                }
            }
            if (entity == "Brand")
            {
                List<Brand> brands = rest.Get<Brand>("brand");
                foreach (var item in brands)
                {
                    Console.WriteLine(item.Name + " Country: " + item.Country);
                }
            }
            if (entity == "Owner")
            {
                List<Owner> owners = rest.Get<Owner>("owner");
                foreach (var item in owners)
                {
                    Console.WriteLine(item.Name + " HasMustache: " + item.HasMustache);
                }
            }
            Console.ReadLine();
        }
        static void Update(string entity)
        {
            if (entity == "Bus")
            {
                Console.Write("Enter Bus's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Bus one = rest.Get<Bus>(id, "bus");
                Console.Write($"New model [old: {one.Model}]: ");
                string name = Console.ReadLine();
                one.Model = name;
                Console.Write($"New price [old: {one.Price}]: ");
                int price =int.Parse( Console.ReadLine());
                one.Price = price;
                rest.Put(one, "bus");
            }
            if (entity == "Owner")
            {
                Console.Write("Enter Owner's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Owner one = rest.Get<Owner>(id, "owner");
                Console.Write($"New ZIPCode [old: {one.ZIPCode}]: ");
                int zip = int.Parse(Console.ReadLine());
                one.ZIPCode = zip;
                Console.Write($"New HasMustache (true/false) [old: {one.HasMustache}]: ");
                bool mu = bool.Parse(Console.ReadLine());
                one.HasMustache = mu;
                rest.Put(one, "owner");
            }
            if (entity == "Brand")
            {
                Console.Write("Enter Brand's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Brand one = rest.Get<Brand>(id, "brand");
                Console.Write($"New Name [old: {one.Name}]: ");
                string name = Console.ReadLine();
                one.Name = name;
                Console.Write($"New country [old: {one.Country}]: ");
                string country = (Console.ReadLine());
                one.Country = country;
                rest.Put(one, "brand");
            }
        }
        static void Delete(string entity)
        {
            if (entity == "Bus")
            {
                Console.Write("Enter Bus's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "bus");

                
            }
            if (entity == "Brand")
            {
                Console.Write("Enter Brand's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "brand");
            }
            if (entity == "Owner")
            {
                Console.Write("Enter Owners's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "owner");
            }

           
            
        }
        //NON CRUD

        static void GetAvaragePriceByOwner(string endpoint)
        { //http://localhost:10615/Stat/AvaragePriceByOwner/Gyula
            Console.WriteLine("Owner name: ");
            var name = Console.ReadLine();
            var res = rest.GetNonCrud<double?>(endpoint,$"AvaragePriceByOwner/{name}");
            Console.WriteLine($"Avarage price by Owner: {res}");
            Console.ReadKey();
        }
        static void GetAvaragePriceByBrandCountry(string endpoint)
        { //http://localhost:10615/Stat/AvaragePriceByOwner/Gyula
            Console.WriteLine("Country name: ");
            var name = Console.ReadLine();
            var res = rest.GetNonCrud<double?>(endpoint, $"GetAvarageByCountry/{name}");
            Console.WriteLine($"Avarage price by country: {res}");
            Console.ReadKey();
        }
        static void HighestPriceByBrand(string endpoint)
        { //http://localhost:10615/Stat/AvaragePriceByOwner/Gyula
            Console.WriteLine("Brand name: ");
            var name = Console.ReadLine();
            var res = rest.GetNonCrud<double?>(endpoint, $"HighestPriceByBrand/{name}");
            Console.WriteLine($"Highest price: {res}");
            Console.ReadKey();
        }
        static void GetBusesByZipCode(string endpoint)
        { //http://localhost:10615/Stat/AvaragePriceByOwner/Gyula
            Console.WriteLine("ZIPCode: ");
            var code =int.Parse( Console.ReadLine());
            var res = rest.GetNonCrud<List<Bus>>(endpoint, $"BusesByZIPCode/{code}");
            Console.WriteLine($"List of buses: ");
            foreach (Bus item in res)
            {
                Console.WriteLine(item.ToString());
            }

            Console.ReadKey();
        }

        static void BusesByMustacheOwners(string endpoint)
        { //http://localhost:10615/Stat/AvaragePriceByOwner/Gyula
          
            var res = rest.GetNonCrud<List<Bus>>(endpoint, $"BusesWithMustacheOwners/");
            Console.WriteLine($"List of buses: ");
            foreach (Bus item in res)
            {
                Console.WriteLine(item.ToString());
            }

            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:10615/","bus");

            

            var busSubmenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Bus"))
                .Add("Create", () => Create("Bus"))
                .Add("Delete", () => Delete("Bus"))
                .Add("Update", () => Update("Bus"))
                .Add("Exit", ConsoleMenu.Close);

            var brandSubmenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Brand"))
                .Add("Create", () => Create("Brand"))
                .Add("Delete", () => Delete("Brand"))
                .Add("Update", () => Update("Brand"))
                .Add("Exit", ConsoleMenu.Close);

            var ownersSubmenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Owner"))
                .Add("Create", () => Create("Owner"))
                .Add("Delete", () => Delete("Owner"))
                .Add("Update", () => Update("Owner"))
                .Add("Exit", ConsoleMenu.Close);

            var noncrudSubMenu = new ConsoleMenu(args, level: 1)
                        .Add("BusesByZIPCode", () => GetBusesByZipCode("Stat"))
                        .Add("BusesWithMustacheOwners", () => BusesByMustacheOwners("Stat"))
                        .Add("GetAvaragePriceByBrandCountry", () => GetAvaragePriceByBrandCountry("Stat"))
                        .Add("GetAvaragePriceByOwner", () => GetAvaragePriceByOwner("Stat"))
                        .Add("HighestPriceByBrand", () => HighestPriceByBrand("Stat"))
                        .Add("Exit", ConsoleMenu.Close);

          
            var menu = new ConsoleMenu(args, level: 0)
                .Add("Bus", () => busSubmenu.Show())
                .Add("Owners", () => ownersSubmenu.Show())
                .Add("Brand", () => brandSubmenu.Show())
                .Add("Non-CRUD", () => noncrudSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();
        }
    }
}

