using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Student.DTO.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //CreateMap<StudentInfo, StudentInfoDTO>()
            //    .ForMember(d => d.DepartName, opt => opt.MapFrom(i => i.Depart.DepartName));
            //CreateMap<StudentInfoDTO, StudentInfo>();

            //CreateMap<Depart, DepartDTO>()
            //     .ForMember(d => d.GradeName, opt => opt.MapFrom(i => i.GradeDepart.DepartName));
            //CreateMap<DepartDTO, Depart>();

            //CreateMap<Account, AccountDTO>()
            //    .ForMember(d => d.PassWord, opt => opt.MapFrom(i => "密码保密"));
            //CreateMap<AccountDTO, Account>()
            //    .ForMember(d => d.PassWord, opt => opt.MapFrom(i => $"{i.UserName}_{i.PassWord}".ToMd5Hash()));

            //CreateMap<AuthInfo, AuthInfoDTO>();
            //CreateMap<AuthInfoDTO, AuthInfo>();

            //CreateMap<Config, ConfigDTO>();
            //CreateMap<ConfigDTO, Config>();
        }
    }
}
