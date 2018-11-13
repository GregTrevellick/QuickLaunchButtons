using VsixRatingChaser;
using VsixRatingChaser.Dtos;
using VsixRatingChaser.Enums;
using VsixRatingChaser.Interfaces;

namespace QuickLaunch.Rating
{
    public class ChaserGateway
    {
        public static ChaseOutcome Probe(IRatingDetailsDto ratingDetailsDto, ExtensionDetailsDto extensionDetailsDto)
        {
            var chaser = new Chaser();
            var chaseOutcome = chaser.Chase(ratingDetailsDto, extensionDetailsDto);
            return chaseOutcome;
        }
    }
}