using System.Linq;
using FluentValidation;
using Hahn.ApplicatonProcess.July2021.Domain.Dtos;
using Hahn.ApplicatonProcess.July2021.Domain.Interfaces;

namespace Hahn.ApplicatonProcess.July2021.Domain.Validators
{
    public class AddUpdateAssetValidator : AbstractValidator<CreateAssetDto>
    {
        private string baseUrl = "https://api.coincap.io/v2/assets";
        public AddUpdateAssetValidator(IHttpRequestRepository httpRequest)
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
            RuleFor(x => x.AssetId).NotEmpty().WithMessage("Asset is required.");
            RuleFor(x => x.Symbol).NotEmpty().WithMessage("Symbol is required.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");

            RuleFor(x => x).MustAsync(async (x, cancellation) =>
                    {
                        //baseUrl = $"{baseUrl}?search={x.Name.ToLower()}";
                        var response = await httpRequest.GetAsync<AssetResponseRootDto>(baseUrl, cancellation);

                        if (response is null || response.data?.Count is 0)
                            return false;

                        if (!response.data.Any(r => r.id == x.AssetId && r.name == x.Name && x.Symbol == x.Symbol))
                            return false;

                        return true;
                    })
                     .WithMessage("ID Must be unique");
        }

    }
}
