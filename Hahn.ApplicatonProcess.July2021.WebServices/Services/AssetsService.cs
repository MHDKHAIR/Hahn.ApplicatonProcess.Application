using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.July2021.Domain.Interfaces;

namespace Hahn.ApplicatonProcess.July2021.WebServices.Services
{
    public interface IAssetsService
    {

    }
    public class AssetsService : BaseService, IAssetsService
    {
        public AssetsService(IApplicationUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
