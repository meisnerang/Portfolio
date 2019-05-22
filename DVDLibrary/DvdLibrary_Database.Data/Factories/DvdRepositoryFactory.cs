using DvdLibrary_Database.Data.ADO;
using DvdLibrary_Database.Data.EF;
using DvdLibrary_Database.Data.Interfaces;
using DvdLibrary_Database.Data.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdLibrary_Database.Data.Factories
{
    public class DvdRepositoryFactory
    {
        public static IDvdRepository GetRepository()
        {
            switch(Settings.GetRepositoryType())
            {
                case "Mock":
                    return new DvdRepositoryMock();
                case "ADO":
                    return new DvdRepositoryADO();
                case "EF":
                    return new DvdRepositoryEF();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value.");
            }
        }
    }
}
