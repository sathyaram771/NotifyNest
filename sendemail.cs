﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Windows;

namespace WpfApp1
{
    internal class sendemail
    {
        string sendermail = "sathyaalh3@gmail.com";
        public void send(string toemail,string subject,string body)
        {
            try
            {
                var fromAddress = new MailAddress(sendermail, "Sathyaram");
                var toAddress = new MailAddress(toemail);
                const string fromPassword = "ciwrjdwacdmauego";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587, 
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {

                    smtp.Send(message);
                }

                MessageBox.Show("Email sent successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while sending the email: {ex.Message}");
            }
        }
    }
}
