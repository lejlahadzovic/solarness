using AutoMapper;
using DotNetEnv;
using FirebaseAdmin.Messaging;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.EntityFrameworkCore;
using Solarness.Model.SearchObjects;
using Solarness.Services.Database;
using Solarness.Services.Service;
using Solarness.Model;
using Solarness.Services.Interface;
using FirebaseAdmin.Auth;

namespace Solarness.Services.Services
{
    public class ObavijestiService : BaseService<Model.Notification, Database.Notification, BaseSearchObject>, INotificationService
    {
        public ObavijestiService(SolarnessDbContext context, IMapper mapper) : base(context, mapper)
        {
            InitializeFirebaseApp();
        }

        private void InitializeFirebaseApp()
        {
            Env.Load();

            if (FirebaseApp.DefaultInstance != null)
                return; // Firebase is already initialized


            // Check if Firebase App is already initialized
            if (FirebaseApp.DefaultInstance == null)
            {
                string FIREBASE_TYPE = "service_account";
                string projectId = "studentolgasi";
                string privateKeyId = "31569b6ab339c2abc3f3d9a0b6cbe7083584ebc8";
                string privateKey = "-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCl7PxMvNHTsSO+\nZXyKi6ZYNEAIc+gXHQs4BJb2+sQKmwJahFFDowajUwrB7Wneq3osdrQjU9oyvZUx\nHnoI7G0zJNu2sOais8LnU2FUEpwmXQVi2mb4ZyJYJGYuB5/EXoU7jlvhcSDS9uLF\n74GGVGehihThYlP/VA89wPkeXK+DdHhQHYfd3l1BoEt0TvD41dSbaonVkKtgFjGQ\nk9mza1gUftym3iMOv2rxE2NqgSemf2QH8UK+WrxE0QQ42fmFGCe7tr3Ghgv2mWUm\nRtcxh/9wiFYzNB4q9352Fz7Gx7HpBNcHZr/3bc+nZ6eRIzpBFphAuI0KAvX6gABJ\njhwnFZY9AgMBAAECggEAAcubS+3u0IZnmnx8Awq2BpAPsZh7dxjwnaLc+2djdFwc\n96sMtTHcOEPa1Rwz7eOy3SBxfdLv5ildyqH1YgerKA1w1JgCOwqYwQYQoS1e2TYu\nBxjRo2iUXjJnlLpx9jpii6tCBZWeO5D+vCpzVhIQ68DaIwi/Z+CLFiapaFklxYAU\nLZYRA4I7ZXnqrOO7LGtLEVvzEy/sqgYHoJ/tRyxG3y2+snbY9PeXv/CFpsTdwfF7\nfDBw5dzK6T2bJ7O0uMDM2yRAsKKfgQYaVYD1gSjoZaBU0fyuS3sTHb2GXCsqH/e6\ncZ3Pm4Wb4jaSEiZxXGO4MKH8QpFkgCKzZt6fAN1AoQKBgQDnqOSK/+vJ0sm+lJiz\nJi4UDxfonB5Rifz4/R8/64O6SKMJb1YUDBE4yvZMMqJH3sOaKcCpujD687MdpJTw\nuSwuhLNlymO3QTrPZH6GdQJZnMN4XJVWS1wcGo+OOCHsKFAFsMjHq/9XB31BZGmY\nZdrnnkXjI1mcES/w+46O8l/qrQKBgQC3XABBd2RNjbzWhXeOtme/AY8qesxs4rLC\ndf8Kai09tLzU3n3gKcFATXgIXzJ/sxqJcVztfIsNVQWEvK8wzmXg6R2e680mlWGi\n2jeFZIBXTbVakIg12KGs7iCIe9/rFAqMALu7jxg3CVJYXjRa25uug2Oia74EEQUi\n7bhkarnb0QKBgCbqHyIeZXxstUkXayNE+Z6S/oAroYgu9tjYpGQ3atRjGUgfdMU/\nVbFFnuQj6VDLYe5Grz+TsGCniWIkYISjhoF19zDVM7T39yoZhouNokxtYgSZANkT\nJbDu4UiRs1kZWH/sN34bLXRwJKFUfcyE56Xa14CVUps94+DGUSdAHc6tAoGATSQT\np3dHZ6ld7RKxvgTXmPBa/xAyuQyXEGD8L7GD3xYO66+/XXOzrg3plfwXDsqXO4PK\nCNAA4FcIv1NykSIEkp8Aqz+hS75FGXKdMS2bIR+8UKHF7IF7Y3m7jjAvgIMp/MTC\nNqJ5GY5+alcXx7HkyYLVhBu4EZXnh05j6TtNfkECgYEAg5mpyRhY8ZmqDEgQWgAh\njMAoKGNkzUu6bHdu1hCK3mZnm2SQjCPSKZoHk9qZq2h6RXfzNSFu7Zte4cScIvBZ\nFac5fqy8sfMrCC+XQ20CHlpF8AMQ2ZkXUw7J7pO5LQovUGN6ktxgsVPMoQWYRXA+\nCN9Ot6X92GFTVxR3RFSw7NE=\n-----END PRIVATE KEY-----\n";
                string clientEmail = "firebase-adminsdk-hc56m@studentolgasi.iam.gserviceaccount.com";
                string clientId = "116131696388133670273";
                string authUri = "https://accounts.google.com/o/oauth2/auth";
                string tokenUri = "https://oauth2.googleapis.com/token";
                string authProviderCertUrl = "https://www.googleapis.com/oauth2/v1/certs";
                string clientCertUrl = "https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-hc56m%40studentolgasi.iam.gserviceaccount.com";
                string FIREBASE_UNIVERSE_DOMAIN = "googleapis.com";
                string FIREBASE_TOKEN = "eNg_GlnYQzy5ir_QVLEWXQ:APA91bGWXlcbDzmzJS26BU95ZiIye5yUz0Qhz0Uo9E3Vv7QFQs3nlhG0YFRjNdYfchPN3Vo1KFEGAyWTR4edSE_v2Ym4gBbgHNhDXfEF49iAmSd6HLl0fyDBSDxG_R9zqR5z5w1eMZgv";

                var options = new AppOptions()
                {
                    Credential = GoogleCredential.FromJson($@"
                {{
                    ""type"": ""service_account"",
                    ""project_id"": ""{projectId}"",
                    ""private_key_id"": ""{privateKeyId}"",
                    ""private_key"": ""{privateKey}"",
                    ""client_email"": ""{clientEmail}"",
                    ""client_id"": ""{clientId}"",
                    ""auth_uri"": ""{authUri}"",
                    ""token_uri"": ""{tokenUri}"",
                    ""auth_provider_x509_cert_url"": ""{authProviderCertUrl}"",
                    ""client_x509_cert_url"": ""{clientCertUrl}""
                }}")
                };

                FirebaseApp.Create(options);
            }
        }

        private async Task<string> SendFirebaseNotification(string title, string message, string notificationType)
        {
            var notificationMessage = new Message()
            {
                Notification = new FirebaseAdmin.Messaging.Notification
                {
                    Title = title,
                    Body = message
                },
                Topic = "news",
                Data = new Dictionary<string, string>()
            {
                { "notificationType", notificationType }
            }
            };

            var messaging = FirebaseMessaging.DefaultInstance;
            try
            {
                var result = await messaging.SendAsync(notificationMessage);
                Console.WriteLine($"Successfully sent message: {result}");
                return result;
            }
            catch (FirebaseMessagingException ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
                Console.WriteLine($"Error details: {ex.ErrorCode}");
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine($"An unexpected error occurred: {e.Message}");
                return null;
            }
        }

        public async Task<string> SendNotificationProject(string title, string message, int id, string notificationType)
        {
            var obavijest = new Database.Notification
            {
                ProjectId = id,
                Title = title,
                Content = message,
                SendDate = DateTime.Now
            };

            _context.Notifications.Add(obavijest);
            await _context.SaveChangesAsync();

            return await SendFirebaseNotification(title, message, notificationType);
        }

    }
}