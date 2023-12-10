using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veseta.Core.entites;
using Veseta.CoreCore.DTOs;

namespace Veseta.Core.Helper
{
    public class MappingProfiles :Profile
    {

        public MappingProfiles() 
        {
            CreateMap<Appointment, AppointmentToReturnDto>()
                .ForMember(P => P.DoctorName, O => O.MapFrom(s => s.Doctor.FirstName));
            CreateMap<CoreAPI.DTOs.UserDTO, Doctor>();
            CreateMap<CoreAPI.DTOs.UserDTO, Patient>();
            CreateMap<Doctor,DoctorsToReturnDto>();
            CreateMap<Patient,PatientsToReturnDto>();
            CreateMap<CoreAPI.DTOs.DiscountCodeCouponDTO, Discount>();
            CreateMap<DiscountToUpdateDto,Discount>();
        }
    }
}
