# i18next-net
.NET C# class for basic i18next functionality

# Basic usage

In this minimal example, we create a i18n instance with InitOptions configuration in its construtor. Since no path have been specified, the i18next-net will load files from the default directory `\locales\` relative to current running assembly:

### Locale file
```json
{
  "CONTENT": {
    "COMMENT_ADD": "Commented \"{{text}}\" on {{type}} \"{{title}}\""
  }
}
```
### Code
```csharp
i18next i18n = new i18next(new InitOptions()
{
    defaultNS = "common",
    localeFileType = LocaleFileTypeEnum.Path,
    fallbackLng = "en"
});


i18n.changeLanguage("pt");
var res = i18n.t("activities:CONTENT.COMMENT_ADD", new {text= "blablabla", type = "a pendencia", title = "Revisar orçamento"  });
System.Console.WriteLine(res);

i18n.changeLanguage("en");
var res2 = i18n.t("activities:CONTENT.COMMENT_ADD", new { text = "blablabla", type = "a pendencia", title = "Revisar orçamento" });
System.Console.WriteLine(res2);

System.Console.ReadKey();
```

### Output
![image](https://user-images.githubusercontent.com/18285859/32694100-5a7ed83e-c71e-11e7-8be3-0e551702f49a.png)


# To-do
- [ ] Project structure
  - [X] Solution and project
  - [ ] NuGet specification
  - [ ] Basic Documentation
  - [ ] Test project
  - [X] Examples
  - [ ] Advanced Documentation
- [X] Read locales files
  - [X] Pre Load  option for all files locales files available
  - [ ] Read locales from "Resource Files"
  - [ ] Read locales from remote server / web
- [X] Translate method with basic Interpolation
- [X] Fallback capability
- [ ] Pluralization
- [ ] Auto detect current language
- [ ] Configurations
- [ ] Advanced features
