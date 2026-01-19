using System.Text.RegularExpressions;

namespace MyShop.Models
{
    public class StringHelper
    {
        public static string GenerateCode(int ord, int length = 5)
        {
            return ord.ToString().PadLeft(length, '0'); // Sinh mã với chiều dài cố định, thêm '0' ở bên trái
        }


        #region Name To Tag
        public static string NameToTag(string strName)
        {
            string strReturn = strName.Trim().ToLower();
            strReturn = GetContent(strReturn, 150);
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            strReturn = Regex.Replace(strReturn, "[^\\w\\s]", string.Empty);
            string strFormD = strReturn.Normalize(System.Text.NormalizationForm.FormD);
            strReturn = regex.Replace(strFormD, string.Empty).Replace("đ", "d");
            strReturn = Regex.Replace(strReturn, "(-+)", " ");
            strReturn = Regex.Replace(strReturn.Trim(), "( +)", "-");
            strReturn = Regex.Replace(strReturn.Trim(), "(?+)", "");
            return strReturn;
        }
        #endregion
        public static string ShowNameLevel(string Name, string Level)
        {
            return ShowNameLevel(Name, Level, 1);
        }
        public static string ShowNameLevel(string Name, string Level, int start)
        {
            int strLevel = (Level.Length / 5);
            string strReturn = "";
            for (int i = start; i < strLevel; i++)
            {
                strReturn = strReturn + "---";
            }
            strReturn += Name;
            return strReturn;
        }
        #region GetContent
        public static string GetContent(object String)
        {
            return String != null && String.ToString().Length > 0 ? String.ToString() : "";
        }
        public static string GetContent(string String, int Length)
        {
            string strReturn = String;
            if (String.Length > Length && String.Length - Length > 5)
            {
                try
                {
                    strReturn = String.Substring(0, String.IndexOf(" ", Length)) + "...";
                }
                catch
                {
                    strReturn = String;
                }
            }
            return strReturn;
        }
        #endregion
    }
}
