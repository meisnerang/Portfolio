﻿using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface IModelRepository
    {
        IEnumerable<ModelItem> GetAll();
        IEnumerable<Model> GetModelsByMake(int makeId);
        void Add(Model model);
    }
}
