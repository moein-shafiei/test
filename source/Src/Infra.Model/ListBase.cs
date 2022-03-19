using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace DotFramework.Infra.Model
{
    [DataContract]
    public class ListBase<TKey, TValue> : List<TValue>, IOrderedEnumerable<TValue> where TValue : ModelBase
    {
        #region Properties

        [DefaultValue(false)]
        //[DataMember]
        public bool UpdateOnExistence { get; set; }

        [DefaultValue(false)]
        [DataMember]
        public bool IsChached { get; set; }

        private int _TotalCount;
        [DataMember]
        public int TotalCount
        {
            get
            {
                if (_TotalCount == 0)
                {
                    return Count;
                }
                else
                {
                    return _TotalCount;
                }
            }
            set
            {
                _TotalCount = value;
            }
        }

        #endregion

        #region Variables

        [DataMember]
        protected bool IsRefreshed = true;

        [DataMember]
        protected Dictionary<Int32, TValue> MainDictionary;

        private static readonly object padlock = new object();

        #endregion

        #region Constructors

        public ListBase()
        {
            UpdateOnExistence = false;
            MainDictionary = new Dictionary<Int32, TValue>();
        }

        public ListBase(IEnumerable<TValue> list) : this()
        {
            Append(list);
        }

        #endregion

        #region Indexers

        public new TValue this[Int32 index]
        {
            get
            {
                return base[index];
            }
        }

        #endregion

        #region public Methods

        public new void Add(TValue obj)
        {
            lock (padlock)
            {
                TKey key = obj.Key;

                if (key != null)
                {
                    if (!key.Equals(default(TKey)))
                    {
                        if (!MainDictionary.ContainsKey(key.GetHashCode()))
                        {
                            base.Add(obj);
                            MainDictionary.Add(key.GetHashCode(), obj);
                        }
                        else if (UpdateOnExistence)
                        {
                            Set(key, obj);
                        }
                    }
                    else
                    {
                        base.Add(obj);

                        Int32 hashedID = Guid.NewGuid().GetHashCode();

                        while (MainDictionary.ContainsKey(hashedID))
                        {
                            hashedID = Guid.NewGuid().GetHashCode();
                        }

                        MainDictionary.Add(hashedID, obj);
                    }
                }
            }
        }

        public virtual void Append(IEnumerable<TValue> list)
        {
            foreach (TValue obj in list)
            {
                Add(obj);
            }
        }

        public virtual bool Remove(TKey ID)
        {
            lock (padlock)
            {
                if (Remove(Get(ID)))
                {
                    MainDictionary.Remove(ID.GetHashCode());
                    IsRefreshed = true;
                    return true;
                }

                return false;
            }
        }

        public virtual bool Contain(TKey ID)
        {
            lock (padlock)
            {
                return MainDictionary.ContainsKey(ID.GetHashCode());
            }
        }

        public virtual new void Clear()
        {
            base.Clear();
            MainDictionary.Clear();
            IsRefreshed = true;
        }

        public virtual List<TValue> GetAllWithPaging(int PageNumber, Int16 RowsInPage)
        {
            List<TValue> NewList = new List<TValue>();

            int StartIndex = (PageNumber * RowsInPage) + 1;

            int EndIndex = (PageNumber + 1) * RowsInPage;

            for (int i = StartIndex; i <= EndIndex; i++)
            {
                if (Count >= i)
                {
                    NewList.Add(this[i - 1]);
                }
            }

            return NewList;
        }

        public virtual void SyncDicAndGetAll()
        {
            foreach (TValue value in this)
            {
                Add(value);
            }
        }

        public virtual ListBase<TKey, TValue> Clone()
        {
            return MemberwiseClone() as ListBase<TKey, TValue>;
        }

        public virtual ListBase<TKey, TValue> Clone(bool deepClone = false)
        {
            if (!deepClone)
            {
                return MemberwiseClone() as ListBase<TKey, TValue>;
            }
            else
            {
                return DeepClone<ListBase<TKey, TValue>>();
            }
        }

        public virtual T Clone<T>(bool deepClone = false) where T : ListBase<TKey, TValue>, new()
        {
            if (!deepClone)
            {
                return MemberwiseClone() as T;
            }
            else
            {
                return DeepClone<T>();
            }
        }

        public virtual TValue Get(TKey ID)
        {
            lock (padlock)
            {
                if (MainDictionary.ContainsKey(ID.GetHashCode()))
                {
                    return MainDictionary[ID.GetHashCode()];
                }
                else
                {
                    return null;
                }
            }
        }

        public virtual void Set(TKey ID, TValue value)
        {
            lock (padlock)
            {
                if (MainDictionary.ContainsKey(ID.GetHashCode()))
                {
                    base[GetIndex(ID)] = value;
                    MainDictionary[ID.GetHashCode()] = value;
                    IsRefreshed = true;
                }
            }
        }

        #endregion

        #region Protected Methods

        protected virtual T DeepClone<T>() where T : ListBase<TKey, TValue>, new()
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(this));
        }

        #endregion

        #region Private Methods

        private Int32 GetIndex(TKey ID)
        {
            return IndexOf(Get(ID));
        }

        #endregion

        #region IOrderedEnumerable

        public IOrderedEnumerable<TValue> CreateOrderedEnumerable<TOrderKey>(Func<TValue, TOrderKey> keySelector, IComparer<TOrderKey> comparer, bool descending)
        {
            if (!descending)
            {
                return this.OrderBy(keySelector, comparer);
            }
            else
            {
                return this.OrderByDescending(keySelector, comparer);
            }
        }

        #endregion
    }
}
