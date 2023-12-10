using Microsoft.AspNetCore.Mvc;
using Veseta.Core.Helper;
using Veseta.Core.IRepository;
using Veseta.Core.IServices;
using Veseta.Core.UnitOfWork;
using Veseta.CoreRepository.Repository;
using Veseta.CoreRepository.UnitOfWork;
using Veseta.CoreService;
using VesetaAPI.Errors;



namespace VesetaAPI.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IAdminDashBoardService, AdminDashBoardService>();
            services.AddScoped<IAdminDashBoardRepository, AdminDashBoardRepository>();
            services.AddScoped<IPatientRepository,PatientRepository>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IDoctorRepository,DoctorRepository>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDiscountCouponService, DiscountCouponService>();
            services.AddAutoMapper(typeof(MappingProfiles));
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                         .SelectMany(P => P.Value.Errors)
                                                         .Select(E => E.ErrorMessage)
                                                         .ToArray();

                    var ValidationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(ValidationErrorResponse);
                };
            });
            return services;
        }
    }
}
