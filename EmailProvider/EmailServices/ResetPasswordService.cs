﻿using Azure.Communication.Email;
using Azure;
using EmailProvider.Models.DataModels;
using EmailProvider.Interfaces;

namespace EmailProvider.EmailServices
{
    public class ResetPasswordService : IEmailService<IdentityEmailModel>
    {

        private readonly string _connectionString;

        public ResetPasswordService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task SendEmailAsync(IdentityEmailModel model)
        {
            var emailClient = new EmailClient(_connectionString);

            var emailMessage = new EmailMessage(
                senderAddress: "DoNotReply@beb6ca96-5cd4-43a4-a4bc-6723d17f26d9.azurecomm.net",
                recipientAddress: model.Receiver,
                content: new EmailContent("Reset Your Password.")
                {
                    Html = $"""
                            <html>
                                <body style="font-family: Arial, sans-serif; background-color: #f4f4f9; color: #333333; margin: 0; padding: 20px;">
                                    <div style="max-width: 600px; margin: 0 auto; background: #ffffff; border: 1px solid #dddddd; border-radius: 8px; padding: 20px; box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);">
                                        <div style="text-align: center; border-bottom: 1px solid #dddddd; padding-bottom: 10px;">
                                            <h1 style="color: #0073e6; font-size: 24px; margin: 0;">Reset Password</h1>
                                        </div>
                                        <div style="margin-top: 20px; line-height: 1.6;">
                                            <p>Hello,</p>
                                            <p>Thank you for your request. Please click the button below to reset your password:</p>
                                            <a href="{model!.PassCode}" style="display: inline-block; margin-top: 20px; padding: 10px 20px; background-color: #0073e6; color: white; text-decoration: none; border-radius: 5px; font-size: 16px;">Reset Password</a>
                                            <p style="margin-top: 20px;">If you did not request this, please ignore this email.</p>
                                        </div>
                                        <div style="text-align: center; margin-top: 20px; font-size: 12px; color: #777777;">
                                            <p>&copy; 2024 Rika Solutions. All rights reserved.</p>
                                        </div>
                                    </div>
                                </body>
                            </html>
                        """
                }
            );

            await emailClient.SendAsync(
                WaitUntil.Completed,
                emailMessage
            );
        }
    }
}
