﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using For.TemplateParser.Caches;
using For.TemplateParser.Models;

namespace For.TemplateParser
{
    public class TemplateParser
    {
        private readonly Core _core;
        public TemplateParser(TemplateParserConfig config = null)
        {
            if (config == null) config = new TemplateParserConfig();
            _core = new Core(config);
        }

        public TemplateParser(ITemplateCacheProvider cache, TemplateParserConfig config = null)
        {
            if (config == null) config = new TemplateParserConfig();
            _core = new Core(cache, config);
        }

        /// <summary>
        /// 組合範本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">inatance</param>
        /// <param name="cacheKey">default is typeof(T).FullName</param>
        /// <returns>template result</returns>
        public string BuildTemplate<T>(T obj, string cacheKey = null)
        {
            var delg = _core.GetTemplateDelegate(cacheKey ?? typeof(T).FullName);
            if (delg is null)
            {
                throw new Exception($"can't find any registed template by {cacheKey}");
            }
            return delg.Invoke(obj) as string;
        }

        /// <summary>
        /// Register template,cache and get the key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="template">inatance</param>
        /// <param name="cacheKey">default is typeof(T).FullName</param>
        /// <returns>cache key</returns>
        public string RegisterTemplate<T>(string template, string cacheKey = null)
        {
            return RegisterTemplate(typeof(T), template, cacheKey);
        }

        /// <summary>
        /// Register template, cache and get the key
        /// </summary>
        /// <param name="type"></param>
        /// <param name="template">inatance</param>
        /// <param name="cacheKey">default is typeof(T).FullName</param>
        /// <returns>cache key</returns>
        public string RegisterTemplate(Type type, string template, string cacheKey = null)
        {
            var key = cacheKey ?? type.FullName;
            _core.RegisterTemplate(type, template, key);
            return key;
        }
    }
}
