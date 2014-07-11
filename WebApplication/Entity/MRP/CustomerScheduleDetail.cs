using System;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public class CustomerScheduleDetail : CustomerScheduleDetailBase
    {
        #region Non O/R Mapping Properties

        public int Sequence { get; set; }

        public string Flow { get; set; }

        #endregion
    }

    [Serializable]
    public class ScheduleBody
    {
        #region  Properties

        public Int32 Seq { get; set; }
        public string Item { get; set; }
        public string Uom { get; set; }
        public Decimal UnitCount { get; set; }
        public string Location { get; set; }
        public string ItemDescription { get; set; }
        public string ItemReference { get; set; }

        public decimal Qty0 { get; set; }
        public decimal Qty1 { get; set; }
        public decimal Qty2 { get; set; }
        public decimal Qty3 { get; set; }
        public decimal Qty4 { get; set; }
        public decimal Qty5 { get; set; }
        public decimal Qty6 { get; set; }
        public decimal Qty7 { get; set; }
        public decimal Qty8 { get; set; }
        public decimal Qty9 { get; set; }
        public decimal Qty10 { get; set; }
        public decimal Qty11 { get; set; }
        public decimal Qty12 { get; set; }
        public decimal Qty13 { get; set; }
        public decimal Qty14 { get; set; }
        public decimal Qty15 { get; set; }
        public decimal Qty16 { get; set; }
        public decimal Qty17 { get; set; }
        public decimal Qty18 { get; set; }
        public decimal Qty19 { get; set; }
        public decimal Qty20 { get; set; }
        public decimal Qty21 { get; set; }
        public decimal Qty22 { get; set; }
        public decimal Qty23 { get; set; }
        public decimal Qty24 { get; set; }
        public decimal Qty25 { get; set; }
        public decimal Qty26 { get; set; }
        public decimal Qty27 { get; set; }
        public decimal Qty28 { get; set; }
        public decimal Qty29 { get; set; }
        public decimal Qty30 { get; set; }
        public decimal Qty31 { get; set; }
        public decimal Qty32 { get; set; }
        public decimal Qty33 { get; set; }
        public decimal Qty34 { get; set; }
        public decimal Qty35 { get; set; }
        public decimal Qty36 { get; set; }
        public decimal Qty37 { get; set; }
        public decimal Qty38 { get; set; }
        public decimal Qty39 { get; set; }
        public decimal Qty40 { get; set; }

        public decimal ActQty0 { get; set; }
        public decimal ActQty1 { get; set; }
        public decimal ActQty2 { get; set; }
        public decimal ActQty3 { get; set; }
        public decimal ActQty4 { get; set; }
        public decimal ActQty5 { get; set; }
        public decimal ActQty6 { get; set; }
        public decimal ActQty7 { get; set; }
        public decimal ActQty8 { get; set; }
        public decimal ActQty9 { get; set; }
        public decimal ActQty10 { get; set; }
        public decimal ActQty11 { get; set; }
        public decimal ActQty12 { get; set; }
        public decimal ActQty13 { get; set; }
        public decimal ActQty14 { get; set; }
        public decimal ActQty15 { get; set; }
        public decimal ActQty16 { get; set; }
        public decimal ActQty17 { get; set; }
        public decimal ActQty18 { get; set; }
        public decimal ActQty19 { get; set; }
        public decimal ActQty20 { get; set; }
        public decimal ActQty21 { get; set; }
        public decimal ActQty22 { get; set; }
        public decimal ActQty23 { get; set; }
        public decimal ActQty24 { get; set; }
        public decimal ActQty25 { get; set; }
        public decimal ActQty26 { get; set; }
        public decimal ActQty27 { get; set; }
        public decimal ActQty28 { get; set; }
        public decimal ActQty29 { get; set; }
        public decimal ActQty30 { get; set; }
        public decimal ActQty31 { get; set; }
        public decimal ActQty32 { get; set; }
        public decimal ActQty33 { get; set; }
        public decimal ActQty34 { get; set; }
        public decimal ActQty35 { get; set; }
        public decimal ActQty36 { get; set; }
        public decimal ActQty37 { get; set; }
        public decimal ActQty38 { get; set; }
        public decimal ActQty39 { get; set; }
        public decimal ActQty40 { get; set; }

        public decimal RequiredQty0 { get; set; }
        public decimal RequiredQty1 { get; set; }
        public decimal RequiredQty2 { get; set; }
        public decimal RequiredQty3 { get; set; }
        public decimal RequiredQty4 { get; set; }
        public decimal RequiredQty5 { get; set; }
        public decimal RequiredQty6 { get; set; }
        public decimal RequiredQty7 { get; set; }
        public decimal RequiredQty8 { get; set; }
        public decimal RequiredQty9 { get; set; }
        public decimal RequiredQty10 { get; set; }
        public decimal RequiredQty11 { get; set; }
        public decimal RequiredQty12 { get; set; }
        public decimal RequiredQty13 { get; set; }
        public decimal RequiredQty14 { get; set; }
        public decimal RequiredQty15 { get; set; }
        public decimal RequiredQty16 { get; set; }
        public decimal RequiredQty17 { get; set; }
        public decimal RequiredQty18 { get; set; }
        public decimal RequiredQty19 { get; set; }
        public decimal RequiredQty20 { get; set; }
        public decimal RequiredQty21 { get; set; }
        public decimal RequiredQty22 { get; set; }
        public decimal RequiredQty23 { get; set; }
        public decimal RequiredQty24 { get; set; }
        public decimal RequiredQty25 { get; set; }
        public decimal RequiredQty26 { get; set; }
        public decimal RequiredQty27 { get; set; }
        public decimal RequiredQty28 { get; set; }
        public decimal RequiredQty29 { get; set; }
        public decimal RequiredQty30 { get; set; }
        public decimal RequiredQty31 { get; set; }
        public decimal RequiredQty32 { get; set; }
        public decimal RequiredQty33 { get; set; }
        public decimal RequiredQty34 { get; set; }
        public decimal RequiredQty35 { get; set; }
        public decimal RequiredQty36 { get; set; }
        public decimal RequiredQty37 { get; set; }
        public decimal RequiredQty38 { get; set; }
        public decimal RequiredQty39 { get; set; }
        public decimal RequiredQty40 { get; set; }

        public string DisplayQty0
        {
            get
            {
                return FormatQty(Qty0) + "(<a id='actQty0'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty0) + "</a> | <a id='requiredQty0' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty0) + "</a>)";
            }
        }
        public string DisplayQty1
        {
            get
            {
                return FormatQty(Qty1) + "(<a id='actQty1'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty1) + "</a> | <a id='requiredQty1' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty1) + "</a>)";
            }
        }
        public string DisplayQty2
        {
            get
            {
                return FormatQty(Qty2) + "(<a id='actQty2'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty2) + "</a> | <a id='requiredQty2' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty2) + "</a>)";
            }
        }
        public string DisplayQty3
        {
            get
            {
                return FormatQty(Qty3) + "(<a id='actQty3'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty3) + "</a> | <a id='requiredQty3' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty3) + "</a>)";
            }
        }
        public string DisplayQty4
        {
            get
            {
                return FormatQty(Qty4) + "(<a id='actQty4'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty4) + "</a> | <a id='requiredQty4' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty4) + "</a>)";
            }
        }
        public string DisplayQty5
        {
            get
            {
                return FormatQty(Qty5) + "(<a id='actQty5'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty5) + "</a> | <a id='requiredQty5' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty5) + "</a>)";
            }
        }
        public string DisplayQty6
        {
            get
            {
                return FormatQty(Qty6) + "(<a id='actQty6'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty6) + "</a> | <a id='requiredQty6' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty6) + "</a>)";
            }
        }
        public string DisplayQty7
        {
            get
            {
                return FormatQty(Qty7) + "(<a id='actQty7'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty7) + "</a> | <a id='requiredQty7' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty7) + "</a>)";
            }
        }
        public string DisplayQty8
        {
            get
            {
                return FormatQty(Qty8) + "(<a id='actQty8'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty8) + "</a> | <a id='requiredQty8' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty8) + "</a>)";
            }
        }
        public string DisplayQty9
        {
            get
            {
                return FormatQty(Qty9) + "(<a id='actQty9'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty9) + "</a> | <a id='requiredQty9' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty9) + "</a>)";
            }
        }
        public string DisplayQty10
        {
            get
            {
                return FormatQty(Qty10) + "(<a id='actQty10'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty10) + "</a> | <a id='requiredQty10' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty10) + "</a>)";
            }
        }
        public string DisplayQty11
        {
            get
            {
                return FormatQty(Qty11) + "(<a id='actQty11'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty11) + "</a> | <a id='requiredQty11' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty11) + "</a>)";
            }
        }
        public string DisplayQty12
        {
            get
            {
                return FormatQty(Qty12) + "(<a id='actQty12'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty12) + "</a> | <a id='requiredQty12' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty12) + "</a>)";
            }
        }
        public string DisplayQty13
        {
            get
            {
                return FormatQty(Qty13) + "(<a id='actQty13'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty13) + "</a> | <a id='requiredQty13' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty13) + "</a>)";
            }
        }
        public string DisplayQty14
        {
            get
            {
                return FormatQty(Qty14) + "(<a id='actQty14'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty14) + "</a> | <a id='requiredQty14' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty14) + "</a>)";
            }
        }
        public string DisplayQty15
        {
            get
            {
                return FormatQty(Qty15) + "(<a id='actQty15'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty15) + "</a> | <a id='requiredQty15' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty15) + "</a>)";
            }
        }
        public string DisplayQty16
        {
            get
            {
                return FormatQty(Qty16) + "(<a id='actQty16'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty16) + "</a> | <a id='requiredQty16' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty16) + "</a>)";
            }
        }
        public string DisplayQty17
        {
            get
            {
                return FormatQty(Qty17) + "(<a id='actQty17'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty17) + "</a> | <a id='requiredQty17' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty17) + "</a>)";
            }
        }
        public string DisplayQty18
        {
            get
            {
                return FormatQty(Qty18) + "(<a id='actQty18'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty18) + "</a> | <a id='requiredQty18' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty18) + "</a>)";
            }
        }
        public string DisplayQty19
        {
            get
            {
                return FormatQty(Qty19) + "(<a id='actQty19'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty19) + "</a> | <a id='requiredQty19' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty19) + "</a>)";
            }
        }
        public string DisplayQty20
        {
            get
            {
                return FormatQty(Qty20) + "(<a id='actQty20'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty20) + "</a> | <a id='requiredQty20' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty20) + "</a>)";
            }
        }
        public string DisplayQty21
        {
            get
            {
                return FormatQty(Qty21) + "(<a id='actQty21'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty21) + "</a> | <a id='requiredQty21' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty21) + "</a>)";
            }
        }
        public string DisplayQty22
        {
            get
            {
                return FormatQty(Qty22) + "(<a id='actQty22'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty22) + "</a> | <a id='requiredQty22' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty22) + "</a>)";
            }
        }
        public string DisplayQty23
        {
            get
            {
                return FormatQty(Qty23) + "(<a id='actQty23'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty23) + "</a> | <a id='requiredQty23' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty23) + "</a>)";
            }
        }
        public string DisplayQty24
        {
            get
            {
                return FormatQty(Qty24) + "(<a id='actQty24'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty24) + "</a> | <a id='requiredQty24' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty24) + "</a>)";
            }
        }
        public string DisplayQty25
        {
            get
            {
                return FormatQty(Qty25) + "(<a id='actQty25'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty25) + "</a> | <a id='requiredQty25' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty25) + "</a>)";
            }
        }
        public string DisplayQty26
        {
            get
            {
                return FormatQty(Qty26) + "(<a id='actQty26'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty26) + "</a> | <a id='requiredQty26' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty26) + "</a>)";
            }
        }
        public string DisplayQty27
        {
            get
            {
                return FormatQty(Qty27) + "(<a id='actQty27'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty27) + "</a> | <a id='requiredQty27' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty27) + "</a>)";
            }
        }
        public string DisplayQty28
        {
            get
            {
                return FormatQty(Qty28) + "(<a id='actQty28'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty28) + "</a> | <a id='requiredQty28' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty28) + "</a>)";
            }
        }
        public string DisplayQty29
        {
            get
            {
                return FormatQty(Qty29) + "(<a id='actQty29'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty29) + "</a> | <a id='requiredQty29' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty29) + "</a>)";
            }
        }
        public string DisplayQty30
        {
            get
            {
                return FormatQty(Qty30) + "(<a id='actQty30'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty30) + "</a> | <a id='requiredQty30' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty30) + "</a>)";
            }
        }
        public string DisplayQty31
        {
            get
            {
                return FormatQty(Qty31) + "(<a id='actQty31'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty31) + "</a> | <a id='requiredQty31' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty31) + "</a>)";
            }
        }
        public string DisplayQty32
        {
            get
            {
                return FormatQty(Qty32) + "(<a id='actQty32'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty32) + "</a> | <a id='requiredQty32' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty32) + "</a>)";
            }
        }
        public string DisplayQty33
        {
            get
            {
                return FormatQty(Qty33) + "(<a id='actQty33'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty33) + "</a> | <a id='requiredQty33' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty33) + "</a>)";
            }
        }
        public string DisplayQty34
        {
            get
            {
                return FormatQty(Qty34) + "(<a id='actQty34'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty34) + "</a> | <a id='requiredQty34' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty34) + "</a>)";
            }
        }
        public string DisplayQty35
        {
            get
            {
                return FormatQty(Qty35) + "(<a id='actQty35'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty35) + "</a> | <a id='requiredQty35' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty35) + "</a>)";
            }
        }
        public string DisplayQty36
        {
            get
            {
                return FormatQty(Qty36) + "(<a id='actQty36'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty36) + "</a> | <a id='requiredQty36' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty36) + "</a>)";
            }
        }
        public string DisplayQty37
        {
            get
            {
                return FormatQty(Qty37) + "(<a id='actQty37'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty37) + "</a> | <a id='requiredQty37' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty37) + "</a>)";
            }
        }
        public string DisplayQty38
        {
            get
            {
                return FormatQty(Qty38) + "(<a id='actQty38'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty38) + "</a> | <a id='requiredQty38' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty38) + "</a>)";
            }
        }
        public string DisplayQty39
        {
            get
            {
                return FormatQty(Qty39) + "(<a id='actQty39'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty39) + "</a> | <a id='requiredQty39' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty39) + "</a>)";
            }
        }
        public string DisplayQty40
        {
            get
            {
                return FormatQty(Qty40) + "(<a id='actQty40'  runat='server' href='javascript:void(0);' onclick='getActQty(this);'>" + FormatActQty(ActQty40) + "</a> | <a id='requiredQty40' href='javascript:void(0);' onclick='getRequiredQty(this);'>" + FormatRequiredQty(RequiredQty40) + "</a>)";
            }
        }
        #endregion
        public decimal TotalQty
        {
            get
            {
                return this.RequiredQty0 + this.RequiredQty1 + this.RequiredQty2 + this.RequiredQty3 + this.RequiredQty4 + this.RequiredQty5 + this.RequiredQty6 + this.RequiredQty7 + this.RequiredQty8 + this.RequiredQty9
                    + this.RequiredQty10 + this.RequiredQty11 + this.RequiredQty12 + this.RequiredQty13 + this.RequiredQty14 + this.RequiredQty15 + this.RequiredQty16 + this.RequiredQty17 + this.RequiredQty18 + this.RequiredQty19
                    + this.RequiredQty20 + this.RequiredQty21 + this.RequiredQty22 + this.RequiredQty23 + this.RequiredQty24 + this.RequiredQty25 + this.RequiredQty26 + this.RequiredQty27 + this.RequiredQty28 + this.RequiredQty29
                    + this.RequiredQty30 + this.RequiredQty31 + this.RequiredQty32 + this.RequiredQty33 + this.RequiredQty34 + this.RequiredQty35 + this.RequiredQty36 + this.RequiredQty37 + this.RequiredQty38 + this.RequiredQty39
                    + this.RequiredQty40;
            }
        }

        private string FormatQty(decimal qty)
        {
            if (qty != 0)
            {
                return "<font color='red'>" + qty.ToString("#,##0") + "</font>";
            }
            else
            {
                return qty.ToString("#,##0");
            }
        }

        private string FormatActQty(decimal qty)
        {
            if (qty != 0)
            {
                return "<font color='green'>" + qty.ToString("#,##0") + "</font>";
            }
            else
            {
                return qty.ToString("#,##0");
            }
        }

        private string FormatRequiredQty(decimal qty)
        {
            if (qty != 0)
            {
                return "<font color='blue'>" + qty.ToString("#,##0") + "</font>";
            }
            else
            {
                return qty.ToString("#,##0");
            }
        }
    }

    public class ScheduleHead
    {
        public string Flow { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public DateTime? LastDateFrom { get; set; }
        public DateTime? LastDateTo { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public DateTime StartDate { get; set; }
        private string _dateHead;
        public string DateHead
        {
            get
            {
                if (Type == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_DAY)
                {
                    _dateHead = Type + "*" + DateFrom.ToString("ddd") + "*" + DateFrom.ToString("MMdd");
                }
                else if (Type == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_MONTH
                    || Type == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_WEEK)
                {
                    _dateHead = Type + "*" + DateFrom.ToString("MMdd") + "-" + DateTo.ToString("MMdd");
                }
                else
                {
                    _dateHead = DateTo.ToString("yyyy-MM-dd");
                }
                return _dateHead;
            }
        }
    }

    public class ScheduleView
    {
        public IList<ScheduleHead> ScheduleHeads { get; set; }
        public IList<ScheduleBody> ScheduleBodys { get; set; }
    }
}