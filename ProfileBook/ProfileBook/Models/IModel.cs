using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Models
{
    public interface IModel
    {
        /// <summary>
        /// Model id for database
        /// </summary>
        int Id { get; set; }
    }
}
