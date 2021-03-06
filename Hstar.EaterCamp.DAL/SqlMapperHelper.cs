﻿using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration;
using IBatisNet.DataMapper.SessionStore;

namespace Hstar.EaterCamp.DAL
{
    /// <summary>
    /// SqlMapper帮助类（密封类，不能实例化）
    /// </summary>
    public sealed class SqlMapperHelper
    {
        private static volatile ISqlMapper mapper;

        /// <summary>
        /// 初始化Ibatis的SqlMapper
        /// </summary>
        public static void InitMapper(string configFilePath = null)
        {
            var builder = new DomSqlMapBuilder();
            mapper = string.IsNullOrEmpty(configFilePath) ? builder.Configure() : builder.Configure(configFilePath);
            mapper.SessionStore = new HybridWebThreadSessionStore(mapper.Id);
        }

        /// <summary>
        /// SqlMapper实例
        /// </summary>
        public static ISqlMapper Instance
        {
            get
            {
                if (mapper != null) return mapper;
                lock (typeof(SqlMapper))
                {
                    if (mapper == null) // double-check
                    {
                        InitMapper();
                    }
                }
                return mapper;
            }
        }
    }
}