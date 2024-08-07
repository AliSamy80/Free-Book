﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IReprository
{
    public interface IServicesReprositoryLog<T> where T : class
    {
        List<T> GetAll();
        T FindBy(Guid Id);
        bool Save(Guid Id, Guid UserId);
        bool Update(Guid Id, Guid UserId);
        bool Delete(Guid Id, Guid UserId);
        bool DeleteLog(Guid Id);
    }
}
