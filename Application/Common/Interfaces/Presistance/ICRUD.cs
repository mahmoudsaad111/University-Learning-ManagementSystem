using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Presistance
{
    public interface ICRUD<T> where T : class
    {
        public T Create(T entity);
        public bool Update(int? Id, T entity);
        public T GetById(int? Id);
        public IEnumerable<T> GetAll();
        public bool Delete(int? Id);

    }
}
