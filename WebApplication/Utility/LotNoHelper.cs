using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.Exception;
using System.Text.RegularExpressions;

namespace com.Sconit.Utility
{
    public static class LotNoHelper
    {
        public static string GenerateLotNo()
        {
            return GenerateLotNo(DateTime.Now);
        }

        public static string GenerateLotNo(DateTime dateTime)
        {
            /*
              	年份	代码	年份	代码	年份	代码
	            2004	4	    2016	G	    2028	W
	            2005	5	    2017	H	    2029	X
	            2006	6	    2018	J	    2030	Y
	            2007	7	    2019	K	    2031	1
	            2008	8	    2010	L	    2032	2
	            2009	9	    2021	M	    2033	3
	            2010	A	    2022	N	    2034	4
	            2011	B	    2023	P	    2035	5
	            2012	C	    2024	Q	    2036	6
	            2013	D	    2025	S	    2037	7
	            2014	E	    2026	T	    2038	8
	            2015	F	    2027	V	    2039	9
                超过2039年再从A开始
            */
            int year = dateTime.Year;
            int yearMod = (year - 2000) % 30;
            string yearStr = string.Empty;
            switch (yearMod)
            {
                case 1:
                    yearStr = "1";
                    break;
                case 2:
                    yearStr = "2";
                    break;
                case 3:
                    yearStr = "3";
                    break;
                case 4:
                    yearStr = "4";
                    break;
                case 5:
                    yearStr = "5";
                    break;
                case 6:
                    yearStr = "6";
                    break;
                case 7:
                    yearStr = "7";
                    break;
                case 8:
                    yearStr = "8";
                    break;
                case 9:
                    yearStr = "9";
                    break;
                case 10:
                    yearStr = "A";
                    break;
                case 11:
                    yearStr = "B";
                    break;
                case 12:
                    yearStr = "C";
                    break;
                case 13:
                    yearStr = "D";
                    break;
                case 14:
                    yearStr = "E";
                    break;
                case 15:
                    yearStr = "F";
                    break;
                case 16:
                    yearStr = "G";
                    break;
                case 17:
                    yearStr = "H";
                    break;
                case 18:
                    yearStr = "J";
                    break;
                case 19:
                    yearStr = "K";
                    break;
                case 20:
                    yearStr = "L";
                    break;
                case 21:
                    yearStr = "M";
                    break;
                case 22:
                    yearStr = "N";
                    break;
                case 23:
                    yearStr = "P";
                    break;
                case 24:
                    yearStr = "Q";
                    break;
                case 25:
                    yearStr = "S";
                    break;
                case 26:
                    yearStr = "T";
                    break;
                case 27:
                    yearStr = "V";
                    break;
                case 28:
                    yearStr = "W";
                    break;
                case 29:
                    yearStr = "X";
                    break;
                case 30:
                    yearStr = "Y";
                    break;
            }

            /*
             月份	代码
                1	1
                2	2
                3	3
                4	4
                5	5
                6	6
                7	7
                8	8
                9	9
                10	A
                11	B
                12	C
            */
            int month = dateTime.Month;
            string monthStr = String.Format("{0:X}", month);
            
            int day = dateTime.Day;
            string dayStr = day.ToString().PadLeft(2, '0');
            
            return yearStr + monthStr + dayStr;
        }

        public static bool validateLotNo(string lotNo)
        {

            if (lotNo.Length != 4)
            {
                throw new BusinessErrorException("MasterData.LotNo.InValidLength");
            }

            Regex ry = new Regex("[A-Y1-9]");
            Regex my = new Regex("[A-C1-9]");

            string year = lotNo.Substring(0, 1);
            string month = lotNo.Substring(1, 1);
            string day = lotNo.Substring(2, 2);

            if (!ry.IsMatch(year))
            {
                throw new BusinessErrorException("MasterData.LotNo.InValidYear");
            }

            if (!my.IsMatch(month))
            {
                throw new BusinessErrorException("MasterData.LotNo.InValidMonth");
            }

            try
            {
                int d = int.Parse(day);
                if (d > 32)
                {
                    throw new BusinessErrorException("MasterData.LotNo.InValidDate");
                }
            }
            catch (FormatException)
            {
                throw new BusinessErrorException("MasterData.LotNo.InValidDate");
            }

            return true;
        }

        public static DateTime ResolveLotNo(string lotNo)
        {
            validateLotNo(lotNo);

            char[] ch = lotNo.ToCharArray();
            char year = ch[0];
            char month = ch[1];
            string day = lotNo.Substring(2, 2);

            int yearDiff = 0;
            int yearInt = 0;

            switch (year)
            {
                case '1':
                    yearDiff = 1;
                    break;
                case '2':
                    yearDiff = 2;
                    break;
                case '3':
                    yearDiff = 3;
                    break;
                case '4':
                    yearDiff = 4;
                    break;
                case '5':
                    yearDiff = 5;
                    break;
                case '6':
                    yearDiff = 6;
                    break;
                case '7':
                    yearDiff = 7;
                    break;
                case '8':
                    yearDiff = 8;
                    break;
                case '9':
                    yearDiff = 9;
                    break;
                case 'A':
                    yearDiff = 10;
                    break;
                case 'B':
                    yearDiff = 11;
                    break;
                case 'C':
                    yearDiff = 12;
                    break;
                case 'D':
                    yearDiff = 13;
                    break;
                case  'E':
                    yearDiff = 14;
                    break;
                case 'F':
                    yearDiff = 15;
                    break;
                case 'G':
                    yearDiff = 16;
                    break;
                case 'H':
                    yearDiff = 17;
                    break;
                case 'J':
                    yearDiff = 18;
                    break;
                case 'K':
                    yearDiff = 19;
                    break;
                case 'L':
                    yearDiff = 20;
                    break;
                case 'M':
                    yearDiff = 21;
                    break;
                case 'N':
                    yearDiff = 22;
                    break;
                case 'P':
                    yearDiff = 23;
                    break;
                case 'Q':
                    yearDiff = 24;
                    break;
                case 'S':
                    yearDiff = 25;
                    break;
                case 'T':
                    yearDiff = 26;
                    break;
                case 'V':
                    yearDiff = 27;
                    break;
                case 'W':
                    yearDiff = 28;
                    break;
                case 'X':
                    yearDiff = 29;
                    break;
                case 'Y':
                    yearDiff = 30;
                    break;
                default:
                    throw new BusinessErrorException("MasterData.LotNo.InValidYear");
            }

            int nowYear = DateTime.Now.Year;
            int baseYear = ((nowYear - 2000) / 30) + 2000;

            if (baseYear + yearDiff > nowYear)
            {
                yearInt = baseYear - yearDiff;
            }
            else
            {
                yearInt = baseYear + yearDiff;
            }
            

            int monthInt = Int32.Parse(month.ToString(), System.Globalization.NumberStyles.HexNumber);
            int dayInt = Int32.Parse(day.TrimStart('0'));

            return new DateTime(yearInt, monthInt, dayInt);
        }
    }
}
