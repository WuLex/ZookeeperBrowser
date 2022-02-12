using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using yrjw.ORM.Chimp;

namespace ZookeeperBrowser.Model.Code
{
    /// <summary>
    /// 包含通用模型实体基类，主键int类型自增
    /// </summary>
    public class EntityBase: EntityBase<int> 
    {
    }

    /// <summary>
    /// 不包含软删除功能的实体基类，主键int类型自增
    /// </summary>
    public class EntityBaseNoDeleted : EntityBaseNoDeleted<int>
    {
    }

    /// <summary>
    /// 不包含软删除功能的实体基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    public class EntityBaseNoDeleted<TKey> : EntityBase<TKey> where TKey : struct
    {
        [NotMapped]
        public override int Deleted { get => base.Deleted; set => base.Deleted = value; }
    }

    /// <summary>
    /// 包含通用模型实体基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    public class EntityBase<TKey> : IEntity where TKey : struct
    {
        [Key]
        public TKey Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreatedTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual DateTime ModifiedTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 操作人名称
        /// </summary>
        public virtual string OperatorName { get; set; }
        /// <summary>
        /// 软删除
        /// </summary>
        [DefaultValue(0)]
        public virtual int Deleted { get; set; }
    }
}
