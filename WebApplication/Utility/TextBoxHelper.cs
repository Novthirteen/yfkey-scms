using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace com.Sconit.Utility
{
    public static class TextBoxHelper
    {
        public static char TypeValueSeperator = '#';
        public static char ParameterSeperator = '$';

        public static string GenSingleData(string field, string value)
        {
            return "{ desc: '" + (field != null ? field : string.Empty) + "', value: '" + (value != null ? value : string.Empty) + "' }";
        }

        public static string GenSingleData(string field, string value, string imageUrl)
        {
            if (imageUrl != null && imageUrl.Length > 2)
            {
                imageUrl = imageUrl.Substring(2);
            }
            return "{desc:'" + (field != null ? field : string.Empty) + "',value:'" + (value != null ? value : string.Empty) + "',imageUrl:'" + (imageUrl != null ? imageUrl : string.Empty) + "'}";
        }

        public static string GetOption(string clientId, int minChars, int width, bool autoFill, bool mustMatch, int cacheLength)
        {
            string option = string.Empty;
            option = "$().ready(function() {" +
            "function formatItem(row) {" +
            "return row[0] + ' (<strong>id: ' + row[1] + '</strong>)';" +
            "}" +
            "function formatResult(row) {" +
            "return row[0].replace(/(<.+?>)/gi, '');" +
            "}";

            option += "$('#" + clientId + "').autocomplete(" + clientId + "_datas, {" +
            "minChars: " + minChars +
            ",width: " + width +
            ",autoFill: " + autoFill.ToString().ToLower().Trim() +
            ",mustMatch: " + mustMatch.ToString().ToLower().Trim() +
            ",formatItem: function(row, i, max) {" +
            "if(row.imageUrl==null||row.imageUrl=='') return row.value + ' [' + row.desc + ']'; else return autoCompleteFormate(row.value,row.desc,row.imageUrl);" +
            "}" +
            ",formatMatch: function(row, i, max) {" +
            "return row.value + ' ' + row.desc;" +
            "}," +
            "formatResult: function(row) {" +
            "	return row.value;" +
            "}" +
            "});" +
            "});";
            return option;
        }
    }
}
