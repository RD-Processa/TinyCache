﻿using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace TinyCacheLib.FileStorage
{
    public class FileStorage : ICacheStorage
    {
        private string cacheFolder;

        public void Initialize(string cacheFolder)
        {
            this.cacheFolder = cacheFolder;
        }

        public object Get(string key, Type t)
        {
            var path = GetPath(key);

            try
            {
                if (File.Exists(path))
                {
                    var json = File.ReadAllText(path);

                    var result = JsonConvert.DeserializeObject(json, t);

                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public void Remove(string key)
        {
            var path = GetPath(key);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static object lockObj = 1;

        public bool Store(string key, object value, bool checkChange = true)
        {
            var hasChanged = true;

            var path = GetPath(key);
            string json = null;
            lock (lockObj)
            {
                json = JsonConvert.SerializeObject(value);
            }

            if (File.Exists(path))
            {
                try
                {
                    var content = File.ReadAllText(path);

                    if (content == json)
                    {
                        hasChanged = false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            if (hasChanged)
            {
                try
                {
                    File.WriteAllText(path, json, Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            return hasChanged;
        }

        private string GetPath(string key)
        {
            if (string.IsNullOrWhiteSpace((cacheFolder)))
            {
                throw new Exception("Initialize has to be called before using TinyCache with file storage");
            }

            var encoded = WebUtility.UrlEncode(key);

            var name = string.Format("TinyCache_{0}.cache", encoded);

            return Path.Combine(cacheFolder, name);
        }
    }
}
