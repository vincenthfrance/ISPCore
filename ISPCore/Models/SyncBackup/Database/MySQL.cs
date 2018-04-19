﻿using ISPCore.Engine.Databases;
using ISPCore.Models.Databases.Interface;

namespace ISPCore.Models.SyncBackup.Database
{
    public class MySQL : IUpdate
    {
        public int Id { get; set; }
        public int TaskId { get; set; }

        private int _port;
        private string _host;

        /// <summary>
        /// Порт
        /// </summary>
        public int Port
        {
            get { return 0 >= _port ? 3306 : _port; }
            set { _port = value; }
        }

        /// <summary>
        /// Хост
        /// </summary>
        public string Host
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_host))
                    return "localhost";

                return _host;
            }
            set { _host = value; }
        }

        /// <summary>
        /// Пользователь
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Обновить поля
        /// </summary>
        /// <param name="item">Новые данные</param>
        void IUpdate.Update(dynamic item) => CommonModels.Update(this, item);
    }
}
