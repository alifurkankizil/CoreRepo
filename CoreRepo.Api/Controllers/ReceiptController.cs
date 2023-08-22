using AutoMapper;
using CoreRepo.Api.Models;
using CoreRepo.Core.Enums;
using CoreRepo.Data;
using CoreRepo.Data.Infrastructure;
using CoreRepo.Data.Models;
using CoreRepo.Data.Receipt;
using CoreRepo.Data.Specification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TanvirArjel.EFCore.GenericRepository;

namespace CoreRepo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiVersion("3.0")]
    public class ReceiptController : ControllerBase
    {
        #region Constructor
        public ReceiptController(IUnitOfWork unitOfWork
            , IQueryRepository queryRepository
            , IRepository repository
            , IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            QueryRepository = queryRepository;
            Repository = repository;
            Mapper = mapper;
        }

        private IUnitOfWork UnitOfWork { get; }
        private IQueryRepository QueryRepository { get; }
        private IRepository Repository { get; }
        private IMapper Mapper { get; }
        #endregion

        #region Add

        [MapToApiVersion("1.0")]
        [HttpPost]
        public async Task<IActionResult> AddV1(ReceiptModel model)
        {
            var entity = Mapper.Map<ReceiptModel,ReceiptEntity>(model);
            await UnitOfWork.Receipt.AddAsync(entity);
            await UnitOfWork.SaveChangesAsync();
            return Ok(entity.Id);
        }
        
        [MapToApiVersion("2.0")]
        [HttpPost]
        public async Task<IActionResult> AddV2(ReceiptModel model)
        {
            var entity = Mapper.Map<ReceiptModel,ReceiptEntity>(model);
            await Repository.AddAsync(entity);
            await Repository.SaveChangesAsync();
            return Ok(entity.Id);
        }
        
        #endregion

        #region Delete

        [MapToApiVersion("1.0")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteV1(Guid id)
        {
            var entity = await UnitOfWork.Receipt.GetById(id);
            
            if (entity is null)
                return NotFound();

            UnitOfWork.Receipt.Delete(entity);
            await UnitOfWork.SaveChangesAsync();
            
            return NoContent();
        }
        
        [MapToApiVersion("2.0")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteV2(Guid id)
        {
            var entity = await QueryRepository.GetByIdAsync<ReceiptEntity>(id);
            
            if (entity is null)
                return NotFound();
            
            Repository.Remove(entity);
            await Repository.SaveChangesAsync();

            return NoContent();
        }
        

        #endregion
        
        #region GetById
        [MapToApiVersion("1.0")]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdV1(Guid id)
        {
            return Ok(await UnitOfWork.Receipt.GetById(id));
        }
        
        [MapToApiVersion("1.0")]
        [HttpGet("{id:guid}/detail")]
        public async Task<IActionResult> GetByIdDetailV1(Guid id)
        {
            return Ok(await UnitOfWork.Receipt.GetByIdDetail(id));
        }

        [MapToApiVersion("2.0")]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdV2(Guid id)
        {
            var specification = new Specification<ReceiptEntity>();
            specification.Conditions.Add(x => x.Id == id);
            return Ok(await QueryRepository.GetAsync(specification));
            //return Ok(await QueryRepository.GetByIdAsync<ReceiptEntity>(id));
        }
        
        [MapToApiVersion("2.0")]
        [HttpGet("{id:guid}/detail")]
        public async Task<IActionResult> GetByIdDetailV2(Guid id)
        {
            var specification = new Specification<ReceiptEntity>();
            specification.Conditions.Add(x => x.Id == id);
            specification.Includes = o => o
                .Include(x => x.ReceiptLines)
                .ThenInclude(x => x.ReceiptLineTags);
            
            return Ok(await QueryRepository.GetAsync(specification));
        }

        [MapToApiVersion("3.0")]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdV3(Guid id)
        {
            var queryModel = new QueryModel<ReceiptEntity>
            {
                Includes = o => o
                    .Include(x => x.ReceiptLines)
                    .ThenInclude(x => x.ReceiptLineTags)
            };
            return Ok(await UnitOfWork.Receipt.GetByIdV3(id, queryModel));
        }

        #endregion

        #region Search

        [MapToApiVersion("1.0")]
        [HttpGet("search")]
        public async Task<IActionResult> SearchV1([FromQuery]ReceiptSearchModel searchModel)
        {
            return Ok(await UnitOfWork.Receipt.Search(searchModel));
        }
        
        [MapToApiVersion("2.0")]
        [HttpGet("search")]
        public async Task<IActionResult> SearchV2([FromQuery]ReceiptSearchModel searchModel)
        {
            var specification = ReceiptSpecification.SearchSpecification(searchModel);
            return Ok(await QueryRepository.GetListAsync(specification));
        }
        
        #endregion
        
        #region Detail Search

        [MapToApiVersion("1.0")]
        [HttpGet("detail-search")]
        public async Task<IActionResult> DetailSearchV1([FromQuery]ReceiptSearchModel searchModel)
        {
            return Ok(await UnitOfWork.Receipt.Search(searchModel));
        }
        
        [MapToApiVersion("2.0")]
        [HttpGet("detail-search")]
        public async Task<IActionResult> DetailSearchV2([FromQuery]ReceiptSearchModel searchModel)
        {
            var specification = ReceiptSpecification.SearchSpecification(searchModel);
            specification.Includes = o => o
                .Include(x => x.ReceiptLines)
                .ThenInclude(x => x.ReceiptLineTags);
            return Ok(await QueryRepository.GetListAsync(specification));
        }
        
        #endregion

        #region SalesReport

        [MapToApiVersion("1.0")]
        [HttpGet("balance/{id:guid}")]
        public async Task<IActionResult> TotalAmountByIdV1(Guid id)
        {
            return Ok(await UnitOfWork.Receipt.TotalAmountById(id));
        }
        
        [MapToApiVersion("2.0")]
        [HttpGet("balance/{id:guid}")]
        public async Task<IActionResult> TotalAmountByIdV2(Guid id)
        {
            var specification = new Specification<ReceiptEntity>();
            specification.Conditions.Add(x => x.Id == id);
            specification.Includes = x => x.Include(y => y.ReceiptLines);
            
            var query = QueryRepository
                .GetQueryable<ReceiptEntity>(specification);
            
            var totalAmount = await query
                .SelectMany(x => x.ReceiptLines)
                .SumAsync(x => x.AmountType == AmountType.Credit
                    ? x.Amount
                    : -x.Amount);
            
            return Ok(totalAmount);
        }

        #endregion
    }
}
