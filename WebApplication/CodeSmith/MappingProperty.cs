using System;
using System.Collections;
using CodeSmith.Engine;
using System.ComponentModel;
using com.Sconit.Entity;

namespace com.Sconit.CodeSmith
{
    [Serializable]
    public enum DataType
    {
        BinaryBlob,
        Boolean,
        Byte,
        DateTime,
        Decimal,
        Double,
        Guid,
        Int16,
        Int32,
        Int64,
        Single,
        String,
        UInt16,
        UInt32,
        UInt64
    }

    [Serializable]
    public enum Generator
    {
        increment,
        identity,
        sequence,
        hilo,
        seqhilo,
        //uuid.hex,
        //uuid.string,
        guid,
        //guid.comb,
        native,
        assigned,
        foreign
    }

    [Serializable]
    public class MappingInfo
    {
        private bool _isPK;
        private bool _isUnique;
        private string _pkGenerator;
        private string _classPropertyName;
        private string _tableColumnName;
        private string _dataType;
        private int _dataLength;
        private bool _isNullable;
        private string _pkMany2OnePropertyName;
        private string _pkMany2OnePropertyDataType;
        private bool _isOne2Many;
        private string _one2ManyTable;
        private bool _one2ManyInverse;
        private bool _one2ManyLazy;
        private string _one2ManyColumn;

        public bool IsPK
        {
            get
            {
                return _isPK;
            }
            set
            {
                _isPK = value;
            }
        }

        public bool IsUnique
        {
            get
            {
                return _isUnique;
            }
            set
            {
                _isUnique = value;
            }
        }

        public string PKGenerator
        {
            get
            {
                return _pkGenerator;
            }
            set
            {
                _pkGenerator = value;
            }
        }

        public string ClassPropertyName
        {
            get
            {
                return _classPropertyName;
            }
            set
            {
                _classPropertyName = value;
            }
        }

        public string TableColumnName
        {
            get
            {
                return _tableColumnName;
            }
            set
            {
                _tableColumnName = value;
            }
        }

        public string DataType
        {
            get
            {
                return _dataType;
            }
            set
            {
                _dataType = value;
            }
        }

        public int DataLength
        {
            get
            {
                return _dataLength;
            }
            set
            {
                _dataLength = value;
            }
        }

        public bool IsNullable
        {
            get
            {
                return _isNullable;
            }
            set
            {
                _isNullable = value;
            }
        }

        public string PKMany2OnePropertyName
        {
            get
            {
                return _pkMany2OnePropertyName;
            }
            set
            {
                _pkMany2OnePropertyName = value;
            }
        }

        public string PKMany2OnePropertyDataType
        {
            get
            {
                return _pkMany2OnePropertyDataType;
            }
            set
            {
                _pkMany2OnePropertyDataType = value;
            }
        }

        public bool IsOne2Many
        {
            get
            {
                return _isOne2Many;
            }
            set
            {
                _isOne2Many = value;
            }
        }

        public string One2ManyTable
        {
            get
            {
                return _one2ManyTable;
            }
            set
            {
                _one2ManyTable = value;
            }
        }

        public bool One2ManyInverse
        {
            get
            {
                return _one2ManyInverse;
            }
            set
            {
                _one2ManyInverse = value;
            }
        }

        public bool One2ManyLazy
        {
            get
            {
                return _one2ManyLazy;
            }
            set
            {
                _one2ManyLazy = value;
            }
        }

        public string One2ManyColumn
        {
            get
            {
                return _one2ManyColumn;
            }
            set
            {
                _one2ManyColumn = value;
            }
        }

        public bool IsBaseDataType
        {
            get
            {
                return Enum.IsDefined(typeof(DataType), _dataType);
            }
        }

        public string UnsavedValue
        {
            get
            {
               switch(DataType)
               {
                   case "Decimal":
                   case "Double":
                   case "Int16":
                   case "Int32":
                   case "Int64":
                   case "UInt16":
                   case "UInt32":
                   case "UInt64":
                       return "0";
                   default:
                       return "null";
               }
            }            
        }
    }

    [PropertySerializer(typeof(com.Sconit.CodeSmith.MappingPropertySerializer))]
    [Editor(typeof(com.Sconit.CodeSmith.MappingPropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public class MappingProperty
    {
        private IList _mappingInfoCollection;

        public MappingProperty()
        {
            _mappingInfoCollection = new ArrayList();
        }

        public IList MappingInfoCollection
        {
            get
            {
                return _mappingInfoCollection;
            }
            set
            {
                _mappingInfoCollection = value;
            }
        }

        public int MappingPKInfoCollectionCount
        {
            get
            {
                return MappingPKInfoCollection.Count;
            }
        }

        public bool IsMappingPKContainObject
        {
            get
            {
                if (MappingPKInfoCollection != null)
                {
                    foreach (MappingInfo mi in MappingPKInfoCollection)
                    {
                        if (!Enum.IsDefined(typeof(DataType), mi.DataType))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        public IList MappingPKInfoCollection
        {
            get
            {
                IList _mappingPKInfoCollection = new ArrayList();
                foreach(MappingInfo mi in MappingInfoCollection)
                {
                    if (mi.IsPK)
                    {
                        _mappingPKInfoCollection.Add(mi);
                    }
                }
                return _mappingPKInfoCollection;
            }
        }

        public int MappingUniqueInfoCollectionCount
        {
            get
            {
                return MappingUniqueInfoCollection.Count;
            }
        }

        public bool IsMappingUniqueContainObject
        {
            get
            {
                if (MappingUniqueInfoCollection != null)
                {
                    foreach (MappingInfo mi in MappingUniqueInfoCollection)
                    {
                        if (!Enum.IsDefined(typeof(DataType), mi.DataType))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        public IList MappingUniqueInfoCollection
        {
            get
            {
                IList _mappingUniqueInfoCollection = new ArrayList();
                foreach (MappingInfo mi in MappingInfoCollection)
                {
                    if (mi.IsUnique)
                    {
                        _mappingUniqueInfoCollection.Add(mi);
                    }
                }
                return _mappingUniqueInfoCollection;
            }
        }

        public IList MappingFieldInfoCollection
        {
            get
            {
                IList _mappingFieldInfoCollection = new ArrayList();
                foreach (MappingInfo mi in MappingInfoCollection)
                {
                    if (!mi.IsPK)
                    {
                        _mappingFieldInfoCollection.Add(mi);
                    }
                }
                return _mappingFieldInfoCollection;
            }
        }

        public bool IncludeIsActiveField
        {
            get
            {
                foreach (MappingInfo mi in MappingInfoCollection)
                {
                    if (mi.TableColumnName.ToLower() == "isactive" && mi.DataType.ToLower() == "boolean")
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
