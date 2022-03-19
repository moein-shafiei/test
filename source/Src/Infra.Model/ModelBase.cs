using System.Linq;
using System.Runtime.Serialization;
using DotFramework.Core;
using Newtonsoft.Json;

namespace DotFramework.Infra.Model
{
    [DataContract]
    public class ModelBase : BindableBase, IModelBase
    {
        #region Properties

        private dynamic _Key;
        public dynamic Key
        {
            get
            {
                if (_Key == null)
                {
                    try
                    {
                        _Key = GetType().GetProperties().First(p => p.GetCustomAttributes(typeof(IsKey), false).Count() != 0).GetValue(this, null);
                    }
                    catch
                    {

                    }
                }

                return _Key;
            }
        }

        private ObjectState _State = ObjectState.None;
        [DataMember]
        public ObjectState State
        {
            get
            {
                return _State;
            }
            set
            {
                SetProperty(ref _State, value);
            }
        }

        #endregion

        #region Virtual Methods

        public virtual T Clone<T>(bool deepClone = false) where T : ModelBase, new()
        {
            if (!deepClone)
            {
                return MemberwiseClone() as T;
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(this));
            }
        }

        #endregion

        #region Public Methods

        public ModelBase Clone(bool deepClone = false)
        {
            return Clone<ModelBase>(deepClone);
        }

        #endregion

        protected virtual T DeepClone<T>() where T : ModelBase, new()
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(this));
        }

        #region Overrided Methods

        public override int GetHashCode()
        {
            if (Key == null)
            {
                return base.GetHashCode();
            }
            else
            {
                return this.Key.GetHashCode();
            }
        }

        #endregion
    }
}
