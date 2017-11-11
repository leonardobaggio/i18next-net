using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace i18next_net
{
    public class InitOptions
    {
        public string fallbackLng { get; set; }
        public string defaultNS { get; set; }
        public LocaleFileTypeEnum localeFileType { get; set; }
        public string localesPath { get; set; }
        public bool preLoadLocales { get; set; }
        public bool autoDetect { get; set; }
    }
}
