using yrjw.ORM.Chimp;
using System.Threading.Tasks;
using yrjw.ORM.Chimp.Result;
using AllDto;
using AllModel;

namespace AllDto.Services
{
    public interface IStudentInfoService: IBaseService<StudentInfo, StudentInfoDTO, long>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="dept"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        Task<IResultModel> GetPagedListAsync(int pageIndex, int pageSize, int dept, string search);
    }
}
