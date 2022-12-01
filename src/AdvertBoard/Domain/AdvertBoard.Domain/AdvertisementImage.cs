namespace AdvertBoard.Domain
{
    public class AdvertisementImage
    {
        public Guid Id { get; set; }

        public Guid ImageId { get; set; }

        public Image Image { get; set; }
        
        public Guid AdvertisementId { get; set; }

        public Advertisement Advertisement { get; set; }
    }
}
