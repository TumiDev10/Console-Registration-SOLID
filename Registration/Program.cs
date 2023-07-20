using System;
using Registration.Services;
using Registration.Handler;
using Registration.Model;


namespace Registration
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=HO-TUMIM;Initial Catalog=RegistrationDB;Integrated Security=True;Persist Security Info=False;Pooling=False;Connect Timeout=60;";

            while (true)
            {
                Console.WriteLine("Welcome to the Registration App");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Display Data");
                Console.WriteLine("3. Exit \n");
                Console.Write("Enter your choice: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    IRegistrationService registrationService = new Services.RegistrationService(connectionString);

                    switch (choice)
                    {
                        case 1:
                            RegisterUser(registrationService);
                            break;
                        case 2:
                            DisplayRegistration(registrationService);
                            break;
                        case 3:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }
            }
        }

        static void RegisterUser(IRegistrationService registrationService)
        {
            Console.Write("Enter your Firstname: ");
            string firstname = Console.ReadLine();

            Console.Write("Enter your Lastname: ");
            string lastname = Console.ReadLine();

            Console.Write("Enter your ID Number: ");
            string idNumber = Console.ReadLine();

            Console.Write("Enter your Email: ");
            string email = Console.ReadLine();
             
            Console.Write("Enter your Physical Address: ");
            string physicalAddress = Console.ReadLine();

            Console.Write("Enter your Postal Address: ");
            string postalAddress = Console.ReadLine();

            Console.Write("Enter your Telephone: ");
            string telephone = Console.ReadLine();

            Console.Write("Enter your Cell: ");
            string cell = Console.ReadLine();

            Console.Write("Enter your Work Contact: ");
            string work = Console.ReadLine();

            Model.Person person = new Model.Person
            {
                Firstname = firstname,
                Lastname = lastname,
                IDNumber = idNumber,
                Email = email,
                PhysicalAddress = new Model.Address { AddressLine = physicalAddress },
                PostalAddress = new Model.Address { AddressLine = postalAddress },
                ContactDetails = new Model.Contact { Telephone = telephone, Cell = cell, Work = work }
            };

            registrationService.RegisterUser(person);
        }

        static void DisplayRegistration(IRegistrationService registrationService)
        {
            registrationService.DisplayRegistration();
        }
    }
}
