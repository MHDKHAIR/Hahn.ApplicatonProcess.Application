using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.July2021.Domain.Interfaces;

namespace Hahn.ApplicatonProcess.July2021.WebServices.Services
{
    public class BaseService
    {
        protected readonly IApplicationUnitOfWork unitOfWork;

        public BaseService(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
    }
}
