using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace i18next_net.Examples.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            i18next i18n = new i18next(new InitOptions()
            {
                defaultNS = "common",
                localeFileType = LocaleFileTypeEnum.Path,
                fallbackLng = "en"
            });


            i18n.changeLanguage("pt");
            var res = i18n.t("activities:CONTENT.COMMENT_ADD", new {text= "blablbla", type = "a pendencia", title = "Revisar orçamento"  });
            System.Console.WriteLine(res);

            i18n.changeLanguage("en");
            var res2 = i18n.t("activities:CONTENT.COMMENT_ADD", new { text = "blablbla", type = "a pendencia", title = "Revisar orçamento" });
            System.Console.WriteLine(res2);

            System.Console.ReadKey();

        }
    }
}
