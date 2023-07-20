using System;
using System.Data.SqlClient;
using Registration.Model;
using Registration.Handler;

namespace Registration.Services
{
    public class RegistrationService : IRegistrationService
    {
        private DbHandler dbHandler;

        public RegistrationService(string connectionString)
        {
            dbHandler = new DbHandler(connectionString);
        }

        public void RegisterUser(Person person)
        {
            using (SqlConnection connection = dbHandler.GetConnection())
            {
                string insertAddressQuery = @"INSERT INTO Address (AddressLine) VALUES (@AddressLine);
                                      SELECT SCOPE_IDENTITY();"; // Retrieve the newly inserted Address ID

                string insertContactQuery = @"INSERT INTO Contact (Telephone, Cell, Work) VALUES (@Telephone, @Cell, @Work);
                                      SELECT SCOPE_IDENTITY();"; // Retrieve the newly inserted Contact ID

                string insertPersonQuery = @"INSERT INTO Person (Firstname, Lastname, IDNumber, Email, PhysicalAddressID, PostalAddressID, ContactID) 
                                     VALUES (@Firstname, @Lastname, @IDNumber, @Email, @PhysicalAddressID, @PostalAddressID, @ContactID);
                                     SELECT SCOPE_IDENTITY();"; // Retrieve the newly inserted Person ID

                using (SqlCommand command = new SqlCommand(insertAddressQuery, connection))
                {
                    command.Parameters.AddWithValue("@AddressLine", person.PhysicalAddress.AddressLine);
                    connection.Open();
                    int physicalAddressId = Convert.ToInt32(command.ExecuteScalar());

                    command.Parameters.Clear();
                    command.CommandText = insertAddressQuery;
                    command.Parameters.AddWithValue("@AddressLine", person.PostalAddress.AddressLine);
                    int postalAddressId = Convert.ToInt32(command.ExecuteScalar());

                    command.Parameters.Clear();
                    command.CommandText = insertContactQuery;
                    command.Parameters.AddWithValue("@Telephone", person.ContactDetails.Telephone);
                    command.Parameters.AddWithValue("@Cell", person.ContactDetails.Cell);
                    command.Parameters.AddWithValue("@Work", person.ContactDetails.Work);
                    int contactId = Convert.ToInt32(command.ExecuteScalar());

                    command.Parameters.Clear();
                    command.CommandText = insertPersonQuery;
                    command.Parameters.AddWithValue("@Firstname", person.Firstname);
                    command.Parameters.AddWithValue("@Lastname", person.Lastname);
                    command.Parameters.AddWithValue("@IDNumber", person.IDNumber);
                    command.Parameters.AddWithValue("@Email", person.Email);
                    command.Parameters.AddWithValue("@PhysicalAddressID", physicalAddressId);
                    command.Parameters.AddWithValue("@PostalAddressID", postalAddressId);
                    command.Parameters.AddWithValue("@ContactID", contactId);

                    try
                    {
                        int newPersonId = Convert.ToInt32(command.ExecuteScalar());
                        Console.WriteLine($"Registration successful! Person ID: {newPersonId}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
        }


        public void DisplayRegistration()
        {
            using (SqlConnection connection = dbHandler.GetConnection())
            {
                string selectQuery = @"SELECT P.ID, P.Firstname, P.Lastname, P.IDNumber, P.Email, 
                                            PA.AddressLine AS PhysicalAddress, PO.AddressLine AS PostalAddress, 
                                            C.Telephone, C.Cell, C.Work
                                       FROM Person P
                                       INNER JOIN Address PA ON P.PhysicalAddressID = PA.ID
                                       INNER JOIN Address PO ON P.PostalAddressID = PO.ID
                                       INNER JOIN Contact C ON P.ContactID = C.ID";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                // Read and display the data
                                int id = reader.GetInt32(0);
                                string firstName = reader.GetString(1);
                                string lastName = reader.GetString(2);
                                string idNumber = reader.GetString(3);
                                string email = reader.GetString(4);
                                string physicalAddress = reader.GetString(5);
                                string postalAddress = reader.GetString(6);
                                string telephone = reader.GetString(7);
                                string cell = reader.GetString(8);
                                string work = reader.GetString(9);

                                Console.WriteLine($"ID:\t\t\t{id}\nFirst Name:\t\t{firstName}\nLast Name:\t\t{lastName}\nID Number:\t\t{idNumber}\nEmail:\t\t\t{email}\nPhysical Address:\t{physicalAddress}\nPostal Address:\t\t{postalAddress}\nTelephone No:\t\t{telephone}\nCell No:\t\t{cell}\nWork No:\t\t{work}\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No registrations found.");
                        }

                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
        }
    }
}
