using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Demo.Core.Tables;
using Demo.Data.Contracts;
using Demo.Service.Contracts;
using Demo.ViewModel.Response;

namespace Demo.Service.Services
{
    public class CommonService : ICommonService
    {

        private IRepositoryWrapper repository;
        private readonly IMapper mapper;

        public CommonService(IRepositoryWrapper _repository, IMapper _mapper)
        {
            repository = _repository;
            mapper = _mapper;
        }



        public async Task<bool> SentEmail(string from, string Recipients, string RecipientsCC, string RecipientsBCC, string mailbody, string Subject, string ReplyTo = "")
        {
            try
            {
                //var settings = repository.Setting.GetAll().Where(a => a.SettingGroup == "Email").ToList();
                //var smtp = settings.Where(a => a.SettingKey == "smtpServer").Select(s => s.SettingValue).FirstOrDefault();
                //var smtpPort = settings.Where(a => a.SettingKey == "smtpPort").Select(s => s.SettingValue).FirstOrDefault();
                //var fromEmail = settings.Where(a => a.SettingKey == "fromEmail").Select(s => s.SettingValue).FirstOrDefault();
                //var fromDisplayName = settings.Where(a => a.SettingKey == "fromEmail").Select(s => s.SettingValue).FirstOrDefault();
                //var smtpUser = settings.Where(a => a.SettingKey == "smtpUser").Select(s => s.SettingValue).FirstOrDefault();
                //var smtpPassword = settings.Where(a => a.SettingKey == "smtpPassword").Select(s => s.SettingValue).FirstOrDefault();

                var smtp = "smtp.gmail.com";
                var smtpPort = "587";
                var fromEmail = "noreplyroutesandtours@gmail.com";
                var fromDisplayName = "Noreply-RoutesAndTours";
                var smtpUser = "noreplyroutesandtours@gmail.com";
                var smtpPassword = "tdytbboaprrsogxg";

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(smtp);

                mail.From = new MailAddress(fromEmail, fromDisplayName);
                mail.To.Add(Recipients);
                if (!string.IsNullOrEmpty(RecipientsCC))
                {
                    mail.CC.Add(RecipientsCC);
                }
                if (!string.IsNullOrEmpty(RecipientsBCC))
                {
                    mail.Bcc.Add(RecipientsBCC);
                }
                if (!string.IsNullOrEmpty(ReplyTo))
                {
                    mail.ReplyToList.Add(ReplyTo);
                }

                mail.Subject = Subject;
                mail.Body = mailbody;
                mail.IsBodyHtml = true;

                SmtpServer.Port = Convert.ToInt32(smtpPort);
                SmtpServer.Credentials = new System.Net.NetworkCredential(smtpUser, smtpPassword);
                SmtpServer.EnableSsl = true;
                //if (Convert.ToBoolean(ConfigurationManager.AppSettings["sendemail"]) == true)
                //{
                SmtpServer.Send(mail);
                //}
                return true;
            }
            catch (System.Exception ex)
            {
            }
            return false;
        }

        public Task<Response<List<Setting>>> GetDataAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Response<Setting>> GetDatabyIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<Setting>>> GetDataWithPaginationAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> CreateDataAsync(Setting inputData, int LoggedInUserId = 0)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> UpdateDataAsync(Setting updateData, int LoggedInUserId = 0)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> DeleteDataAsync(object id, int LoggedInUserId = 0)
        {
            throw new NotImplementedException();
        }

        public void DetachEntity(Setting entityToDetach)
        {
            throw new NotImplementedException();
        }
    }
}
