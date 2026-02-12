using AutoMapper;
using DebtTracker.Application.DTOs.Debt;
using DebtTracker.Application.DTOs.Notification;
using DebtTracker.Application.DTOs.Payment;
using DebtTracker.Application.DTOs.User;
using DebtTracker.Domain.Entities;

namespace DebtTracker.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        
        CreateMap<Debt, DebtDto>()
            .ForMember(dest => dest.DebtorName, opt => opt.MapFrom(src => src.Debtor.Name))
            .ForMember(dest => dest.CreditorName, opt => opt.MapFrom(src => src.Creditor.Name))
            .ForMember(dest => dest.AmountPaid, opt => opt.MapFrom(src => src.AmountPaid))
            .ForMember(dest => dest.AmountRemaining, opt => opt.MapFrom(src => src.AmountRemaining));
        
        CreateMap<CreateDebtDto, Debt>();
        CreateMap<UpdateDebtDto, Debt>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Payment, PaymentDto>();
        CreateMap<CreatePaymentDto, Payment>();

        CreateMap<Notification, NotificationDto>();
        CreateMap<CreateNotificationDto, Notification>();
    }
}