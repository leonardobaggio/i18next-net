using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace i18next_net
{
    public class i18next
    {
        public InitOptions _config { get; set; }
        public string currentLanguage { get; set; }

        Dictionary<string, string> loadedLocales = new Dictionary<string, string>();

        public string path { get; set; }

        public i18next(InitOptions config)
        {
            _config = config;

            if (_config.autoDetect)
            {
                //todo: implement autodetect using CurrentThread Culture blablabla
            }

            if (string.IsNullOrWhiteSpace(_config.fallbackLng))
            {
                throw new Exception("No fallback language defined");
            }
         

            this.path = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory + "\\" + (_config.localesPath ?? "locales") + "\\";


            if (_config.preLoadLocales)
            {
                LoadAllLocales();
            }


        }

        private void LoadAllLocales()
        {
            var files_found = Directory.EnumerateFiles(this.path, "*.json| *.JSON", SearchOption.AllDirectories);

            foreach (var item in files_found)
            {
                var file = new FileInfo(item);

                var lng = file.DirectoryName;
                var ns = file.FullName.Replace(file.Extension, "");

                LoadLocale(lng, ns);
            }
        }

        public void changeLanguage(string locale)
        {
            if (string.IsNullOrWhiteSpace(locale))
            {
                throw new Exception("Invalid language");
            }



            currentLanguage = locale;
        }

        public void LoadLocale(string lng, string ns)
        {
            string key = lng + "_" + ns;
            if (loadedLocales.ContainsKey(key))
            {
                return;
            }

            switch (_config.localeFileType)
            {
                case LocaleFileTypeEnum.Resource:
                    throw new NotImplementedException("Loading using Resource file not implemented");
                case LocaleFileTypeEnum.Path:

                    string file_path = this.path + lng + "\\" + ns + ".json";
                    if (!File.Exists(file_path))
                    {
                        throw new FileNotFoundException("Locale file not found");
                    }

                    string json = File.ReadAllText(file_path);
                    loadedLocales.Add(key, json);

                    break;
                case LocaleFileTypeEnum.Web:
                    throw new NotImplementedException("Loading using Web path not implemented");
                default:
                    break;
            }
        }

        public string t(string key, object transpose_prop)
        {
            // split the key
            var array_key = key.Split(':');

            // get the namespace
            string ns = array_key[0];

            LoadLocale(currentLanguage, ns);


            JToken key_value;

            key_value = GetKeyValue(array_key, ns);

            if (key_value == null)
            {
                //load fallback language
                LoadLocale(_config.fallbackLng, ns);

            }
            string res = key_value.ToString();

            //continue only if transpose object exists
            if (transpose_prop == null)
            {
                return res;
            }


            //hat trick to serialize an anonymous 
            var transpose_obj = Newtonsoft.Json.JsonConvert.SerializeObject(transpose_prop);
            JObject transpose = JObject.Parse(transpose_obj);

            foreach (var item in transpose)
            {
                var str_to_replace = "{{" + item.Key + "}}";
                res = res.Replace(str_to_replace, item.Value.ToString());
            }

            return res;
        }

        private JToken GetKeyValue(string[] array_key, string ns)
        {
            JToken key_value;
            string json = loadedLocales[currentLanguage + "_" + ns];
            JObject o = JObject.Parse(json);

            key_value = o.SelectToken("$." + array_key[1]);
            return key_value;
        }
    }
}
