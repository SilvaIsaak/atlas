namespace CryptoAIPlatform.Domain.Research;

public interface IResearchEngineService
{
    Task<ResearchResult> ExecuteStudyAsync(ResearchStudy study, CancellationToken cancellationToken = default);
}
