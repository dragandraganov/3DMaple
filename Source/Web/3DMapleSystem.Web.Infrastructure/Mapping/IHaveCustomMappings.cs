using AutoMapper;
using System;
using System.Linq;

namespace _3DMapleSystem.Web.Infrastructure.Mapping
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IConfiguration configuration);
    }
}
