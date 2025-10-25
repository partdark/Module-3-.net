using Hello_world_API.Interfaces;

namespace Hello_world_API.Services
{
    public class TimeService : ITimeSevice
    {
       public string GetCurentTime()
        {
            return DateTime.Now.ToString("HH:mm:ss dd:MM:yyyy");
        }
    }
}
