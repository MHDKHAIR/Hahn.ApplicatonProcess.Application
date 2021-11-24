namespace Hahn.ApplicatonProcess.July2021.Domain.Dtos
{
    public class CreateAssetDto
    {
        public int UserId { get; set; }
        public string AssetId { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
    }
}
