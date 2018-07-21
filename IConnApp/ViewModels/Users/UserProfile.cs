using AutoMapper;
using IConnApp.Models;

namespace IConnApp.ViewModels.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>();
        }
    }
}
