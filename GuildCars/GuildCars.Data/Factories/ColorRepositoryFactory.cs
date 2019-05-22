using GuildCars.Data.Interfaces;
using GuildCars.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Factories
{
    public class ColorRepositoryFactory
    {
        public static IColorRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "QA":
                    return new ColorRepository();
                case "PROD":
                    throw new NotImplementedException();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value.");
            }
        }
    }
}
