using System.Threading.Tasks;

namespace Service
{
    public interface IBackgroundEmailSender
    {
        Task DoWork();
    }
}
