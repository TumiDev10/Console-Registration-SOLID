using Registration.Model;

namespace Registration.Services
{
    public interface IRegistrationService
    {
        void RegisterUser(Person person);
        void DisplayRegistration();
    }
}