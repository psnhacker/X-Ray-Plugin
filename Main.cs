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
            File.AppendAllLines(@path + "items.txt", m_items);
            File.AppendAllLines(@path + "stalker.txt", m_stalker);
            File.AppendAllLines(@path + "monster.txt", m_monster);
            File.AppendAllLines(@path + "anomaly.txt", m_anomaly);
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

            foreach (string str in data) if (str.Contains("section_name = dog_weak")) Dog_weak(data);
            foreach (string str in data) if (str.Contains("section_name = boar_weak")) Boar_weak(data);
            foreach (string str in data) if (str.Contains("section_name = flesh_weak")) Flesh_weak(data);
            foreach (string str in data) if (str.Contains("section_name = dog_strong")) Dog_strong(data);
            foreach (string str in data) if (str.Contains("section_name = dog_normal")) Dog_normal(data);
            foreach (string str in data) if (str.Contains("section_name = pseudodog_weak")) Pseudodog_weak(data);
            foreach (string str in data) if (str.Contains("section_name = boar_normal")) Boar_normal(data);
            foreach (string str in data) if (str.Contains("section_name = bloodsucker_normal")) Bloodsucker_normal(data);
            foreach (string str in data) if (str.Contains("section_name = bloodsucker_strong")) Bloodsucker_strong(data);
            foreach (string str in data) if (str.Contains("section_name = boar_strong")) Boar_strong(data);
            foreach (string str in data) if (str.Contains("section_name = flesh_normal")) Flesh_normal(data);
            foreach (string str in data) if (str.Contains("section_name = gigant_normal")) Gigant_normal(data);
            foreach (string str in data) if (str.Contains("section_name = gigant_strong")) Gigant_strong(data);
            foreach (string str in data) if (str.Contains("section_name = snork_jumper")) Snork_jumper(data);
            foreach (string str in data) if (str.Contains("section_name = snork_normal")) Snork_normal(data);
            foreach (string str in data) if (str.Contains("section_name = snork_outdoor")) Snork_outdoor(data);
            foreach (string str in data) if (str.Contains("section_name = snork_strong")) Snork_strong(data);
            foreach (string str in data) if (str.Contains("section_name = snork_weak")) Snork_weak(data);
            foreach (string str in data) if (str.Contains("section_name = psy_dog")) Psy_dog(data);
            foreach (string str in data) if (str.Contains("section_name = psy_dog_radar")) Psy_dog_radar(data);
            foreach (string str in data) if (str.Contains("section_name = tushkano_normal")) Tushkano_normal(data);
            foreach (string str in data) if (str.Contains("section_name = pseudodog_normal")) Pseudodog_normal(data);
            foreach (string str in data) if (str.Contains("section_name = pseudodog_strong")) Pseudodog_strong(data);
            foreach (string str in data) if (str.Contains("section_name = gunslinger_flash")) Punslinger_flash(data);
            foreach (string str in data) if (str.Contains("section_name = controller_tubeman")) Controller_tubeman(data);

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

            foreach (string str in data) if (str.Contains("section_name = bread")) Bread(data);
            foreach (string str in data) if (str.Contains("section_name = af_ameba_mica")) Af_ameba_mica(data);
            foreach (string str in data) if (str.Contains("section_name = af_ameba_slug")) Af_ameba_slug(data);
            foreach (string str in data) if (str.Contains("section_name = af_dummy_glassbeads")) Af_dummy_glassbeads(data);
            foreach (string str in data) if (str.Contains("section_name = af_dummy_pellicle")) Af_dummy_pellicle(data);
            foreach (string str in data) if (str.Contains("section_name = af_dummy_spring")) Af_dummy_spring(data);
            foreach (string str in data) if (str.Contains("section_name = af_electra_moonlight")) Af_electra_moonlight(data);
            foreach (string str in data) if (str.Contains("section_name = af_fireball")) Af_fireball(data);
            foreach (string str in data) if (str.Contains("section_name = af_fuzz_kolobok")) Af_fuzz_kolobok(data);
            foreach (string str in data) if (str.Contains("section_name = af_gold_fish")) Af_gold_fish(data);
            foreach (string str in data) if (str.Contains("section_name = af_night_star")) Af_night_star(data);
            foreach (string str in data) if (str.Contains("section_name = af_rusty_sea-urchin")) Af_rusty_sea_urchin(data);
            foreach (string str in data) if (str.Contains("section_name = ammo_11.43x23_hydro")) Ammo_11_43x23_hydro(data);
            foreach (string str in data) if (str.Contains("section_name = ammo_12x76_dart")) Ammo_12x76_dart(data);
            foreach (string str in data) if (str.Contains("section_name = ammo_12x76_zhekan")) Ammo_12x76_zhekan(data);
            foreach (string str in data) if (str.Contains("section_name = ammo_5.45x39_ap")) Ammo_5_45x39_ap(data);
            foreach (string str in data) if (str.Contains("section_name = ammo_5.56x45_ap")) Ammo_5_56x45_ap(data);
            foreach (string str in data) if (str.Contains("section_name = ammo_5.56x45_ss190")) Ammo_5_56x45_ss190(data);
            foreach (string str in data) if (str.Contains("section_name = ammo_9x19_pbp")) Ammo_9x19_pbp(data);
            foreach (string str in data) if (str.Contains("section_name = ammo_9x39_pab9")) Ammo_9x39_pab9(data);
            foreach (string str in data) if (str.Contains("section_name = ammo_og-7b")) Ammo_og_7b(data);
            foreach (string str in data) if (str.Contains("section_name = ammo_vog-25")) Ammo_vog_25(data);
            foreach (string str in data) if (str.Contains("section_name = bread_a")) Bread_a(data);
            foreach (string str in data) if (str.Contains("section_name = dolg_outfit")) Dolg_outfit(data);
            foreach (string str in data) if (str.Contains("section_name = exo_outfit")) Exo_outfit(data);
            foreach (string str in data) if (str.Contains("section_name = decoder")) Decoder(data);
            foreach (string str in data) if (str.Contains("section_name = military_outfit")) Military_outfit(data);
            foreach (string str in data) if (str.Contains("section_name = monolit_outfit")) Monolit_outfit(data);
            foreach (string str in data) if (str.Contains("section_name = medkit_scientic")) Medkit_scientic(data);
            foreach (string str in data) if (str.Contains("section_name = wpn_abakan")) Wpn_abakan(data);
            foreach (string str in data) if (str.Contains("section_name = wpn_ak74")) Wpn_ak74(data);
            foreach (string str in data) if (str.Contains("section_name = wpn_ak74_m1")) Wpn_ak74_m1(data);
            foreach (string str in data) if (str.Contains("section_name = wpn_binoc")) Wpn_binoc(data);
            foreach (string str in data) if (str.Contains("section_name = wpn_groza")) Wpn_groza(data);
            foreach (string str in data) if (str.Contains("section_name = wpn_lr300")) Wpn_lr300(data);
            foreach (string str in data) if (str.Contains("section_name = wpn_lr300_m1")) Wpn_lr300_m1(data);
            foreach (string str in data) if (str.Contains("section_name = wpn_mp5")) Wpn_mp5(data);
            foreach (string str in data) if (str.Contains("section_name = wpn_rg-6")) Wpn_rg_6(data);
            foreach (string str in data) if (str.Contains("section_name = wpn_rpg7")) Wpn_rpg7(data);
            foreach (string str in data) if (str.Contains("section_name = wpn_vintorez")) Wpn_vintorez(data);
            foreach (string str in data) if (str.Contains("section_name = guitar_a")) Guitar_a(data);
            foreach (string str in data) if (str.Contains("section_name = pri_decoder_documents")) Pri_decoder_documents(data);
            foreach (string str in data) if (str.Contains("section_name = svoboda_heavy_outfit")) Svoboda_heavy_outfit(data);
            foreach (string str in data) if (str.Contains("section_name = svoboda_light_outfit")) Svoboda_light_outfit(data);
            foreach (string str in data) if (str.Contains("section_name = hunters_toz")) Hunters_toz(data);
            foreach (string str in data) if (str.Contains("section_name = specops_outfit")) Specops_outfit(data);
            foreach (string str in data) if (str.Contains("section_name = stalker_outfit")) Stalker_outfit(data);
            foreach (string str in data) if (str.Contains("section_name = grenade_rgd5")) Grenade_rgd5(data);
            foreach (string str in data) if (str.Contains("section_name = killer_outfit")) Killer_outfit(data);
            foreach (string str in data) if (str.Contains("section_name = hand_radio")) Hand_radio(data);
            foreach (string str in data) if (str.Contains("section_name = quest_case_02")) Quest_case_02(data);
            foreach (string str in data) if (str.Contains("section_name = dar_document4")) Bar_document4(data);
            foreach (string str in data) if (str.Contains("section_name = ammo_9x19_fmj")) Ammo_9x19_fmj(data);
            foreach (string str in data) if (str.Contains("section_name = bandit_outfit")) Bandit_outfit(data);
            foreach (string str in data) if (str.Contains("section_name = wpn_walther")) Wpn_walther(data);
            foreach (string str in data) if (str.Contains("section_name = medkit_army")) Medkit_army(data);
            foreach (string str in data) if (str.Contains("section_name = quest_case_01")) Quest_case_01(data);
            foreach (string str in data) if (str.Contains("section_name = conserva")) Conserva(data);
            foreach (string str in data) if (str.Contains("section_name = vodka")) Vodka(data);
            foreach (string str in data) if (str.Contains("section_name = energy_drink")) Energy_drink(data);
            foreach (string str in data) if (str.Contains("section_name = kolbasa")) Kolbasa(data);
            foreach (string str in data) if (str.Contains("section_name = wpn_ak74u")) Wpn_ak74u(data);
            foreach (string str in data) if (str.Contains("section_name = ammo_9x18_fmj")) Ammo_9x18_fmj(data);
            foreach (string str in data) if (str.Contains("section_name = wpn_pm")) Wpn_pm(data);
            foreach (string str in data) if (str.Contains("section_name = ammo_9x18_pmm")) Ammo_9x18_pmm(data);
            foreach (string str in data) if (str.Contains("section_name = bandage")) Bandage(data);
            foreach (string str in data) if (str.Contains("section_name = af_blood")) Af_blood(data);
            foreach (string str in data) if (str.Contains("section_name = wpn_bm16")) Wpn_bm16(data);
            foreach (string str in data) if (str.Contains("section_name = ammo_12x70_buck")) Ammo_12x70_buck(data);
            foreach (string str in data) if (str.Contains("section_name = medkit") && !str.Contains("section_name = medkit_army")) Medkit(data);
            foreach (string str in data) if (str.Contains("section_name = af_medusa")) Af_medusa(data);
            foreach (string str in data) if (str.Contains("section_name = outfit_bandit_m1")) Outfit_bandit_m1(data);
            foreach (string str in data) if (str.Contains("section_name = af_electra_flash")) Af_electra_flash(data);
            foreach (string str in data) if (str.Contains("section_name = af_cristall_flower")) Af_cristall_flower(data);
            foreach (string str in data) if (str.Contains("section_name = af_electra_sparkler")) Af_electra_sparkler(data);
            foreach (string str in data) if (str.Contains("section_name = af_dummy_battery")) Af_dummy_battery(data);
            foreach (string str in data) if (str.Contains("section_name = af_dummy_dummy")) Af_dummy_dummy(data);
            foreach (string str in data) if (str.Contains("section_name = af_gravi")) Af_gravi(data);
            foreach (string str in data) if (str.Contains("section_name = af_mincer_meat")) Af_mincer_meat(data);
            foreach (string str in data) if (str.Contains("section_name = af_vyvert")) Af_vyvert(data);
            foreach (string str in data) if (str.Contains("section_name = ammo_11.43x23_fmj")) Ammo_11_43x23_fmj(data);
            foreach (string str in data) if (str.Contains("section_name = ammo_5.45x39_fmj")) Ammo_5_45x39_fmj(data);
            foreach (string str in data) if (str.Contains("section_name = antirad")) Antirad(data);
            foreach (string str in data) if (str.Contains("section_name = grenade_f1")) Grenade_f1(data);

            foreach (string str in data) if (str.Contains("section_name = explosive_barrel")) Explosive_barrel(data);
            foreach (string str in data) if (str.Contains("section_name = explosive_mobiltank")) Explosive_mobiltank(data);
            foreach (string str in data) if (str.Contains("section_name = explosive_barrel_low")) Explosive_barrel_low(data);
            foreach (string str in data) if (str.Contains("section_name = explosive_dinamit")) Explosive_dinamit(data);
            foreach (string str in data) if (str.Contains("section_name = explosive_fuelcan")) Explosive_fuelcan(data);
            foreach (string str in data) if (str.Contains("section_name = explosive_tank")) Explosive_tank(data);

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

        //items
        static void Bread(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = bread"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Kolbasa(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = kolbasa"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Wpn_ak74u(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = wpn_ak74u"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Ammo_9x18_fmj(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = ammo_9x18_fmj"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Wpn_pm(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = wpn_pm"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Ammo_9x18_pmm(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = ammo_9x18_pmm"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Bandage(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = bandage"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Af_blood(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = af_blood"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Conserva(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = conserva"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Vodka(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = vodka"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Energy_drink(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = energy_drink"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Wpn_bm16(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = wpn_bm16"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Ammo_12x70_buck(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = ammo_12x70_buck"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Medkit(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = medkit"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Af_medusa(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = af_medusa"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Af_electra_sparkler(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = af_electra_sparkler"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Wpn_walther(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = wpn_walther"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Ammo_9x19_fmj(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = ammo_9x19_fmj"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Af_electra_flash(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = af_electra_flash"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Af_cristall_flower(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = af_cristall_flower"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Medkit_army(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = medkit_army"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Quest_case_01(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = quest_case_01"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Outfit_bandit_m1(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = outfit_bandit_m1"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Bandit_outfit(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = bandit_outfit"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Af_dummy_battery(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = af_dummy_battery"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Af_dummy_dummy(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = af_dummy_dummy"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Af_gravi(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = af_gravi"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Af_mincer_meat(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = af_mincer_meat"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Af_vyvert(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = af_vyvert"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Ammo_11_43x23_fmj(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = ammo_11.43x23_fmj"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Ammo_5_45x39_fmj(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = ammo_5.45x39_fmj"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Antirad(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = antirad"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Grenade_f1(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = grenade_f1"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }

        static void Af_ameba_mica(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = af_ameba_mica"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Af_ameba_slug(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = af_ameba_slug"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Af_dummy_glassbeads(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = af_dummy_glassbeads"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Af_dummy_pellicle(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = af_dummy_pellicle"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Af_dummy_spring(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = af_dummy_spring"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Af_electra_moonlight(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = af_electra_moonlight"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Af_fireball(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = af_fireball"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Af_fuzz_kolobok(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = af_fuzz_kolobok"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Af_gold_fish(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = af_gold_fish"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Af_night_star(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = af_night_star"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Af_rusty_sea_urchin(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = af_rusty_sea-urchin"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Ammo_11_43x23_hydro(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = ammo_11.43x23_hydro"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Ammo_12x76_dart(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = ammo_12x76_dart"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Ammo_12x76_zhekan(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = ammo_12x76_zhekan"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Ammo_5_45x39_ap(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = ammo_5.45x39_ap"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Ammo_5_56x45_ap(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = ammo_5.56x45_ap"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Ammo_5_56x45_ss190(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = ammo_5.56x45_ss190"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Ammo_9x19_pbp(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = ammo_9x19_pbp"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Ammo_9x39_pab9(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = ammo_9x39_pab9"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Ammo_og_7b(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = ammo_og-7b"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Ammo_vog_25(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = ammo_vog-25"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Bread_a(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = bread_a"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Dolg_outfit(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = dolg_outfit"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Exo_outfit(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = exo_outfit"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Decoder(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = decoder"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Military_outfit(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = military_outfit"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Monolit_outfit(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = monolit_outfit"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Medkit_scientic(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = medkit_scientic"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Wpn_abakan(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = wpn_abakan"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Wpn_ak74(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = wpn_ak74"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Wpn_ak74_m1(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = wpn_ak74_m1"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Wpn_binoc(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = wpn_binoc"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Wpn_groza(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = wpn_groza"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Wpn_lr300(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = wpn_lr300"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Wpn_lr300_m1(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = wpn_lr300_m1"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Wpn_mp5(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = wpn_mp5"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Wpn_rg_6(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = wpn_rg-6"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Wpn_rpg7(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = wpn_rpg7"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Wpn_vintorez(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = wpn_vintorez"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Guitar_a(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = guitar_a"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Pri_decoder_documents(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = pri_decoder_documents"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Svoboda_heavy_outfit(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = svoboda_heavy_outfit"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Svoboda_light_outfit(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = svoboda_light_outfit"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Hunters_toz(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = hunters_toz"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Specops_outfit(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = specops_outfit"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Stalker_outfit(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = stalker_outfit"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Grenade_rgd5(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = grenade_rgd5"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Killer_outfit(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = killer_outfit"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Hand_radio(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = hand_radio"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Quest_case_02(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = quest_case_02"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }
        static void Bar_document4(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = dar_document4"))
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
            }
            string resultString = "";
            foreach (string st in resultData) resultString += st + ",";
            items.Add(resultString);
        }

        //monster
        static void Dog_strong(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = dog_strong"))
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
            monster.Add(resultString);
        }
        static void Dog_normal(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = dog_normal"))
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
            monster.Add(resultString);
        }
        static void Pseudodog_weak(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = pseudodog_weak"))
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
            monster.Add(resultString);
        }
        static void Boar_normal(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = boar_normal"))
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
            monster.Add(resultString);
        }
        static void Dog_weak(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = dog_weak"))
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
            monster.Add(resultString);
        }
        static void Boar_weak(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = boar_weak"))
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
            monster.Add(resultString);
        }
        static void Flesh_weak(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = flesh_weak"))
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
            monster.Add(resultString);
        }
        static void Bloodsucker_normal(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = bloodsucker_normal"))
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
            monster.Add(resultString);
        }
        static void Bloodsucker_strong(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = bloodsucker_strong"))
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
            monster.Add(resultString);
        }
        static void Boar_strong(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = boar_strong"))
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
            monster.Add(resultString);
        }
        static void Flesh_normal(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = flesh_normal"))
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
            monster.Add(resultString);
        }
        static void Gigant_normal(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = gigant_normal"))
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
            monster.Add(resultString);
        }
        static void Gigant_strong(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = gigant_strong"))
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
            monster.Add(resultString);
        }
        static void Snork_jumper(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = snork_jumper"))
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
            monster.Add(resultString);
        }
        static void Snork_normal(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = snork_normal"))
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
            monster.Add(resultString);
        }
        static void Snork_outdoor(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = snork_outdoor"))
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
            monster.Add(resultString);
        }
        static void Snork_strong(List<string> objects)
        {
            List<string> resultData = new List<string>();
            foreach (string str in objects)
            {
                if (str.Contains("section_name = snork_strong"))
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
            monster.
