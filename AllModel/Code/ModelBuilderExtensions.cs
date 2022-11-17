using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllModel.Code
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConfigEntity>().HasData(InitializationData.Initialization.ConfigEntity);
            modelBuilder.Entity<AccountEntity>().HasData(InitializationData.Initialization.AccountEntity);
            //modelBuilder.Entity<Depart>().HasData(InitializationData.Initialization.Depart);
            //modelBuilder.Entity<StudentInfo>().HasData(InitializationData.Initialization.StudentInfo);
        }
    }
}