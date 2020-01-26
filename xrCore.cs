using System.Collections.Generic;

namespace xrCore
{
    public class Base_Parser
    {
        public static string SetVisualName(string item)
        {
            int index = 0;
            char[] data = item.ToCharArray();
            for (int i = 0; i < data.Length; i++) if (data[i] == '\\') index = i++;
            char[] c_data = new char[data.Length - index];
            for (int i = 0; i < c_data.Length; i++)
            {
                int charIndex = index + i;
                c_data[i] = data[charIndex];
            }
            string r_data = new string(c_data);
            return r_data.Substring(1);
        }
        public static string ObjectName(string item)
        {
            return item = item.Replace("name = ", "");
        }
        public static string SectionName(string item)
        {
            return item = item.Replace("section_name = ", "");
        }
        public static string SetDistance(string item)
        {
            return item = item.Replace("distance = ", "");
        }
        public static string SetHealth(string item)
        {
            return item = item.Replace("health = ", "");
        }
        public static string SetCharacter(string item)
        {
            return item = item.Replace("money = ", "");
        }
        public static string GetItems(List<string> items)
        {
            string data = "";
            foreach (string item in items) data += item + ';';
            return data;
        }
    }
}
