﻿using System.Linq.Expressions;

namespace DotnetCoding.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();        
        Task<T> GetById(int id);
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);        
    }
}