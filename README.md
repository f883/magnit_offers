# magnit_offers
Magnit offers is software to simplify access to this grocery shop actual offers. It created by C#, WPF and HtmlAgilityPack library. It is my first university course work.

**Installation file is "Magnic actions setup.exe". It works on .Net Framework 4.7.**

In project "GUI_main_WPF" there are main application and source code. Program collects actual offers from grocery shop site and post it in app. Wrote using WPF and HtmlAgilityPack.



In "GetAllCities" there are cities and regions parser. It collects list of region:city and then save it via C# serialization for using in main program. Wrote using HtmlAgilityPack library. 

**Parsed cities are in file "regions.xml".**

Main program window
![program window](/main_window.png)
