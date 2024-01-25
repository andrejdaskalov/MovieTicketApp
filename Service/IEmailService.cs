using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Service
{
    public interface IEmailService
    {
        Task SendEmailAsync(List<EmailMessage> allMails);
    }
}
