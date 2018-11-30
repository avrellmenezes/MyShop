﻿using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class InMemoryRepository<T> : IRepository<T> where T: BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;


        public InMemoryRepository()
        {
            className = typeof(T).Name;
            items = cache[className] as List<T>;
            if (items == null)
            {
                items = new List<T>();
            }
        }

        public void Commit()
        {
            cache[className] = items;
        }

        public void Insert(T t)
        {
            items.Add(t);
        }

        public void Update(T t)
        {
            T tUpd = items.Find(p => p.Id == p.Id);
            if (tUpd != null)
            {
                tUpd = t;
            }
            else
            {
                throw new Exception(className.ToString() + " not found");
            }
        }

        public T Find(string Id)
        {
            T tFind = items.Find(p => p.Id == p.Id);
            if (tFind != null)
            {
                return tFind;
            }
            else
            {
                throw new Exception(className.ToString() + " not found");
            }
        }

        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

        public void Delete(string Id)
        {
            T tDelete = items.Find(p => p.Id == p.Id);
            if (tDelete != null)
            {
                items.Remove(tDelete);
            }
            else
            {
                throw new Exception(className.ToString() + " not found");
            }
        }
    }
}