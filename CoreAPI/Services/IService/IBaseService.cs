using AllModel;
using AllModel.Code;
using AllModel.MyOrm.Result;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace CoreAPI.Services.IService
{
    /// <summary>
    /// 封装公共方法CURD操作给API直接调用，业务相关操作请在子类中重写基类方法
    /// </summary>
    /// <typeparam name="TEntity">数据库模型</typeparam>
    /// <typeparam name="TEntityDTO">DTO模型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public interface IBaseService<TEntity, TEntityDTO, TKey> where TEntityDTO : class where TEntity : EntityBase<TKey>, new() where TKey : struct
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        //IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<IResultModel> GetByIdAsync(TKey id);

        /// <summary>
        /// 所有数据
        /// </summary>
        /// <param name="isDescending">是否倒序排序</param>
        /// <returns></returns>
        Task<IResultModel> GetListAllAsync(bool isDescending = false);

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model">DTO视图模型</param>
        /// <returns></returns>
        Task<IResultModel> InsertAsync(TEntityDTO model);

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="model">DTO视图模型</param>
        /// <returns></returns>
        Task<IResultModel> UpdateAsync(TEntityDTO model);

        /// <summary>
        /// 修改数据-批量
        /// </summary>
        /// <param name="entitys">DTO视图模型</param>
        /// <returns></returns>
        Task<IResultModel> UpdateAsync(IEnumerable<TEntityDTO> models);

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<IResultModel> DeleteAsync(TKey id);

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="ids">多个主键</param>
        /// <returns></returns>
        Task<IResultModel> DeleteAsync(IList<TKey> ids);

        /// <summary>
        /// 数据库中移除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<IResultModel> RemoveAsync(TKey id);

        /// <summary>
        /// 数据库中移除
        /// </summary>
        /// <param name="ids">多个主键</param>
        /// <returns></returns>
        Task<IResultModel> RemoveAsync(IList<TKey> ids);
    }
}
