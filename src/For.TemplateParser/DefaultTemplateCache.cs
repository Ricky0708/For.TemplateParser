﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace For.TemplateParser
{
    public interface ITemplateCache
    {
        bool IsExist(string key);
        object GetValue(string key);
        void Add(string key, object value);
        void Lock();
        void Unlock();
        void RemoveCache();
    }
    internal class DefaultTemplateCache : ITemplateCache
    {
        private readonly Dictionary<string, object> _dictionaryTemplates = new Dictionary<string, object>();

        /// <summary>
        /// check cache is exist
        /// </summary>
        /// <param name="cacheEnum">which cache</param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsExist(string key)
        {
            var result = false;
            result = _dictionaryTemplates.ContainsKey(key);
            return result;
        }

        /// <summary>
        /// get cache
        /// </summary>
        /// <param name="cacheEnum"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetValue(string key)
        {
            _dictionaryTemplates.TryGetValue(key, out object obj);
            return obj;
        }

        /// <summary>
        /// add to cache
        /// </summary>
        /// <param name="cacheEnum"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public void Add(string key, object value)
        {
            _dictionaryTemplates.Add(key, value);
        }

        /// <summary>
        /// lock cache, make thread save
        /// </summary>
        /// <param name="cacheEnum"></param>
        public void Lock()
        {
            Monitor.Enter(_dictionaryTemplates);
        }

        /// <summary>
        /// unlock cache
        /// </summary>
        public void Unlock()
        {
            Monitor.Exit(_dictionaryTemplates);
        }


        public void RemoveCache()
        {
            _dictionaryTemplates.Clear();
        }
    }
}
