using Core;
using Core.ViewModel;
using MailKit.Net.Smtp;
using MimeKit;
using Payment.API.Interfaces;
using System.Globalization;
using System.Security.Authentication;
using CoreResource = Core.Properties;

namespace Payment.API.Application
{
    public class SendMailService : ISendMailService
    {
        public readonly CultureInfo vietnameseCulture = new CultureInfo("vi-VN");

        #region constructor
        private readonly EmailConfiguration _emailConfig;

        public SendMailService(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }
        #endregion

        #region SendEmailPaymentSuccessToAuctioneerAsync
        public async Task SendEmailPaymentSuccessToAuctioneerAsync(MailMessage message, MailModel detail)
        {
            var mailMessage = CreateEmailPaymentSuccessToAuctioneer(message, detail);

            await SendAsync(mailMessage);
        }
        #endregion

        #region SendEmailPaymentSuccessToBidderAsync
        public async Task SendEmailPaymentSuccessToBidderAsync(MailMessage message, PaymentViewModel detail, string productName)
        {
            var mailMessage = CreateEmailPaymentSuccessToBidder(message, detail, productName);

            await SendAsync(mailMessage);
        }
        #endregion

        #region CreateEmailPaymentSuccessToAuctioneer
        public MimeMessage CreateEmailPaymentSuccessToAuctioneer(MailMessage message, MailModel detail)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            var bodyBuilder = new BodyBuilder();

            string formatedCurrentPrice = formatPrice(detail.CurrentPrice);
            string formatedStartDate = formatDate(detail.StartDate);

            // Construct the HTML body using message properties
            string htmlBody = string.Format(vietnameseCulture, CoreResource.EmailBodyResources.EmailSuccessPaymentToAuctioneer,
                detail.ProductName, formatedCurrentPrice, formatedStartDate);

            bodyBuilder.HtmlBody = htmlBody;
            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }
        #endregion

        #region CreateEmailPaymentSuccessToBidder
        public MimeMessage CreateEmailPaymentSuccessToBidder(MailMessage message, PaymentViewModel detail, string productName)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            var bodyBuilder = new BodyBuilder();
            string formatedTotalPrice = formatPrice(detail.TotalPrice);
            string formatedDate = formatDate(DateTime.Now);
            string formateTelephone = formatPhoneNumber(detail.Telephone);

            // Construct the HTML body using message properties
            string htmlBody = string.Format(vietnameseCulture, CoreResource.EmailBodyResources.EmailPaymentSuccessToBidder,
                productName, detail.FirstName, detail.LastName, formatedDate, detail.ShipingAddress,
                formateTelephone, detail.OrderType, formatedTotalPrice, formatedTotalPrice, detail.OrderNotes);

            bodyBuilder.HtmlBody = htmlBody;
            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }
        #endregion

        #region SendAsync
        public async Task SendAsync(MimeMessage mailMessage)
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
                catch
                {
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
        #endregion

        #region format output
        public string formatDate(DateTime? date)
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

        public string formatPrice(double? price)
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

        public string formatPhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Length < 10)
            {
                return phoneNumber;
            }

            string formatted = $"0 {phoneNumber.Substring(1, 3)} {phoneNumber.Substring(4, 3)} {phoneNumber.Substring(7, 3)}";
            return formatted;
        }
        #endregion
    }
}
