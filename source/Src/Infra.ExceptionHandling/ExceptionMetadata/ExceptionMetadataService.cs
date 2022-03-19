using DotFramework.Core;
using System;
using System.Collections.Generic;

namespace DotFramework.Infra.ExceptionHandling
{
    public class ExceptionMetadataService : IExceptionMetadataService
    {
        private const string _DefaultBaseUri = "dotframework.net";
        private readonly string _RFCBaseUri;

        private readonly Dictionary<Type, IExceptionMetadata> _MetadataDictionary = new Dictionary<Type, IExceptionMetadata>();
        private readonly object _PadLock = new object();

        public ExceptionMetadataService(string rfcBaseUri)
        {
            _RFCBaseUri = rfcBaseUri;
        }

        public IExceptionMetadata GetMetadata(ExceptionBase exception)
        {
            Type exceptionType = exception.GetType();

            if (!_MetadataDictionary.ContainsKey(exceptionType))
            {
                lock (_PadLock)
                {
                    if (!_MetadataDictionary.ContainsKey(exceptionType))
                    {
                        _MetadataDictionary.Add(exceptionType, new ExceptionMetadata(exception.RFC.Replace(_DefaultBaseUri, _RFCBaseUri), exception.Title));
                    }
                }
            }

            return _MetadataDictionary[exceptionType];
        }
    }
}
