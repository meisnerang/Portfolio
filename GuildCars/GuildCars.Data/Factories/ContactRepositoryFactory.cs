using GuildCars.Data.Interfaces;
using GuildCars.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Factories
{
    public class ContactRepositoryFactory
    {
        public static IContactRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "QA":
                    return new ContactRepository();
                case "PROD":
                    throw new NotImplementedException();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value.");
            }
        }
    }
}
