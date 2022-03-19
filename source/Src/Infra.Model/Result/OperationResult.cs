using System.ComponentModel;
using System.Runtime.Serialization;

namespace DotFramework.Infra.Model
{
    [DataContract]
    public class OperationResult
    {
        public OperationResult()
        {

        }

        public OperationResult(bool result)
        {
            Result = result;
        }

        public OperationResult(bool result, OperationResult innerResult)
            : this(result)
        {
            InnerResult = innerResult;
        }

        [DefaultValue(true)]
        [DataMember]
        public bool Result { get; set; }

        [DataMember]
        public OperationResult InnerResult { get; set; }
    }

    [DataContract]
    public class OperationResult<TModel> : OperationResult
    {
        public OperationResult(bool result, TModel replyModel)
            : base(result)
        {
            ReplyModel = replyModel;
        }

        public OperationResult(bool result, OperationResult innerResult, TModel replyModel)
            : base(result, innerResult)
        {
            ReplyModel = replyModel;
        }

        [DataMember]
        public TModel ReplyModel { get; set; }
    }
}
