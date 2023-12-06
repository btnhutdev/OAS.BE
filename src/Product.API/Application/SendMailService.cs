using Core;
using Core.ViewModel;
using MailKit.Net.Smtp;
using MimeKit;
using Product.API.Interfaces;
using System.Globalization;
using System.Security.Authentication;
using CoreResource = Core.Properties;

namespace Product.API.Application
{
    public class SendMailService : ISendMailService
    {
        public readonly CultureInfo vietnameseCulture = new CultureInfo("vi-VN");

        #region Constructor
        private readonly EmailConfiguration _emailConfig;

        public SendMailService(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }
        #endregion

        #region SendMailSuccessAsync
        public async Task SendMailSuccessAsync(MailMessage message, MailModel detail)
        {
            var mailMessage = CreateSuccessMailMessage(message, detail);
            await SendAsync(mailMessage);
        }
        #endregion

        #region SendEmailFailedAsync
        public async Task SendMailFailedAsync(MailMessage message, MailModel detail)
        {
            var mailMessage = CreateFailedMailMessage(message, detail);
            await SendAsync(mailMessage);
        }
        #endregion

        #region SendMailApproveTask
        public async Task SendMailApproveTask(MailMessage message, MailModel detail)
        {
            var mailMessage = CreateSendMailApproveMessage(message, detail);
            await SendAsync(mailMessage);
        }
        #endregion

        #region SendMailRejectTask
        public async Task SendMailRejectTask(MailMessage message, MailModel detail, string reason)
        {
            var mailMessage = CreateSendMailRejectMessage(message, detail, reason);
            await SendAsync(mailMessage);
        }
        #endregion

        #region SendMailNotHighestPrice
        public async Task SendMailNotHighestPrice(MailMessage message, string productName, string categoryName, float currentPrice, float yourPrice)
        {
            var mailMessage = CreateMailNotHighestPrice(message, productName, categoryName, currentPrice, yourPrice);
            await SendAsync(mailMessage);
        }
        #endregion

        #region CreateMailNotHighestPrice
        private MimeMessage CreateMailNotHighestPrice(MailMessage message, string productName, string categoryName, float currentPrice, float yourPrice)
        {
            var emailMessage = new MimeMessage();
            var bodyBuilder = new BodyBuilder();

            emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            string formatedYourPrice = FormatPrice(Convert.ToDouble(yourPrice));
            string formatedCurrentPrice = FormatPrice(Convert.ToDouble(currentPrice));

            string htmlBody = string.Format(vietnameseCulture, CoreResource.EmailBodyResources.EmailPriceNotHighest, 
                productName, categoryName, formatedCurrentPrice, formatedYourPrice);

            bodyBuilder.HtmlBody = htmlBody;
            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }
        #endregion

        #region CreateSuccessMailMessage
        private MimeMessage CreateSuccessMailMessage(MailMessage message, MailModel detail)
        {
            var emailMessage = new MimeMessage();
            var bodyBuilder = new BodyBuilder();

            emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            string formatedPriceCurrentMax = FormatPrice(detail.PriceCurrentMax);
            string formatedCurrentPrice = FormatPrice(detail.CurrentPrice);
            string formatedStartDate = FormatDate(detail.StartDate);
            string formatedEndDate = FormatDate(detail.EndDate);

            string htmlBody = string.Format(vietnameseCulture, CoreResource.EmailBodyResources.EmailSuccessAuction,
                detail.ProductName, detail.CategoryName, detail.AuctionType, formatedPriceCurrentMax,
                formatedCurrentPrice, formatedStartDate, formatedEndDate);

            bodyBuilder.HtmlBody = htmlBody;
            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }
        #endregion

        #region CreateFailedMailMessage
        private MimeMessage CreateFailedMailMessage(MailMessage message, MailModel detail)
        {
            var emailMessage = new MimeMessage();
            var bodyBuilder = new BodyBuilder();

            emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            string formatedPriceCurrentMax = FormatPrice(detail.PriceCurrentMax);
            string formatedCurrentPrice = FormatPrice(detail.CurrentPrice);
            string formatedStartDate = FormatDate(detail.StartDate);
            string formatedEndDate = FormatDate(detail.EndDate);

            string htmlBody = string.Format(vietnameseCulture, CoreResource.EmailBodyResources.EmailFailedAuction,
                detail.ProductName, detail.CategoryName, detail.AuctionType, formatedPriceCurrentMax,
                formatedCurrentPrice, formatedStartDate, formatedEndDate);

            bodyBuilder.HtmlBody = htmlBody;
            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }
        #endregion

        #region CreateSendMailRejectMessage
        private MimeMessage CreateSendMailRejectMessage(MailMessage message, MailModel detail, string reason)
        {
            var emailMessage = new MimeMessage();   
            var bodyBuilder = new BodyBuilder();

            emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            string formatedInitPrice = FormatPrice(detail.Init_Price);
            string formatedStepPrice = FormatPrice(detail.Step_Price);
            string formatedStartDate = FormatDate(detail.StartDate);

            string htmlBody = string.Format(vietnameseCulture, CoreResource.EmailBodyResources.EmailRejectProduct,
                detail.ProductName, detail.CategoryName, formatedInitPrice, formatedStepPrice,
                formatedStartDate, reason);

            bodyBuilder.HtmlBody = htmlBody;
            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }
        #endregion

        #region CreateSendMailApproveMessage
        private MimeMessage CreateSendMailApproveMessage(MailMessage message, MailModel detail)
        {
            var emailMessage = new MimeMessage();
            var bodyBuilder = new BodyBuilder();

            emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            string formatedInitPrice = FormatPrice(detail.Init_Price);
            string formatedStepPrice = FormatPrice(detail.Step_Price);
            string formatedApproveDate = FormatDate(detail.StartDate);

            string htmlBody = string.Format(vietnameseCulture, CoreResource.EmailBodyResources.EmailApproveProduct,
                detail.ProductName, detail.CategoryName, formatedInitPrice, formatedStepPrice,
                formatedApproveDate);

            bodyBuilder.HtmlBody = htmlBody;
            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }
        #endregion

        #region SendAsync

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.CheckCertificateRevocation = false;
                    client.SslProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Tls13;
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
                    await client.SendAsync(mailMessage);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
        #endregion

        #region FormatOutput
        private string FormatDate(DateTime? date)
        {
            if (date != null)
            {
                return date?.ToString("d/M/yyyy H:mm");
            }
            else
            {
                return "Not set";
            }
        }

        private string FormatPrice(double? price)
        {
            if (price.HasValue)
            {
                return string.Format("{0:N0}", price);
            }
            else
            {
                return "Not set";
            }
        }

        #endregion
    }
}
