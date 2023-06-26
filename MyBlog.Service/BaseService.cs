using MyBlog.IRepository;
using MyBlog.IService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        //从子类的构造函数中传入
        protected IBaseRepository<TEntity>? _iBaseRepository;

        public async Task<bool> CreateAsync(TEntity entity)
        {
            return await _iBaseRepository.CreateAsync(entity);
        }

        public async Task<bool> DeleteAsync(dynamic[] id)
        {
            return await _iBaseRepository.DeleteAsync(id);
        }

        //
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _iBaseRepository.GetByIdAsync(id);
        }

        public async Task<TEntity> GetFindAsync(Expression<Func<TEntity, bool>> func)
        {
            return await _iBaseRepository.GetFindAsync(func);
        }

        public async Task<List<TEntity>> QueryAsync()
        {
            return await _iBaseRepository.QueryAsync();
        }

        public async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func)
        {
            return await _iBaseRepository.QueryAsync(func);
        }

        public async Task<List<TEntity>> QueryAsync(int page, int size, RefAsync<int> total)
        {
            return await _iBaseRepository.QueryAsync(page, size, total);
        }

        public async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func, int page, int size, RefAsync<int> total)
        {
            return await _iBaseRepository.QueryAsync(func, page, size, total);
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            return await _iBaseRepository.UpdateAsync(entity);
        }
    }
}
