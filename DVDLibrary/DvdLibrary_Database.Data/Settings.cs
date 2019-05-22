using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdLibrary_Database.Data
{
    public class Settings
    {
        private static string _repositoryType;

        public static string GetRepositoryType()
        {
            if (string.IsNullOrEmpty(_repositoryType))
                _repositoryType = ConfigurationManager.AppSettings["RepositoryType"].ToString();

            return _repositoryType;
        }
    }
}
