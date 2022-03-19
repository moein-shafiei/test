using System;

namespace DotFramework.Infra.DataAccess
{
    public static class DBValueExtensions
    {
        public static object GetDbValue(this object obj)
        {
            object retlVal = DBNull.Value;

            if (obj is DateTime)
            {
                if ((DateTime)obj != new DateTime())
                {
                    retlVal = obj;
                }
            }
            else if (obj is Guid)
            {
                if ((Guid)obj != new Guid())
                {
                    retlVal = obj;
                }
            }
            else
            {
                if (obj != null)
                {
                    retlVal = obj;
                }
            }

            return retlVal;
        }

        public static object GetDbValue(this object obj, bool isForeignKey)
        {
            object retlVal = DBNull.Value;

            if (isForeignKey)
            {
                if (obj is Guid)
                {
                    if ((Guid)obj != new Guid())
                    {
                        retlVal = obj;
                    }
                }
                else
                {
                    try
                    {
                        long value = Convert.ToInt64(obj);

                        if (value != 0)
                        {
                            retlVal = obj;
                        }
                    }
                    catch
                    {
                        throw new NotSupportedException(String.Format("{0} is not supported as a foreign key", obj.GetType().FullName));
                    }
                }
            }
            else
            {
                retlVal = obj.GetDbValue();
            }

            return retlVal;
        }

        public static long ConvertToRowVersion(this object obj)
        {
            if (obj is Int64)
            {
                return (Int64)obj;
            }
            else if (obj is byte[])
            {
                byte[] rowVersion = (byte[])obj;

                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(rowVersion);
                }

                return BitConverter.ToInt64(rowVersion, 0);
            }
            else
            {
                throw new NotSupportedException("Not Supported Object Type");
            }
        }
    }
}
