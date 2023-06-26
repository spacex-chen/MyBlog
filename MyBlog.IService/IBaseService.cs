﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.IService
{
    public interface IBaseService<TEntity> where TEntity : class,new()
    {
        Task<bool> CreateAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(dynamic[] id);
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> GetFindAsync(Expression<Func<TEntity, bool>> func);

        //查询全部数据
        Task<List<TEntity>> QueryAsync();

        //自定义查询
        Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func);

        //分页查询
        Task<List<TEntity>> QueryAsync(int page, int size, RefAsync<int> total);

        //自定义分页查询
        Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func, int page, int size, RefAsync<int> total);
    }
}
