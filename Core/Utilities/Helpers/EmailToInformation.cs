using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers
{
    //Gönderilecek bilgilendirme mailinin şablonu oluşturulur
    public class EmailToInformation
    {
        private string smtpServer = "smtp.office365.com"; //gönderim yapacak hizmetin smtp adresi
        private int smtpPort = 587;
        private string username = "yasnesra@outlook.com";
        private string password = "Esra1030515786";
        private string senderEmail = "yasnesra@outlook.com";

        // e-posta gönderimi için gerekli işlemleri gerçekleştirir
        public void SendEmail(string recipientEmail)
        {
            // E-posta gönderimi için SMTP istemci oluşturma
            //SMTP/Gönderici bilgilerinin yer aldığı erişim/doğrulama bilgileri
            SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(username, password);

           
            string template =
               @$"<html>
                <body>
                
                    Merhaba {recipientEmail}  <br
                    <h2>Probleminiz çözülmüştür.Sizlerin memnuniyeti bizler için önemli.Herhangi bir sorun olduğunda bizimle iletişime geçebilirsiniz.İyi günler dileriz</h2>
                        <hr/>              
                </body>
                </html>";

            // E-posta oluşturma (mesajı oluşturma)
            MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail);
            mailMessage.Subject = $"Görüşme Bilgilendirmesi";
            mailMessage.Body = template;
            mailMessage.IsBodyHtml = true;

            try
            {
                // E-postayı gönderme
                smtpClient.Send(mailMessage);
                Console.WriteLine("E-posta gönderildi.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("E-posta gönderilirken bir hata oluştu: " + ex.Message);
            }
            finally
            {
                // Kaynakları serbest bırakma
                mailMessage.Dispose();
                smtpClient.Dispose();
            }
        }
    }
}
