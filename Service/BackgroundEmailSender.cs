﻿using System.Linq;
using System.Threading.Tasks;
using Domain;
using Repository;

namespace Service
{
    public class BackgroundEmailSender : IBackgroundEmailSender
    {

        private readonly IEmailService _emailService;
        private readonly IRepository<EmailMessage> _mailRepository;

        public BackgroundEmailSender(IEmailService emailService, IRepository<EmailMessage> mailRepository)
        {
            _emailService = emailService;
            _mailRepository = mailRepository;
        }
        public async Task DoWork()
        {
            await _emailService.SendEmailAsync(_mailRepository.GetAll().Where(z => !z.Status).ToList());
            _mailRepository.GetAll().Where(z => !z.Status).ToList().ForEach(z =>
            {
                z.Status = true;
                _mailRepository.Update(z);
            });
        }
    }
}
