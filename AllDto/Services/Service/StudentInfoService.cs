using AllDto.Common.Auth.Jwt;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AllDto;
using System;
using System.Linq;
using System.Threading.Tasks;
using yrjw.ORM.Chimp;
using yrjw.ORM.Chimp.Result;
using AllDto.Services;
using AllModel;
using AllModel.Enums;

namespace ZookeeperBrowser.Services.Service
{
    public class StudentInfoService: BaseService<StudentInfo, StudentInfoDTO, long>, IStudentInfoService, IDependency
    {
        private readonly Lazy<IRepository<Depart>> repDepart;

        public StudentInfoService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<StudentInfoService> logger, Lazy<ILoginInfo> loginInfo,
            Lazy<IRepository<StudentInfo>> _repository,
            Lazy<IRepository<Depart>> repDepart) : base(mapper, unitOfWork, logger, loginInfo, _repository)
        {
            this.repDepart = repDepart;
        }

        /// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="dept"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public async Task<IResultModel> GetPagedListAsync(int pageIndex, int pageSize, int dept, string search)
        {
            var data = _repository.Value.TableNoTracking.Where(p => p.Deleted == 0);
            if(dept > 0)
            {
                var deptlist = repDepart.Value.GetById(dept);
                if (deptlist != null)
                {
                    if(deptlist.DeptType== EnumDeptType.grade)
                    {
                        var ids = repDepart.Value.Table.Where(p => p.DeptType ==  EnumDeptType.classes && p.GradeId == dept).Select(s=>s.Id).ToList();
                        data = data.Where(p => ids.Contains(p.DepartId));
                    }
                    else
                    {
                        data = data.Where(p => p.DepartId == dept);
                    }
                }
            }
            if (!search.IsNull())
            {
                if (search.IsMobileNumber())
                {
                    data = data.Where(p => p.Phone == search);
                }
                else if (search.IsIdentityCard())
                {
                    data = data.Where(p => p.IdentityCard == search);
                }
                else if (search.IsEmail())
                {
                    data = data.Where(p => p.Email == search);
                }
                else if (search.IsNumeric())
                {
                    data = data.Where(p => p.Id == search.ToLong());
                }
                else
                {
                    data = data.Where(p => p.Name.Contains(search));
                }
            }
            var list = await data.OrderByDescending(k => k.Id).ProjectTo<StudentInfoDTO>(_mapper.Value.ConfigurationProvider).ToPagedListAsync(pageIndex, pageSize);
            return ResultModel.Success(list);
        }

        /// <summary>
        /// 重写父类Insert方法，处理业务逻辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override async Task<IResultModel> InsertAsync(StudentInfoDTO model)
        {
            //外键判断
            var dept = repDepart.Value.GetById(model.DepartId);
            if (dept == null || dept.DeptType != EnumDeptType.classes)
            {
                _logger.LogError($"error：DepartId {model.DepartId} does not exist or the EnumDeptType is not classes");
                return ResultModel.Failed("外键不存在，或部门必须指定班级", "DepartId");
            }
            //检查手机号是否唯一
            if (model.Phone.NotNull())
            {
                var isphone = await _repository.Value.TableNoTracking.AnyAsync(p => p.Phone == model.Phone);
                if (isphone)
                {

                    _logger.LogError($"error：Phone {model.Phone} It's not the only one");
                    return ResultModel.Failed("该手机号已被其他账号绑定使用", "Phone");
                }
            }
            //检查身份证号是否唯一
            if (model.IdentityCard.NotNull())
            {
                var isIdentityCard = await _repository.Value.TableNoTracking.AnyAsync(p => p.IdentityCard == model.IdentityCard);
                if (isIdentityCard)
                {
                    _logger.LogError($"error：IdentityCard {model.IdentityCard} It's not the only one");
                    return ResultModel.Failed("该身份证号已被其他账号绑定使用", "IdentityCard");
                }
            }
            //调用父类方法
            return await base.InsertAsync(model);
        }

        /// <summary>
        /// 重写父类Update方法，处理业务逻辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override async Task<IResultModel> UpdateAsync(StudentInfoDTO model)
        {
            //外键判断
            var dept = repDepart.Value.GetById(model.DepartId);
            if (dept == null || dept.DeptType !=EnumDeptType.classes)
            {
                _logger.LogError($"error：DepartId {model.DepartId} does not exist or the EnumDeptType is not classes");
                return ResultModel.Failed("外键不存在，或部门必须指定班级", "DepartId");
            }
            //调用父类方法
            return await base.UpdateAsync(model);
        }

    }
}
