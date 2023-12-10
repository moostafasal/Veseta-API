using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veseta.Core.entites;
using Veseta.Core.IServices;
using Veseta.Core.UnitOfWork;
using Veseta.CoreAPI.DTOs;
using Veseta.CoreCore.DTOs;

namespace Veseta.CoreService
{
    public class DiscountCouponService : IDiscountCouponService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DiscountCouponService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddNewCoupon(CoreAPI.DTOs.DiscountCodeCouponDTO model)
        {

            await _unitOfWork.Repository<Discount>().Add(_mapper.Map<DiscountCodeCouponDTO,Discount>(model));
            return await _unitOfWork.Complete();
        }

        public async Task<int> DeActivateCoupon(int id)
        {
            var coupon = await _unitOfWork.Repository<Discount>().GetByIdAsync(id);
            if (coupon == null)
            {
                return 0;
            }
            coupon.IsActive = false;
            _unitOfWork.Repository<Discount>().Update(coupon);
            return await _unitOfWork.Complete();
        }

        public async Task<int> DeleteCoupon(int id)
        {
            var entity = await _unitOfWork.Repository<Discount>().GetByIdAsync(id);
            _unitOfWork.Repository<Discount>().Delete(entity);
            return await _unitOfWork.Complete();
        }

        public async Task<IReadOnlyList<Discount>> GetAllCoupons()
        {
            return await _unitOfWork.Repository<Discount>().GetAllAsync();
        }

        public async Task<int> UpdateCoupon(DiscountToUpdateDto model)
        {
            var discount = await _unitOfWork.Repository<Discount>().GetByIdAsync(model.Id);
            if (discount == null)
                return 0;
            _unitOfWork.Repository<Discount>().Update(_mapper.Map<DiscountToUpdateDto,Discount>(model));
            return await _unitOfWork.Complete();
        }

    }
}
