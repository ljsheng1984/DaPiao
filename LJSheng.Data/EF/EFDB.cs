using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace LJSheng.Data
{
    public class EFDB : DbContext
    {
        public EFDB()
            : base("name=MSSQL")
        {
            //模型更改时重新创建数据库
            //Database.SetInitializer<Context>(new DropCreateDatabaseIfModelChanges<Context>());
            ////数据库不存在时重新创建数据库
            //Database.SetInitializer<Context>(new CreateDatabaseIfNotExists<Context>());
            ////每次启动应用程序时创建数据库
            //Database.SetInitializer<Context>(new DropCreateDatabaseAlways<Context>());
            ////从不创建数据库
            //Database.SetInitializer<Context>(null);
        }

        /// <summary>
        /// 禁止创建表的时候表名复数
        /// </summary>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<fl> fl { get; set; }
        public DbSet<sj> sj { get; set; }
        public DbSet<splb> splb { get; set; }
        public DbSet<hy> hy { get; set; }
    }
}
