using System.Linq;
using System.Threading.Tasks;
using Application.Data.DataObjects;
using Application.Data.RepositoryInterfaces;
using Application.Data.ResponseData;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Data.Repositories
{
    public class ApplicationRepository:IApplicationRepository
    {
        private readonly ApplicationContext _context;
        private IMapper _mapper;

        public ApplicationRepository(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task Create(ApplicationDTO application)
        {
            var entry = _context.Entry(application);
            if (entry.State == EntityState.Detached)
            {
                await _context.Application.AddAsync(application);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<ApplicationResponse> GetRequestResponse(string number)
        {
            var responseDto = await _context.Application
                .Include(p => p.Applicant)
                .Include(p => p.RequestedCredit)
                .Where(p => p.ApplicationNum == number).FirstOrDefaultAsync();
            
            var response = _mapper.Map<ApplicationResponse>(responseDto);
            return response;

        }
    }
}