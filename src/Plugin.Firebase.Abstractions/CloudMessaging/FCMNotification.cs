﻿using System.Collections.Generic;
using System.Linq;

namespace Plugin.Firebase.Abstractions.CloudMessaging
{
    public sealed class FCMNotification
    {
        public static FCMNotification Empty()
        {
            return new FCMNotification();
        }

        private readonly string _body;
        private readonly string _title;
        
        public FCMNotification(
            string body = null, 
            string title = null, 
            IDictionary<string, string> data = null)
        {
            _body = body;
            _title = title;
            Data = data;
        }

        public override string ToString()
        {
            return $"[FCMNotification: Body={Body}, Title={Title}, Data={(Data == null ? "" : string.Join(", ", Data.Select(kvp => $"{kvp.Key}:{kvp.Value}")))}]";
        }

        public string Body => _body ?? Data?["body"];
        public string Title => _title ?? (Data != null && Data.Any() ? Data["title"] : "");
        public IDictionary<string, string> Data { get; }
    }
}