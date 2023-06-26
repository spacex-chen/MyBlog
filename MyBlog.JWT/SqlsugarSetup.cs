using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Repository;
using MyBlog.Service;
using SqlSugar;

namespace MyBlog.JWT
{
    public static class SqlsugarSetup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service">服务</param>
        /// <param name="configuration">配置文件</param>
        public static void AppSqlsugarSetup(this IServiceCollection service, IConfiguration configuration)
        {
            SqlSugarScope sqlSugar = new(
                new ConnectionConfig()
                {
                    DbType = SqlSugar.DbType.SqlServer,//数据库类型
                    ConnectionString = configuration["SqlConn"],//配置文件中的数据库链接key值
                    IsAutoCloseConnection = true,//是否自动关闭连接
                },
                db =>
                {

                    //单例参数配置，所有上下文生效
                    db.Aop.OnLogExecuting = (sql, pars) =>
                    {
                        //Console.WriteLine(sql);//输出sql
                    };
                    //技巧：拿到非ORM注入对象
                    //services.GetService<注入对象>();
                }
            );

            //这边是SqlSugarScope用AddSingleton
            service.AddSingleton<ISqlSugarClient>(sqlSugar);

            /*service.AddScoped<IBlogNewsRepository, BlogNewsRepository>();
            service.AddScoped<IBlogNewsService, BlogNewsService>();
            service.AddScoped<ITypeInfoRepository, TypeInfoRepository>();
            service.AddScoped<ITypeInfoService, TypeInfoService>();*/

            service.AddScoped<IWriterInfoRepository, WriterInfoRepository>();
            service.AddScoped<IWriterInfoService, WriterInfoService>();
        }
    }
}
