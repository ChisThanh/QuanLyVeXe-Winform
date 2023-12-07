using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyVeXe.Models
{
    public static class Helper
    {
        public static Task<bool> SendEmail(string toAddress, string subject, string body)
        {
            string fromAddress = "chithanh18042003@gmail.com";
            string password = "lkhfwvzglbmbvrgs";

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromAddress, password),
                EnableSsl = true,
            };

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(fromAddress),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(toAddress);

            try
            {
                smtpClient.Send(mailMessage);
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }
        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string GetNextCode(string sw, string currentCode)
        {
            if (!currentCode.StartsWith(sw))
            {
                throw new ArgumentException("Invalid code format");
            }
            string lastNumberString = currentCode.Substring(2);
            if (int.TryParse(lastNumberString, out int lastNumber))
            {
                int nextNumber = lastNumber + 1;
                string nextNumberString = nextNumber.ToString("D3");
                string nextCode = sw + nextNumberString;
                return nextCode;
            }
            else
            {
                throw new ArgumentException("Invalid number in the code");
            }
        }
    }
}
