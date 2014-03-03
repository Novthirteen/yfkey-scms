using System;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Dss
{
    [Serializable]
    public class DssImportHistory : DssImportHistoryBase
    {
        #region Non O/R Mapping Properties

        //山寨方法,日后再说
        public string this[int i]
        {
            get
            {
                #region Get
                switch (i)
                {
                    case 0:
                        return data0;
                    case 1:
                        return data1;
                    case 2:
                        return data2;
                    case 3:
                        return data3;
                    case 4:
                        return data4;
                    case 5:
                        return data5;
                    case 6:
                        return data6;
                    case 7:
                        return data7;
                    case 8:
                        return data8;
                    case 9:
                        return data9;
                    case 10:
                        return data10;
                    case 11:
                        return data11;
                    case 12:
                        return data12;
                    case 13:
                        return data13;
                    case 14:
                        return data14;
                    case 15:
                        return data15;
                    case 16:
                        return data16;
                    case 17:
                        return data17;
                    case 18:
                        return data18;
                    case 19:
                        return data19;
                    case 20:
                        return data20;
                    default:
                        return data20;
                }
                #endregion
            }
            set
            {
                #region Set
                switch (i)
                {
                    case 0:
                        data0 = value;
                        break;
                    case 1:
                        data1 = value;
                        break;
                    case 2:
                        data2 = value;
                        break;
                    case 3:
                        data3 = value;
                        break;
                    case 4:
                        data4 = value;
                        break;
                    case 5:
                        data5 = value;
                        break;
                    case 6:
                        data6 = value;
                        break;
                    case 7:
                        data7 = value;
                        break;
                    case 8:
                        data8 = value;
                        break;
                    case 9:
                        data9 = value;
                        break;
                    case 10:
                        data10 = value;
                        break;
                    case 11:
                        data11 = value;
                        break;
                    case 12:
                        data12 = value;
                        break;
                    case 13:
                        data13 = value;
                        break;
                    case 14:
                        data14 = value;
                        break;
                    case 15:
                        data15 = value;
                        break;
                    case 16:
                        data16 = value;
                        break;
                    case 17:
                        data17 = value;
                        break;
                    case 18:
                        data18 = value;
                        break;
                    case 19:
                        data19 = value;
                        break;
                    case 20:
                        data20 = value;
                        break;
                    default:
                        data20 = value;
                        break;
                }
                #endregion
            }
        }

        #region WoReceiptJob
        public string ProdLine { get; set; }
        public string ShiftCode { get; set; }
        #endregion

        #endregion
    }
}