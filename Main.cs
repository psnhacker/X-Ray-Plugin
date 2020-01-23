using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
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
                    if (st.Contains("0") || st.Contains("1") || st.Contains("2") || st.Contains("3") || st.Contains("4") || st.Contains("5") || st.Contains("6") || st.Contains("7") || st.Contains("8") || st.Contains("9"))
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
            //Console.WriteLine("Нажмите Enter для вывода всех классов построчно");
            //Console.ReadKey();
            //foreach (string st1 in physic_object) Console.WriteLine(st1);
            //foreach (string st2 in lights_hanging_lamp) Console.WriteLine(st2);
            //foreach (string st3 in items) Console.WriteLine(st3);
            //foreach (string st4 in stalker) Console.WriteLine(st4);
            //foreach (string st5 in monster) Console.WriteLine(st5);
            //foreach (string st6 in anomaly) Console.WriteLine(st6);
            //foreach (string st7 in explosive) Console.WriteLine(st7);

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
            File.AppendAllLines(@path + "physic_destroyable_object.csv", m_explosive);

        }

        static void DataConverter(List<string> data)
        {
            foreach (string str in data) if (str.Contains("section_name = physic_object")) Physic_object(data);
            foreach (string str in data) if (str.Contains("section_name = lights_hanging_lamp")) Lights_hanging_lamp(data);
            foreach (string str in data) if (str.Contains("section_name = m_trader")) M_trader(data);
            foreach (string str in data) if (str.Contains("section_name = space_restrictor")) Space_restrictor(data);
            foreach (string str in data) if (str.Contains("section_name = m_flesh_e")) M_flesh_e(data);
            foreach (string str in data) if (str.Contains("section_name = stalker")) Stalker(data);
            foreach (string str in data) if (str.Contains("section_name = smart_terrain")) Smart_terrain(data);
            foreach (string str in data) if (str.Contains("section_name = helicopter")) Helicopter(data);
            foreach (string str in data) if (str.Contains("section_name = physic_destroyable_object")) Physic_destroyable_object(data);
            foreach (string str in data) if (str.Contains("section_name = level_changer")) Level_changer(data);
            foreach (string str in data) if (str.Contains("section_name = m_crow")) M_crow(data);
            foreach (string str in data) if (str.Contains("section_name = respawn")) Respawn(data);
            foreach (string str in data) if (str.Contains("section_name = inventory_box")) Inventory_box(data);

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


            foreach (string str in data) if (str.Contains("section_name = zone_mosquito_bald_weak")) Zone_mosquito_bald_weak(data);
            foreach (string str in data) if (str.Contains("section_name = zone_radioactive_killing")) Zone_radioactive_killing(data);
            foreach (string str in data) if (str.Contains("section_name = zone_mincer_weak_noart")) Zone_mincer_weak_noart(data);
            foreach (string str in data) if (str.Contains("section_name = zone_mosquito_bald_weak_noart")) Zone_mosquito_bald_weak_noart(data);
            foreach (string str in data) if (str.Contains("section_name = zone_flame_small")) Zone_flame_small(data);
            foreach (string str in data) if (str.Contains("section_name = zone_radioactive_average")) Zone_radioactive_average(data);
            foreach (string str in data) if (str.Contains("section_name = zone_gravi_Zone_average")) Zone_gravi_Zone_average(data);
            foreach (string str in data) if (str.Contains("section_name = zone_gravi_Zone_weak")) Zone_gravi_Zone_weak(data);
            foreach (string str in data) if (str.Contains("section_name = zone_mincer_average")) Zone_mincer_average(data);
            foreach (string str in data) if (str.Contains("section_name = zone_mincer_strong")) Zone_mincer_strong(data);
            foreach (string str in data) if (str.Contains("section_name = zone_mincer_weak")) Zone_mincer_weak(data);
            foreach (string str in data) if (str.Contains("section_name = zone_mosquito_bald_average")) Zone_mosquito_bald_average(data);
            foreach (string str in data) if (str.Contains("section_name = zone_radioactive_strong")) Zone_radioactive_strong(data);
            foreach (string str in data) if (str.Contains("section_name = zone_radioactive_weak")) Zone_radioactive_weak(data);
            foreach (string str in data) if (str.Contains("section_name = zone_mine_field")) Zone_mine_field(data);
            foreach (string str in data) if (str.Contains("section_name = zone_witches_galantine")) Zone_witches_galantine(data);
            foreach (string str in data) if (str.Contains("section_name = zone_burning_fuzz_strong")) Zone_burning_fuzz_strong(data);
            foreach (string str in data) if (str.Contains("section_name = zone_burning_fuzz1")) Zone_burning_fuzz1(data);
            foreach (string str in data) if (str.Contains("section_name = zone_buzz")) Zone_buzz(data);
            foreach (string str in data) if (str.Contains("section_name = zone_buzz_average")) Zone_buzz_average(data);
            foreach (string str in data) if (str.Contains("section_name = zone_campfire_grill")) Zone_campfire_grill(data);
            foreach (string str in data) if (str.Contains("section_name = zone_emi")) Zone_emi(data);
            foreach (string str in data) if (str.Contains("section_name = zone_flame")) Zone_flame(data);
            foreach (string str in data) if (str.Contains("section_name = zone_gravi_zone")) Zone_gravi_zone(data);
            foreach (string str in data) if (str.Contains("section_name = zone_gravi_Zone_strong")) Zone_gravi_Zone_strong(data);
            foreach (string str in data) if (str.Contains("section_name = zone_mincer")) Zone_mincer(data);
            foreach (string str in data) if (str.Contains("section_name = zone_monolith")) Zone_monolith(data);
            foreach (string str in data) if (str.Contains("section_name = zone_mosquito_bald")) Zone_mosquito_bald(data);
            foreach (string str in data) if (str.Contains("section_name = zone_mosquito_bald_strong")) Zone_mosquito_bald_strong(data);
            foreach (string str in data) if (str.Contains("section_name = zone_mosquito_bald_strong_noart")) Zone_mosquito_bald_strong_noart(data);
            foreach (string str in data) if (str.Contains("section_name = zone_no_gravity")) Zone_no_gravity(data);
            foreach (string str in data) if (str.Contains("section_name = zone_radioactive")) Zone_radioactive(data);
            foreach (string str in data) if (str.Contains("section_name = zone_teleport")) Zone_teleport(data);
            foreach (string str in data) if (str.Contains("section_name = zone_witches_galantine_average")) Zone_witches_galantine_average(data);
            foreach (string str in data) if (str.Contains("section_name = zone_witches_galantine_strong")) Zone_witches_galantine_strong(data);
            foreach (string str in data) if (str.Contains("section_name = zone_zhar")) Zone_zhar(data);
            foreach (string str in data) if (str.Contains("section_name = zone_zharka_static_average")) Zone_zharka_static_average(data);
            foreach (string str in data) if (str.Contains("section_name = zone_zharka_static_strong")) Zone_zharka_static_strong(data);
            foreach (string str in data) if (str.Contains("section_name = zone_zharka_static_weak")) Zone_zharka_static_weak(data);
            foreach (string str in data) if (str.Contains("section_name = fireball_zone")) Fireball_zone(data);



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
        //all npc
        static void Stalker(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = stalker"))
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
                if (str.Contains("money") && !str.Contains("upd:"))
                {
                    string st = str.Replace("money = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("visual_name") && !str.Contains("upd:"))
                {
                    string st = str.Replace("visual_name = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("health") && !str.Contains("upd:"))
                {
                    string st = str.Replace("health = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            stalker.Add(resultString);
        }


        //anomaly
        static void Zone_mine_field(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_mine_field"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_witches_galantine(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_witches_galantine"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_mincer_weak_noart(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_mincer_weak_noart"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_mosquito_bald_weak_noart(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_mosquito_bald_weak_noart"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_flame_small(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_flame_small"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_radioactive_average(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_radioactive_average"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_mosquito_bald_weak(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_mosquito_bald_weak"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_radioactive_killing(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_radioactive_killing"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_gravi_Zone_average(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_gravi_Zone_average"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_gravi_Zone_weak(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_gravi_Zone_weak"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_mincer_average(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_mincer_average"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_mincer_strong(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_mincer_strong"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_mincer_weak(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_mincer_weak"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_mosquito_bald_average(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_mosquito_bald_average"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_radioactive_strong(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_radioactive_strong"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_radioactive_weak(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_radioactive_weak"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_burning_fuzz_strong(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_burning_fuzz_strong"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_burning_fuzz1(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_burning_fuzz1"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_buzz(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_buzz"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_buzz_average(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_buzz_average"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_campfire_grill(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_campfire_grill"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_emi(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_emi"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_flame(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_flame"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_gravi_zone(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_gravi_zone"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_gravi_Zone_strong(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_gravi_Zone_strong"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_mincer(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_mincer"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_monolith(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_monolith"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_mosquito_bald(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_mosquito_bald"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_mosquito_bald_strong(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_mosquito_bald_strong"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_mosquito_bald_strong_noart(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_mosquito_bald_strong_noart"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_no_gravity(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_no_gravity"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_radioactive(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_radioactive"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_teleport(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_teleport"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_witches_galantine_average(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_witches_galantine_average"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_witches_galantine_strong(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_witches_galantine_strong"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_zhar(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_zhar"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_zharka_static_average(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_zharka_static_average"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_zharka_static_strong(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_zharka_static_strong"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Zone_zharka_static_weak(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = zone_zharka_static_weak"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
        }
        static void Fireball_zone(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = fireball_zone"))
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
                if (str.Contains("distance") && !str.Contains("upd:"))
                {
                    string st = str.Replace("distance = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:type") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:type = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:offset") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:offset = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("shape0:radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("shape0:radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("max_power") && !str.Contains("upd:"))
                {
                    string st = str.Replace("max_power = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("offline_interactive_radius") && !str.Contains("upd:"))
                {
                    string st = str.Replace("offline_interactive_radius = ", "");
                    resultData.Add(st);
                    continue;
                }
                if (str.Contains("artefact_spawn_count") && !str.Contains("upd:"))
                {
                    string st = str.Replace("artefact_spawn_count = ", "");
                    resultData.Add(st);
                    continue;
                }
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            anomaly.Add(resultString);
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
                    string st = str.Replace("section_name = ", "");
                    section_name = st;
                    continue;
                }
                if (str.Contains("name") && !str.Contains("upd:") && !str.Contains("skeleton_name") && !str.Contains("visual_name"))
                {
                    string st = str.Replace("name = ", "");
                    name = st;
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
                    string st = str.Replace("distance = ", "");
                    distance = st;
                    continue;
                }
                if (str.Contains("visual_name") && !str.Contains("upd:"))
                {
                    string st = str.Replace("visual_name = ", "");
                    st = st.Replace("monsters\\dog\\", "");
                    st = st.Replace("monsters\\flesh\\", "");
                    st = st.Replace("monsters\\mutant_boar\\", "");
                    st = st.Replace("monsters\\pseudodog\\", "");
                    visual_name = st;
                    continue;
                }
                if (str.Contains("health") && !str.Contains("upd:"))
                {
                    string st = str.Replace("health = ", "");
                    health = st;
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
                    string st = str.Replace("section_name = ", "");
                    section_name = st;
                    continue;
                }
                if (str.Contains("name") && !str.Contains("upd:") && !str.Contains("skeleton_name") && !str.Contains("visual_name"))
                {
                    string st = str.Replace("name = ", "");
                    name = st;
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
                    string st = str.Replace("visual_name = ", "");
                    st = st.Replace("food\\", "");
                    st = st.Replace("Weapons\\vodka\\", "");
                    st = st.Replace("equipments\\", "");
                    st = st.Replace("physics\\anomaly\\", "");
                    st = st.Replace("weapons\\ak-74u\\", "");
                    st = st.Replace("weapons\\kolbasa\\", "");
                    st = st.Replace("weapons\\ammo\\", "");
                    st = st.Replace("weapons\\bm_16\\", "");
                    st = st.Replace("weapons\\bred\\", "");
                    st = st.Replace("weapons\\pm\\", "");
                    st = st.Replace("weapons\\walter_99\\", "");
                    visual_name = st;
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
                    string st = str.Replace("section_name = ", "");
                    section_name = st;
                    continue;
                }
                if (str.Contains("name") && !str.Contains("upd:") && !str.Contains("skeleton_name") && !str.Contains("visual_name"))
                {
                    string st = str.Replace("name = ", "");
                    name = st;
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
                    string st = str.Replace("visual_name = ", "");
                    st = st.Replace("physics\\box\\", "");
                    st = st.Replace("physics\\decor\\", "");
                    st = st.Replace("physics\\door\\", "");
                    st = st.Replace("physics\\small_trash\\", "");
                    st = st.Replace("visual_physics\\balon\\", "");
                    st = st.Replace("physics\\balon\\", "");
                    visual_name = st;
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
                    string st = str.Replace("section_name = ", "");
                    section_name = st;
                    continue;
                }
                if (str.Contains("name") && !str.Contains("upd:") && !str.Contains("skeleton_name") && !str.Contains("visual_name"))
                {
                    string st = str.Replace("name = ", "");
                    name = st;
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
                    string st = str.Replace("visual_name = ", "");
                    st = st.Replace("physics\\box\\", "");
                    st = st.Replace("physics\\decor\\", "");
                    st = st.Replace("physics\\door\\", "");
                    st = st.Replace("physics\\small_trash\\", "");
                    st = st.Replace("visual_physics\\balon\\", "");
                    st = st.Replace("physics\\balon\\", "");
                    visual_name = st;
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
