using BulletJournalApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BulletJournal.Domain;

namespace BulletJournalApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public static string ConvertDateToString(DateTime date)
        {
            return date.ToString("dd-MM-yyyy");
        }

        public AutoMapperProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<RegisterUserModel, User>();
            CreateMap<UpdateUserModel, User>();
            CreateMap<Item, ItemModel>()
                .ForMember(x => x.Date, opt => opt.MapFrom(x => AutoMapperProfile.ConvertDateToString(x.Date)));
            CreateMap<ItemModel, Item>();
        }
    }

}
