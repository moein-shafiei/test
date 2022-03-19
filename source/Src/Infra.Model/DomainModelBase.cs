using System;
using System.Runtime.Serialization;

namespace DotFramework.Infra.Model 
{
    [DataContract]
    public class DomainModelBase : ModelBase
    {
        #region Properties

        private Exception _Exception;
        [DataMember]
        public Exception Exception
        {
            get
            {
                return _Exception;
            }
            set
            {
                SetProperty(ref _Exception, value);
            }
        }

        public DateTime _ModificationTime;
        [DataMember]
        public DateTime ModificationTime
        {
            get
            {
                return _ModificationTime;
            }
            set
            {
                SetProperty(ref _ModificationTime, value);
            }
        }

        private Byte _Status = 0;
        [DataMember]
        public Byte Status
        {
            get
            {
                return _Status;
            }
            set
            {
                SetProperty(ref _Status, value);
            }
        }

        private Int64 _RowVersion;
        [DataMember]
        public Int64 RowVersion
        {
            get
            {
                return _RowVersion;
            }
            set
            {
                SetProperty(ref _RowVersion, value);
            }
        }

        private Boolean _IsPopulated;
        [DataMember]
        public Boolean IsPopulated
        {
            get
            {
                return _IsPopulated;
            }
            set
            {
                SetProperty(ref _IsPopulated, value);
            }
        }

        private Boolean _HasValue;
        [DataMember]
        public Boolean HasValue
        {
            get
            {
                return _HasValue;
            }
            set
            {
                SetProperty(ref _HasValue, value);
            }
        }

        #endregion

        #region Virtual Methods



        #endregion

        #region Public Methods



        #endregion

        #region Overrided Methods



        #endregion
    }
}
