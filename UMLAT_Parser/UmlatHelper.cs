using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLAT_Parser
{
    /// <summary>
    /// Helper class
    /// </summary>
    public static class UmlatHelper
    {
        /// <summary>
        /// Convert name to UAML version
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ConvertToUMLAT(this string str)
        {
            for (int i = 0; i < str.Length - 1; i++)
            {
                var umlatDoubleStr = str[i].ToString() + str[i + 1].ToString();

                if (_umaltDict.ContainsKey(umlatDoubleStr.ToLower()))
                {
                    str = str.Replace(
                        umlatDoubleStr,
                        _umaltDict[umlatDoubleStr.ToLower()].ToString().ToUpper()
                    );
                }
            }

            return str;
        }

        private static Dictionary<string, int> CountAllUAMLT(this string str)
        {
            Dictionary<string, int> countUAMLT = new Dictionary<string, int>();

            for (int i = 0; i < str.Length - 1; i++)
            {
                var umlatDoubleStr = str[i].ToString() + str[i + 1].ToString();

                if (_umaltDict.ContainsKey(umlatDoubleStr.ToLower()))
                {
                    if (!(countUAMLT.ContainsKey(umlatDoubleStr.ToLower())))
                    {
                        countUAMLT.Add(umlatDoubleStr.ToLower(), 1);
                    }
                    else
                    {
                        countUAMLT[umlatDoubleStr.ToLower()]++;
                    }
                }
            }

            return countUAMLT;
        }

        /// <summary>
        /// Get all uamlt variations (Step 2)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<string> GetAllUAMLTVariations(this string str)
        {
            var uamLTVariations = new List<string>();

            uamLTVariations.Add(str);

            for (int i = 0; i < str.Length - 1; i++)
            {
                var umlatDoubleStr = str[i].ToString() + str[i + 1].ToString();

                if (_umaltDict.ContainsKey(umlatDoubleStr.ToLower()))
                {
                    //Save a snapshot of the string
                    var strSnapshot = uamLTVariations.FirstOrDefault();

                    str = str.Replace(
                        umlatDoubleStr,
                        _umaltDict[umlatDoubleStr.ToLower()].ToString().ToUpper()
                    );

                    strSnapshot = strSnapshot.Replace(
                        umlatDoubleStr,
                        _umaltDict[umlatDoubleStr.ToLower()].ToString().ToUpper()
                    );

                    if (!uamLTVariations.Contains(strSnapshot))
                    {
                        uamLTVariations.Add(strSnapshot);
                    }

                    uamLTVariations.Add(str);
                }
            }

            return uamLTVariations.Distinct().ToList();
        }

        /// <summary>
        /// Generate SQL search statement (STEP 3)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GenerateUAMLTSqlStatement(this string str)
        {
            var allVariations = str.GetAllUAMLTVariations();

            for (int i = 0; i < allVariations.Count; i++)
            {
                allVariations[i] = "'" + allVariations[i] + "'";
            }

            string allVariationsArray = string.Join(',', allVariations);

            string sqlStatement =
                $"SELECT * FROM tbl_phonebook WHERE last_name in ({allVariationsArray})";

            return sqlStatement;
        }

        /// <summary>
        /// Dictionary to hold UAMLT values
        /// </summary>
        public static readonly Dictionary<string, char> _umaltDict = new Dictionary<string, char>
        {
            { "ae", 'ä' },
            { "oe", 'ö' },
            { "ue", 'ü' },
            { "Ae", 'Ä' },
            { "Oe", 'Ö' },
            { "Ue", 'Ü' },
            { "ss", 'ß' }
        };
    }
}
