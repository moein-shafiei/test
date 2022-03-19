using DotFramework.Core;

namespace DotFramework.Infra.ExceptionHandling
{
    public interface IExceptionMetadataService
    {
        IExceptionMetadata GetMetadata(ExceptionBase exception);
    }

    public class ExceptionMetadata : IExceptionMetadata
    {
        public ExceptionMetadata(string rfc, string title)
        {
            RFC = rfc;
            Title = title;
        }

        public string RFC { get; private set; }

        public string Title { get; private set; }
    }
}
