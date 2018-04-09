﻿using ISPCore.Models.core.Cache.CheckLink;
using ISPCore.Models.RequestsFilter.Base;
using System;

namespace ISPCore.Models.core.Cache
{
    public class AntiBotCacheToGlobalConf
    {
        /// <summary>
        /// 
        /// </summary>
        public string DomainsToreCaptchaRegex { get; set; } = "^$";

        /// <summary>
        /// Оригинальные настройки
        /// </summary>
        public Models.Databases.json.AntiBot conf { get; set; }
    }
}
