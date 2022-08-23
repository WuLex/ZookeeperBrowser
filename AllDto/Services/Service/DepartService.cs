using AllDto.Common.Auth.Jwt;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AllDto;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using yrjw.ORM.Chimp;
using yrjw.ORM.Chimp.Result;
using AllDto.Services;
using AllModel;
using AllModel.Enums;

namespace ZookeeperBrowser.Services.Service
{
    public class DepartService : BaseService<Depart, DepartDTO, int>, IDepartService, IDependency
    {
        private readonly Lazy<IRepository<StudentInfo>> repStudentInfo;

        public DepartService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<DepartService> logger, Lazy<ILoginInfo> loginInfo,
            Lazy<IRepository<Depart>> _repository, Lazy<IRepository<StudentInfo>> repStudentInfo) : base(mapper, unitOfWork, logger, loginInfo, _repository)
        {
            this.repStudentInfo = repStudentInfo;
        }

        public async Task<IResultModel> GetPagedListAsync(int pageIndex, int pageSize, string search)
        {
            var data = _repository.Value.TableNoTracking;
            if (!search.IsNull())
            {
                data = data.Where(p => p.DepartName.Contains(search));
            }
            var list = await data.ProjectTo<DepartDTO>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }

        public async Task<IResultModel> GetClassesListAsync()
        {
            var data = _repository.Value.TableNoTracking.Where(p => p.DeptType == EnumDeptType.classes);
            var list = await data.ProjectTo<DepartDTO>(_mapper.Value.ConfigurationProvider).ToListAsync();
            return ResultModel.Success(list);
        }

        public override async Task<IResultModel> InsertAsync(DepartDTO model)
        {
            //外键判断
            if (model.GradeId.HasValue)
            {
                var dept = _repository.Value.GetById(model.GradeId);
                if (dept == null || dept.DeptType != EnumDeptType.grade)
                {
                    _logger.LogError($"error：GradeId {model.GradeId} does not exist or the EnumDeptType is not grade");
                    return ResultModel.Failed("外键不存在，或上级部门必须指定年组", "GradeId");
                }
            }
            //调用父类方法
            return await base.InsertAsync(model);
        }

        public override async Task<IResultModel> UpdateAsync(DepartDTO model)
        {
            //外键判断
            if (model.GradeId.HasValue)
            {
                var dept = _repository.Value.GetById(model.GradeId);
                if (dept == null || dept.DeptType != EnumDeptType.grade)
                {
                    _logger.LogError($"error：GradeId {model.GradeId} does not exist or the EnumDeptType is not grade");
                    return ResultModel.Failed("外键不存在，或上级部门必须指定年组", "GradeId");
                }
            }
            //调用父类方法
            return await base.UpdateAsync(model);
        }

        public override async Task<IResultModel> RemoveAsync(int id)
        {
            //级联删除，这里判断部门存在学生禁止删除
            var count = await repStudentInfo.Value.TableNoTracking.CountAsync(p => p.DepartId == id);
            if (count > 0)
                return ResultModel.Failed("部门已分配学生，无法删除");
            return await base.RemoveAsync(id);
        }

        public override async Task<IResultModel> RemoveAsync(IList<int> ids)
        {
            //这里判断部门存在学生禁止删除
            foreach (var id in ids)
            {
                var count = await repStudentInfo.Value.TableNoTracking.CountAsync(p => p.DepartId == id);
                if (count > 0)
                {
                    return ResultModel.Failed($"部门{id}已分配学生，无法删除");
                }
            }
            return await base.RemoveAsync(ids);
        }
    }
}
