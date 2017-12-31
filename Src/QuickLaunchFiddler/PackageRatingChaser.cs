using VsixRatingChaser.Dtos;
using VsixRatingChaser.Interfaces;
using static QuickLaunch.Rating.ChaserGateway;

namespace QuickLaunch.Fiddler
{
    public class PackageRatingChaser
    {
        public void Hunt(IRatingDetailsDto ratingDetailsDto)
        {
            var extensionDetailsDto = new ExtensionDetailsDto
            {
                AuthorName = Vsix.Author,
                ExtensionName = Vsix.Name,
                MarketPlaceUrl = "https://marketplace.visualstudio.com/items?itemName=GregTrevellick.OpenFiddler"
            };

            Probe(ratingDetailsDto, extensionDetailsDto);
        }
    }
}