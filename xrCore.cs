using System.Collections.Generic;
using System.Drawing;

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
        /// <summary>
        /// Анализ физических объектов
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="object_name"></param>
        public static string B_Base_Physics(List<string> objects, string object_name)
        {
            string section_name = "";
            string name = "";
            string position = "";
            string direction = "";
            string visual_name = "";
            string mass = "";
            foreach (string str in objects)
            {
                if (str.Contains("section_name = " + object_name))
                {
                    section_name = SectionName(str);
                    continue;
                }
                if (str.Contains("name") && !str.Contains("upd:") && !str.Contains("skeleton_name") && !str.Contains("visual_name"))
                {
                    name = ObjectName(str);
                    continue;
                }
                if (str.Contains("position") && !str.Contains("upd:"))
                {
                    string st = str.Replace("position = ", "");
                    string[] pos = st.Split(',');
                    for (int i = 0; i < pos.Length; i++)
                    {
                        pos[i] = pos[i] + "f,:";
                        position += pos[i];
                    }
                    continue;
                }
                if (str.Contains("direction") && !str.Contains("upd:"))
                {
                    string st = str.Replace("direction = ", "");
                    string[] dir = st.Split(',');
                    for (int i = 0; i < dir.Length; i++)
                    {
                        dir[i] = dir[i] + "f,:";
                        direction += dir[i];
                    }
                    continue;
                }
                if (str.Contains("visual_name") && !str.Contains("upd:"))
                {
                    visual_name = SetVisualName(str);
                    continue;
                }
                if (str.Contains("mass") && !str.Contains("upd:"))
                {
                    string st = str.Replace("mass = ", "");
                    mass = st;
                    continue;
                }
            }
            string resultString = "";
            resultString += '&' + visual_name + '&' + ',' + ':';
            resultString += '&' + section_name + '&' + ',' + ':';
            resultString += '&' + name + '&' + ',' + ':';
            resultString += position;
            resultString += direction;
            resultString += mass + 'f' + ',' + ':';
            return resultString;
        }
        /// <summary>
        /// Анализ источников света
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="lamp_name"></param>
        public static string B_Base_Light(List<string> objects, string lamp_name)
        {

            string section_name = "";
            string name = "";
            string position = "";
            string direction = "";
            string main_color = "";
            string visual_name = "";
            string main_brightness = "";
            string main_range = "";
            string light_flags = "";
            string main_cone_angle = "";
            foreach (string str in objects)
            {
                if (str.Contains("section_name = " + lamp_name))
                {
                    section_name = SectionName(str);
                    continue;
                }
                if (str.Contains("name") && !str.Contains("upd:") && !str.Contains("skeleton_name") && !str.Contains("visual_name"))
                {
                    name = ObjectName(str);
                    continue;
                }
                if (str.Contains("position") && !str.Contains("upd:"))
                {
                    string st = str.Replace("position = ", "");
                    string[] pos = st.Split(',');
                    for (int i = 0; i < pos.Length; i++)
                    {
                        pos[i] = pos[i] + "f,:";
                        position += pos[i];
                    }
                    continue;
                }
                if (str.Contains("direction") && !str.Contains("upd:"))
                {
                    string st = str.Replace("direction = ", "");
                    string[] dir = st.Split(',');
                    for (int i = 0; i < dir.Length; i++)
                    {
                        dir[i] = dir[i] + "f,:";
                        direction += dir[i];
                    }
                    continue;
                }
                if (str.Contains("visual_name") && !str.Contains("upd:"))
                {
                    visual_name = SetVisualName(str);
                    continue;
                }
                if (str.Contains("main_brightness"))
                {
                    string st = str.Replace("main_brightness = ", "");
                    main_brightness = st;
                    continue;
                }
                if (str.Contains("main_cone_angle"))
                {
                    string st = str.Replace("main_cone_angle = ", "");
                    main_cone_angle = st;
                    continue;
                }
                if (str.Contains("light_flags"))
                {
                    string st = str.Replace("light_flags = ", "");
                    light_flags = st;
                    continue;
                }
                if (str.Contains("main_range"))
                {
                    string st = str.Replace("main_range = ", "");
                    main_range = st;
                    continue;
                }
                if (str.Contains("main_color") && !str.Contains("main_color_animator "))
                {
                    string st = str.Replace("main_color = ", "");
                    Color color = ColorTranslator.FromHtml(st);
                    st = color.R + "f,:" + color.G + "f,:" + color.B + "f,:";
                    main_color = st;
                    continue;
                }
            }
            string resultString = "";
            resultString += '&' + visual_name + '&' + ',' + ':';
            resultString += '&' + section_name + '&' + ',' + ':';
            resultString += '&' + name + '&' + ',' + ':';
            resultString += position;
            resultString += direction;
            resultString += main_color;
            resultString += main_brightness + 'f' + ',' + ':';
            resultString += main_range + 'f' + ',' + ':';
            resultString += main_cone_angle + 'f' + ',' + ':';
            resultString += '&' + light_flags + '&' + ',' + ':';
            return resultString;
        }
        /// <summary>
        /// Анализ сталкеров
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="stalker_name"></param>
        public static string B_Base_Stalker(List<string> objects, string stalker_name)
        {
            string section_name = "";
            string name = "";
            string position = "";
            string direction = "";
            string visual_name = "";
            string distance = "";
            string money = "";
            string character_profile = "";
            string health = "";
            string items = "";
            foreach (string str in objects)
            {
                if (str.Contains("visual_name") && !str.Contains("upd:"))
                {
                    visual_name = SetVisualName(str);
                    continue;
                }
                if (str.Contains("section_name = " + stalker_name))
                {
                    section_name = SectionName(str);
                    continue;
                }
                if (str.Contains("name") && !str.Contains("upd:") && !str.Contains("skeleton_name") && !str.Contains("visual_name"))
                {
                    name = ObjectName(str);
                    continue;
                }
                if (str.Contains("position") && !str.Contains("upd:"))
                {
                    string st = str.Replace("position = ", "");
                    string[] pos = st.Split(',');
                    for (int i = 0; i < pos.Length; i++)
                    {
                        pos[i] = pos[i] + "f,:";
                        position += pos[i];
                    }
                    continue;
                }
                if (str.Contains("direction") && !str.Contains("upd:"))
                {
                    string st = str.Replace("direction = ", "");
                    string[] dir = st.Split(',');
                    for (int i = 0; i < dir.Length; i++)
                    {
                        dir[i] = dir[i] + "f,:";
                        direction += dir[i];
                    }
                    continue;
                }
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    distance = SetDistance(str);
                    continue;
                }
                if (str.Contains("money") && !str.Contains("upd:"))
                {
                    money = SetCharacter(str);
                    continue;
                }
                if (str.Contains("character_profile") && !str.Contains("upd:"))
                {
                    string st = str.Replace("character_profile = ", "");
                    character_profile = st;
                    continue;
                }
                if (str.Contains("health") && !str.Contains("upd:"))
                {
                    health = SetHealth(str);
                    continue;
                }
                if (str.Contains("[spawn]") && !str.Contains("upd:"))
                {
                    int indexNullitem = 0;
                    int indexEndItemlist = 0;
                    for (int i = 0; i < objects.Count; i++)
                    {
                        if (str.Contains("[spawn]") && !str.Contains("upd:"))
                        {
                            indexNullitem = ++i;
                            continue;
                        }
                        if (str.Contains("END") && !str.Contains("upd:") && !str.Contains("custom_data = <<END") && !str.Contains("<<END"))
                        {
                            indexEndItemlist = --i;
                            break;
                        }
                    }
                    List<string> itemList = new List<string>();
                    for (int j = indexNullitem; j < indexEndItemlist; j++) itemList.Add(objects[j]);
                    items = GetItems(itemList);
                    continue;
                }
            }
            string resultString = "";
            resultString += '&' + visual_name + '&' + ',' + ':';
            resultString += '&' + section_name + '&' + ',' + ':';
            resultString += '&' + name + '&' + ',' + ':';
            resultString += position;
            resultString += direction;
            resultString += distance + 'f' + ',' + ':';
            resultString += money + 'f' + ',' + ':';
            resultString += health + 'f' + ',' + ':';
            resultString += '&' + character_profile + '&' + ',' + ':';
            resultString += '&' + items + '&' + ',' + ':';
            return resultString;
        }
        /// <summary>
        /// Анализ мутантов 
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="mutant_name"></param>
        public static string B_Base_Monster(List<string> objects, string mutant_name)
        {
            string section_name = "";
            string name = "";
            string position = "";
            string direction = "";
            string visual_name = "";
            string distance = "";
            string health = "";
            foreach (string str in objects)
            {
                if (str.Contains("section_name = " + mutant_name))
                {
                    section_name = SectionName(str);
                    continue;
                }
                if (str.Contains("name") && !str.Contains("upd:") && !str.Contains("skeleton_name") && !str.Contains("visual_name"))
                {
                    name = ObjectName(str);
                    continue;
                }
                if (str.Contains("position") && !str.Contains("upd:"))
                {
                    string st = str.Replace("position = ", "");
                    string[] pos = st.Split(',');
                    for (int i = 0; i < pos.Length; i++)
                    {
                        pos[i] = pos[i] + "f,:";
                        position += pos[i];
                    }
                    continue;
                }
                if (str.Contains("direction") && !str.Contains("upd:"))
                {
                    string st = str.Replace("direction = ", "");
                    string[] dir = st.Split(',');
                    for (int i = 0; i < dir.Length; i++)
                    {
                        dir[i] = dir[i] + "f,:";
                        direction += dir[i];
                    }
                    continue;
                }
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    distance = SetDistance(str);
                    continue;
                }
                if (str.Contains("visual_name") && !str.Contains("upd:"))
                {
                    visual_name = SetVisualName(str);
                    continue;
                }
                if (str.Contains("health") && !str.Contains("upd:"))
                {
                    health = SetHealth(str);
                    continue;
                }
            }

            string resultString = "";
            resultString += '&' + visual_name + '&' + ',' + ':';
            resultString += '&' + section_name + '&' + ',' + ':';
            resultString += '&' + name + '&' + ',' + ':';
            resultString += position;
            resultString += direction;
            resultString += distance + 'f' + ',' + ':';
            resultString += health + 'f' + ',' + ':';
            return resultString;
        }
        /// <summary>
        /// Анализ Item'ов
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="item_name"></param>
        public static string B_Base_Item(List<string> objects, string item_name)
        {
            string section_name = "";
            string name = "";
            string position = "";
            string direction = "";
            string visual_name = "";
            foreach (string str in objects)
            {
                if (str.Contains("section_name = " + item_name))
                {
                    section_name = SectionName(str);
                    continue;
                }
                if (str.Contains("name") && !str.Contains("upd:") && !str.Contains("skeleton_name") && !str.Contains("visual_name"))
                {
                    name = ObjectName(str);
                    continue;
                }
                if (str.Contains("position") && !str.Contains("upd:"))
                {
                    string st = str.Replace("position = ", "");
                    string[] pos = st.Split(',');
                    for (int i = 0; i < pos.Length; i++)
                    {
                        pos[i] = pos[i] + "f,:";
                        position += pos[i];
                    }
                    continue;
                }
                if (str.Contains("direction") && !str.Contains("upd:"))
                {
                    string st = str.Replace("direction = ", "");
                    string[] dir = st.Split(',');
                    for (int i = 0; i < dir.Length; i++)
                    {
                        dir[i] = dir[i] + "f,:";
                        direction += dir[i];
                    }
                    continue;
                }
                if (str.Contains("visual_name") && !str.Contains("upd:"))
                {
                    visual_name = SetVisualName(str);
                    continue;
                }
            }
            string resultString = "";
            resultString += '&' + visual_name + '&' + ',' + ':';
            resultString += '&' + section_name + '&' + ',' + ':';
            resultString += '&' + name + '&' + ',' + ':';
            resultString += position;
            resultString += direction;
            return resultString;
        }
        /// <summary>
        /// Анализ аномальных зон
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="anomaly_name"></param>
        public static string B_Base_Anomaly_Zone(List<string> objects, string anomaly_name)
        {
            string section_name = "";
            string name = "";
            string position = "";
            string direction = "";
            string distance = "";
            string type = "";
            string radius = "";
            string max_power = "";
            string offline_interactive_radius = "";
            string artefact_spawn_count = "";
            foreach (string str in objects)
            {
                if (str.Contains("section_name = " + anomaly_name))
                {
                    section_name = SectionName(str);
                    continue;
                }
                if (str.Contains("name") && !str.Contains("upd:") && !str.Contains("skeleton_name") && !str.Contains("visual_name"))
                {
                    name = ObjectName(str);
                    continue;
                }
                if (str.Contains("position") && !str.Contains("upd:"))
                {
                    string st = str.Replace("position = ", "");
                    string[] pos = st.Split(',');
                    for (int i = 0; i < pos.Length; i++)
                    {
                        pos[i] = pos[i] + "f,:";
                        position += pos[i];
                    }
                    continue;
                }
                if (str.Contains("direction") && !str.Contains("upd:"))
                {
                    string st = str.Replace("direction = ", "");
                    string[] dir = st.Split(',');
                    for (int i = 0; i < dir.Length; i++)
                    {
                        dir[i] = dir[i] + "f,:";
                        direction += dir[i];
                    }
                    continue;
                }
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    distance = SetDistance(str);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    type = st;
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    radius = st;
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    max_power = st;
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    offline_interactive_radius = st;
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    artefact_spawn_count = st;
                    continue;
                }
            }
            string resultString = "";
            resultString += '&' + section_name + '&' + ',' + ':';
            resultString += '&' + name + '&' + ',' + ':';
            resultString += position;
            resultString += direction;
            resultString += distance + 'f' + ',' + ':';
            resultString += '&' + type + '&' + ',' + ':';
            resultString += radius + 'f' + ',' + ':';
            resultString += max_power + 'f' + ',' + ':';
            resultString += offline_interactive_radius + 'f' + ',' + ':';
            resultString += artefact_spawn_count + 'f' + ',' + ':';
            return resultString;
        }
        /// <summary>
        /// Анализ взрывающихся объектов
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="object_name"></param>
        public static string B_Base_Explosive(List<string> objects, string object_name)
        {
            string section_name = "";
            string name = "";
            string position = "";
            string direction = "";
            string visual_name = "";
            string item = "";
            string mass = "";
            foreach (string str in objects)
            {
                if (str.Contains("section_name = " + object_name))
                {
                    section_name = SectionName(str);
                    continue;
                }
                if (str.Contains("name") && !str.Contains("upd:") && !str.Contains("skeleton_name") && !str.Contains("visual_name"))
                {
                    name = ObjectName(str);
                    continue;
                }
                if (str.Contains("position") && !str.Contains("upd:"))
                {
                    string st = str.Replace("position = ", "");
                    string[] pos = st.Split(',');
                    for (int i = 0; i < pos.Length; i++)
                    {
                        pos[i] = pos[i] + "f,:";
                        position += pos[i];
                    }
                    continue;
                }
                if (str.Contains("direction") && !str.Contains("upd:"))
                {
                    string st = str.Replace("direction = ", "");
                    string[] dir = st.Split(',');
                    for (int i = 0; i < dir.Length; i++)
                    {
                        dir[i] = dir[i] + "f,:";
                        direction += dir[i];
                    }
                    continue;
                }
                if (str.Contains("visual_name") && !str.Contains("upd:"))
                {
                    visual_name = SetVisualName(str);
                    continue;
                }
                if (str.Contains("mass") && !str.Contains("upd:"))
                {
                    string st = str.Replace("mass = ", "");
                    mass = st;
                    continue;
                }
                if (str.Contains("items") && !str.Contains("upd:"))
                {
                    string st = str.Replace("items = ", "");
                    item = st;
                    continue;
                }
            }
            string resultString = "";
            resultString += '&' + visual_name + '&' + ',' + ':';
            resultString += '&' + section_name + '&' + ',' + ':';
            resultString += '&' + name + '&' + ',' + ':';
            resultString += position;
            resultString += direction;
            item = item.Replace(",", ";");
            if (mass == "") mass = "10";
            if (item == "") item = "empty";
            resultString += mass + 'f' + ',' + ':';
            resultString += '&' + item + '&' + ',' + ':';
            return resultString;
        }
    }
}
