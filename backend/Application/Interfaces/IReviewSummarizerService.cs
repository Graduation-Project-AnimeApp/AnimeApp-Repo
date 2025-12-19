namespace AnimeFlixBackend.Application.Interfaces
{
    public interface IReviewSummarizerService
    {
        Task SummarizeReviewsAsync(int animeId);
    }
}