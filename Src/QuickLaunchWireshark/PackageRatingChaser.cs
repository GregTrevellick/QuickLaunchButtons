using QuickLaunch.Rating;
using VsixRatingChaser.Dtos;
using VsixRatingChaser.Interfaces;

namespace QuickLaunch.Wireshark
{
    public class PackageRatingChaser
    {
        public void Hunt(IRatingDetailsDto ratingDetailsDto)
        {
            var extensionDetailsDto = new ExtensionDetailsDto
            {
                AuthorName = Vsix.Author,
                ExtensionName = Vsix.Name,
                MarketPlaceUrl = "https://marketplace.visualstudio.com/items?itemName=GregTrevellick.OpenWireshark"
                
            };

            ChaserGateway.Probe(ratingDetailsDto, extensionDetailsDto);
        }
    }
}