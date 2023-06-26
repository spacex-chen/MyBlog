using MyBlog.IRepository;
using MyBlog.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Repository
{
    public class TypeInfoRepository : BaseRepository<TypeInfo>, ITypeInfoRepository
    {
        public TypeInfoRepository(ISqlSugarClient db) : base(db)
        {
        }
    }
}
