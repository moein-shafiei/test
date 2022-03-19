using System.Runtime.Serialization;

namespace DotFramework.Infra.Model
{
    [DataContract]
    public enum ObjectState
    {
        [EnumMember]
        None,
        [EnumMember]
        Added,
        [EnumMember]
        Edited,
        [EnumMember]
        Removed,
        [EnumMember]
        Dirty
    }
}
