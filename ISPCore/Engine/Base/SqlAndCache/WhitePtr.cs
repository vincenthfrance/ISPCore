﻿using System;
using Microsoft.Extensions.Caching.Memory;
using ISPCore.Models.Databases;
using ISPCore.Models.Base;
using Trigger = ISPCore.Models.Triggers.Events.Base.SqlAndCache.WhitePtrIP;

namespace ISPCore.Engine.Base.SqlAndCache
{
    public static class WhitePtr
    {
        static IMemoryCache memoryCache = Service.Get<IMemoryCache>();

        #region IsWhiteIP
        /// <summary>
        /// Проверка IP-адреса
        /// </summary>
        /// <param name="IPv4Or6">IP-адрес</param>
        public static bool IsWhiteIP(string IPv4Or6)
        {
            return memoryCache.TryGetValue(KeyToMemoryCache.WhitePtrIP(IPv4Or6), out _);
        }
        #endregion

        #region Add
        /// <summary>
        /// Добавить IP-адрес
        /// </summary>
        /// <param name="IPv4Or6">IP-адрес</param>
        /// <param name="ptr">PTR-адрес</param>
        /// <param name="Expires">На какой период добавить IP в белый список</param>
        public static void Add(string IPv4Or6, string ptr, DateTime Expires)
        {
            if (IsWhiteIP(IPv4Or6))
                return;

            // 
            Trigger.OnAdd((IPv4Or6, ptr, Expires));

            // Добовляем IP в кеш
            memoryCache.Set(KeyToMemoryCache.WhitePtrIP(IPv4Or6), (byte)0, Expires);

            // Меняем режим работы с SQL
            SqlToMode.SetMode(SqlMode.Read);

            // Подключаемся к базе
            using (var coreDB = Service.Get<CoreDB>())
            {
                // Добовляем IP в базу
                coreDB.WhitePtrIPs.Add(new WhitePtrIP()
                {
                    IPv4Or6 = IPv4Or6,
                    PTR = ptr,
                    Expires = Expires
                });

                // Сохраняем базу 
                coreDB.SaveChanges();
            }

            // Меняем режим работы с SQL
            SqlToMode.SetMode(SqlMode.ReadOrWrite);
        }
        #endregion
    }
}
