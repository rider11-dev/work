using EmitMapper;
using EmitMapper.MappingConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Components.Mapper
{
    public class OOMapper
    {
        public static TTo Map<TFrom, TTo>(TFrom from, IMappingConfigurator mappingConfigurator = null)
        {
            //EmitMapper内部有缓存机制，故不需要再维护Mapper的缓存
            var mapper = mappingConfigurator == null ?
                ObjectMapperManager.DefaultInstance.GetMapper<TFrom, TTo>() :
                ObjectMapperManager.DefaultInstance.GetMapper<TFrom, TTo>(mappingConfigurator);
            return mapper.Map(from);
        }

        public static void Map<TFrom, TTo>(TFrom from, TTo to, IMappingConfigurator mappingConfigurator = null)
        {
            //EmitMapper内部有缓存机制，故不需要再维护Mapper的缓存
            var mapper = mappingConfigurator == null ?
                ObjectMapperManager.DefaultInstance.GetMapper<TFrom, TTo>() :
                ObjectMapperManager.DefaultInstance.GetMapper<TFrom, TTo>(mappingConfigurator);
            mapper.Map(from, to);
        }

        public static void Map(Type fromType, Type toType, object from, object to)
        {
            var mapper = ObjectMapperManager.DefaultInstance.GetMapperImpl(fromType, toType, new DefaultMapConfig());
            mapper.Map(from, to, null);
        }
    }
}
