using System.Threading.Tasks;
using Application.Data.DataObjects;
using Application.Data.ResponseData;

namespace Application.Data.RepositoryInterfaces
{
    public interface IApplicationRepository
    {
        Task Create(ApplicationDTO application);
        Task<ApplicationResponse> GetRequestResponse(string number);
    }
}