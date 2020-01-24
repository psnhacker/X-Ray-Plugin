using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

namespace A_Life_converter
{
    class Program
    {
        const string applicationName = "A-Life converter.exe";
        private static List<string> str = new List<string>();
        private static List<string> items = new List<string>();
        private static List<string> stalker = new List<string>();
        private static List<string> monster = new List<string>();
        private static List<string> anomaly = new List<string>();
        private static List<string> explosive = new List<string>();
        private static List<string> physic_object = new List<string>();
        private static List<string> lights_hanging_lamp = new List<string>();
        private static List<string> physic_destroyable_object = new List<string>();


        static void Main(string[] args)
        {
            ObjectReplace();
        }

        //Разделение строк по объектам
        static void ObjectReplace()
        {
            Console.WriteLine("Укажите имя файла"); 
            string fileName = Console.ReadLine();
            if (!fileName.Contains(".ltx")) fileName += ".ltx"; //проверяем наличие расширения в имени (корректируем по необходимости)
            string fileNameNotxt = fileName.Replace(".ltx", "");
            string applicationPath = Assembly.GetExecutingAssembly().Location; //получение пути
            applicationPath = applicationPath.Replace("A-Life converter.exe", "");
            string pathOutFile = applicationPath + fileNameNotxt;
            string path = applicationPath + fileName;
            Console.WriteLine(@path);
            List<string> textFile = new List<string>();
            if (File.Exists(@path)) 
            {
                string[] allText = File.ReadAllLines(@path); //считывание файла построчно в массив строк
                textFile.Clear();
                foreach (string st in allText) textFile.Add(st);
            }
            else
            {
                Console.WriteLine("Файл не существует!");
                Environment.Exit(0); //Выход из приложения
            }
            Console.WriteLine("Файл загружен успешно");

            foreach (string st in textFile)
            {
                if (st.Contains("[") && st.Contains("]"))
                {
                    if (st.Contains("0") || st.Contains("1") || st.Contains("2") || st.Contains("3") || st.Contains("4") || st.Contains("5") || st.Contains("6") || st.Contains("7") || st.Contains("8") || st.Contains("9")
                        && !st.Contains("[ph_sound1]") && !st.Contains("@")
                        )
                    {
                        DataConverter(str);
                        str.Clear();
                        continue;
                    }
                    else
                    {
                        str.Add(st);
                        continue;
                    }
                }
                else
                {
                    str.Add(st);
                    continue;
                }
            }


            Console.WriteLine("Строки преобразованы");
            Console.WriteLine("Нажмите Enter для сохранения классов в файлы");
            Console.ReadKey();
            DataSave(pathOutFile);
            Console.WriteLine("Файлы сохранены!");
            Console.WriteLine("Через несколько секунд файлы появятся в папке приложения");
            Console.ReadKey();

        }


        static void DataSave(string savePath)
        {
            string path = savePath + "_";
            string[] m_physic_object = physic_object.ToArray();
            string[] m_lights_hanging_lamp = lights_hanging_lamp.ToArray();
            string[] m_items = items.ToArray();
            string[] m_stalker = stalker.ToArray();
            string[] m_monster = monster.ToArray();
            string[] m_anomaly = anomaly.ToArray();
            string[] m_explosive = explosive.ToArray();
            physic_destroyable_object.Sort();
            string[] m_physic_destroyable_object = physic_destroyable_object.ToArray();
            Array.Sort(m_physic_destroyable_object);

            File.AppendAllLines(@path + "physic_object.txt", m_physic_object);
            File.AppendAllLines(@path + "lights_hanging_lamp.txt", m_lights_hanging_lamp);
            File.AppendAllLines(@path + "items.txt", m_items);                         //.xlsx 
            File.AppendAllLines(@path + "stalker.txt", m_stalker);
            File.AppendAllLines(@path + "monster.txt", m_monster);                     //.xlsx
            File.AppendAllLines(@path + "anomaly.txt", m_anomaly);                     //.xlsx
            File.AppendAllLines(@path + "explosive.txt", m_explosive);
            File.AppendAllLines(@path + "physic_destroyable_object.txt", m_explosive); //.xlsx
        }

        static void DataConverter(List<string> data)
        {
            foreach (string str in data) if (str.Contains("section_name = physic_object")) Physic_object(data);
            foreach (string str in data) if (str.Contains("section_name = lights_hanging_lamp")) Lights_hanging_lamp(data);
            foreach (string str in data) if (str.Contains("section_name = m_trader")) M_trader(data);
            foreach (string str in data) if (str.Contains("section_name = space_restrictor")) Space_restrictor(data);
            foreach (string str in data) if (str.Contains("section_name = m_flesh_e")) M_flesh_e(data);
            foreach (string str in data) if (str.Contains("section_name = stalker")) C_Base_Stalker(data, "stalker");
            foreach (string str in data) if (str.Contains("section_name = smart_terrain")) Smart_terrain(data);
            foreach (string str in data) if (str.Contains("section_name = helicopter")) Helicopter(data);
            foreach (string str in data) if (str.Contains("section_name = physic_destroyable_object")) Physic_destroyable_object(data);
            foreach (string str in data) if (str.Contains("section_name = level_changer")) Level_changer(data);
            foreach (string str in data) if (str.Contains("section_name = m_crow")) M_crow(data);
            foreach (string str in data) if (str.Contains("section_name = respawn")) Respawn(data);
            foreach (string str in data) if (str.Contains("section_name = inventory_box")) Inventory_box(data);



            foreach (string str in data) if (str.Contains("section_name = stalker"))                    C_Base_Stalker(data, "stalker");

            foreach (string str in data) if (str.Contains("section_name = dog_weak"))                   C_Base_Monster(data, "dog_weak");
            foreach (string str in data) if (str.Contains("section_name = boar_weak"))                  C_Base_Monster(data, "boar_weak");
            foreach (string str in data) if (str.Contains("section_name = flesh_weak"))                 C_Base_Monster(data, "flesh_weak");
            foreach (string str in data) if (str.Contains("section_name = dog_strong"))                 C_Base_Monster(data, "dog_strong");
            foreach (string str in data) if (str.Contains("section_name = dog_normal"))                 C_Base_Monster(data, "dog_normal");
            foreach (string str in data) if (str.Contains("section_name = pseudodog_weak"))             C_Base_Monster(data, "pseudodog_weak");
            foreach (string str in data) if (str.Contains("section_name = boar_normal"))                C_Base_Monster(data, "boar_normal");
            foreach (string str in data) if (str.Contains("section_name = bloodsucker_normal"))         C_Base_Monster(data, "bloodsucker_normal");
            foreach (string str in data) if (str.Contains("section_name = bloodsucker_strong"))         C_Base_Monster(data, "bloodsucker_strong");
            foreach (string str in data) if (str.Contains("section_name = boar_strong"))                C_Base_Monster(data, "boar_strong");
            foreach (string str in data) if (str.Contains("section_name = flesh_normal"))               C_Base_Monster(data, "flesh_normal");
            foreach (string str in data) if (str.Contains("section_name = gigant_normal"))              C_Base_Monster(data, "gigant_normal");
            foreach (string str in data) if (str.Contains("section_name = gigant_strong"))              C_Base_Monster(data, "gigant_strong");
            foreach (string str in data) if (str.Contains("section_name = snork_jumper"))               C_Base_Monster(data, "snork_jumper");
            foreach (string str in data) if (str.Contains("section_name = snork_normal"))               C_Base_Monster(data, "snork_normal");
            foreach (string str in data) if (str.Contains("section_name = snork_outdoor"))              C_Base_Monster(data, "snork_outdoor");
            foreach (string str in data) if (str.Contains("section_name = snork_strong"))               C_Base_Monster(data, "snork_strong");
            foreach (string str in data) if (str.Contains("section_name = snork_weak"))                 C_Base_Monster(data, "snork_weak");
            foreach (string str in data) if (str.Contains("section_name = psy_dog"))                    C_Base_Monster(data, "psy_dog");
            foreach (string str in data) if (str.Contains("section_name = psy_dog_radar"))              C_Base_Monster(data, "psy_dog_radar");
            foreach (string str in data) if (str.Contains("section_name = tushkano_normal"))            C_Base_Monster(data, "tushkano_normal");
            foreach (string str in data) if (str.Contains("section_name = pseudodog_normal"))           C_Base_Monster(data, "pseudodog_normal");
            foreach (string str in data) if (str.Contains("section_name = pseudodog_strong"))           C_Base_Monster(data, "pseudodog_strong");
            foreach (string str in data) if (str.Contains("section_name = gunslinger_flash"))           C_Base_Monster(data, "gunslinger_flash");
            foreach (string str in data) if (str.Contains("section_name = controller_tubeman"))         C_Base_Monster(data, "controller_tubeman");
            foreach (string str in data) if (str.Contains("section_name = bread"))                      C_Base_Item(data, "bread");
            foreach (string str in data) if (str.Contains("section_name = af_ameba_mica"))              C_Base_Item(data, "af_ameba_mica");
            foreach (string str in data) if (str.Contains("section_name = af_ameba_slug"))              C_Base_Item(data, "af_ameba_slug");
            foreach (string str in data) if (str.Contains("section_name = af_dummy_glassbeads"))        C_Base_Item(data, "af_dummy_glassbeads");
            foreach (string str in data) if (str.Contains("section_name = af_dummy_pellicle"))          C_Base_Item(data, "af_dummy_pellicle");
            foreach (string str in data) if (str.Contains("section_name = af_dummy_spring"))            C_Base_Item(data, "af_dummy_spring");
            foreach (string str in data) if (str.Contains("section_name = af_electra_moonlight"))       C_Base_Item(data, "af_electra_moonlight");
            foreach (string str in data) if (str.Contains("section_name = af_fireball"))                C_Base_Item(data, "af_fireball");
            foreach (string str in data) if (str.Contains("section_name = af_fuzz_kolobok"))            C_Base_Item(data, "af_fuzz_kolobok");
            foreach (string str in data) if (str.Contains("section_name = af_gold_fish"))               C_Base_Item(data, "af_gold_fish");
            foreach (string str in data) if (str.Contains("section_name = af_night_star"))              C_Base_Item(data, "af_night_star");
            foreach (string str in data) if (str.Contains("section_name = af_rusty_sea-urchin"))        C_Base_Item(data, "af_rusty_sea-urchin");
            foreach (string str in data) if (str.Contains("section_name = ammo_11.43x23_hydro"))        C_Base_Item(data, "ammo_11.43x23_hydro");
            foreach (string str in data) if (str.Contains("section_name = ammo_12x76_dart"))            C_Base_Item(data, "ammo_12x76_dart");
            foreach (string str in data) if (str.Contains("section_name = ammo_12x76_zhekan"))          C_Base_Item(data, "ammo_12x76_zhekan");
            foreach (string str in data) if (str.Contains("section_name = ammo_5.45x39_ap"))            C_Base_Item(data, "ammo_5.45x39_ap");
            foreach (string str in data) if (str.Contains("section_name = ammo_5.56x45_ap"))            C_Base_Item(data, "ammo_5.56x45_ap");
            foreach (string str in data) if (str.Contains("section_name = ammo_5.56x45_ss190"))         C_Base_Item(data, "ammo_5.56x45_ss190");
            foreach (string str in data) if (str.Contains("section_name = ammo_9x19_pbp"))              C_Base_Item(data, "ammo_9x19_pbp");
            foreach (string str in data) if (str.Contains("section_name = ammo_9x39_pab9"))             C_Base_Item(data, "ammo_9x39_pab9");
            foreach (string str in data) if (str.Contains("section_name = ammo_og-7b"))                 C_Base_Item(data, "ammo_og-7b");
            foreach (string str in data) if (str.Contains("section_name = ammo_vog-25"))                C_Base_Item(data, "ammo_vog-25");
            foreach (string str in data) if (str.Contains("section_name = bread_a"))                    C_Base_Item(data, "bread_a");
            foreach (string str in data) if (str.Contains("section_name = dolg_outfit"))                C_Base_Item(data, "dolg_outfit");
            foreach (string str in data) if (str.Contains("section_name = exo_outfit"))                 C_Base_Item(data, "exo_outfit");
            foreach (string str in data) if (str.Contains("section_name = decoder"))                    C_Base_Item(data, "decoder");
            foreach (string str in data) if (str.Contains("section_name = military_outfit"))            C_Base_Item(data, "military_outfit");
            foreach (string str in data) if (str.Contains("section_name = monolit_outfit"))             C_Base_Item(data, "monolit_outfit");
            foreach (string str in data) if (str.Contains("section_name = medkit_scientic"))            C_Base_Item(data, "medkit_scientic");
            foreach (string str in data) if (str.Contains("section_name = wpn_abakan"))                 C_Base_Item(data, "wpn_abakan");
            foreach (string str in data) if (str.Contains("section_name = wpn_ak74"))                   C_Base_Item(data, "wpn_ak74");
            foreach (string str in data) if (str.Contains("section_name = wpn_ak74_m1"))                C_Base_Item(data, "wpn_ak74_m1");
            foreach (string str in data) if (str.Contains("section_name = wpn_binoc"))                  C_Base_Item(data, "wpn_binoc");
            foreach (string str in data) if (str.Contains("section_name = wpn_groza"))                  C_Base_Item(data, "wpn_groza");
            foreach (string str in data) if (str.Contains("section_name = wpn_lr300"))                  C_Base_Item(data, "wpn_lr300");
            foreach (string str in data) if (str.Contains("section_name = wpn_lr300_m1"))               C_Base_Item(data, "wpn_lr300_m1");
            foreach (string str in data) if (str.Contains("section_name = wpn_mp5"))                    C_Base_Item(data, "wpn_mp5");
            foreach (string str in data) if (str.Contains("section_name = wpn_rg-6"))                   C_Base_Item(data, "wpn_rg-6");
            foreach (string str in data) if (str.Contains("section_name = wpn_rpg7"))                   C_Base_Item(data, "wpn_rpg7");
            foreach (string str in data) if (str.Contains("section_name = wpn_vintorez"))               C_Base_Item(data, "wpn_vintorez");
            foreach (string str in data) if (str.Contains("section_name = guitar_a"))                   C_Base_Item(data, "guitar_a");
            foreach (string str in data) if (str.Contains("section_name = pri_decoder_documents"))      C_Base_Item(data, "pri_decoder_documents");
            foreach (string str in data) if (str.Contains("section_name = svoboda_heavy_outfit"))       C_Base_Item(data, "svoboda_heavy_outfit");
            foreach (string str in data) if (str.Contains("section_name = svoboda_light_outfit"))       C_Base_Item(data, "svoboda_light_outfit");
            foreach (string str in data) if (str.Contains("section_name = hunters_toz"))                C_Base_Item(data, "hunters_toz");
            foreach (string str in data) if (str.Contains("section_name = specops_outfit"))             C_Base_Item(data, "specops_outfit");
            foreach (string str in data) if (str.Contains("section_name = stalker_outfit"))             C_Base_Item(data, "stalker_outfit");
            foreach (string str in data) if (str.Contains("section_name = grenade_rgd5"))               C_Base_Item(data, "grenade_rgd5");
            foreach (string str in data) if (str.Contains("section_name = killer_outfit"))              C_Base_Item(data, "killer_outfit");
            foreach (string str in data) if (str.Contains("section_name = hand_radio"))                 C_Base_Item(data, "hand_radio");
            foreach (string str in data) if (str.Contains("section_name = quest_case_02"))              C_Base_Item(data, "quest_case_02");
            foreach (string str in data) if (str.Contains("section_name = dar_document4"))              C_Base_Item(data, "dar_document4");
            foreach (string str in data) if (str.Contains("section_name = ammo_9x19_fmj"))              C_Base_Item(data, "ammo_9x19_fmj");
            foreach (string str in data) if (str.Contains("section_name = bandit_outfit"))              C_Base_Item(data, "bandit_outfit");
            foreach (string str in data) if (str.Contains("section_name = wpn_walther"))                C_Base_Item(data, "wpn_walther");
            foreach (string str in data) if (str.Contains("section_name = medkit_army"))                C_Base_Item(data, "medkit_army");
            foreach (string str in data) if (str.Contains("section_name = quest_case_01"))              C_Base_Item(data, "quest_case_01");
            foreach (string str in data) if (str.Contains("section_name = conserva"))                   C_Base_Item(data, "conserva");
            foreach (string str in data) if (str.Contains("section_name = vodka"))                      C_Base_Item(data, "vodka");
            foreach (string str in data) if (str.Contains("section_name = energy_drink"))               C_Base_Item(data, "energy_drink");
            foreach (string str in data) if (str.Contains("section_name = kolbasa"))                    C_Base_Item(data, "kolbasa");
            foreach (string str in data) if (str.Contains("section_name = wpn_ak74u"))                  C_Base_Item(data, "wpn_ak74u");
            foreach (string str in data) if (str.Contains("section_name = ammo_9x18_fmj"))              C_Base_Item(data, "ammo_9x18_fmj");
            foreach (string str in data) if (str.Contains("section_name = wpn_pm"))                     C_Base_Item(data, "wpn_pm");
            foreach (string str in data) if (str.Contains("section_name = ammo_9x18_pmm"))              C_Base_Item(data, "ammo_9x18_pmm");
            foreach (string str in data) if (str.Contains("section_name = bandage"))                    C_Base_Item(data, "bandage");
            foreach (string str in data) if (str.Contains("section_name = af_blood"))                   C_Base_Item(data, "af_blood");
            foreach (string str in data) if (str.Contains("section_name = wpn_bm16"))                   C_Base_Item(data, "wpn_bm16");
            foreach (string str in data) if (str.Contains("section_name = ammo_12x70_buck"))            C_Base_Item(data, "ammo_12x70_buck");
            foreach (string str in data) if (str.Contains("section_name = af_medusa"))                  C_Base_Item(data, "af_medusa");
            foreach (string str in data) if (str.Contains("section_name = outfit_bandit_m1"))           C_Base_Item(data, "outfit_bandit_m1");
            foreach (string str in data) if (str.Contains("section_name = af_electra_flash"))           C_Base_Item(data, "af_electra_flash");
            foreach (string str in data) if (str.Contains("section_name = af_cristall_flower"))         C_Base_Item(data, "af_cristall_flower");
            foreach (string str in data) if (str.Contains("section_name = af_electra_sparkler"))        C_Base_Item(data, "af_electra_sparkler");
            foreach (string str in data) if (str.Contains("section_name = af_dummy_battery"))           C_Base_Item(data, "af_dummy_battery");
            foreach (string str in data) if (str.Contains("section_name = af_dummy_dummy"))             C_Base_Item(data, "af_dummy_dummy");
            foreach (string str in data) if (str.Contains("section_name = af_gravi"))                   C_Base_Item(data, "af_gravi");
            foreach (string str in data) if (str.Contains("section_name = af_mincer_meat"))             C_Base_Item(data, "af_mincer_meat");
            foreach (string str in data) if (str.Contains("section_name = af_vyvert"))                  C_Base_Item(data, "af_vyvert");
            foreach (string str in data) if (str.Contains("section_name = ammo_11.43x23_fmj"))          C_Base_Item(data, "ammo_11.43x23_fmj");
            foreach (string str in data) if (str.Contains("section_name = ammo_5.45x39_fmj"))           C_Base_Item(data, "ammo_5.45x39_fmj");
            foreach (string str in data) if (str.Contains("section_name = antirad"))                    C_Base_Item(data, "antirad");
            foreach (string str in data) if (str.Contains("section_name = grenade_f1"))                 C_Base_Item(data, "grenade_f1");

            foreach (string str in data) if (str.Contains("section_name = explosive_barrel"))           C_Base_Explosive(data, "explosive_barrel");
            foreach (string str in data) if (str.Contains("section_name = explosive_mobiltank"))        C_Base_Explosive(data, "explosive_mobiltank");
            foreach (string str in data) if (str.Contains("section_name = explosive_barrel_low"))       C_Base_Explosive(data, "explosive_barrel_low");
            foreach (string str in data) if (str.Contains("section_name = explosive_dinamit"))          C_Base_Explosive(data, "explosive_dinamit");
            foreach (string str in data) if (str.Contains("section_name = explosive_fuelcan"))          C_Base_Explosive(data, "explosive_fuelcan");
            foreach (string str in data) if (str.Contains("section_name = explosive_tank"))             C_Base_Explosive(data, "explosive_tank");


            foreach (string str in data) if (str.Contains("section_name = zone_mosquito_bald_weak"))            C_Base_Anomaly_Zone(data, "zone_mosquito_bald_weak");
            foreach (string str in data) if (str.Contains("section_name = zone_radioactive_killing"))           C_Base_Anomaly_Zone(data, "zone_radioactive_killing");
            foreach (string str in data) if (str.Contains("section_name = zone_mincer_weak_noart"))             C_Base_Anomaly_Zone(data, "zone_mincer_weak_noart");
            foreach (string str in data) if (str.Contains("section_name = zone_mosquito_bald_weak_noart"))      C_Base_Anomaly_Zone(data, "zone_mosquito_bald_weak_noart");
            foreach (string str in data) if (str.Contains("section_name = zone_flame_small"))                   C_Base_Anomaly_Zone(data, "zone_flame_small");
            foreach (string str in data) if (str.Contains("section_name = zone_radioactive_average"))           C_Base_Anomaly_Zone(data, "zone_radioactive_average");
            foreach (string str in data) if (str.Contains("section_name = zone_gravi_Zone_average"))            C_Base_Anomaly_Zone(data, "zone_gravi_Zone_average");
            foreach (string str in data) if (str.Contains("section_name = zone_gravi_Zone_weak"))               C_Base_Anomaly_Zone(data, "zone_gravi_Zone_weak");
            foreach (string str in data) if (str.Contains("section_name = zone_mincer_average"))                C_Base_Anomaly_Zone(data, "zone_mincer_average");
            foreach (string str in data) if (str.Contains("section_name = zone_mincer_strong"))                 C_Base_Anomaly_Zone(data, "zone_mincer_strong");
            foreach (string str in data) if (str.Contains("section_name = zone_mincer_weak"))                   C_Base_Anomaly_Zone(data, "zone_mincer_weak");
            foreach (string str in data) if (str.Contains("section_name = zone_mosquito_bald_average"))         C_Base_Anomaly_Zone(data, "zone_mosquito_bald_average");
            foreach (string str in data) if (str.Contains("section_name = zone_radioactive_strong"))            C_Base_Anomaly_Zone(data, "zone_radioactive_strong");
            foreach (string str in data) if (str.Contains("section_name = zone_radioactive_weak"))              C_Base_Anomaly_Zone(data, "zone_radioactive_weak");
            foreach (string str in data) if (str.Contains("section_name = zone_mine_field"))                    C_Base_Anomaly_Zone(data, "zone_mine_field");
            foreach (string str in data) if (str.Contains("section_name = zone_witches_galantine"))             C_Base_Anomaly_Zone(data, "zone_witches_galantine");
            foreach (string str in data) if (str.Contains("section_name = zone_burning_fuzz_strong"))           C_Base_Anomaly_Zone(data, "zone_burning_fuzz_strong");
            foreach (string str in data) if (str.Contains("section_name = zone_burning_fuzz1"))                 C_Base_Anomaly_Zone(data, "zone_burning_fuzz1");
            foreach (string str in data) if (str.Contains("section_name = zone_buzz"))                          C_Base_Anomaly_Zone(data, "zone_buzz");
            foreach (string str in data) if (str.Contains("section_name = zone_buzz_average"))                  C_Base_Anomaly_Zone(data, "zone_buzz_average");
            foreach (string str in data) if (str.Contains("section_name = zone_campfire_grill"))                C_Base_Anomaly_Zone(data, "zone_campfire_grill");
            foreach (string str in data) if (str.Contains("section_name = zone_emi"))                           C_Base_Anomaly_Zone(data, "zone_emi");
            foreach (string str in data) if (str.Contains("section_name = zone_flame"))                         C_Base_Anomaly_Zone(data, "zone_flame");
            foreach (string str in data) if (str.Contains("section_name = zone_gravi_zone"))                    C_Base_Anomaly_Zone(data, "zone_gravi_zone");
            foreach (string str in data) if (str.Contains("section_name = zone_gravi_Zone_strong"))             C_Base_Anomaly_Zone(data, "zone_gravi_Zone_strong");
            foreach (string str in data) if (str.Contains("section_name = zone_mincer"))                        C_Base_Anomaly_Zone(data, "zone_mincer");
            foreach (string str in data) if (str.Contains("section_name = zone_monolith"))                      C_Base_Anomaly_Zone(data, "zone_monolith");
            foreach (string str in data) if (str.Contains("section_name = zone_mosquito_bald"))                 C_Base_Anomaly_Zone(data, "zone_mosquito_bald");
            foreach (string str in data) if (str.Contains("section_name = zone_mosquito_bald_strong"))          C_Base_Anomaly_Zone(data, "zone_mosquito_bald_strong");
            foreach (string str in data) if (str.Contains("section_name = zone_mosquito_bald_strong_noart"))    C_Base_Anomaly_Zone(data, "zone_mosquito_bald_strong_noart");
            foreach (string str in data) if (str.Contains("section_name = zone_no_gravity"))                    C_Base_Anomaly_Zone(data, "zone_no_gravity");
            foreach (string str in data) if (str.Contains("section_name = zone_radioactive"))                   C_Base_Anomaly_Zone(data, "zone_radioactive");
            foreach (string str in data) if (str.Contains("section_name = zone_teleport"))                      C_Base_Anomaly_Zone(data, "zone_teleport");
            foreach (string str in data) if (str.Contains("section_name = zone_witches_galantine_average"))     C_Base_Anomaly_Zone(data, "zone_witches_galantine_average");
            foreach (string str in data) if (str.Contains("section_name = zone_witches_galantine_strong"))      C_Base_Anomaly_Zone(data, "zone_witches_galantine_strong");
            foreach (string str in data) if (str.Contains("section_name = zone_zhar"))                          C_Base_Anomaly_Zone(data, "zone_zhar");
            foreach (string str in data) if (str.Contains("section_name = zone_zharka_static_average"))         C_Base_Anomaly_Zone(data, "zone_zharka_static_average");
            foreach (string str in data) if (str.Contains("section_name = zone_zharka_static_strong"))          C_Base_Anomaly_Zone(data, "zone_zharka_static_strong");
            foreach (string str in data) if (str.Contains("section_name = zone_zharka_static_weak"))            C_Base_Anomaly_Zone(data, "zone_zharka_static_weak");
            foreach (string str in data) if (str.Contains("section_name = fireball_zone"))                      C_Base_Anomaly_Zone(data, "fireball_zone");

            foreach (string str in data) if (str.Contains("section_name = medkit") && !str.Contains("section_name = medkit_army")) C_Base_Item(data, "medkit");
        }

        //physic_object
        static void Physic_object(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = physic_object"))
                {
                    string st = str.Replace("section_name = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("name") && !str.Contains("upd:"))
                {
                    string st = str.Replace("name = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("position") && !str.Contains("upd:"))
                {
                    string st = str.Replace("position = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("direction") && !str.Contains("upd:"))
                {
                    string st = str.Replace("direction = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("visual_name") && !str.Contains("upd:"))
                {
                    string st = str.Replace("visual_name = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("mass") && !str.Contains("upd:"))
                {
                    string st = str.Replace("mass = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            physic_object.Add(resultString);
        }
        //lights_hanging_lamp
        static void Lights_hanging_lamp(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = lights_hanging_lamp"))
                {
                    string st = str.Replace("section_name = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("name") && !str.Contains("upd:"))
                {
                    string st = str.Replace("name = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("position") && !str.Contains("upd:"))
                {
                    string st = str.Replace("position = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("direction") && !str.Contains("upd:"))
                {
                    string st = str.Replace("direction = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("visual_name") && !str.Contains("upd:"))
                {
                    string st = str.Replace("visual_name = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("ambient_radius"))
                {
                    string st = str.Replace("ambient_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("main_cone_angle"))
                {
                    string st = str.Replace("main_cone_angle = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("glow_texture"))
                {
                    string st = str.Replace("glow_texture = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            lights_hanging_lamp.Add(resultString);
        }
        
        /// <summary>
        /// Анализ сталкеров
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="stalker_name"></param>
        static void C_Base_Stalker(List<string> objects, string stalker_name)
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
            stalker.Add(resultString);
        }

        /// <summary>
        /// Анализ мутантов 
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="mutant_name"></param>
        static void C_Base_Monster(List<string> objects, string mutant_name)
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
            monster.Add(resultString);
        }

        /// <summary>
        /// Анализ Item'ов
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="item_name"></param>
        static void C_Base_Item(List<string> objects, string item_name)
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
            items.Add(resultString);
        }

        /// <summary>
        /// Анализ аномальных зон
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="anomaly_name"></param>
        static void C_Base_Anomaly_Zone(List<string> objects, string anomaly_name)
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
            anomaly.Add(resultString);
        }

        /// <summary>
        /// Анализ взрывающихся объектов
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="object_name"></param>
        static void C_Base_Explosive(List<string> objects, string object_name)
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
            explosive.Add(resultString);
        }

        

        /// <summary>
        /// Анализ разрушаемых объектов и сюрпрайз боксов
        /// </summary>
        /// <param name="objects">Список строк в 1 блоке</param>
        static void Physic_destroyable_object(List<string> objects)
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
                if (str.Contains("section_name = physic_destroyable_object"))
                {
                    section_name = SectionName(str);
                    continue;
                }
                if (str.Contains("name") && !str.Contains("upd:") && !str.Contains("skeleton_name") && !str.Contains("visual_name"))
                {
                    string st = str.Replace("name = ", "");
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
            explosive.Add(resultString);
        }


        /// <summary>
        /// Возвращает visual name
        /// </summary>
        /// <param name="item">Строка</param>
        /// <returns></returns>
        //static string SetVisualName(string item)
        //{
        //    item = item.Replace("visual_name = ", "");
        //    //item = item.Replace("equipments\\", "");
        //    //item = item.Replace("food\\", "");
        //    //item = item.Replace("monitemers\\dog\\", "");
        //    //item = item.Replace("monitemers\\flesh\\", "");
        //    //item = item.Replace("monitemers\\mutant_boar\\", "");
        //    //item = item.Replace("monitemers\\pseudodog\\", "");
        //    //item = item.Replace("physics\\anomaly\\", "");
        //    //item = item.Replace("physics\\balon\\", "");
        //    //item = item.Replace("physics\\box\\", "");
        //    //item = item.Replace("physics\\decor\\", "");
        //    //item = item.Replace("physics\\door\\", "");
        //    //item = item.Replace("physics\\small_trash\\", "");
        //    //item = item.Replace("visual_physics\\balon\\", "");
        //    //item = item.Replace("weapons\\ak-74u\\", "");
        //    //item = item.Replace("weapons\\ammo\\", "");
        //    //item = item.Replace("weapons\\bm_16\\", "");
        //    //item = item.Replace("weapons\\bred\\", "");
        //    //item = item.Replace("weapons\\kolbasa\\", "");
        //    //item = item.Replace("weapons\\pm\\", "");
        //    //item = item.Replace("Weapons\\vodka\\", "");
        //    //item = item.Replace("weapons\\walter_99\\", "");
        //    return item;
        //}

        static string SetVisualName(string item)
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
        /// <summary>
        /// Возвращает имя объекта
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        static string ObjectName(string item)
        {
            return item = item.Replace("name = ", "");
        }
        /// <summary>
        /// Возвращает имя секции
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        static string SectionName(string item)
        {
            return item = item.Replace("section_name = ", "");
        }
        static string SetDistance(string item)
        {
            return item = item.Replace("distance = ", "");
        }
        static string SetHealth(string item)
        {
            return item = item.Replace("health = ", "");
        }
        static string SetCharacter(string item)
        {
            return item = item.Replace("money = ", "");
        }
        static string GetItems(List<string> items)
        {
            string data = "";
            foreach(string item in items) data += item + ';';
            return data;
        }

        //all other classes....????
        static void Inventory_box(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = inventory_box"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("name") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("position") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("direction") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("cfg") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("visual_name") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
            }


        }
        static void M_trader(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = m_trader"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("name") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("position") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("direction") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("cfg"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("visual_name") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("money") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }

            }


        }
        static void Space_restrictor(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = space_restrictor"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("name") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("position") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("direction") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
            }


        }
        static void M_flesh_e(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = m_flesh_e"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("name") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("position") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("direction") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("visual_name") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("health") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
            }


        }
        static void Smart_terrain(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = smart_terrain"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("name") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("position") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("direction") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }

            }


        }
        static void Helicopter(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = helicopter"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("name") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("position") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("direction") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("visual_name") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("motion_name") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("engine_sound") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
            }


        }

        static void Level_changer(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = level_changer"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("name") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("position") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("direction") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("dest_level_name") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
            }


        }
        static void M_crow(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = m_crow"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("name") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("position") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("direction") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("visual_name") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("health") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
            }


        }
        static void Respawn(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = respawn"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("name") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("position") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("direction") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
                if (str.Contains("respawn_section") && !str.Contains("upd:"))
                {
                    resultData.Add(str);
                    continue;
                }
            }


        }
    }
}
