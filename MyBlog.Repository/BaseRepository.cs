using MyBlog.IRepository;
using SqlSugar;
using System.Linq.Expressions;

namespace MyBlog.Repository
{
    public class BaseRepository<TEntity> : SimpleClient<TEntity>, IBaseRepository<TEntity> where TEntity : class, new()
    {
        // public ISqlSugarClient Db { get; }
        public BaseRepository(ISqlSugarClient db) : base(db) { 
            // Db = db;
            // 创建数据库
            // Db.DbMaintenance.CreateDatabase();
            // 创建数据表
            // Db.CodeFirst.InitTables(typeof(BlogNews), typeof(TypeInfo), typeof(WriterInfo));
        }

        public async Task<bool> CreateAsync(TEntity entity)
        {
            return await base.InsertAsync(entity);
        }

        public async Task<bool> DeleteAsync(dynamic[] id)
        {
            return await base.DeleteByIdsAsync(id);
        }

        public override async Task<bool> UpdateAsync(TEntity entity)
        {
            return await base.UpdateAsync(entity);
        }

        //子类实现导航查询可以改写
        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public virtual async Task<List<TEntity>> QueryAsync()
        {
            return await base.GetListAsync();
        }

        public virtual async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func)
        {
            return await base.GetListAsync(func);
        }

        public virtual async Task<List<TEntity>> QueryAsync(int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<TEntity>().ToPageListAsync(page, size, total);
        }

        public virtual async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func, int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<TEntity>().Where(func).ToPageListAsync(page, size, total);
        }

        public async Task<TEntity> GetFindAsync(Expression<Func<TEntity, bool>> func)
        {
            return await base.GetSingleAsync(func);
        }
    }
}
